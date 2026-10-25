# Especificación funcional: Gestión de incidencias

**Feature**: `001-gestion-incidencias`  
**Estado**: Ampliada para nueva planificación técnica  
**Idioma**: Español

## Resumen

GestorIncidencias permitirá a un operador registrar, consultar, modificar y cambiar el estado de incidencias de una pequeña empresa de software. Esta ampliación incorpora el campo `ReportadoPor`, una vista previa de reportes en formulario independiente y mejoras visuales sobre la base T039 ya validada.

## Alcance

### Incluido

- Registro de incidencias con identificación, fecha, reportado por, responsable, prioridad y estado.
- Consulta ordenada de incidencias con visualización de `ReportadoPor`.
- Modificación de los datos permitidos incluyendo `ReportadoPor`.
- Transiciones controladas de estado.
- Búsqueda parcial y filtro por estado.
- Conservación de la información entre ejecuciones.
- Compatibilidad de lectura con XML antiguos de siete campos sin `ReportadoPor`.
- Vista previa de reporte en formulario dedicado y exportación a HTML UTF-8.
- Validación de entradas y comunicación clara de errores.

### Fuera del alcance

- Inicio de sesión o gestión de permisos.
- Eliminación de incidencias.
- Trabajo en red o acceso remoto.
- Múltiples usuarios concurrentes.
- Envío de correos.
- Facturación.
- Filtrado por prioridad.
- Dependencias de impresión (`PrintDocument`, `PrintPreviewDialog`, `PrintPreviewControl`, `PrintDialog`).

## Actores

- **Operador**: persona que registra, consulta y mantiene las incidencias en la aplicación local.

## Historias de usuario y pruebas de aceptación

### Historia 1 - Registrar una incidencia (Prioridad: P1)

Como operador, quiero registrar una incidencia con sus datos esenciales para dejar constancia de un problema pendiente.

**Prueba independiente**: Crear una incidencia válida y comprobar identificador positivo, fecha de creación, estado Pendiente y valores ingresados.

**Criterios de aceptación**:

1. **Dado** que el operador registra una incidencia, **cuando** introduce título válido, descripción válida, `ReportadoPor` válido (2 a 80), responsable válido (2 a 80) y prioridad válida, **entonces** la incidencia se registra correctamente.
2. **Dado** que los textos contienen espacios iniciales/finales, **cuando** confirma el registro, **entonces** se recortan antes de validar y guardar.
3. **Dado** que se confirma un registro válido, **cuando** se crea la incidencia, **entonces** recibe identificador único positivo, fecha y estado Pendiente.
4. **Dado** que no se especifica prioridad, **cuando** se abre el alta, **entonces** la prioridad inicial es Media.

### Historia 2 - Consultar la lista de incidencias (Prioridad: P1)

Como operador, quiero consultar todas las incidencias con sus datos relevantes para conocer el estado actual del trabajo pendiente.

**Prueba independiente**: Cargar varias incidencias y comprobar que la lista muestra los campos requeridos en orden.

**Criterios de aceptación**:

1. **Dado** que existen incidencias, **cuando** el operador consulta la lista, **entonces** cada fila muestra identificador, título, descripción, `ReportadoPor`, responsable, prioridad, estado y fecha.
2. **Dado** que existen incidencias con fechas distintas, **cuando** se muestra la lista, **entonces** las más recientes aparecen primero.
3. **Dado** que dos incidencias tienen la misma fecha, **cuando** se muestra la lista, **entonces** se ordenan por identificador descendente.
4. **Dado** que no existen incidencias, **cuando** se consulta, **entonces** se muestra un estado vacío comprensible.

### Historia 3 - Modificar una incidencia (Prioridad: P1)

Como operador, quiero corregir los datos descriptivos de una incidencia para mantener la información actualizada.

**Prueba independiente**: Modificar campos editables válidos y verificar invariantes.

**Criterios de aceptación**:

1. **Dado** que existe una incidencia, **cuando** el operador modifica título, descripción, `ReportadoPor`, responsable o prioridad con valores válidos, **entonces** los cambios se guardan.
2. **Dado** que se confirma la modificación, **cuando** finaliza la operación, **entonces** se conservan identificador, fecha y estado.
3. **Dado** que un dato editable es inválido, **cuando** intenta guardar, **entonces** se rechaza y se mantiene la información anterior.

### Historia 4 - Cambiar el estado de una incidencia (Prioridad: P1)

Como operador, quiero avanzar o devolver una incidencia entre estados permitidos para reflejar su progreso real.

**Prueba independiente**: Ejecutar transiciones permitidas y rechazadas.

**Criterios de aceptación**:

1. **Dado** que una incidencia está Pendiente, **cuando** solicita En proceso, **entonces** cambia a En proceso.
2. **Dado** que una incidencia está En proceso, **cuando** solicita Resuelta, **entonces** cambia a Resuelta.
3. **Dado** que una incidencia está Resuelta, **cuando** solicita En proceso, **entonces** cambia a En proceso.
4. **Dado** que solicita el mismo estado actual, **cuando** confirma, **entonces** no hay cambios ni error.
5. **Dado** que solicita otra transición, **cuando** confirma, **entonces** se rechaza sin mutar datos.

### Historia 5 - Buscar y filtrar incidencias (Prioridad: P1)

Como operador, quiero buscar por título o responsable y filtrar por estado para localizar rápidamente las incidencias.

**Prueba independiente**: Combinar búsqueda y filtro con casos con y sin coincidencias.

**Criterios de aceptación**:

1. **Dado** que existen incidencias, **cuando** escribe parte del título o del responsable, **entonces** se muestran coincidencias parciales sin distinguir mayúsculas.
2. **Dado** que selecciona Todos, Pendiente, En proceso o Resuelta, **cuando** aplica el filtro, **entonces** se listan solo las incidencias de ese estado.
3. **Dado** que búsqueda y filtro están activos, **cuando** consulta, **entonces** se aplica condición AND.
4. **Dado** que no hay coincidencias, **cuando** consulta, **entonces** se muestra un estado sin resultados comprensible.

### Historia 6 - Conservar incidencias y compatibilidad XML (Prioridad: P1)

Como operador, quiero conservar operaciones confirmadas y poder abrir datos históricos para no perder información.

**Prueba independiente**: Reabrir datos con y sin `ReportadoPor`, validar guardado/recuperación y continuidad de identificadores.

**Criterios de aceptación**:

1. **Dado** que una operación se confirma, **cuando** cierra y abre la aplicación, **entonces** la información permanece disponible.
2. **Dado** un XML antiguo sin `ReportadoPor`, **cuando** se carga, **entonces** se recuperan identificadores y los siete datos originales sin pérdida ni alteración.
3. **Dado** un registro antiguo sin `ReportadoPor`, **cuando** se muestra en lista, edición o reporte, **entonces** se representa como `No informado (registro legado)`.
4. **Dado** que falla el guardado o hay datos dañados, **cuando** se informa el error, **entonces** se conservan datos anteriores y no se anuncia éxito.
5. **Dado** que existe corrupción en el XML, **cuando** se intenta cargar, **entonces** se bloquea la sobrescritura automática del contenido dañado.

### Historia 7 - Validar entradas y comunicar errores (Prioridad: P1)

Como operador, quiero recibir mensajes claros cuando una operación no es válida para poder corregirla sin perder información.

**Prueba independiente**: Intentar registrar y modificar con límites inválidos, vacíos y valores no admitidos.

**Criterios de aceptación**:

1. **Dado** que un campo obligatorio queda vacío tras recorte, **cuando** confirma, **entonces** se rechaza indicando el campo.
2. **Dado** que un texto sale de longitud permitida, **cuando** confirma, **entonces** se rechaza indicando el límite.
3. **Dado** que `ReportadoPor` o responsable no cumple 2 a 80 tras recorte, **cuando** confirma, **entonces** se rechaza sin alterar la incidencia.
4. **Dado** que prioridad no pertenece a Baja/Media/Alta, **cuando** confirma, **entonces** se rechaza sin mutación.

### Historia 8 - Vista previa de reporte (Prioridad: P2)

Como operador, quiero visualizar un reporte antes de guardarlo para confirmar su contenido.

**Prueba independiente**: Generar vista previa con filtros aplicados y validar representación con y sin datos.

**Criterios de aceptación**:

1. **Dado** que el operador solicita vista previa, **cuando** se abre `frmVistaPreviaReporte`, **entonces** se muestra encabezado empresarial, fecha/hora de generación y las incidencias reales del filtro/búsqueda actual.
2. **Dado** que hay incidencias, **cuando** se renderiza el reporte, **entonces** incluye identificador, título, `ReportadoPor`, responsable, prioridad, estado y fecha.
3. **Dado** que no hay incidencias, **cuando** se abre la vista previa, **entonces** muestra un estado vacío comprensible sin error técnico.
4. **Dado** que el operador exporta, **cuando** guarda a HTML, **entonces** se crea archivo UTF-8 con contenido escapado correctamente.
5. **Dado** el flujo de vista previa, **cuando** se usa la funcionalidad, **entonces** no depende de impresoras ni de APIs de impresión prohibidas.

### Historia 9 - Mejoras visuales de la pantalla principal (Prioridad: P2)

Como operador, quiero una pantalla principal más clara para filtrar, revisar mensajes y acceder al reporte.

**Prueba independiente**: Validación visual en diseñador y ejecución F5 sobre base T039.

**Criterios de aceptación**:

1. **Dado** el formulario principal, **cuando** se visualiza, **entonces** el filtro de estado aparece compacto y alineado.
2. **Dado** que existen mensajes operativos, **cuando** se muestra la pantalla, **entonces** el área inferior de mensajes es visible y legible.
3. **Dado** que el operador necesita reportes, **cuando** revisa acciones principales, **entonces** existe botón `Vista previa de reporte`.
4. **Dado** la actualización visual, **cuando** se compara con T039, **entonces** se conservan colores corporativos, tipografía Segoe UI y compatibilidad de diseñador.

## Casos límite

- `ReportadoPor` con 2 y 80 caracteres: válido.
- `ReportadoPor` con 1 u 81 caracteres: inválido.
- `ReportadoPor` nulo, vacío o solo espacios en alta/edición nueva: inválido.
- Registro legado sin `ReportadoPor`: representación visible `No informado (registro legado)` sin asignar una persona inventada.
- Exportación HTML con caracteres especiales (`<`, `>`, `&`, comillas): contenido escapado correcto.
- Vista previa con cero incidencias: estado vacío sin excepciones.
- Vista previa y exportación sin componentes de impresión: funcionamiento completo.

## Requisitos funcionales

- **RF-001**: El sistema DEBE permitir registrar incidencias con título, descripción, responsable y prioridad.
- **RF-002**: El sistema DEBE quitar espacios iniciales y finales de los campos de texto antes de validarlos.
- **RF-003**: El sistema DEBE exigir título entre 5 y 100 caracteres inclusive.
- **RF-004**: El sistema DEBE exigir descripción entre 10 y 1000 caracteres inclusive.
- **RF-005**: El sistema DEBE exigir responsable entre 2 y 80 caracteres inclusive.
- **RF-006**: El sistema DEBE aceptar únicamente las prioridades Baja, Media y Alta, usando Media como valor inicial.
- **RF-007**: El sistema DEBE asignar a cada incidencia un identificador positivo y único, una fecha de creación y el estado Pendiente.
- **RF-008**: El sistema DEBE conservar la unicidad de identificadores al reiniciar y permitir títulos repetidos.
- **RF-009**: El sistema DEBE listar identificador, título, descripción, responsable, prioridad, estado y fecha de creación.
- **RF-010**: El sistema DEBE ordenar las incidencias por fecha de creación descendente y, en caso de empate, por identificador descendente.
- **RF-011**: El sistema DEBE permitir modificar título, descripción, responsable y prioridad usando las mismas validaciones del registro.
- **RF-012**: El sistema DEBE conservar identificador, fecha de creación y estado al modificar los datos descriptivos.
- **RF-013**: El sistema DEBE permitir únicamente las transiciones Pendiente → En proceso, En proceso → Resuelta y Resuelta → En proceso.
- **RF-014**: El sistema DEBE tratar la selección del estado actual como una operación sin cambios.
- **RF-015**: El sistema DEBE rechazar transiciones no permitidas con un mensaje claro.
- **RF-016**: El sistema DEBE buscar coincidencias parciales por título o responsable mientras el operador escribe, sin distinguir mayúsculas y minúsculas.
- **RF-017**: El sistema DEBE permitir filtrar por Todos, Pendiente, En proceso y Resuelta.
- **RF-018**: El sistema DEBE combinar la búsqueda con el filtro de estado y mostrar todos los resultados del filtro cuando la búsqueda esté vacía.
- **RF-019**: El sistema DEBE conservar las operaciones confirmadas al cerrar y volver a abrir la aplicación.
- **RF-020**: Si falla el guardado, el sistema DEBE informar del error, conservar los datos anteriores y no anunciar éxito.
- **RF-021**: Si los datos guardados están dañados, el sistema DEBE informar del problema y bloquear su sobrescritura automática.
- **RF-022**: El sistema DEBE rechazar datos inválidos sin alterar la información anterior y comunicar claramente la corrección requerida.
- **RF-023**: El sistema DEBE comunicar de forma comprensible las listas vacías y las búsquedas sin resultados.
- **RF-024**: El sistema DEBE capturar `ReportadoPor` al registrar una incidencia, diferenciándolo de responsable técnico.
- **RF-025**: El sistema DEBE validar `ReportadoPor` con longitud de 2 a 80 caracteres tras recorte.
- **RF-026**: El sistema DEBE mostrar `ReportadoPor` en listado y edición.
- **RF-027**: El sistema DEBE persistir `ReportadoPor` en XML para nuevas incidencias y ediciones.
- **RF-028**: El sistema DEBE cargar XML antiguos de siete campos que no contienen `ReportadoPor` sin perder identificadores ni datos existentes.
- **RF-029**: Para registros legacy sin `ReportadoPor`, el sistema DEBE mostrar la representación `No informado (registro legado)` sin inventar personas.
- **RF-030**: El sistema DEBE mantener los identificadores y reglas de orden al introducir `ReportadoPor`.
- **RF-031**: El sistema DEBE ofrecer un botón de `Vista previa de reporte` en `frmIncidencias`.
- **RF-032**: El sistema DEBE abrir `frmVistaPreviaReporte` con encabezado empresarial y fecha/hora de generación.
- **RF-033**: El sistema DEBE construir la vista previa con incidencias reales según búsqueda y filtro de estado activos.
- **RF-034**: El sistema DEBE mostrar en reporte identificador, título, `ReportadoPor`, responsable, prioridad, estado y fecha.
- **RF-035**: El sistema DEBE manejar correctamente la vista previa cuando no hay incidencias.
- **RF-036**: El sistema DEBE permitir exportar el reporte a HTML local en UTF-8 con contenido escapado.
- **RF-037**: El sistema NO DEBE usar `PrintDocument`, `PrintPreviewDialog`, `PrintPreviewControl` ni `PrintDialog`.
- **RF-038**: El sistema DEBE mantener el diseño base T039, compactar/alinear filtro de estado y mantener visible el área inferior de mensajes.

## Entidades principales

- **Incidencia**: incluye identificador, título, descripción, `ReportadoPor`, responsable, prioridad, estado y fecha de creación.
- **Prioridad**: clasificación Baja, Media o Alta.
- **Estado de incidencia**: Pendiente, En proceso o Resuelta.
- **Filtro de estado**: selección Todos, Pendiente, En proceso o Resuelta.
- **Reporte de incidencias**: proyección temporal para vista previa y exportación HTML basada en el resultado actual de búsqueda/filtro.

## Criterios de éxito

- **CE-001**: El operador puede registrar una incidencia válida y verla en la lista sin perder ninguno de sus ocho datos mostrables.
- **CE-002**: El 100 % de intentos fuera de límites definidos (incluyendo `ReportadoPor`) se rechaza indicando el campo.
- **CE-003**: El 100 % de transiciones permitidas se aplica y el 100 % de no permitidas se rechaza sin alterar estado previo.
- **CE-004**: Búsqueda/filtro devuelve todas y solo las incidencias que cumplen los criterios activos.
- **CE-005**: Operaciones confirmadas y datos legacy siguen disponibles tras reinicio.
- **CE-006**: Ningún fallo de guardado/corrupción produce falso éxito ni sobrescritura automática indebida.
- **CE-007**: XML antiguo sin `ReportadoPor` se interpreta de forma explícita y trazable sin inventar personas.
- **CE-008**: La vista previa muestra datos reales con timestamp y permite exportar HTML UTF-8 escapado.
- **CE-009**: El formulario principal conserva diseño base T039 y compatibilidad con diseñador tras mejoras visuales.

## Supuestos y dependencias

- Existe un único operador por ejecución y no se requiere autenticación.
- La aplicación se ejecuta localmente en Windows sin dependencia de red.
- La fecha de creación se asigna al registrar y permanece inmutable.
- La estrategia de compatibilidad legacy mostrará `No informado (registro legado)` cuando el XML antiguo no incluya `ReportadoPor`.
- Los mensajes se redactan en español para operador no técnico.
- Se mantienen C# 7.3, Windows Forms y .NET Framework 4.7.2.

## Fuera de alcance

No se especifican autenticación, eliminación, red, concurrencia multiusuario, correo, facturación ni impresión mediante componentes de impresión del framework en esta ampliación.
