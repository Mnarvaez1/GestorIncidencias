# Investigación técnica: Gestión de incidencias

## Estado

Este documento registra decisiones para la planificación. No implica que se hayan implementado los cambios.

## Decisión 1: Persistencia local mediante XML y `DataContractSerializer`

- **Decisión**: usar un archivo XML local serializado con `DataContractSerializer`, en una carpeta de datos de usuario bajo `%LOCALAPPDATA%\GestorIncidencias\incidencias.xml`.
- **Motivo**: es compatible con .NET Framework 4.7.2, no introduce una dependencia de serialización externa y permite validar la estructura al cargar.
- **Alternativas consideradas**: JSON con Newtonsoft.Json y base de datos local. JSON requeriría una dependencia adicional; una base de datos añadiría más infraestructura de la necesaria para un único operador y una sola instancia.
- **Consecuencia**: habrá que añadir la referencia de framework necesaria para serialización si el proyecto no la incorpora, y definir un formato persistido estable.

## Decisión 2: Guardado atómico con archivo temporal y copia de respaldo

- **Decisión**: escribir primero un archivo temporal en la misma carpeta, validar que se puede leer, conservar el archivo anterior como copia de respaldo y reemplazar el archivo principal solo después de completar la escritura.
- **Motivo**: evita que un fallo de escritura deje un archivo principal truncado y permite conservar los datos anteriores.
- **Alternativas consideradas**: sobrescribir directamente el archivo y confiar en el cierre del stream. Se descarta porque no cumple la protección exigida frente a fallos de almacenamiento.
- **Consecuencia**: los errores de escritura deben propagarse como resultado fallido; la interfaz no mostrará confirmación de éxito si el reemplazo no termina.

## Decisión 3: Rechazo seguro de datos dañados

- **Decisión**: cargar a través de un servicio que valide existencia, XML, versión/formato, identificadores, fechas, estados y prioridades. Ante un error, informar y bloquear el guardado automático sobre el archivo dañado.
- **Motivo**: la especificación exige preservar el contenido original y evitar su sobrescritura automática.
- **Alternativas consideradas**: descartar el archivo y comenzar una lista vacía. Se descarta porque podría ocultar la pérdida de datos.

## Decisión 4: Control de una sola instancia

- **Decisión**: proteger el arranque con un `Mutex` nombrado para la aplicación y cerrar la segunda instancia con un mensaje claro.
- **Motivo**: satisface el alcance de una sola instancia activa y evita escrituras concurrentes sobre el mismo archivo.
- **Alternativas consideradas**: permitir varias instancias con bloqueo de archivo. Añade complejidad y no es necesaria para el alcance inicial.

## Decisión 5: Pruebas MSTest sobre .NET Framework 4.7.2

- **Decisión**: añadir `GestorIncidencias.Tests` como proyecto clásico de .NET Framework 4.7.2, con `MSTest.TestFramework` 4.5.1, `MSTest.TestAdapter` 4.5.1 y `Microsoft.NET.Test.Sdk` 18.11.0 en `packages.config`.
- **Motivo**: la consulta de metadatos de NuGet del 2026-10-24 mostró versiones estables publicadas con grupo de destino `.NETFramework4.6.2`, compatible con 4.7.2, y soporte para VSTest/Visual Studio.
- **Alternativas consideradas**: NUnit y xUnit. No se eligen porque la solicitud exige MSTest.
- **Nota**: las versiones quedan documentadas, pero no se instalan en esta fase. La restauración y la ejecución real deberán validarse en Visual Studio 2026.

## Decisión 6: Instancia única desde `Program.Main`

- **Decisión**: adquirir el `Mutex` antes de crear `frmIncidencias` o cargar datos, mantenerlo durante toda la ejecución y liberarlo en `finally` al terminar `Main`.
- **Motivo**: evita que una segunda instancia toque el almacenamiento y garantiza que `Load` no pueda duplicar el bloqueo.
- **Alternativas consideradas**: adquirir el bloqueo en `Form.Load` o en el repositorio. Se descartan porque llegan después de crear la UI o permiten que varias instancias compitan durante el arranque.

## Decisión 7: Descubrimiento, TRX y pruebas vacías

- **Decisión**: localizar VSTest con `vswhere.exe`, pasar explícitamente `TestAdapterPath`, generar TRX y fallar si no se descubre ninguna prueba.
- **Motivo**: hace reproducible la ejecución en Visual Studio y CI, conserva evidencia y evita que un pipeline verde oculte una suite no descubierta.
- **Alternativas consideradas**: depender de `PATH` o ejecutar solo `dotnet test`. Se descartan por la estructura clásica `packages.config` y por la necesidad de controlar el adaptador MSTest.

## Decisión 8: Compilación del proyecto existente

- **Decisión**: usar MSBuild de Visual Studio 2026 para la solución `.slnx`, con configuración Debug o Release según el contexto de CI.
- **Motivo**: el proyecto es un `.csproj` clásico de .NET Framework 4.7.2; MSBuild de Visual Studio es el comando compatible con su formato y referencias de escritorio.
- **Comprobado**: `MSBuild.exe` está disponible en `C:\Program Files\Microsoft Visual Studio\18\Community\...`; la solución ya compiló correctamente antes de esta planificación.
- **Alternativa considerada**: `dotnet build`. Se mantiene como alternativa de diagnóstico, pero no será el comando principal del plan para este proyecto clásico.
