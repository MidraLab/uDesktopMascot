# uDesktopMascot

[![Unity Version](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Releases](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Unity Test CI](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

Japanese | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Note**: The languages provided above (English, 中文, Español, Français) were generated through automatic translation by GPT-4o-mini. For accuracy and nuance in translation, please refer to the original text (Japanese).

<!-- TOC -->
- [uDesktopMascot](#udesktopmascot)
  - [Overview](#overview)
  - [Features List](#features-list)
  - [Running on macOS](#running-on-macos)
  - [Requirements](#requirements)
  - [License](#license)
  - [Resources](#resources)
  - [How to Create an Installer](#how-to-create-an-installer)
    - [Windows](#windows)
    - [macOS](#macos)
  - [Creator Credits](#creator-credits)
  - [3rd Party Notices](#3rd-party-notices)
  - [Sponsor](#sponsor)
<!-- TOC -->

## Overview

"uDesktopMascot" is an open-source project for a desktop mascot application themed around "liberation of creativity." As an example of its functionality, it can load models in VRM or GLB/FBX formats and display them on the desktop. You can also freely set the colors and background images of GUI elements like menus and application windows. For a detailed list of features, please refer to the [Features List](#features-list).

![](Docs/Image/AppImage.png)

**Supported Platforms**
* Windows 10/11
* macOS

## Features List

The application has the following features implemented. For details, please see the list below.

External assets can be added by placing them in the `StreamingAssets` folder.

<details>

<summary>Models and Animations</summary>

* Displays any model file placed in the StreamingAssets.
  * Supports VRM (1.x, 0.x) format models.
  * Supports GLB/GLTF format models (animations are not supported).
  * Supports FBX format models (however, some models may not load textures, and animations are not supported).
    * Textures can be loaded by placing them in StreamingAssets/textures/.
* Adds VRM models from model selection/addition screen.
  * Add by specifying a path.
  * Add through the file selection dialog.

</details>

<details>

<summary>Voice and BGM</summary>

* Loads and plays audio files placed in SteamingAssets/Voice/. If there are multiple files, they are played randomly.
  * Audio that plays on click is loaded from audio files placed in StreamingAssets/Voice/Click/. 
* Loads and plays music files placed in SteamingAssets/BGM/. If there are multiple files, they are played randomly.
* Adds default voice for the character.
  * The default voice uses the audio from [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Played at application launch, application exit, and on click.

</details>

<details>

<summary>Application Settings via Text File</summary>

You can change application settings via the application_settings.txt file.

The structure of the settings file is as follows:

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

<summary>Menu Screen</summary>

* You can set the background image and background color of the menu screen.
  * The background image can be loaded from image files placed in StreamingAssets/Menu/. The supported image formats are:
    * PNG
    * JPG (JPEG)
    * BMP
    * GIF (static image)
    * TGA
    * TIFF
  * The background color can be specified with a color code.
* Access the following functions from the menu screen:
  * Model selection and addition screen
  * AI chat feature
  * LocalWeb feature
  * Application settings
  * Exit application
* By pressing the minimize button on the menu screen, the application can be minimized to the notification area (Windows only).
  * Minimized applications can be restored by clicking the notification area icon.

</details>

## Running on macOS

When running the application on macOS, the application may be blocked by GateKeeper.
In that case, please run the following command from the terminal:

```sh
xattr -r -c uDesktopMascot.app
```

## requirements
* Unity 6000.1.1f1(IL2CPP)

## License
* The code is licensed under the [Apache License 2.0](LICENSE).
* The following assets are licensed under [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/):
  * BGM
  * Models

## Resources
* The default character animation is created using the [“VRM Doll Play” animation data pack](https://fumi2kick.booth.pm/items/1655686). It has been confirmed that distribution in the repository is allowed.
* The font used is [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). The Noto Sans JP font is redistributed under the [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). The copyright of the font belongs to the original author (Google).
* The default voice uses the audio from [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). Confirmation has been made with COEIROINK regarding its usage.
* Button icons are sourced from [MingCute](https://github.com/MidraLab/MingCute).

## How to Create an Installer
### Windows
* Build in Unity to create a folder named `uDesktopMascot` within the `build` folder.
```
uDesktopMascot
└── build
    └── uDesktopMascot
        ├── uDesktopMascot.exe
        └── README.txt
        ...
```

* Install [Inno Setup](https://www.jrsoftware.org/isdl.php).

* Once opened, click `More files` and select `setup.iss` found within the project directory.

![](Docs/Image/SetupIss-1.png)
* After selection, click the play button.

![](Docs/Image/SetupIss-2.png)
* Once the build is complete, the installer will be generated in the project root.

### macOS
The installer can only be created on a macOS PC.

* Build in Unity to create a folder named `uDesktopMascot` within the `build/uDesktopMascot` folder.
```
uDesktopMascot
└── build
    └── uDesktopMascot
        ├── uDesktopMascot.app
        └── README.txt
```

* Execute the following command:
```sh
cd build
productbuild --component uDesktopMascot/uDesktopMascot.app /Applications ./uDesktopMascot_mac_installer.pkg
```
* Once the build is complete, the `uDesktopMascot_mac_installer.pkg` will be generated in the `build` folder.

## Creator Credits
* Model: "Aozora" 
* BGM: MidraLab (eisuke)
* Software Icon: Yamucha

## 3rd Party Notices

See [NOTICE](./NOTICE.md).

## Sponsor
- Luna
- uezo