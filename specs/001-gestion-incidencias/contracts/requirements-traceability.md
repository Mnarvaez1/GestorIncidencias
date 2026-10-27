# Matriz de trazabilidad

La implementación relaciona cada requisito RF-001 en adelante con pruebas automatizadas o verificaciones manuales.

| Requisitos | Verificación prevista |
|---|---|
| RF-001–RF-008 | Pruebas unitarias de registro, validación, identidad y valores iniciales. |
| RF-009 | `ConsultarRecuperaLosSieteCamposDeLaIncidencia` (MSTest) valida proyección de los siete campos. Resultado: correcto en `T022-verde.trx`. |
| RF-010 | `ConsultarOrdenaPorFechaDeCreacionDescendente` y `ConsultarConFechasIgualesDesempataPorIdDescendente` (MSTest) validan orden y desempate. Resultado: correcto en `T022-verde.trx`. |
| RF-011 | `EditarActualizaCamposEditablesYConservaInvariantes` y `EditarConDatosInvalidosRechazaYConservaValoresOriginales` (MSTest) validan edición de campos editables y rechazo de datos inválidos. Resultado: correcto en `T026-verde.trx`. |
| RF-012 | `EditarActualizaCamposEditablesYConservaInvariantes` y `EditarMantieneIntactasLasDemasIncidenciasYSinRegistrosExtra` (MSTest) validan conservación de ID, fecha y estado. Resultado: correcto en `T026-verde.trx`. |
| RF-013 | `CambiarEstadoRespetaTransicionesYConservaDatos` (MSTest) valida transiciones permitidas Pendiente→EnProceso, EnProceso→Resuelta y Resuelta→EnProceso. Resultado: correcto en `T029-verde.trx`. |
| RF-014 | `CambiarEstadoRespetaTransicionesYConservaDatos` (MSTest) valida operación idempotente al solicitar el mismo estado. Resultado: correcto en `T029-verde.trx`. |
| RF-015 | `CambiarEstadoRespetaTransicionesYConservaDatos`, `CambiarEstadoConIdInexistenteRechazaYNoAlteraDatos` y `CambiarEstadoConValorFueraDeEnumRechazaYNoAlteraDatos` (MSTest) validan rechazos sin mutación. Resultado: correcto en `T029-verde.trx`. |
| RF-016 | `BuscarFiltrarCoincidenciaParcialPorTituloOResponsable`, `BuscarFiltrarNoDistingueMayusculasYMinusculas` y `BuscarFiltrarCoincidenciaSoloEnDescripcionNoIncluyeRegistro` (MSTest) validan coincidencias parciales solo en título/responsable sin distinguir mayúsculas. Resultado: correcto en `T032-verde.trx`. |
| RF-017 | `BuscarFiltrarTextoVacioAplicaSoloFiltroDeEstado` y `BuscarFiltrarTodosEquivaleAAusenciaDeFiltroDeEstadoYConservaOrden` (MSTest) validan filtro por estado y opción Todos (`null`). Resultado: correcto en `T032-verde.trx`. |
| RF-018 | `BuscarFiltrarCombinaBusquedaYFiltroConAnd` y `BuscarFiltrarSinCoincidenciasDevuelveColeccionVacia` (MSTest) validan combinación AND y vacío sin coincidencias. Resultado: correcto en `T032-verde.trx`. |
| RF-019 | `CargarConArchivoAusenteDevuelveColeccionVacia`, `GuardarYReabrirRecuperaExactamenteLosSieteCampos` y `RegistrarDespuesDeReabrirRecuperaSiguienteIdYConservaRegistros` (MSTest) validan carga inicial, reapertura y continuidad de identificadores con directorios temporales. Resultado: correcto en `T035-verde-verificado.trx`. |
| RF-020 | Cobertura parcial: `GuardarReemplazaColeccionCompletaSinDuplicados` y `CargarDevuelveCopiasIndependientesDeListaEIncidencias` (MSTest) validan reemplazo completo sin acumulación y copias independientes en carga; `GuardarConArchivoBloqueadoDebeFallarYPreservarDatosAnteriores`, `GuardarConFalloControladoEnValidacionTemporalDebeConservarPrincipalYDevolverError` y `RegistrarConErrorRealDeGuardadoNoDebeReportarExitoNiPerderDatosPersistidos` (MSTest) validan fallo de guardado sin falso éxito y preservación de datos anteriores en integración con controlador. Permanece parcial hasta verificación funcional final en interfaz (Fase 9). Evidencia: `T038-verificacion-inicial.trx`. |
| RF-021 | `CargarConXmlDanadoDebeFallarYBloquearSobrescrituraAutomatica`, `CargarConTiposDeDatosInvalidosDebeFallar`, `CargarConIdentificadoresDuplicadosDebeFallarYPreservarArchivoOriginal`, `CargarConPrioridadInvalidaDebeFallar`, `CargarConEstadoInvalidoDebeFallar` y `GuardarConArchivoBloqueadoDebeFallarYPreservarDatosAnteriores` (MSTest) validan detección de corrupción, bloqueo de sobrescritura y preservación ante fallos de persistencia. Resultado: correcto en `T037-verificacion-inicial.trx`. |
| RF-022 | Pruebas unitarias de validación en `IncidenciaValidatorTests` (límites, recorte, prioridad). Resultado: correcto en `T022-verde.trx`. |
| RF-023 | `ConsultarSinRegistrosDevuelveColeccionVacia` (MSTest) valida estado vacío sin error técnico. Resultado: correcto en `T022-verde.trx`. |
| RF-039–RF-040 | Implementado en T095 con `EmpleadoController` + `EmpleadoControllerTests`: registro con identificador monotónico (`max(ID)+1`), validación de nombre con `EmpleadoValidator` y rechazo de entradas inválidas sin éxito falso. Verificación real: compilación correcta y MSTest 160/160 aprobadas. |
| RF-041–RF-042 | Implementado en T095/T096/T097: controlador con consulta/edición y formulario `frmEditarEmpleado` para alta/edición con validación visual coherente (nombre recortado y validado 2..120 vía `EmpleadoValidator`), modo nuevo y modo edición sin exponer Identificador/FechaCreacion ni persistencia directa desde el diálogo. Evidencia real reportada: compilación Visual Studio 0 errores, MSTest 160/160 aprobadas y apertura correcta en Diseñador. |
| RF-043–RF-044 | Implementado en T095 a nivel controlador: ausencia de eliminación física, inactivación/activación y búsqueda parcial por nombre sin distinguir mayúsculas/minúsculas. |
| RF-045–RF-046 | Implementado en T094 con `IEmpleadoRepository` + `XmlEmpleadoRepository` y verificado con `XmlEmpleadoRepositoryTests`: carga con archivo ausente, persistencia/recuperación completa de campos, XML corrupto, errores de guardado y preservación de datos previos sin falso éxito. Verificación real: compilación correcta y MSTest 140/140 aprobadas. |
| RF-047–RF-050 | Planificado para pruebas de integración en `IncidenciaControllerTests` y formularios de incidencia: selección exclusiva de técnicos activos, compatibilidad con responsable textual legacy e independencia de `ReportadoPor`. Estado: pendiente (T098–T099). |
| RF-051–RF-054 | Planificado: validación de separación estado laboral/disponibilidad operativa y cálculo de `Libre`/`Ocupado` desde incidencias abiertas vinculadas por `ResponsableEmpleadoId`. Estado: pendiente (T100–T102). |
| RF-055–RF-057 | Planificado: recálculo por transición a `Resuelta`, por cambio de técnico responsable y sin persistencia de bandera fija de disponibilidad. Estado: pendiente (T101–T102). |
| RF-058–RF-059 | Planificado: compatibilidad legacy sin inferencia por nombre y priorización visual de técnicos activos/libres con política de ocupados documentada. Estado: pendiente (T103–T104). |
| RF-060–RF-061 | Avance parcial: T096 implementa `frmEmpleados` con columna `Disponibilidad` y diseño XAMY aprobado; el botón `Administrar empleados` en `frmIncidencias` permanece pendiente para T098. |

## Evidencia del bloque de consulta (US2)

- Fase roja: `T021-rojo.trx` (32 pruebas, 29 correctas, 3 fallidas de consulta antes de T022).
- Fase verde: `T022-verde.trx` (32 pruebas, 32 correctas).

## Evidencia del bloque de edición (US3)

- Fase roja: `T024-rojo.trx` (36 pruebas, 34 correctas, 2 fallidas de edición antes de T025).
- Fase verde: `T026-verde.trx` (36 pruebas, 36 correctas).

## Evidencia del bloque de estados (US4)

- Fase roja: `T027-rojo.trx` (47 pruebas, 41 correctas, 6 fallidas en transiciones aún no implementadas).
- Fase verde: `T029-verde.trx` (47 pruebas, 47 correctas).

### Matriz de verificación de transiciones (3x3)

| Estado actual | Estado solicitado | Resultado esperado | Verificación |
|---|---|---|---|
| Pendiente | Pendiente | Sin cambios (idempotente) | `CambiarEstadoRespetaTransicionesYConservaDatos` ✅ |
| Pendiente | En proceso | Permitido | `CambiarEstadoRespetaTransicionesYConservaDatos` ✅ |
| Pendiente | Resuelta | Rechazado | `CambiarEstadoRespetaTransicionesYConservaDatos` ✅ |
| En proceso | Pendiente | Rechazado | `CambiarEstadoRespetaTransicionesYConservaDatos` ✅ |
| En proceso | En proceso | Sin cambios (idempotente) | `CambiarEstadoRespetaTransicionesYConservaDatos` ✅ |
| En proceso | Resuelta | Permitido | `CambiarEstadoRespetaTransicionesYConservaDatos` ✅ |
| Resuelta | Pendiente | Rechazado | `CambiarEstadoRespetaTransicionesYConservaDatos` ✅ |
| Resuelta | En proceso | Permitido | `CambiarEstadoRespetaTransicionesYConservaDatos` ✅ |
| Resuelta | Resuelta | Sin cambios (idempotente) | `CambiarEstadoRespetaTransicionesYConservaDatos` ✅ |

## Evidencia del bloque de búsqueda y filtrado (US5)

- Fase roja: `T030-rojo.trx` (55 pruebas, 49 correctas, 6 fallidas en búsqueda/filtro antes de T031).
- Fase verde: `T032-verde.trx` (55 pruebas, 55 correctas).

## Evidencia inicial del bloque de persistencia (US6)

- Fase roja: `T035-rojo.trx` (68 pruebas, 64 correctas, 4 fallidas en persistencia XML inicial).
- Verificación verde: `T035-verde-verificado.trx` (68 pruebas, 68 correctas).
- Alcance de esta verificación: cubre T033–T035; no constituye cobertura completa de T036–T038.
- Verificación específica T037: `T037-verificacion-inicial.trx` (74 pruebas, 74 correctas, incluyendo 6 pruebas de corrupción/fallo de persistencia).
- Verificación específica T036 (validación temporal): `T036-validacion-temporal.trx` (77 pruebas, 77 correctas; incluye fallo controlado durante validación del `.tmp` con preservación del archivo principal).

## Evidencia del bloque de interfaz base (US7 / T039)

- Verificación manual: `frmIncidencias` abre correctamente en Vista Diseño de Visual Studio 2026 y muestra controles del layout base empresarial.
- Verificación manual: ejecución con F5 abre el formulario principal sin errores de carga de diseñador.
- Verificación automatizada asociada: `T039-checkpoint-verificacion.trx` (79 pruebas, 79 correctas).
- Verificación específica T038 (integración controlador + repositorio XML con datos temporales): `T038-verificacion-inicial.trx` (79 pruebas, 79 correctas).

## Evidencia del bloque ReportadoPor (US1 / T057-T058)

- Fase roja T057: `T057-rojo-reportadopor.trx` (7 pruebas, 2 correctas, 5 fallidas por ausencia de validación de `ReportadoPor`) y `T057-rojo.trx` (86 pruebas, 81 correctas, 5 fallidas).
- Fase verde T058: `T058-verde-reportadopor.trx` (7 pruebas, 7 correctas) y `T058-verde.trx` (86 pruebas, 86 correctas).

## Evidencia del bloque de persistencia XML compatible (US6 / T059-T060)

- Fase roja T059: `T059-targeted.trx` (2 pruebas, 1 correcta, 1 fallida por ausencia de persistencia de `ReportadoPor`) y `T059-suite-completa.trx` (88 pruebas, 87 correctas, 1 fallida).
- Fase verde T060: `T060-targeted.trx` (2 pruebas, 2 correctas) y `T060-verde.trx` (88 pruebas, 88 correctas).
- Cobertura añadida RF-027 a RF-030 en `XmlIncidenciaRepositoryTests`: carga de XML legacy sin `ReportadoPor`, persistencia/recuperación del esquema ampliado y preservación de identificadores/datos sin migración silenciosa.

## Evidencia de regresión persistencia + controlador (US6 / T061)

- Regresión dirigida T061: `T061-regresion-persistencia-controlador.trx` (57 pruebas, 57 correctas) ejecutando `XmlIncidenciaRepositoryTests` e `IncidenciaControllerTests`.
- Suite completa T061: `T061-suite-completa.trx` (88 pruebas, 88 correctas).
- Resultado de control: la ampliación `ReportadoPor` no introduce regresiones en persistencia ni en comportamiento existente del controlador; RF-020 permanece en cobertura parcial hasta validación funcional de interfaz.

## Evidencia del bloque controlador con ReportadoPor (US1 / T062)

- Fase roja T062: `T062-targeted-rojo.trx` (11 pruebas, 1 correcta, 10 fallidas) y `T062-suite-completa-rojo.trx` (99 pruebas, 89 correctas, 10 fallidas).
- Fallos esperados: ausencia de sobrecargas `IncidenciaController.Registrar(titulo, descripcion, reportadoPor, responsable, prioridad)` y `IncidenciaController.Editar(id, titulo, descripcion, reportadoPor, responsable, prioridad)`, además de proyección específica de `ReportadoPor` pendiente de T063.
- Cobertura preservada: las 88 pruebas previas permanecen correctas en esta fase roja.

## Evidencia del bloque controlador con ReportadoPor (US1 / T063)

- Fase verde T063: `T063-targeted-verde.trx` (11 pruebas, 11 correctas) y `T063-suite-completa-verde.trx` (99 pruebas, 99 correctas).
- Alcance verificado: captura, validación, edición y proyección de `ReportadoPor` en `IncidenciaController`, conservando invariantes y compatibilidad con registros legacy.

## Cobertura pendiente de la ampliación aprobada (RF-024 a RF-038)

- RF-024 y RF-025: cobertura inicial completada en validador mediante T057–T058.
- RF-026: cobertura de controlador completada en T062–T063.
- RF-027 a RF-030: cobertura de persistencia XML completada en T059–T060 y regresión de integración de controlador verificada en T061–T063.
- RF-031 a RF-038: pendiente cubrir en formularios y reportes (T064–T070), sin declarar implementación aún.
- RF-020 se mantiene con cobertura parcial hasta completar la validación funcional final de interfaz.

## Evidencia de carga inicial en formulario principal (US6 / T044)

- Revisión técnica (sin cambios de código): `Program.Main` mantiene adquisición/liberación de `SingleInstanceGuard` y abre `frmIncidencias` una sola vez; `frmIncidencias.Load` ejecuta `RecargarGrilla()` y la consulta se realiza mediante `IncidenciaController.Consultar()`.
- Manejo de error de carga verificado en UI: ante `consulta.Exito=false`, `frmIncidencias` comunica `consulta.Mensaje` o el fallback en español `"No se pudieron cargar las incidencias."`, sin anunciar éxito falso.
- Verificación ejecutada en esta sesión: compilación correcta de la solución y ejecución de suite MSTest completa (99/99 superadas).

## Evidencia de mensajes de usuario (US1–US7 / T045)

- Revisión de código realizada en `frmIncidencias.cs`, `frmEditarIncidencia.cs`, `IncidenciaController.cs`, `IncidenciaValidator.cs` y `XmlIncidenciaRepository.cs` para validar mensajes en español, cancelación sin mutación y conservación de datos ante fallo.
- Cambios generados por IA en pruebas: `IncidenciaRepositoryDoble` permite simular fallo de carga controlado (`CargarDebeFallar`, `CargarMensajeError`) para verificar propagación de errores reales de lectura sin anunciar éxito falso.
- Nuevas pruebas MSTest agregadas en `IncidenciaControllerTests`:
  - `ConsultarSinRegistrosDevuelveMensajeClaroDeListaVacia`
  - `BuscarFiltrarSinCoincidenciasDevuelveMensajeClaro`
  - `ConsultarConFalloDeCargaDevuelveMensajeDeErrorDelRepositorio`
  - `RegistrarConFalloDeCargaDebeConservarMensajeDeErrorReal`
  - `EditarConFalloDeCargaDebeConservarMensajeDeErrorReal`
  - `CambiarEstadoConFalloDeCargaDebeConservarMensajeDeErrorReal`
- Cobertura automatizada relacionada preservada: validaciones de `IncidenciaValidatorTests`; errores de almacenamiento y conservación de datos en `IncidenciaControllerTests`; XML dañado/archivo bloqueado en `XmlIncidenciaRepositoryTests` (siempre con archivos temporales aislados).
- Evidencia de ejecución completa aportada por el operador en Visual Studio: suite MSTest 105 ejecutadas, 105 aprobadas, 0 fallidas, 0 omitidas, 0 advertencias.

## Evidencia de vista previa de reporte (US8 / T067)

- Implementación realizada en `frmVistaPreviaReporte.cs`, `frmVistaPreviaReporte.Designer.cs` y `frmVistaPreviaReporte.resx`, registrados en `GestorIncidencias.csproj` como formulario WinForms compatible con diseñador.
- Cobertura funcional implementada:
  - RF-032: encabezado empresarial y fecha/hora de generación en la vista previa.
  - RF-033: recepción de colección real vía constructor `frmVistaPreviaReporte(IList<Incidencia>)` sin inventar incidencias; se clona la entrada para visualización segura.
  - RF-034: tabla `dgvIncidenciasReporte` con columnas ID, título, `ReportadoPor`, responsable, prioridad, estado y fecha.
  - RF-035: estado vacío comprensible (`lblEstadoVacio`) cuando la colección no contiene incidencias.
- Regla legacy preservada: `ReportadoPor` ausente se representa solo de forma visual como `No informado (registro legado)` dentro del formulario de reporte, sin mutar datos fuente.
- Verificación auténtica aportada por el operador: apertura correcta en Ver diseñador, compilación correcta (2 proyectos, 0 errores) y regresión MSTest completa (105/105 aprobadas).
- Alcance explícitamente no incluido en T067: conexión del botón `btnVistaPreviaReporte` y exportación HTML (T065/T068 en adelante).

## Evidencia de exportación HTML (US8 / T068)

- Implementación realizada en `frmVistaPreviaReporte.cs` y `frmVistaPreviaReporte.Designer.cs` con botón `Exportar HTML` y flujo de guardado mediante `SaveFileDialog`.
- Cobertura funcional implementada:
  - RF-036: exportación a archivo `.html` en UTF-8 (`File.WriteAllText(..., Encoding.UTF8)`) usando exclusivamente la colección de incidencias recibida por el formulario.
  - RF-036: documento con encabezado empresarial, fecha/hora de generación, contador y tabla con ID, título, `ReportadoPor`, responsable, prioridad, estado y fecha.
  - RF-036: escape de contenido dinámico mediante `WebUtility.HtmlEncode` para `<`, `>`, `&`, comillas y caracteres especiales.
  - RF-037: sin uso de `PrintDocument`, `PrintPreviewDialog`, `PrintPreviewControl` ni `PrintDialog`.
- Seguridad y consistencia: `ReportadoPor` ausente se representa como `No informado (registro legado)` solo en salida visual/HTML, sin modificar datos originales en memoria.
- Manejo de flujo y errores en español: cancelación de diálogo y fallos de escritura informados al operador; éxito comunicado solo después de guardado confirmado.
- Verificación real en esta ejecución: compilación correcta de la solución (2 proyectos, 0 errores).

## Evidencia de pruebas automatizadas de reporte (US8 / T069)

- Pruebas añadidas en `GestorIncidencias.Tests/Controlador/VistaPreviaReporteHtmlTests.cs` y registradas en `GestorIncidencias.Tests.csproj`.
- Cobertura automatizada incorporada para reportes HTML:
  - Exportación con incidencias reales y conservación de identificadores/datos.
  - Uso exclusivo de la colección recibida (sin incluir incidencias ajenas al filtrado).
  - Escape HTML seguro de `<`, `>`, `&` y comillas.
  - Representación legacy de `ReportadoPor` como `No informado (registro legado)` sin mutar el dato fuente.
  - Caso de cero incidencias con estado vacío comprensible.
  - Escritura en UTF-8 con verificación de BOM y meta charset.
  - Manejo de error de ruta inválida sin falso éxito.
- Corrección documentada de expectativa de prueba: el contenido exportado se mantiene escapado (`WebUtility.HtmlEncode`), por lo que la validación de caracteres acentuados se ajustó usando `WebUtility.HtmlDecode` en la aserción de texto visible (ejemplo: `&#241;` -> `ñ`) sin debilitar controles de seguridad sobre HTML sin decodificar.
- Evidencia real aportada por el operador en Visual Studio: suite MSTest completa 111 ejecutadas, 111 aprobadas, 0 fallidas, 0 omitidas, 0 advertencias; compilación correcta.
- Alcance no incluido en T069: no se declara conexión del botón principal de vista previa (T065) ni cierre documental global de ampliación (T070).

## Evolución visual pendiente de validación manual (US8 / T071)

- Se define una evolución de presentación para `frmVistaPreviaReporte` (modelo ejecutivo negro y dorado) sin cambiar reglas de negocio ni exportación segura ya validada.
- Evidencia manual aportada por el estudiante: diseñador funcional en Visual Studio, ejecución F5 correcta, indicadores y tarjetas del informe ejecutivo con datos reales y contadores coherentes.

## Evolución visual compacta pendiente de validación manual (US8 / T071)

- Dentro de T071 se incorpora ajuste de compactación del informe ejecutivo para mejorar visibilidad integral del formulario y mantener acciones inferiores siempre accesibles.
- Este ajuste conserva paleta negro/dorado/crema y comportamiento funcional; la validación manual fue reportada por el estudiante con apertura correcta del formulario y controles visibles.
- Se reubica `Exportar HTML` al encabezado derecho del panel "Detalle de incidencias" junto al conteo y enlace de detalle técnico, manteniendo un único botón y el mismo evento existente.

## Evolución visual de identidad unificada pendiente de validación manual (US1 / US9 / T072-T074)

- Se aplica la identidad visual negro/dorado/crema de forma consistente a `frmIncidencias` y `frmEditarIncidencia` usando controles nativos de Windows Forms.
- En `frmIncidencias` se ajustan encabezado, panel de filtros, `DataGridView`, botones operativos y área de mensajes sin alterar búsqueda inmediata, filtro combinado, selección ni eventos existentes.
- En `frmEditarIncidencia` se ajustan encabezado con detalle dorado, superficie de formulario, etiquetas/campos y botones Guardar/Cancelar sin modificar validaciones, eventos ni flujo de alta/edición.
- Evidencia manual aportada por el estudiante: apertura en diseñador de formularios, ejecución F5 y verificación visual de la paleta corporativa aplicada sin regresión funcional.

## Evolución de usabilidad de salida visible pendiente de validación manual (US9 / T075-T077)

- Se incorpora `btnSalir` en `frmIncidencias` con estilo consistente negro/dorado/crema y ubicación en el panel de acciones.
- Se conecta confirmación en español: "¿Está seguro de que desea salir de XAMY Technologies?" con opciones Sí/No.
- Si el operador confirma Sí, el formulario principal se cierra por vía estándar (`Close`) para respetar el ciclo normal de finalización.
- Si el operador selecciona No, la operación se cancela sin cambios funcionales ni de datos.
- Evidencia manual aportada por el estudiante: compilación correcta, navegación operativa y validación funcional del botón Salir (confirmación y cierre) en Visual Studio.

## Evolución de identidad corporativa XAMY pendiente de validación manual (US8 / US9 / T078-T080)

- Se actualiza la marca visible en formularios principales y reporte ejecutivo a `XAMY TECHNOLOGIES`, manteniendo `GestorIncidencias` como nombre interno técnico del proyecto.
- En `frmIncidencias` se actualizan textos de encabezado, subtítulo institucional, título de ventana y confirmación de salida sin alterar flujos de operación.
- En `frmEditarIncidencia` se actualizan subtítulo corporativo y títulos de ventana por modo (nueva/modificar) sin cambiar campos, validaciones ni eventos.
- En `frmVistaPreviaReporte` se actualizan textos de marca en encabezado, leyenda inferior y título de ventana manteniendo diseño aprobado, indicadores dinámicos y tarjetas de incidencias.
- En la exportación HTML se actualizan únicamente textos de identidad corporativa visibles, preservando estructura, codificación UTF-8, escape seguro (`WebUtility.HtmlEncode`) y datos filtrados reales.
- Evidencia manual aportada por el estudiante: marca XAMY TECHNOLOGIES visible en formularios y reporte, reporte HTML abierto en Chrome con nueva identidad corporativa.

## Evolución de plantilla HTML ejecutiva pendiente de validación manual (US8 / T081-T083)

- Se moderniza la plantilla HTML exportada desde `frmVistaPreviaReporte` para reflejar identidad ejecutiva XAMY con paleta negro/dorado/crema y estructura responsiva local.
- El encabezado del HTML incluye marca corporativa, título `INFORME EJECUTIVO`, subtítulo operativo, separador dorado y fecha/hora real de generación.
- Se incorporan indicadores dinámicos (Total, Pendientes, En proceso, Resueltas) calculados sobre la colección exportada real.
- El detalle principal se presenta en tarjetas por incidencia con metadatos y estado; la tabla completa se conserva como detalle técnico secundario para mantener cobertura de campos requeridos.
- Se mantiene seguridad y compatibilidad existentes: codificación UTF-8, `WebUtility.HtmlEncode`, datos filtrados, representación legacy de `ReportadoPor`, estado vacío y flujo de exportación sin componentes de impresión.
- Evidencia manual aportada por el estudiante: exportación HTML validada en Chrome con filtro Todos y filtro Pendiente (ID 2 Cobertura), plantilla ejecutiva XAMY, compilación sin errores y regresión MSTest 111/111 aprobadas.

## Pendiente intencional de cierre documental global (US8 / US9 / T070)

- T070 permanece abierto por alcance explícito: requiere actualización adicional de `quickstart.md` y consolidación final de cobertura RF-024 a RF-038 en un cierre posterior.

## Evolución planificada módulo Empleados pendiente de implementación (US10 / T084-T099)

- Se documenta el inicio SDD del módulo Empleados en fases 1-3 (requerimientos, especificación y plan técnico) sin cambios de código en esta iteración.
- Se definen RF-039 a RF-050, entidad `Empleado`, contratos de negocio/persistencia y estrategia de compatibilidad con responsables legacy en texto.
- Se mantiene `ReportadoPor` como campo independiente del técnico asignado.
- La cobertura de estos RF queda en estado pendiente hasta ejecutar las tareas de implementación T092–T099 y su regresión MSTest asociada.

## Evidencia de implementación inicial de entidad Empleado (US10 / T092)

- Implementación realizada: `GestorIncidencias/Entidades/Empleado.cs` con campos `Identificador`, `NombreCompleto`, `EsTecnicoSoporte`, `Activo` y `FechaCreacion`.
- Pruebas añadidas: `GestorIncidencias.Tests/Entidades/EmpleadoTests.cs` con validación de asignación de constructor, estabilidad de `Identificador`/`FechaCreacion` y límites aceptados de `NombreCompleto` (2 y 120).
- Integración de proyecto confirmada: ambos archivos registrados en `GestorIncidencias.csproj` y `GestorIncidencias.Tests.csproj`.
- Verificación real aportada por el estudiante en Visual Studio: compilación correcta (0 errores) y regresión MSTest completa 115/115 aprobadas (incluye 4 ejecuciones nuevas asociadas a `EmpleadoTests`).
- Alcance explícito de T092: solo entidad y pruebas de entidad; no incluye aún validador, controlador, persistencia XML ni formularios de empleados.

## Evidencia de validación de empleados (US10 / T093)

- Implementación realizada: `GestorIncidencias/Controlador/EmpleadoValidator.cs`.
- Pruebas añadidas: `GestorIncidencias.Tests/Controlador/EmpleadoValidatorTests.cs`.
- Reglas verificadas por pruebas diseñadas para TDD: rechazo de empleado nulo, identificador positivo, `NombreCompleto` obligatorio, evaluación de longitud 2..120 después de `Trim()`, mensajes en español y validación sin mutar `Identificador`/`FechaCreacion` de la entidad evaluada.
- Integración de proyecto confirmada por el estudiante: `Controlador\EmpleadoValidator.cs` registrado en `GestorIncidencias.csproj` y `Controlador\EmpleadoValidatorTests.cs` registrado en `GestorIncidencias.Tests.csproj`.
- Verificación real aportada por el estudiante en Visual Studio: compilación sin errores y suite MSTest completa 128/128 aprobadas (baseline previo 115, incremento de 13 ejecuciones).
- Registro de advertencia: Test Explorer mostró una advertencia no identificada sin impacto en resultado (0 fallidas).
- Nota de TDD: en esta evidencia se registra diseño y ejecución de pruebas automatizadas; no se declara ni inventa evidencia separada de ciclos RED/GREEN más allá de la ejecución real confirmada.
- Alcance explícito de T093: no incluye repositorio/persistencia XML de empleados, `EmpleadoController` ni formularios (`frmEmpleados`, `frmEditarEmpleado`).

## Evidencia de persistencia XML de empleados (US10 / T094)

- Implementación realizada: `GestorIncidencias/Controlador/EmpleadoRepository.cs` y `GestorIncidencias/Controlador/XmlEmpleadoRepository.cs`.
- Pruebas añadidas: `GestorIncidencias.Tests/Controlador/XmlEmpleadoRepositoryTests.cs`.
- Cobertura automatizada de persistencia (rutas temporales aisladas): archivo inexistente, guardado/recuperación de uno y varios empleados, conservación de `Identificador` y `FechaCreacion`, conservación de `Activo` y `EsTecnicoSoporte`, rechazo de datos inválidos sin sobrescritura, XML corrupto, fallos de lectura/escritura controlados, preservación de archivo previo ante fallo de validación temporal y repetición de cargas/guardados sin pérdida.
- Independencia de almacenamiento: pruebas y repositorio usan `empleados.xml`, separado de `incidencias.xml`; no se alteran archivos de incidencias existentes.
- Integración de proyecto confirmada por el estudiante: `Controlador\EmpleadoRepository.cs` y `Controlador\XmlEmpleadoRepository.cs` registrados en `GestorIncidencias.csproj`; `Controlador\XmlEmpleadoRepositoryTests.cs` registrado en `GestorIncidencias.Tests.csproj`.
- Verificación real aportada por el estudiante en Visual Studio: compilación sin errores y suite MSTest completa 140/140 aprobadas, 0 fallidas, 0 advertencias (baseline previo 128; incremento de 12 ejecuciones).
- Diferenciación de evidencia: lo anterior corresponde a verificación automatizada ejecutada en pruebas; no se declara validación manual de formularios ni funcionalidades de T095–T099.
- Alcance explícito de T094: no incluye `EmpleadoController`, administración por formularios ni asignación de técnicos en incidencias.

## Evidencia de casos de uso de empleados en controlador (US10 / T095)

- Implementación realizada: `GestorIncidencias/Controlador/EmpleadoController.cs`.
- Pruebas añadidas: `GestorIncidencias.Tests/Controlador/EmpleadoControllerTests.cs` y doble auxiliar `GestorIncidencias.Tests/Controlador/EmpleadoRepositoryDoble.cs`.
- Operaciones cubiertas por pruebas automatizadas: registrar, consultar, buscar, editar, activar e inactivar empleados, con mensajes en español y propagación de errores reales de repositorio (`Cargar`/`Guardar`).
- Reglas verificadas: conservación de `Identificador` y `FechaCreacion` en edición/cambio de estado, no eliminación física al inactivar, bloqueo por `PuedeGuardar=false`, rechazo sin persistencia en validaciones fallidas y ausencia de éxito falso ante errores de guardado/carga.
- Consistencia de identificadores con persistencia XML: `EmpleadoController` calcula alta con estrategia monotónica `max(ID)+1`, compatible con `SiguienteIdentificador` de `XmlEmpleadoRepository` (también derivado de `max(ID)+1` en guardado). No se evidencia reutilización de IDs; riesgo residual concurrente no cubierto en el alcance actual monousuario.
- Integración de proyecto confirmada por el estudiante: `EmpleadoController.cs`, `EmpleadoControllerTests.cs` y `EmpleadoRepositoryDoble.cs` incluidos en los `.csproj` clásicos.
- Verificación real aportada por el estudiante en Visual Studio: compilación sin errores y suite MSTest completa 160/160 aprobadas (baseline previo 140; incremento de 20 ejecuciones).
- Registro de advertencia: Test Explorer mostró 1 advertencia no identificada sin impacto en el resultado (0 fallidas).
- Diferenciación de evidencia: se documenta ejecución automatizada real de pruebas; no se declara validación manual de formularios ni integración de T096–T099.

## Evolución planificada de disponibilidad operativa y acceso a empleados (US10 / T100-T104)

- Se incorpora a especificación la separación entre estado laboral (`Activo/Inactivo`) y disponibilidad operativa (`Libre/Ocupado/No calculada`).
- La disponibilidad operativa se define como cálculo derivado desde incidencias reales abiertas (`Pendiente`/`En proceso`) vinculadas por `ResponsableEmpleadoId`; no se persiste como estado fijo en `Empleado`.
- Se mantiene compatibilidad legacy: no se vinculan automáticamente responsables históricos por comparación textual de nombres.
- Se añade requisito de interfaz para `frmEmpleados` (columna `Disponibilidad`) y para `frmIncidencias` (botón corporativo `Administrar empleados`).
- Estado de implementación: pendiente, sin evidencia de ejecución aún en esta iteración documental.

## Evidencia de interfaz de administración de empleados (US10 / T096)

- Implementación realizada: `GestorIncidencias/frmEmpleados.cs`, `GestorIncidencias/frmEmpleados.Designer.cs` y `GestorIncidencias/frmEmpleados.resx`.
- Integración de proyecto confirmada por el estudiante: formulario registrado en `GestorIncidencias.csproj` clásico.
- Diseño validado por inspección y aprobación del estudiante: encabezado corporativo XAMY, paleta negro carbón/dorado/crema/blanco, barra de búsqueda alineada y panel de acciones con botones `Nuevo empleado`, `Modificar`, `Activar/Inactivar` y `Cerrar`.
- Configuración de `dgvEmpleados` validada para T096: cinco columnas visuales (`Identificador`, `Nombre completo`, `Técnico de soporte`, `Estado laboral`, `Disponibilidad`), sin mostrar `FechaCreacion` en grilla.
- Decisión funcional documentada: `FechaCreacion` se conserva en entidad/persistencia para trazabilidad, pero no se muestra en la tabla de administración de T096.
- Estado de disponibilidad en esta fase: sin cálculo automático implementado; debe representarse `No calculada` hasta integración real de T100–T104.
- Verificación real aportada por el estudiante en Visual Studio: compilación correcta (0 errores), suite MSTest 160/160 aprobadas (0 fallidas, 0 advertencias) y apertura previa del formulario en Ver diseñador.
- Diferenciación de evidencia: no se declara validación funcional completa de registro/edición ni cálculo automático de disponibilidad, aún pendientes en T097 y T100–T104.
