# uDesktopMascot

[![Versión de Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Lanzamientos](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Pruebas de CI de Unity](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)
[![Preguntar a DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/MidraLab/uDesktopMascot)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Nota**: Los idiomas anteriores (English, 中文, Español, Français) han sido generados mediante traducción automática por GPT-4o-mini. Para mayor precisión y matices, consulte el texto original (日本語).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Resumen](#resumen)
  * [Lista de funciones](#lista-de-funciones)
  * [Ejecución en macOS](#ejecución-en-macos)
  * [Requisitos](#requisitos)
  * [Licencia](#licencia)
  * [Materiales](#materiales)
  * [Método para crear instaladores](#método-para-crear-instaladores)
    * [Windows](#windows)
    * [macOS](#macos)
  * [Créditos de los creadores](#créditos-de-los-creadores)
  * [Avisos de terceros](#avisos-de-terceros)
  * [Patrocinador](#patrocinador)
<!-- TOC -->

## Resumen

"uDesktopMascot" es un proyecto de código abierto de una aplicación de mascota de escritorio con el tema de `liberar la creatividad`.
Como ejemplo de una de las funciones, puede cargar modelos en formatos VRM o GLB/FBX y mostrarlos en el escritorio. También puede configurar libremente los colores y las imágenes de fondo de la GUI, como la pantalla del menú y las ventanas de la aplicación.
Para una lista detallada de funciones, consulte [Lista de funciones](#lista-de-funciones).

![](Docs/Image/AppImage.png)

**Plataformas soportadas**
* Windows 10/11
* macOS

## Lista de funciones

La aplicación cuenta con las siguientes funcionalidades. Consulte la lista a continuación para más detalles.

La adición de activos externos se puede lograr colocando archivos en la carpeta StreamingAssets.

<details>

<summary>Modelos y animaciones</summary>

* Carga y visualización de archivos de modelo ubicados en StreamingAssets.
  * Soporta modelos en formato VRM (1.x, 0.x).
  * Soporta modelos en formato GLB/GLTF. (Las animaciones no están soportadas)
  * Soporta modelos en formato FBX. (Sin embargo, en algunos modelos, las texturas pueden no cargarse. Además, las animaciones no están soportadas)
    * Las texturas pueden ser cargadas al ubicarlas en StreamingAssets/textures/.
* Pantalla para seleccionar y añadir modelos VRM
  * Añadir especificando la ruta
  * Añadir desde un cuadro de diálogo de selección de archivos

</details>

<details>

<summary>Voz y BGM</summary>

* Carga y reproduce archivos de audio ubicados en StreamingAssets/Voice/. Si hay varios, se reproducirán aleatoriamente.
  * Los sonidos que se reproducen al hacer clic se cargan y reproducen desde los archivos de audio ubicados en StreamingAssets/Voice/Click/. 
* Carga y reproduce archivos de música ubicados en StreamingAssets/BGM/. Si hay varios, se reproducirán aleatoriamente.
* Adición de voz predeterminada del personaje
  * La voz predeterminada utiliza el audio de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Se reproduce al iniciar la aplicación, al cerrar la aplicación y al hacer clic.

</details>

<details>

<summary>Configuración de la aplicación a través de archivos de texto</summary>
Puede cambiar la configuración de la aplicación usando el archivo application_settings.txt.

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
  * La imagen de fondo se puede cargar desde archivos de imagen ubicados en StreamingAssets/Menu/. Los formatos de imagen soportados son:
    * PNG
    * JPG (JPEG)
    * BMP
    * GIF (imagen fija)
    * TGA
    * TIFF
  * Se puede especificar un código de color para el color de fondo.
* Desde la pantalla del menú se puede acceder a las siguientes funciones:
  * Pantalla de selección y adición de modelos
  * Función de chat de IA
  * Función LocalWeb
  * Configuración de la aplicación
  * Salir de la aplicación
* Al presionar el botón de minimizar en la pantalla del menú, la aplicación puede ser minimizada en la bandeja del sistema en Windows.
  * La aplicación minimizada se puede mostrar nuevamente haciendo clic en el icono de la bandeja del sistema.

</details>

## Ejecución en macOS

Al ejecutar la aplicación en macOS, puede que GateKeeper bloquee la aplicación.
En este caso, ejecute el siguiente comando desde la terminal.

```sh
xattr -r -c uDesktopMascot.app
```

## Requisitos
* Unity 6000.1.1f1 (IL2CPP)

## Licencia
* El código está licenciado bajo la [Licencia Apache 2.0](LICENSE).
* Los siguientes activos están licenciados bajo [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/).
  * BGM
  * Modelos

## Materiales
* La animación predeterminada del personaje se crea usando los [datos de animación de "Juguetes de muñeca VRM"](https://fumi2kick.booth.pm/items/1655686). Se ha confirmado la distribución de este material dentro del repositorio.
* La fuente utilizada es [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). Se redistribuye bajo la [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Los derechos de autor de la fuente pertenecen al autor original (Google).
* La voz predeterminada se utiliza de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). Se ha confirmado el uso con COEIROINK previamente.
* Los iconos de los botones se crean utilizando [MingCute](https://github.com/MidraLab/MingCute).

## Método para crear instaladores
### Windows
* En Unity, construya en una carpeta llamada `build` con el nombre `uDesktopMascot`.
```
uDesktopMasscot
└── build
    └── uDesktopMasscot
        ├── uDesktopMascot.exe
        └── README.txt
        ...
```

* Instale [Inno Setup](https://www.jrsoftware.org/isdl.php).
  
* Al abrirlo, haga clic en `More files` y seleccione `setup.iss` en la carpeta del proyecto.
  
![](Docs/Image/SetupIss-1.png)
* Una vez seleccionado, haga clic en el botón de reproducir.
  
![](Docs/Image/SetupIss-2.png)
* Una vez completada la construcción, se generará el instalador en la raíz del proyecto.

### macOS
Solo se puede crear un instalador en una PC macOS.

* Construya en Unity en una carpeta `build/uDesktopMascot` con el nombre `uDesktopMascot`.
```
uDesktopMasscot
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
* Una vez completada la construcción, se generará `uDesktopMascot_mac_installer.pkg` en la carpeta `build`.

## Créditos de los creadores
* Modelos: "Aozora"
* BGM: MidraLab (eisuke)
* Icono de software: Yamucha

## Avisos de terceros

Vea [NOTICE](./NOTICE.md).

## Patrocinador
- Luna
- uezo