# uDesktopMascot

[![Unity Version](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Releases](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)

Japanese | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Note**: The above languages (English, 中文, Español, Français) are generated through automatic translation by GPT-4o-mini. For accuracy and nuance of the translation, please refer to the original text (Japanese).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Overview](#overview)
  * [Feature List](#feature-list)
  * [Running on macOS](#running-on-macos)
  * [Requirements](#requirements)
  * [License](#license)
  * [About Materials](#about-materials)
  * [Creator Credits](#creator-credits)
  * [3rd Party Notices](#3rd-party-notices)
  * [Sponsor](#sponsor)
<!-- TOC -->

## Overview

"uDesktopMascot" is an open-source project that displays characters on the desktop and plays reactions or sounds based on user interaction. This project is developed using Unity and supports VRM format characters, allowing you to easily enjoy your favorite characters on your desktop.

**Supported Platforms**
* Windows 10/11
* macOS

## Feature List

The application implements the following features. Please refer to the list below for details.

You can add external assets by placing them in the StreamingAssets folder.

<details>

<summary>Models and Animations</summary>
* Displays any model file placed in StreamingAssets.
  * Supports VRM (1.x, 0.x) format models.
  * Supports GLB/GLTF format models. (Animations are not supported)
  * Supports FBX format models. (However, some models may not load textures, and animations are not supported)
    * Textures can be loaded by placing them in StreamingAssets/textures/.

</details>

<details>

<summary>Voice & BGM</summary>
* Loads and plays audio files placed under SteamingAssets/Voice/. If multiple files are available, it plays them randomly.
  * The audio played upon clicking is loaded from audio files placed in StreamingAssets/Voice/Click/.
* Loads and plays music files placed under SteamingAssets/BGM/. If multiple files are available, it plays them randomly.
* Addition of default character voices
  * The default voice uses audio from [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Played upon application launch, application exit, and clicks.

</details>

<details>

<summary>Application Settings via Text File</summary>
You can change the application settings using the application_settings.txt file.

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

* You can set the background image and background color for the menu screen.
  * The background image can be loaded from image files placed in StreamingAssets/Menu/. The supported image formats are:
    * PNG
    * JPG (JPEG)
    * BMP
    * GIF (still image)
    * TGA
    * TIFF
  * You can specify the background color using a color code.

</details>

## Running on macOS

When running the application on macOS, GateKeeper may block the app.
In such cases, please run the following command from the terminal:

```sh
xattr -r -c uDesktopMascot.app
```

## Requirements
* Unity 6000.0.31f1 (IL2CPP)

## License
* The code is licensed under the [Apache License 2.0](LICENSE).
* The following assets are licensed under [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/):
  * BGM
  * Models

## About Materials
* The default character animations are created using the [“Animation Data Set for VRM Doll Play”](https://fumi2kick.booth.pm/items/1655686). It has been confirmed that distribution along with the repository is permissible.
* The font is [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). The Noto Sans JP font is redistributed under the [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). The copyright of the font belongs to the original author (Google).
* The default voice uses audio from [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). The usage has been checked with COEIROINK in advance.
* The button icons use [MingCute](https://github.com/MidraLab/MingCute).

## Creator Credits
* Models: "Aozora"
* BGM: MidraLab (eisuke)
* Software icon: Yamucha

## 3rd Party Notices

See [NOTICE](./NOTICE.md).

## Sponsor
- Luna
- uezo