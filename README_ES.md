# uDesktopMascot

[![Versión de Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Lanzamientos](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Pruebas de Unity CI](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Nota**: Los idiomas anteriores (English, 中文, Español, Français) han sido generados mediante traducción automática por GPT-4o-mini. Para la precisión y matices de la traducción, consulte el texto original (日本語).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Resumen](#resumen)
  * [Lista de funciones](#lista-de-funciones)
  * [Ejecución en macOS](#ejecución-en-macos)
  * [requisitos](#requisitos)
  * [licencia](#licencia)
  * [Acerca de los materiales](#acerca-de-los-materiales)
  * [Cómo crear un instalador](#cómo-crear-un-instalador)
    * [Windows](#windows)
    * [macOS](#macos)
  * [Créditos de los creadores](#créditos-de-los-creadores)
  * [Avisos de terceros](#avisos-de-terceros)
  * [patrocinio](#patrocinio)
<!-- TOC -->

## Resumen

"uDesktopMascot" es un proyecto de código abierto de una aplicación de mascota de escritorio que tiene como tema la `liberación de la creación`.
Como ejemplo de funcionalidad, puede cargar modelos en formatos VRM y GLB/FBX y mostrarlos en el escritorio. También puede establecer libremente los colores y las imágenes de fondo de la GUI, como la pantalla del menú y la ventana de la aplicación.
Consulte la [Lista de funciones](#lista-de-funciones) para más detalles.

![](Docs/Image/AppImage.png)

**Plataformas compatibles**
* Windows 10/11
* macOS

## Lista de funciones

La aplicación tiene implementadas las siguientes funciones. Consulte la lista a continuación para más detalles.

La adición de activos externos se puede lograr colocando los archivos en la carpeta StreamingAssets.

<details>

<summary>Modelos y animaciones</summary>

* Cargar y mostrar archivos de modelo de su elección que se encuentren en StreamingAssets.
  * Soporta modelos en formato VRM (1.x, 0.x).
  * Soporta modelos en formato GLB/GLTF. (No soporta animaciones)
  * Soporta modelos en formato FBX. (Sin embargo, algunos modelos pueden tener problemas para cargar texturas. Además, no soporta animaciones).
    * Las texturas se pueden cargar colocándolas en StreamingAssets/textures/.
* Desde la pantalla de añadir modelos, puede agregar modelos de personajes en VRM.
  * Agregar ruta de VRM
  * Seleccionar un archivo desde el diálogo de selección de archivos

</details>

<details>

<summary>Voz y BGM</summary>

* Cargar y reproducir archivos de audio colocados en StreamingAssets/Voice/. Si hay varios, se reproducirán de forma aleatoria.
  * Los sonidos que se reproducen al hacer clic se cargan desde los archivos de audio colocados en StreamingAssets/Voice/Click/. 
* Cargar y reproducir archivos de música colocados en StreamingAssets/BGM/. Si hay varios, se reproducirán de forma aleatoria.
* Añadir la voz predeterminada del personaje.
  * La voz predeterminada utiliza el audio de [COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Se reproduce al iniciar la aplicación, al cerrar la aplicación y al hacer clic.

</details>

<details>

<summary>Configuración de la aplicación mediante archivo de texto</summary>
Puede cambiar la configuración de la aplicación a través del archivo application_settings.txt.

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
  * La imagen de fondo se puede cargar desde archivos de imagen colocados en StreamingAssets/Menu/. Los formatos de imagen compatibles son:
    * PNG
    * JPG (JPEG)
    * BMP
    * GIF (imagen estática)
    * TGA
    * TIFF
  * El color de fondo se puede especificar a través de un código de color.

Las siguientes funciones están disponibles desde el menú:
* Agregar o cambiar modelos
* Cambiar la configuración de la aplicación
* Salir de la aplicación
* (Solo en Windows) Minimizar la aplicación a la bandeja: puede minimizar la aplicación.
  * Al minimizar la aplicación a la bandeja, el ícono de la aplicación se mostrará en la bandeja de notificaciones en lugar de en el escritorio.
  * Al hacer clic en el ícono en la bandeja de notificaciones, podrá mostrar la aplicación en el escritorio.
* Función de chat con IA: puede utilizar la función de chat con IA.
  * Soporte para modelo local utilizando el modelo Qwen2

</details>

## Ejecución en macOS

Al ejecutar la aplicación en macOS, a veces puede ser bloqueada por GateKeeper.
En ese caso, ejecute el siguiente comando desde la terminal:

```sh
xattr -r -c uDesktopMascot.app
```

## requisitos
* Unity 6000.0.31f1 (IL2CPP)

## licencia
* El código está licenciado bajo la [Licencia Apache 2.0](LICENSE).
* Los siguientes activos están licenciados bajo [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/).
  * BGM
  * Modelos

## Acerca de los materiales
* La animación de personaje predeterminada se ha creado utilizando [“Paquete de datos de animación para VRM muñeca juguete”](https://fumi2kick.booth.pm/items/1655686). Se ha confirmado que se puede redistribuir en el repositorio.
* La fuente utilizada es [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). Se redistribuye bajo la [Licencia SIL OPEN FONT Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Los derechos de autor de la fuente pertenecen al autor original (Google).
* La voz predeterminada utiliza el audio de [COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan). Se ha confirmado el uso previo con COEIROINK.
* Los íconos de los botones utilizan [MingCute](https://github.com/MidraLab/MingCute).

## Cómo crear un instalador
### Windows
* Realice la construcción en Unity en una carpeta llamada `build` para `uDesktopMascot`.

* Instale [Inno Setup](https://www.jrsoftware.org/isdl.php).
  
* Al abrirlo, haga clic en `More files` y seleccione el archivo `setup.iss` que se encuentra en el proyecto.
  
![](Docs/Image/SetupIss-1.png)
* Después de seleccionarlo, haga clic en el botón de reproducción.
  
![](Docs/Image/SetupIss-2.png)
* Una vez que la construcción esté completa, se generará el instalador en la raíz del proyecto.

### macOS
Solo se puede crear un instalador en una PC con macOS.

* Realice la construcción en Unity en una carpeta llamada `build` para `uDesktopMascot`.

* Ejecute el siguiente comando:
```sh
cd build
productbuild --component uDesktopMascot/uDesktopMascot.app /Applications ./uDesktopMascot_mac_installer.pkg
```
* Una vez que la construcción esté completa, se generará `uDesktopMascot_mac_installer.pkg` en la carpeta `build`.

## Créditos de los creadores
* Modelos: "アオゾラ" (Aozora)
* BGM: MidraLab (eisuke)
* Icono del software: やむちゃ (Yamucha)

## Avisos de terceros

Consulte [NOTICE](./NOTICE.md).

## patrocinio
- Luna
- uezo