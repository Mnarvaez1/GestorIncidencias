# Contrato de repositorio de persistencia

Este contrato define el comportamiento esperado de `IIncidenciaRepository` sin modificar sus firmas actuales.

## Cargar

- `Cargar()` devuelve la colección completa persistida.
- La colección devuelta debe ser una copia independiente de la lista almacenada.
- Cada incidencia de la colección devuelta debe ser una copia independiente del dato almacenado.

## Guardar

- `Guardar(IList<Incidencia> incidencias)` recibe la colección completa que debe quedar almacenada.
- El contenido persistido se reemplaza por completo con la colección recibida.
- `Guardar` no agrega elementos sobre el contenido anterior ni introduce duplicados por acumulación.

## Manejo de fallos

- Si ocurre un fallo en guardado o carga, se conservan los datos anteriores.
- En fallo, el resultado debe devolver `Exito=false`.

## Regla de confirmación en casos de uso

- `Registrar`, `Editar` y `CambiarEstado` deben confirmar guardado exitoso antes de comunicar éxito al operador.
- Solicitar el mismo estado es una operación sin cambios y no requiere guardar.

## Datos dañados y bloqueo de sobrescritura

- Cuando se detecta archivo persistido dañado, se conserva el archivo original.
- Ante archivo dañado, se bloquea la sobrescritura automática estableciendo `PuedeGuardar=false`.
