# Contratos de reglas de negocio

Estos contratos describen comportamientos verificables para las futuras pruebas de reglas de negocio. No son una API implementada.

## Registro y validación

- Recibir título, descripción, `ReportadoPor`, responsable y prioridad.
- Recortar los textos antes de comprobar longitudes.
- Devolver una incidencia creada solo si todos los valores son válidos.
- Devolver errores identificables por campo cuando una validación falla.
- Asignar identificador positivo, fecha y estado Pendiente.
- Validar `ReportadoPor` y responsable con longitud de 2 a 80 caracteres tras recorte.

## Edición

- Recibir un identificador existente y los cinco campos editables.
- Reutilizar las mismas reglas de validación del registro.
- Conservar identificador, fecha y estado.
- No cambiar la colección si la validación falla.

## Estados

- Permitir únicamente Pendiente → En proceso, En proceso → Resuelta y Resuelta → En proceso.
- Considerar una solicitud igual al estado actual como operación idempotente sin cambios.
- Rechazar el resto con un error claro y sin mutación.

## Consulta

- Ordenar por fecha descendente y luego identificador descendente.
- Buscar parcialmente por título o responsable sin distinguir mayúsculas y minúsculas.
- Aplicar búsqueda y estado como condiciones conjuntas.
- Tratar una búsqueda vacía como ausencia de criterio textual.
- Incluir `ReportadoPor` en la proyección de consulta de incidencias.
- Si `ReportadoPor` está ausente por origen legacy, representar `No informado (registro legado)`.

## Reportes

- Generar vista previa en `frmVistaPreviaReporte` con encabezado empresarial y fecha/hora.
- Construir el reporte con la colección real filtrada y buscada en pantalla principal.
- Incluir identificador, título, `ReportadoPor`, responsable, prioridad, estado y fecha.
- Si no hay incidencias, mostrar estado vacío comprensible.
- Permitir exportar a HTML local UTF-8 con escape de contenido textual.
- Prohibir dependencias de impresión (`PrintDocument`, `PrintPreviewDialog`, `PrintPreviewControl`, `PrintDialog`).

## Persistencia

- Una carga válida devuelve los datos y el siguiente identificador seguro.
- Una carga ausente inicia una colección vacía.
- Una carga inválida devuelve un error sin reemplazar ni borrar el archivo original.
- Un guardado fallido conserva el estado anterior y no devuelve confirmación positiva.
- El guardado nuevo persiste `ReportadoPor` para registros creados o editados en el nuevo modelo.
- La carga de XML de siete campos conserva datos existentes y aplica representación legacy de `ReportadoPor` solo a nivel de visualización.
