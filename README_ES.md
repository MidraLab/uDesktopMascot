# uDesktopMascot

[![Versión de Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Lanzamientos](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Prueba de CI de Unity](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

日本語 | [Inglés](README_EN.md) | [Chino](README_CN.md) | [Español](README_ES.md) | [Francés](README_FR.md)

**Nota**: Los idiomas mencionados anteriormente (Inglés, Chino, Español, Francés) fueron generados mediante traducción automática de GPT-4o-mini. Para la precisión y matices de la traducción, consulte el texto original (japonés).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Resumen](#resumen)
  * [Lista de características](#lista-de-características)
  * [Ejecución en macOS](#ejecución-en-macos)
  * [requisitos](#requisitos)
  * [licencia](#licencia)
  * [Acerca del material](#acerca-del-material)
  * [Créditos del creador](#créditos-del-creador)
  * [Avisos de terceros](#avisos-de-terceros)
  * [patrocinador](#patrocinador)
<!-- TOC -->

## Resumen

"uDesktopMascot" es un proyecto de código abierto que muestra un personaje en el escritorio y reproduce reacciones y sonidos en función de la interacción del usuario. Este proyecto está desarrollado utilizando Unity y soporta personajes en formato VRM, lo que permite disfrutar fácilmente de sus personajes favoritos en el escritorio.

![](Docs/Image/AppImage.png)

**Plataformas compatibles**
* Windows 10/11
* macOS

## Lista de características

La aplicación incluye las siguientes funciones. Consulte la lista a continuación para más detalles.

La adición de activos externos se puede realizar colocando los archivos en la carpeta StreamingAssets.

<details>

<summary>Modelos y Animaciones</summary>

* Carga y muestra cualquier archivo de modelo colocado en StreamingAssets.
  * Soporta modelos en formato VRM (1.x, 0.x).
  * Soporta modelos en formato GLB/GLTF. (No soporta animaciones)
  * Soporta modelos en formato FBX. (Sin embargo, algunos modelos pueden no cargar texturas. Además, no soporta animaciones)
    * Las texturas se pueden cargar colocando archivos en StreamingAssets/textures/.

</details>

<details>

<summary>Voz y BGM</summary>

* Carga y reproduce archivos de audio colocados en StreamingAssets/Voice/. Si hay varios, se reproducen de forma aleatoria.
  * Los sonidos que se reproducen al hacer clic se cargan desde los archivos de audio colocados en StreamingAssets/Voice/Click/.
* Carga y reproduce archivos de música colocados en StreamingAssets/BGM/. Si hay varios, se reproducen de forma aleatoria.
* Adición de la voz predeterminada del personaje
  * La voz predeterminada utiliza el audio del personaje [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Se reproducen al inicio de la aplicación, al salir de la aplicación y al hacer clic.

</details>

<details>

<summary>Configuración de la aplicación mediante archivo de texto</summary>
Puede cambiar la configuración de la aplicación mediante el archivo application_settings.txt.

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

* Puede configurar la imagen de fondo y el color de fondo de la pantalla de menú.
  * Se pueden cargar imágenes de fondo desde archivos colocados en StreamingAssets/Menu/. Los formatos de imagen compatibles son:
    * PNG
    * JPG (JPEG)
    * BMP
    * GIF (imagen estática)
    * TGA
    * TIFF
  * Se puede especificar un color de fondo mediante el código de color.

</details>

## Ejecución en macOS

Al ejecutar la aplicación en macOS, la aplicación puede ser bloqueada por GateKeeper. En ese caso, ejecute el siguiente comando desde la terminal:

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

## Acerca del material
* La animación del personaje predeterminado se creó utilizando [el conjunto de datos de animación para "VRM Doll Play"](https://fumi2kick.booth.pm/items/1655686). Se ha confirmado que se puede distribuir incluido en el repositorio.
* La fuente utilizada es [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). La redistribución de la fuente Noto Sans JP se realiza bajo la [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Los derechos de autor de la fuente pertenecen al autor original (Google).
* La voz predeterminada utiliza el audio de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). Se ha confirmado previamente el método de uso con COEIROINK.
* Los íconos de los botones utilizan [MingCute](https://github.com/MidraLab/MingCute).

## Créditos del creador
* Modelos: "Aozora" 
* BGM: MidraLab (eisuke)
* Icono de software: Yamucha

## Avisos de terceros

Consulte [NOTICE](./NOTICE.md).

## patrocinador
- Luna
- uezo