# Plan técnico: Gestión de incidencias

**Rama**: `docs/especificaciones` | **Feature**: `001-gestion-incidencias` | **Fecha**: 2026-10-24  
**Especificación**: [spec.md](spec.md)

## Resumen

Se ampliará la aplicación Windows Forms existente para incorporar `ReportadoPor`, mantener
compatibilidad con XML antiguos de siete campos, mejorar la experiencia visual del formulario
principal y añadir una vista previa de reportes en `frmVistaPreviaReporte` con exportación HTML
UTF-8 escapada.

En esta iteración documental también se define el arranque del módulo de administración de
empleados (fases SDD 1-3): requisitos, modelo, contratos y planificación técnica sin crear código.
El módulo cubrirá alta/consulta/edición/búsqueda de empleados, gestión Activo/Inactivo,
selección de técnicos activos como responsables y compatibilidad con responsables históricos
almacenados como texto en incidencias legacy.

Evolución aprobada para siguiente incremento: disponibilidad operativa de técnicos calculada desde
incidencias vinculadas por `ResponsableEmpleadoId`, diferenciada del estado laboral Activo/Inactivo,
y acceso directo al módulo mediante botón `Administrar empleados` en `frmIncidencias`.

Las reglas seguirán fuera de los eventos de la interfaz para poder probarse sin abrir formularios.
La implementación conservará el proyecto clásico `GestorIncidencias.csproj`, su destino
.NET Framework 4.7.2 y la configuración C# 7.3, sin dependencias de impresión ni paquetes nuevos.

## Evolución T105: filtro visual Activas en frmIncidencias

T105 introduce una extensión de filtrado visual en `frmIncidencias` sin alterar persistencia XML,
transiciones de estado ni contratos de repositorio. Se añadirá la opción `Activas` al
`cboFiltroEstado`, seleccionada por defecto al abrir el formulario.

La semántica de `Activas` será: incluir incidencias en `Pendiente` y `En proceso`, excluir
`Resuelta`. El resto de opciones (`Todos`, `Pendiente`, `En proceso`, `Resuelta`) se mantiene
sin cambios para preservar consulta histórica.

La búsqueda por texto seguirá combinándose con el filtro de estado mediante condición AND. El
refresco tras registrar, editar o cambiar estado conservará el filtro activo, por lo que una
incidencia que pase a `Resuelta` dejará de verse automáticamente cuando el filtro sea `Activas`,
sin eliminación física en XML.

Para minimizar riesgo, se reutilizará la lógica actual de `AplicarBusquedaYFiltro`, incorporando
una validación específica de estado activo sin duplicar rutas de consulta. También se comprobará
que el flujo de selección no deje referencias inválidas cuando una fila desaparezca del conjunto
visible por efecto del filtro.

## Evolución T106: registro de incidencias sin técnico asignado

T106 introduce la opción funcional `Sin asignar` en `frmEditarIncidencia` para permitir altas y
ediciones sin vínculo técnico cuando no exista un técnico elegible o cuando el operador decida
registrar la incidencia sin asignación inmediata.

La representación de dominio se mantiene compatible con el modelo actual: `ResponsableEmpleadoId`
en `null` y `Responsable` textual coherente (`Sin asignar`), sin cambiar el formato XML ni crear
identificadores sintéticos. Las reglas de bloqueo existentes para asignaciones con técnico se
mantienen: si hay `ResponsableEmpleadoId`, debe corresponder a técnico activo, de soporte y libre
(excepto conservación del mismo técnico histórico durante edición).

## Estado comprobado y decisiones previstas

### Comprobado en el repositorio

- `GestorIncidencias.slnx` contiene únicamente `GestorIncidencias/GestorIncidencias.csproj`.
- El proyecto es no-SDK, de tipo `WinExe`, y apunta a .NET Framework 4.7.2.
- `Program.cs` inicia `new frmIncidencias()` dentro del namespace `GestorIncidencias`.
- `frmIncidencias.cs` contiene actualmente solo el constructor y `InitializeComponent()`.
- `frmIncidencias.Designer.cs` ya contiene el diseño base T039 validado en diseñador y ejecución F5.
- `MSBuild.exe` y `dotnet.exe` están disponibles en el entorno de Visual Studio 2026.
- La solución compiló correctamente antes de esta planificación.
- No hay `packages.config`, `Directory.Packages.props` ni proyecto de pruebas existente.
- La cobertura actual automatizada es 79/79 pruebas y RF-020 permanece con cobertura parcial en
  validación funcional de interfaz.

### Previsto, no implementado

- Carpetas, clases de dominio, controlador, persistencia y controles descritos en este documento.
- Proyecto `GestorIncidencias.Tests`, paquetes NuGet y referencias de solución.
- Workflow de GitHub Actions.
- Campo `ReportadoPor` en entidad, reglas, controlador, formularios y persistencia.
- Compatibilidad de lectura para XML legado sin `ReportadoPor`, con representación explícita sin
  inventar personas.
- Formulario `frmVistaPreviaReporte` para visualización previa y exportación local a HTML UTF-8.
- Ajustes visuales adicionales sobre base T039 (filtro de estado compacto/alineado, área de
  mensajes visible, botón de vista previa).
- No se han generado código, proyecto de pruebas ni workflow en esta fase.

## Contexto técnico

**Lenguaje y versión**: C# 7.3, configuración actual del proyecto.  
**Framework objetivo**: .NET Framework 4.7.2.  
**Dependencias principales**: referencias de framework existentes; `System.Runtime.Serialization`
para `DataContractSerializer`; MSTest solo en el proyecto de pruebas.  
**Persistencia**: XML local en `%LOCALAPPDATA%\GestorIncidencias\incidencias.xml`, con temporal,
respaldo y compatibilidad de lectura para esquema legacy de siete campos.  
**Pruebas**: MSTest con `Microsoft.NET.Test.Sdk` 18.11.0, `MSTest.TestAdapter` 4.5.1 y
`MSTest.TestFramework` 4.5.1, fijados en `packages.config`.  
**Plataforma**: Windows, Visual Studio 2026, aplicación de escritorio de una sola instancia.  
**Tipo de proyecto**: aplicación Windows Forms clásica y proyecto de pruebas .NET Framework.  
**Rendimiento previsto**: para el alcance inicial, las operaciones de lista, búsqueda y filtro
se ejecutarán sobre la colección local y deberán responder sin esperas perceptibles para un
volumen pequeño de incidencias.  
**Restricciones**: una sola instancia activa, sin red, sin múltiples usuarios, sin base de datos
externa, sin cambiar el framework ni convertir el proyecto a SDK, sin `PrintDocument`,
`PrintPreviewDialog`, `PrintPreviewControl` ni `PrintDialog`.

## Comprobación de la constitución

| Principio | Aplicación en este plan | Estado |
|---|---|---|
| Visual Studio y Copilot | Desarrollo y revisión en Visual Studio 2026 con GitHub Copilot. | Cumple |
| C# y Windows Forms | Se conserva C#, WinForms y .NET Framework 4.7.2. | Cumple |
| Especificaciones | Este plan deriva de `spec.md` y precede a tareas e implementación. | Cumple |
| Tareas revisadas | Las tareas se generarán después con `/speckit-tasks`. | Previsto |
| Separación | Entidades, Controlador y Formularios con reglas fuera de la UI. | Previsto |
| Diseñador | Controles y eventos visuales se definirán en archivos `.Designer.cs`. | Previsto |
| Validación | Validador reutilizable y mensajes claros en español. | Previsto |
| TDD | Pruebas de reglas antes de completar su implementación. | Previsto |
| Integración | Rama corta, PR, revisión y GitHub Actions hacia `master`. | Previsto |
| Mejora specification-first | No se implementa nada fuera de esta planificación. | Cumple |

No se detectan violaciones que requieran complejidad adicional.

## Estructura de carpetas y archivos

### Árbol de documentación de la funcionalidad

```text
specs/001-gestion-incidencias/
├── spec.md                              # Requisitos funcionales y criterios de aceptación
├── plan.md                              # Este plan técnico
├── research.md                          # Decisiones y alternativas investigadas
├── data-model.md                        # Entidades y reglas de datos previstas
├── quickstart.md                        # Comandos y validación local previstos
├── contracts/
│   ├── business-rules.md                # Contratos verificables de reglas de negocio
│   ├── repository-contract.md           # Contrato operativo de carga/guardado y confirmación
│   └── requirements-traceability.md     # Trazabilidad RF-001 en adelante (incluye empleados)
└── checklists/requirements.md            # Checklist de calidad de la especificación
```

### Árbol de código propuesto

```text
GestorIncidencias/
├── Entidades/
│   ├── Incidencia.cs                     # Entidad simple con propiedades get/set
│   ├── Empleado.cs                       # Entidad de empleado con rol técnico y estado
│   ├── Prioridad.cs                      # Valores Baja, Media y Alta
│   └── EstadoIncidencia.cs               # Pendiente, EnProceso y Resuelta
├── Controlador/
│   ├── IncidenciaController.cs           # Casos de uso, edición, estados, consulta
│   ├── EmpleadoController.cs             # Casos de uso de alta, edición, consulta y búsqueda
│   ├── EmpleadoValidator.cs              # Validaciones de nombre y reglas del empleado
│   ├── IncidenciaValidator.cs            # Validaciones compartidas de alta y edición
│   ├── IncidenciaRepository.cs           # Contrato de carga y guardado local
│   ├── XmlIncidenciaRepository.cs        # XML, temporal, respaldo y datos dañados
│   ├── EmpleadoRepository.cs             # Contrato de persistencia de empleados
│   ├── XmlEmpleadoRepository.cs          # Persistencia XML segura para empleados
│   ├── ResultadoOperacion.cs             # Resultado y mensaje sin depender de WinForms
│   └── SingleInstanceGuard.cs            # Bloqueo adquirido y liberado por Program.Main
├── Formularios/
│   ├── frmIncidencias.cs                 # Eventos y coordinación con el controlador
│   ├── frmIncidencias.Designer.cs        # Controles, layout y eventos de UI
│   ├── frmEditarIncidencia.cs            # Edición de incidencia con responsable técnico activo
│   ├── frmEditarIncidencia.Designer.cs   # Controles de edición compatibles con diseñador
│   ├── frmEmpleados.cs                   # Administración y búsqueda de empleados
│   ├── frmEmpleados.Designer.cs          # Diseño WinForms del módulo empleados
│   ├── frmEditarEmpleado.cs              # Alta/edición de empleado
│   ├── frmEditarEmpleado.Designer.cs     # Diseño WinForms de alta/edición empleado
│   └── frmIncidencias.resx               # Recursos del formulario
├── Program.cs                            # Arranque existente; mantiene frmIncidencias
├── App.config                             # Configuración existente, solo si fuera necesario
└── GestorIncidencias.csproj              # Referencias y archivos del proyecto clásico

GestorIncidencias.Tests/
├── Entidades/
│   └── IncidenciaTests.cs                # Valores iniciales e invariantes de entidad
├── Controlador/
│   ├── IncidenciaValidatorTests.cs       # Límites y errores de validación
│   ├── IncidenciaControllerTests.cs      # Registro, edición, estados, búsqueda y orden
│   ├── XmlIncidenciaRepositoryTests.cs   # Persistencia aislada y fallos de almacenamiento
│   ├── EmpleadoValidatorTests.cs         # Reglas de validación del empleado
│   ├── EmpleadoControllerTests.cs        # Alta, edición, búsqueda y estado Activo/Inactivo
│   └── XmlEmpleadoRepositoryTests.cs     # Persistencia XML segura del catálogo
├── packages.config                        # Versiones NuGet fijadas para MSTest
└── GestorIncidencias.Tests.csproj         # Proyecto clásico .NET Framework 4.7.2

.github/workflows/ci.yml                   # Workflow Windows previsto, aún no creado
```

El proyecto clásico debe incluir explícitamente en su `.csproj` cada archivo `.cs`, los recursos
`.resx`, `ProjectReference`, referencias de framework e imports NuGet necesarios. La referencia
`System.Runtime.Serialization` se añadirá para `DataContractSerializer`. El proyecto de pruebas
incluirá explícitamente sus fuentes, `packages.config`, la referencia al proyecto principal y los
imports/targets de `Microsoft.NET.Test.Sdk` y `MSTest.TestAdapter`; NuGet resolverá y verificará
las dependencias transitivas.

La ubicación actual de `frmIncidencias` está en la raíz del proyecto. Durante la implementación
se podrá mover a `Formularios` actualizando las entradas explícitas del `.csproj`, manteniendo el
namespace, el nombre de clase, los archivos dependientes y la apertura desde `Program.cs`.
Si el diseñador de Visual Studio muestra riesgo de pérdida de metadatos, se conservará la ruta
actual y se usará `Formularios` como separación lógica para los nuevos formularios.

### Responsabilidades

- **Entidades**: datos y enumeraciones sin referencias a `System.Windows.Forms`.
- **Controlador**: reglas de negocio, validación, consulta, persistencia y protección de instancia.
- **Formularios**: controles, lectura de valores, eventos, mensajes y actualización de la vista.
- **Pruebas**: ejercitan Entidades y Controlador con datos temporales, sin abrir ventanas.

## Interfaz Windows Forms

### Formulario principal `frmIncidencias`

El formulario conservará su nombre, namespace y entrada desde `Program.cs`. Sus controles se
crearán en `frmIncidencias.Designer.cs`, dentro de `InitializeComponent()`, para que Visual
Studio 2026 pueda mostrarlos y editarlos en el diseñador.

Controles previstos:

- `DataGridView dgvIncidencias`: identificador, título, descripción, `ReportadoPor`, responsable,
  prioridad, estado y fecha de creación; selección de una incidencia.
- `TextBox txtTitulo`: título de alta y edición.
- `TextBox txtDescripcion`: descripción multilínea.
- `TextBox txtReportadoPor`: persona que comunica la incidencia.
- `TextBox txtResponsable`: técnico responsable de atenderla.
- `ComboBox cboPrioridad`: Baja, Media, Alta; Media seleccionada inicialmente.
- `ComboBox cboFiltroEstado`: Todos, Pendiente, En proceso, Resuelta.
- `TextBox txtBuscar`: búsqueda mientras se escribe.
- `ComboBox cboNuevoEstado`: estado a solicitar.
- `Button btnNuevo`, `btnGuardar`, `btnEditar`, `btnCambiarEstado`, `btnCancelar` y
  `btnVistaPreviaReporte`.
- `Label lblMensaje`: mensajes de validación, error, estado vacío y confirmación.
- `Label` auxiliares para que cada entrada tenga un texto identificable.

Eventos previstos:

- `Load`: usar el controlador ya preparado por `Program.Main`, cargar datos y refrescar la lista;
  no adquiere ni libera otro bloqueo.
- `txtBuscar.TextChanged`: volver a consultar aplicando búsqueda y filtro.
- `cboFiltroEstado.SelectedIndexChanged`: volver a consultar aplicando el filtro.
- `btnNuevo.Click`: limpiar el modo de edición y aplicar valores iniciales.
- `btnGuardar.Click`: construir la solicitud (incluyendo `ReportadoPor`), invocar registro o
  edición y refrescar solo si el controlador devuelve éxito.
- `btnEditar.Click`: cargar los datos seleccionados en los controles editables.
- `btnCambiarEstado.Click`: solicitar una transición independiente y refrescar su resultado.
- `btnCancelar.Click`: descartar valores no confirmados.
- `btnVistaPreviaReporte.Click`: abrir `frmVistaPreviaReporte` con el resultado actual de
  búsqueda y filtro.
- `FormClosing`: liberar recursos de instancia y cerrar sin guardar cambios no confirmados.

Los eventos no decidirán longitudes, estados permitidos, orden, identificadores ni formato de
persistencia. Solo traducirán valores de controles a solicitudes del controlador y mostrarán el
resultado en español.

## Lógica de negocio

### Validación

`IncidenciaValidator` recortará título, descripción, `ReportadoPor` y responsable antes de
validar. Rechazará valores vacíos, límites inferiores/superiores y prioridades desconocidas.
Devolverá errores por campo sin tocar la incidencia anterior. Las mismas reglas se usarán en alta
y edición.

### Identificadores y registro

`IncidenciaController` mantendrá el siguiente identificador positivo a partir del máximo cargado
más uno. Si no hay datos, comenzará en uno. Al guardar una alta deberá persistir también el
identificador asignado; nunca se reutilizará un identificador confirmado. Los títulos no se
compararán para unicidad.

### Edición y estados

La edición copiará únicamente título, descripción, `ReportadoPor`, responsable y prioridad
después de validar.
Identificador, fecha y estado permanecerán invariantes. El cambio de estado tendrá un método
separado y una tabla explícita de transiciones. El mismo estado será una operación idempotente;
las demás combinaciones devolverán error sin mutar la incidencia.

### Consulta

La lista se ordenará por fecha descendente y luego identificador descendente. La búsqueda
normalizará mayúsculas/minúsculas usando una comparación cultural definida y buscará subcadenas
en título y responsable. El filtro de estado se aplicará después o como condición equivalente;
la búsqueda vacía no excluirá ninguna incidencia del filtro.

## Persistencia local

El contrato detallado de carga, guardado, confirmación y bloqueo de sobrescritura se documenta
en `contracts/repository-contract.md`, manteniendo las firmas de `IIncidenciaRepository`.

La capa de persistencia deberá aceptar XML antiguo sin `ReportadoPor` y cargarlo sin pérdida de
datos, preservando identificadores y valores existentes. Para registros legacy sin ese campo, la
representación visible será `No informado (registro legado)` sin inventar nombres ni reescribir
automáticamente el archivo original.

## Vista previa y exportación de reportes

Se añadirá `frmVistaPreviaReporte` como formulario independiente y compatible con diseñador.
Recibirá la colección ya filtrada/buscada desde `frmIncidencias` y mostrará encabezado
empresarial, fecha/hora de generación y tabla con: identificador, título, `ReportadoPor`,
responsable, prioridad, estado y fecha.

La exportación generará un archivo HTML local en UTF-8 con escape de contenido textual. El flujo
no dependerá de impresoras ni usará `PrintDocument`, `PrintPreviewDialog`,
`PrintPreviewControl` o `PrintDialog`.

### Ubicación y formato

`XmlIncidenciaRepository` usará `%LOCALAPPDATA%\GestorIncidencias\incidencias.xml`. El XML
contendrá una colección, el siguiente identificador seguro y una versión de formato. Se usará
`DataContractSerializer` para evitar una dependencia de serialización externa y mantener la
compatibilidad con .NET Framework 4.7.2.

### Inicio y guardado

- Si la carpeta o el archivo no existen, se inicializa una colección vacía con siguiente
  identificador uno.
- Antes de confirmar una operación, se crea una copia inmutable de la colección en memoria.
- El guardado se realiza a un temporal situado en la misma carpeta.
- Se cierra y valida el temporal; después se reemplaza el archivo principal y se conserva una
  copia de respaldo anterior.
- Solo después del reemplazo exitoso se actualiza la colección en memoria y se muestra éxito.
- Si falla cualquier paso, se conserva la colección anterior, se informa del error y no se
  confirma la operación.

### Carga y datos dañados

La carga validará XML, versión, tipos, identificadores positivos y únicos, fechas, prioridades,
estados y campos obligatorios. Si falla, conservará el archivo original sin repararlo ni
sobrescribirlo automáticamente, marcará el repositorio como no guardable y mostrará un mensaje
claro. La UI no anunciará datos recuperados como válidos cuando la carga haya fallado.

### Una sola instancia

`Program.Main` adquirirá un `Mutex` global/local con nombre estable antes de crear
`frmIncidencias` o cargar datos. Si otra instancia lo posee, la segunda mostrará un mensaje y
terminará sin crear el formulario ni tocar el archivo. El objeto de bloqueo se mantendrá vivo
durante toda la ejecución y se liberará en un bloque `finally` de `Main` al terminar la aplicación.
El evento `Load` de `frmIncidencias` no adquirirá ni liberará otro bloqueo.

## Proyecto de pruebas MSTest

Se propondrá `GestorIncidencias.Tests` como proyecto clásico dirigido a .NET Framework 4.7.2,
referenciando el proyecto de aplicación. La consulta de NuGet del 2026-10-24 identificó estas
versiones estables publicadas y compatibles con el grupo `.NETFramework4.6.2`, que incluye 4.7.2:

- `Microsoft.NET.Test.Sdk` 18.11.0: plataforma de descubrimiento y ejecución para proyectos
  .NET Framework y VSTest; su paquete declara grupo `.NETFramework4.6.2`.
- `MSTest.TestAdapter` 4.5.1: adaptador mantenido para Visual Studio/VSTest y grupo
  `.NETFramework4.6.2`; depende de `MSTest.TestFramework` 4.5.1.
- `MSTest.TestFramework` 4.5.1: atributos y aserciones MSTest para `.NETFramework4.6.2`.

Las versiones exactas se fijan para evitar restauraciones no reproducibles. La compatibilidad de
paquetes se comprobó leyendo sus metadatos publicados; la restauración y ejecución real quedan
pendientes de la implementación. Las dependencias transitivas las resolverá NuGet. No se
añadirán dependencias de UI a las pruebas de reglas.

Cobertura prevista:

- Límites válidos e inválidos de título, descripción y responsable, incluidos espacios.
- Prioridad inicial, prioridades desconocidas y títulos repetidos.
- Identificadores iniciales, reinicio y no reutilización.
- Orden por fecha y desempate por identificador.
- Edición válida, edición inválida e invariantes.
- Las tres transiciones permitidas, estado actual y transiciones rechazadas.
- Búsqueda parcial, mayúsculas/minúsculas, búsqueda vacía y combinación de filtros.
- Archivo ausente, carga válida, XML dañado, formato inválido y guardado fallido.
- Datos temporales por prueba, sin usar el directorio de usuario real.

### Primer ciclo TDD

La primera regla será la validación de longitud del título. Antes del rojo solo se preparará el
esqueleto mínimo compilable: el proyecto de pruebas, sus referencias, `packages.config`, una
clase de prueba con los `using` y atributos MSTest necesarios, y las firmas mínimas que permitan
compilar. La regla seguirá devolviendo un resultado incorrecto de forma intencionada; no se
aceptará un rojo causado por clases, métodos, referencias o paquetes inexistentes.

1. **Rojo**: ejecutar `IncidenciaValidatorTests` con título de 4 caracteres rechazado y título de
   5 caracteres aceptado; guardar un TRX y la salida de MSTest mostrando el fallo de la regla
   aún no implementada, con compilación y descubrimiento correctos.
2. **Verde**: implementar la lógica mínima de recorte y límites; ejecutar MSTest y conservar la
	  salida TRX con ambos casos pasando.
3. **Refactorización**: extraer constantes o una función común si mejora la claridad, volver a
	  ejecutar toda la suite, conservar un TRX sin regresiones y guardar el diff revisado.

La evidencia se conservará en el Pull Request o en la documentación de ejecución: comandos,
resultado rojo, resultado verde, diff de refactorización y resultado final de la suite.

## Compilación e integración continua

### Comandos locales previstos

Para el proyecto existente, el comando principal será MSBuild de Visual Studio. El pipeline debe
tratar como error cualquier fallo de restauración, compilación, descubrimiento o ejecución:

```powershell
msbuild .\GestorIncidencias.slnx /t:Build /p:Configuration=Debug /m
```

La restauración del futuro proyecto clásico se hará con `nuget.exe restore` sobre la solución y
después se repetirá MSBuild. Se localizará `vstest.console.exe` mediante `vswhere.exe` y se
usará la ruta de la instalación de Visual Studio 2026; como alternativa documentada se usará la
ruta de `Common7\IDE\CommonExtensions\Microsoft\TestWindow`. El adaptador MSTest se localizará
en la carpeta `packages\MSTest.TestAdapter.4.5.1\build\_common` o la ruta equivalente que
NuGet haya restaurado, y se verificará que VSTest lo carga.

```powershell
vstest.console.exe .\GestorIncidencias.Tests\bin\Debug\GestorIncidencias.Tests.dll /TestAdapterPath:.\packages\MSTest.TestAdapter.4.5.1\build\_common /Logger:"trx;LogFileName=tests.trx" /TestCaseFilter:"TestCategory!=Inconclusive"
```

El script deberá comprobar que `vstest.console.exe` existe, que el adaptador se carga, que se
descubre al menos una prueba y que el proceso termina con código cero. Un conjunto vacío de
pruebas será un fallo explícito. El archivo TRX se conservará como evidencia aun cuando las
pruebas fallen; el pipeline devolverá código distinto de cero ante cualquier fallo.
No se usará `dotnet test` como comando principal porque el proyecto será clásico y usará
`packages.config`.

### GitHub Actions previsto

Se creará posteriormente `.github/workflows/ci.yml` con `windows-latest` y estos pasos:

1. `actions/checkout` para obtener la rama.
2. Configurar NuGet y localizar MSBuild mediante las acciones oficiales.
3. Restaurar `packages.config` y dependencias de la solución.
4. Compilar `GestorIncidencias.slnx` en `Release` con MSBuild.
5. Ejecutar `GestorIncidencias.Tests.dll` con VSTest.
6. Publicar resultados de pruebas como artefacto o resumen de workflow.
7. Publicar el contenido compilado `bin\Release` como artefacto `GestorIncidencias-release`.

El workflow será una fase posterior y no se crea en este plan.

## Verificación y trazabilidad

| Requisitos | Verificación prevista |
|---|---|
| RF-001–RF-002 | Pruebas de alta válida y recorte de campos. |
| RF-003–RF-005 | Pruebas de límites inferior/superior, fuera de rango y solo espacios. |
| RF-006 | Pruebas de prioridades válidas, valor Media inicial y prioridad inválida. |
| RF-007–RF-008 | Pruebas de identificador positivo, unicidad tras carga y títulos repetidos. |
| RF-009–RF-010 | Pruebas de proyección y orden; revisión manual de columnas de `DataGridView`. |
| RF-011–RF-012 | Pruebas de edición válida, rechazo e invariantes. |
| RF-013–RF-015 | Pruebas de las tres transiciones, estado actual y rechazo. |
| RF-016–RF-018 | Pruebas de subcadena, mayúsculas/minúsculas, filtros y combinación. |
| RF-019 | Prueba de guardar, cerrar, abrir y recuperar en directorio temporal. |
| RF-020 | Prueba con ruta o stream de almacenamiento fallido; confirmar estado anterior y sin éxito falso. |
| RF-021 | Prueba con XML dañado; confirmar mensaje, preservación y bloqueo de guardado automático. |
| RF-022–RF-023 | Pruebas de rechazo sin mutación y revisión manual de estados vacíos/mensajes. |
| RF-024–RF-025 | Pruebas de validación de `ReportadoPor` (2..80, recorte, vacío inválido) en alta y edición. |
| RF-026–RF-027 | Pruebas de listado/edición y persistencia de `ReportadoPor` en XML ampliado. |
| RF-028–RF-030 | Pruebas de compatibilidad XML legado sin pérdida y conservación de identificadores/orden. |
| RF-031–RF-032 | Verificación manual y pruebas de apertura de `frmVistaPreviaReporte` con encabezado y timestamp. |
| RF-033–RF-036 | Pruebas de datos reales filtrados en reporte, estado vacío y exportación HTML UTF-8 escapada. |
| RF-037 | Revisión de implementación y pruebas para confirmar ausencia de APIs de impresión prohibidas. |
| RF-038 | Verificación manual del layout T039+ (filtro compacto/alineado, área de mensajes visible, botón de vista previa). |
| RF-039–RF-040 | Pruebas unitarias de alta y validación de nombre de empleado (recorte y límites). |
| RF-041–RF-042 | Pruebas de consulta/edición de empleado con conservación de identificador y fecha. |
| RF-043–RF-044 | Pruebas de ciclo Activo/Inactivo y búsqueda parcial por nombre. |
| RF-045–RF-046 | Pruebas de persistencia XML de empleados, fallos controlados y preservación de datos. |
| RF-047–RF-050 | Pruebas de integración incidencia-empleado: selección de técnicos activos, compatibilidad legacy de responsable textual y preservación de `ReportadoPor`. |
| RF-051–RF-057 | Pruebas de cálculo de disponibilidad operativa derivada, recálculo por alta/edición/cambio de estado de incidencias vinculadas y no persistencia de bandera fija. |
| RF-058 | Pruebas de compatibilidad legacy para asegurar ausencia de vinculación automática por nombre textual de `Responsable`. |
| RF-059 | Pruebas de orden/priorización de selector técnico y documentación de política pendiente para técnicos ocupados. |
| RF-060–RF-061 | Verificación de interfaz: columna `Disponibilidad` en `frmEmpleados` y botón `Administrar empleados` en `frmIncidencias` con estilo XAMY y apertura correcta. |

Verificación manual adicional:

- Abrir `frmIncidencias` en el diseñador de Visual Studio 2026 y confirmar que se cargan todos
  los controles sin excepción.
- Confirmar que los controles están definidos en `frmIncidencias.Designer.cs`, no creados
  exclusivamente en tiempo de ejecución.
- Ejecutar la aplicación desde Visual Studio y revisar alta, edición, estado, filtros y errores.
- Confirmar que el formulario principal sigue siendo `frmIncidencias`.
- Revisar que la interfaz no contiene reglas de negocio duplicadas.
- Verificar que `frmVistaPreviaReporte` abre con datos reales según búsqueda/filtro vigente.
- Verificar exportación HTML UTF-8 con caracteres especiales escapados.
- Verificar que no existe dependencia de componentes de impresión prohibidos.

## Pull Request e integración

La implementación se realizará en una rama corta asociada a `001-gestion-incidencias`. Antes del
Pull Request hacia `master` se actualizarán las especificaciones si aparece una decisión nueva,
se ejecutará el ciclo TDD, se compilará la solución, se ejecutarán todas las pruebas, se revisará
el diseñador y se verificará la matriz RF. El Pull Request deberá enlazar `spec.md`, `plan.md` y
las tareas generadas, incluir evidencia roja/verde/refactorización, resultados de CI y el
artefacto compilado. La revisión deberá comprobar la constitución, especialmente separación,
validación, persistencia segura y compatibilidad con el diseñador.

## Constitution Check posterior al diseño

- Se conserva Visual Studio 2026, GitHub Copilot, C#, Windows Forms y .NET Framework 4.7.2.
- Las reglas son independientes de los formularios y se pueden probar con MSTest.
- El diseñador mantiene los controles en archivos `.Designer.cs`.
- La persistencia usa una dependencia mínima y contempla datos ausentes, errores y corrupción.
- Se mantiene compatibilidad con XML legacy sin pérdida de información histórica.
- El plan incluye especificación, aclaraciones, plan técnico, tareas posteriores, TDD,
  verificación y Pull Request a `master`.
- No se crean código, pruebas ni workflow durante esta fase.

**Resultado**: plan técnico listo para generar tareas con `/speckit-tasks`.

## Seguimiento de complejidad

No se solicita una excepción constitucional. El proyecto de pruebas es un segundo proyecto
justificado por el principio de pruebas automatizadas y por la separación de reglas respecto de
la interfaz. No se añade base de datos, capa web ni infraestructura de red.
