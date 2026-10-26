using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using GestorIncidencias.Controlador;
using GestorIncidencias.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestorIncidencias.Tests.Controlador
{
    [TestClass]
    public class XmlEmpleadoRepositoryTests
    {
        [TestMethod]
        public void CargarConArchivoAusenteDevuelveColeccionVacia()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var repositorio = CrearRepositorio(ruta);

                var resultado = repositorio.Cargar();

                Assert.IsTrue(resultado.Exito);
                Assert.IsNotNull(resultado.Datos);
                Assert.AreEqual(0, resultado.Datos.Count);
                Assert.IsTrue(repositorio.PuedeGuardar);
            });
        }

        [TestMethod]
        public void GuardarEmpleadoValidoDebePersistirlo()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var repositorio = CrearRepositorio(ruta);

                var guardado = repositorio.Guardar(new[] { CrearEmpleado(1, "Ana Torres", true, true) });

                Assert.IsTrue(guardado.Exito);
                Assert.IsTrue(File.Exists(ObtenerRutaArchivo(ruta)));
            });
        }

        [TestMethod]
        public void GuardarYReabrirRecuperaEmpleadoGuardado()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var fecha = new DateTime(2026, 10, 30, 8, 45, 0);
                var repositorio = CrearRepositorio(ruta);
                var guardado = repositorio.Guardar(new[]
                {
                    new Empleado(7, "Carlos Peña", true, true, fecha)
                });

                var recargado = CrearRepositorio(ruta).Cargar();

                Assert.IsTrue(guardado.Exito);
                Assert.IsTrue(recargado.Exito);
                Assert.AreEqual(1, recargado.Datos.Count);
                Assert.AreEqual(7, recargado.Datos[0].Identificador);
                Assert.AreEqual("Carlos Peña", recargado.Datos[0].NombreCompleto);
            });
        }

        [TestMethod]
        public void GuardarYReabrirVariosEmpleadosConservaRegistros()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var repositorio = CrearRepositorio(ruta);
                var guardado = repositorio.Guardar(new[]
                {
                    CrearEmpleado(1, "Ana Torres", true, true),
                    CrearEmpleado(2, "Luis Pérez", false, true),
                    CrearEmpleado(3, "Marta Rojas", true, false)
                });

                var recargado = CrearRepositorio(ruta).Cargar();

                Assert.IsTrue(guardado.Exito);
                Assert.IsTrue(recargado.Exito);
                Assert.AreEqual(3, recargado.Datos.Count);
                Assert.AreEqual(1, recargado.Datos[0].Identificador);
                Assert.AreEqual(2, recargado.Datos[1].Identificador);
                Assert.AreEqual(3, recargado.Datos[2].Identificador);
            });
        }

        [TestMethod]
        public void GuardarYReabrirConservaIdentificadorYFechaCreacion()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var fecha = new DateTime(2026, 10, 30, 9, 10, 0);
                var repositorio = CrearRepositorio(ruta);
                var guardado = repositorio.Guardar(new[]
                {
                    new Empleado(11, "Laura Mejía", true, true, fecha)
                });

                var recargado = CrearRepositorio(ruta).Cargar();

                Assert.IsTrue(guardado.Exito);
                Assert.IsTrue(recargado.Exito);
                Assert.AreEqual(11, recargado.Datos[0].Identificador);
                Assert.AreEqual(fecha, recargado.Datos[0].FechaCreacion);
            });
        }

        [TestMethod]
        public void GuardarYReabrirConservaActivoYEsTecnicoSoporte()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var repositorio = CrearRepositorio(ruta);
                var guardado = repositorio.Guardar(new[]
                {
                    CrearEmpleado(1, "Ana Torres", true, false),
                    CrearEmpleado(2, "Luis Pérez", false, true)
                });

                var recargado = CrearRepositorio(ruta).Cargar();

                Assert.IsTrue(guardado.Exito);
                Assert.IsTrue(recargado.Exito);
                Assert.IsTrue(recargado.Datos[0].EsTecnicoSoporte);
                Assert.IsFalse(recargado.Datos[0].Activo);
                Assert.IsFalse(recargado.Datos[1].EsTecnicoSoporte);
                Assert.IsTrue(recargado.Datos[1].Activo);
            });
        }

        [TestMethod]
        public void GuardarConDatosInvalidosDebeFallarSinSobrescribirArchivoExistente()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var rutaArchivo = ObtenerRutaArchivo(ruta);
                var repositorio = CrearRepositorio(ruta);
                var guardadoInicial = repositorio.Guardar(new[] { CrearEmpleado(1, "Inicial", true, true) });
                Assert.IsTrue(guardadoInicial.Exito);
                var contenidoAnterior = File.ReadAllText(rutaArchivo);

                var guardadoInvalido = repositorio.Guardar(new[] { CrearEmpleado(2, " ", true, true) });

                Assert.IsFalse(guardadoInvalido.Exito);
                Assert.AreEqual(contenidoAnterior, File.ReadAllText(rutaArchivo));
            });
        }

        [TestMethod]
        public void CargarConXmlCorruptoDebeFallarYBloquearSobrescrituraAutomatica()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var rutaArchivo = ObtenerRutaArchivo(ruta);
                File.WriteAllText(rutaArchivo, "<EmpleadosPersistidos><Version>1");

                var repositorio = CrearRepositorio(ruta);
                var carga = repositorio.Cargar();
                var guardado = repositorio.Guardar(new[] { CrearEmpleado(1, "Ana", true, true) });

                Assert.IsFalse(carga.Exito);
                Assert.IsFalse(repositorio.PuedeGuardar);
                Assert.IsFalse(guardado.Exito);
            });
        }

        [TestMethod]
        public void CargarConCamposInvalidosDebeFallar()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var rutaArchivo = ObtenerRutaArchivo(ruta);
                var repositorio = CrearRepositorio(ruta);
                var guardado = repositorio.Guardar(new[] { CrearEmpleado(1, "Ana Torres", true, true) });
                Assert.IsTrue(guardado.Exito);

                CorromperPrimerNodo(rutaArchivo, "FechaCreacion", "no-es-fecha");

                var carga = CrearRepositorio(ruta).Cargar();

                Assert.IsFalse(carga.Exito);
                Assert.AreEqual(0, carga.Datos.Count);
            });
        }

        [TestMethod]
        public void GuardarConArchivoBloqueadoDebeFallarYPreservarDatosAnteriores()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var rutaArchivo = ObtenerRutaArchivo(ruta);
                var repositorio = CrearRepositorio(ruta);
                repositorio.Guardar(new[] { CrearEmpleado(1, "Inicial", true, true) });
                var contenidoAnterior = File.ReadAllText(rutaArchivo);

                ResultadoOperacion guardado;
                using (new FileStream(rutaArchivo, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    guardado = repositorio.Guardar(new[] { CrearEmpleado(2, "Nuevo", true, true) });
                }

                Assert.IsFalse(guardado.Exito);
                Assert.AreEqual(contenidoAnterior, File.ReadAllText(rutaArchivo));

                var recargado = CrearRepositorio(ruta).Cargar();
                Assert.IsTrue(recargado.Exito);
                Assert.AreEqual(1, recargado.Datos.Count);
                Assert.AreEqual(1, recargado.Datos[0].Identificador);
            });
        }

        [TestMethod]
        public void GuardarConFalloControladoEnValidacionTemporalDebeConservarPrincipalYDevolverError()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var rutaArchivo = ObtenerRutaArchivo(ruta);
                var rutaRespaldo = rutaArchivo + ".bak";
                CrearRepositorio(ruta).Guardar(new[] { CrearEmpleado(1, "Inicial", true, true) });
                var contenidoAnterior = File.ReadAllText(rutaArchivo);

                var repositorio = new XmlEmpleadoRepositoryConFalloValidacionTemporal(rutaArchivo);
                var guardado = repositorio.Guardar(new[] { CrearEmpleado(2, "Nuevo", true, true) });

                Assert.IsFalse(guardado.Exito);
                Assert.AreEqual(contenidoAnterior, File.ReadAllText(rutaArchivo));
                Assert.IsFalse(File.Exists(rutaRespaldo));

                var recargado = CrearRepositorio(ruta).Cargar();
                Assert.IsTrue(recargado.Exito);
                Assert.AreEqual(1, recargado.Datos.Count);
                Assert.AreEqual(1, recargado.Datos[0].Identificador);
            });
        }

        [TestMethod]
        public void GuardarYCargarRepetidosNoPierdeRegistros()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var repositorio = CrearRepositorio(ruta);
                var primerGuardado = repositorio.Guardar(new[]
                {
                    CrearEmpleado(1, "Ana Torres", true, true),
                    CrearEmpleado(2, "Luis Pérez", false, true)
                });

                var cargaIntermedia = CrearRepositorio(ruta).Cargar();
                Assert.IsTrue(primerGuardado.Exito);
                Assert.IsTrue(cargaIntermedia.Exito);
                Assert.AreEqual(2, cargaIntermedia.Datos.Count);

                var segundoGuardado = CrearRepositorio(ruta).Guardar(new[]
                {
                    CrearEmpleado(1, "Ana Torres", true, true),
                    CrearEmpleado(2, "Luis Pérez", false, true),
                    CrearEmpleado(3, "Marta Rojas", true, false)
                });

                var cargaFinal = CrearRepositorio(ruta).Cargar();

                Assert.IsTrue(segundoGuardado.Exito);
                Assert.IsTrue(cargaFinal.Exito);
                Assert.AreEqual(3, cargaFinal.Datos.Count);
                Assert.AreEqual(1, cargaFinal.Datos[0].Identificador);
                Assert.AreEqual(2, cargaFinal.Datos[1].Identificador);
                Assert.AreEqual(3, cargaFinal.Datos[2].Identificador);
            });
        }

        private static XmlEmpleadoRepository CrearRepositorio(string directorio)
        {
            return new XmlEmpleadoRepository(ObtenerRutaArchivo(directorio));
        }

        private static string ObtenerRutaArchivo(string directorio)
        {
            return Path.Combine(directorio, "empleados.xml");
        }

        private static Empleado CrearEmpleado(int id, string nombre, bool esTecnico, bool activo)
        {
            return new Empleado(id, nombre, esTecnico, activo, new DateTime(2026, 10, 31, 8, 0, 0).AddMinutes(id));
        }

        private static void CorromperPrimerNodo(string rutaArchivo, string nombreNodo, string nuevoValor)
        {
            var doc = XDocument.Load(rutaArchivo);
            var nodo = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == nombreNodo);
            Assert.IsNotNull(nodo, "No se encontró el nodo esperado para corrupción controlada.");
            nodo.Value = nuevoValor;
            doc.Save(rutaArchivo);
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

        private class XmlEmpleadoRepositoryConFalloValidacionTemporal : XmlEmpleadoRepository
        {
            public XmlEmpleadoRepositoryConFalloValidacionTemporal(string rutaArchivo)
                : base(rutaArchivo)
            {
            }

            protected override void ValidarArchivoTemporal(string rutaTemporal)
            {
                Assert.IsTrue(File.Exists(rutaTemporal), "El archivo temporal debe existir antes de validar.");
                throw new InvalidOperationException("Fallo controlado en validación de temporal.");
            }
        }
    }
}
