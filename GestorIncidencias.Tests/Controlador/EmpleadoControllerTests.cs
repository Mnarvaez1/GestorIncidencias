using System;
using System.Collections.Generic;
using GestorIncidencias.Controlador;
using GestorIncidencias.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestorIncidencias.Tests.Controlador
{
    [TestClass]
    public class EmpleadoControllerTests
    {
        [TestMethod]
        public void RegistrarEmpleadoValidoDebeGuardarYDevolverActivo()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            var resultado = CrearControlador(repositorio).Registrar("Ana Torres", true);

            Assert.IsTrue(resultado.Exito);
            Assert.IsNotNull(resultado.Datos);
            Assert.AreEqual(1, resultado.Datos.Identificador);
            Assert.AreEqual("Ana Torres", resultado.Datos.NombreCompleto);
            Assert.IsTrue(resultado.Datos.Activo);
            Assert.IsTrue(resultado.Datos.EsTecnicoSoporte);
            Assert.AreEqual(1, repositorio.Registros.Count);
            Assert.AreEqual(1, repositorio.IntentosGuardar);
        }

        [TestMethod]
        public void RegistrarConNombreInvalidoDebeRechazarSinGuardar()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            var resultado = CrearControlador(repositorio).Registrar(" ", true);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual(0, repositorio.Registros.Count);
            Assert.AreEqual(0, repositorio.IntentosGuardar);
        }

        [TestMethod]
        public void RegistrarSucesivoDebeAsignarIdentificadoresUnicos()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            var controlador = CrearControlador(repositorio);

            var primero = controlador.Registrar("Ana Torres", true);
            var segundo = controlador.Registrar("Luis Pérez", false);

            Assert.IsTrue(primero.Exito);
            Assert.IsTrue(segundo.Exito);
            Assert.AreEqual(1, primero.Datos.Identificador);
            Assert.AreEqual(2, segundo.Datos.Identificador);
        }

        [TestMethod]
        public void RegistrarDespuesDeInactivarNoDebeReutilizarIdentificadores()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            var controlador = CrearControlador(repositorio);

            var primero = controlador.Registrar("Ana Torres", true);
            var segundo = controlador.Registrar("Luis Pérez", false);
            var inactivado = controlador.Inactivar(primero.Datos.Identificador);
            var tercero = controlador.Registrar("Marta Rojas", true);

            Assert.IsTrue(inactivado.Exito);
            Assert.IsTrue(tercero.Exito);
            Assert.AreEqual(3, tercero.Datos.Identificador);
        }

        [TestMethod]
        public void ConsultarSinRegistrosDebeDevolverColeccionVacia()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            var resultado = CrearControlador(repositorio).Consultar();

            Assert.IsTrue(resultado.Exito);
            Assert.IsNotNull(resultado.Datos);
            Assert.AreEqual(0, resultado.Datos.Count);
        }

        [TestMethod]
        public void ConsultarConVariosEmpleadosDebeDevolverDatosOrdenadosPorNombre()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(3, "Luis Pérez", false, true, new DateTime(2026, 11, 1, 9, 2, 0)));
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            repositorio.Registros.Add(CrearEmpleado(2, "Marta Rojas", true, false, new DateTime(2026, 11, 1, 9, 1, 0)));

            var resultado = CrearControlador(repositorio).Consultar();

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(3, resultado.Datos.Count);
            Assert.AreEqual("Ana Torres", resultado.Datos[0].NombreCompleto);
            Assert.AreEqual("Luis Pérez", resultado.Datos[1].NombreCompleto);
            Assert.AreEqual("Marta Rojas", resultado.Datos[2].NombreCompleto);
        }

        [TestMethod]
        public void BuscarPorNombreDebeCoincidirParcialSinDistinguirMayusculas()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            repositorio.Registros.Add(CrearEmpleado(2, "Luis Pérez", false, true, new DateTime(2026, 11, 1, 9, 1, 0)));

            var resultado = CrearControlador(repositorio).Buscar("ANa");

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(1, resultado.Datos.Count);
            Assert.AreEqual("Ana Torres", resultado.Datos[0].NombreCompleto);
        }

        [TestMethod]
        public void BuscarSinCoincidenciasDebeDevolverColeccionVacia()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));

            var resultado = CrearControlador(repositorio).Buscar("ZZZ");

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(0, resultado.Datos.Count);
        }

        [TestMethod]
        public void EditarEmpleadoExistenteDebeActualizarCamposPermitidos()
        {
            var fecha = new DateTime(2026, 11, 1, 9, 0, 0);
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, fecha));

            var resultado = CrearControlador(repositorio).Editar(1, "Ana María Torres", false, false);

            Assert.IsTrue(resultado.Exito);
            Assert.IsNotNull(resultado.Datos);
            Assert.AreEqual("Ana María Torres", resultado.Datos.NombreCompleto);
            Assert.IsFalse(resultado.Datos.EsTecnicoSoporte);
            Assert.IsFalse(resultado.Datos.Activo);
            Assert.AreEqual(1, repositorio.Registros.Count);
        }

        [TestMethod]
        public void EditarEmpleadoInexistenteDebeRechazar()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));

            var resultado = CrearControlador(repositorio).Editar(99, "Nombre", true, true);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("No existe un empleado con el identificador indicado.", resultado.Mensaje);
        }

        [TestMethod]
        public void EditarDebeConservarIdentificadorYFechaCreacion()
        {
            var fecha = new DateTime(2026, 11, 1, 9, 0, 0);
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(7, "Ana Torres", true, true, fecha));

            var resultado = CrearControlador(repositorio).Editar(7, "Ana T.", true, false);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(7, resultado.Datos.Identificador);
            Assert.AreEqual(fecha, resultado.Datos.FechaCreacion);
        }

        [TestMethod]
        public void ActivarEmpleadoDebeCambiarEstadoActivo()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, false, new DateTime(2026, 11, 1, 9, 0, 0)));

            var resultado = CrearControlador(repositorio).Activar(1);

            Assert.IsTrue(resultado.Exito);
            Assert.IsTrue(resultado.Datos.Activo);
            Assert.IsTrue(repositorio.Registros[0].Activo);
        }

        [TestMethod]
        public void InactivarEmpleadoDebeCambiarEstadoActivo()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));

            var resultado = CrearControlador(repositorio).Inactivar(1);

            Assert.IsTrue(resultado.Exito);
            Assert.IsFalse(resultado.Datos.Activo);
            Assert.IsFalse(repositorio.Registros[0].Activo);
        }

        [TestMethod]
        public void InactivarDebeConservarEmpleadoEnColeccion()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            repositorio.Registros.Add(CrearEmpleado(2, "Luis Pérez", false, true, new DateTime(2026, 11, 1, 9, 1, 0)));

            var resultado = CrearControlador(repositorio).Inactivar(1);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(2, repositorio.Registros.Count);
            Assert.IsFalse(repositorio.Registros[0].Activo);
        }

        [TestMethod]
        public void ConsultarConFalloDeCargaDebeConservarErrorReal()
        {
            var repositorio = new EmpleadoRepositoryDoble
            {
                CargarDebeFallar = true,
                CargarMensajeError = "Error real de carga de empleados."
            };

            var resultado = CrearControlador(repositorio).Consultar();

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("Error real de carga de empleados.", resultado.Mensaje);
            Assert.AreEqual(0, resultado.Datos.Count);
        }

        [TestMethod]
        public void RegistrarConFalloDeGuardadoDebeRechazarSinExitoFalso()
        {
            var repositorio = new EmpleadoRepositoryDoble
            {
                GuardarDebeFallar = true,
                GuardarMensajeError = "Error real de guardado de empleados."
            };

            var resultado = CrearControlador(repositorio).Registrar("Ana Torres", true);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("Error real de guardado de empleados.", resultado.Mensaje);
        }

        [TestMethod]
        public void RegistrarConPuedeGuardarFalseDebeRechazarSinIntentarGuardar()
        {
            var repositorio = new EmpleadoRepositoryDoble
            {
                PuedeGuardarConfigurado = false
            };

            var resultado = CrearControlador(repositorio).Registrar("Ana Torres", true);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("El repositorio no permite guardar empleados.", resultado.Mensaje);
            Assert.AreEqual(0, repositorio.IntentosGuardar);
        }

        [TestMethod]
        public void EditarConDatosInvalidosNoDebePersistirCambios()
        {
            var fecha = new DateTime(2026, 11, 1, 9, 0, 0);
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, fecha));

            var resultado = CrearControlador(repositorio).Editar(1, " ", false, false);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual("Ana Torres", repositorio.Registros[0].NombreCompleto);
            Assert.IsTrue(repositorio.Registros[0].EsTecnicoSoporte);
            Assert.IsTrue(repositorio.Registros[0].Activo);
            Assert.AreEqual(fecha, repositorio.Registros[0].FechaCreacion);
        }

        [TestMethod]
        public void BuscarConTextoVacioDebeEquivalerAConsultar()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(2, "Luis Pérez", false, true, new DateTime(2026, 11, 1, 9, 1, 0)));
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));

            var consulta = CrearControlador(repositorio).Consultar();
            var busqueda = CrearControlador(repositorio).Buscar("   ");

            Assert.IsTrue(consulta.Exito);
            Assert.IsTrue(busqueda.Exito);
            Assert.AreEqual(consulta.Datos.Count, busqueda.Datos.Count);
            for (var i = 0; i < consulta.Datos.Count; i++)
            {
                Assert.AreEqual(consulta.Datos[i].Identificador, busqueda.Datos[i].Identificador);
            }
        }

        [TestMethod]
        public void ConsultarDevuelveCopiasIndependientesSinMutarRepositorio()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));

            var resultado = CrearControlador(repositorio).Consultar();

            Assert.IsTrue(resultado.Exito);
            resultado.Datos[0].NombreCompleto = "Mutado";

            Assert.AreEqual("Ana Torres", repositorio.Registros[0].NombreCompleto);
        }

        [TestMethod]
        public void ConsultarDisponibilidadTecnicoSinIncidenciasDebeSerLibre()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));

            var resultado = CrearControlador(repositorio).ConsultarDisponibilidad(1, new List<Incidencia>());

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Libre, resultado.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadConPendienteDebeSerOcupado()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));

            var incidencias = new List<Incidencia>
            {
                CrearIncidencia(1, 1, EstadoIncidencia.Pendiente)
            };

            var resultado = CrearControlador(repositorio).ConsultarDisponibilidad(1, incidencias);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Ocupado, resultado.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadConEnProcesoDebeSerOcupado()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));

            var incidencias = new List<Incidencia>
            {
                CrearIncidencia(1, 1, EstadoIncidencia.EnProceso)
            };

            var resultado = CrearControlador(repositorio).ConsultarDisponibilidad(1, incidencias);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Ocupado, resultado.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadConResueltaDebeSerLibre()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));

            var incidencias = new List<Incidencia>
            {
                CrearIncidencia(1, 1, EstadoIncidencia.Resuelta)
            };

            var resultado = CrearControlador(repositorio).ConsultarDisponibilidad(1, incidencias);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Libre, resultado.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadConMultiplesYUnaAbiertaDebeSerOcupado()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));

            var incidencias = new List<Incidencia>
            {
                CrearIncidencia(1, 1, EstadoIncidencia.Resuelta),
                CrearIncidencia(2, 1, EstadoIncidencia.EnProceso)
            };

            var resultado = CrearControlador(repositorio).ConsultarDisponibilidad(1, incidencias);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Ocupado, resultado.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadConTodasResueltasDebeSerLibre()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));

            var incidencias = new List<Incidencia>
            {
                CrearIncidencia(1, 1, EstadoIncidencia.Resuelta),
                CrearIncidencia(2, 1, EstadoIncidencia.Resuelta)
            };

            var resultado = CrearControlador(repositorio).ConsultarDisponibilidad(1, incidencias);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Libre, resultado.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadNoDebeAfectarsePorIncidenciasDeOtrosTecnicos()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            repositorio.Registros.Add(CrearEmpleado(2, "Luis Pérez", true, true, new DateTime(2026, 11, 1, 9, 1, 0)));

            var incidencias = new List<Incidencia>
            {
                CrearIncidencia(1, 2, EstadoIncidencia.Pendiente)
            };

            var resultado = CrearControlador(repositorio).ConsultarDisponibilidad(1, incidencias);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Libre, resultado.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadConIncidenciasLegacySinResponsableEmpleadoIdDebeSerLibre()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));

            var incidencias = new List<Incidencia>
            {
                CrearIncidencia(1, null, EstadoIncidencia.Pendiente)
            };

            var resultado = CrearControlador(repositorio).ConsultarDisponibilidad(1, incidencias);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Libre, resultado.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadNoDebeModificarEmpleadosNiIncidencias()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));

            var incidencias = new List<Incidencia>
            {
                CrearIncidencia(1, 1, EstadoIncidencia.Pendiente)
            };

            var nombreAntes = repositorio.Registros[0].NombreCompleto;
            var estadoAntes = incidencias[0].Estado;
            var responsableAntes = incidencias[0].ResponsableEmpleadoId;

            var resultado = CrearControlador(repositorio).ConsultarDisponibilidad(1, incidencias);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(nombreAntes, repositorio.Registros[0].NombreCompleto);
            Assert.AreEqual(estadoAntes, incidencias[0].Estado);
            Assert.AreEqual(responsableAntes, incidencias[0].ResponsableEmpleadoId);
        }

        [TestMethod]
        public void ConsultarDisponibilidadDebeCambiarCuandoCambiaEstadoDeIncidencia()
        {
            var repositorio = new EmpleadoRepositoryDoble();
            repositorio.Registros.Add(CrearEmpleado(1, "Ana Torres", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));

            var incidencias = new List<Incidencia>
            {
                CrearIncidencia(1, 1, EstadoIncidencia.EnProceso)
            };

            var controlador = CrearControlador(repositorio);
            var ocupada = controlador.ConsultarDisponibilidad(1, incidencias);
            incidencias[0].Estado = EstadoIncidencia.Resuelta;
            var libre = controlador.ConsultarDisponibilidad(1, incidencias);

            Assert.IsTrue(ocupada.Exito);
            Assert.IsTrue(libre.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Ocupado, ocupada.Datos);
            Assert.AreEqual(DisponibilidadOperativa.Libre, libre.Datos);
        }

        private static EmpleadoController CrearControlador(EmpleadoRepositoryDoble repositorio)
        {
            return new EmpleadoController(repositorio);
        }

        private static Empleado CrearEmpleado(int id, string nombre, bool esTecnico, bool activo, DateTime fechaCreacion)
        {
            return new Empleado(id, nombre, esTecnico, activo, fechaCreacion);
        }

        private static Incidencia CrearIncidencia(int id, int? responsableEmpleadoId, EstadoIncidencia estado)
        {
            return new Incidencia
            {
                Identificador = id,
                Titulo = "Titulo",
                Descripcion = "Descripcion valida",
                ReportadoPor = "Reportero",
                Responsable = "Tecnico",
                ResponsableEmpleadoId = responsableEmpleadoId,
                Prioridad = Prioridad.Media,
                Estado = estado,
                FechaCreacion = new DateTime(2026, 11, 1, 9, 0, 0)
            };
        }
    }
}
