Pasos a seguir:

    Construir el Proyecto:
        Asegúrate de construir el proyecto en modo Release para que mp3downloader.exe se cree en la carpeta F:\Release.

    Verificar Archivo en la Carpeta de Salida:
        Verifica manualmente que mp3downloader.exe esté presente en la carpeta F:\Release antes de ejecutar el script.

    Ejecutar el Script .bat:
        Una vez que te asegures de que el archivo está en la carpeta correcta, ejecuta el script .bat nuevamente.



@echo off

:: Set ILMerge version and path
SET ILMERGE_VERSION=3.0.41
SET ILMERGE_PATH=F:\PROYECTOS Y SISTEMAS\Mp3_Downloader\MP3_Downloader\packages\ILMerge.%ILMERGE_VERSION%\tools\net452

:: Set your target executable name and build configuration
SET APP_NAME=mp3downloader.exe
SET BUILD_CONFIG=Release

:: Define output directory as the root of drive F:
SET OUTPUT_DIR=F:\Release

:: Define path to ffmpeg.exe (absolute path)
SET FFMPEG_PATH=F:\PROYECTOS Y SISTEMAS\Mp3_Downloader\MP3_Downloader\MP3_Downloader\ffmpeg.exe

:: Ensure the output directory exists
if not exist "%OUTPUT_DIR%" mkdir "%OUTPUT_DIR%"

:: Copy ffmpeg.exe to the output directory
echo Copying ffmpeg.exe to the output directory...
copy "%FFMPEG_PATH%" "%OUTPUT_DIR%"

:: Check if the target executable exists in the output directory
if not exist "%OUTPUT_DIR%\%APP_NAME%" (
    echo Error: %APP_NAME% not found in %OUTPUT_DIR%.
    echo Please ensure that %APP_NAME% is built and placed in the correct directory.
    pause
    exit /b 1
)

:: Merge DLLs using ILMerge
echo Merging %APP_NAME% ...
"%ILMERGE_PATH%\ILMerge.exe" ^
  /lib:"%OUTPUT_DIR%" ^
  /out:"%OUTPUT_DIR%\%APP_NAME%" ^
  "%OUTPUT_DIR%\%APP_NAME%" ^
  Dependency1.dll ^
  Dependency2.dll ^
  "%OUTPUT_DIR%\ffmpeg.exe"

:: Check if merge was successful
IF EXIST "%OUTPUT_DIR%\%APP_NAME%" (
  echo Merging completed successfully.
) ELSE (
  echo Merging failed.
)

:: Optional: List the output directory contents
dir "%OUTPUT_DIR%"

pause
