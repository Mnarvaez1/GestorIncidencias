# Matriz de trazabilidad

La implementación relaciona cada requisito RF-001 a RF-023 con pruebas automatizadas o verificaciones manuales.

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
