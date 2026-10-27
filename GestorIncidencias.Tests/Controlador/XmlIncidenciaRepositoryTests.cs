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
        public void CargarXmlLegacySinReportadoPorDebeConservarDatosYSinInventarNombre()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var repositorio = CrearRepositorio(ruta);
                var guardado = repositorio.Guardar(new[]
                {
                    new Incidencia
                    {
                        Identificador = 11,
                        Titulo = "Incidencia legacy",
                        Descripcion = "Descripción legacy válida.",
                        Responsable = "Ana",
                        Prioridad = Prioridad.Media,
                        Estado = EstadoIncidencia.Pendiente,
                        FechaCreacion = new DateTime(2026, 10, 30, 13, 30, 0)
                    }
                });

                Assert.IsTrue(guardado.Exito);

                var rutaArchivo = ObtenerRutaArchivo(ruta);
                var doc = XDocument.Load(rutaArchivo);
                doc.Descendants().Where(x => x.Name.LocalName == "ReportadoPor").Remove();
                doc.Save(rutaArchivo);

                var xmlLegacy = File.ReadAllText(rutaArchivo);
                Assert.IsFalse(xmlLegacy.Contains("ReportadoPor"));

                var recargado = CrearRepositorio(ruta).Cargar();

                Assert.IsTrue(recargado.Exito);
                Assert.AreEqual(1, recargado.Datos.Count);
                Assert.AreEqual(11, recargado.Datos[0].Identificador);
                Assert.AreEqual("Incidencia legacy", recargado.Datos[0].Titulo);
                Assert.AreEqual("Descripción legacy válida.", recargado.Datos[0].Descripcion);
                Assert.AreEqual("Ana", recargado.Datos[0].Responsable);
                Assert.IsTrue(string.IsNullOrWhiteSpace(recargado.Datos[0].ReportadoPor));
                Assert.AreEqual(xmlLegacy, File.ReadAllText(rutaArchivo));
            });
        }

        [TestMethod]
        public void GuardarYReabrirConReportadoPorDebePreservarValorEnEsquemaAmpliado()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var repositorio = CrearRepositorio(ruta);
                var guardado = repositorio.Guardar(new[]
                {
                    new Incidencia
                    {
                        Identificador = 21,
                        Titulo = "Incidencia con reportado por",
                        Descripcion = "Descripción para persistencia ampliada.",
                        ReportadoPor = "María",
                        Responsable = "Luis",
                        Prioridad = Prioridad.Alta,
                        Estado = EstadoIncidencia.EnProceso,
                        FechaCreacion = new DateTime(2026, 10, 30, 14, 45, 0)
                    }
                });

                var recargado = CrearRepositorio(ruta).Cargar();

                Assert.IsTrue(guardado.Exito);
                Assert.IsTrue(recargado.Exito);
                Assert.AreEqual(1, recargado.Datos.Count);
                Assert.AreEqual(21, recargado.Datos[0].Identificador);
                Assert.AreEqual("Incidencia con reportado por", recargado.Datos[0].Titulo);
                Assert.AreEqual("Descripción para persistencia ampliada.", recargado.Datos[0].Descripcion);
                Assert.AreEqual("Luis", recargado.Datos[0].Responsable);
                Assert.AreEqual(Prioridad.Alta, recargado.Datos[0].Prioridad);
                Assert.AreEqual(EstadoIncidencia.EnProceso, recargado.Datos[0].Estado);
                Assert.AreEqual(new DateTime(2026, 10, 30, 14, 45, 0), recargado.Datos[0].FechaCreacion);
                Assert.AreEqual("María", recargado.Datos[0].ReportadoPor);
            });
        }

        [TestMethod]
        public void GuardarYReabrirConResponsableEmpleadoIdDebePreservarVinculoSinAlterarTextos()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var repositorio = CrearRepositorio(ruta);
                var guardado = repositorio.Guardar(new[]
                {
                    new Incidencia
                    {
                        Identificador = 41,
                        Titulo = "Incidencia vinculada",
                        Descripcion = "Descripción de incidencia vinculada.",
                        ReportadoPor = "María",
                        Responsable = "Luis",
                        ResponsableEmpleadoId = 7,
                        Prioridad = Prioridad.Alta,
                        Estado = EstadoIncidencia.EnProceso,
                        FechaCreacion = new DateTime(2026, 10, 30, 15, 10, 0)
                    }
                });

                var recargado = CrearRepositorio(ruta).Cargar();

                Assert.IsTrue(guardado.Exito);
                Assert.IsTrue(recargado.Exito);
                Assert.AreEqual(1, recargado.Datos.Count);
                Assert.AreEqual(7, recargado.Datos[0].ResponsableEmpleadoId);
                Assert.AreEqual("María", recargado.Datos[0].ReportadoPor);
                Assert.AreEqual("Luis", recargado.Datos[0].Responsable);
            });
        }

        [TestMethod]
        public void CargarXmlLegacySinResponsableEmpleadoIdDebeMantenerCompatibilidad()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var repositorio = CrearRepositorio(ruta);
                var guardado = repositorio.Guardar(new[]
                {
                    new Incidencia
                    {
                        Identificador = 42,
                        Titulo = "Incidencia sin vínculo",
                        Descripcion = "Descripción legacy sin vínculo.",
                        ReportadoPor = "María",
                        Responsable = "Luis",
                        ResponsableEmpleadoId = 3,
                        Prioridad = Prioridad.Media,
                        Estado = EstadoIncidencia.Pendiente,
                        FechaCreacion = new DateTime(2026, 10, 30, 16, 10, 0)
                    }
                });

                Assert.IsTrue(guardado.Exito);

                var rutaArchivo = ObtenerRutaArchivo(ruta);
                var doc = XDocument.Load(rutaArchivo);
                doc.Descendants().Where(x => x.Name.LocalName == "ResponsableEmpleadoId").Remove();
                doc.Save(rutaArchivo);

                var recargado = CrearRepositorio(ruta).Cargar();

                Assert.IsTrue(recargado.Exito);
                Assert.AreEqual(1, recargado.Datos.Count);
                Assert.IsNull(recargado.Datos[0].ResponsableEmpleadoId);
                Assert.AreEqual("María", recargado.Datos[0].ReportadoPor);
                Assert.AreEqual("Luis", recargado.Datos[0].Responsable);
            });
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-8)]
        public void CargarConResponsableEmpleadoIdInvalidoDebeFallar(int responsableEmpleadoId)
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var rutaArchivo = ObtenerRutaArchivo(ruta);
                var guardado = CrearRepositorio(ruta).Guardar(new[]
                {
                    new Incidencia
                    {
                        Identificador = 43,
                        Titulo = "Incidencia inválida",
                        Descripcion = "Descripción válida para corrupción controlada.",
                        Responsable = "Ana",
                        ResponsableEmpleadoId = 7,
                        Prioridad = Prioridad.Media,
                        Estado = EstadoIncidencia.Pendiente,
                        FechaCreacion = new DateTime(2026, 10, 30, 17, 10, 0)
                    }
                });

                Assert.IsTrue(guardado.Exito);

                var xmlGenerado = File.ReadAllText(rutaArchivo);
                StringAssert.Contains(xmlGenerado, "ResponsableEmpleadoId");

                CorromperPrimerNodo(rutaArchivo, "ResponsableEmpleadoId", responsableEmpleadoId.ToString());

                var carga = CrearRepositorio(ruta).Cargar();

                Assert.IsFalse(carga.Exito);
                Assert.IsNotNull(carga.Errores);
                Assert.AreEqual(0, carga.Datos.Count);
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

        [TestMethod]
        public void CargarConXmlDanadoDebeFallarYBloquearSobrescrituraAutomatica()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var rutaArchivo = ObtenerRutaArchivo(ruta);
                CrearRepositorio(ruta).Guardar(new[] { CrearIncidencia(1, "Inicial") });

                File.WriteAllText(rutaArchivo, "<IncidenciasPersistidas>");
                var repositorio = CrearRepositorio(ruta);

                var carga = repositorio.Cargar();
                var guardado = repositorio.Guardar(new[] { CrearIncidencia(2, "Nueva") });

                Assert.IsFalse(carga.Exito);
                Assert.IsFalse(repositorio.PuedeGuardar);
                Assert.IsFalse(guardado.Exito);
                Assert.AreEqual("<IncidenciasPersistidas>", File.ReadAllText(rutaArchivo));
            });
        }

        [TestMethod]
        public void CargarConTiposDeDatosInvalidosDebeFallar()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var rutaArchivo = ObtenerRutaArchivo(ruta);
                CrearRepositorio(ruta).Guardar(new[] { CrearIncidencia(1, "Inicial") });

                CorromperPrimerNodo(rutaArchivo, "FechaCreacion", "no-es-fecha");

                var carga = CrearRepositorio(ruta).Cargar();

                Assert.IsFalse(carga.Exito);
                Assert.IsNotNull(carga.Errores);
                Assert.AreEqual(0, carga.Datos.Count);
            });
        }

        [TestMethod]
        public void CargarConIdentificadoresDuplicadosDebeFallarYPreservarArchivoOriginal()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var rutaArchivo = ObtenerRutaArchivo(ruta);
                CrearRepositorio(ruta).Guardar(new[]
                {
                    CrearIncidencia(1, "Primera"),
                    CrearIncidencia(2, "Segunda")
                });

                CorromperIdentificadoresDuplicados(rutaArchivo);
                var contenidoCorrupto = File.ReadAllText(rutaArchivo);

                var carga = CrearRepositorio(ruta).Cargar();

                Assert.IsFalse(carga.Exito);
                Assert.AreEqual(contenidoCorrupto, File.ReadAllText(rutaArchivo));
            });
        }

        [TestMethod]
        public void CargarConPrioridadInvalidaDebeFallar()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var rutaArchivo = ObtenerRutaArchivo(ruta);
                CrearRepositorio(ruta).Guardar(new[] { CrearIncidencia(1, "Inicial") });

                CorromperPrimerNodo(rutaArchivo, "Prioridad", "99");

                var repositorio = CrearRepositorio(ruta);
                var carga = repositorio.Cargar();

                Assert.IsFalse(carga.Exito);
                Assert.IsFalse(repositorio.PuedeGuardar);
            });
        }

        [TestMethod]
        public void CargarConEstadoInvalidoDebeFallar()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var rutaArchivo = ObtenerRutaArchivo(ruta);
                CrearRepositorio(ruta).Guardar(new[] { CrearIncidencia(1, "Inicial") });

                CorromperPrimerNodo(rutaArchivo, "Estado", "99");

                var repositorio = CrearRepositorio(ruta);
                var carga = repositorio.Cargar();

                Assert.IsFalse(carga.Exito);
                Assert.IsFalse(repositorio.PuedeGuardar);
            });
        }

        [TestMethod]
        public void GuardarConArchivoBloqueadoDebeFallarYPreservarDatosAnteriores()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var rutaArchivo = ObtenerRutaArchivo(ruta);
                var repositorio = CrearRepositorio(ruta);
                repositorio.Guardar(new[] { CrearIncidencia(1, "Inicial") });
                var contenidoAnterior = File.ReadAllText(rutaArchivo);

                using (new FileStream(rutaArchivo, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    var guardado = repositorio.Guardar(new[] { CrearIncidencia(2, "Cambio") });
                    Assert.IsFalse(guardado.Exito);
                }

                var recargado = CrearRepositorio(ruta).Cargar();

                Assert.IsTrue(recargado.Exito);
                Assert.AreEqual(1, recargado.Datos.Count);
                Assert.AreEqual(1, recargado.Datos[0].Identificador);
                Assert.AreEqual(contenidoAnterior, File.ReadAllText(rutaArchivo));
            });
        }

        [TestMethod]
        public void GuardarSobreArchivoExistenteDebeCrearRespaldoYReemplazarContenidoPrincipal()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var rutaArchivo = ObtenerRutaArchivo(ruta);
                var rutaRespaldo = rutaArchivo + ".bak";
                var repositorio = CrearRepositorio(ruta);

                repositorio.Guardar(new[] { CrearIncidencia(1, "Inicial") });
                var contenidoPrincipalAnterior = File.ReadAllText(rutaArchivo);

                var guardado = repositorio.Guardar(new[] { CrearIncidencia(2, "Nuevo") });
                var contenidoPrincipalActual = File.ReadAllText(rutaArchivo);

                Assert.IsTrue(guardado.Exito);
                Assert.IsTrue(File.Exists(rutaRespaldo));
                Assert.AreEqual(contenidoPrincipalAnterior, File.ReadAllText(rutaRespaldo));
                Assert.AreNotEqual(contenidoPrincipalAnterior, contenidoPrincipalActual);

                var recargado = CrearRepositorio(ruta).Cargar();
                Assert.IsTrue(recargado.Exito);
                Assert.AreEqual(1, recargado.Datos.Count);
                Assert.AreEqual(2, recargado.Datos[0].Identificador);
            });
        }

        [TestMethod]
        public void GuardarExitosoNoDebeDejarArchivoTemporal()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var rutaArchivo = ObtenerRutaArchivo(ruta);
                var rutaTemporal = rutaArchivo + ".tmp";
                var repositorio = CrearRepositorio(ruta);

                var guardado = repositorio.Guardar(new[] { CrearIncidencia(1, "Temporal") });

                Assert.IsTrue(guardado.Exito);
                Assert.IsFalse(File.Exists(rutaTemporal));
                Assert.IsTrue(File.Exists(rutaArchivo));
            });
        }

        [TestMethod]
        public void GuardarConFalloControladoEnValidacionTemporalDebeConservarPrincipalYDevolverError()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var rutaArchivo = ObtenerRutaArchivo(ruta);
                var rutaRespaldo = rutaArchivo + ".bak";

                CrearRepositorio(ruta).Guardar(new[] { CrearIncidencia(1, "Inicial") });
                var contenidoAnterior = File.ReadAllText(rutaArchivo);

                var repositorio = new XmlIncidenciaRepositoryConFalloValidacionTemporal(rutaArchivo);
                var guardado = repositorio.Guardar(new[] { CrearIncidencia(2, "Nuevo") });

                Assert.IsFalse(guardado.Exito);
                Assert.AreEqual(contenidoAnterior, File.ReadAllText(rutaArchivo));

                var recargado = CrearRepositorio(ruta).Cargar();
                Assert.IsTrue(recargado.Exito);
                Assert.AreEqual(1, recargado.Datos.Count);
                Assert.AreEqual(1, recargado.Datos[0].Identificador);

                Assert.IsFalse(File.Exists(rutaRespaldo));
            });
        }

        [TestMethod]
        public void RegistrarConRepositorioXmlEnNuevasInstanciasConservaIdentificadoresYCampos()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var controladorPrimeraSesion = new IncidenciaController(CrearRepositorio(ruta));
                var alta1 = controladorPrimeraSesion.Registrar(
                    "Incidencia uno",
                    "Descripción válida uno.",
                    "Ana",
                    Prioridad.Media);

                var controladorSegundaSesion = new IncidenciaController(CrearRepositorio(ruta));
                var alta2 = controladorSegundaSesion.Registrar(
                    "Incidencia dos",
                    "Descripción válida dos.",
                    "Luis",
                    Prioridad.Alta);

                var recargado = CrearRepositorio(ruta).Cargar();

                Assert.IsTrue(alta1.Exito);
                Assert.IsTrue(alta2.Exito);
                Assert.AreEqual(1, alta1.Datos.Identificador);
                Assert.AreEqual(2, alta2.Datos.Identificador);

                Assert.IsTrue(recargado.Exito);
                Assert.AreEqual(2, recargado.Datos.Count);
                Assert.AreEqual("Incidencia uno", recargado.Datos[0].Titulo);
                Assert.AreEqual("Descripción válida uno.", recargado.Datos[0].Descripcion);
                Assert.AreEqual("Ana", recargado.Datos[0].Responsable);
                Assert.AreEqual(Prioridad.Media, recargado.Datos[0].Prioridad);

                Assert.AreEqual("Incidencia dos", recargado.Datos[1].Titulo);
                Assert.AreEqual("Descripción válida dos.", recargado.Datos[1].Descripcion);
                Assert.AreEqual("Luis", recargado.Datos[1].Responsable);
                Assert.AreEqual(Prioridad.Alta, recargado.Datos[1].Prioridad);
            });
        }

        [TestMethod]
        public void RegistrarConErrorRealDeGuardadoNoDebeReportarExitoNiPerderDatosPersistidos()
        {
            EjecutarEnDirectorioTemporal(ruta =>
            {
                var rutaArchivo = ObtenerRutaArchivo(ruta);
                var controladorInicial = new IncidenciaController(CrearRepositorio(ruta));
                var altaInicial = controladorInicial.Registrar(
                    "Incidencia previa",
                    "Descripción previa válida.",
                    "Ana",
                    Prioridad.Media);

                Assert.IsTrue(altaInicial.Exito);
                var contenidoAnterior = File.ReadAllText(rutaArchivo);

                ResultadoOperacion<Incidencia> altaConFallo;
                using (new FileStream(rutaArchivo, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    var controladorBloqueado = new IncidenciaController(CrearRepositorio(ruta));
                    altaConFallo = controladorBloqueado.Registrar(
                        "Incidencia nueva",
                        "Descripción nueva válida.",
                        "Luis",
                        Prioridad.Alta);
                }

                var recargado = CrearRepositorio(ruta).Cargar();

                Assert.IsFalse(altaConFallo.Exito);
                Assert.IsNotNull(altaConFallo.Mensaje);
                Assert.IsTrue(recargado.Exito);
                Assert.AreEqual(1, recargado.Datos.Count);
                Assert.AreEqual(1, recargado.Datos[0].Identificador);
                Assert.AreEqual(contenidoAnterior, File.ReadAllText(rutaArchivo));
            });
        }

        private static XmlIncidenciaRepository CrearRepositorio(string directorio)
        {
            return new XmlIncidenciaRepository(ObtenerRutaArchivo(directorio));
        }

        private static string ObtenerRutaArchivo(string directorio)
        {
            return Path.Combine(directorio, "incidencias.xml");
        }

        private static Incidencia CrearIncidencia(int id, string titulo)
        {
            return new Incidencia
            {
                Identificador = id,
                Titulo = titulo,
                Descripcion = "Descripción válida para persistencia.",
                Responsable = "Ana",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = new DateTime(2026, 10, 30, 9, 0, 0).AddMinutes(id)
            };
        }

        private static void CorromperPrimerNodo(string rutaArchivo, string nombreNodo, string nuevoValor)
        {
            var doc = XDocument.Load(rutaArchivo);
            var nodo = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == nombreNodo);
            Assert.IsNotNull(nodo, "No se encontró el nodo esperado para corrupción controlada.");
            nodo.Value = nuevoValor;
            doc.Save(rutaArchivo);
        }

        private static void CorromperIdentificadoresDuplicados(string rutaArchivo)
        {
            var doc = XDocument.Load(rutaArchivo);
            var ids = doc.Descendants()
                .Where(x => x.Name.LocalName == "Identificador")
                .Take(2)
                .ToList();

            Assert.AreEqual(2, ids.Count, "Se esperaban al menos dos nodos Identificador para forzar duplicados.");
            ids[0].Value = "1";
            ids[1].Value = "1";
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

        private class XmlIncidenciaRepositoryConFalloValidacionTemporal : XmlIncidenciaRepository
        {
            public XmlIncidenciaRepositoryConFalloValidacionTemporal(string rutaArchivo)
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
