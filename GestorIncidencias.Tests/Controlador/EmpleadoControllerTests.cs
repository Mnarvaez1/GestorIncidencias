using System;
using System.Collections.Generic;
using System.Linq;
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

        [TestMethod]
        public void ConsultarDisponibilidadDebeRecalcularConDatosActualesTrasCambioDeEstadoPersistido()
        {
            var repositorioEmpleados = new EmpleadoRepositoryDoble();
            repositorioEmpleados.Registros.Add(CrearEmpleado(1, "Michael", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            var repositorioIncidencias = new IncidenciaRepositoryDoble();
            var controladorIncidencias = CrearControladorIncidencias(repositorioIncidencias);
            var controladorEmpleados = CrearControlador(repositorioEmpleados);

            var creada = controladorIncidencias.Registrar(
                "Error base",
                "Descripción válida para recálculo.",
                "María",
                "Michael",
                Prioridad.Media,
                1);

            var ocupada = controladorEmpleados.ConsultarDisponibilidad(1, controladorIncidencias.Consultar().Datos);

            var enProceso = controladorIncidencias.CambiarEstado(creada.Datos.Identificador, EstadoIncidencia.EnProceso);
            var resuelta = controladorIncidencias.CambiarEstado(creada.Datos.Identificador, EstadoIncidencia.Resuelta);

            var libre = controladorEmpleados.ConsultarDisponibilidad(1, controladorIncidencias.Consultar().Datos);

            Assert.IsTrue(creada.Exito);
            Assert.IsTrue(ocupada.Exito);
            Assert.IsTrue(enProceso.Exito);
            Assert.IsTrue(resuelta.Exito);
            Assert.IsTrue(libre.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Ocupado, ocupada.Datos);
            Assert.AreEqual(DisponibilidadOperativa.Libre, libre.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadConDosAbiertasYResolviendoUnaDebePermanecerOcupado()
        {
            var repositorioEmpleados = new EmpleadoRepositoryDoble();
            repositorioEmpleados.Registros.Add(CrearEmpleado(1, "Michael", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            var repositorioIncidencias = new IncidenciaRepositoryDoble();
            var controladorIncidencias = CrearControladorIncidencias(repositorioIncidencias);
            var controladorEmpleados = CrearControlador(repositorioEmpleados);

            var una = controladorIncidencias.Registrar(
                "Error red",
                "Descripción válida de red.",
                "María",
                "Michael",
                Prioridad.Media,
                1);
            var dos = controladorIncidencias.Registrar(
                "Error api",
                "Descripción válida de api.",
                "María",
                "Michael",
                Prioridad.Alta,
                1);

            var aEnProceso = controladorIncidencias.CambiarEstado(una.Datos.Identificador, EstadoIncidencia.EnProceso);
            var aResuelta = controladorIncidencias.CambiarEstado(una.Datos.Identificador, EstadoIncidencia.Resuelta);

            var disponibilidad = controladorEmpleados.ConsultarDisponibilidad(1, controladorIncidencias.Consultar().Datos);

            Assert.IsTrue(una.Exito);
            Assert.IsTrue(dos.Exito);
            Assert.IsTrue(aEnProceso.Exito);
            Assert.IsTrue(aResuelta.Exito);
            Assert.IsTrue(disponibilidad.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Ocupado, disponibilidad.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadReasignacionAbiertaDebeLiberarSalienteYOcuparEntrante()
        {
            var repositorioEmpleados = new EmpleadoRepositoryDoble();
            repositorioEmpleados.Registros.Add(CrearEmpleado(1, "Michael", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            repositorioEmpleados.Registros.Add(CrearEmpleado(2, "Laura", true, true, new DateTime(2026, 11, 1, 9, 1, 0)));
            var repositorioIncidencias = new IncidenciaRepositoryDoble();
            var controladorIncidencias = CrearControladorIncidencias(repositorioIncidencias);
            var controladorEmpleados = CrearControlador(repositorioEmpleados);

            var creada = controladorIncidencias.Registrar(
                "Error base",
                "Descripción válida para reasignar.",
                "María",
                "Michael",
                Prioridad.Media,
                1);

            var edicion = controladorIncidencias.Editar(
                creada.Datos.Identificador,
                "Error base",
                "Descripción válida para reasignar.",
                "María",
                "Laura",
                Prioridad.Media,
                2);

            var incidenciasActuales = controladorIncidencias.Consultar().Datos;
            var michael = controladorEmpleados.ConsultarDisponibilidad(1, incidenciasActuales);
            var laura = controladorEmpleados.ConsultarDisponibilidad(2, incidenciasActuales);

            Assert.IsTrue(creada.Exito);
            Assert.IsTrue(edicion.Exito);
            Assert.IsTrue(michael.Exito);
            Assert.IsTrue(laura.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Libre, michael.Datos);
            Assert.AreEqual(DisponibilidadOperativa.Ocupado, laura.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadReasignacionConOtraAbiertaDebeMantenerOcupadoAlSaliente()
        {
            var repositorioEmpleados = new EmpleadoRepositoryDoble();
            repositorioEmpleados.Registros.Add(CrearEmpleado(1, "Michael", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            repositorioEmpleados.Registros.Add(CrearEmpleado(2, "Laura", true, true, new DateTime(2026, 11, 1, 9, 1, 0)));
            var repositorioIncidencias = new IncidenciaRepositoryDoble();
            var controladorIncidencias = CrearControladorIncidencias(repositorioIncidencias);
            var controladorEmpleados = CrearControlador(repositorioEmpleados);

            var una = controladorIncidencias.Registrar(
                "Error base",
                "Descripción válida primera.",
                "María",
                "Michael",
                Prioridad.Media,
                1);
            var otra = controladorIncidencias.Registrar(
                "Error extra",
                "Descripción válida segunda.",
                "María",
                "Michael",
                Prioridad.Alta,
                1);

            var edicion = controladorIncidencias.Editar(
                una.Datos.Identificador,
                "Error base",
                "Descripción válida primera.",
                "María",
                "Laura",
                Prioridad.Media,
                2);

            var incidenciasActuales = controladorIncidencias.Consultar().Datos;
            var michael = controladorEmpleados.ConsultarDisponibilidad(1, incidenciasActuales);
            var laura = controladorEmpleados.ConsultarDisponibilidad(2, incidenciasActuales);

            Assert.IsTrue(una.Exito);
            Assert.IsTrue(otra.Exito);
            Assert.IsTrue(edicion.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Ocupado, michael.Datos);
            Assert.AreEqual(DisponibilidadOperativa.Ocupado, laura.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadReasignarResueltaNoDebeOcuparAlReceptor()
        {
            var repositorioEmpleados = new EmpleadoRepositoryDoble();
            repositorioEmpleados.Registros.Add(CrearEmpleado(1, "Michael", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            repositorioEmpleados.Registros.Add(CrearEmpleado(2, "Laura", true, true, new DateTime(2026, 11, 1, 9, 1, 0)));
            var repositorioIncidencias = new IncidenciaRepositoryDoble();
            var controladorIncidencias = CrearControladorIncidencias(repositorioIncidencias);
            var controladorEmpleados = CrearControlador(repositorioEmpleados);

            var creada = controladorIncidencias.Registrar(
                "Error base",
                "Descripción válida para cierre.",
                "María",
                "Michael",
                Prioridad.Media,
                1);

            var aEnProceso = controladorIncidencias.CambiarEstado(creada.Datos.Identificador, EstadoIncidencia.EnProceso);
            var aResuelta = controladorIncidencias.CambiarEstado(creada.Datos.Identificador, EstadoIncidencia.Resuelta);

            var edicion = controladorIncidencias.Editar(
                creada.Datos.Identificador,
                "Error base",
                "Descripción válida para cierre.",
                "María",
                "Laura",
                Prioridad.Media,
                2);

            var incidenciasActuales = controladorIncidencias.Consultar().Datos;
            var michael = controladorEmpleados.ConsultarDisponibilidad(1, incidenciasActuales);
            var laura = controladorEmpleados.ConsultarDisponibilidad(2, incidenciasActuales);

            Assert.IsTrue(creada.Exito);
            Assert.IsTrue(aEnProceso.Exito);
            Assert.IsTrue(aResuelta.Exito);
            Assert.IsTrue(edicion.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Libre, michael.Datos);
            Assert.AreEqual(DisponibilidadOperativa.Libre, laura.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadEditarReportadoPorYResponsableTextoSinCambiarIdNoDebeAlterarDisponibilidad()
        {
            var repositorioEmpleados = new EmpleadoRepositoryDoble();
            repositorioEmpleados.Registros.Add(CrearEmpleado(1, "Michael", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            var repositorioIncidencias = new IncidenciaRepositoryDoble();
            var controladorIncidencias = CrearControladorIncidencias(repositorioIncidencias);
            var controladorEmpleados = CrearControlador(repositorioEmpleados);

            var creada = controladorIncidencias.Registrar(
                "Error base",
                "Descripción válida para edición.",
                "María",
                "Michael",
                Prioridad.Media,
                1);

            var antes = controladorEmpleados.ConsultarDisponibilidad(1, controladorIncidencias.Consultar().Datos);

            var editada = controladorIncidencias.Editar(
                creada.Datos.Identificador,
                "Error base actualizado",
                "Descripción válida para edición actualizada.",
                "Laura",
                "Soporte Nivel 2",
                Prioridad.Alta,
                1);

            var incidenciasActuales = controladorIncidencias.Consultar().Datos;
            var despues = controladorEmpleados.ConsultarDisponibilidad(1, incidenciasActuales);

            Assert.IsTrue(creada.Exito);
            Assert.IsTrue(antes.Exito);
            Assert.IsTrue(editada.Exito);
            Assert.IsTrue(despues.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Ocupado, antes.Datos);
            Assert.AreEqual(DisponibilidadOperativa.Ocupado, despues.Datos);
            Assert.AreEqual(1, incidenciasActuales[0].ResponsableEmpleadoId);
        }

        [TestMethod]
        public void ConsultarDisponibilidadTecnicoInactivoDebePermanecerNoCalculadaSinMutarDatos()
        {
            var repositorioEmpleados = new EmpleadoRepositoryDoble();
            repositorioEmpleados.Registros.Add(CrearEmpleado(1, "Michael", true, false, new DateTime(2026, 11, 1, 9, 0, 0)));
            var incidencias = new List<Incidencia>
            {
                CrearIncidencia(10, 1, EstadoIncidencia.Pendiente)
            };

            var controlador = CrearControlador(repositorioEmpleados);
            var resultado = controlador.ConsultarDisponibilidad(1, incidencias);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(DisponibilidadOperativa.NoCalculada, resultado.Datos);
            Assert.AreEqual(10, incidencias[0].Identificador);
            Assert.AreEqual(EstadoIncidencia.Pendiente, incidencias[0].Estado);
            Assert.AreEqual(1, incidencias[0].ResponsableEmpleadoId);
        }

        [TestMethod]
        public void ConsultarDisponibilidadConRecalculoNoDebeEliminarNiAlterarIdentificadoresDeIncidencias()
        {
            var repositorioEmpleados = new EmpleadoRepositoryDoble();
            repositorioEmpleados.Registros.Add(CrearEmpleado(1, "Michael", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            repositorioEmpleados.Registros.Add(CrearEmpleado(2, "Laura", true, true, new DateTime(2026, 11, 1, 9, 1, 0)));
            var repositorioIncidencias = new IncidenciaRepositoryDoble();
            var controladorIncidencias = CrearControladorIncidencias(repositorioIncidencias);
            var controladorEmpleados = CrearControlador(repositorioEmpleados);

            var una = controladorIncidencias.Registrar(
                "Error base",
                "Descripción válida primera.",
                "María",
                "Michael",
                Prioridad.Media,
                1);
            var dos = controladorIncidencias.Registrar(
                "Error extra",
                "Descripción válida segunda.",
                "María",
                "Michael",
                Prioridad.Alta,
                1);

            var idUno = una.Datos.Identificador;
            var idDos = dos.Datos.Identificador;

            var edicion = controladorIncidencias.Editar(
                idUno,
                "Error base",
                "Descripción válida primera.",
                "María",
                "Laura",
                Prioridad.Media,
                2);

            var incidenciasActuales = controladorIncidencias.Consultar().Datos;
            var disponibilidadMichael = controladorEmpleados.ConsultarDisponibilidad(1, incidenciasActuales);
            var disponibilidadLaura = controladorEmpleados.ConsultarDisponibilidad(2, incidenciasActuales);

            Assert.IsTrue(una.Exito);
            Assert.IsTrue(dos.Exito);
            Assert.IsTrue(edicion.Exito);
            Assert.IsTrue(disponibilidadMichael.Exito);
            Assert.IsTrue(disponibilidadLaura.Exito);
            Assert.AreEqual(2, incidenciasActuales.Count);
            Assert.IsTrue(incidenciasActuales.Any(i => i.Identificador == idUno));
            Assert.IsTrue(incidenciasActuales.Any(i => i.Identificador == idDos));
            Assert.AreEqual(DisponibilidadOperativa.Ocupado, disponibilidadMichael.Datos);
            Assert.AreEqual(DisponibilidadOperativa.Ocupado, disponibilidadLaura.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadSinListaDebeUsarIncidenciasRealesYDevolverLibreSinVinculos()
        {
            var repositorioEmpleados = new EmpleadoRepositoryDoble();
            repositorioEmpleados.Registros.Add(CrearEmpleado(1, "Michael", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            var repositorioIncidencias = new IncidenciaRepositoryDoble();
            repositorioIncidencias.Registros.Add(CrearIncidencia(1, null, EstadoIncidencia.Pendiente));

            var controlador = CrearControlador(repositorioEmpleados, repositorioIncidencias);
            var resultado = controlador.ConsultarDisponibilidad(1);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Libre, resultado.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadSinListaConPendienteRealDebeSerOcupado()
        {
            var repositorioEmpleados = new EmpleadoRepositoryDoble();
            repositorioEmpleados.Registros.Add(CrearEmpleado(1, "Michael", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            var repositorioIncidencias = new IncidenciaRepositoryDoble();
            repositorioIncidencias.Registros.Add(CrearIncidencia(1, 1, EstadoIncidencia.Pendiente));

            var controlador = CrearControlador(repositorioEmpleados, repositorioIncidencias);
            var resultado = controlador.ConsultarDisponibilidad(1);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Ocupado, resultado.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadSinListaConEnProcesoRealDebeSerOcupado()
        {
            var repositorioEmpleados = new EmpleadoRepositoryDoble();
            repositorioEmpleados.Registros.Add(CrearEmpleado(1, "Michael", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            var repositorioIncidencias = new IncidenciaRepositoryDoble();
            repositorioIncidencias.Registros.Add(CrearIncidencia(1, 1, EstadoIncidencia.EnProceso));

            var controlador = CrearControlador(repositorioEmpleados, repositorioIncidencias);
            var resultado = controlador.ConsultarDisponibilidad(1);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Ocupado, resultado.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadSinListaConResueltaRealDebeSerLibre()
        {
            var repositorioEmpleados = new EmpleadoRepositoryDoble();
            repositorioEmpleados.Registros.Add(CrearEmpleado(1, "Michael", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            var repositorioIncidencias = new IncidenciaRepositoryDoble();
            repositorioIncidencias.Registros.Add(CrearIncidencia(1, 1, EstadoIncidencia.Resuelta));

            var controlador = CrearControlador(repositorioEmpleados, repositorioIncidencias);
            var resultado = controlador.ConsultarDisponibilidad(1);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Libre, resultado.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadSinListaTrasCambioEstadoGuardadoDebeActualizarResultado()
        {
            var repositorioEmpleados = new EmpleadoRepositoryDoble();
            repositorioEmpleados.Registros.Add(CrearEmpleado(1, "Michael", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            var repositorioIncidencias = new IncidenciaRepositoryDoble();
            var controladorIncidencias = CrearControladorIncidencias(repositorioIncidencias);
            var controlador = CrearControlador(repositorioEmpleados, repositorioIncidencias);

            var creada = controladorIncidencias.Registrar(
                "Error base",
                "Descripción válida para recálculo.",
                "María",
                "Michael",
                Prioridad.Media,
                1);

            var ocupada = controlador.ConsultarDisponibilidad(1);

            var enProceso = controladorIncidencias.CambiarEstado(creada.Datos.Identificador, EstadoIncidencia.EnProceso);
            var resuelta = controladorIncidencias.CambiarEstado(creada.Datos.Identificador, EstadoIncidencia.Resuelta);

            var libre = controlador.ConsultarDisponibilidad(1);

            Assert.IsTrue(creada.Exito);
            Assert.IsTrue(ocupada.Exito);
            Assert.IsTrue(enProceso.Exito);
            Assert.IsTrue(resuelta.Exito);
            Assert.IsTrue(libre.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Ocupado, ocupada.Datos);
            Assert.AreEqual(DisponibilidadOperativa.Libre, libre.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadSinListaTrasReasignacionGuardadaDebeRecalcularSalienteYEntrante()
        {
            var repositorioEmpleados = new EmpleadoRepositoryDoble();
            repositorioEmpleados.Registros.Add(CrearEmpleado(1, "Michael", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            repositorioEmpleados.Registros.Add(CrearEmpleado(2, "Laura", true, true, new DateTime(2026, 11, 1, 9, 1, 0)));
            var repositorioIncidencias = new IncidenciaRepositoryDoble();
            var controladorIncidencias = CrearControladorIncidencias(repositorioIncidencias);
            var controlador = CrearControlador(repositorioEmpleados, repositorioIncidencias);

            var creada = controladorIncidencias.Registrar(
                "Error base",
                "Descripción válida para reasignar.",
                "María",
                "Michael",
                Prioridad.Media,
                1);

            var reasignada = controladorIncidencias.Editar(
                creada.Datos.Identificador,
                "Error base",
                "Descripción válida para reasignar.",
                "María",
                "Laura",
                Prioridad.Media,
                2);

            var michael = controlador.ConsultarDisponibilidad(1);
            var laura = controlador.ConsultarDisponibilidad(2);

            Assert.IsTrue(creada.Exito);
            Assert.IsTrue(reasignada.Exito);
            Assert.IsTrue(michael.Exito);
            Assert.IsTrue(laura.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Libre, michael.Datos);
            Assert.AreEqual(DisponibilidadOperativa.Ocupado, laura.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadSinListaConDosAbiertasYResolviendoUnaDebeSeguirOcupado()
        {
            var repositorioEmpleados = new EmpleadoRepositoryDoble();
            repositorioEmpleados.Registros.Add(CrearEmpleado(1, "Michael", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            var repositorioIncidencias = new IncidenciaRepositoryDoble();
            var controladorIncidencias = CrearControladorIncidencias(repositorioIncidencias);
            var controlador = CrearControlador(repositorioEmpleados, repositorioIncidencias);

            var una = controladorIncidencias.Registrar(
                "Error red",
                "Descripción válida de red.",
                "María",
                "Michael",
                Prioridad.Media,
                1);
            var dos = controladorIncidencias.Registrar(
                "Error api",
                "Descripción válida de api.",
                "María",
                "Michael",
                Prioridad.Alta,
                1);

            var aEnProceso = controladorIncidencias.CambiarEstado(una.Datos.Identificador, EstadoIncidencia.EnProceso);
            var aResuelta = controladorIncidencias.CambiarEstado(una.Datos.Identificador, EstadoIncidencia.Resuelta);

            var disponibilidad = controlador.ConsultarDisponibilidad(1);

            Assert.IsTrue(una.Exito);
            Assert.IsTrue(dos.Exito);
            Assert.IsTrue(aEnProceso.Exito);
            Assert.IsTrue(aResuelta.Exito);
            Assert.IsTrue(disponibilidad.Exito);
            Assert.AreEqual(DisponibilidadOperativa.Ocupado, disponibilidad.Datos);
        }

        [TestMethod]
        public void ConsultarDisponibilidadSinListaConErrorDeCargaIncidenciasNoDebeDevolverLibreFalso()
        {
            var repositorioEmpleados = new EmpleadoRepositoryDoble();
            repositorioEmpleados.Registros.Add(CrearEmpleado(1, "Michael", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            var repositorioIncidencias = new IncidenciaRepositoryDoble
            {
                CargarDebeFallar = true,
                CargarMensajeError = "Error real al cargar incidencias."
            };

            var controlador = CrearControlador(repositorioEmpleados, repositorioIncidencias);
            var resultado = controlador.ConsultarDisponibilidad(1);

            Assert.IsFalse(resultado.Exito);
            Assert.AreEqual(DisponibilidadOperativa.NoCalculada, resultado.Datos);
            Assert.AreEqual("Error real al cargar incidencias.", resultado.Mensaje);
        }

        [TestMethod]
        public void ConsultarDisponibilidadSinListaNoDebeMutarRegistrosDeIncidencias()
        {
            var repositorioEmpleados = new EmpleadoRepositoryDoble();
            repositorioEmpleados.Registros.Add(CrearEmpleado(1, "Michael", true, true, new DateTime(2026, 11, 1, 9, 0, 0)));
            var repositorioIncidencias = new IncidenciaRepositoryDoble();
            repositorioIncidencias.Registros.Add(CrearIncidencia(7, 1, EstadoIncidencia.Pendiente));

            var controlador = CrearControlador(repositorioEmpleados, repositorioIncidencias);

            var idAntes = repositorioIncidencias.Registros[0].Identificador;
            var estadoAntes = repositorioIncidencias.Registros[0].Estado;
            var responsableAntes = repositorioIncidencias.Registros[0].ResponsableEmpleadoId;

            var resultado = controlador.ConsultarDisponibilidad(1);

            Assert.IsTrue(resultado.Exito);
            Assert.AreEqual(idAntes, repositorioIncidencias.Registros[0].Identificador);
            Assert.AreEqual(estadoAntes, repositorioIncidencias.Registros[0].Estado);
            Assert.AreEqual(responsableAntes, repositorioIncidencias.Registros[0].ResponsableEmpleadoId);
            Assert.AreEqual(1, repositorioIncidencias.Registros.Count);
        }

        private static EmpleadoController CrearControlador(EmpleadoRepositoryDoble repositorio)
        {
            return new EmpleadoController(repositorio);
        }

        private static EmpleadoController CrearControlador(
            EmpleadoRepositoryDoble repositorioEmpleados,
            IncidenciaRepositoryDoble repositorioIncidencias)
        {
            return new EmpleadoController(repositorioEmpleados, repositorioIncidencias);
        }

        private static IncidenciaController CrearControladorIncidencias(IncidenciaRepositoryDoble repositorio)
        {
            return new IncidenciaController(repositorio);
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
