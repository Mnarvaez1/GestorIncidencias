using System;
using GestorIncidencias.Controlador;
using GestorIncidencias.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestorIncidencias.Tests.Controlador
{
    [TestClass]
    public class EmpleadoValidatorTests
    {
        [TestMethod]
        public void EmpleadoNuloDebeRechazarse()
        {
            var resultado = CrearValidador().Validar(null);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("El empleado es obligatorio.", resultado.Mensaje);
        }

        [TestMethod]
        public void EmpleadoValidoDebeAceptar()
        {
            var resultado = CrearValidador().Validar(CrearEmpleado(1, "Ana Torres"));

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual("Validación correcta.", resultado.Mensaje);
        }

        [TestMethod]
        public void IdentificadorCeroDebeRechazarse()
        {
            var resultado = CrearValidador().Validar(CrearEmpleado(0, "Ana Torres"));

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("El identificador del empleado debe ser un número positivo.", resultado.Mensaje);
        }

        [TestMethod]
        public void IdentificadorNegativoDebeRechazarse()
        {
            var resultado = CrearValidador().Validar(CrearEmpleado(-5, "Ana Torres"));

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("El identificador del empleado debe ser un número positivo.", resultado.Mensaje);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void NombreCompletoNuloVacioOSoloEspaciosDebeRechazarse(string nombre)
        {
            var resultado = CrearValidador().Validar(CrearEmpleado(1, nombre));

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("El nombre completo del empleado debe tener entre 2 y 120 caracteres.", resultado.Mensaje);
        }

        [TestMethod]
        public void NombreCompletoConUnCaracterDebeRechazarse()
        {
            var resultado = CrearValidador().Validar(CrearEmpleado(1, "A"));

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("El nombre completo del empleado debe tener entre 2 y 120 caracteres.", resultado.Mensaje);
        }

        [DataTestMethod]
        [DataRow(2)]
        [DataRow(120)]
        public void NombreCompletoEnLimitesDebeAceptar(int longitud)
        {
            var nombre = new string('A', longitud);
            var resultado = CrearValidador().Validar(CrearEmpleado(1, nombre));

            Assert.IsTrue(resultado.Exito);
        }

        [TestMethod]
        public void NombreCompletoConCientoVeintiunCaracteresDebeRechazarse()
        {
            var resultado = CrearValidador().Validar(CrearEmpleado(1, new string('A', 121)));

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("El nombre completo del empleado debe tener entre 2 y 120 caracteres.", resultado.Mensaje);
        }

        [TestMethod]
        public void NombreCompletoConEspaciosExterioresSeValidaDespuesDeTrim()
        {
            var resultado = CrearValidador().Validar(CrearEmpleado(1, "  Ana Torres  "));

            Assert.IsTrue(resultado.Exito);
        }

        [TestMethod]
        public void ValidacionFallidaNoModificaIdentificadorNiFechaCreacion()
        {
            var fecha = new DateTime(2026, 10, 28, 10, 0, 0);
            var empleado = new Empleado(9, " ", true, true, fecha);

            var resultado = CrearValidador().Validar(empleado);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual(9, empleado.Identificador);
            Assert.AreEqual(fecha, empleado.FechaCreacion);
        }

        private static EmpleadoValidator CrearValidador()
        {
            return new EmpleadoValidator();
        }

        private static Empleado CrearEmpleado(int identificador, string nombreCompleto)
        {
            return new Empleado(identificador, nombreCompleto, true, true, new DateTime(2026, 10, 28, 10, 0, 0));
        }
    }
}
