# Especificación funcional: Gestión de incidencias

**Feature**: `001-gestion-incidencias`
**Estado**: Preparada para planificación técnica
**Idioma**: Español

## Resumen

GestorIncidencias permitirá a un operador registrar, consultar, modificar y cambiar el estado de incidencias de una pequeña empresa de software. La aplicación será de uso local y funcionará con un único operador y una sola instancia activa cada vez.

## Alcance

### Incluido

- Registro de incidencias con identificación, fecha, responsable, prioridad y estado.
- Consulta ordenada de incidencias.
- Modificación de los datos permitidos.
- Transiciones controladas de estado.
- Búsqueda parcial y filtro por estado.
- Conservación de la información entre ejecuciones.
- Validación de entradas y comunicación clara de errores.

### Fuera del alcance inicial

- Inicio de sesión o gestión de permisos.
- Eliminación de incidencias.
- Trabajo en red o acceso remoto.
- Múltiples usuarios concurrentes.
- Envío de correos.
- Facturación.
- Filtrado por prioridad.

## Actores

- **Operador**: persona que registra, consulta y mantiene las incidencias en la aplicación local.

## Historias de usuario y pruebas de aceptación

### Historia 1 - Registrar una incidencia (Prioridad: P1)

Como operador, quiero registrar una incidencia con sus datos esenciales para dejar constancia de un problema pendiente.

**Valor y prioridad**: Es la capacidad principal del producto; sin incidencias registradas no se pueden realizar las consultas ni el seguimiento.

**Prueba independiente**: Crear una incidencia válida y comprobar que aparece con un identificador positivo, fecha de creación, estado Pendiente y los valores introducidos.

**Criterios de aceptación**:

1. **Dado** que el operador está en el formulario de registro, **cuando** introduce un título de 5 a 100 caracteres, una descripción de 10 a 1000 caracteres, un responsable de 2 a 80 caracteres y una prioridad válida, **entonces** la incidencia se registra correctamente.
2. **Dado** que los valores contienen espacios al principio o al final, **cuando** se confirma el registro, **entonces** se eliminan esos espacios antes de validar y guardar.
3. **Dado** que se confirma un registro válido, **cuando** se crea la incidencia, **entonces** recibe un identificador positivo y único, la fecha de creación y el estado Pendiente.
4. **Dado** que no se especifica una prioridad, **cuando** se muestra el formulario de alta, **entonces** la prioridad inicial es Media.
5. **Dado** que ya existe una incidencia con el mismo título, **cuando** se registra otra incidencia válida, **entonces** se permite el registro porque los títulos pueden repetirse.
6. **Dado** que existe información registrada, **cuando** la aplicación se reinicia, **entonces** el siguiente identificador conserva la unicidad respecto a los identificadores anteriores.

### Historia 2 - Consultar la lista de incidencias (Prioridad: P1)

Como operador, quiero consultar todas las incidencias con sus datos relevantes para conocer el estado actual del trabajo pendiente.

**Valor y prioridad**: La consulta permite controlar el inventario de problemas y es necesaria para cualquier seguimiento.

**Prueba independiente**: Cargar varias incidencias y comprobar que la lista muestra todos los campos requeridos en el orden establecido.

**Criterios de aceptación**:

1. **Dado** que existen incidencias, **cuando** el operador consulta la lista, **entonces** cada fila muestra identificador, título, descripción, responsable, prioridad, estado y fecha de creación.
2. **Dado** que existen incidencias con fechas de creación diferentes, **cuando** se muestra la lista, **entonces** las más recientes aparecen primero.
3. **Dado** que dos incidencias tienen la misma fecha de creación, **cuando** se muestra la lista, **entonces** se ordenan por identificador descendente.
4. **Dado** que no existen incidencias, **cuando** el operador consulta la lista, **entonces** se muestra un estado vacío comprensible sin error técnico.

### Historia 3 - Modificar una incidencia (Prioridad: P1)

Como operador, quiero corregir los datos descriptivos de una incidencia para mantener la información actualizada.

**Valor y prioridad**: La información puede cambiar o contener errores; poder corregirla es necesario para que el seguimiento sea fiable.

**Prueba independiente**: Seleccionar una incidencia, modificar cada campo permitido con valores válidos y comprobar qué datos permanecen invariantes.

**Criterios de aceptación**:

1. **Dado** que existe una incidencia, **cuando** el operador modifica título, descripción, responsable o prioridad con valores válidos, **entonces** los cambios se guardan.
2. **Dado** que se modifica una incidencia, **cuando** se confirma la operación, **entonces** se conservan el identificador, la fecha de creación y el estado.
3. **Dado** que se introduce un valor inválido, **cuando** se intenta confirmar la modificación, **entonces** se rechaza la operación, se indica qué debe corregirse y se conserva la información anterior.
4. **Dado** que el operador cancela o no confirma la edición, **cuando** abandona la operación, **entonces** no se altera la incidencia.

### Historia 4 - Cambiar el estado de una incidencia (Prioridad: P1)

Como operador, quiero avanzar o devolver una incidencia entre estados permitidos para reflejar su progreso real.

**Valor y prioridad**: El estado permite distinguir los problemas pendientes, en tratamiento y resueltos.

**Prueba independiente**: Ejecutar cada transición permitida y rechazada sobre incidencias con estados conocidos.

**Criterios de aceptación**:

1. **Dado** que una incidencia está Pendiente, **cuando** se solicita el estado En proceso, **entonces** el estado cambia a En proceso.
2. **Dado** que una incidencia está En proceso, **cuando** se solicita el estado Resuelta, **entonces** el estado cambia a Resuelta.
3. **Dado** que una incidencia está Resuelta, **cuando** se solicita el estado En proceso, **entonces** el estado cambia a En proceso.
4. **Dado** que se selecciona el estado actual, **cuando** se confirma el cambio, **entonces** la incidencia permanece sin cambios y no se produce un error.
5. **Dado** que se solicita cualquier otra transición, **cuando** se confirma, **entonces** se rechaza y se muestra un mensaje claro sin modificar el estado anterior.

### Historia 5 - Buscar y filtrar incidencias (Prioridad: P1)

Como operador, quiero buscar por título o responsable y filtrar por estado para localizar rápidamente las incidencias que necesito revisar.

**Valor y prioridad**: La localización rápida reduce el tiempo de consulta y permite centrarse en el trabajo pendiente.

**Prueba independiente**: Crear incidencias con distintos títulos, responsables y estados, y combinar búsquedas con cada filtro.

**Criterios de aceptación**:

1. **Dado** que existen incidencias, **cuando** el operador escribe una parte del título o del responsable, **entonces** se muestran las coincidencias parciales sin distinguir mayúsculas y minúsculas.
2. **Dado** que el operador selecciona Todos, Pendiente, En proceso o Resuelta, **cuando** aplica el filtro, **entonces** solo se muestran las incidencias del filtro elegido.
3. **Dado** que hay una búsqueda y un filtro de estado activos, **cuando** se consulta la lista, **entonces** solo se muestran las incidencias que cumplen ambas condiciones.
4. **Dado** que la búsqueda está vacía, **cuando** se consulta la lista, **entonces** se muestran todas las incidencias que cumplen el filtro seleccionado.
5. **Dado** que no existen coincidencias, **cuando** se completa la búsqueda o el filtro, **entonces** se muestra un estado sin resultados comprensible.

### Historia 6 - Conservar las incidencias (Prioridad: P1)

Como operador, quiero conservar las operaciones confirmadas al cerrar y volver a abrir la aplicación para no perder el trabajo realizado.

**Valor y prioridad**: La persistencia evita la pérdida de información y es esencial para una aplicación de seguimiento.

**Prueba independiente**: Confirmar altas, modificaciones y cambios de estado, cerrar la aplicación, abrirla de nuevo y comprobar que los datos se mantienen.

**Criterios de aceptación**:

1. **Dado** que una operación se confirma correctamente, **cuando** el operador cierra y vuelve a abrir la aplicación, **entonces** la información confirmada permanece disponible.
2. **Dado** que falla el guardado de una operación, **cuando** se comunica el error, **entonces** se conservan los datos anteriores y no se anuncia la operación como exitosa.
3. **Dado** que los datos guardados están dañados, **cuando** se inicia o consulta la aplicación, **entonces** se informa del problema y se bloquea la sobrescritura automática del contenido dañado.
4. **Dado** que existe un fallo de carga, **cuando** el operador intenta continuar, **entonces** recibe un mensaje comprensible y no se presenta contenido como válido sin haberlo podido recuperar.

### Historia 7 - Validar entradas y comunicar errores (Prioridad: P1)

Como operador, quiero recibir mensajes claros cuando una operación no es válida para poder corregirla sin perder información.

**Valor y prioridad**: La validación protege la calidad de los datos y evita errores difíciles de detectar posteriormente.

**Prueba independiente**: Intentar registrar y modificar con cada límite inválido, campos vacíos y datos no admitidos.

**Criterios de aceptación**:

1. **Dado** que un campo obligatorio está vacío después de quitar espacios, **cuando** se confirma la operación, **entonces** se rechaza y se identifica el campo que debe completar.
2. **Dado** que un texto queda por debajo o por encima de su longitud permitida, **cuando** se confirma, **entonces** se rechaza con el límite que debe corregirse.
3. **Dado** que una prioridad no pertenece a Baja, Media o Alta, **cuando** se confirma, **entonces** se rechaza sin modificar la información anterior.
4. **Dado** que una lista o búsqueda no tiene resultados, **cuando** se muestra la pantalla, **entonces** se informa de esa situación sin presentar un error técnico.

## Casos límite

- Título con exactamente 5 y 100 caracteres: válido.
- Título con 4 o 101 caracteres: inválido.
- Descripción con exactamente 10 y 1000 caracteres: válida.
- Descripción con 9 o 1001 caracteres: inválida.
- Responsable con exactamente 2 y 80 caracteres: válido.
- Responsable con 1 u 81 caracteres: inválido.
- Campos compuestos solo por espacios: inválidos después de quitar espacios.
- Espacios internos: se conservan; solo se quitan los espacios iniciales y finales.
- Dos incidencias con el mismo título: permitidas.
- Identificador próximo al último utilizado tras reiniciar: no se reutiliza.
- Fechas de creación iguales: desempate por identificador descendente.
- Cambio al estado actual: no modifica la incidencia.
- Transición no permitida: se rechaza y conserva el estado anterior.
- Búsqueda vacía con cada filtro: muestra el contenido del filtro.
- Búsqueda sin coincidencias: muestra una indicación de ausencia de resultados.
- Fallo durante el guardado: conserva los datos anteriores y no confirma éxito.
- Contenido persistido dañado: informa y evita sobrescribirlo automáticamente.
- Lista inicial sin incidencias: muestra un estado vacío claro.

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

## Entidades principales

- **Incidencia**: problema registrado por el operador. Incluye identificador, título, descripción, responsable, prioridad, estado y fecha de creación.
- **Prioridad**: clasificación Baja, Media o Alta.
- **Estado de incidencia**: Pendiente, En proceso o Resuelta.
- **Filtro de estado**: selección Todos, Pendiente, En proceso o Resuelta para limitar la consulta.

## Criterios de éxito

- **CE-001**: El operador puede registrar una incidencia válida y verla en la lista en una única sesión, sin perder ninguno de sus siete datos mostrables.
- **CE-002**: El 100 % de los intentos con valores fuera de los límites definidos se rechaza con indicación del campo que debe corregirse.
- **CE-003**: El 100 % de las transiciones permitidas se aplica y el 100 % de las transiciones no permitidas se rechaza sin alterar el estado anterior.
- **CE-004**: Una búsqueda por título o responsable devuelve todas y solo las coincidencias parciales que no distinguen mayúsculas y minúsculas.
- **CE-005**: Las operaciones confirmadas siguen disponibles después de cerrar y volver a abrir la aplicación.
- **CE-006**: Ningún fallo de guardado o datos dañados produce una confirmación falsa ni sobrescribe automáticamente los datos anteriores.
- **CE-007**: El operador puede identificar si una consulta no tiene resultados sin interpretar un error técnico.
- **CE-008**: Las pruebas de aceptación cubren todas las historias P1 y sus casos límite antes de considerar terminada la funcionalidad.

## Supuestos y dependencias

- Existe un único operador por ejecución y no se requiere autenticación.
- La aplicación se ejecuta localmente en un equipo con Windows y no necesita conectividad de red para registrar o consultar incidencias.
- La fecha de creación se asigna al confirmar el registro y no se modifica posteriormente.
- El almacenamiento persistente forma parte de la aplicación, pero su mecanismo concreto se decidirá durante la planificación técnica.
- La política de recuperación ante datos dañados consiste, como mínimo, en informar, conservar el contenido original y bloquear la sobrescritura automática.
- Los mensajes deben estar redactados en español y ser comprensibles para un operador no técnico.

## Fuera de alcance

No se especifican autenticación, eliminación, red, concurrencia multiusuario, correo electrónico, facturación ni filtrado por prioridad en esta versión.
