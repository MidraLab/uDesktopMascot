# uDesktopMascot

[![Versión de Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Versiones](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Pruebas CI de Unity](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Nota**: Los idiomas mencionados anteriormente (English, 中文, Español, Français) fueron generados mediante traducción automática por GPT-4o-mini. Para la precisión y matices de la traducción, consulte el texto original (日本語).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Resumen](#resumen)
  * [Lista de características](#lista-de-características)
  * [Ejecutar en macOS](#ejecutar-en-macos)
  * [Requerimientos](#requerimientos)
  * [Licencia](#licencia)
  * [Sobre los素材](#sobre-los素材)
  * [Créditos de los creadores](#créditos-de-los-creadores)
  * [Avisos de terceros](#avisos-de-terceros)
  * [Patrocinador](#patrocinador)
<!-- TOC -->

## Resumen

"uDesktopMascot" es un proyecto de código abierto de una aplicación de mascota de escritorio cuyo tema es `la libertad de creación`.
Como una de sus funciones, permite cargar modelos en formatos VRM y GLB/FBX, y mostrarlos en el escritorio. Además, se pueden personalizar libremente los colores y las imágenes de fondo de la GUI, como las pantallas de menú y las ventanas de la aplicación.
Para una lista más detallada de las características, consulte la [lista de características](#lista-de-características).

![](Docs/Image/AppImage.png)

**Plataformas soportadas**
* Windows 10/11
* macOS

## Lista de características

La aplicación tiene las siguientes funciones implementadas. Consulte la lista a continuación para obtener más detalles.

La adición de activos externos se puede lograr ubicando los archivos en la carpeta StreamingAssets.

<details>

<summary>Modelos y Animaciones</summary>

* Carga y muestra archivos de modelos en la carpeta StreamingAssets.
  * Soporta modelos en formato VRM (1.x, 0.x).
  * Soporta modelos en formato GLB/GLTF. (Las animaciones no están soportadas)
  * Soporta modelos en formato FBX. (Sin embargo, algunos modelos pueden no cargar texturas. Además, las animaciones no están soportadas)
    * Las texturas se pueden cargar ubicándolas en StreamingAssets/textures/.

</details>

<details>

<summary>Voz y BGM</summary>

* Carga y reproduce archivos de voz ubicados en StreamingAssets/Voice/. Si hay varios, se reproducen aleatoriamente.
  * Los sonidos que se reproducen al hacer clic se cargan desde los archivos de voz ubicados en StreamingAssets/Voice/Click/.
* Carga y reproduce archivos de música ubicados en StreamingAssets/BGM/. Si hay varios, se reproducen aleatoriamente.
* Adición de la voz predeterminada del personaje
  * La voz predeterminada utiliza el audio de [COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Se reproduce al iniciar la aplicación, al cerrarla y al hacer clic.

</details>

<details>

<summary>Configuración de la aplicación mediante archivo de texto</summary>
El archivo application_settings.txt permite modificar la configuración de la aplicación.

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

* Se puede configurar la imagen de fondo y el color del menú.
  * La imagen de fondo se puede cargar desde archivos de imagen ubicados en StreamingAssets/Menu/. Los formatos de imagen admitidos son:
    * PNG
    * JPG (JPEG)
    * BMP
    * GIF (imágenes fijas)
    * TGA
    * TIFF
  * Se puede especificar un código de color para el color de fondo.

</details>

## Ejecutar en macOS

Al ejecutar la aplicación en macOS, puede que GateKeeper bloquee la aplicación.
En ese caso, ejecute el siguiente comando desde la terminal.

```sh
xattr -r -c uDesktopMascot.app
```

## Requerimientos
* Unity 6000.0.31f1 (IL2CPP)

## Licencia
* El código está licenciado bajo la [Licencia Apache 2.0](LICENSE).
* Los siguientes activos están licenciados bajo [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/).
  * BGM
  * Modelos

## Sobre los素材
* La animación predeterminada del personaje se creó utilizando el [“Paquete de datos de animación para la serie VRM”](https://fumi2kick.booth.pm/items/1655686). Se ha confirmado que se distribuye dentro del repositorio.
* La fuente utilizada es [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). Se redistribuye bajo la [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Los derechos de autor de la fuente pertenecen al autor original (Google).
* La voz predeterminada utiliza audio de [COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan). Se ha confirmado que el uso ha sido autorizado por COEIROINK.
* El icono del botón utiliza [MingCute](https://github.com/MidraLab/MingCute).

## Métodos para crear un instalador
### Windows
* Compilar en Unity en una carpeta llamada `uDesktopMascot` bajo el directorio `build`.

* Instalar [Inno Setup](https://www.jrsoftware.org/isdl.php).
  
* Una vez abierto, haga clic en `More files` y seleccione el archivo `setup.iss` que se encuentra en la carpeta del proyecto.
  
![](Docs/Image/SetupIss-1.png)
* Después de seleccionar, haga clic en el botón de reproducción.
  
![](Docs/Image/SetupIss-2.png)
* Una vez que se complete la compilación, se generará el instalador en la raíz del proyecto.

### macOS
Solo se puede crear un instalador en una PC con macOS.

* Compilar en Unity en una carpeta llamada `uDesktopMascot` bajo el directorio `build`.

* Ejecute el siguiente comando.
```sh
cd build
productbuild --component uDesktopMascot/uDesktopMascot.app /Applications ./uDesktopMascot_mac_installer.pkg
```
* Una vez que se complete la compilación, se generará `uDesktopMascot_mac_installer.pkg` en la carpeta `build`.

## Créditos de los creadores
* Modelos: 「アオゾラ」様
* BGM: MidraLab(eisuke)
* Icono de software: やむちゃ様

## Avisos de terceros

Vea [NOTICE](./NOTICE.md).

## Patrocinador
- Luna
- uezo