using System;
using System.Collections.Generic;
using System.Linq;
using GestorIncidencias.Controlador;
using GestorIncidencias.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestorIncidencias.Tests.Controlador
{
    [TestClass]
    public class IncidenciaControllerTests
    {
        [TestMethod]
        public void RegistrarIncidenciaValidaDevuelveIdFechaYEstadoInicial()
        {
            var repositorio = new IncidenciaRepositoryDoble();
            var resultado = CrearControlador(repositorio).Registrar(
                "Error de conexión",
                "La conexión falla al iniciar.",
                "Ana",
                Prioridad.Alta);

            Assert.IsTrue(resultado.Exito);
            Assert.IsNotNull(resultado.Datos);
            Assert.IsTrue(resultado.Datos.Identificador > 0);
            Assert.AreNotEqual(DateTime.MinValue, resultado.Datos.FechaCreacion);
            Assert.AreEqual(EstadoIncidencia.Pendiente, resultado.Datos.Estado);
            Assert.AreEqual(1, repositorio.Registros.Count);
        }

        [TestMethod]
        public void RegistrarIncidenciaRecortaTextosYConservaPrioridad()
        {
            var repositorio = new IncidenciaRepositoryDoble();
            var resultado = CrearControlador(repositorio).Registrar(
                "  Error de conexión  ",
                "  La conexión falla al iniciar.  ",
                "  Ana  ",
                Prioridad.Baja);

            Assert.IsTrue(resultado.Exito);
            Assert.IsNotNull(resultado.Datos);
            Assert.AreEqual("Error de conexión", resultado.Datos.Titulo);
            Assert.AreEqual("La conexión falla al iniciar.", resultado.Datos.Descripcion);
            Assert.AreEqual("Ana", resultado.Datos.Responsable);
            Assert.AreEqual(Prioridad.Baja, resultado.Datos.Prioridad);
        }

        [TestMethod]
        public void RegistrarConReportadoPorValidoDebeConservarloYDiferenciarloDeResponsable()
        {
            var repositorio = new IncidenciaRepositoryDoble();
            var resultado = RegistrarConReportadoPor(CrearControlador(repositorio),
                "Error de conexión",
                "La conexión falla al iniciar.",
                "María",
                "Ana",
                Prioridad.Alta);

            Assert.IsTrue(resultado.Exito);
            Assert.IsNotNull(resultado.Datos);
            Assert.AreEqual("María", resultado.Datos.ReportadoPor);
            Assert.AreEqual("Ana", resultado.Datos.Responsable);
            Assert.AreEqual(1, repositorio.Registros.Count);
            Assert.AreEqual("María", repositorio.Registros[0].ReportadoPor);
            Assert.AreEqual("Ana", repositorio.Registros[0].Responsable);
        }

        [TestMethod]
        public void RegistrarConVinculoResponsableEmpleadoDebeConservarResponsableYReportadoPor()
        {
            var repositorio = new IncidenciaRepositoryDoble();
            var empleados = new EmpleadoRepositoryDoble();
            empleados.Registros.Add(CrearEmpleado(25, "Ana", true, true));
            var resultado = RegistrarConReportadoPorYResponsableEmpleadoId(
                CrearControlador(repositorio, empleados),
                "Error de conexión",
                "La conexión falla al iniciar.",
                "María",
                "Ana",
                Prioridad.Alta,
                25);

            Assert.IsTrue(resultado.Exito);
            Assert.IsNotNull(resultado.Datos);
            Assert.AreEqual(25, resultado.Datos.ResponsableEmpleadoId);
            Assert.AreEqual("María", resultado.Datos.ReportadoPor);
            Assert.AreEqual("Ana", resultado.Datos.Responsable);
            Assert.AreEqual(25, repositorio.Registros[0].ResponsableEmpleadoId);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-2)]
        public void RegistrarConVinculoResponsableEmpleadoInvalidoDebeRechazarSinAgregarRegistros(int responsableEmpleadoId)
        {
            var repositorio = new IncidenciaRepositoryDoble();
            var resultado = RegistrarConReportadoPorYResponsableEmpleadoId(
                CrearControlador(repositorio),
                "Error de conexión",
                "La conexión falla al iniciar.",
                "María",
                "Ana",
                Prioridad.Alta,
                responsableEmpleadoId);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual(0, repositorio.Registros.Count);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("A")]
        public void RegistrarConReportadoPorInvalidoDebeRechazarSinAgregarRegistros(string reportadoPor)
        {
            var repositorio = new IncidenciaRepositoryDoble();
            var resultado = RegistrarConReportadoPor(CrearControlador(repositorio),
                "Error de conexión",
                "La conexión falla al iniciar.",
                reportadoPor,
                "Ana",
                Prioridad.Alta);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual(0, repositorio.Registros.Count);
        }

        [TestMethod]
        public void RegistrarTitulosDuplicadosAsignaIdentificadoresDistintos()
        {
            var repositorio = new IncidenciaRepositoryDoble();
            var controlador = CrearControlador(repositorio);
            var primero = controlador.Registrar("Error repetido", "Descripción válida.", "Ana", Prioridad.Media);
            var segundo = controlador.Registrar("Error repetido", "Otra descripción válida.", "Luis", Prioridad.Media);

            Assert.IsTrue(primero.Exito);
            Assert.IsTrue(segundo.Exito);
            Assert.IsNotNull(primero.Datos);
            Assert.IsNotNull(segundo.Datos);
            Assert.AreNotEqual(primero.Datos.Identificador, segundo.Datos.Identificador);
            Assert.AreEqual(2, repositorio.Registros.Count);
        }

        [TestMethod]
        public void RegistrarDatosInvalidosLosRechazaSinAgregarRegistros()
        {
            var repositorio = new IncidenciaRepositoryDoble();
            var resultado = CrearControlador(repositorio).Registrar(
                "cort",
                "Descripción válida.",
                "Ana",
                Prioridad.Media);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual(0, repositorio.Registros.Count);
        }

        [TestMethod]
        public void ConsultarSinRegistrosDevuelveColeccionVacia()
        {
            var repositorio = new IncidenciaRepositoryDoble();
            var resultado = CrearControlador(repositorio).Consultar();

            Assert.IsTrue(resultado.Exito);
            Assert.IsNotNull(resultado.Datos);
            Assert.AreEqual(0, resultado.Datos.Count);
        }

        [TestMethod]
        public void ConsultarRecuperaLosSieteCamposDeLaIncidencia()
        {
            var fecha = new DateTime(2026, 10, 25, 10, 30, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 7,
                Titulo = "Error de carga",
                Descripcion = "La carga inicial falla al arrancar.",
                Responsable = "Laura",
                Prioridad = Prioridad.Alta,
                Estado = EstadoIncidencia.EnProceso,
                FechaCreacion = fecha
            });

            var resultado = CrearControlador(repositorio).Consultar();

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(1, resultado.Datos.Count);
            Assert.AreEqual(7, resultado.Datos[0].Identificador);
            Assert.AreEqual("Error de carga", resultado.Datos[0].Titulo);
            Assert.AreEqual("La carga inicial falla al arrancar.", resultado.Datos[0].Descripcion);
            Assert.AreEqual("Laura", resultado.Datos[0].Responsable);
            Assert.AreEqual(Prioridad.Alta, resultado.Datos[0].Prioridad);
            Assert.AreEqual(EstadoIncidencia.EnProceso, resultado.Datos[0].Estado);
            Assert.AreEqual(fecha, resultado.Datos[0].FechaCreacion);
        }

        [TestMethod]
        public void ConsultarDebeIncluirReportadoPorYRepresentarLegacySinInventarDato()
        {
            var fecha = new DateTime(2026, 10, 25, 10, 30, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 7,
                Titulo = "Error de carga",
                Descripcion = "La carga inicial falla al arrancar.",
                ReportadoPor = "María",
                Responsable = "Laura",
                Prioridad = Prioridad.Alta,
                Estado = EstadoIncidencia.EnProceso,
                FechaCreacion = fecha
            });
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 8,
                Titulo = "Incidencia legacy",
                Descripcion = "Registro histórico sin reportado.",
                ReportadoPor = null,
                Responsable = "Luis",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = fecha.AddMinutes(1)
            });

            var resultado = CrearControlador(repositorio).Consultar();

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(2, resultado.Datos.Count);
            Assert.AreEqual(8, resultado.Datos[0].Identificador);
            Assert.AreEqual("No informado (registro legado)", resultado.Datos[0].ReportadoPor);
            Assert.AreEqual(7, resultado.Datos[1].Identificador);
            Assert.AreEqual("María", resultado.Datos[1].ReportadoPor);
        }

        [TestMethod]
        public void ConsultarOrdenaPorFechaDeCreacionDescendente()
        {
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 1,
                Titulo = "Primera",
                Descripcion = "Descripción válida 1.",
                Responsable = "Ana",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = new DateTime(2026, 10, 24, 8, 0, 0)
            });
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 2,
                Titulo = "Segunda",
                Descripcion = "Descripción válida 2.",
                Responsable = "Luis",
                Prioridad = Prioridad.Baja,
                Estado = EstadoIncidencia.EnProceso,
                FechaCreacion = new DateTime(2026, 10, 25, 8, 0, 0)
            });

            var resultado = CrearControlador(repositorio).Consultar();

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(2, resultado.Datos.Count);
            Assert.AreEqual(2, resultado.Datos[0].Identificador);
            Assert.AreEqual(1, resultado.Datos[1].Identificador);
        }

        [TestMethod]
        public void ConsultarConFechasIgualesDesempataPorIdDescendente()
        {
            var fecha = new DateTime(2026, 10, 25, 8, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 3,
                Titulo = "Tercera",
                Descripcion = "Descripción válida 3.",
                Responsable = "Ana",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = fecha
            });
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 8,
                Titulo = "Octava",
                Descripcion = "Descripción válida 8.",
                Responsable = "Luis",
                Prioridad = Prioridad.Alta,
                Estado = EstadoIncidencia.Resuelta,
                FechaCreacion = fecha
            });

            var resultado = CrearControlador(repositorio).Consultar();

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(2, resultado.Datos.Count);
            Assert.AreEqual(8, resultado.Datos[0].Identificador);
            Assert.AreEqual(3, resultado.Datos[1].Identificador);
        }

        [TestMethod]
        public void EditarActualizaCamposEditablesYConservaInvariantes()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 10,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                Responsable = "Ana",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.EnProceso,
                FechaCreacion = fechaOriginal
            });

            var resultado = CrearControlador(repositorio).Editar(
                10,
                "  Titulo actualizado  ",
                "  Descripción actualizada válida.  ",
                "  Luis  ",
                Prioridad.Alta);

            Assert.IsTrue(resultado.Exito);
            Assert.IsNotNull(resultado.Datos);
            Assert.AreEqual("Titulo actualizado", resultado.Datos.Titulo);
            Assert.AreEqual("Descripción actualizada válida.", resultado.Datos.Descripcion);
            Assert.AreEqual("Luis", resultado.Datos.Responsable);
            Assert.AreEqual(Prioridad.Alta, resultado.Datos.Prioridad);
            Assert.AreEqual(10, resultado.Datos.Identificador);
            Assert.AreEqual(fechaOriginal, resultado.Datos.FechaCreacion);
            Assert.AreEqual(EstadoIncidencia.EnProceso, resultado.Datos.Estado);
            Assert.AreEqual(1, repositorio.Registros.Count);
        }

        [TestMethod]
        public void EditarConReportadoPorValidoDebeActualizarloYConservarInvariantes()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 10,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                ReportadoPor = "Carlos",
                Responsable = "Ana",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.EnProceso,
                FechaCreacion = fechaOriginal
            });

            var resultado = EditarConReportadoPor(CrearControlador(repositorio),
                10,
                "Titulo actualizado",
                "Descripción actualizada válida.",
                "María",
                "Luis",
                Prioridad.Alta);

            Assert.IsTrue(resultado.Exito);
            Assert.IsNotNull(resultado.Datos);
            Assert.AreEqual("María", resultado.Datos.ReportadoPor);
            Assert.AreEqual("Luis", resultado.Datos.Responsable);
            Assert.AreEqual(10, resultado.Datos.Identificador);
            Assert.AreEqual(fechaOriginal, resultado.Datos.FechaCreacion);
            Assert.AreEqual(EstadoIncidencia.EnProceso, resultado.Datos.Estado);
        }

        [TestMethod]
        public void EditarConVinculoResponsableEmpleadoDebeActualizarIdSinAlterarTextos()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            var empleados = new EmpleadoRepositoryDoble();
            empleados.Registros.Add(CrearEmpleado(4, "Ana", true, true));
            empleados.Registros.Add(CrearEmpleado(9, "Luis", true, true));
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 10,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                ReportadoPor = "Carlos",
                Responsable = "Ana",
                ResponsableEmpleadoId = 4,
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.EnProceso,
                FechaCreacion = fechaOriginal
            });

            var resultado = EditarConReportadoPorYResponsableEmpleadoId(
                CrearControlador(repositorio, empleados),
                10,
                "Titulo actualizado",
                "Descripción actualizada válida.",
                "María",
                "Luis",
                Prioridad.Alta,
                9);

            Assert.IsTrue(resultado.Exito);
            Assert.IsNotNull(resultado.Datos);
            Assert.AreEqual(9, resultado.Datos.ResponsableEmpleadoId);
            Assert.AreEqual("María", resultado.Datos.ReportadoPor);
            Assert.AreEqual("Luis", resultado.Datos.Responsable);
            Assert.AreEqual(9, repositorio.Registros[0].ResponsableEmpleadoId);
        }

        [TestMethod]
        public void RegistrarConTecnicoActivoLibreDebePermitirAsignacion()
        {
            var repositorio = new IncidenciaRepositoryDoble();
            var empleados = new EmpleadoRepositoryDoble();
            empleados.Registros.Add(CrearEmpleado(7, "Michael", true, true));

            var resultado = RegistrarConReportadoPorYResponsableEmpleadoId(
                CrearControlador(repositorio, empleados),
                "Error de conexión",
                "La conexión falla al iniciar.",
                "María",
                "Michael",
                Prioridad.Alta,
                7);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(7, resultado.Datos.ResponsableEmpleadoId);
            Assert.AreEqual(1, repositorio.Registros.Count);
        }

        [TestMethod]
        public void RegistrarConTecnicoActivoOcupadoDebeRechazarAsignacionNueva()
        {
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 1,
                Titulo = "Abierta",
                Descripcion = "Descripción válida abierta.",
                ReportadoPor = "María",
                Responsable = "Michael",
                ResponsableEmpleadoId = 7,
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = new DateTime(2026, 10, 20, 9, 0, 0)
            });
            var empleados = new EmpleadoRepositoryDoble();
            empleados.Registros.Add(CrearEmpleado(7, "Michael", true, true));

            var resultado = RegistrarConReportadoPorYResponsableEmpleadoId(
                CrearControlador(repositorio, empleados),
                "Nueva abierta",
                "Descripción válida nueva.",
                "Laura",
                "Michael",
                Prioridad.Alta,
                7);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("El técnico seleccionado está ocupado y no puede recibir una nueva incidencia.", resultado.Mensaje);
            Assert.AreEqual(1, repositorio.Registros.Count);
        }

        [TestMethod]
        public void RegistrarConTecnicoInactivoDebeRechazarAsignacionNueva()
        {
            var repositorio = new IncidenciaRepositoryDoble();
            var empleados = new EmpleadoRepositoryDoble();
            empleados.Registros.Add(CrearEmpleado(8, "Laura", true, false));

            var resultado = RegistrarConReportadoPorYResponsableEmpleadoId(
                CrearControlador(repositorio, empleados),
                "Nueva",
                "Descripción válida nueva.",
                "María",
                "Laura",
                Prioridad.Media,
                8);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("El técnico seleccionado está inactivo y no puede recibir nuevas asignaciones.", resultado.Mensaje);
            Assert.AreEqual(0, repositorio.Registros.Count);
        }

        [TestMethod]
        public void RegistrarConEmpleadoNoTecnicoDebeRechazarAsignacionNueva()
        {
            var repositorio = new IncidenciaRepositoryDoble();
            var empleados = new EmpleadoRepositoryDoble();
            empleados.Registros.Add(CrearEmpleado(9, "Pedro", false, true));

            var resultado = RegistrarConReportadoPorYResponsableEmpleadoId(
                CrearControlador(repositorio, empleados),
                "Nueva",
                "Descripción válida nueva.",
                "María",
                "Pedro",
                Prioridad.Media,
                9);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("El empleado seleccionado no está habilitado como técnico de soporte.", resultado.Mensaje);
            Assert.AreEqual(0, repositorio.Registros.Count);
        }

        [TestMethod]
        public void EditarConMismoTecnicoOcupadoDebePermitirConservarAsignacion()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 10,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                ReportadoPor = "Carlos",
                Responsable = "Michael",
                ResponsableEmpleadoId = 7,
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.EnProceso,
                FechaCreacion = fechaOriginal
            });
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 11,
                Titulo = "Otra abierta",
                Descripcion = "Descripción válida abierta.",
                ReportadoPor = "María",
                Responsable = "Michael",
                ResponsableEmpleadoId = 7,
                Prioridad = Prioridad.Alta,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = fechaOriginal.AddMinutes(1)
            });
            var empleados = new EmpleadoRepositoryDoble();
            empleados.Registros.Add(CrearEmpleado(7, "Michael", true, true));

            var resultado = EditarConReportadoPorYResponsableEmpleadoId(
                CrearControlador(repositorio, empleados),
                10,
                "Titulo actualizado",
                "Descripción actualizada válida.",
                "María",
                "Michael",
                Prioridad.Alta,
                7);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(7, resultado.Datos.ResponsableEmpleadoId);
        }

        [TestMethod]
        public void EditarReasignandoAocupadoDebeRechazar()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 10,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                ReportadoPor = "Carlos",
                Responsable = "Laura",
                ResponsableEmpleadoId = 8,
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.EnProceso,
                FechaCreacion = fechaOriginal
            });
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 11,
                Titulo = "Abierta de Michael",
                Descripcion = "Descripción válida abierta.",
                ReportadoPor = "María",
                Responsable = "Michael",
                ResponsableEmpleadoId = 7,
                Prioridad = Prioridad.Alta,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = fechaOriginal.AddMinutes(1)
            });
            var empleados = new EmpleadoRepositoryDoble();
            empleados.Registros.Add(CrearEmpleado(7, "Michael", true, true));
            empleados.Registros.Add(CrearEmpleado(8, "Laura", true, true));

            var resultado = EditarConReportadoPorYResponsableEmpleadoId(
                CrearControlador(repositorio, empleados),
                10,
                "Titulo actualizado",
                "Descripción actualizada válida.",
                "María",
                "Michael",
                Prioridad.Alta,
                7);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("No se puede reasignar a un técnico ocupado. Seleccione un técnico activo y libre.", resultado.Mensaje);
            Assert.AreEqual(8, repositorio.Registros[0].ResponsableEmpleadoId);
        }

        [TestMethod]
        public void EditarReasignandoATecnicoLibreDebePermitir()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 10,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                ReportadoPor = "Carlos",
                Responsable = "Laura",
                ResponsableEmpleadoId = 8,
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.EnProceso,
                FechaCreacion = fechaOriginal
            });
            var empleados = new EmpleadoRepositoryDoble();
            empleados.Registros.Add(CrearEmpleado(7, "Michael", true, true));
            empleados.Registros.Add(CrearEmpleado(8, "Laura", true, true));

            var resultado = EditarConReportadoPorYResponsableEmpleadoId(
                CrearControlador(repositorio, empleados),
                10,
                "Titulo actualizado",
                "Descripción actualizada válida.",
                "María",
                "Michael",
                Prioridad.Alta,
                7);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(7, repositorio.Registros[0].ResponsableEmpleadoId);
        }

        [TestMethod]
        public void RegistrarConFalloAlCargarIncidenciasEnValidacionDebeRechazar()
        {
            var repositorio = new IncidenciaRepositoryDoble
            {
                CargarDebeFallar = true,
                CargarMensajeError = "Error real al cargar incidencias para disponibilidad."
            };
            var empleados = new EmpleadoRepositoryDoble();
            empleados.Registros.Add(CrearEmpleado(7, "Michael", true, true));

            var resultado = RegistrarConReportadoPorYResponsableEmpleadoId(
                CrearControlador(repositorio, empleados),
                "Nueva",
                "Descripción válida nueva.",
                "María",
                "Michael",
                Prioridad.Media,
                7);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("Error real al cargar incidencias para disponibilidad.", resultado.Mensaje);
        }

        [TestMethod]
        public void EditarConSobrecargaAnteriorDebeConservarVinculoExistente()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 10,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                ReportadoPor = "Carlos",
                Responsable = "Ana",
                ResponsableEmpleadoId = 17,
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.EnProceso,
                FechaCreacion = fechaOriginal
            });

            var resultado = EditarConReportadoPor(
                CrearControlador(repositorio),
                10,
                "Titulo actualizado",
                "Descripción actualizada válida.",
                "María",
                "Luis",
                Prioridad.Alta);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(17, resultado.Datos.ResponsableEmpleadoId);
            Assert.AreEqual(17, repositorio.Registros[0].ResponsableEmpleadoId);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("A")]
        public void EditarConReportadoPorInvalidoDebeRechazarYNoMutarRegistro(string reportadoPor)
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 10,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                ReportadoPor = "Carlos",
                Responsable = "Ana",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.EnProceso,
                FechaCreacion = fechaOriginal
            });

            var resultado = EditarConReportadoPor(CrearControlador(repositorio),
                10,
                "Titulo actualizado",
                "Descripción actualizada válida.",
                reportadoPor,
                "Luis",
                Prioridad.Alta);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("Carlos", repositorio.Registros[0].ReportadoPor);
            Assert.AreEqual("Ana", repositorio.Registros[0].Responsable);
            Assert.AreEqual("Titulo inicial", repositorio.Registros[0].Titulo);
        }

        [TestMethod]
        public void EditarMantieneIntactasLasDemasIncidenciasYSinRegistrosExtra()
        {
            var fechaPrimera = new DateTime(2026, 10, 20, 9, 0, 0);
            var fechaSegunda = new DateTime(2026, 10, 21, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 1,
                Titulo = "Primera",
                Descripcion = "Descripción válida de la primera.",
                Responsable = "Ana",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = fechaPrimera
            });
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 2,
                Titulo = "Segunda",
                Descripcion = "Descripción válida de la segunda.",
                Responsable = "Luis",
                Prioridad = Prioridad.Baja,
                Estado = EstadoIncidencia.Resuelta,
                FechaCreacion = fechaSegunda
            });

            var resultado = CrearControlador(repositorio).Editar(
                1,
                "Primera editada",
                "Descripción editada de la primera.",
                "Carlos",
                Prioridad.Alta);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(2, repositorio.Registros.Count);
            Assert.AreEqual("Primera editada", repositorio.Registros[0].Titulo);
            Assert.AreEqual("Segunda", repositorio.Registros[1].Titulo);
            Assert.AreEqual("Descripción válida de la segunda.", repositorio.Registros[1].Descripcion);
            Assert.AreEqual("Luis", repositorio.Registros[1].Responsable);
            Assert.AreEqual(Prioridad.Baja, repositorio.Registros[1].Prioridad);
            Assert.AreEqual(EstadoIncidencia.Resuelta, repositorio.Registros[1].Estado);
            Assert.AreEqual(fechaSegunda, repositorio.Registros[1].FechaCreacion);
        }

        [TestMethod]
        public void EditarConIdInexistenteRechazaYNoAlteraDatos()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 3,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                Responsable = "Ana",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = fechaOriginal
            });

            var resultado = CrearControlador(repositorio).Editar(
                99,
                "Titulo actualizado",
                "Descripción actualizada válida.",
                "Luis",
                Prioridad.Alta);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual(1, repositorio.Registros.Count);
            Assert.AreEqual("Titulo inicial", repositorio.Registros[0].Titulo);
            Assert.AreEqual("Descripción inicial válida.", repositorio.Registros[0].Descripcion);
            Assert.AreEqual("Ana", repositorio.Registros[0].Responsable);
            Assert.AreEqual(Prioridad.Media, repositorio.Registros[0].Prioridad);
            Assert.AreEqual(EstadoIncidencia.Pendiente, repositorio.Registros[0].Estado);
            Assert.AreEqual(fechaOriginal, repositorio.Registros[0].FechaCreacion);
        }

        [TestMethod]
        public void EditarConDatosInvalidosRechazaYConservaValoresOriginales()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 5,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                Responsable = "Ana",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.EnProceso,
                FechaCreacion = fechaOriginal
            });

            var resultado = CrearControlador(repositorio).Editar(
                5,
                "cort",
                "Descripción actualizada válida.",
                "Luis",
                Prioridad.Alta);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual(1, repositorio.Registros.Count);
            Assert.AreEqual("Titulo inicial", repositorio.Registros[0].Titulo);
            Assert.AreEqual("Descripción inicial válida.", repositorio.Registros[0].Descripcion);
            Assert.AreEqual("Ana", repositorio.Registros[0].Responsable);
            Assert.AreEqual(Prioridad.Media, repositorio.Registros[0].Prioridad);
            Assert.AreEqual(EstadoIncidencia.EnProceso, repositorio.Registros[0].Estado);
            Assert.AreEqual(fechaOriginal, repositorio.Registros[0].FechaCreacion);
        }

        [DataTestMethod]
        [DataRow(EstadoIncidencia.Pendiente, EstadoIncidencia.Pendiente, true, EstadoIncidencia.Pendiente)]
        [DataRow(EstadoIncidencia.Pendiente, EstadoIncidencia.EnProceso, true, EstadoIncidencia.EnProceso)]
        [DataRow(EstadoIncidencia.Pendiente, EstadoIncidencia.Resuelta, false, EstadoIncidencia.Pendiente)]
        [DataRow(EstadoIncidencia.EnProceso, EstadoIncidencia.Pendiente, false, EstadoIncidencia.EnProceso)]
        [DataRow(EstadoIncidencia.EnProceso, EstadoIncidencia.EnProceso, true, EstadoIncidencia.EnProceso)]
        [DataRow(EstadoIncidencia.EnProceso, EstadoIncidencia.Resuelta, true, EstadoIncidencia.Resuelta)]
        [DataRow(EstadoIncidencia.Resuelta, EstadoIncidencia.Pendiente, false, EstadoIncidencia.Resuelta)]
        [DataRow(EstadoIncidencia.Resuelta, EstadoIncidencia.EnProceso, true, EstadoIncidencia.EnProceso)]
        [DataRow(EstadoIncidencia.Resuelta, EstadoIncidencia.Resuelta, true, EstadoIncidencia.Resuelta)]
        public void CambiarEstadoRespetaTransicionesYConservaDatos(
            EstadoIncidencia estadoInicial,
            EstadoIncidencia estadoSolicitado,
            bool exitoEsperado,
            EstadoIncidencia estadoFinalEsperado)
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var fechaSecundaria = new DateTime(2026, 10, 21, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 11,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                Responsable = "Ana",
                Prioridad = Prioridad.Media,
                Estado = estadoInicial,
                FechaCreacion = fechaOriginal
            });
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 12,
                Titulo = "Incidencia secundaria",
                Descripcion = "Descripción secundaria válida.",
                Responsable = "Luis",
                Prioridad = Prioridad.Alta,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = fechaSecundaria
            });

            var resultado = CrearControlador(repositorio).CambiarEstado(11, estadoSolicitado);

            Assert.AreEqual(exitoEsperado, resultado.Exito);
            Assert.AreEqual(2, repositorio.Registros.Count);
            Assert.AreEqual(11, repositorio.Registros[0].Identificador);
            Assert.AreEqual("Titulo inicial", repositorio.Registros[0].Titulo);
            Assert.AreEqual("Descripción inicial válida.", repositorio.Registros[0].Descripcion);
            Assert.AreEqual("Ana", repositorio.Registros[0].Responsable);
            Assert.AreEqual(Prioridad.Media, repositorio.Registros[0].Prioridad);
            Assert.AreEqual(fechaOriginal, repositorio.Registros[0].FechaCreacion);
            Assert.AreEqual(estadoFinalEsperado, repositorio.Registros[0].Estado);

            Assert.AreEqual(12, repositorio.Registros[1].Identificador);
            Assert.AreEqual("Incidencia secundaria", repositorio.Registros[1].Titulo);
            Assert.AreEqual("Descripción secundaria válida.", repositorio.Registros[1].Descripcion);
            Assert.AreEqual("Luis", repositorio.Registros[1].Responsable);
            Assert.AreEqual(Prioridad.Alta, repositorio.Registros[1].Prioridad);
            Assert.AreEqual(EstadoIncidencia.Pendiente, repositorio.Registros[1].Estado);
            Assert.AreEqual(fechaSecundaria, repositorio.Registros[1].FechaCreacion);
        }

        [TestMethod]
        public void CambiarEstadoConIdInexistenteRechazaYNoAlteraDatos()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 3,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                Responsable = "Ana",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = fechaOriginal
            });

            var resultado = CrearControlador(repositorio).CambiarEstado(99, EstadoIncidencia.EnProceso);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual(1, repositorio.Registros.Count);
            Assert.AreEqual(EstadoIncidencia.Pendiente, repositorio.Registros[0].Estado);
            Assert.AreEqual("Titulo inicial", repositorio.Registros[0].Titulo);
            Assert.AreEqual(fechaOriginal, repositorio.Registros[0].FechaCreacion);
        }

        [TestMethod]
        public void CambiarEstadoConValorFueraDeEnumRechazaYNoAlteraDatos()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 4,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                Responsable = "Ana",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.EnProceso,
                FechaCreacion = fechaOriginal
            });

            var resultado = CrearControlador(repositorio).CambiarEstado(4, (EstadoIncidencia)99);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual(1, repositorio.Registros.Count);
            Assert.AreEqual(EstadoIncidencia.EnProceso, repositorio.Registros[0].Estado);
            Assert.AreEqual("Titulo inicial", repositorio.Registros[0].Titulo);
            Assert.AreEqual(fechaOriginal, repositorio.Registros[0].FechaCreacion);
        }

        [TestMethod]
        public void BuscarFiltrarSinRegistrosDevuelveColeccionVacia()
        {
            var repositorio = new IncidenciaRepositoryDoble();

            var resultado = CrearControlador(repositorio).Consultar("error", EstadoIncidencia.Pendiente);

            Assert.IsTrue(resultado.Exito);
            Assert.IsNotNull(resultado.Datos);
            Assert.AreEqual(0, resultado.Datos.Count);
        }

        [TestMethod]
        public void BuscarFiltrarCoincidenciaParcialPorTituloOResponsable()
        {
            var repositorio = CrearRepositorioParaBusqueda();

            var porTitulo = CrearControlador(repositorio).Consultar("error", null);
            var porResponsable = CrearControlador(repositorio).Consultar("lui", null);

            Assert.AreEqual(2, porTitulo.Datos.Count);
            Assert.AreEqual(5, porTitulo.Datos[0].Identificador);
            Assert.AreEqual(1, porTitulo.Datos[1].Identificador);
            Assert.AreEqual(1, porResponsable.Datos.Count);
            Assert.AreEqual(2, porResponsable.Datos[0].Identificador);
        }

        [TestMethod]
        public void BuscarFiltrarNoDistingueMayusculasYMinusculas()
        {
            var repositorio = CrearRepositorioParaBusqueda();

            var resultado = CrearControlador(repositorio).Consultar("aNa", null);

            Assert.AreEqual(1, resultado.Datos.Count);
            Assert.AreEqual(1, resultado.Datos[0].Identificador);
        }

        [TestMethod]
        public void BuscarFiltrarCoincidenciaSoloEnDescripcionNoIncluyeRegistro()
        {
            var repositorio = CrearRepositorioParaBusqueda();

            var resultado = CrearControlador(repositorio).Consultar("autenticación", null);

            Assert.AreEqual(0, resultado.Datos.Count);
        }

        [TestMethod]
        public void BuscarFiltrarCombinaBusquedaYFiltroConAnd()
        {
            var repositorio = CrearRepositorioParaBusqueda();

            var resultado = CrearControlador(repositorio).Consultar("error", EstadoIncidencia.Pendiente);

            Assert.AreEqual(1, resultado.Datos.Count);
            Assert.AreEqual(1, resultado.Datos[0].Identificador);
        }

        [TestMethod]
        public void BuscarFiltrarTextoVacioAplicaSoloFiltroDeEstado()
        {
            var repositorio = CrearRepositorioParaBusqueda();

            var resultado = CrearControlador(repositorio).Consultar("   ", EstadoIncidencia.EnProceso);

            Assert.AreEqual(1, resultado.Datos.Count);
            Assert.AreEqual(2, resultado.Datos[0].Identificador);
        }

        [TestMethod]
        public void BuscarFiltrarTodosEquivaleAAusenciaDeFiltroDeEstadoYConservaOrden()
        {
            var repositorio = CrearRepositorioParaBusqueda();

            var resultado = CrearControlador(repositorio).Consultar(string.Empty, null);

            Assert.AreEqual(4, resultado.Datos.Count);
            Assert.AreEqual(5, resultado.Datos[0].Identificador);
            Assert.AreEqual(2, resultado.Datos[1].Identificador);
            Assert.AreEqual(1, resultado.Datos[2].Identificador);
            Assert.AreEqual(3, resultado.Datos[3].Identificador);
        }

        [TestMethod]
        public void BuscarFiltrarSinCoincidenciasDevuelveColeccionVacia()
        {
            var repositorio = CrearRepositorioParaBusqueda();

            var resultado = CrearControlador(repositorio).Consultar("zzz", null);

            Assert.AreEqual(0, resultado.Datos.Count);
        }

        [TestMethod]
        public void ConsultarSinRegistrosDevuelveMensajeClaroDeListaVacia()
        {
            var repositorio = new IncidenciaRepositoryDoble();

            var resultado = CrearControlador(repositorio).Consultar();

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual("No hay incidencias registradas.", resultado.Mensaje);
            Assert.IsNotNull(resultado.Datos);
            Assert.AreEqual(0, resultado.Datos.Count);
        }

        [TestMethod]
        public void BuscarFiltrarSinCoincidenciasDevuelveMensajeClaro()
        {
            var repositorio = CrearRepositorioParaBusqueda();

            var resultado = CrearControlador(repositorio).Consultar("zzz", EstadoIncidencia.Resuelta);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual("No se encontraron incidencias para los criterios indicados.", resultado.Mensaje);
            Assert.IsNotNull(resultado.Datos);
            Assert.AreEqual(0, resultado.Datos.Count);
        }

        [TestMethod]
        public void ConsultarConFalloDeCargaDevuelveMensajeDeErrorDelRepositorio()
        {
            var repositorio = new IncidenciaRepositoryDoble
            {
                CargarDebeFallar = true,
                CargarMensajeError = "No se pudieron cargar las incidencias desde el archivo XML."
            };

            var resultado = CrearControlador(repositorio).Consultar();

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("No se pudieron cargar las incidencias desde el archivo XML.", resultado.Mensaje);
            Assert.IsNotNull(resultado.Datos);
            Assert.AreEqual(0, resultado.Datos.Count);
        }

        [TestMethod]
        public void RegistrarConFalloDeCargaDebeConservarMensajeDeErrorReal()
        {
            var repositorio = new IncidenciaRepositoryDoble
            {
                CargarDebeFallar = true,
                CargarMensajeError = "No se pudieron cargar las incidencias desde el archivo XML."
            };

            var resultado = CrearControlador(repositorio).Registrar(
                "Incidencia nueva",
                "Descripción nueva válida.",
                "Ana",
                Prioridad.Alta);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("No se pudieron cargar las incidencias desde el archivo XML.", resultado.Mensaje);
            Assert.AreEqual(0, repositorio.IntentosGuardar);
            Assert.AreEqual(0, repositorio.Registros.Count);
        }

        [TestMethod]
        public void EditarConFalloDeCargaDebeConservarMensajeDeErrorReal()
        {
            var repositorio = new IncidenciaRepositoryDoble
            {
                CargarDebeFallar = true,
                CargarMensajeError = "No se pudieron cargar las incidencias desde el archivo XML."
            };

            var resultado = CrearControlador(repositorio).Editar(
                10,
                "Título actualizado",
                "Descripción actualizada válida.",
                "Luis",
                Prioridad.Media);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("No se pudieron cargar las incidencias desde el archivo XML.", resultado.Mensaje);
            Assert.AreEqual(0, repositorio.IntentosGuardar);
        }

        [TestMethod]
        public void CambiarEstadoConFalloDeCargaDebeConservarMensajeDeErrorReal()
        {
            var repositorio = new IncidenciaRepositoryDoble
            {
                CargarDebeFallar = true,
                CargarMensajeError = "No se pudieron cargar las incidencias desde el archivo XML."
            };

            var resultado = CrearControlador(repositorio).CambiarEstado(10, EstadoIncidencia.EnProceso);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("No se pudieron cargar las incidencias desde el archivo XML.", resultado.Mensaje);
            Assert.AreEqual(0, repositorio.IntentosGuardar);
        }

        [TestMethod]
        public void ContratoRepositorioRegistrarVariasIncidenciasDebeConservarAnterioresSinDuplicar()
        {
            var repositorio = new IncidenciaRepositoryDoble();
            var controlador = CrearControlador(repositorio);

            var primero = controlador.Registrar("Error uno", "Descripción válida uno.", "Ana", Prioridad.Media);
            var segundo = controlador.Registrar("Error dos", "Descripción válida dos.", "Luis", Prioridad.Alta);
            var consulta = controlador.Consultar();

            Assert.IsTrue(primero.Exito);
            Assert.IsTrue(segundo.Exito);
            Assert.AreEqual(2, consulta.Datos.Count);
            Assert.IsTrue(consulta.Datos.Any(i => i.Identificador == 1));
            Assert.IsTrue(consulta.Datos.Any(i => i.Identificador == 2));
        }

        [TestMethod]
        public void ContratoRepositorioEditarDebeInvocarGuardarYPersistirCambiosAlVolverACargar()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 21,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                Responsable = "Ana",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = fechaOriginal
            });

            var resultado = CrearControlador(repositorio).Editar(
                21,
                "Titulo actualizado",
                "Descripción actualizada válida.",
                "Luis",
                Prioridad.Alta);

            var recarga = CrearControlador(repositorio).Consultar();

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(1, repositorio.IntentosGuardar);
            Assert.AreEqual("Titulo actualizado", recarga.Datos[0].Titulo);
            Assert.AreEqual("Descripción actualizada válida.", recarga.Datos[0].Descripcion);
            Assert.AreEqual("Luis", recarga.Datos[0].Responsable);
            Assert.AreEqual(Prioridad.Alta, recarga.Datos[0].Prioridad);
        }

        [TestMethod]
        public void ContratoRepositorioCambiarEstadoPermitidoDebeInvocarGuardarYPersistirCambio()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 22,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                Responsable = "Ana",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = fechaOriginal
            });

            var resultado = CrearControlador(repositorio).CambiarEstado(22, EstadoIncidencia.EnProceso);
            var recarga = CrearControlador(repositorio).Consultar();

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(1, repositorio.IntentosGuardar);
            Assert.AreEqual(EstadoIncidencia.EnProceso, recarga.Datos[0].Estado);
            Assert.AreEqual("Titulo inicial", recarga.Datos[0].Titulo);
            Assert.AreEqual(fechaOriginal, recarga.Datos[0].FechaCreacion);
        }

        [TestMethod]
        public void ContratoRepositorioFalloGuardarEnRegistrarDevuelveErrorYConservaDatos()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble { GuardarDebeFallar = true };
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 30,
                Titulo = "Persistida",
                Descripcion = "Descripción persistida válida.",
                Responsable = "Ana",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = fechaOriginal
            });

            var resultado = CrearControlador(repositorio).Registrar(
                "Nuevo error",
                "Descripción válida de alta.",
                "Luis",
                Prioridad.Alta);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual(1, repositorio.IntentosGuardar);
            Assert.AreEqual(1, repositorio.Registros.Count);
            Assert.AreEqual(30, repositorio.Registros[0].Identificador);
            Assert.AreEqual("Persistida", repositorio.Registros[0].Titulo);
            Assert.AreEqual(fechaOriginal, repositorio.Registros[0].FechaCreacion);
        }

        [TestMethod]
        public void ContratoRepositorioFalloGuardarEnEditarDevuelveErrorYConservaDatos()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble { GuardarDebeFallar = true };
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 31,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                Responsable = "Ana",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.EnProceso,
                FechaCreacion = fechaOriginal
            });

            var resultado = CrearControlador(repositorio).Editar(
                31,
                "Titulo actualizado",
                "Descripción actualizada válida.",
                "Luis",
                Prioridad.Alta);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual(1, repositorio.IntentosGuardar);
            Assert.AreEqual("Titulo inicial", repositorio.Registros[0].Titulo);
            Assert.AreEqual(Prioridad.Media, repositorio.Registros[0].Prioridad);
            Assert.AreEqual(EstadoIncidencia.EnProceso, repositorio.Registros[0].Estado);
        }

        [TestMethod]
        public void ContratoRepositorioFalloGuardarEnCambiarEstadoDevuelveErrorYConservaDatos()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble { GuardarDebeFallar = true };
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 32,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                Responsable = "Ana",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = fechaOriginal
            });

            var resultado = CrearControlador(repositorio).CambiarEstado(32, EstadoIncidencia.EnProceso);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual(1, repositorio.IntentosGuardar);
            Assert.AreEqual(EstadoIncidencia.Pendiente, repositorio.Registros[0].Estado);
            Assert.AreEqual("Titulo inicial", repositorio.Registros[0].Titulo);
            Assert.AreEqual(fechaOriginal, repositorio.Registros[0].FechaCreacion);
        }

        [TestMethod]
        public void ContratoRepositorioSolicitarMismoEstadoNoInvocaGuardar()
        {
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 33,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                Responsable = "Ana",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.EnProceso,
                FechaCreacion = new DateTime(2026, 10, 20, 9, 0, 0)
            });

            var resultado = CrearControlador(repositorio).CambiarEstado(33, EstadoIncidencia.EnProceso);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(0, repositorio.IntentosGuardar);
        }

        [TestMethod]
        public void ContratoRepositorioPuedeGuardarFalseImpideOperacionesSinIntentarGuardar()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble { PuedeGuardarConfigurado = false };
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 34,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                Responsable = "Ana",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = fechaOriginal
            });

            var controlador = CrearControlador(repositorio);
            var registro = controlador.Registrar("Nuevo", "Descripción válida de alta.", "Luis", Prioridad.Alta);
            var edicion = controlador.Editar(34, "Titulo editado", "Descripción editada válida.", "Luis", Prioridad.Baja);
            var cambioEstado = controlador.CambiarEstado(34, EstadoIncidencia.EnProceso);

            Assert.IsFalse(registro.Exito);
            Assert.IsFalse(edicion.Exito);
            Assert.IsFalse(cambioEstado.Exito);
            Assert.AreEqual(0, repositorio.IntentosGuardar);
            Assert.AreEqual("Titulo inicial", repositorio.Registros[0].Titulo);
            Assert.AreEqual(EstadoIncidencia.Pendiente, repositorio.Registros[0].Estado);
        }

        [TestMethod]
        public void FiltroActivasIncluyePendienteYEnProcesoYExcluyeResuelta()
        {
            var pendiente = CrearIncidenciaParaFiltro(1, "Pendiente", "Ana", EstadoIncidencia.Pendiente);
            var enProceso = CrearIncidenciaParaFiltro(2, "En proceso", "Luis", EstadoIncidencia.EnProceso);
            var resuelta = CrearIncidenciaParaFiltro(3, "Resuelta", "Marta", EstadoIncidencia.Resuelta);

            Assert.IsTrue(CumpleFiltroEstadoEnFormulario(pendiente, null, true));
            Assert.IsTrue(CumpleFiltroEstadoEnFormulario(enProceso, null, true));
            Assert.IsFalse(CumpleFiltroEstadoEnFormulario(resuelta, null, true));
        }

        [TestMethod]
        public void FiltroTodosIncluyeLosTresEstados()
        {
            var incidencias = new[]
            {
                CrearIncidenciaParaFiltro(1, "Pendiente", "Ana", EstadoIncidencia.Pendiente),
                CrearIncidenciaParaFiltro(2, "En proceso", "Luis", EstadoIncidencia.EnProceso),
                CrearIncidenciaParaFiltro(3, "Resuelta", "Marta", EstadoIncidencia.Resuelta)
            };

            var visibles = incidencias
                .Where(i => CumpleFiltroEstadoEnFormulario(i, null, false))
                .ToList();

            Assert.AreEqual(3, visibles.Count);
            Assert.IsTrue(visibles.Any(i => i.Estado == EstadoIncidencia.Pendiente));
            Assert.IsTrue(visibles.Any(i => i.Estado == EstadoIncidencia.EnProceso));
            Assert.IsTrue(visibles.Any(i => i.Estado == EstadoIncidencia.Resuelta));
        }

        [TestMethod]
        public void FiltroResueltaPermiteConsultarHistorial()
        {
            var incidencias = new[]
            {
                CrearIncidenciaParaFiltro(1, "Pendiente", "Ana", EstadoIncidencia.Pendiente),
                CrearIncidenciaParaFiltro(2, "Resuelta uno", "Luis", EstadoIncidencia.Resuelta),
                CrearIncidenciaParaFiltro(3, "Resuelta dos", "Marta", EstadoIncidencia.Resuelta)
            };

            var visibles = incidencias
                .Where(i => CumpleFiltroEstadoEnFormulario(i, EstadoIncidencia.Resuelta, false))
                .ToList();

            Assert.AreEqual(2, visibles.Count);
            Assert.IsTrue(visibles.All(i => i.Estado == EstadoIncidencia.Resuelta));
        }

        [TestMethod]
        public void BusquedaYFiltroActivasSeCombinanConAnd()
        {
            var incidencias = new[]
            {
                CrearIncidenciaParaFiltro(1, "Error login", "Ana", EstadoIncidencia.Pendiente),
                CrearIncidenciaParaFiltro(2, "Error timeout", "Luis", EstadoIncidencia.Resuelta),
                CrearIncidenciaParaFiltro(3, "Mejora visual", "Error QA", EstadoIncidencia.EnProceso)
            };

            var visibles = incidencias
                .Where(i =>
                    CumpleFiltroEstadoEnFormulario(i, null, true) &&
                    (ContieneTextoEnFormulario(i.Titulo, "error") ||
                     ContieneTextoEnFormulario(i.ReportadoPor, "error") ||
                     ContieneTextoEnFormulario(i.Responsable, "error")))
                .ToList();

            Assert.AreEqual(2, visibles.Count);
            Assert.IsTrue(visibles.Any(i => i.Identificador == 1));
            Assert.IsTrue(visibles.Any(i => i.Identificador == 3));
            Assert.IsFalse(visibles.Any(i => i.Estado == EstadoIncidencia.Resuelta));
        }

        [TestMethod]
        public void FiltrarNoModificaNiEliminaIncidenciasOriginales()
        {
            var original = new[]
            {
                CrearIncidenciaParaFiltro(1, "Pendiente", "Ana", EstadoIncidencia.Pendiente),
                CrearIncidenciaParaFiltro(2, "Resuelta", "Luis", EstadoIncidencia.Resuelta)
            };

            var copiaEstados = original.Select(i => i.Estado).ToArray();
            var copiaIds = original.Select(i => i.Identificador).ToArray();
            var copiaTitulos = original.Select(i => i.Titulo).ToArray();

            var visibles = original
                .Where(i => CumpleFiltroEstadoEnFormulario(i, null, true))
                .ToList();

            Assert.AreEqual(1, visibles.Count);
            CollectionAssert.AreEqual(copiaEstados, original.Select(i => i.Estado).ToArray());
            CollectionAssert.AreEqual(copiaIds, original.Select(i => i.Identificador).ToArray());
            CollectionAssert.AreEqual(copiaTitulos, original.Select(i => i.Titulo).ToArray());
            Assert.AreEqual(2, original.Length);
        }

        private static IncidenciaRepositoryDoble CrearRepositorioParaBusqueda()
        {
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 1,
                Titulo = "Error de login",
                Descripcion = "Falla en autenticación interna.",
                Responsable = "Ana Pérez",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = new DateTime(2026, 10, 24, 10, 0, 0)
            });
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 2,
                Titulo = "Timeout API",
                Descripcion = "Error de login en descripción solamente.",
                Responsable = "Luis Gómez",
                Prioridad = Prioridad.Alta,
                Estado = EstadoIncidencia.EnProceso,
                FechaCreacion = new DateTime(2026, 10, 25, 11, 0, 0)
            });
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 5,
                Titulo = "Error en reporte",
                Descripcion = "Fallo al exportar reporte.",
                Responsable = "María López",
                Prioridad = Prioridad.Baja,
                Estado = EstadoIncidencia.Resuelta,
                FechaCreacion = new DateTime(2026, 10, 25, 11, 0, 0)
            });
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 3,
                Titulo = "Mejora visual",
                Descripcion = "Ajustes de interfaz.",
                Responsable = "Carlos Ruiz",
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = new DateTime(2026, 10, 23, 9, 0, 0)
            });

            return repositorio;
        }

        [TestMethod]
        public void RegistrarSinTecnicoDebePermitirPendienteConResponsableEmpleadoIdNuloYReportadoPorConservado()
        {
            var repositorio = new IncidenciaRepositoryDoble();

            var resultado = RegistrarConReportadoPorYResponsableEmpleadoId(
                CrearControlador(repositorio, new EmpleadoRepositoryDoble()),
                "Error de conexión",
                "La conexión falla al iniciar.",
                "María",
                "Sin asignar",
                Prioridad.Alta,
                null);

            Assert.IsTrue(resultado.Exito);
            Assert.IsNotNull(resultado.Datos);
            Assert.AreEqual(EstadoIncidencia.Pendiente, resultado.Datos.Estado);
            Assert.IsNull(resultado.Datos.ResponsableEmpleadoId);
            Assert.AreEqual("Sin asignar", resultado.Datos.Responsable);
            Assert.AreEqual("María", resultado.Datos.ReportadoPor);
            Assert.AreEqual(1, repositorio.Registros.Count);
        }

        [TestMethod]
        public void RegistrarSinTecnicoConTodosOcupadosDebePermitirAlta()
        {
            var fecha = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 10,
                Titulo = "Abierta",
                Descripcion = "Descripción válida abierta.",
                ReportadoPor = "Marta",
                Responsable = "Michael",
                ResponsableEmpleadoId = 7,
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.EnProceso,
                FechaCreacion = fecha
            });

            var empleados = new EmpleadoRepositoryDoble();
            empleados.Registros.Add(CrearEmpleado(7, "Michael", true, true));

            var resultado = RegistrarConReportadoPorYResponsableEmpleadoId(
                CrearControlador(repositorio, empleados),
                "Nueva incidencia",
                "Descripción válida para alta.",
                "María",
                "Sin asignar",
                Prioridad.Alta,
                null);

            Assert.IsTrue(resultado.Exito);
            Assert.IsNull(resultado.Datos.ResponsableEmpleadoId);
            Assert.AreEqual(2, repositorio.Registros.Count);
        }

        [TestMethod]
        public void RegistrarSinTecnicoSinEmpleadosDebePermitirAlta()
        {
            var repositorio = new IncidenciaRepositoryDoble();
            var empleados = new EmpleadoRepositoryDoble();

            var resultado = RegistrarConReportadoPorYResponsableEmpleadoId(
                CrearControlador(repositorio, empleados),
                "Nueva incidencia",
                "Descripción válida para alta.",
                "María",
                "Sin asignar",
                Prioridad.Media,
                null);

            Assert.IsTrue(resultado.Exito);
            Assert.IsNull(resultado.Datos.ResponsableEmpleadoId);
            Assert.AreEqual("Sin asignar", repositorio.Registros[0].Responsable);
        }

        [TestMethod]
        public void RegistrarSinTecnicoConTodosInactivosDebePermitirAlta()
        {
            var repositorio = new IncidenciaRepositoryDoble();
            var empleados = new EmpleadoRepositoryDoble();
            empleados.Registros.Add(CrearEmpleado(7, "Michael", true, false));
            empleados.Registros.Add(CrearEmpleado(8, "Laura", true, false));

            var resultado = RegistrarConReportadoPorYResponsableEmpleadoId(
                CrearControlador(repositorio, empleados),
                "Nueva incidencia",
                "Descripción válida para alta.",
                "María",
                "Sin asignar",
                Prioridad.Baja,
                null);

            Assert.IsTrue(resultado.Exito);
            Assert.IsNull(resultado.Datos.ResponsableEmpleadoId);
        }

        [TestMethod]
        public void EditarIncidenciaSinAsignarSinReasignarDebePermitirYConservarNulo()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 40,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                ReportadoPor = "Carlos",
                Responsable = "Sin asignar",
                ResponsableEmpleadoId = null,
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = fechaOriginal
            });

            var resultado = EditarConReportadoPorYResponsableEmpleadoId(
                CrearControlador(repositorio, new EmpleadoRepositoryDoble()),
                40,
                "Titulo actualizado",
                "Descripción actualizada válida.",
                "María",
                "Sin asignar",
                Prioridad.Alta,
                null);

            Assert.IsTrue(resultado.Exito);
            Assert.IsNull(repositorio.Registros[0].ResponsableEmpleadoId);
            Assert.AreEqual("Sin asignar", repositorio.Registros[0].Responsable);
        }

        [TestMethod]
        public void EditarIncidenciaSinAsignarYAsignarATecnicoLibreDebePermitir()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 41,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                ReportadoPor = "Carlos",
                Responsable = "Sin asignar",
                ResponsableEmpleadoId = null,
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = fechaOriginal
            });

            var empleados = new EmpleadoRepositoryDoble();
            empleados.Registros.Add(CrearEmpleado(7, "Michael", true, true));

            var resultado = EditarConReportadoPorYResponsableEmpleadoId(
                CrearControlador(repositorio, empleados),
                41,
                "Titulo actualizado",
                "Descripción actualizada válida.",
                "María",
                "Michael",
                Prioridad.Alta,
                7);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(7, repositorio.Registros[0].ResponsableEmpleadoId);
        }

        [TestMethod]
        public void EditarIncidenciaSinAsignarYAsignarATecnicoOcupadoDebeRechazar()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 41,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                ReportadoPor = "Carlos",
                Responsable = "Sin asignar",
                ResponsableEmpleadoId = null,
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = fechaOriginal
            });
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 42,
                Titulo = "Abierta",
                Descripcion = "Descripción válida abierta.",
                ReportadoPor = "Marta",
                Responsable = "Michael",
                ResponsableEmpleadoId = 7,
                Prioridad = Prioridad.Alta,
                Estado = EstadoIncidencia.EnProceso,
                FechaCreacion = fechaOriginal.AddMinutes(1)
            });

            var empleados = new EmpleadoRepositoryDoble();
            empleados.Registros.Add(CrearEmpleado(7, "Michael", true, true));

            var resultado = EditarConReportadoPorYResponsableEmpleadoId(
                CrearControlador(repositorio, empleados),
                41,
                "Titulo actualizado",
                "Descripción actualizada válida.",
                "María",
                "Michael",
                Prioridad.Alta,
                7);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("No se puede reasignar a un técnico ocupado. Seleccione un técnico activo y libre.", resultado.Mensaje);
            Assert.IsNull(repositorio.Registros[0].ResponsableEmpleadoId);
        }

        [TestMethod]
        public void EditarConResponsableEmpleadoIdActualInvalidoYSinAsignarDebeRechazarParaEvitarBypass()
        {
            var fechaOriginal = new DateTime(2026, 10, 20, 9, 0, 0);
            var repositorio = new IncidenciaRepositoryDoble();
            repositorio.Registros.Add(new Incidencia
            {
                Identificador = 55,
                Titulo = "Titulo inicial",
                Descripcion = "Descripción inicial válida.",
                ReportadoPor = "Carlos",
                Responsable = "Responsable inválido",
                ResponsableEmpleadoId = 999,
                Prioridad = Prioridad.Media,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = fechaOriginal
            });

            var empleados = new EmpleadoRepositoryDoble();
            empleados.Registros.Add(CrearEmpleado(7, "Michael", true, true));

            var resultado = EditarConReportadoPorYResponsableEmpleadoId(
                CrearControlador(repositorio, empleados),
                55,
                "Titulo actualizado",
                "Descripción actualizada válida.",
                "María",
                "Sin asignar",
                Prioridad.Alta,
                null);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("No se pudo validar el técnico responsable para la asignación indicada.", resultado.Mensaje);
            Assert.AreEqual(999, repositorio.Registros[0].ResponsableEmpleadoId);
        }

        private static IncidenciaController CrearControlador(IncidenciaRepositoryDoble repositorio)
        {
            return new IncidenciaController(repositorio);
        }

        private static IncidenciaController CrearControlador(
            IncidenciaRepositoryDoble repositorio,
            EmpleadoRepositoryDoble repositorioEmpleados)
        {
            return new IncidenciaController(repositorio, repositorioEmpleados);
        }

        private static Empleado CrearEmpleado(int id, string nombre, bool esTecnico, bool activo)
        {
            return new Empleado(id, nombre, esTecnico, activo, new DateTime(2026, 10, 20, 8, 0, 0));
        }

        private static ResultadoOperacion<Incidencia> RegistrarConReportadoPor(
            IncidenciaController controlador,
            string titulo,
            string descripcion,
            string reportadoPor,
            string responsable,
            Prioridad prioridad)
        {
            var metodo = typeof(IncidenciaController).GetMethod(
                "Registrar",
                new[]
                {
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(Prioridad)
                });

            Assert.IsNotNull(metodo, "Falta sobrecarga Registrar con ReportadoPor para T063.");

            return (ResultadoOperacion<Incidencia>)metodo.Invoke(
                controlador,
                new object[] { titulo, descripcion, reportadoPor, responsable, prioridad });
        }

        private static ResultadoOperacion<Incidencia> EditarConReportadoPor(
            IncidenciaController controlador,
            int identificador,
            string titulo,
            string descripcion,
            string reportadoPor,
            string responsable,
            Prioridad prioridad)
        {
            var metodo = typeof(IncidenciaController).GetMethod(
                "Editar",
                new[]
                {
                    typeof(int),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(Prioridad)
                });

            Assert.IsNotNull(metodo, "Falta sobrecarga Editar con ReportadoPor para T063.");

            return (ResultadoOperacion<Incidencia>)metodo.Invoke(
                controlador,
                new object[] { identificador, titulo, descripcion, reportadoPor, responsable, prioridad });
        }

        private static ResultadoOperacion<Incidencia> RegistrarConReportadoPorYResponsableEmpleadoId(
            IncidenciaController controlador,
            string titulo,
            string descripcion,
            string reportadoPor,
            string responsable,
            Prioridad prioridad,
            int? responsableEmpleadoId)
        {
            var metodo = typeof(IncidenciaController).GetMethod(
                "Registrar",
                new[]
                {
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(Prioridad),
                    typeof(int?)
                });

            Assert.IsNotNull(metodo, "Falta sobrecarga Registrar con ResponsableEmpleadoId para T098-B1.");

            return (ResultadoOperacion<Incidencia>)metodo.Invoke(
                controlador,
                new object[] { titulo, descripcion, reportadoPor, responsable, prioridad, responsableEmpleadoId });
        }

        private static ResultadoOperacion<Incidencia> EditarConReportadoPorYResponsableEmpleadoId(
            IncidenciaController controlador,
            int identificador,
            string titulo,
            string descripcion,
            string reportadoPor,
            string responsable,
            Prioridad prioridad,
            int? responsableEmpleadoId)
        {
            var metodo = typeof(IncidenciaController).GetMethod(
                "Editar",
                new[]
                {
                    typeof(int),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(Prioridad),
                    typeof(int?)
                });

            Assert.IsNotNull(metodo, "Falta sobrecarga Editar con ResponsableEmpleadoId para T098-B1.");

            return (ResultadoOperacion<Incidencia>)metodo.Invoke(
                controlador,
                new object[] { identificador, titulo, descripcion, reportadoPor, responsable, prioridad, responsableEmpleadoId });
        }

        private static Incidencia CrearIncidenciaParaFiltro(
            int identificador,
            string titulo,
            string responsable,
            EstadoIncidencia estado)
        {
            return new Incidencia
            {
                Identificador = identificador,
                Titulo = titulo,
                Descripcion = "Descripción válida para filtro.",
                ReportadoPor = "Reporta " + responsable,
                Responsable = responsable,
                Prioridad = Prioridad.Media,
                Estado = estado,
                FechaCreacion = new DateTime(2026, 11, 5, 9, 0, 0).AddMinutes(identificador)
            };
        }

        private static bool CumpleFiltroEstadoEnFormulario(
            Incidencia incidencia,
            EstadoIncidencia? filtroEstado,
            bool filtroActivas)
        {
            var metodo = typeof(frmIncidencias).GetMethod(
                "CumpleFiltroEstado",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            Assert.IsNotNull(metodo, "Se esperaba método interno CumpleFiltroEstado para T105.");

            return (bool)metodo.Invoke(null, new object[] { incidencia, filtroEstado, filtroActivas });
        }

        private static bool ContieneTextoEnFormulario(string valor, string texto)
        {
            var metodo = typeof(frmIncidencias).GetMethod(
                "ContieneTexto",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            Assert.IsNotNull(metodo, "Se esperaba método interno ContieneTexto para T105.");

            return (bool)metodo.Invoke(null, new object[] { valor, texto });
        }
    }
}
