# uDesktopMascot

[![Unity 版本](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![版本发布](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)

日本语 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**注意**: 上述语言（English、中文、Español、Français）是由GPT-4o-mini自动翻译生成的。关于翻译的准确性和细微差别，请参阅原文（日本语）。

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [概述](#概述)
  * [功能列表](#功能列表)
  * [在macOS上运行](#在macos上运行)
  * [要求](#要求)
  * [许可证](#许可证)
  * [素材说明](#素材说明)
  * [制作人鸣谢](#制作人鸣谢)
  * [第三方声明](#第三方声明)
  * [赞助商](#赞助商)
<!-- TOC -->

## 概述

「uDesktopMascot」是一个开放源代码项目，能够在桌面上显示角色，并根据用户的互动进行反应和播放声音。该项目使用Unity进行开发，并支持VRM格式的角色，因此用户可以轻松地在桌面上享受自己喜欢的角色。

**支持的平台**
* Windows 10/11
* macOS

## 功能列表

该应用程序实现了以下功能。详情请参阅以下列表。

通过将外部资产放置于StreamingAssets文件夹中来实现添加。

<details>

<summary>模型・动画</summary>
* 读取并显示放置在StreamingAssets中的任意模型文件。
  * 支持VRM(1.x, 0.x)格式的模型。
  * 支持GLB/GLTF格式的模型。（动画不支持）
  * 支持FBX格式的模型。（但是某些模型无法加载纹理，动画也不支持）
    * 纹理可以通过放置在StreamingAssets/textures/中进行加载。

</details>

<details>

<summary>语音・BGM</summary>
* 读取并播放放置在SteamingAssets/Voice/中的音频文件。如果有多个，则随机播放。
  * 点击时播放的音频是从StreamingAssets/Voice/Click/中读取的。
* 读取并播放放置在SteamingAssets/BGM/中的音乐文件。如果有多个，则随机播放。
* 添加角色的默认语音
  * 默认语音使用[COEIROINK:つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan)的声音。
  * 在应用启动时、应用结束时、点击时播放。

</details>

<details>

<summary>通过文本文件进行应用程序设置</summary>
可以通过application_settings.txt文件修改应用程序的设置。

设置文件的结构如下所示：

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

## 在macOS上运行

在macOS上运行应用程序时，GateKeeper可能会阻止应用程序。
在这种情况下，请从终端执行以下命令。

```sh
xattr -r -c uDesktopMascot.app
```

## 要求
* Unity 6000.0.31f1(IL2CPP)

## 许可证
* 代码基于[Apache License 2.0](LICENSE)进行许可。
* 以下资产基于[CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/)进行许可。
  * BGM
  * 模型

## 素材说明
* 默认的角色动画使用[『VRMお人形遊び』用动画数据合集](https://fumi2kick.booth.pm/items/1655686)制作。已确认可以分发时包含在仓库中。
* 字体是[Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan)。根据[SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan)再分发Noto Sans JP字体。字体的版权属于原作者（Google）。
* 默认语音使用[COEIROINK:つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan)的声音。使用方法已提前向COEIROINK确认。
* 按钮图标使用[MingCute](https://github.com/MidraLab/MingCute)。

## 制作人鸣谢
* 模型: 「アオゾラ」小姐
* BGM: MidraLab(eisuke)
* 软件图标: やむちゃ小姐

## 第三方声明

请参见[NOTICE](./NOTICE.md)。

## 赞助商
- Luna
- uezo