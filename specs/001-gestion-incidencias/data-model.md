# Modelo de datos: Gestión de incidencias

## Estado

Modelo previsto para implementación; no contiene clases implementadas.

## Entidad Incidencia

| Campo | Tipo conceptual | Obligatorio | Reglas |
|---|---|---:|---|
| Identificador | Entero positivo | Sí | Único y monotónico; no se reutiliza después de reiniciar. |
| Título | Texto | Sí | Recortado; longitud entre 5 y 100 caracteres. Puede repetirse. |
| Descripción | Texto | Sí | Recortada; longitud entre 10 y 1000 caracteres. |
| Responsable | Texto | Sí | Recortado; longitud entre 2 y 80 caracteres. |
| Prioridad | Enumeración | Sí | Baja, Media o Alta; valor inicial Media. |
| Estado | Enumeración | Sí | Pendiente, En proceso o Resuelta; valor inicial Pendiente. |
| Fecha de creación | Fecha y hora | Sí | Asignada al registrar; inmutable al editar. |

## Valores controlados

- **Prioridad**: `Baja`, `Media`, `Alta`.
- **Estado**: `Pendiente`, `En proceso`, `Resuelta`.
- **Filtro**: `Todos`, `Pendiente`, `En proceso`, `Resuelta`.

## Reglas de identidad y edición

- La creación asigna identificador, fecha y estado; el operador no los introduce.
- La edición solo cambia título, descripción, responsable y prioridad.
- Una edición inválida no modifica el objeto existente.
- Un cambio de estado es una operación independiente de la edición.

## Transiciones de estado

| Estado actual | Estado solicitado | Resultado |
|---|---|---|
| Pendiente | En proceso | Permitido |
| En proceso | Resuelta | Permitido |
| Resuelta | En proceso | Permitido |
| Cualquiera | Igual al actual | Sin cambios |
| Cualquier otra combinación | Cualquiera | Rechazado con error de negocio |

## Vista de consulta

La colección se proyecta con los siete campos de la entidad: identificador, título,
descripción, responsable, prioridad, estado y fecha de creación.

El orden es fecha de creación descendente y, ante igualdad, identificador descendente.
La búsqueda aplica coincidencia parcial sin distinguir mayúsculas y minúsculas sobre
título o responsable. El filtro de estado se aplica conjuntamente con la búsqueda.
