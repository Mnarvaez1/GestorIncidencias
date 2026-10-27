using System;
using System.Linq;
using System.Reflection;
using GestorIncidencias;
using GestorIncidencias.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestorIncidencias.Tests.Controlador
{
    [TestClass]
    public class FrmEditarIncidenciaTecnicosTests
    {
        [TestMethod]
        public void ClonarIncidenciaDebeConservarResponsableEmpleadoId()
        {
            var original = new Incidencia
            {
                Identificador = 55,
                Titulo = "Incidencia",
                Descripcion = "Descripción válida para clonado.",
                ReportadoPor = "María",
                Responsable = "Luis",
                ResponsableEmpleadoId = 9,
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.EnProceso,
                FechaCreacion = new DateTime(2026, 11, 2, 10, 0, 0)
            };

            var metodo = typeof(frmEditarIncidencia).GetMethod(
                "ClonarIncidencia",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(metodo, "Se esperaba método de clonación para preservar campos en B2.");

            var clon = (Incidencia)metodo.Invoke(null, new object[] { original });

            Assert.IsNotNull(clon);
            Assert.AreEqual(9, clon.ResponsableEmpleadoId);
            Assert.AreEqual("María", clon.ReportadoPor);
            Assert.AreEqual("Luis", clon.Responsable);
            Assert.AreEqual(original.FechaCreacion, clon.FechaCreacion);
            Assert.AreNotSame(original, clon);
        }

        [TestMethod]
        public void ClonarIncidenciaConNuloDebeRetornarNulo()
        {
            var metodo = typeof(frmEditarIncidencia).GetMethod(
                "ClonarIncidencia",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(metodo, "Se esperaba método de clonación para preservar campos en B2.");

            var clon = (Incidencia)metodo.Invoke(null, new object[] { null });

            Assert.IsNull(clon);
        }

        [TestMethod]
        public void OpcionTecnicoResponsableHistoricaDebeMostrarPrefijoDeConservacion()
        {
            var tipoOpcion = typeof(frmEditarIncidencia)
                .GetNestedTypes(BindingFlags.NonPublic)
                .FirstOrDefault(t => t.Name == "OpcionTecnicoResponsable");

            Assert.IsNotNull(tipoOpcion, "Se esperaba tipo interno OpcionTecnicoResponsable en B2.");

            var instancia = Activator.CreateInstance(tipoOpcion, new object[] { 7, "Luis Pérez", true });

            Assert.IsNotNull(instancia);
            Assert.AreEqual("Conservar responsable actual: Luis Pérez", instancia.ToString());
        }
    }
}
