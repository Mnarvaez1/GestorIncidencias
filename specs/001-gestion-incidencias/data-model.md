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

## Compatibilidad de datos legacy

- Un XML antiguo puede no incluir el campo `ReportadoPor`.
- En carga de registros legacy, el sistema conservará íntegros los siete campos existentes.
- Para UI y reportes, ausencia de `ReportadoPor` se representará como: `No informado (registro legado)`.
- Esta representación no sustituye ni altera los datos históricos originales; solo define interpretación visible.

## Valores controlados

- **Prioridad**: `Baja`, `Media`, `Alta`.
- **Estado**: `Pendiente`, `En proceso`, `Resuelta`.
- **Filtro**: `Todos`, `Pendiente`, `En proceso`, `Resuelta`.

## Reglas de identidad y edición

- La creación asigna identificador, fecha y estado; el operador no los introduce.
- La edición cambia título, descripción, `ReportadoPor`, responsable y prioridad.
- Una edición inválida no modifica el objeto existente.
- Un cambio de estado es independiente de la edición.

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

## Vista previa de reporte

La vista previa usa la misma proyección de consulta activa (búsqueda + filtro) e incluye:

- Encabezado empresarial.
- Fecha y hora de generación.
- Tabla con identificador, título, `ReportadoPor`, responsable, prioridad, estado y fecha.
- Estado vacío comprensible si no hay incidencias.

La exportación a HTML debe usar UTF-8 y escapar el contenido textual para evitar ruptura de formato.
