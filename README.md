<p align="center">
  <img src="assets/readme/hero.svg" alt="MP3 Downloader: encola enlaces de YouTube y obtén tus canciones en MP3" width="100%">
</p>

<p align="center">
  <img alt="Plataforma: Windows" src="https://img.shields.io/badge/plataforma-Windows-0078D6?logo=windows&logoColor=white">
  <img alt=".NET Framework 4.8" src="https://img.shields.io/badge/.NET%20Framework-4.8-512BD4?logo=dotnet&logoColor=white">
  <img alt="Windows Forms" src="https://img.shields.io/badge/UI-Windows%20Forms-5C2D91">
  <img alt="Lenguaje: C#" src="https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white">
</p>

**MP3 Downloader** es una aplicación de escritorio para Windows que descarga el audio de videos de YouTube y lo guarda como MP3. Pegas los enlaces, se arma una cola y la app se encarga de descargar y convertir cada tema. Incluye además un módulo para ordenar la biblioteca ya descargada: convertir, quitar duplicados y reordenar.

<!-- TODO: agregar capturas de pantalla de ambas ventanas en assets/readme/ y mostrarlas aquí. -->

## Cómo funciona

<img src="assets/readme/flujo.svg" alt="Flujo: enlace de YouTube, cola de descargas, audio de mayor bitrate, conversión a MP3 con FFmpeg" width="100%">

1. La app lee los enlaces de YouTube del portapapeles (o te pide uno) y obtiene el título de cada video.
2. Los agrega a una cola; ignora los que ya están en la cola o ya se descargaron.
3. Descarga el stream de solo audio con mayor bitrate usando [YoutubeExplode](https://github.com/Tyrrrz/YoutubeExplode).
4. Convierte el resultado a MP3 con [FFMpegCore](https://github.com/rosenbjerg/FFMpegCore) (códec `libmp3lame`) y elimina el archivo original.

## Qué incluye

La ventana principal es un contenedor MDI con dos módulos.

| Módulo | Para qué sirve |
| --- | --- |
| **Descargas** | Cola de URLs con estado y progreso, historial de descargas completadas, elegir carpeta de destino, **exportar** e **importar** la cola como `.txt`. |
| **Convertidor** | Trabaja sobre una carpeta de MP3 existente: **convertir todo a MP3**, **eliminar duplicados** y **cambiar el orden** de las canciones. |

<details>
<summary><b>Detalles del convertidor</b></summary>

- **Convertir todo a MP3** genera una copia por archivo con prefijo `new_`, así que los archivos se duplican (la app pide confirmación antes).
- **Cambiar orden** renombra cada canción con un prefijo numérico aleatorio de tres dígitos (`042_Nombre.mp3`) y limpia el nombre (guiones bajos, mayúsculas, caracteres especiales).
- Ambos módulos registran los tiempos de cada operación en un `timing_log.txt` dentro de la carpeta de trabajo.

</details>

<details>
<summary><b>Formato de la lista exportada</b></summary>

Un tema por línea, separado por `|`:

```text
1|Título del video|https://www.youtube.com/watch?v=XXXXXXXXXXX
```

</details>

## Requisitos

- Windows con .NET Framework 4.8.
- `ffmpeg.exe` junto al ejecutable (o dos carpetas por encima del directorio de trabajo). La app lo busca al abrir y muestra un error si no lo encuentra.

> [!NOTE]
> `ffmpeg.exe` pesa más de 100 MB y está excluido del repositorio (`.gitignore`). Descárgalo desde [ffmpeg.org](https://ffmpeg.org/download.html) y colócalo en la carpeta del ejecutable.

## Compilar y ejecutar

1. Clona el repositorio y abre `MP3_Downloader.sln` en Visual Studio 2022.
2. Restaura los paquetes NuGet y compila.
3. Copia `ffmpeg.exe` a la carpeta de salida (`bin\Debug` o `bin\Release`).
4. Ejecuta la app y abre el módulo **Descargas**.

```bash
git clone https://github.com/nelson-nunez/Mp3Downloader.git
```

**Primer uso:** copia un enlace de YouTube, pulsa el botón de agregar a la cola, elige la carpeta de destino y pulsa descargar.

## Stack

| Pieza | Uso |
| --- | --- |
| [YoutubeExplode](https://github.com/Tyrrrz/YoutubeExplode) | Información del video y descarga del stream de audio |
| [FFMpegCore](https://github.com/rosenbjerg/FFMpegCore) | Conversión a MP3 invocando FFmpeg |
| [TagLibSharp](https://github.com/mono/taglib-sharp) | Manejo de metadatos de audio |
| ILMerge | Empaquetado en un único ejecutable (ver `Readme.txt`) |

## Estructura

```text
MP3_Downloader/
├── Form_1_Menu.cs        Ventana principal (MDI)
├── Form_Descargas.cs     Módulo de descargas y cola
├── Form_Convertidor.cs   Módulo de conversión y limpieza
├── Extensions/           Descarga, conversión, duplicados, grids
├── Models/               Encolado, DownloadedVideo, repositorio
└── Resources/            Iconos de la interfaz
```

## Límites conocidos

- Solo acepta enlaces de `youtube.com`; los enlaces cortos `youtu.be` se ignoran.
- Si no existe la carpeta `F:\4.Mi música\ZZ-NUEVAS-DESCARGAS`, hay que elegir el destino a mano.
- Es una herramienta personal: no hay pruebas automatizadas.

## Uso responsable

Descargar contenido de YouTube puede infringir sus términos de servicio y los derechos de autor. Úsala solo con contenido propio, libre o con permiso.

## Licencia

<!-- TODO: el repositorio no incluye archivo de licencia. Elige una y agrega LICENSE. -->
Sin licencia definida por ahora.
