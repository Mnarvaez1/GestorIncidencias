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

### Evolución visual solicitada - Modelo 6 (Informe ejecutivo negro y dorado)

Como operador, quiero que `frmVistaPreviaReporte` tenga una presentación ejecutiva moderna para leer y compartir el estado operativo con formato gerencial sin alterar la lógica funcional existente.

**Criterios de evolución**:

1. El formulario conserva vista previa y exportación HTML actuales, pero adopta paleta negro/dorado con fondo crema y tipografía Segoe UI.
2. Se incluye encabezado ejecutivo con marca institucional, título, subtítulo, fecha y hora de generación y separador dorado.
3. Se incorporan cuatro indicadores calculados sobre la colección real recibida: Total, Pendientes, En proceso y Resueltas.
4. La presentación inicial del detalle usa tarjetas/resumen por incidencia con desplazamiento interno; el `DataGridView` nativo de columnas actuales se conserva como vista técnica secundaria accesible.
5. Se mantiene compatibilidad de diseñador de Visual Studio y se preserva la prohibición de componentes de impresión.
6. La ventana de reporte adopta formato compacto (aprox. 1120x700 de área cliente), mantiene visibles los botones inferiores y conserva adaptación al redimensionado sin superposiciones.

### Evolución visual solicitada - Identidad unificada de formularios (Negro/Dorado/Crema)

Como operador, quiero que `frmIncidencias` y `frmEditarIncidencia` compartan la misma identidad visual empresarial del informe ejecutivo para mantener coherencia de uso sin afectar las funciones existentes.

**Criterios de evolución**:

1. `frmIncidencias` y `frmEditarIncidencia` adoptan la paleta: negro carbón `#252729`, dorado `#C5A15C`, crema `#F5F3EC`, blanco `#FFFFFF`, gris oscuro `#484848` y gris secundario `#77736B`.
2. Se preservan todos los eventos y flujos funcionales existentes (alta, edición, búsqueda, filtro, cambio de estado, vista previa y mensajes operativos).
3. En `frmIncidencias` se conserva `DataGridView` y columnas actuales, aplicando solo ajustes visuales de encabezados, filas, selección y contraste.
4. En `frmEditarIncidencia` se mantienen campos, validaciones y orden de tabulación, aplicando solo ajustes visuales de encabezado, etiquetas, entradas y botones.
5. La evolución mantiene compatibilidad con Visual Studio Designer y no introduce dependencias externas ni componentes no nativos.

### Evolución de usabilidad solicitada - Acción de salida visible

Como operador, quiero un botón `Salir` visible en `frmIncidencias` para cerrar la aplicación de forma explícita y segura sin depender solo del botón X de Windows.

**Criterios de evolución**:

1. `frmIncidencias` incorpora un botón `Salir` en el área de acciones, con estilo visual consistente con la paleta negro/dorado/crema.
2. Al pulsar `Salir`, el sistema solicita confirmación en español con opciones Sí/No y, si se confirma, cierra el formulario principal por la vía estándar (`Close`).
3. La opción No cancela la salida sin alterar datos ni estado visible.
4. La evolución no modifica búsqueda, filtros, alta/edición, cambio de estado, vista previa, exportación HTML ni persistencia XML.
5. La evolución mantiene compatibilidad con Visual Studio Designer y no introduce cierres forzados (`Environment.Exit`, `Application.ExitThread`).

### Evolución de identidad corporativa solicitada - XAMY TECHNOLOGIES

Como operador, quiero que los formularios y reportes muestren la marca empresarial ficticia `XAMY TECHNOLOGIES` para presentar el sistema con una identidad profesional uniforme, manteniendo `GestorIncidencias` como nombre interno técnico.

**Criterios de evolución**:

1. En `frmIncidencias`, `frmEditarIncidencia` y `frmVistaPreviaReporte` se actualizan únicamente los textos visibles de marca hacia `XAMY TECHNOLOGIES` sin alterar nombres internos de clases, namespaces ni controles.
2. Se preserva la paleta negro carbón/dorado/crema ya aprobada y la tipografía Segoe UI.
3. La exportación HTML conserva datos reales, seguridad de escape (`WebUtility.HtmlEncode`), codificación UTF-8 y estructura funcional, ajustando solo textos de identidad corporativa.
4. Las funcionalidades existentes (alta, edición, estado, búsqueda, filtros, vista previa, exportación y salida segura) permanecen sin cambios de comportamiento.
5. La evolución mantiene compatibilidad con Visual Studio Designer y no introduce dependencias externas ni cambios de arquitectura.

### Evolución visual solicitada - Plantilla HTML ejecutiva XAMY

Como operador, quiero que el archivo HTML exportado tenga la misma estética ejecutiva del informe en Windows Forms para compartir resultados con una presentación corporativa consistente.

**Criterios de evolución**:

1. El HTML exportado adopta paleta negro carbón `#252729`, dorado `#C5A15C`, crema `#F5F3EC`, blanco `#FFFFFF` y gris secundario `#77736B`, manteniendo tipografía Segoe UI.
2. El encabezado HTML incluye marca `XAMY TECHNOLOGIES · SOPORTE TI`, título `INFORME EJECUTIVO`, subtítulo operativo, separador dorado y fecha/hora real de generación.
3. Se renderizan cuatro indicadores dinámicos (Total, Pendientes, En proceso, Resueltas) calculados únicamente desde la colección exportada.
4. La sección principal de detalle muestra incidencias en tarjetas-resumen y conserva la tabla completa como detalle técnico secundario para no perder cobertura contractual.
5. Se preservan seguridad y compatibilidad existentes: UTF-8, `WebUtility.HtmlEncode`, datos filtrados reales, comportamiento de colección vacía y manejo legacy de `ReportadoPor`.

**Estado de validación informado**: el estudiante reporta compilación sin errores, validación visual en diseñador/F5 y regresión MSTest 111/111, además de comprobación manual del HTML exportado en escenarios Todos, Pendiente y sin coincidencias.

### Evolución funcional solicitada - Módulo de administración de empleados

Como operador, quiero administrar empleados y seleccionar técnicos activos como responsables de incidencias para mejorar consistencia operativa sin perder compatibilidad con datos históricos.

**Criterios de evolución**:

1. El sistema incorpora una entidad `Empleado` con identificador único estable, nombre completo, bandera de técnico de soporte y estado Activo/Inactivo.
2. El sistema permite registrar, consultar, editar y buscar empleados por nombre sin eliminar registros históricos.
3. Solo empleados técnicos y activos pueden seleccionarse como responsables al registrar o editar incidencias.
4. El sistema conserva compatibilidad con incidencias históricas cuyo responsable está almacenado solo como texto.
5. `ReportadoPor` se mantiene como dato independiente del técnico responsable.
6. La persistencia de empleados se realiza en XML local seguro, con manejo de fallos sin falso éxito.
7. La evolución mantiene compatibilidad con Visual Studio Designer y no altera reglas existentes de incidencias fuera de la integración prevista.

**Decisiones funcionales propuestas (requieren aprobación antes de codificar)**:

- DF-EMP-01: conservar `Incidencia.Responsable` como texto visible para retrocompatibilidad y añadir vínculo opcional a empleado técnico (id nullable) en una etapa de integración.
- DF-EMP-02: cuando un técnico pase a Inactivo, las incidencias históricas conservan su nombre textual y no se reasignan automáticamente.
- DF-EMP-03: si el vínculo opcional no existe (legacy o carga antigua), el sistema mantiene comportamiento actual por texto sin bloquear edición/consulta.
- DF-EMP-04: no se implementa eliminación física de empleados; el ciclo de vida se resuelve con Activo/Inactivo.

## Casos límite

- `ReportadoPor` con 2 y 80 caracteres: válido.
- `ReportadoPor` con 1 u 81 caracteres: inválido.
- `ReportadoPor` nulo, vacío o solo espacios en alta/edición nueva: inválido.
- Registro legado sin `ReportadoPor`: representación visible `No informado (registro legado)` sin asignar una persona inventada.
- Empleado con nombre de 2 y 120 caracteres: válido.
- Empleado con nombre de 1 o 121 caracteres: inválido.
- Técnico inactivo: no elegible para nuevas asignaciones como responsable.
- Incidencia legacy con responsable textual no presente en catálogo de empleados: se muestra y conserva sin bloqueo.
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
- **RF-039**: El sistema DEBE permitir registrar empleados con identificador único positivo, nombre completo, indicador de técnico de soporte y estado Activo/Inactivo.
- **RF-040**: El sistema DEBE validar nombre completo de empleado con recorte previo y longitud entre 2 y 120 caracteres.
- **RF-041**: El sistema DEBE listar empleados con identificador, nombre, rol técnico (Sí/No) y estado (Activo/Inactivo).
- **RF-042**: El sistema DEBE permitir editar nombre e indicador técnico, y activar/inactivar empleados mediante su estado, conservando su identificador.
- **RF-043**: El sistema NO DEBE eliminar físicamente empleados; la baja funcional se realizará solo mediante estado Inactivo.
- **RF-044**: El sistema DEBE buscar empleados por coincidencia parcial en nombre sin distinguir mayúsculas/minúsculas.
- **RF-045**: El sistema DEBE persistir empleados en XML local y recuperarlos manteniendo identificadores y estados.
- **RF-046**: Si falla la persistencia de empleados, el sistema DEBE informar el error y conservar los datos anteriores sin falso éxito.
- **RF-047**: El sistema DEBE permitir seleccionar únicamente empleados técnicos activos como responsables al registrar o editar incidencias.
- **RF-048**: El sistema DEBE conservar incidencias históricas cuyo responsable solo exista como texto, sin exigir conversión forzada a empleado.
- **RF-049**: El sistema DEBE mantener `ReportadoPor` independiente del responsable técnico y de la entidad empleado.
- **RF-050**: El sistema DEBE cargar datos legacy de incidencias y empleados sin perder trazabilidad ni romper consultas/reportes existentes.
- **RF-051**: El sistema DEBE distinguir entre estado laboral del empleado (Activo/Inactivo) y disponibilidad operativa del técnico (Libre/Ocupado).
- **RF-052**: Un empleado técnico activo DEBE considerarse `Libre` cuando no tenga incidencias abiertas vinculadas por `ResponsableEmpleadoId`.
- **RF-053**: Tras registrar o editar una incidencia con vínculo técnico (`ResponsableEmpleadoId`), el sistema DEBE recalcular disponibilidad de los técnicos involucrados usando incidencias abiertas reales.
- **RF-054**: Para disponibilidad operativa, el sistema DEBE considerar abiertas las incidencias `Pendiente` y `En proceso`, y cerradas las `Resuelta`.
- **RF-055**: Al cambiar una incidencia a `Resuelta`, el sistema DEBE recalcular disponibilidad y dejar `Libre` al técnico solo si no conserva otras incidencias abiertas vinculadas.
- **RF-056**: Si una incidencia cambia de técnico responsable, el sistema DEBE recalcular disponibilidad tanto del técnico saliente como del entrante según incidencias realmente vinculadas.
- **RF-057**: El sistema NO DEBE mantener un indicador persistido de `Ocupado` como estado permanente; la disponibilidad DEBE derivarse de incidencias vinculadas por `ResponsableEmpleadoId`.
- **RF-058**: El sistema NO DEBE inferir ni vincular automáticamente técnicos legacy comparando nombres de texto en `Responsable`.
- **RF-059**: En la selección de responsables técnicos, el sistema DEBE priorizar visualmente técnicos activos y libres, documentando el manejo de selección de técnicos ocupados como regla pendiente de aprobación funcional.
- **RF-060**: `frmEmpleados` DEBE mostrar la columna `Disponibilidad` junto a Nombre, Rol y Estado laboral; si no existe integración real con incidencias vinculadas, DEBE mostrarse `No calculada` en lugar de `Libre` ficticio.
- **RF-061**: `frmIncidencias` DEBE incorporar un botón corporativo `Administrar empleados` que abra `frmEmpleados`, manteniendo estilo visual XAMY negro/dorado/crema y sin alterar `ReportadoPor`.
- **RF-062**: `frmIncidencias` DEBE incorporar la opción de filtro `Activas`, seleccionada por defecto al abrir el formulario, para mostrar únicamente incidencias en estado `Pendiente` y `En proceso`, manteniendo `Todos`, `Pendiente`, `En proceso` y `Resuelta` para consulta histórica.
- **RF-063**: Con filtro `Activas` seleccionado, al cambiar una incidencia a `Resuelta` el sistema DEBE dejar de mostrarla automáticamente en la lista filtrada, sin eliminar ni modificar el registro persistido en XML.
- **RF-064**: La búsqueda por texto DEBE combinarse correctamente con `Activas` y con los demás filtros de estado existentes, preservando el comportamiento actual de vista previa e informe HTML basado en el conjunto filtrado visible.

## Entidades principales

- **Incidencia**: incluye identificador, título, descripción, `ReportadoPor`, responsable, prioridad, estado y fecha de creación.
- **Prioridad**: clasificación Baja, Media o Alta.
- **Estado de incidencia**: Pendiente, En proceso o Resuelta.
- **Filtro de estado**: selección Todos, Pendiente, En proceso o Resuelta.
- **Filtro de estado ampliado**: selección `Activas`, Todos, Pendiente, En proceso o Resuelta, donde `Activas` agrupa Pendiente + En proceso como filtro visual.
- **Reporte de incidencias**: proyección temporal para vista previa y exportación HTML basada en el resultado actual de búsqueda/filtro.
- **Empleado**: registro administrativo con identificador, nombre completo, perfil técnico y estado activo/inactivo.
- **Responsable técnico vinculado**: referencia opcional de incidencia a empleado técnico para integraciones futuras, manteniendo texto legacy.
- **Disponibilidad operativa de técnico**: estado derivado (`Libre`, `Ocupado` o `No calculada`) calculado dinámicamente desde incidencias vinculadas por `ResponsableEmpleadoId`.

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
- **CE-010**: El operador administra empleados (alta, consulta, edición, búsqueda y estado Activo/Inactivo) sin eliminación física ni pérdida de identificadores.
- **CE-011**: Solo técnicos activos aparecen como candidatos a responsable en alta/edición de incidencia.
- **CE-012**: Las incidencias históricas con responsable textual permanecen visibles y editables sin migración forzada.
- **CE-013**: La disponibilidad operativa se recalcula sin persistir bandera fija y refleja correctamente incidencias abiertas/cerradas vinculadas por `ResponsableEmpleadoId`.
- **CE-014**: El módulo de empleados muestra estado laboral y disponibilidad operativa como conceptos diferenciados sin inferencias legacy por nombre.
- **CE-015**: El acceso `Administrar empleados` desde `frmIncidencias` abre el formulario de gestión sin alterar flujos existentes de incidencias.
- **CE-016**: `Activas` aparece como filtro predeterminado y lista únicamente incidencias `Pendiente`/`En proceso`, ocultando automáticamente las que pasan a `Resuelta` sin eliminarlas del repositorio.
- **CE-017**: Los filtros `Todos`, `Pendiente`, `En proceso` y `Resuelta` continúan operativos para consulta histórica y se combinan con búsqueda sin romper vista previa ni exportación HTML.

## Supuestos y dependencias

- Existe un único operador por ejecución y no se requiere autenticación.
- La aplicación se ejecuta localmente en Windows sin dependencia de red.
- La fecha de creación se asigna al registrar y permanece inmutable.
- La estrategia de compatibilidad legacy mostrará `No informado (registro legado)` cuando el XML antiguo no incluya `ReportadoPor`.
- Los mensajes se redactan en español para operador no técnico.
- Se mantienen C# 7.3, Windows Forms y .NET Framework 4.7.2.

## Fuera de alcance

No se especifican autenticación, eliminación, red, concurrencia multiusuario, correo, facturación ni impresión mediante componentes de impresión del framework en esta ampliación.
