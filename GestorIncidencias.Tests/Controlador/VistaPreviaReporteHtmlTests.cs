using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using GestorIncidencias;
using GestorIncidencias.Controlador;
using GestorIncidencias.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestorIncidencias.Tests.Controlador
{
    [TestClass]
    public class VistaPreviaReporteHtmlTests
    {
        [TestMethod]
        public void ConstruirDocumentoHtmlConIncidenciasDebeIncluirSoloLaColeccionRecibida()
        {
            var fecha = new DateTime(2026, 11, 1, 10, 30, 0);
            var filtradas = new List<Incidencia>
            {
                CrearIncidencia(7, "Error filtrado", "Marta", "Luis", Prioridad.Alta, EstadoIncidencia.EnProceso, fecha)
            };

            var html = ConstruirHtml(filtradas, fecha);

            StringAssert.Contains(html, "Total de incidencias incluidas: 1");
            StringAssert.Contains(html, "<td>7</td>");
            StringAssert.Contains(html, "Error filtrado");
            Assert.IsFalse(
                html.Contains("Incidencia ajena"),
                "El HTML no debe incluir incidencias fuera de la colección filtrada recibida.");
        }

        [TestMethod]
        public void ConstruirDocumentoHtmlDebeEscaparCaracteresEspeciales()
        {
            var fecha = new DateTime(2026, 11, 1, 11, 0, 0);
            var incidencias = new List<Incidencia>
            {
                CrearIncidencia(
                    8,
                    "<Error> & \"critico\"",
                    "Ana <QA>",
                    "Luis & Equipo",
                    Prioridad.Media,
                    EstadoIncidencia.Pendiente,
                    fecha)
            };

            var html = ConstruirHtml(incidencias, fecha);

            StringAssert.Contains(html, "&lt;Error&gt; &amp; &quot;critico&quot;");
            Assert.IsFalse(html.Contains("<Error>"), "El título no debe quedar sin escape en el HTML generado.");
            StringAssert.Contains(html, "Ana &lt;QA&gt;");
            StringAssert.Contains(html, "Luis &amp; Equipo");
        }

        [TestMethod]
        public void ConstruirDocumentoHtmlConReportadoPorLegacyDebeRepresentarloSinMutarFuente()
        {
            var fecha = new DateTime(2026, 11, 1, 11, 30, 0);
            var incidencia = CrearIncidencia(9, "Legacy", null, "Sofia", Prioridad.Baja, EstadoIncidencia.Resuelta, fecha);
            var incidencias = new List<Incidencia> { incidencia };

            var html = ConstruirHtml(incidencias, fecha);

            StringAssert.Contains(html, "No informado (registro legado)");
            Assert.IsTrue(string.IsNullOrWhiteSpace(incidencia.ReportadoPor));
        }

        [TestMethod]
        public void ConstruirDocumentoHtmlSinIncidenciasDebeMostrarEstadoVacio()
        {
            var fecha = new DateTime(2026, 11, 1, 12, 0, 0);

            var html = ConstruirHtml(new List<Incidencia>(), fecha);

            StringAssert.Contains(html, "Total de incidencias incluidas: 0");
            StringAssert.Contains(html, "No hay incidencias para mostrar en la vista previa del reporte.");
            Assert.IsFalse(
                html.Contains("<tbody>"),
                "Cuando no hay incidencias no debe renderizarse la sección tbody de la tabla.");
        }

        [TestMethod]
        public void ExportarHtmlDebeGuardarArchivoEnUtf8YConservarDatos()
        {
            EjecutarEnDirectorioTemporal(directorio =>
            {
                var fecha = new DateTime(2026, 11, 1, 12, 30, 0);
                var ruta = Path.Combine(directorio, "reporte.html");
                var incidencias = new List<Incidencia>
                {
                    CrearIncidencia(15, "Título ñ", "María", "José", Prioridad.Alta, EstadoIncidencia.EnProceso, fecha)
                };

                var resultado = ExportarHtml(ruta, incidencias, fecha);
                var bytes = File.ReadAllBytes(ruta);
                var contenido = File.ReadAllText(ruta, Encoding.UTF8);
                var contenidoDecodificado = WebUtility.HtmlDecode(contenido);

                Assert.IsTrue(resultado.Exito);
                Assert.IsTrue(File.Exists(ruta));
                CollectionAssert.AreEqual(Encoding.UTF8.GetPreamble(), SubArray(bytes, 0, Encoding.UTF8.GetPreamble().Length));
                StringAssert.Contains(contenido, "<meta charset=\"utf-8\" />");
                StringAssert.Contains(contenido, "<td>15</td>");
                StringAssert.Contains(contenidoDecodificado, "Título ñ");
                StringAssert.Contains(contenidoDecodificado, "María");
                StringAssert.Contains(contenidoDecodificado, "José");
            });
        }

        [TestMethod]
        public void ExportarHtmlConRutaInvalidaDebeDevolverErrorSinFalsoExito()
        {
            var fecha = new DateTime(2026, 11, 1, 13, 0, 0);
            var incidencias = new List<Incidencia>
            {
                CrearIncidencia(21, "Incidencia válida", "Carla", "Tomás", Prioridad.Media, EstadoIncidencia.Pendiente, fecha)
            };

            var resultado = ExportarHtml("*ruta-invalida*.html", incidencias, fecha);

            Assert.IsFalse(resultado.Exito);
            StringAssert.Contains(resultado.Mensaje, "No se pudo exportar el reporte a HTML.");
        }

        private static string ConstruirHtml(IList<Incidencia> incidencias, DateTime fechaGeneracion)
        {
            var metodo = typeof(frmVistaPreviaReporte).GetMethod("ConstruirDocumentoHtml", BindingFlags.Static | BindingFlags.NonPublic);
            Assert.IsNotNull(metodo, "No se encontró el método interno para construir HTML.");

            return (string)metodo.Invoke(null, new object[] { incidencias, fechaGeneracion });
        }

        private static ResultadoOperacion ExportarHtml(string rutaArchivo, IList<Incidencia> incidencias, DateTime fechaGeneracion)
        {
            var metodo = typeof(frmVistaPreviaReporte).GetMethod("ExportarHtml", BindingFlags.Static | BindingFlags.NonPublic);
            Assert.IsNotNull(metodo, "No se encontró el método interno para exportar HTML.");

            return (ResultadoOperacion)metodo.Invoke(null, new object[] { rutaArchivo, incidencias, fechaGeneracion });
        }

        private static Incidencia CrearIncidencia(
            int id,
            string titulo,
            string reportadoPor,
            string responsable,
            Prioridad prioridad,
            EstadoIncidencia estado,
            DateTime fecha)
        {
            return new Incidencia
            {
                Identificador = id,
                Titulo = titulo,
                Descripcion = "Descripción de prueba para reporte.",
                ReportadoPor = reportadoPor,
                Responsable = responsable,
                Prioridad = prioridad,
                Estado = estado,
                FechaCreacion = fecha
            };
        }

        private static byte[] SubArray(byte[] origen, int inicio, int longitud)
        {
            var resultado = new byte[longitud];
            Array.Copy(origen, inicio, resultado, 0, longitud);
            return resultado;
        }

        private static void EjecutarEnDirectorioTemporal(Action<string> accion)
        {
            var directorio = Path.Combine(Path.GetTempPath(), "GestorIncidencias.Tests", Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(directorio);

            try
            {
                accion(directorio);
            }
            finally
            {
                if (Directory.Exists(directorio))
                {
                    Directory.Delete(directorio, true);
                }
            }
        }
    }
}
