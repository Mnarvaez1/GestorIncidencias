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

        private static IncidenciaController CrearControlador(IncidenciaRepositoryDoble repositorio)
        {
            return new IncidenciaController(repositorio);
        }
    }
}
