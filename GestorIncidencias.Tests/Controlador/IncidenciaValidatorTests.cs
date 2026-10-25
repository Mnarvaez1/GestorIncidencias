using GestorIncidencias.Controlador;
using GestorIncidencias.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestorIncidencias.Tests.Controlador
{
    [TestClass]
    public class IncidenciaValidatorTests
    {
        [TestMethod]
        public void TituloConCuatroCaracteresDebeRechazarse()
        {
            var resultado = CrearValidador().Validar("abcd", "Descripción válida", "Ana", Prioridad.Media);

            Assert.IsFalse(resultado.Exito);
        }

        [TestMethod]
        public void TituloConCincoCaracteresDebeAceptarse()
        {
            var resultado = CrearValidador().Validar("abcde", "Descripción válida", "Ana", Prioridad.Media);

            Assert.IsTrue(resultado.Exito);
        }

        private static IncidenciaValidator CrearValidador()
        {
            return new IncidenciaValidator();
        }
    }
}
