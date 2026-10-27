# Guía de validación prevista: Gestión de incidencias

## Estado

Guía de ejecución prevista para la fase de implementación. Todavía no existen el proyecto de pruebas, los controles ni el workflow de CI descritos aquí.

## Precondiciones

1. Visual Studio 2026 con las herramientas de desarrollo de escritorio .NET instaladas.
2. .NET Framework 4.7.2 disponible.
3. Restauración de paquetes NuGet habilitada para la solución.
4. Una copia de trabajo limpia en la rama de la funcionalidad.

## Compilación local prevista

```powershell
msbuild .\GestorIncidencias.slnx /t:Restore,Build /p:Configuration=Debug /m
```

La ruta concreta de `MSBuild.exe` debe ser la instalada por Visual Studio 2026 si no está en PATH.

## Pruebas locales previstas

```powershell
msbuild .\GestorIncidencias.slnx /t:Restore,Build /p:Configuration=Debug /m
$vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
$vstest = & $vswhere -latest -products * -requires Microsoft.VisualStudio.PackageGroup.TestTools -find "Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
if (-not $vstest) { throw "No se encontró vstest.console.exe" }
& $vstest .\GestorIncidencias.Tests\bin\Debug\GestorIncidencias.Tests.dll /TestAdapterPath:.\packages\MSTest.TestAdapter.4.5.1\build\_common /Logger:"trx;LogFileName=tests.trx"
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
$trx = Get-ChildItem . -Filter tests.trx -Recurse | Select-Object -First 1
if (-not $trx) { throw "No se generó el archivo TRX" }
```

La ejecución real deberá comprobar además que se descubrió al menos una prueba y fallar si el
recuento es cero. El archivo TRX se conservará como evidencia de cada ejecución.

## Evidencia TDD prevista

- **Rojo**: salida de una prueba ejecutada que falla por la regla aún no implementada, con el
  esqueleto compilable y un TRX generado; no se acepta un error de compilación como evidencia.
- **Verde**: salida de MSTest con la prueba pasando tras la implementación mínima.
- **Refactorización**: diff revisado, segunda ejecución sin regresiones y TRX conservado.

## Verificación manual prevista

1. Abrir `frmIncidencias` en el diseñador y comprobar que todos los controles se visualizan.
2. Ejecutar la aplicación y registrar una incidencia válida.
3. Probar límites, mensajes de validación, edición, estados, búsqueda y filtros.
4. Cerrar y abrir la aplicación para comprobar persistencia.
5. Simular un guardado fallido y un archivo dañado en un entorno de prueba aislado.
