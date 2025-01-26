# uDesktopMascot

[![Versión de Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Versiones](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Nota**: Los idiomas mencionados anteriormente (English, 中文, Español, Français) han sido generados mediante traducción automática por GPT-4o-mini. Para la precisión y matices de la traducción, consulte el texto original (日本語).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Descripción](#descripción)
  * [Lista de características](#lista-de-características)
  * [Ejecución en macOS](#ejecución-en-macos)
  * [requisitos](#requisitos)
  * [licencia](#licencia)
  * [Materiales](#materiales)
  * [Créditos de los creadores](#créditos-de-los-creadores)
  * [Avisos de terceros](#avisos-de-terceros)
  * [patrocinador](#patrocinador)
<!-- TOC -->

## Descripción

"uDesktopMascot" es un proyecto de código abierto que muestra un personaje en el escritorio y reproduce reacciones y sonidos en función de la interacción del usuario. Este proyecto ha sido desarrollado utilizando Unity y soporta personajes en formato VRM, lo que permite disfrutar fácilmente de tus personajes favoritos en el escritorio.

**Plataformas soportadas**
* Windows 10/11
* macOS

## Lista de características

La aplicación incluye las siguientes funciones. Consulte la lista a continuación para más detalles.

La adición de activos externos se puede lograr colocando archivos en la carpeta StreamingAssets.

<details>

<summary>Modelos y animaciones</summary>
* Carga y visualización de archivos de modelo colocados en StreamingAssets.
  * Soporta modelos en formato VRM (1.x, 0.x).
  * Soporta modelos en formato GLB/GLTF. (No soporta animaciones)
  * Soporta modelos en formato FBX. (Sin embargo, algunos modelos pueden tener problemas de carga de texturas. Además, no soporta animaciones).
    * Las texturas pueden cargarse colocando los archivos en StreamingAssets/textures/.

</details>

<details>

<summary>Voz y BGM</summary>
* Carga y reproducción de archivos de audio colocados en SteamingAssets/Voice/. Se reproducen de forma aleatoria si hay varios.
  * El audio que se reproduce al hacer clic se carga desde los archivos de audio colocados en StreamingAssets/Voice/Click/.
* Carga y reproducción de archivos de música colocados en SteamingAssets/BGM/. También se reproducen de forma aleatoria si hay varios.
* Adición de la voz predeterminada del personaje.
  * La voz predeterminada utiliza audio de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Se reproduce al iniciar la aplicación, al cerrar la aplicación, y al hacer clic.

</details>

<details>

<summary>Configuración de la aplicación mediante archivo de texto</summary>
Puedes cambiar la configuración de la aplicación mediante el archivo application_settings.txt.

La estructura del archivo de configuración es la siguiente:

```txt
[Character]
ModelPath=default.vrm
TexturePaths=test.png
Scale=3
PositionX=0
PositionY=0
PositionZ=0
RotationX=0
RotationY=0
RotationZ=0

[Sound]
VoiceVolume=1
BGMVolume=0.5
SEVolume=1

[Display]
Opacity=1
AlwaysOnTop=True

[Performance]
TargetFrameRate=60
QualityLevel=2
```

</details>

<details>

<summary>Pantalla de menú</summary>

* Puedes configurar la imagen de fondo de la pantalla de menú y el color de fondo.
  * La imagen de fondo se puede cargar desde archivos de imagen colocados en StreamingAssets/Menu/. Los formatos de imagen soportados son:
    * PNG
    * JPG (JPEG)
    * BMP
    * GIF (imagen estática)
    * TGA
    * TIFF
  * El color de fondo puede especificarse mediante un código de color.

</details>

## Ejecución en macOS

Al ejecutar la aplicación en macOS, la aplicación puede ser bloqueada por GateKeeper. En ese caso, ejecuta el siguiente comando desde la terminal:

```sh
xattr -r -c uDesktopMascot.app
```

## requisitos
* Unity 6000.0.31f1 (IL2CPP)

## licencia
* El código está licenciado bajo la [Licencia Apache 2.0](LICENSE).
* Los siguientes activos están licenciados bajo [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/):
  * BGM
  * Modelos

## Materiales
* La animación predeterminada del personaje se ha creado utilizando el [conjunto de datos de animación para "VRM o-Hinagata"](https://fumi2kick.booth.pm/items/1655686). Se ha verificado la distribución de este material en el repositorio.
* La fuente utilizada es [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). Esta fuente se redistribuye bajo la [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Los derechos de autor de la fuente pertenecen al autor original (Google).
* La voz predeterminada utiliza audio de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). Se ha verificado el uso de esta voz con COEIROINK previamente.
* El ícono de los botones se obtiene de [MingCute](https://github.com/MidraLab/MingCute).

## Créditos de los creadores
* Modelos: "Aozora"
* BGM: MidraLab (eisuke)
* Icono del software: Yamucha

## Avisos de terceros

Consulte [NOTICE](./NOTICE.md).

## patrocinador
- Luna
- uezo