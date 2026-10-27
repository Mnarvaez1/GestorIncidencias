using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using GestorIncidencias.Entidades;

namespace GestorIncidencias.Controlador
{
    public class XmlEmpleadoRepository : IEmpleadoRepository
    {
        private const int VersionFormatoActual = 1;
        private readonly string rutaArchivo;
        private bool puedeGuardar = true;

        public XmlEmpleadoRepository()
            : this(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "GestorIncidencias",
                "empleados.xml"))
        {
        }

        public XmlEmpleadoRepository(string rutaArchivo)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivo))
            {
                throw new ArgumentException("La ruta del archivo XML es obligatoria.", nameof(rutaArchivo));
            }

            this.rutaArchivo = rutaArchivo;
        }

        public bool PuedeGuardar
        {
            get { return puedeGuardar; }
        }

        public ResultadoOperacion<IList<Empleado>> Cargar()
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    puedeGuardar = true;
                    return ExitoCarga(new List<Empleado>());
                }

                EmpleadosXmlData datos;

                using (var stream = File.OpenRead(rutaArchivo))
                {
                    datos = (EmpleadosXmlData)CrearSerializer().ReadObject(stream);
                }

                ValidarDatosXml(datos);

                puedeGuardar = true;
                return ExitoCarga(MapearADominio(datos.Empleados));
            }
            catch (Exception ex)
            {
                puedeGuardar = false;

                return new ResultadoOperacion<IList<Empleado>>
                {
                    Exito = false,
                    Mensaje = "No se pudieron cargar los empleados desde el archivo XML.",
                    Errores = new List<string> { ex.Message },
                    Datos = new List<Empleado>()
                };
            }
        }

        public ResultadoOperacion Guardar(IList<Empleado> empleados)
        {
            if (!puedeGuardar)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = "El repositorio no permite guardar porque la carga del XML falló previamente."
                };
            }

            var empleadosNormalizados = ClonarLista(empleados ?? new List<Empleado>());
            var validacionLista = ValidarEmpleadosParaGuardado(empleadosNormalizados);
            if (!validacionLista.Exito)
            {
                return validacionLista;
            }

            string temporal = null;

            try
            {
                var directorio = Path.GetDirectoryName(rutaArchivo);

                if (string.IsNullOrWhiteSpace(directorio))
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = "La ruta del archivo XML no es válida."
                    };
                }

                Directory.CreateDirectory(directorio);

                temporal = Path.Combine(directorio, Path.GetFileName(rutaArchivo) + ".tmp");
                var respaldo = rutaArchivo + ".bak";

                var datos = new EmpleadosXmlData
                {
                    Version = VersionFormatoActual,
                    SiguienteIdentificador = CalcularSiguienteIdentificador(empleadosNormalizados),
                    Empleados = MapearADto(empleadosNormalizados)
                };

                using (var stream = File.Create(temporal))
                {
                    CrearSerializer().WriteObject(stream, datos);
                    stream.Flush();
                }

                ValidarArchivoTemporal(temporal);

                if (File.Exists(rutaArchivo))
                {
                    if (File.Exists(respaldo))
                    {
                        File.Delete(respaldo);
                    }

                    File.Replace(temporal, rutaArchivo, respaldo);
                }
                else
                {
                    File.Move(temporal, rutaArchivo);
                }

                return new ResultadoOperacion
                {
                    Exito = true,
                    Mensaje = "Empleados guardados correctamente."
                };
            }
            catch (Exception ex)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = "No se pudieron guardar los empleados en el archivo XML.",
                    Errores = new List<string> { ex.Message }
                };
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(temporal) && File.Exists(temporal))
                {
                    try
                    {
                        File.Delete(temporal);
                    }
                    catch
                    {
                    }
                }
            }
        }

        protected virtual void ValidarArchivoTemporal(string rutaTemporal)
        {
            using (var validacion = File.OpenRead(rutaTemporal))
            {
                var datos = (EmpleadosXmlData)CrearSerializer().ReadObject(validacion);
                ValidarDatosXml(datos);
            }
        }

        private static ResultadoOperacion ValidarEmpleadosParaGuardado(IList<Empleado> empleados)
        {
            var ids = new HashSet<int>();
            var validador = new EmpleadoValidator();

            foreach (var empleado in empleados)
            {
                var validacion = validador.Validar(empleado);
                if (!validacion.Exito)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = validacion.Mensaje,
                        Errores = validacion.Errores
                    };
                }

                if (empleado.FechaCreacion == default(DateTime))
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = "La fecha de creación del empleado es obligatoria.",
                        Errores = new List<string> { "La fecha de creación del empleado es obligatoria." }
                    };
                }

                if (!ids.Add(empleado.Identificador))
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = "La colección de empleados contiene identificadores duplicados.",
                        Errores = new List<string> { "La colección de empleados contiene identificadores duplicados." }
                    };
                }
            }

            return new ResultadoOperacion { Exito = true };
        }

        private static ResultadoOperacion<IList<Empleado>> ExitoCarga(IList<Empleado> empleados)
        {
            return new ResultadoOperacion<IList<Empleado>>
            {
                Exito = true,
                Datos = ClonarLista(empleados)
            };
        }

        private static DataContractSerializer CrearSerializer()
        {
            return new DataContractSerializer(typeof(EmpleadosXmlData));
        }

        private static int CalcularSiguienteIdentificador(IList<Empleado> empleados)
        {
            if (empleados == null || empleados.Count == 0)
            {
                return 1;
            }

            return empleados.Max(e => e.Identificador) + 1;
        }

        private static IList<Empleado> ClonarLista(IList<Empleado> empleados)
        {
            var origen = empleados ?? new List<Empleado>();
            var copia = new List<Empleado>(origen.Count);

            foreach (var empleado in origen)
            {
                copia.Add(Clonar(empleado));
            }

            return copia;
        }

        private static Empleado Clonar(Empleado empleado)
        {
            if (empleado == null)
            {
                return null;
            }

            return new Empleado(
                empleado.Identificador,
                empleado.NombreCompleto,
                empleado.EsTecnicoSoporte,
                empleado.Activo,
                empleado.FechaCreacion);
        }

        private static List<EmpleadoXmlData> MapearADto(IList<Empleado> empleados)
        {
            var origen = empleados ?? new List<Empleado>();
            var resultado = new List<EmpleadoXmlData>(origen.Count);

            foreach (var empleado in origen)
            {
                resultado.Add(new EmpleadoXmlData
                {
                    Identificador = empleado.Identificador,
                    NombreCompleto = empleado.NombreCompleto,
                    EsTecnicoSoporte = empleado.EsTecnicoSoporte,
                    Activo = empleado.Activo,
                    FechaCreacion = empleado.FechaCreacion
                });
            }

            return resultado;
        }

        private static IList<Empleado> MapearADominio(IList<EmpleadoXmlData> empleados)
        {
            var origen = empleados ?? new List<EmpleadoXmlData>();
            var resultado = new List<Empleado>(origen.Count);

            foreach (var empleado in origen)
            {
                resultado.Add(new Empleado(
                    empleado.Identificador,
                    empleado.NombreCompleto,
                    empleado.EsTecnicoSoporte,
                    empleado.Activo,
                    empleado.FechaCreacion));
            }

            return resultado;
        }

        private static void ValidarDatosXml(EmpleadosXmlData datos)
        {
            if (datos == null)
            {
                throw new SerializationException("El contenido XML no es válido.");
            }

            if (datos.Version != VersionFormatoActual)
            {
                throw new SerializationException("La versión del archivo XML no es compatible.");
            }

            var empleados = datos.Empleados ?? new List<EmpleadoXmlData>();
            var ids = new HashSet<int>();
            var validador = new EmpleadoValidator();

            foreach (var empleado in empleados)
            {
                if (empleado == null)
                {
                    throw new SerializationException("El archivo XML contiene empleados inválidos.");
                }

                var entidad = new Empleado(
                    empleado.Identificador,
                    empleado.NombreCompleto,
                    empleado.EsTecnicoSoporte,
                    empleado.Activo,
                    empleado.FechaCreacion);

                var validacion = validador.Validar(entidad);
                if (!validacion.Exito)
                {
                    throw new SerializationException(validacion.Mensaje ?? "El archivo XML contiene datos de empleados inválidos.");
                }

                if (empleado.FechaCreacion == default(DateTime))
                {
                    throw new SerializationException("El archivo XML contiene fechas inválidas.");
                }

                if (!ids.Add(empleado.Identificador))
                {
                    throw new SerializationException("El archivo XML contiene identificadores inválidos o duplicados.");
                }
            }

            if (datos.SiguienteIdentificador <= 0)
            {
                throw new SerializationException("El archivo XML contiene un siguiente identificador inválido.");
            }

            if (empleados.Count > 0 && datos.SiguienteIdentificador <= empleados.Max(i => i.Identificador))
            {
                throw new SerializationException("El archivo XML contiene un siguiente identificador inconsistente.");
            }
        }

        [DataContract(Name = "EmpleadosPersistidos")]
        private class EmpleadosXmlData
        {
            [DataMember(Order = 1, IsRequired = true)]
            public int Version { get; set; }

            [DataMember(Order = 2, IsRequired = true)]
            public int SiguienteIdentificador { get; set; }

            [DataMember(Order = 3, IsRequired = true)]
            public List<EmpleadoXmlData> Empleados { get; set; }
        }

        [DataContract(Name = "Empleado")]
        private class EmpleadoXmlData
        {
            [DataMember(Order = 1, IsRequired = true)]
            public int Identificador { get; set; }

            [DataMember(Order = 2, IsRequired = true)]
            public string NombreCompleto { get; set; }

            [DataMember(Order = 3, IsRequired = true)]
            public bool EsTecnicoSoporte { get; set; }

            [DataMember(Order = 4, IsRequired = true)]
            public bool Activo { get; set; }

            [DataMember(Order = 5, IsRequired = true)]
            public DateTime FechaCreacion { get; set; }
        }
    }
}
