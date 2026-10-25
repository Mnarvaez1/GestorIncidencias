# Plan técnico: Gestión de incidencias

**Rama**: `docs/especificaciones` | **Feature**: `001-gestion-incidencias` | **Fecha**: 2026-10-24  
**Especificación**: [spec.md](spec.md)

## Resumen

Se ampliará la aplicación Windows Forms existente para que `frmIncidencias` permita registrar,
consultar, editar, cambiar estados, buscar y filtrar incidencias, con persistencia local segura.
Las reglas vivirán fuera de los eventos de la interfaz para poder probarse sin abrir formularios.

La implementación conservará el proyecto clásico `GestorIncidencias.csproj`, su destino .NET
Framework 4.7.2 y la configuración C# 7.3. Se añadirá un proyecto MSTest clásico dirigido al
mismo framework y un workflow de GitHub Actions para Windows.

## Estado comprobado y decisiones previstas

### Comprobado en el repositorio

- `GestorIncidencias.slnx` contiene únicamente `GestorIncidencias/GestorIncidencias.csproj`.
- El proyecto es no-SDK, de tipo `WinExe`, y apunta a .NET Framework 4.7.2.
- `Program.cs` inicia `new frmIncidencias()` dentro del namespace `GestorIncidencias`.
- `frmIncidencias.cs` contiene actualmente solo el constructor y `InitializeComponent()`.
- `frmIncidencias.Designer.cs` contiene actualmente el formulario vacío sin controles funcionales.
- `MSBuild.exe` y `dotnet.exe` están disponibles en el entorno de Visual Studio 2026.
- La solución compiló correctamente antes de esta planificación.
- No hay `packages.config`, `Directory.Packages.props` ni proyecto de pruebas existente.
- CE-001 confirma siete datos mostrables: identificador, título, descripción, responsable,
  prioridad, estado y fecha de creación.

### Previsto, no implementado

- Carpetas, clases de dominio, controlador, persistencia y controles descritos en este documento.
- Proyecto `GestorIncidencias.Tests`, paquetes NuGet y referencias de solución.
- Workflow de GitHub Actions.
- XML de datos de usuario, mutex de instancia única y pruebas de recuperación.
- No se han generado código, proyecto de pruebas ni workflow en esta fase.

## Contexto técnico

**Lenguaje y versión**: C# 7.3, configuración actual del proyecto.  
**Framework objetivo**: .NET Framework 4.7.2.  
**Dependencias principales**: referencias de framework existentes; `System.Runtime.Serialization`
para `DataContractSerializer`; MSTest solo en el proyecto de pruebas.  
**Persistencia**: XML local en `%LOCALAPPDATA%\GestorIncidencias\incidencias.xml`, con temporal y
respaldo.  
**Pruebas**: MSTest con `Microsoft.NET.Test.Sdk` 18.11.0, `MSTest.TestAdapter` 4.5.1 y
`MSTest.TestFramework` 4.5.1, fijados en `packages.config`.  
**Plataforma**: Windows, Visual Studio 2026, aplicación de escritorio de una sola instancia.  
**Tipo de proyecto**: aplicación Windows Forms clásica y proyecto de pruebas .NET Framework.  
**Rendimiento previsto**: para el alcance inicial, las operaciones de lista, búsqueda y filtro
se ejecutarán sobre la colección local y deberán responder sin esperas perceptibles para un
volumen pequeño de incidencias.  
**Restricciones**: una sola instancia activa, sin red, sin múltiples usuarios, sin base de datos
externa, sin cambiar el framework ni convertir el proyecto a SDK.

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
│   └── requirements-traceability.md     # Trazabilidad RF-001 a RF-023
└── checklists/requirements.md            # Checklist de calidad de la especificación
```

### Árbol de código propuesto

```text
GestorIncidencias/
├── Entidades/
│   ├── Incidencia.cs                     # Entidad simple con propiedades get/set
│   ├── Prioridad.cs                      # Valores Baja, Media y Alta
│   └── EstadoIncidencia.cs               # Pendiente, EnProceso y Resuelta
├── Controlador/
│   ├── IncidenciaController.cs           # Casos de uso, edición, estados, consulta
│   ├── IncidenciaValidator.cs            # Validaciones compartidas de alta y edición
│   ├── IncidenciaRepository.cs           # Contrato de carga y guardado local
│   ├── XmlIncidenciaRepository.cs        # XML, temporal, respaldo y datos dañados
│   ├── ResultadoOperacion.cs             # Resultado y mensaje sin depender de WinForms
│   └── SingleInstanceGuard.cs            # Bloqueo adquirido y liberado por Program.Main
├── Formularios/
│   ├── frmIncidencias.cs                 # Eventos y coordinación con el controlador
│   ├── frmIncidencias.Designer.cs        # Controles, layout y eventos de UI
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
│   └── XmlIncidenciaRepositoryTests.cs   # Persistencia aislada y fallos de almacenamiento
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

- `DataGridView dgvIncidencias`: identificador, título, descripción, responsable, prioridad,
  estado y fecha de creación; selección de una incidencia.
- `TextBox txtTitulo`: título de alta y edición.
- `TextBox txtDescripcion`: descripción multilínea.
- `TextBox txtResponsable`: responsable.
- `ComboBox cboPrioridad`: Baja, Media, Alta; Media seleccionada inicialmente.
- `ComboBox cboFiltroEstado`: Todos, Pendiente, En proceso, Resuelta.
- `TextBox txtBuscar`: búsqueda mientras se escribe.
- `ComboBox cboNuevoEstado`: estado a solicitar.
- `Button btnNuevo`, `btnGuardar`, `btnEditar`, `btnCambiarEstado` y `btnCancelar`.
- `Label lblMensaje`: mensajes de validación, error, estado vacío y confirmación.
- `Label` auxiliares para que cada entrada tenga un texto identificable.

Eventos previstos:

- `Load`: usar el controlador ya preparado por `Program.Main`, cargar datos y refrescar la lista;
  no adquiere ni libera otro bloqueo.
- `txtBuscar.TextChanged`: volver a consultar aplicando búsqueda y filtro.
- `cboFiltroEstado.SelectedIndexChanged`: volver a consultar aplicando el filtro.
- `btnNuevo.Click`: limpiar el modo de edición y aplicar valores iniciales.
- `btnGuardar.Click`: construir la solicitud, invocar registro o edición y refrescar solo si
  el controlador devuelve éxito.
- `btnEditar.Click`: cargar los datos seleccionados en los controles editables.
- `btnCambiarEstado.Click`: solicitar una transición independiente y refrescar su resultado.
- `btnCancelar.Click`: descartar valores no confirmados.
- `FormClosing`: liberar recursos de instancia y cerrar sin guardar cambios no confirmados.

Los eventos no decidirán longitudes, estados permitidos, orden, identificadores ni formato de
persistencia. Solo traducirán valores de controles a solicitudes del controlador y mostrarán el
resultado en español.

## Lógica de negocio

### Validación

`IncidenciaValidator` recortará título, descripción y responsable antes de validar. Rechazará
valores vacíos, límites inferiores/superiores y prioridades desconocidas. Devolverá errores por
campo sin tocar la incidencia anterior. Las mismas reglas se usarán en alta y edición.

### Identificadores y registro

`IncidenciaController` mantendrá el siguiente identificador positivo a partir del máximo cargado
más uno. Si no hay datos, comenzará en uno. Al guardar una alta deberá persistir también el
identificador asignado; nunca se reutilizará un identificador confirmado. Los títulos no se
compararán para unicidad.

### Edición y estados

La edición copiará únicamente título, descripción, responsable y prioridad después de validar.
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

Verificación manual adicional:

- Abrir `frmIncidencias` en el diseñador de Visual Studio 2026 y confirmar que se cargan todos
  los controles sin excepción.
- Confirmar que los controles están definidos en `frmIncidencias.Designer.cs`, no creados
  exclusivamente en tiempo de ejecución.
- Ejecutar la aplicación desde Visual Studio y revisar alta, edición, estado, filtros y errores.
- Confirmar que el formulario principal sigue siendo `frmIncidencias`.
- Revisar que la interfaz no contiene reglas de negocio duplicadas.

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
- El plan incluye especificación, aclaraciones, plan técnico, tareas posteriores, TDD,
  verificación y Pull Request a `master`.
- No se crean código, pruebas ni workflow durante esta fase.

**Resultado**: plan técnico listo para generar tareas con `/speckit-tasks`.

## Seguimiento de complejidad

No se solicita una excepción constitucional. El proyecto de pruebas es un segundo proyecto
justificado por el principio de pruebas automatizadas y por la separación de reglas respecto de
la interfaz. No se añade base de datos, capa web ni infraestructura de red.
