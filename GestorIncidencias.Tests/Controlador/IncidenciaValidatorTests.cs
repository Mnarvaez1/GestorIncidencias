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

        [TestMethod]
        public void TituloConCienCaracteresDebeAceptarse()
        {
            var resultado = CrearValidador().Validar(new string('a', 100), "Descripción válida", "Ana", Prioridad.Media);

            Assert.IsTrue(resultado.Exito);
        }

        [TestMethod]
        public void TituloConCientoUnCaracteresDebeRechazarse()
        {
            var resultado = CrearValidador().Validar(new string('a', 101), "Descripción válida", "Ana", Prioridad.Media);

            Assert.IsFalse(resultado.Exito);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void TituloNuloVacioOSoloEspaciosDebeRechazarse(string titulo)
        {
            var resultado = CrearValidador().Validar(titulo, "Descripción válida", "Ana", Prioridad.Media);

            Assert.IsFalse(resultado.Exito);
        }

        [TestMethod]
        public void TextosConEspaciosExterioresSeValidanDespuesDeTrim()
        {
            var resultado = CrearValidador().Validar(" abcde ", " Descripcion10 ", " Ana ", Prioridad.Media);

            Assert.IsTrue(resultado.Exito);
        }

        [TestMethod]
        public void DescripcionConDiezCaracteresDebeAceptarse()
        {
            var resultado = CrearValidador().Validar("abcde", new string('a', 10), "Ana", Prioridad.Media);

            Assert.IsTrue(resultado.Exito);
        }

        [TestMethod]
        public void DescripcionConMilCaracteresDebeAceptarse()
        {
            var resultado = CrearValidador().Validar("abcde", new string('a', 1000), "Ana", Prioridad.Media);

            Assert.IsTrue(resultado.Exito);
        }

        [TestMethod]
        public void DescripcionFueraDeLimitesDebeRechazarse()
        {
            var descripcionCorta = CrearValidador().Validar("abcde", new string('a', 9), "Ana", Prioridad.Media);
            var descripcionLarga = CrearValidador().Validar("abcde", new string('a', 1001), "Ana", Prioridad.Media);

            Assert.IsFalse(descripcionCorta.Exito);
            Assert.IsFalse(descripcionLarga.Exito);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void DescripcionNulaVaciaOSoloEspaciosDebeRechazarse(string descripcion)
        {
            var resultado = CrearValidador().Validar("abcde", descripcion, "Ana", Prioridad.Media);

            Assert.IsFalse(resultado.Exito);
        }

        [TestMethod]
        public void ResponsableConDosCaracteresDebeAceptarse()
        {
            var resultado = CrearValidador().Validar("abcde", "Descripción válida", "AB", Prioridad.Media);

            Assert.IsTrue(resultado.Exito);
        }

        [TestMethod]
        public void ResponsableConOchentaCaracteresDebeAceptarse()
        {
            var resultado = CrearValidador().Validar("abcde", "Descripción válida", new string('a', 80), Prioridad.Media);

            Assert.IsTrue(resultado.Exito);
        }

        [TestMethod]
        public void ResponsableFueraDeLimitesDebeRechazarse()
        {
            var responsableCorto = CrearValidador().Validar("abcde", "Descripción válida", "A", Prioridad.Media);
            var responsableLargo = CrearValidador().Validar("abcde", "Descripción válida", new string('a', 81), Prioridad.Media);

            Assert.IsFalse(responsableCorto.Exito);
            Assert.IsFalse(responsableLargo.Exito);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void ResponsableNuloVacioOSoloEspaciosDebeRechazarse(string responsable)
        {
            var resultado = CrearValidador().Validar("abcde", "Descripción válida", responsable, Prioridad.Media);

            Assert.IsFalse(resultado.Exito);
        }

        [DataTestMethod]
        [DataRow(Prioridad.Baja)]
        [DataRow(Prioridad.Media)]
        [DataRow(Prioridad.Alta)]
        public void PrioridadesPermitidasDebenAceptarse(Prioridad prioridad)
        {
            var resultado = CrearValidador().Validar("abcde", "Descripción válida", "Ana", prioridad);

            Assert.IsTrue(resultado.Exito);
        }

        [TestMethod]
        public void PrioridadNoPermitidaDebeRechazarse()
        {
            var resultado = CrearValidador().Validar("abcde", "Descripción válida", "Ana", (Prioridad)99);

            Assert.IsFalse(resultado.Exito);
        }

        private static IncidenciaValidator CrearValidador()
        {
            return new IncidenciaValidator();
        }
    }
}
