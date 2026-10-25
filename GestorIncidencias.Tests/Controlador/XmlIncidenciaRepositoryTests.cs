using System;
using System.IO;
using GestorIncidencias.Controlador;
using GestorIncidencias.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestorIncidencias.Tests.Controlador
{
    [TestClass]
    public class XmlIncidenciaRepositoryTests
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
            });
        }

        [TestMethod]
        public void GuardarYReabrirRecuperaExactamenteLosSieteCampos()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var fecha = new DateTime(2026, 10, 30, 10, 15, 0);
                var repositorio = CrearRepositorio(ruta);
                var guardado = repositorio.Guardar(new[]
                {
                    new Incidencia
                    {
                        Identificador = 1,
                        Titulo = "Error login",
                        Descripcion = "No permite iniciar sesión.",
                        Responsable = "Ana",
                        Prioridad = Prioridad.Alta,
                        Estado = EstadoIncidencia.EnProceso,
                        FechaCreacion = fecha
                    }
                });

                var recargado = CrearRepositorio(ruta).Cargar();

                Assert.IsTrue(guardado.Exito);
                Assert.AreEqual(1, recargado.Datos.Count);
                Assert.AreEqual(1, recargado.Datos[0].Identificador);
                Assert.AreEqual("Error login", recargado.Datos[0].Titulo);
                Assert.AreEqual("No permite iniciar sesión.", recargado.Datos[0].Descripcion);
                Assert.AreEqual("Ana", recargado.Datos[0].Responsable);
                Assert.AreEqual(Prioridad.Alta, recargado.Datos[0].Prioridad);
                Assert.AreEqual(EstadoIncidencia.EnProceso, recargado.Datos[0].Estado);
                Assert.AreEqual(fecha, recargado.Datos[0].FechaCreacion);
            });
        }

        [TestMethod]
        public void GuardarReemplazaColeccionCompletaSinDuplicados()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var repositorio = CrearRepositorio(ruta);
                repositorio.Guardar(new[]
                {
                    new Incidencia
                    {
                        Identificador = 1,
                        Titulo = "Primera incidencia",
                        Descripcion = "Descripción válida 1.",
                        Responsable = "Ana",
                        Prioridad = Prioridad.Media,
                        Estado = EstadoIncidencia.Pendiente,
                        FechaCreacion = new DateTime(2026, 10, 30, 9, 0, 0)
                    }
                });

                repositorio.Guardar(new[]
                {
                    new Incidencia
                    {
                        Identificador = 2,
                        Titulo = "Segunda incidencia",
                        Descripcion = "Descripción válida 2.",
                        Responsable = "Luis",
                        Prioridad = Prioridad.Baja,
                        Estado = EstadoIncidencia.Resuelta,
                        FechaCreacion = new DateTime(2026, 10, 30, 11, 0, 0)
                    }
                });

                var recargado = CrearRepositorio(ruta).Cargar();

                Assert.AreEqual(1, recargado.Datos.Count);
                Assert.AreEqual(2, recargado.Datos[0].Identificador);
            });
        }

        [TestMethod]
        public void CargarDevuelveCopiasIndependientesDeListaEIncidencias()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var repositorio = CrearRepositorio(ruta);
                repositorio.Guardar(new[]
                {
                    new Incidencia
                    {
                        Identificador = 3,
                        Titulo = "Incidencia base",
                        Descripcion = "Descripción base válida.",
                        Responsable = "Carlos",
                        Prioridad = Prioridad.Media,
                        Estado = EstadoIncidencia.Pendiente,
                        FechaCreacion = new DateTime(2026, 10, 30, 12, 0, 0)
                    }
                });

                var primeraCarga = CrearRepositorio(ruta).Cargar();
                Assert.AreEqual(1, primeraCarga.Datos.Count);
                primeraCarga.Datos[0].Titulo = "Mutado";
                primeraCarga.Datos.Clear();

                var segundaCarga = CrearRepositorio(ruta).Cargar();

                Assert.AreEqual(1, segundaCarga.Datos.Count);
                Assert.AreEqual("Incidencia base", segundaCarga.Datos[0].Titulo);
            });
        }

        [TestMethod]
        public void RegistrarDespuesDeReabrirRecuperaSiguienteIdYConservaRegistros()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var repositorioInicial = CrearRepositorio(ruta);
                repositorioInicial.Guardar(new[]
                {
                    new Incidencia
                    {
                        Identificador = 1,
                        Titulo = "Incidencia previa",
                        Descripcion = "Descripción previa válida.",
                        Responsable = "Ana",
                        Prioridad = Prioridad.Media,
                        Estado = EstadoIncidencia.Pendiente,
                        FechaCreacion = new DateTime(2026, 10, 30, 8, 0, 0)
                    }
                });

                var controlador = new IncidenciaController(CrearRepositorio(ruta));
                var registro = controlador.Registrar(
                    "Incidencia nueva",
                    "Descripción nueva válida.",
                    "Luis",
                    Prioridad.Alta);

                var recargado = CrearRepositorio(ruta).Cargar();

                Assert.IsTrue(registro.Exito);
                Assert.IsNotNull(registro.Datos);
                Assert.AreEqual(2, registro.Datos.Identificador);
                Assert.AreEqual(2, recargado.Datos.Count);
            });
        }

        private static XmlIncidenciaRepository CrearRepositorio(string directorio)
        {
            var rutaArchivo = Path.Combine(directorio, "incidencias.xml");
            return new XmlIncidenciaRepository(rutaArchivo);
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
