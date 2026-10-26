# Tareas de implementación: Gestión de incidencias

**Feature**: `001-gestion-incidencias`  
**Especificación**: [spec.md](spec.md)  
**Plan**: [plan.md](plan.md)  
**Estado**: Todas las tareas están pendientes.

## Convenciones

- `[P]` indica que la tarea puede ejecutarse en paralelo con otras tareas del mismo bloque y
  que no comparte archivos con ellas.
- `[USn]` relaciona la tarea con una historia de usuario de `spec.md`.
- Las tareas deben ejecutarse en orden de dependencia; no se generará código antes de completar
  la preparación y el primer bloque TDD.
- Cada tarea debe conservar compatibilidad con C# 7.3 y .NET Framework 4.7.2.

## Fase 1: Preparación y documentación

**Objetivo**: fijar la base documental antes de modificar código.

- [ ] T001 Revisar `specs/001-gestion-incidencias/spec.md`, `plan.md`, `research.md`,
  `data-model.md`, `quickstart.md` y `contracts/` contra la constitución y confirmar que
  CE-001 aparece una sola vez con “siete datos mostrables”. Depende de la documentación
  existente y bloquea T003.
- [ ] T002 Guardar en Git la documentación aprobada de `specs/001-gestion-incidencias/`,
  `.specify/memory/constitution.md`, `.specify/init-options.json`, `.specify/feature.json`,
  `.specify/integration.json`, `.specify/integrations/`, `.specify/scripts/`,
  `.specify/templates/`, `.specify/workflows/` y `.github/skills/` en un commit documental
  independiente, después de T001 y T003 y antes de crear o modificar código, proyectos de
  pruebas o workflows. Excluir `__pycache__`, binarios y datos locales.
- [ ] T003 Registrar en `specs/001-gestion-incidencias/quickstart.md` la evidencia de que el
  proyecto existente es clásico, usa C# 7.3/.NET Framework 4.7.2 y compila con MSBuild de
  Visual Studio 2026. Depende de T001 y debe completarse antes de T002.

## Fase 2: Fundamentos bloqueantes

**Objetivo**: preparar estructura, dependencias, contratos y arranque antes de las historias.
Todas las historias dependen de esta fase.

- [ ] T004 Crear `Entidades/Incidencia.cs` con propiedades simples `get/set` para identificador,
  título, descripción, responsable, prioridad, estado y fecha de creación, sin referencias a
  Windows Forms.
- [ ] T005 [P] Crear `Entidades/Prioridad.cs` y `Entidades/EstadoIncidencia.cs` con los valores
  cerrados definidos en `data-model.md`.
- [ ] T006 Crear `Controlador/ResultadoOperacion.cs` para comunicar éxito, errores y mensajes
  sin depender de `System.Windows.Forms`.
- [ ] T007 Crear `Controlador/IncidenciaRepository.cs` con el contrato de carga, guardado,
  estado de guardabilidad y errores de persistencia.
- [ ] T008 Crear `GestorIncidencias.Tests/GestorIncidencias.Tests.csproj` clásico dirigido a
  .NET Framework 4.7.2, añadir entradas `Compile`, `None` para `packages.config`,
  `ProjectReference` al proyecto de aplicación, referencias de framework, imports/targets de
  NuGet y registrarlo en `GestorIncidencias.slnx`. Depende de T004–T007.
- [ ] T009 Crear `GestorIncidencias.Tests/packages.config` con versiones exactas:
  `Microsoft.NET.Test.Sdk` 18.11.0, `MSTest.TestAdapter` 4.5.1 y
	`MSTest.TestFramework` 4.5.1; restaurar, comprobar grupos `.NETFramework4.6.2`, dependencias
  transitivas, imports generados y adaptador antes de usar las pruebas. Depende de T008.
- [ ] T054 Modificar `GestorIncidencias/GestorIncidencias.csproj` con entradas `Compile` para
  Entidades y Controlador, referencias explícitas de framework incluida `System.Runtime.Serialization`,
  y metadatos `SubType`, `DependentUpon`, `EmbeddedResource` y `LastGenOutput` del formulario.
  Depende de T004–T007 y debe completarse antes de T010.
- [ ] T010 Preparar el esqueleto compilable de `GestorIncidencias.Tests/Controlador/IncidenciaValidatorTests.cs`
  y las firmas mínimas de producción necesarias para ejecutar una primera prueba MSTest; no
  implementar todavía la regla de límites del título.
- [ ] T011 Ejecutar el primer rojo TDD con una prueba real de título de 4 y 5 caracteres,
  compilación correcta, descubrimiento de al menos una prueba, archivo TRX y fallo debido a la
  regla aún no implementada; guardar la evidencia en el Pull Request, sin aceptar errores de
  clases, métodos, referencias o adaptadores inexistentes.
- [ ] T012 Implementar el verde mínimo en `Controlador/IncidenciaValidator.cs` para recortar y
  validar el título, ejecutar la prueba y conservar el TRX con resultado correcto.
- [ ] T013 Refactorizar la validación del título sin cambiar comportamiento, ejecutar la suite
  completa, conservar el TRX final y adjuntar el diff revisado como evidencia del ciclo TDD.
- [ ] T015 Crear `Controlador/SingleInstanceGuard.cs` con el mutex nombrado, adquisición única,
  disposición segura y mensaje de instancia duplicada; `frmIncidencias.Load` no debe adquirir
  otro bloqueo. Depende de T001–T013 y se ejecuta después del primer bloque.
- [ ] T014 Modificar `Program.cs` después de T015 para adquirir el mutex mediante
  `SingleInstanceGuard` antes de crear `frmIncidencias` o cargar datos, mantenerlo durante toda
  la ejecución y liberarlo en `finally`; la segunda instancia debe finalizar sin crear el
  formulario. Depende de T015.

## Fase 3: Historia 1 — Registrar una incidencia (P1)

**Objetivo**: completar el alta válida, validación, valores iniciales e identificadores.

**Prueba independiente**: crear una incidencia válida y comprobar sus datos, identificador,
fecha y estado Pendiente sin abrir un formulario.

- [ ] T016 [P] [US1] Completar `GestorIncidencias.Tests/Controlador/IncidenciaValidatorTests.cs`
  con campos vacíos, espacios, límites inclusivos y límites inválidos de título, descripción y
  responsable; relacionar los casos con RF-002 a RF-006 y RF-022.
- [ ] T017 [US1] Implementar en `Controlador/IncidenciaValidator.cs` las reglas de recorte,
  longitudes, prioridad válida y mensajes de error por campo; conservar la entrada anterior
  cuando la validación falla.
- [ ] T019 [US1] Preparar y ejecutar pruebas en
  `GestorIncidencias.Tests/Controlador/IncidenciaControllerTests.cs` para títulos repetidos
  permitidos, identificadores positivos y no reutilización tras cargar datos, cubriendo RF-007
  y RF-008. Depende del esqueleto compilable de T010 y precede a T018; no debe compartir archivo
  en paralelo con otras pruebas.
- [ ] T018 [US1] Implementar en `Controlador/IncidenciaController.cs`, después de T019, el
  registro con prioridad Media por defecto, estado Pendiente, fecha de creación y siguiente
  identificador positivo; cubrir RF-001, RF-006 y RF-007 y ejecutar verde.
- [ ] T020 [US1] Ejecutar las pruebas de la historia, verificar el ciclo TDD en cada nueva regla,
  guardar TRX y documentar la evidencia roja, verde y de refactorización.

## Fase 4: Historia 2 — Consultar y ordenar (P1)

**Objetivo**: listar los siete campos de la entidad en el orden requerido y comunicar listas vacías.

- [x] T021 [US2] Añadir a `IncidenciaControllerTests.cs` pruebas de proyección, lista vacía,
  orden por fecha descendente y desempate por identificador descendente para RF-009, RF-010 y
  RF-023.
- [x] T022 [US2] Implementar en `IncidenciaController.cs` la consulta ordenada y una respuesta
  diferenciada para colección vacía, sin introducir dependencias de la UI.
- [x] T023 [US2] Revisar la trazabilidad de RF-009, RF-010 y RF-023 en
  `contracts/requirements-traceability.md` y conservar la salida de pruebas correspondiente.

## Fase 5: Historia 3 — Modificar una incidencia (P1)

**Objetivo**: editar únicamente título, descripción, responsable y prioridad manteniendo las
propiedades inmutables.

- [x] T024 [US3] Añadir pruebas de edición válida, edición inválida, cancelación lógica y
  conservación de identificador, fecha y estado en `IncidenciaControllerTests.cs`.
- [x] T025 [US3] Implementar el método de edición de `IncidenciaController.cs` reutilizando el
  validador y aplicando cambios solo después de validar; cubrir RF-011, RF-012 y RF-022.
- [x] T026 [US3] Ejecutar pruebas de regresión de registro y edición, comprobar que no se altera
  el objeto ante errores y guardar el TRX de la historia.

## Fase 6: Historia 4 — Cambiar el estado (P1)

**Objetivo**: aplicar exclusivamente las transiciones permitidas y rechazar las demás.

- [x] T027 [US4] Añadir a `IncidenciaControllerTests.cs` pruebas para Pendiente → En proceso,
  En proceso → Resuelta, Resuelta → En proceso, estado actual y combinaciones rechazadas.
- [x] T028 [US4] Implementar en `IncidenciaController.cs` la tabla de transiciones, la operación
  idempotente del estado actual y el error de negocio claro; cubrir RF-013 a RF-015.
- [x] T029 [US4] Ejecutar la matriz completa de estados y verificar que ninguna transición
  rechazada muta la incidencia ni confirma un guardado.

## Fase 7: Historia 5 — Buscar y filtrar (P1)

**Objetivo**: consultar coincidencias parciales por título/responsable y combinarlas con el filtro.

- [x] T030 [US5] Añadir pruebas de búsqueda parcial, mayúsculas/minúsculas, búsqueda vacía,
  cada filtro, combinación de criterios y resultado vacío en `IncidenciaControllerTests.cs`.
- [x] T031 [US5] Implementar búsqueda y filtro en `IncidenciaController.cs`, incluyendo el orden
  establecido y mensajes de ausencia de resultados; cubrir RF-016 a RF-018 y RF-023.
- [x] T032 [US5] Ejecutar pruebas de consulta junto con registro, edición y estados, y conservar
  el resultado TRX.

## Fase 8: Historia 6 — Persistencia y recuperación (P1)

**Objetivo**: conservar datos confirmados y proteger los datos anteriores ante fallos o corrupción.

- [x] T055 [US6] Alinear por TDD `IncidenciaRepositoryDoble` y `IncidenciaController.cs` con
  `contracts/repository-contract.md` antes de T033, verificando semántica de reemplazo completo,
  confirmación de guardado antes de éxito y bloqueo de sobrescritura cuando `PuedeGuardar=false`.

- [x] T033 [P] [US6] Crear `Controlador/XmlIncidenciaRepository.cs` con el formato XML versionado,
  ruta configurable para pruebas y directorios de usuario para producción.
- [x] T034 [US6] Implementar carga de archivo ausente como colección vacía y cálculo del siguiente
  identificador positivo, cubriendo RF-007, RF-008 y RF-019.
- [x] T035 [P] [US6] Añadir `XmlIncidenciaRepositoryTests.cs` con directorios temporales únicos
  por prueba para carga, guardado, cierre/reapertura y recuperación de identificadores.
- [x] T036 [US6] Implementar guardado temporal, validación del temporal, respaldo y reemplazo
  atómico; actualizar memoria solo tras éxito y devolver fallo sin confirmación falsa, cubriendo
  RF-019 y RF-020.
- [x] T037 [US6] Añadir pruebas de XML dañado, tipos inválidos, identificadores duplicados,
  prioridades/estados no válidos y fallo de escritura; comprobar preservación y bloqueo de
  sobrescritura automática según RF-021.
- [x] T038 [US6] Integrar el repositorio con `IncidenciaController.cs` y ejecutar todas las
  pruebas de persistencia usando exclusivamente datos temporales.

## Fase 9: Historia 7 — Formulario y experiencia de operador (P1)

**Objetivo**: exponer los casos de uso sin duplicar reglas y conservar el diseñador.

- [x] T039 [US1] Modificar `frmIncidencias.Designer.cs` para declarar y disponer `DataGridView`,
  cajas de texto, combos, botones, etiquetas y layout; comprobar que los controles se muestran
  en el diseñador de Visual Studio 2026.
- [x] T040 [US1] Mantener `Formularios/frmIncidencias.cs` o la ruta actual compatible con el
  `.csproj`, conectar eventos de alta, edición y cancelación con el controlador y conservar
  `frmIncidencias` como formulario iniciado por `Program.cs`.
- [x] T041 [US2] Conectar la lista a `dgvIncidencias`, mostrar identificador, título, descripción,
  responsable, prioridad, estado y fecha, y comunicar el estado vacío.
- [x] T042 [US4] Conectar el cambio de estado independiente y mostrar errores de transición sin
  mutar los controles ni anunciar éxito falso.
- [x] T043 [US5] Conectar `txtBuscar.TextChanged` y `cboFiltroEstado.SelectedIndexChanged` para
  refrescar búsqueda/filtro mientras se escribe, sin mover reglas de negocio a los eventos.
- [x] T044 [US6] En `frmIncidencias.Load`, usar el controlador y bloqueo ya preparados por
  `Program.Main`, cargar datos y mostrar errores de carga; no adquirir un segundo mutex.
- [x] T045 [US1]–[US7] Revisar mensajes en español para validaciones, errores de almacenamiento,
  archivo dañado, listas vacías y búsquedas sin resultados.

## Fase 10: Integración continua y verificación final

**Objetivo**: automatizar compilación, pruebas, evidencias y revisión final.

- [ ] T046 Crear `.github/workflows/ci.yml` para `windows-latest`, checkout, configuración de
  NuGet, restauración de `packages.config` y localización de MSBuild mediante las acciones
  oficiales.
- [ ] T047 Configurar en `.github/workflows/ci.yml` la compilación Release de
  `GestorIncidencias.slnx` y el fallo del job ante cualquier error de restauración o compilación.
- [ ] T048 Configurar la localización de `vstest.console.exe` mediante `vswhere.exe`, pasar
  `/TestAdapterPath` al adaptador MSTest 4.5.1, generar TRX y fallar si no se descubre ninguna
  prueba o si alguna falla.
- [ ] T049 Publicar los TRX y el contenido compilado `bin/Release` como artefactos de GitHub
  Actions, incluso cuando las pruebas fallen siempre que el job pueda producirlos.
- [ ] T050 Ejecutar la matriz RF-001 a RF-023, actualizar `contracts/requirements-traceability.md`
  con nombres reales de pruebas y registrar las verificaciones manuales restantes.
- [ ] T051 Revisar manualmente `frmIncidencias` en el diseñador y ejecutar el comportamiento de
  alta, lista, edición, estados, búsqueda, filtros, persistencia, errores y segunda instancia.
- [ ] T052 Ejecutar `quickstart.md`, comprobar que la solución y las pruebas se descubren, revisar
  todos los TRX y confirmar que no hay falsos positivos por suite vacía.
- [ ] T053 Crear el Pull Request hacia `master` con enlaces a `spec.md`, `plan.md`, `tasks.md`,
  evidencia TDD roja/verde/refactorizada, resultados de CI, revisión de diseñador y artefacto.

## Fase 11: Ampliación — Modelo y pruebas de `ReportadoPor` (P1)

**Objetivo**: incorporar el nuevo campo en el modelo y asegurar validaciones/criterios base por TDD.

- [x] T056 [US1] Actualizar `Entidades/Incidencia.cs` para incluir `ReportadoPor` como dato
  diferenciado de `Responsable`, manteniendo compatibilidad con C# 7.3/.NET Framework 4.7.2.
- [x] T057 [US1] Añadir pruebas en `GestorIncidencias.Tests/Controlador/IncidenciaValidatorTests.cs`
  para validar `ReportadoPor` con recorte y longitud 2..80, incluyendo vacío, solo espacios y
  límites inválidos (RF-024, RF-025).
- [x] T058 [US1] Implementar en `Controlador/IncidenciaValidator.cs` la validación de
  `ReportadoPor` reutilizable en alta y edición, con mensajes claros en español.

## Fase 12: Ampliación — Persistencia XML compatible (P1)

**Objetivo**: persistir `ReportadoPor` sin perder compatibilidad con XML legado de siete campos.

- [x] T059 [US6] Añadir pruebas en
  `GestorIncidencias.Tests/Controlador/XmlIncidenciaRepositoryTests.cs` para carga de XML
  antiguo sin `ReportadoPor`, guardado/recuperación en esquema ampliado y preservación de
  identificadores/datos (RF-027, RF-028, RF-029, RF-030).
- [x] T060 [US6] Ajustar `Controlador/XmlIncidenciaRepository.cs` para leer ambos esquemas
  (legacy y ampliado), conservar datos históricos y persistir `ReportadoPor` en nuevos guardados
  sin sobrescribir información válida previa.
- [x] T061 [US6] Ejecutar regresión de persistencia y controlador con TRX, verificando que
  RF-020 permanece en cobertura parcial hasta la validación funcional de interfaz.

## Fase 13: Ampliación — Controlador y casos de uso (P1)

**Objetivo**: extender registro/edición/consulta con `ReportadoPor` sin romper reglas actuales.

- [x] T062 [US1] Añadir pruebas en `GestorIncidencias.Tests/Controlador/IncidenciaControllerTests.cs`
  para registro, edición y consulta con `ReportadoPor`, incluyendo continuidad de identificadores
  y comportamiento con datos legacy.
- [x] T063 [US1] Implementar en `Controlador/IncidenciaController.cs` la captura, edición y
  proyección de `ReportadoPor`, manteniendo invariantes, orden y reglas vigentes (RF-024, RF-026,
  RF-030).

## Fase 14: Ampliación — Formularios principales (P2)

**Objetivo**: actualizar `frmIncidencias` sobre base T039 sin romper compatibilidad de diseñador.

- [x] T064 [US9] Modificar `frmIncidencias.Designer.cs` para agregar controles de `ReportadoPor`,
  compactar/alinear filtro de estado, asegurar visibilidad del área de mensajes e incorporar el
  botón `Vista previa de reporte` (RF-031, RF-038).
- [x] T065 [US1] [US9] Conectar en `frmIncidencias.cs` alta/edición con `ReportadoPor`, refresco
  de grilla con el nuevo campo y apertura de vista previa, sin mover reglas de negocio a eventos.
- [x] T066 [US9] Ejecutar validación manual en diseñador de Visual Studio 2026 y ejecución F5,
  documentando evidencias separadas de pruebas automatizadas.

## Fase 15: Ampliación — Vista previa y exportación de reportes (P2)

**Objetivo**: implementar `frmVistaPreviaReporte` y exportación HTML local sin dependencias de impresión.

- [x] T067 [US8] Crear `frmVistaPreviaReporte.Designer.cs`, `frmVistaPreviaReporte.cs` y
  `frmVistaPreviaReporte.resx` con encabezado empresarial, fecha/hora, tabla de incidencias y
  estado vacío comprensible (RF-032, RF-033, RF-034, RF-035).
- [x] T068 [US8] Implementar exportación a HTML UTF-8 con escape de contenido y datos reales
  filtrados, sin usar `PrintDocument`, `PrintPreviewDialog`, `PrintPreviewControl` ni
  `PrintDialog` (RF-036, RF-037).
- [x] T069 [US8] Añadir pruebas automatizadas para generación/exportación de reporte y casos de
  cero incidencias; conservar aislamiento de archivos temporales.
- [ ] T070 [US8] [US9] Actualizar `contracts/requirements-traceability.md` y `quickstart.md` con
  la cobertura RF-024 a RF-038 y evidencias manuales/automáticas de la ampliación.

## Fase 16: Evolución visual de reporte ejecutivo (P2)

**Objetivo**: modernizar la presentación de `frmVistaPreviaReporte` con el modelo ejecutivo negro y dorado sin alterar comportamiento funcional.

- [x] T071 [US8] Aplicar rediseño visual en `frmVistaPreviaReporte.Designer.cs` y ajustes mínimos en
  `frmVistaPreviaReporte.cs` para encabezado ejecutivo, indicadores (Total/Pendientes/En proceso/Resueltas),
	detalle optimizado con presentación inicial en tarjetas y vista técnica secundaria, además de pie con acciones,
  conservando datos reales filtrados, exportación existente,
  compatibilidad de diseñador y sin usar componentes de impresión.

## Fase 17: Evolución visual de identidad unificada (P2)

**Objetivo**: unificar la identidad visual negro/dorado/crema en formularios principales sin alterar comportamiento funcional.

- [x] T072 [US9] Aplicar en `frmIncidencias.Designer.cs` la paleta corporativa negro/dorado/crema,
  conservando estructura de búsqueda/filtros, `DataGridView`, botones y mensajes operativos existentes.
- [x] T073 [US1] [US9] Aplicar en `frmEditarIncidencia.Designer.cs` la paleta corporativa
  negro/dorado/crema con encabezado y acciones consistentes, preservando validaciones, eventos,
  campos y compatibilidad de diseñador.
- [x] T074 [US1] [US9] Documentar en `contracts/requirements-traceability.md` la evolución visual
  unificada y dejar pendiente su cierre hasta validación manual en diseñador/F5 por el estudiante.

## Fase 18: Evolución de usabilidad en formulario principal (P2)

**Objetivo**: mejorar la salida explícita del sistema desde `frmIncidencias` sin alterar reglas funcionales.

- [x] T075 [US9] Agregar en `frmIncidencias.Designer.cs` el botón `btnSalir` con estilo corporativo,
  ubicación en panel de acciones y distribución compatible con el resto de botones.
- [x] T076 [US9] Conectar en `frmIncidencias.cs` el evento de `btnSalir` con confirmación Sí/No en
  español y cierre estándar del formulario principal sin cierres forzados.
- [x] T077 [US9] Actualizar `contracts/requirements-traceability.md` con evidencia de alcance de la
  evolución de salida visible, dejando pendiente validación manual en Visual Studio.

## Fase 19: Evolución de identidad corporativa XAMY (P2)

**Objetivo**: personalizar la marca visible de la aplicación a XAMY TECHNOLOGIES sin alterar funcionamiento interno ni arquitectura.

- [x] T078 [US8] [US9] Actualizar textos visibles de marca en `frmIncidencias` y `frmEditarIncidencia`
  (encabezados, subtítulos y títulos de ventana) para reflejar `XAMY TECHNOLOGIES` con
  conservación de distribución y estilos aprobados.
- [x] T079 [US8] Actualizar textos visibles de marca en `frmVistaPreviaReporte` y en el HTML exportado
  para reflejar `XAMY TECHNOLOGIES`, preservando indicadores, tarjetas, codificación UTF-8,
  escape seguro y flujo funcional vigente.
- [x] T080 [US8] [US9] Documentar en `contracts/requirements-traceability.md` la evolución de
  identidad corporativa XAMY, dejando pendiente su cierre hasta validación manual en Designer,
  ejecución F5 y regresión de pruebas MSTest por el estudiante.

## Fase 20: Evolución de plantilla HTML ejecutiva (P2)

**Objetivo**: modernizar la plantilla del HTML exportado para reflejar el estilo ejecutivo XAMY sin alterar contratos de seguridad ni datos.

- [x] T081 [US8] Actualizar en `frmVistaPreviaReporte.cs` la plantilla HTML/CSS exportada a diseño
  ejecutivo negro/dorado/crema con encabezado corporativo, indicadores dinámicos y tarjetas de
  incidencias como presentación principal.
- [x] T082 [US8] Conservar en el HTML exportado la tabla de detalle técnico como sección secundaria,
  preservando cobertura funcional de campos completos, estado vacío y compatibilidad con filtros.
- [x] T083 [US8] [US9] Actualizar `contracts/requirements-traceability.md` con la evolución de
  plantilla HTML ejecutiva, dejando pendiente su validación manual y regresión MSTest por el estudiante.

## Fase 21: SDD Fase 1 — Requerimientos módulo Empleados

**Objetivo**: formalizar alcance funcional del módulo de empleados sin implementar código.

- [x] T084 Analizar y documentar en `spec.md` el alcance del módulo Empleados (alta, consulta,
  edición, técnico de soporte, estado Activo/Inactivo, búsqueda, integración con responsable,
  persistencia y exclusiones de nómina/salarios/asistencia) creando RF nuevos sin renumerar RF-001..RF-038.
- [x] T085 Documentar en `spec.md` las decisiones funcionales del módulo Empleados que requieren
  aprobación previa a codificación (compatibilidad de responsable textual legacy y estrategia de vínculo opcional).

## Fase 22: SDD Fase 2 — Especificación módulo Empleados

**Objetivo**: definir artefactos de diseño funcional y contratos verificables del módulo.

- [x] T086 Actualizar `data-model.md` con entidad `Empleado` (campos, tipos, validaciones,
  identificadores estables, estado Activo/Inactivo y relación con incidencia/responsable).
- [x] T087 Actualizar `contracts/business-rules.md` con reglas de negocio de empleados,
  selección de técnicos activos, independencia de `ReportadoPor` y casos de error.
- [x] T088 Actualizar `contracts/repository-contract.md` con contrato de persistencia XML segura
  para empleados, manejo de fallos y compatibilidad con incidencias legacy.
- [x] T089 Actualizar `contracts/requirements-traceability.md` incorporando RF del módulo
  Empleados con verificación prevista y estado pendiente hasta implementación.

## Fase 23: SDD Fase 3 — Plan técnico módulo Empleados

**Objetivo**: planificar implementación futura compatible con arquitectura y diseñador WinForms.

- [x] T090 Actualizar `plan.md` con la arquitectura propuesta del módulo Empleados (`Entidades/Empleado.cs`,
  validaciones, repositorio XML, controlador, formularios `frmEmpleados` y `frmEditarEmpleado`,
  integración con `frmIncidencias` y `frmEditarIncidencia`, y pruebas MSTest).
- [x] T091 Actualizar `tasks.md` con backlog técnico de implementación de Empleados (T092 en adelante),
  dependencias por orden de ejecución y criterios de verificación, sin cerrar tareas ni alterar T070.

## Fase 24: Implementación futura módulo Empleados (pendiente)

**Objetivo**: reservar la secuencia de trabajo para fases de codificación posteriores a aprobación.

- [x] T092 [US10] Crear `Entidades/Empleado.cs` y pruebas de entidad asociadas.
- [x] T093 [US10] Crear `Controlador/EmpleadoValidator.cs` y `EmpleadoValidatorTests.cs` con ciclo TDD.
- [x] T094 [US10] Crear contratos/repositorio de empleados (`EmpleadoRepository.cs`, `XmlEmpleadoRepository.cs`) y pruebas de persistencia XML segura.
- [x] T095 [US10] Crear `Controlador/EmpleadoController.cs` con pruebas de alta, consulta, edición, búsqueda y estado Activo/Inactivo.
- [ ] T096 [US10] Implementar `frmEmpleados.cs` y `frmEmpleados.Designer.cs` con controles nativos WinForms y paleta XAMY.
- [ ] T096 [US10] Implementar `frmEmpleados.cs` y `frmEmpleados.Designer.cs` con controles nativos WinForms y paleta XAMY, incluyendo columna `Disponibilidad` (Libre/Ocupado/No calculada) junto a Nombre, Rol y Estado laboral sin mostrar `Libre` ficticio antes de integración real.
- [ ] T097 [US10] Implementar `frmEditarEmpleado.cs` y `frmEditarEmpleado.Designer.cs` para alta/edición con validación visual coherente.
- [ ] T098 [US10] Integrar selección de técnicos activos y acceso al módulo `frmEmpleados` desde `frmIncidencias` (botón corporativo `Administrar empleados`), además de `frmEditarIncidencia`, preservando compatibilidad con responsables legacy en texto.
- [ ] T099 [US10] Ejecutar regresión MSTest completa y actualizar evidencia en `requirements-traceability.md` sin cerrar T070.

## Fase 25: Disponibilidad operativa automática de técnicos (pendiente)

**Objetivo**: incorporar cálculo dinámico de disponibilidad de técnicos basado en incidencias vinculadas sin persistir estado fijo.

- [ ] T100 [US10] Extender contratos/modelo operativo en código para distinguir estado laboral y disponibilidad derivada (`Libre`/`Ocupado`/`No calculada`) sin persistencia fija en `Empleado`.
- [ ] T101 [US10] Añadir pruebas MSTest de disponibilidad en controlador para incidencias abiertas (`Pendiente`/`En proceso`), cierre `Resuelta` y recálculo por reasignación de `ResponsableEmpleadoId`.
- [ ] T102 [US10] Implementar en la capa de casos de uso el recálculo de disponibilidad desde incidencias reales vinculadas por `ResponsableEmpleadoId`, sin inferencias por nombre legacy.
- [ ] T103 [US10] Integrar priorización visual de técnicos activos/libres en selector de responsable y documentar explícitamente la política aplicada cuando el técnico esté ocupado.
- [ ] T104 [US10] Actualizar pruebas de regresión e evidencia de trazabilidad para RF-051 a RF-061 manteniendo T070 pendiente.

## Dependencias y orden de ejecución

### Dependencias por fase

- **Fase 1**: no depende de código; debe completarse y guardarse en Git antes de la implementación.
- **Fase 2**: depende de Fase 1 y bloquea todas las historias.
- **Fase 3**: depende de T004–T013; es el primer bloque funcional de implementación después del
  esqueleto TDD y constituye el MVP de registro.
- **Fases 4–7**: dependen de los fundamentos y pueden avanzar de forma incremental; las fases
  4, 5, 6 y 7 comparten el controlador y se recomienda secuenciarlas para evitar conflictos.
- **Fase 8**: depende de la entidad, el controlador y los contratos; debe completarse antes de
  conectar el formulario a datos reales.
- **Fase 9**: depende de las reglas y persistencia de las fases 3–8.
- **Fase 10**: depende de la implementación completa y de la evidencia de pruebas.
- **Fase 11**: depende de la base actual completada hasta T039 y abre la ampliación `ReportadoPor`.
- **Fase 12**: depende de Fase 11 y de contratos de persistencia para compatibilidad XML legacy.
- **Fase 13**: depende de Fase 12 para integrar controlador con datos ya persistibles.
- **Fase 14**: depende de Fase 13 para conectar UI con reglas y datos ampliados.
- **Fase 15**: depende de Fase 14 para consumir búsqueda/filtro reales en vista previa y cierre documental.
- **Fase 21**: documental, inicia alcance del módulo Empleados y habilita fases 22 y 23.
- **Fase 22**: depende de Fase 21 y bloquea planificación detallada e implementación del módulo Empleados.
- **Fase 23**: depende de Fase 22 y produce el backlog ejecutable del módulo Empleados.
- **Fase 24**: depende de aprobación formal de fases 21-23 y habilita implementación posterior (fuera de este alcance).
- **Fase 25**: depende de Fase 24 (T095+), y de la integración de formularios T096–T098 para exponer disponibilidad operativa en UI y selector técnico.

### Primer bloque de implementación

El primer bloque será **Fase 1 + Fase 2 hasta T013**:

1. Guardar la documentación aprobada en Git.
2. Crear el esqueleto clásico de MSTest y dependencias fijadas.
3. Preparar y ejecutar el rojo real con prueba descubierta y TRX.
4. Completar verde y refactorización con evidencia.
5. Preparar las entidades y el bloqueo de instancia en `Program.Main`.

No se comenzará la interfaz ni la persistencia antes de cerrar este bloque.

### Oportunidades de paralelismo

- T001 y T003 pueden ejecutarse en paralelo; T002 depende de revisar ambos resultados.
- T004 y T005 pueden ejecutarse en paralelo; T006 y T007 pueden prepararse en paralelo después
  de las entidades.
- T016 y T019 son pruebas separables una vez disponible el esqueleto, pero su implementación
  depende del controlador y del validador.
- T021, T024, T027 y T030 pueden redactarse en paralelo si no se modifican los mismos métodos;
  su ejecución e integración se recomienda en el orden de las historias.
- T033 y T035 pueden prepararse en paralelo; T036 y T037 dependen del repositorio definido.
- T046, T047 y T048 pueden configurarse en pasos separados, pero deben integrarse antes de T049.
- T057 y T062 pueden redactarse en paralelo sobre archivos de pruebas distintos.
- T064 y T067 pueden prepararse en paralelo si no se modifica el mismo archivo de recursos.

## Estrategia de implementación

1. **Preparación**: revisar y guardar documentación antes de código.
2. **Fundamentos**: crear entidades, contratos, proyecto MSTest y bloqueo de instancia.
3. **TDD inicial**: demostrar rojo real, verde y refactorización con TRX.
4. **MVP**: completar registro y validación; detenerse para validación independiente.
5. **Incremento funcional**: añadir lista, edición, estados y búsqueda/filtros.
6. **Persistencia segura**: completar XML, fallos, corrupción y recuperación.
7. **UI**: añadir controles al diseñador y conectar eventos del formulario sin duplicar reglas.
8. **CI y cierre**: ejecutar trazabilidad, revisión manual, workflow, artefactos y Pull Request.

### Primer bloque recomendado de implementación para la ampliación

Después de cerrar la base previa, iniciar por **T056–T061**:

1. Modelo + validación de `ReportadoPor` (T056–T058).
2. Compatibilidad XML legacy y persistencia ampliada (T059–T060).
3. Regresión y evidencia TRX manteniendo RF-020 como cobertura parcial (T061).

Con ese bloque validado, continuar con controlador (T062–T063), luego formularios (T064–T066)
y finalmente reportes (T067–T070).

## Criterio de parada de esta fase

Este archivo define tareas pendientes y sus dependencias. No crea código, proyectos de pruebas,
paquetes instalados ni workflows. La siguiente acción permitida es iniciar la implementación del
primer bloque, comenzando por T001 y T002.
