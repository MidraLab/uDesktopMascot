# uDesktopMascot

[![Versión de Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Lanzamientos](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Prueba CI de Unity](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Nota**: Los idiomas mencionados arriba (English, 中文, Español, Français) han sido generados mediante traducción automática por GPT-4o-mini. Para la precisión y matices de la traducción, consulte el texto original (日本語).

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
  * [Patrocinador](#patrocinador)
<!-- TOC -->

## Resumen

"uDesktopMascot" es un proyecto de código abierto de aplicación de mascota de escritorio, con el tema de `liberación creativa`. Como ejemplo de una función, puede cargar modelos en formato VRM o GLB/FBX y mostrarlos en el escritorio. También puede configurar libremente los colores y las imágenes de fondo de la GUI, como la pantalla del menú y las ventanas de aplicación. Para una lista detallada de funciones, consulte [Lista de funciones](#lista-de-funciones).

![](Docs/Image/AppImage.png)

**Plataformas compatibles**
* Windows 10/11
* macOS

## Lista de funciones

La aplicación cuenta con las siguientes funciones implementadas. Consulte la lista a continuación para más detalles.

La adición de activos externos se puede lograr colocando archivos en la carpeta StreamingAssets.

<details>

<summary>Modelos y animaciones</summary>

* Carga y muestra archivos de modelo personalizados colocados en StreamingAssets.
  * Soporta modelos en formato VRM (1.x, 0.x).
  * Soporta modelos en formato GLB/GLTF. (No se soportan animaciones)
  * Soporta modelos en formato FBX. (Sin embargo, algunos modelos pueden tener problemas al cargar texturas. Además, no se soportan animaciones).
    * Las texturas se pueden cargar colocando archivos en StreamingAssets/textures/.

</details>

<details>

<summary>Voces y BGM</summary>

* Carga y reproduce archivos de audio colocados en StreamingAssets/Voice/. Si hay varios, se reproducirán aleatoriamente.
  * El audio que se reproduce al hacer clic se carga desde los archivos de audio colocados en StreamingAssets/Voice/Click/. 
* Carga y reproduce archivos de música colocados en StreamingAssets/BGM/. Si hay varios, se reproducirán aleatoriamente.
* Adición de voces predeterminadas para los personajes.
  * La voz predeterminada utiliza audio de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Se reproduce al iniciar la aplicación, al cerrarla y al hacer clic.

</details>

<details>

<summary>Configuración de la aplicación mediante un archivo de texto</summary>
Puede modificar la configuración de la aplicación mediante el archivo application_settings.txt.

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

<summary>Pantalla del menú</summary>

* Puede configurar la imagen de fondo y el color de fondo de la pantalla del menú.
  * La imagen de fondo puede cargarse desde archivos de imagen ubicados en StreamingAssets/Menu/. Los formatos de imagen compatibles son los siguientes:
    * PNG
    * JPG (JPEG)
    * BMP
    * GIF (imagen estática)
    * TGA
    * TIFF
  * Se puede especificar un código de color para el color de fondo.

</details>

## Ejecución en macOS

Al ejecutar la aplicación en macOS, es posible que GateKeeper bloquee la aplicación. En ese caso, ejecute el siguiente comando desde la terminal:

```sh
xattr -r -c uDesktopMascot.app
```

## Requisitos
* Unity 6000.0.31f1 (IL2CPP)

## Licencia
* El código está licenciado bajo la [Licencia Apache 2.0](LICENSE).
* Los siguientes activos están licenciados bajo [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/):
  * BGM
  * Modelos

## Materiales
* La animación predeterminada del personaje se ha creado utilizando el [paquete de datos de animación para "VRM muñecos de papel"](https://fumi2kick.booth.pm/items/1655686). Se ha confirmado que se puede distribuir junto con el repositorio.
* La fuente utilizada es [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). La fuente Noto Sans JP se redistribuye bajo la [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Los derechos de autor de la fuente pertenecen al autor original (Google).
* La voz predeterminada se utiliza del [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). Se ha confirmado previamente con COEIROINK sobre el uso.
* Los iconos de los botones se han tomado de [MingCute](https://github.com/MidraLab/MingCute).

## Créditos de los creadores
* Modelos: 「アオゾラ」様
* BGM: MidraLab(eisuke)
* Icono de software: やむちゃ様

## Avisos de terceros

Consulte [NOTICE](./NOTICE.md).

## Patrocinador
- Luna
- uezo