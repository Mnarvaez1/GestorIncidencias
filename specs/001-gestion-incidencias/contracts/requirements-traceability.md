# Matriz de trazabilidad

La implementación relaciona cada requisito RF-001 a RF-023 con pruebas automatizadas o verificaciones manuales.

| Requisitos | Verificación prevista |
|---|---|
| RF-001–RF-008 | Pruebas unitarias de registro, validación, identidad y valores iniciales. |
| RF-009 | `ConsultarRecuperaLosSieteCamposDeLaIncidencia` (MSTest) valida proyección de los siete campos. Resultado: correcto en `T022-verde.trx`. |
| RF-010 | `ConsultarOrdenaPorFechaDeCreacionDescendente` y `ConsultarConFechasIgualesDesempataPorIdDescendente` (MSTest) validan orden y desempate. Resultado: correcto en `T022-verde.trx`. |
| RF-011–RF-012 | Pruebas unitarias de edición e invariantes. |
| RF-013–RF-015 | Pruebas unitarias de transición permitida, idempotencia y rechazo. |
| RF-016–RF-018 | Pruebas unitarias de búsqueda, sensibilidad de mayúsculas y combinación de filtros. |
| RF-019–RF-021 | Pruebas de persistencia con directorios temporales y simulación de errores/daños. |
| RF-022 | Pruebas unitarias de validación en `IncidenciaValidatorTests` (límites, recorte, prioridad). Resultado: correcto en `T022-verde.trx`. |
| RF-023 | `ConsultarSinRegistrosDevuelveColeccionVacia` (MSTest) valida estado vacío sin error técnico. Resultado: correcto en `T022-verde.trx`. |

## Evidencia del bloque de consulta (US2)

- Fase roja: `T021-rojo.trx` (32 pruebas, 29 correctas, 3 fallidas de consulta antes de T022).
- Fase verde: `T022-verde.trx` (32 pruebas, 32 correctas).
