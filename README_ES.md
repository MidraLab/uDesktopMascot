# uDesktopMascot

[![Versión de Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Versiones](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Nota**: Los idiomas mencionados anteriormente (English, 中文, Español, Français) han sido generados mediante traducción automática por GPT-4o-mini. Para la precisión y el matiz de la traducción, consulte el texto original (日本語).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Resumen](#resumen)
  * [Lista de funciones](#lista-de-funciones)
  * [requerimientos](#requerimientos)
  * [licencia](#licencia)
  * [Sobre los materiales](#sobre-los-materiales)
  * [Créditos de los creadores](#creditos-de-los-creadores)
  * [Avisos de terceros](#avisos-de-terceros)
  * [patrocinador](#patrocinador)
<!-- TOC -->

## Resumen

"uDesktopMascot" es un proyecto de código abierto que muestra un personaje en el escritorio y reproduce reacciones y sonidos en función de la interacción del usuario. Este proyecto ha sido desarrollado utilizando Unity y soporta personajes en formato VRM, por lo que puedes disfrutar fácilmente de tu personaje favorito en el escritorio.

**Plataformas soportadas**
* Windows 10/11
* macOS

## Lista de funciones

La aplicación implementa las siguientes funciones. Para más detalles, consulte la lista a continuación.

La adición de activos externos se puede lograr colocando los archivos en la carpeta StreamingAssets.

<details>

<summary>Modelos y animaciones</summary>
* Carga y muestra cualquier archivo de modelo colocado en StreamingAssets.
  * Soporta modelos en formato VRM (1.x, 0.x).
  * Soporta modelos en formato GLB/GLTF. (No es compatible con animaciones)
  * Soporta modelos en formato FBX. (Sin embargo, algunos modelos pueden no cargar texturas. Además, no es compatible con animaciones)
    * Las texturas se pueden cargar colocando archivos en StreamingAssets/textures/.

</details>

<details>

<summary>Voz y BGM</summary>
* Carga y reproduce archivos de audio colocados en StreamingAssets/Voice/. Si hay varios, se reproducen aleatoriamente.
  * La voz reproducida al hacer clic se carga desde los archivos de audio colocados en StreamingAssets/Voice/Click/. 
* Carga y reproduce archivos de música colocados en StreamingAssets/BGM/. Si hay varios, se reproducen aleatoriamente.
* Adición de la voz predeterminada del personaje
  * La voz predeterminada utiliza el audio de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Se reproduce al iniciar la aplicación, al cerrar la aplicación y al hacer clic.

</details>

<details>

<summary>Configuración de la aplicación mediante archivo de texto</summary>
Puedes cambiar la configuración de la aplicación a través del archivo application_settings.txt.

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

## requerimientos
* Unity 6000.0.31f1 (IL2CPP)

## licencia
* El código está licenciado bajo [Apache License 2.0](LICENSE).
* Los siguientes activos están licenciados bajo [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/).
  * BGM
  * Modelos

## Sobre los materiales
* La animación del personaje predeterminado se creó utilizando el [paquete de datos de animación para "VRM Oningyou Asobi"](https://fumi2kick.booth.pm/items/1655686). Se ha verificado su uso y distribución en el repositorio.
* La fuente utilizada es [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). Se redistribuye bajo la [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Los derechos de autor de la fuente pertenecen al autor original (Google).
* La voz predeterminada utiliza el audio de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). Se ha confirmado su uso previamente con COEIROINK.
* El ícono de botón se basa en [MingCute](https://github.com/MidraLab/MingCute).

## Créditos de los creadores
* Modelo: "Aozora"
* BGM: MidraLab (eisuke)
* Ícono de software: Yamucha

## Avisos de terceros

Ver [NOTICE](./NOTICE.md).

## patrocinador
- Luna
- uezo