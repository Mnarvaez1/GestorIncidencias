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
| Responsable | Texto | Sí | Recortado; longitud entre 2 y 80 caracteres. |
| Prioridad | Enumeración | Sí | Baja, Media o Alta; valor inicial Media. |
| Estado | Enumeración | Sí | Pendiente, En proceso o Resuelta; valor inicial Pendiente. |
| Fecha de creación | Fecha y hora | Sí | Asignada al registrar; inmutable al editar. |
| ResponsableEmpleadoId | Entero positivo nullable | No (temporal de integración) | Referencia opcional a `Empleado.Identificador`; solo si el responsable proviene del catálogo de técnicos activos. |

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

## Reglas de relación Incidencia-Empleado

- `ReportadoPor` es independiente de `Empleado` y del técnico asignado.
- Al registrar/editar incidencias nuevas, el responsable seleccionable debe ser un empleado con `EsTecnicoSoporte=true` y `Activo=true`.
- Si la incidencia ya tiene responsable textual legacy sin vínculo (`ResponsableEmpleadoId=null`), el sistema conserva ese valor histórico.
- Si un empleado técnico pasa a inactivo, las incidencias históricas mantienen su responsable textual existente.

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

La colección de empleados se proyecta con cuatro campos: identificador, nombre completo,
indicador técnico (Sí/No) y estado (Activo/Inactivo).

El orden recomendado para administración es nombre ascendente y, en empate, identificador ascendente.
La búsqueda aplica coincidencia parcial sin distinguir mayúsculas/minúsculas sobre nombre completo.

## Vista previa de reporte

La vista previa usa la misma proyección de consulta activa (búsqueda + filtro) e incluye:

- Encabezado empresarial.
- Fecha y hora de generación.
- Tabla con identificador, título, `ReportadoPor`, responsable, prioridad, estado y fecha.
- Estado vacío comprensible si no hay incidencias.

La exportación a HTML debe usar UTF-8 y escapar el contenido textual para evitar ruptura de formato.
