using System;
using GestorIncidencias.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestorIncidencias.Tests.Entidades
{
    [TestClass]
    public class EmpleadoTests
    {
        [TestMethod]
        public void ConstructorAsignaTodosLosCamposDelEmpleado()
        {
            var fecha = new DateTime(2026, 10, 27, 9, 30, 0);
            var empleado = new Empleado(5, "Ana Torres", true, true, fecha);

            Assert.AreEqual(5, empleado.Identificador);
            Assert.AreEqual("Ana Torres", empleado.NombreCompleto);
            Assert.IsTrue(empleado.EsTecnicoSoporte);
            Assert.IsTrue(empleado.Activo);
            Assert.AreEqual(fecha, empleado.FechaCreacion);
        }

        [TestMethod]
        public void IdentificadorYFechaCreacionSonSoloLectura()
        {
            var fecha = new DateTime(2026, 10, 27, 9, 30, 0);
            var empleado = new Empleado(7, "Luis Pérez", false, true, fecha);

            empleado.NombreCompleto = "Luis Alberto Pérez";
            empleado.EsTecnicoSoporte = true;
            empleado.Activo = false;

            Assert.AreEqual(7, empleado.Identificador);
            Assert.AreEqual(fecha, empleado.FechaCreacion);
            Assert.AreEqual("Luis Alberto Pérez", empleado.NombreCompleto);
            Assert.IsTrue(empleado.EsTecnicoSoporte);
            Assert.IsFalse(empleado.Activo);
        }

        [DataTestMethod]
        [DataRow(2)]
        [DataRow(120)]
        public void NombreCompletoAceptaLimitesDefinidosEnEspecificacion(int longitud)
        {
            var nombre = new string('A', longitud);
            var empleado = new Empleado(1, nombre, true, true, new DateTime(2026, 10, 27));

            Assert.AreEqual(nombre, empleado.NombreCompleto);
            Assert.AreEqual(longitud, empleado.NombreCompleto.Length);
        }
    }
}
