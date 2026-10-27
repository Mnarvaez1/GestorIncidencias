# Modelo de datos: Gestión de incidencias

## Estado

Modelo ampliado para `ReportadoPor`, compatibilidad XML legacy y vista previa de reporte.

## Entidad Incidencia

| Campo | Tipo conceptual | Obligatorio | Reglas |
|---|---|---:|---|
| Identificador | Entero positivo | Sí | Único y monotónico; no se reutiliza después de reiniciar. |
| Título | Texto | Sí | Recortado; longitud entre 5 y 100 caracteres. Puede repetirse. |
| Descripción | Texto | Sí | Recortada; longitud entre 10 y 1000 caracteres. |
| ReportadoPor | Texto | Sí (en registros nuevos) | Recortado; longitud entre 2 y 80 caracteres. |
| Responsable | Texto | Sí | Recortado; longitud entre 2 y 80 caracteres. Para incidencias sin vínculo técnico se usará la representación textual `Sin asignar`. |
| Prioridad | Enumeración | Sí | Baja, Media o Alta; valor inicial Media. |
| Estado | Enumeración | Sí | Pendiente, En proceso o Resuelta; valor inicial Pendiente. |
| Fecha de creación | Fecha y hora | Sí | Asignada al registrar; inmutable al editar. |
| ResponsableEmpleadoId | Entero positivo nullable | No (temporal de integración) | Referencia opcional a `Empleado.Identificador`; `null` representa incidencia sin técnico asignado. Si tiene valor, debe corresponder a técnico activo elegible y libre al asignar/reasignar. |

## Compatibilidad de datos legacy

- Un XML antiguo puede no incluir el campo `ReportadoPor`.
- En carga de registros legacy, el sistema conservará íntegros los siete campos existentes.
- Para UI y reportes, ausencia de `ReportadoPor` se representará como: `No informado (registro legado)`.
- Esta representación no sustituye ni altera los datos históricos originales; solo define interpretación visible.
- Un XML antiguo de incidencias puede contener únicamente `Responsable` textual sin vínculo a empleado; el sistema lo mantiene sin conversión forzada.

## Entidad Empleado

| Campo | Tipo conceptual | Obligatorio | Reglas |
|---|---|---:|---|
| Identificador | Entero positivo | Sí | Único, estable y monotónico; no se reutiliza. |
| NombreCompleto | Texto | Sí | Recortado; longitud entre 2 y 120 caracteres. |
| EsTecnicoSoporte | Booleano | Sí | `true` identifica elegibilidad funcional como técnico. |
| Activo | Booleano | Sí | `true` activo, `false` inactivo; no implica eliminación física. |
| FechaCreacion | Fecha y hora | Sí | Asignada al registrar empleado; inmutable en edición. |

## Proyección operativa de disponibilidad (derivada)

| Campo derivado | Tipo conceptual | Persistencia | Reglas |
|---|---|---:|---|
| DisponibilidadOperativa | Enumeración (`Libre`/`Ocupado`/`No calculada`) | No | Se calcula desde incidencias reales vinculadas por `ResponsableEmpleadoId`; no se guarda como bandera fija. |

## Reglas de relación Incidencia-Empleado

- `ReportadoPor` es independiente de `Empleado` y del técnico asignado.
- Al registrar/editar incidencias nuevas, el responsable seleccionable debe ser un empleado con `EsTecnicoSoporte=true` y `Activo=true`.
- Si la incidencia ya tiene responsable textual legacy sin vínculo (`ResponsableEmpleadoId=null`), el sistema conserva ese valor histórico.
- Si un empleado técnico pasa a inactivo, las incidencias históricas mantienen su responsable textual existente.
- Una incidencia nueva puede registrarse sin técnico (`ResponsableEmpleadoId=null`) conservando `Responsable="Sin asignar"` como valor de presentación coherente.
- Una incidencia se considera abierta para disponibilidad cuando su estado sea `Pendiente` o `En proceso`; `Resuelta` no cuenta como carga activa.
- La disponibilidad operativa solo se calcula para técnicos activos (`EsTecnicoSoporte=true` y `Activo=true`) con vínculo por `ResponsableEmpleadoId`.
- No se permite inferir vínculos automáticos desde `Responsable` textual legacy por comparación de nombres.

## Valores controlados

- **Prioridad**: `Baja`, `Media`, `Alta`.
- **Estado**: `Pendiente`, `En proceso`, `Resuelta`.
- **Filtro**: `Todos`, `Pendiente`, `En proceso`, `Resuelta`.

## Reglas de identidad y edición

- La creación asigna identificador, fecha y estado; el operador no los introduce.
- La edición cambia título, descripción, `ReportadoPor`, responsable y prioridad.
- Una edición inválida no modifica el objeto existente.
- Un cambio de estado es independiente de la edición.
- La edición de empleados cambia `NombreCompleto`, `EsTecnicoSoporte` y `Activo`, conservando `Identificador` y `FechaCreacion`.

## Transiciones de estado

| Estado actual | Estado solicitado | Resultado |
|---|---|---|
| Pendiente | En proceso | Permitido |
| En proceso | Resuelta | Permitido |
| Resuelta | En proceso | Permitido |
| Cualquiera | Igual al actual | Sin cambios |
| Cualquier otra combinación | Cualquiera | Rechazado con error de negocio |

## Vista de consulta

La colección se proyecta con ocho campos: identificador, título, descripción, `ReportadoPor`,
responsable, prioridad, estado y fecha de creación.

El orden es fecha de creación descendente y, ante igualdad, identificador descendente.
La búsqueda aplica coincidencia parcial sin distinguir mayúsculas y minúsculas sobre
título o responsable. El filtro de estado se aplica conjuntamente con la búsqueda.

## Vista de consulta de empleados

La colección de empleados se proyecta con cinco campos: identificador, nombre completo,
indicador técnico (Sí/No), estado laboral (Activo/Inactivo) y disponibilidad operativa.

Reglas de proyección de disponibilidad:

- `No calculada` cuando aún no exista integración efectiva con incidencias vinculadas o el empleado no sea técnico activo.
- `Libre` cuando técnico activo no tenga incidencias abiertas vinculadas por `ResponsableEmpleadoId`.
- `Ocupado` cuando técnico activo tenga al menos una incidencia abierta vinculada por `ResponsableEmpleadoId`.

El orden recomendado para administración es nombre ascendente y, en empate, identificador ascendente.
La búsqueda aplica coincidencia parcial sin distinguir mayúsculas/minúsculas sobre nombre completo.

## Vista previa de reporte

La vista previa usa la misma proyección de consulta activa (búsqueda + filtro) e incluye:

- Encabezado empresarial.
- Fecha y hora de generación.
- Tabla con identificador, título, `ReportadoPor`, responsable, prioridad, estado y fecha.
- Estado vacío comprensible si no hay incidencias.

La exportación a HTML debe usar UTF-8 y escapar el contenido textual para evitar ruptura de formato.
