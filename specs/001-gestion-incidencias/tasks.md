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

- [ ] T021 [US2] Añadir a `IncidenciaControllerTests.cs` pruebas de proyección, lista vacía,
  orden por fecha descendente y desempate por identificador descendente para RF-009, RF-010 y
  RF-023.
- [ ] T022 [US2] Implementar en `IncidenciaController.cs` la consulta ordenada y una respuesta
  diferenciada para colección vacía, sin introducir dependencias de la UI.
- [ ] T023 [US2] Revisar la trazabilidad de RF-009, RF-010 y RF-023 en
  `contracts/requirements-traceability.md` y conservar la salida de pruebas correspondiente.

## Fase 5: Historia 3 — Modificar una incidencia (P1)

**Objetivo**: editar únicamente título, descripción, responsable y prioridad manteniendo las
propiedades inmutables.

- [ ] T024 [US3] Añadir pruebas de edición válida, edición inválida, cancelación lógica y
  conservación de identificador, fecha y estado en `IncidenciaControllerTests.cs`.
- [ ] T025 [US3] Implementar el método de edición de `IncidenciaController.cs` reutilizando el
  validador y aplicando cambios solo después de validar; cubrir RF-011, RF-012 y RF-022.
- [ ] T026 [US3] Ejecutar pruebas de regresión de registro y edición, comprobar que no se altera
  el objeto ante errores y guardar el TRX de la historia.

## Fase 6: Historia 4 — Cambiar el estado (P1)

**Objetivo**: aplicar exclusivamente las transiciones permitidas y rechazar las demás.

- [ ] T027 [US4] Añadir a `IncidenciaControllerTests.cs` pruebas para Pendiente → En proceso,
  En proceso → Resuelta, Resuelta → En proceso, estado actual y combinaciones rechazadas.
- [ ] T028 [US4] Implementar en `IncidenciaController.cs` la tabla de transiciones, la operación
  idempotente del estado actual y el error de negocio claro; cubrir RF-013 a RF-015.
- [ ] T029 [US4] Ejecutar la matriz completa de estados y verificar que ninguna transición
  rechazada muta la incidencia ni confirma un guardado.

## Fase 7: Historia 5 — Buscar y filtrar (P1)

**Objetivo**: consultar coincidencias parciales por título/responsable y combinarlas con el filtro.

- [ ] T030 [US5] Añadir pruebas de búsqueda parcial, mayúsculas/minúsculas, búsqueda vacía,
  cada filtro, combinación de criterios y resultado vacío en `IncidenciaControllerTests.cs`.
- [ ] T031 [US5] Implementar búsqueda y filtro en `IncidenciaController.cs`, incluyendo el orden
  establecido y mensajes de ausencia de resultados; cubrir RF-016 a RF-018 y RF-023.
- [ ] T032 [US5] Ejecutar pruebas de consulta junto con registro, edición y estados, y conservar
  el resultado TRX.

## Fase 8: Historia 6 — Persistencia y recuperación (P1)

**Objetivo**: conservar datos confirmados y proteger los datos anteriores ante fallos o corrupción.

- [ ] T033 [P] [US6] Crear `Controlador/XmlIncidenciaRepository.cs` con el formato XML versionado,
  ruta configurable para pruebas y directorios de usuario para producción.
- [ ] T034 [US6] Implementar carga de archivo ausente como colección vacía y cálculo del siguiente
  identificador positivo, cubriendo RF-007, RF-008 y RF-019.
- [ ] T035 [P] [US6] Añadir `XmlIncidenciaRepositoryTests.cs` con directorios temporales únicos
  por prueba para carga, guardado, cierre/reapertura y recuperación de identificadores.
- [ ] T036 [US6] Implementar guardado temporal, validación del temporal, respaldo y reemplazo
  atómico; actualizar memoria solo tras éxito y devolver fallo sin confirmación falsa, cubriendo
  RF-019 y RF-020.
- [ ] T037 [US6] Añadir pruebas de XML dañado, tipos inválidos, identificadores duplicados,
  prioridades/estados no válidos y fallo de escritura; comprobar preservación y bloqueo de
  sobrescritura automática según RF-021.
- [ ] T038 [US6] Integrar el repositorio con `IncidenciaController.cs` y ejecutar todas las
  pruebas de persistencia usando exclusivamente datos temporales.

## Fase 9: Historia 7 — Formulario y experiencia de operador (P1)

**Objetivo**: exponer los casos de uso sin duplicar reglas y conservar el diseñador.

- [ ] T039 [US1] Modificar `frmIncidencias.Designer.cs` para declarar y disponer `DataGridView`,
  cajas de texto, combos, botones, etiquetas y layout; comprobar que los controles se muestran
  en el diseñador de Visual Studio 2026.
- [ ] T040 [US1] Mantener `Formularios/frmIncidencias.cs` o la ruta actual compatible con el
  `.csproj`, conectar eventos de alta, edición y cancelación con el controlador y conservar
  `frmIncidencias` como formulario iniciado por `Program.cs`.
- [ ] T041 [US2] Conectar la lista a `dgvIncidencias`, mostrar identificador, título, descripción,
  responsable, prioridad, estado y fecha, y comunicar el estado vacío.
- [ ] T042 [US4] Conectar el cambio de estado independiente y mostrar errores de transición sin
  mutar los controles ni anunciar éxito falso.
- [ ] T043 [US5] Conectar `txtBuscar.TextChanged` y `cboFiltroEstado.SelectedIndexChanged` para
  refrescar búsqueda/filtro mientras se escribe, sin mover reglas de negocio a los eventos.
- [ ] T044 [US6] En `frmIncidencias.Load`, usar el controlador y bloqueo ya preparados por
  `Program.Main`, cargar datos y mostrar errores de carga; no adquirir un segundo mutex.
- [ ] T045 [US1]–[US7] Revisar mensajes en español para validaciones, errores de almacenamiento,
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

## Estrategia de implementación

1. **Preparación**: revisar y guardar documentación antes de código.
2. **Fundamentos**: crear entidades, contratos, proyecto MSTest y bloqueo de instancia.
3. **TDD inicial**: demostrar rojo real, verde y refactorización con TRX.
4. **MVP**: completar registro y validación; detenerse para validación independiente.
5. **Incremento funcional**: añadir lista, edición, estados y búsqueda/filtros.
6. **Persistencia segura**: completar XML, fallos, corrupción y recuperación.
7. **UI**: añadir controles al diseñador y conectar eventos del formulario sin duplicar reglas.
8. **CI y cierre**: ejecutar trazabilidad, revisión manual, workflow, artefactos y Pull Request.

## Criterio de parada de esta fase

Este archivo define tareas pendientes y sus dependencias. No crea código, proyectos de pruebas,
paquetes instalados ni workflows. La siguiente acción permitida es iniciar la implementación del
primer bloque, comenzando por T001 y T002.
