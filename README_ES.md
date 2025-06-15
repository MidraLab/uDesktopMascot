# uDesktopMascot

[![Versión de Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Lanzamientos](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Unity Test CI](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)
[![Preguntar a DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/MidraLab/uDesktopMascot)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Nota**: Los idiomas mencionados arriba (English, 中文, Español, Français) han sido generados por traducción automática de GPT-4o-mini. Para la precisión y matices de la traducción, consulte el texto original (japonés).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Resumen](#resumen)
  * [Lista de características](#lista-de-características)
  * [Ejecución en macOS](#ejecución-en-macos)
  * [Requisitos](#requisitos)
  * [Licencia](#licencia)
  * [Sobre los materiales](#sobre-los-materiales)
  * [Cómo crear el instalador](#cómo-crear-el-instalador)
    * [Windows](#windows)
    * [macOS](#macos)
  * [Créditos de los creadores](#créditos-de-los-creadores)
  * [Avisos de terceros](#avisos-de-terceros)
  * [Patrocinador](#patrocinador)
<!-- TOC -->

## Resumen

"uDesktopMascot" es un proyecto de código abierto para una aplicación de mascotas de escritorio con el tema de `libertad creativa`. 
Como ejemplo de función, se pueden cargar modelos en formatos VRM y GLB/FBX y mostrarlos en el escritorio. También se pueden personalizar libremente los colores y las imágenes de fondo de la GUI, como la pantalla del menú y las ventanas de la aplicación.
Consulte la [Lista de características](#lista-de-características) para más detalles sobre las funciones.

![](Docs/Image/AppImage.png)

**Plataformas soportadas**
* Windows 10/11
* macOS

## Lista de características

La aplicación tiene implementadas las siguientes funciones. Consulte la lista a continuación para más detalles.

La adición de activos externos se puede lograr colocando archivos en la carpeta StreamingAssets.

<details>

<summary>Modelos y Animaciones</summary>

* Carga y muestra archivos de modelo ubicados en StreamingAssets.
  * Soporta modelos en formato VRM (1.x, 0.x).
  * Soporta modelos en formato GLB/GLTF (no soporta animación).
  * Soporta modelos en formato FBX (sin embargo, algunos modelos podrían no cargar texturas. Además, no soporta animación).
    * Las texturas se pueden cargar colocando archivos en StreamingAssets/textures/.
* Pantalla de selección y adición de modelos para agregar modelos VRM.
  * Agregar especificando la ruta.
  * Agregar desde un cuadro de diálogo para seleccionar archivos.

</details>

<details>

<summary>Voz y BGM</summary>

* Carga y reproduce archivos de voz ubicados en StreamingAssets/Voice/. Si hay varios, se reproducen aleatoriamente.
  * La voz que se reproduce al hacer clic se carga desde archivos de voz ubicados en StreamingAssets/Voice/Click/.
* Carga y reproduce archivos de música ubicados en StreamingAssets/BGM/. Si hay varios, se reproducen aleatoriamente.
* Adición de una voz predeterminada para el personaje.
  * La voz predeterminada utiliza el audio de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Se reproduce al iniciar la aplicación, al cerrarla y al hacer clic.

</details>

<details>

<summary>Reconocimiento de voz y chat AI</summary>

* Se integra un motor de reconocimiento de voz sin conexión [Vosk](https://alphacephei.com/vosk/) que convierte en texto la entrada del micrófono en tiempo real.
  * Se muestran los resultados intermedios `[STT][partial]` y los resultados finales `[STT][final]` en la consola de Unity.
  * Si hay un silencio que dura `VadSilenceSeconds` (predeterminado 1.0 segundo), el texto se confirma y se envía al LLM (`[STT][send]` log).
* Al confirmarse el texto, se pasa un mensaje de voz a la función de chat AI, que hace que el personaje lea la respuesta en voz alta.
  * Durante la generación de la respuesta, el micrófono se pausa automáticamente para evitar errores de reconocimiento.
* Puede alternar entre grabar y detener usando el botón del micrófono en la pantalla de ChatDialog.
* Las DLL nativas necesarias (`libvosk.dll`, `libstdc++-6.dll`, `libgcc_s_seh-1.dll`, `libwinpthread-1.dll`, etc.) se colocan en `Assets/Plugins/x86_64/` y se empaquetan automáticamente durante la construcción.
* Al colocar modelos de sonido (ej: `vosk-model-small-ja-0.22`) o modelos GGUF cuantificados en `StreamingAssets`, se pueden tratar múltiples idiomas como japonés/inglés sin conexión a la red.

</details>

<details>

<summary>Configuración de la aplicación mediante archivo de texto</summary>
La configuración de la aplicación se puede modificar mediante el archivo application_settings.txt.

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

* Se puede configurar la imagen de fondo y el color de fondo de la pantalla de menú.
  * La imagen de fondo se puede cargar desde archivos de imagen ubicados en StreamingAssets/Menu/. Los formatos de imagen compatibles son:
    * PNG
    * JPG(JPEG)
    * BMP
    * GIF (imagen fija)
    * TGA
    * TIFF
  * Se puede especificar un código de color para el color de fondo.
* Desde la pantalla de menú se puede acceder a las siguientes funciones:
  * Pantalla de selección y adición de modelos.
  * Función de chat AI.
  * Función LocalWeb.
  * Configuración de la aplicación.
  * Cierre de la aplicación.
* Al presionar el botón de minimizar en la pantalla de menú, la aplicación se puede minimizar en la bandeja del sistema (solo en Windows).
  * La aplicación minimizada se puede restaurar haciendo clic en el icono de la bandeja del sistema.

</details>

## Ejecución en macOS

Al ejecutar la aplicación en macOS, es posible que GateKeeper bloquee la aplicación.
En ese caso, ejecute el siguiente comando desde la terminal.

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

## Sobre los materiales
* La animación predeterminada del personaje se creó usando el [conjunto de datos de animación para "VRM Hina Dolls"](https://fumi2kick.booth.pm/items/1655686). Se ha confirmado la distribución incluida en el repositorio.
* La fuente utilizada es [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). Se redistribuye bajo la [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Los derechos de autor de la fuente pertenecen al autor original (Google).
* La voz predeterminada utiliza el audio de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). Se ha verificado previamente con COEIROINK sobre el uso.
* Los íconos de los botones utilizan [MingCute](https://github.com/MidraLab/MingCute).

## Cómo crear el instalador
### Windows
* Compilar en Unity en una carpeta llamada `uDesktopMascot` dentro de `build`.
```
uDesktopMascot
└── build
    └── uDesktopMascot
        ├── uDesktopMascot.exe
        └── README.txt
        ...
```

* Instalar [Inno Setup](https://www.jrsoftware.org/isdl.php).
  
* Al abrirlo, haga clic en `More files` y seleccione `setup.iss` dentro del proyecto.
  
![](Docs/Image/SetupIss-1.png)
* Una vez seleccionado, haga clic en el botón de reproducción.
  
![](Docs/Image/SetupIss-2.png)
* Una vez completada la construcción, se generará el instalador en la raíz del proyecto.

### macOS
El instalador solo se puede crear en una PC macOS.

* Compilar en Unity en una carpeta llamada `uDesktopMascot` dentro de `build`.
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
* Una vez completada la construcción, se generará `uDesktopMascot_mac_installer.pkg` en la carpeta `build`.

## Créditos de los creadores
* Modelos: "Aozora"
* BGM: MidraLab (eisuke)
* Icono de software: Yamucha

## Avisos de terceros

Consulte [NOTICE](./NOTICE.md).

## Patrocinador
- Luna
- uezo