# uDesktopMascot

[![Versión de Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Releases](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Pruebas CI de Unity](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Nota**: Los idiomas anteriores (English, 中文, Español, Français) han sido generados por traducción automática utilizando GPT-4o-mini. Para la precisión y matices de la traducción, consulte el texto original (日本語).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Resumen](#resumen)
  * [Lista de funciones](#lista-de-funciones)
  * [Ejecución en macOS](#ejecución-en-macos)
  * [Requisitos](#requisitos)
  * [Licencia](#licencia)
  * [Materiales](#materiales)
  * [Créditos de los creadores](#créditos-de-los-creadores)
  * [Avisos de terceros](#avisos-de-terceros)
  * [Patrocinadores](#patrocinadores)
<!-- TOC -->

## Resumen

"uDesktopMascot" es un proyecto de código abierto que muestra un personaje en el escritorio y reproduce reacciones y sonidos según la interacción del usuario. Este proyecto está desarrollado utilizando Unity y soporta personajes en formato VRM, lo que permite disfrutar de los personajes favoritos en el escritorio de forma sencilla.

**Plataformas soportadas**
* Windows 10/11
* macOS

## Lista de funciones

La aplicación incluye las siguientes funciones. Consulte la lista a continuación para más detalles.

La adición de activos externos se puede lograr colocando los archivos en la carpeta StreamingAssets.

<details>

<summary>Modelos y animaciones</summary>
* Carga y muestra archivos de modelo ubicados en StreamingAssets.
  * Soporta modelos en formato VRM (1.x, 0.x).
  * Soporta modelos en formato GLB/GLTF. (No soporta animaciones)
  * Soporta modelos en formato FBX. (Sin embargo, algunos modelos pueden no cargar texturas. Además, no soporta animaciones)
    * Las texturas se pueden cargar colocando archivos en StreamingAssets/textures/.

</details>

<details>

<summary>Voces y BGM</summary>
* Carga y reproduce archivos de audio ubicados en StreamingAssets/Voice/. Si hay varios, se reproducirán de forma aleatoria.
  * Los sonidos que se reproducen al hacer clic se cargan desde archivos de audio en StreamingAssets/Voice/Click/. 
* Carga y reproduce archivos de música ubicados en StreamingAssets/BGM/. Si hay varios, se reproducirán de forma aleatoria.
* Adición de la voz predeterminada del personaje
  * La voz predeterminada utiliza el audio de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Se reproduce al iniciar la aplicación, al cerrarla y al hacer clic.

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

* Puede establecer la imagen de fondo y el color de fondo de la pantalla de menú.
  * La imagen de fondo se puede cargar desde archivos de imagen ubicados en StreamingAssets/Menu/. Los formatos de imagen soportados son los siguientes:
    * PNG
    * JPG (JPEG)
    * BMP
    * GIF (imágenes estáticas)
    * TGA
    * TIFF
  * Se puede especificar el color de fondo utilizando un código de color.

</details>

## Ejecución en macOS

Al ejecutar la aplicación en macOS, puede ser bloqueada por GateKeeper. 
En ese caso, ejecute el siguiente comando desde la terminal:

```sh
xattr -r -c uDesktopMascot.app
```

## Requisitos
* Unity 6000.0.31f1 (IL2CPP)

## Licencia
* El código está licenciado bajo la [Licencia Apache 2.0](LICENSE).
* Los siguientes activos están licenciados bajo [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/).
  * BGM
  * Modelos

## Materiales
* La animación por defecto del personaje fue creada utilizando un [paquete de datos de animación para "VRM Hina Doll Play"](https://fumi2kick.booth.pm/items/1655686). Se ha verificado que se puede redistribuir en el repositorio.
* La fuente utilizada es [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). Se redistribuye bajo la [Licencia de Fuente Abierta SIL Versión 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Los derechos de autor de la fuente pertenecen al autor original (Google).
* La voz predeterminada utiliza el audio de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). Se ha verificado previamente el uso con COEIROINK.
* Los iconos de botones utilizan [MingCute](https://github.com/MidraLab/MingCute).

## Créditos de los creadores
* Modelos: "Aozora" 
* BGM: MidraLab (eisuke)
* Icono del software: Yamuya

## Avisos de terceros

Consulte [NOTICE](./NOTICE.md).

## Patrocinadores
- Luna
- uezo