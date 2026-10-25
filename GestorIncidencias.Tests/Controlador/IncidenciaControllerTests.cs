using System;
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
            Assert.AreEqual(1, consulta.Datos[0].Identificador);
            Assert.AreEqual(2, consulta.Datos[1].Identificador);
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

        private static IncidenciaController CrearControlador(IncidenciaRepositoryDoble repositorio)
        {
            return new IncidenciaController(repositorio);
        }
    }
}
