# uDesktopMascot

[![Versión de Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Lanzamientos](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Pruebas CI de Unity](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Nota**: Los idiomas mencionados anteriormente (English, 中文, Español, Français) han sido generados mediante traducción automática con GPT-4o-mini. Para precisión y matices, consulte el texto original (日本語).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Resumen](#resumen)
  * [Lista de funciones](#lista-de-funciones)
  * [Ejecución en macOS](#ejecución-en-macos)
  * [Requisitos](#requisitos)
  * [Licencia](#licencia)
  * [Acerca de los materiales](#acerca-de-los-materiales)
  * [Cómo crear un instalador](#cómo-crear-un-instalador)
    * [Windows](#windows)
    * [macOS](#macos)
  * [Créditos de los creadores](#créditos-de-los-creadores)
  * [Avisos de terceros](#avisos-de-terceros)
  * [Patrocinador](#patrocinador)
<!-- TOC -->

## Resumen

“uDesktopMascot” es un proyecto de código abierto de aplicación de mascota de escritorio con el tema de `libertad creativa`. Como ejemplo de funcionalidad, puede cargar modelos en formato VRM o GLB/FBX y mostrarlos en el escritorio. También puede configurar libremente los colores de la GUI y las imágenes de fondo de las pantallas de menú y ventanas de la aplicación.
Consulte la [Lista de funciones](#lista-de-funciones) para obtener más detalles.

![](Docs/Image/AppImage.png)

**Plataformas compatibles**
* Windows 10/11
* macOS

## Lista de funciones

La aplicación tiene las siguientes funciones implementadas. Consulte la lista a continuación para más detalles.

La adición de activos externos se puede lograr colocando archivos en la carpeta StreamingAssets.

<details>

<summary>Modelos y animaciones</summary>

* Carga y muestra archivos de modelo en cualquier formato colocado en StreamingAssets.
  * Soporta modelos en formato VRM (1.x, 0.x).
  * Soporta modelos en formato GLB/GLTF. (Las animaciones no son compatibles)
  * Soporta modelos en formato FBX. (Sin embargo, algunos modelos pueden no cargar las texturas. Además, las animaciones no son compatibles)
    * Las texturas se pueden cargar colocándolas en StreamingAssets/textures/.
* Agregar modelos VRM desde la pantalla de selección y adición de modelos.
  * Agregar especificando la ruta.
  * Agregar desde el cuadro de diálogo de selección de archivos.

</details>

<details>

<summary>Voz y BGM</summary>

* Carga y reproduce archivos de audio colocados en StreamingAssets/Voice/. Si hay varios, se reproducen de forma aleatoria.
  * El audio que se reproduce al hacer clic se carga y reproduce desde los archivos de audio colocados en StreamingAssets/Voice/Click/. 
* Carga y reproduce archivos de música colocados en StreamingAssets/BGM/. Si hay varios, se reproducen de forma aleatoria.
* Adición de la voz predeterminada del personaje.
  * La voz predeterminada utiliza el audio de [COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Se reproduce al iniciar la aplicación, al cerrar la aplicación y al hacer clic.

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
  * La imagen de fondo se puede cargar desde un archivo de imagen colocado en StreamingAssets/Menu/. Los formatos de imagen compatibles son:
    * PNG
    * JPG (JPEG)
    * BMP
    * GIF (imagen estática)
    * TGA
    * TIFF
  * El color de fondo se puede especificar utilizando un código de color.
* Desde la pantalla de menú se puede acceder a las siguientes funciones:
  * Pantalla de selección y adición de modelos
  * Función de chat AI
  * Función LocalWeb
  * Configuración de la aplicación
  * Cerrar la aplicación
* Al presionar el botón de minimización en la pantalla de menú, puede minimizar la aplicación en el área de notificaciones solo en Windows.
  * La aplicación minimizada se puede volver a mostrar haciendo clic en el icono del área de notificaciones.

</details>

## Ejecución en macOS

Al ejecutar la aplicación en macOS, es posible que GateKeeper bloquee la aplicación. En ese caso, ejecute el siguiente comando desde la terminal:

```sh
xattr -r -c uDesktopMascot.app
```

## Requisitos
* Unity 6000.1.1f1 (IL2CPP)

## Licencia
* El código está licenciado bajo la [Licencia Apache 2.0](LICENSE).
* Los siguientes activos están bajo la licencia [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/).
  * BGM
  * Modelos

## Acerca de los materiales
* La animación predeterminada del personaje se crea utilizando [「VRMお人形遊び」用アニメーションデータ詰め合わせ](https://fumi2kick.booth.pm/items/1655686). Se ha confirmado la posibilidad de distribuirlo junto con este repositorio.
* La fuente utilizada es [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). Se redistribuye bajo la [SIL OPEN FONT LICENSE Versión 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Los derechos de autor de la fuente pertenecen al autor original (Google).
* La voz predeterminada utiliza el audio de [COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan). Se ha confirmado el uso con COEIROINK de antemano.
* Los iconos de los botones utilizan [MingCute](https://github.com/MidraLab/MingCute).

## Cómo crear un instalador
### Windows
* En Unity, construya en una carpeta llamada `uDesktopMascot` bajo `build`.
```
uDesktopMascot
└── build
    └── uDesktopMascot
        ├── uDesktopMascot.exe
        └── README.txt
        ...
```

* Instale [Inno Setup](https://www.jrsoftware.org/isdl.php).
  
* Al abrirlo, haga clic en `More files` y seleccione el archivo `setup.iss` que está en la carpeta del proyecto.
  
![](Docs/Image/SetupIss-1.png)
* Después de seleccionar, haga clic en el botón de reproducción.
  
![](Docs/Image/SetupIss-2.png)
* Cuando la construcción haya finalizado, se generará el instalador en la raíz del proyecto.

### macOS
Solo puede crear un instalador en una computadora macOS.

* En Unity, construya en la carpeta `build/uDesktopMascot` con el nombre `uDesktopMascot`.
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
* Cuando la construcción haya finalizado, se generará `uDesktopMascot_mac_installer.pkg` en la carpeta `build`.

## Créditos de los creadores
* Modelo: 「アオゾラ」様
* BGM: MidraLab(eisuke)
* Icono del software: やむちゃ様

## Avisos de terceros

Consulte [NOTICE](./NOTICE.md).

## Patrocinador
- Luna
- uezo