# uDesktopMascot

[![Unity Version](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)  
[![Releases](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Nota**: Los idiomas anteriores (English, 中文, Español, Français) fueron generados mediante traducción automática por GPT-4o-mini. Para la precisión y matices de la traducción, consulte el texto original (日本語).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Resumen](#resumen)
  * [Lista de Funciones](#lista-de-funciones)
  * [Ejecución en macOS](#ejecución-en-macos)
  * [requisitos](#requisitos)
  * [licencia](#licencia)
  * [Sobre los Materiales](#sobre-los-materiales)
  * [Créditos de los Creadores](#créditos-de-los-creadores)
  * [Notificaciones de Terceros](#notificaciones-de-terceros)
  * [patrocinador](#patrocinador)
<!-- TOC -->

## Resumen

"uDesktopMascot" es un proyecto de código abierto que muestra un personaje en el escritorio y reproduce reacciones y sonidos en función de la interacción del usuario. Este proyecto se desarrolla utilizando Unity y soporta personajes en formato VRM, permitiéndote disfrutar fácilmente de tu personaje favorito en el escritorio.

**Plataformas Soportadas**
* Windows 10/11
* macOS

## Lista de Funciones

La aplicación incluye las siguientes funciones. Para más detalles, consulta la lista a continuación.

Puedes añadir activos externos colocando los archivos en la carpeta StreamingAssets.

<details>

<summary>Modelo y Animación</summary>
* Carga y muestra archivos de modelo colocados en StreamingAssets.
  * Soporta modelos en formato VRM (1.x, 0.x).
  * Soporta modelos en formato GLB/GLTF. (No se admite animación)
  * Soporta modelos en formato FBX. (Sin embargo, algunos modelos pueden no cargar correctamente las texturas. Además, no se admite animación)
    * Las texturas se pueden cargar colocando los archivos en StreamingAssets/textures/.

</details>

<details>

<summary>Voz y BGM</summary>
* Carga y reproduce archivos de audio colocados en StreamingAssets/Voice/. Si hay varios archivos, se reproducirán aleatoriamente.
  * Los sonidos reproducidos al hacer clic cargan archivos de audio colocados en StreamingAssets/Voice/Click/.  
* Carga y reproduce archivos de música ubicados en StreamingAssets/BGM/. Si hay varios, se reproducirán aleatoriamente.
* Adición de voz predeterminada del personaje:
  * La voz predeterminada utiliza audio de [COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Se reproduce al iniciar la aplicación, al cerrarla y al hacer clic.

</details>

<details>

<summary>Ajustes de Aplicación a través de Archivos de Texto</summary>
Puedes modificar la configuración de la aplicación mediante el archivo application_settings.txt.

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

## Ejecución en macOS

Al ejecutar la aplicación en macOS, es posible que GateKeeper bloquee la aplicación.  
En ese caso, ejecuta el siguiente comando desde la terminal:

```sh
xattr -r -c uDesktopMascot.app
```

## requisitos
* Unity 6000.0.31f1(IL2CPP)

## licencia
* El código está licenciado bajo la [Licencia Apache 2.0](LICENSE).
* Los siguientes activos están licenciados bajo [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/):
  * BGM
  * Modelos

## Sobre los Materiales
* La animación del personaje predeterminado se creó utilizando el [“Conjunto de datos de animación para juegos de muñecas VRM”](https://fumi2kick.booth.pm/items/1655686). Esto ha sido confirmado para distribución en el repositorio.
* La fuente utilizada es [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). Se redistribuye bajo la [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Los derechos de autor de la fuente pertenecen a su autor original (Google).
* La voz predeterminada utiliza audio de [COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan). El uso ha sido previamente confirmado con COEIROINK.
* El ícono de botón está diseñado utilizando [MingCute](https://github.com/MidraLab/MingCute).

## Créditos de los Creadores
* Modelos: “アオゾラ”  
* BGM: MidraLab(eisuke)  
* Ícono del Software: やむちゃ

## Notificaciones de Terceros

Ver [NOTICE](./NOTICE.md).

## patrocinador
- Luna
- uezo