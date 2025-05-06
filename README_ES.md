Sure! Here is the translated text in Spanish:

# uDesktopMascot

[![Versión de Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Lanzamientos](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Pruebas CI de Unity](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Nota**: Los idiomas anteriores (English, 中文, Español, Français) han sido generados mediante traducción automática por GPT-4o-mini. Para la precisión y matices de la traducción, se recomienda consultar el texto original (japonés).

<!-- TOC -->
- [uDesktopMascot](#udesktopmascot)
  - [Resumen](#resumen)
  - [Lista de funciones](#lista-de-funciones)
  - [Ejecución en macOS](#ejecución-en-macos)
  - [Requisitos](#requisitos)
  - [Licencia](#licencia)
  - [Sobre los materiales](#sobre-los-materiales)
  - [Cómo crear el instalador](#cómo-crear-el-instalador)
    - [Windows](#windows)
    - [macOS](#macos)
  - [Créditos de los creadores](#créditos-de-los-creadores)
  - [Avisos de terceros](#avisos-de-terceros)
  - [Patrocinador](#patrocinador)
<!-- TOC -->

## Resumen

"uDesktopMascot" es un proyecto de código abierto de una aplicación de mascotas de escritorio con el tema de `liberación creativa`. 
Como un ejemplo de función, puede cargar modelos en formatos VRM y GLB/FBX, y mostrarlos en el escritorio. Además, puede establecer libremente el color y la imagen de fondo de la GUI, como la pantalla del menú o la ventana de la aplicación.
Para ver una lista detallada de funciones, consulte [Lista de funciones](#lista-de-funciones).

![](Docs/Image/AppImage.png)

**Plataformas compatibles**
* Windows 10/11
* macOS

## Lista de funciones

La aplicación implementa las siguientes funcionalidades. Consulte la lista a continuación para más detalles.

Puede agregar activos externos colocando archivos en la carpeta StreamingAssets.

<details>

<summary>Modelos y animaciones</summary>

* Carga y muestra archivos de modelo de su elección colocados en StreamingAssets.
  * Soporta modelos en formato VRM (1.x, 0.x).
  * Soporta modelos en formato GLB/GLTF (sin soporte de animaciones).
  * Soporta modelos en formato FBX (sin embargo, algunos modelos pueden no cargar texturas y tampoco soporta animaciones).
    * Las texturas se pueden cargar colocando en StreamingAssets/textures/.
* Adición de modelos VRM desde la pantalla de selección y adición de modelos.
  * Adición especificando la ruta.
  * Adición desde el cuadro de diálogo de selección de archivos.

</details>

<details>

<summary>Voz y BGM</summary>

* Carga y reproduce archivos de audio colocados en StreamingAssets/Voice/. Si hay varios, se reproducirán aleatoriamente.
  * Los sonidos que se reproducen al hacer clic se cargan desde archivos de audio en StreamingAssets/Voice/Click/. 
* Carga y reproduce archivos de música colocados en StreamingAssets/BGM/. Si hay varios, se reproducirán aleatoriamente.
* Adición de la voz predeterminada del personaje.
  * La voz predeterminada utiliza los sonidos de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Se reproducirá al iniciar la aplicación, al cerrarla, y al hacer clic.

</details>

<details>

<summary>Configuración de la aplicación mediante archivo de texto</summary>
Puede cambiar la configuración de la aplicación utilizando el archivo application_settings.txt.

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
  * Las imágenes de fondo se pueden cargar desde archivos ubicados en StreamingAssets/Menu/. Los formatos de imagen compatibles son los siguientes:
    * PNG
    * JPG(JPEG)
    * BMP
    * GIF (imagen estática)
    * TGA
    * TIFF
  * Puede especificar un código de color para el color de fondo.
* Desde la pantalla de menú, se puede acceder a las siguientes funciones:
  * Pantalla de selección y adición de modelos
  * Función de chat AI
  * Función LocalWeb
  * Configuración de la aplicación
  * Cierre de la aplicación
* Al presionar el botón de ocultar en la pantalla de menú, puede ocultar la aplicación en la bandeja del sistema solo en Windows.
  * La aplicación ocultada se puede mostrar nuevamente haciendo clic en el icono en la bandeja del sistema.

</details>

## Ejecución en macOS

Al ejecutar la aplicación en macOS, puede que GateKeeper bloquee la aplicación. 
En tal caso, ejecute el siguiente comando desde la terminal.

```sh
xattr -r -c uDesktopMascot.app
```

## requirements
* Unity 6000.1.1f1(IL2CPP)

## Licencia
* El código está licenciado bajo la [Licencia Apache 2.0](LICENSE).
* Los siguientes activos están licenciados bajo [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/):
  * BGM
  * Modelos

## Sobre los materiales
* Las animaciones del personaje predeterminado se crean utilizando [“Colección de datos de animación para juegos con muñecas VRM”](https://fumi2kick.booth.pm/items/1655686). Se ha confirmado que se puede distribuir incluyendo en el repositorio.
* La fuente utilizada es [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). Se redistribuye la fuente Noto Sans JP bajo [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Los derechos de autor de la fuente pertenecen al autor original (Google).
* La voz predeterminada utiliza los sonidos de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). Se ha confirmado previamente con COEIROINK sobre el uso.
* El icono del botón utiliza [MingCute](https://github.com/MidraLab/MingCute).

## Cómo crear el instalador
### Windows
* Construya en Unity en una carpeta llamada `uDesktopMascot` dentro de `build`.
```
uDesktopMascot
└── build
    └── uDesktopMascot
        ├── uDesktopMascot.exe
        └── README.txt
        ...
```

* Instale [Inno Setup](https://www.jrsoftware.org/isdl.php).
  
* Cuando lo abra, haga clic en `More files` y seleccione el archivo `setup.iss` que se encuentra en el proyecto.
  
![](Docs/Image/SetupIss-1.png)
* Una vez seleccionado, haga clic en el botón de reproducción.
  
![](Docs/Image/SetupIss-2.png)
* Una vez finalizada la construcción, se generará el instalador en la raíz del proyecto.

### macOS
Solo puede crear el instalador en una PC con macOS.

* Construya en Unity en una carpeta llamada `uDesktopMascot` dentro de `build`.
```
uDesktopMascot
└── build
    └── uDesktopMascot
        ├── uDesktopMascot.app
        └── README.txt
```

* Ejecute el siguiente comando.
```sh
cd build
productbuild --component uDesktopMascot/uDesktopMascot.app /Applications ./uDesktopMascot_mac_installer.pkg
```
* Una vez finalizada la construcción, se generará `uDesktopMascot_mac_installer.pkg` en la carpeta `build`.

## Créditos de los creadores
* Modelos: "Aozora" 
* BGM: MidraLab (eisuke)
* Icono del software: Yamucha

## Avisos de terceros

Consulte [NOTICE](./NOTICE.md).

## Patrocinador
- Luna
- uezo