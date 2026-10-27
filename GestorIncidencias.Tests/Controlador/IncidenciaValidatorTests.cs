using GestorIncidencias.Controlador;
using GestorIncidencias.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

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

        [TestMethod]
        public void ReportadoPorConDosCaracteresDespuesDeRecorteDebeAceptarse()
        {
            var resultado = ValidarConReportadoPor("abcde", "Descripción válida", "Ana", Prioridad.Media, " AB ");

            Assert.IsTrue(resultado.Exito);
        }

        [TestMethod]
        public void ReportadoPorConOchentaCaracteresDebeAceptarse()
        {
            var resultado = ValidarConReportadoPor("abcde", "Descripción válida", "Ana", Prioridad.Media, new string('a', 80));

            Assert.IsTrue(resultado.Exito);
        }

        [TestMethod]
        public void ReportadoPorConUnCaracterDebeRechazarse()
        {
            var resultado = ValidarConReportadoPor("abcde", "Descripción válida", "Ana", Prioridad.Media, "A");

            Assert.IsFalse(resultado.Exito);
        }

        [TestMethod]
        public void ReportadoPorConOchentaYUnCaracteresDebeRechazarse()
        {
            var resultado = ValidarConReportadoPor("abcde", "Descripción válida", "Ana", Prioridad.Media, new string('a', 81));

            Assert.IsFalse(resultado.Exito);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow(null)]
        public void ReportadoPorNuloVacioOSoloEspaciosDebeRechazarseEnRegistroNuevo(string reportadoPor)
        {
            var resultado = ValidarConReportadoPor("abcde", "Descripción válida", "Ana", Prioridad.Media, reportadoPor);

            Assert.IsFalse(resultado.Exito);
        }

        [TestMethod]
        public void ResponsableEmpleadoIdPositivoDebeAceptarse()
        {
            var resultado = ValidarConResponsableEmpleadoId(
                "abcde",
                "Descripción válida",
                "Ana",
                Prioridad.Media,
                3);

            Assert.IsTrue(resultado.Exito);
        }

        [TestMethod]
        public void ResponsableEmpleadoIdNuloDebeAceptarse()
        {
            var resultado = ValidarConResponsableEmpleadoId(
                "abcde",
                "Descripción válida",
                "Ana",
                Prioridad.Media,
                null);

            Assert.IsTrue(resultado.Exito);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void ResponsableEmpleadoIdNoPositivoDebeRechazarse(int responsableEmpleadoId)
        {
            var resultado = ValidarConResponsableEmpleadoId(
                "abcde",
                "Descripción válida",
                "Ana",
                Prioridad.Media,
                responsableEmpleadoId);

            Assert.IsFalse(resultado.Exito);
        }

        private static IncidenciaValidator CrearValidador()
        {
            return new IncidenciaValidator();
        }

        private static ResultadoOperacion<Incidencia> ValidarConReportadoPor(
            string titulo,
            string descripcion,
            string responsable,
            Prioridad prioridad,
            string reportadoPor)
        {
            var validador = CrearValidador();
            var tipoValidador = typeof(IncidenciaValidator);
            var firmaConReportadoPor = tipoValidador.GetMethod(
                "Validar",
                new[]
                {
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(Prioridad),
                    typeof(string)
                });

            Assert.IsNotNull(firmaConReportadoPor,
                "Se esperaba una sobrecarga pública Validar(..., reportadoPor) para validar ReportadoPor en T058.");

            return (ResultadoOperacion<Incidencia>)firmaConReportadoPor.Invoke(
                validador,
                new object[] { titulo, descripcion, responsable, prioridad, reportadoPor });
        }

        private static ResultadoOperacion<Incidencia> ValidarConResponsableEmpleadoId(
            string titulo,
            string descripcion,
            string responsable,
            Prioridad prioridad,
            int? responsableEmpleadoId)
        {
            var validador = CrearValidador();
            var tipoValidador = typeof(IncidenciaValidator);
            var firmaConResponsableEmpleadoId = tipoValidador.GetMethod(
                "Validar",
                new[]
                {
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(Prioridad),
                    typeof(int?)
                });

            Assert.IsNotNull(firmaConResponsableEmpleadoId,
                "Se esperaba una sobrecarga pública Validar(..., responsableEmpleadoId) para T098-B1.");

            return (ResultadoOperacion<Incidencia>)firmaConResponsableEmpleadoId.Invoke(
                validador,
                new object[] { titulo, descripcion, responsable, prioridad, responsableEmpleadoId });
        }
    }
}
