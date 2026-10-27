using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using GestorIncidencias.Entidades;

namespace GestorIncidencias.Controlador
{
    public class XmlIncidenciaRepository : IIncidenciaRepository
    {
        private const int VersionFormatoActual = 1;
        private const string ReportadoPorLegacyPlaceholder = "No informado (registro legado)";
        private readonly string rutaArchivo;
        private bool puedeGuardar = true;

        public XmlIncidenciaRepository()
            : this(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "GestorIncidencias",
                "incidencias.xml"))
        {
        }

        public XmlIncidenciaRepository(string rutaArchivo)
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

        public ResultadoOperacion<IList<Incidencia>> Cargar()
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    puedeGuardar = true;
                    return ExitoCarga(new List<Incidencia>());
                }

                IncidenciasXmlData datos;

                using (var stream = File.OpenRead(rutaArchivo))
                {
                    datos = (IncidenciasXmlData)CrearSerializer().ReadObject(stream);
                }

                ValidarDatosXml(datos);

                puedeGuardar = true;
                return ExitoCarga(MapearADominio(datos.Incidencias));
            }
            catch (Exception ex)
            {
                puedeGuardar = false;

                return new ResultadoOperacion<IList<Incidencia>>
                {
                    Exito = false,
                    Mensaje = "No se pudieron cargar las incidencias desde el archivo XML.",
                    Errores = new List<string> { ex.Message },
                    Datos = new List<Incidencia>()
                };
            }
        }

        public ResultadoOperacion Guardar(IList<Incidencia> incidencias)
        {
            if (!puedeGuardar)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = "El repositorio no permite guardar porque la carga del XML falló previamente."
                };
            }

            var incidenciasNormalizadas = ClonarLista(incidencias ?? new List<Incidencia>());

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

                var temporal = Path.Combine(directorio, Path.GetFileName(rutaArchivo) + ".tmp");
                var respaldo = rutaArchivo + ".bak";

                var datos = new IncidenciasXmlData
                {
                    Version = VersionFormatoActual,
                    SiguienteIdentificador = CalcularSiguienteIdentificador(incidenciasNormalizadas),
                    Incidencias = MapearADto(incidenciasNormalizadas)
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
                    Mensaje = "Incidencias guardadas correctamente."
                };
            }
            catch (Exception ex)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = "No se pudieron guardar las incidencias en el archivo XML.",
                    Errores = new List<string> { ex.Message }
                };
            }
        }

        private static ResultadoOperacion<IList<Incidencia>> ExitoCarga(IList<Incidencia> incidencias)
        {
            return new ResultadoOperacion<IList<Incidencia>>
            {
                Exito = true,
                Datos = ClonarLista(incidencias)
            };
        }

        private static DataContractSerializer CrearSerializer()
        {
            return new DataContractSerializer(typeof(IncidenciasXmlData));
        }

        private static int CalcularSiguienteIdentificador(IList<Incidencia> incidencias)
        {
            if (incidencias == null || incidencias.Count == 0)
            {
                return 1;
            }

            return incidencias.Max(i => i.Identificador) + 1;
        }

        protected virtual void ValidarArchivoTemporal(string rutaTemporal)
        {
            using (var validacion = File.OpenRead(rutaTemporal))
            {
                CrearSerializer().ReadObject(validacion);
            }
        }

        private static IList<Incidencia> ClonarLista(IList<Incidencia> incidencias)
        {
            var origen = incidencias ?? new List<Incidencia>();
            var copia = new List<Incidencia>(origen.Count);

            foreach (var incidencia in origen)
            {
                copia.Add(Clonar(incidencia));
            }

            return copia;
        }

        private static Incidencia Clonar(Incidencia incidencia)
        {
            if (incidencia == null)
            {
                return new Incidencia();
            }

            return new Incidencia
            {
                Identificador = incidencia.Identificador,
                Titulo = incidencia.Titulo,
                Descripcion = incidencia.Descripcion,
                ReportadoPor = incidencia.ReportadoPor,
                Responsable = incidencia.Responsable,
                ResponsableEmpleadoId = incidencia.ResponsableEmpleadoId,
                Prioridad = incidencia.Prioridad,
                Estado = incidencia.Estado,
                FechaCreacion = incidencia.FechaCreacion
            };
        }

        private static List<IncidenciaXmlData> MapearADto(IList<Incidencia> incidencias)
        {
            var origen = incidencias ?? new List<Incidencia>();
            var resultado = new List<IncidenciaXmlData>(origen.Count);

            foreach (var incidencia in origen)
            {
                resultado.Add(new IncidenciaXmlData
                {
                    Identificador = incidencia.Identificador,
                    Titulo = incidencia.Titulo,
                    Descripcion = incidencia.Descripcion,
                    ReportadoPor = NormalizarReportadoPorParaPersistencia(incidencia.ReportadoPor),
                    Responsable = incidencia.Responsable,
                    ResponsableEmpleadoId = incidencia.ResponsableEmpleadoId,
                    Prioridad = (int)incidencia.Prioridad,
                    Estado = (int)incidencia.Estado,
                    FechaCreacion = incidencia.FechaCreacion
                });
            }

            return resultado;
        }

        private static IList<Incidencia> MapearADominio(IList<IncidenciaXmlData> incidencias)
        {
            var origen = incidencias ?? new List<IncidenciaXmlData>();
            var resultado = new List<Incidencia>(origen.Count);

            foreach (var incidencia in origen)
            {
                resultado.Add(new Incidencia
                {
                    Identificador = incidencia.Identificador,
                    Titulo = incidencia.Titulo,
                    Descripcion = incidencia.Descripcion,
                    ReportadoPor = incidencia.ReportadoPor,
                    Responsable = incidencia.Responsable,
                    ResponsableEmpleadoId = incidencia.ResponsableEmpleadoId,
                    Prioridad = (Prioridad)incidencia.Prioridad,
                    Estado = (EstadoIncidencia)incidencia.Estado,
                    FechaCreacion = incidencia.FechaCreacion
                });
            }

            return resultado;
        }

        private static string NormalizarReportadoPorParaPersistencia(string reportadoPor)
        {
            if (string.IsNullOrWhiteSpace(reportadoPor))
            {
                return null;
            }

            var recortado = reportadoPor.Trim();

            if (string.Equals(recortado, ReportadoPorLegacyPlaceholder, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return recortado;
        }

        private static void ValidarDatosXml(IncidenciasXmlData datos)
        {
            if (datos == null)
            {
                throw new SerializationException("El contenido XML no es válido.");
            }

            if (datos.Version != VersionFormatoActual)
            {
                throw new SerializationException("La versión del archivo XML no es compatible.");
            }

            var incidencias = datos.Incidencias ?? new List<IncidenciaXmlData>();
            var ids = new HashSet<int>();

            foreach (var incidencia in incidencias)
            {
                if (incidencia == null)
                {
                    throw new SerializationException("El archivo XML contiene incidencias inválidas.");
                }

                if (incidencia.Identificador <= 0 || !ids.Add(incidencia.Identificador))
                {
                    throw new SerializationException("El archivo XML contiene identificadores inválidos o duplicados.");
                }

                if (string.IsNullOrWhiteSpace(incidencia.Titulo) ||
                    string.IsNullOrWhiteSpace(incidencia.Descripcion) ||
                    string.IsNullOrWhiteSpace(incidencia.Responsable))
                {
                    throw new SerializationException("El archivo XML contiene campos obligatorios vacíos.");
                }

                if (!Enum.IsDefined(typeof(Prioridad), incidencia.Prioridad) ||
                    !Enum.IsDefined(typeof(EstadoIncidencia), incidencia.Estado))
                {
                    throw new SerializationException("El archivo XML contiene valores de prioridad o estado inválidos.");
                }

                if (incidencia.FechaCreacion == default(DateTime))
                {
                    throw new SerializationException("El archivo XML contiene fechas inválidas.");
                }

                if (incidencia.ResponsableEmpleadoId.HasValue && incidencia.ResponsableEmpleadoId.Value <= 0)
                {
                    throw new SerializationException("El archivo XML contiene identificadores de responsable empleado inválidos.");
                }
            }

            if (datos.SiguienteIdentificador <= 0)
            {
                throw new SerializationException("El archivo XML contiene un siguiente identificador inválido.");
            }

            if (incidencias.Count > 0 && datos.SiguienteIdentificador <= incidencias.Max(i => i.Identificador))
            {
                throw new SerializationException("El archivo XML contiene un siguiente identificador inconsistente.");
            }
        }

        [DataContract(Name = "IncidenciasPersistidas")]
        private class IncidenciasXmlData
        {
            [DataMember(Order = 1, IsRequired = true)]
            public int Version { get; set; }

            [DataMember(Order = 2, IsRequired = true)]
            public int SiguienteIdentificador { get; set; }

            [DataMember(Order = 3, IsRequired = true)]
            public List<IncidenciaXmlData> Incidencias { get; set; }
        }

        [DataContract(Name = "Incidencia")]
        private class IncidenciaXmlData
        {
            [DataMember(Order = 1, IsRequired = true)]
            public int Identificador { get; set; }

            [DataMember(Order = 2, IsRequired = true)]
            public string Titulo { get; set; }

            [DataMember(Order = 3, IsRequired = true)]
            public string Descripcion { get; set; }

            [DataMember(Order = 4, IsRequired = false, EmitDefaultValue = false)]
            public string ReportadoPor { get; set; }

            [DataMember(Order = 5, IsRequired = true)]
            public string Responsable { get; set; }

            [DataMember(Order = 6, IsRequired = false, EmitDefaultValue = false)]
            public int? ResponsableEmpleadoId { get; set; }

            [DataMember(Order = 7, IsRequired = true)]
            public int Prioridad { get; set; }

            [DataMember(Order = 8, IsRequired = true)]
            public int Estado { get; set; }

            [DataMember(Order = 9, IsRequired = true)]
            public DateTime FechaCreacion { get; set; }
        }
    }
}
