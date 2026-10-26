# Contrato de repositorio de persistencia

Este contrato define el comportamiento esperado de `IIncidenciaRepository` sin modificar sus firmas actuales.

## Compatibilidad de esquema

- El repositorio DEBE aceptar XML con esquema previo de siete campos (sin `ReportadoPor`) y esquema ampliado.
- La ausencia de `ReportadoPor` en datos legacy NO DEBE provocar error de carga ni pérdida de información existente.
- Al guardar datos del esquema ampliado, `ReportadoPor` DEBE persistirse en XML.

## Cargar

- `Cargar()` devuelve la colección completa persistida.
- La colección devuelta debe ser una copia independiente de la lista almacenada.
- Cada incidencia de la colección devuelta debe ser una copia independiente del dato almacenado.
- En registros legacy sin `ReportadoPor`, la carga debe mantener identidad y valores históricos, devolviendo una señal de ausencia interpretable por capas superiores.

## Guardar

- `Guardar(IList<Incidencia> incidencias)` recibe la colección completa que debe quedar almacenada.
- El contenido persistido se reemplaza por completo con la colección recibida.
- `Guardar` no agrega elementos sobre el contenido anterior ni introduce duplicados por acumulación.
- El guardado de una incidencia legacy editada con valor explícito en `ReportadoPor` debe persistir ese valor sin alterar identificador ni fecha.

## Manejo de fallos

- Si ocurre un fallo en guardado o carga, se conservan los datos anteriores.
- En fallo, el resultado debe devolver `Exito=false`.

## Regla de confirmación en casos de uso

- `Registrar`, `Editar` y `CambiarEstado` deben confirmar guardado exitoso antes de comunicar éxito al operador.
- Solicitar el mismo estado es una operación sin cambios y no requiere guardar.

## Datos dañados y bloqueo de sobrescritura

- Cuando se detecta archivo persistido dañado, se conserva el archivo original.
- Ante archivo dañado, se bloquea la sobrescritura automática estableciendo `PuedeGuardar=false`.

## Interpretación legacy requerida

- La representación visible `No informado (registro legado)` se define como una convención de negocio/UI.
- El repositorio no debe inventar personas ni reescribir automáticamente XML legado solo por ausencia de `ReportadoPor`.

## Evidencia TDD (T055)

- Fase roja: `T055-rojo.trx` (63 pruebas, 52 correctas, 11 fallidas en semántica de persistencia previa).
- Fase verde: `T055-verde.trx` (63 pruebas, 63 correctas).

## Extensión prevista para repositorio de empleados

Este bloque define el comportamiento esperado del futuro `IEmpleadoRepository` y su implementación XML, sin alterar el contrato actual de incidencias.

### Compatibilidad de esquema de empleados

- El repositorio de empleados DEBE aceptar ausencia de archivo como estado inicial vacío.
- El repositorio de empleados DEBE detectar XML dañado y devolver `Exito=false` sin sobrescribir automáticamente el archivo original.
- El guardado de empleados DEBE persistir identificador, nombre completo, indicador técnico, estado activo/inactivo y fecha de creación.

### Cargar empleados

- `Cargar()` devuelve la colección completa persistida de empleados.
- La colección devuelta y cada empleado deben ser copias independientes del almacenamiento.
- La carga inválida DEBE bloquear confirmación positiva de operaciones de negocio dependientes.

### Guardar empleados

- `Guardar(IList<Empleado> empleados)` recibe la colección completa que quedará almacenada.
- El contenido persistido se reemplaza por completo con la colección recibida.
- El guardado no agrega elementos por acumulación ni altera identificadores existentes.

### Integración con incidencias y legado

- La persistencia de incidencias legacy con responsable textual DEBE mantenerse compatible aunque no exista vínculo a empleado.
- El repositorio de empleados NO DEBE reescribir incidencias históricas para forzar asignaciones retroactivas.
