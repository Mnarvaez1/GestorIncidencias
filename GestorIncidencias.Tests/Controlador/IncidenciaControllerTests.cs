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
                "corto",
                "Descripción válida.",
                "Ana",
                Prioridad.Media);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual(0, repositorio.Registros.Count);
        }

        private static IncidenciaController CrearControlador(IncidenciaRepositoryDoble repositorio)
        {
            return new IncidenciaController(repositorio);
        }
    }
}
