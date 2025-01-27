# uDesktopMascot

[![Unity 版本](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)  
[![发布版本](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)  
[![Unity 测试 CI](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)  

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)  

**注意**: 上述语言（English、中文、Español、Français）是由GPT-4o-mini自动翻译生成的。翻译的准确性和细微差别请参见原文（日本语）。  

<!-- TOC -->  
* [uDesktopMascot](#udesktopmascot)  
  * [概述](#概述)  
  * [功能列表](#功能列表)  
  * [在 macOS 上的运行](#在-macos-上的运行)  
  * [系统要求](#系统要求)  
  * [许可证](#许可证)  
  * [素材说明](#素材说明)  
  * [制作人员名单](#制作人员名单)  
  * [第三方通知](#第三方通知)  
  * [赞助商](#赞助商)  
<!-- TOC -->  

## 概述

“uDesktopMascot”是一个开放源码项目，可以在桌面上显示角色并根据用户的交互做出反应，播放声音。该项目使用Unity开发，并支持VRM格式的角色，使您可以轻松地在桌面上享受自己喜欢的角色。

**支持平台**  
* Windows 10/11  
* macOS  

## 功能列表  

应用程序实现了以下功能。详细信息如下列表所示。

通过将外部资产放置在StreamingAssets文件夹中，可以实现添加。

<details>

<summary>模型与动画</summary>
* 加载和显示放置于StreamingAssets的任意模型文件。
  * 支持VRM（1.x，0.x）格式的模型。
  * 支持GLB/GLTF格式的模型。（不支持动画）
  * 支持FBX格式的模型。（但某些模型可能无法加载纹理，且不支持动画）
    * 纹理可以通过放置在StreamingAssets/textures/中进行加载。

</details>

<details>

<summary>音效与背景音乐</summary>
* 加载和播放放置于SteamingAssets/Voice/下的音频文件。如果有多个，将随机播放。
  * 点击时播放的音频是从StreamingAssets/Voice/Click/中读取的音频文件。
* 加载和播放放置于SteamingAssets/BGM/下的音乐文件。如果有多个，将随机播放。
* 角色的默认声音添加
  * 默认声音使用[COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan)的音频。
  * 在应用程序启动时、应用程序结束时和点击时播放。

</details>

<details>

<summary>通过文本文件进行应用设置</summary>
可以通过application_settings.txt文件修改应用程序的设置。

设置文件的结构如下：

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

<summary>菜单画面</summary>

* 可以设置菜单画面的背景图像和背景颜色。
  * 背景图像可以加载放置于StreamingAssets/Menu/的图像文件。支持的图像格式如下：
    * PNG
    * JPG（JPEG）
    * BMP
    * GIF（静态图像）
    * TGA
    * TIFF
  * 背景颜色可以通过指定颜色代码来设置。

</details>

## 在 macOS 上的运行

在macOS上运行应用程序时，可能会由于GateKeeper阻止应用程序的运行。
在这种情况下，请通过终端执行以下命令：

```sh
xattr -r -c uDesktopMascot.app
```

## 系统要求
* Unity 6000.0.31f1(IL2CPP)

## 许可证
* 代码基于[Apache License 2.0](LICENSE)进行授权。
* 以下资产根据[CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/)进行授权：
  * BGM
  * 模型

## 素材说明
* 默认角色动画使用[“VRM人偶游戏”用动画数据打包](https://fumi2kick.booth.pm/items/1655686)制作。已确认可在仓库中包含并分发。
* 字体为[Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan)。根据[SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan)重新分发Noto Sans JP字体。字体的版权归原作者（Google）所有。
* 默认声音使用[COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan)的音频。使用方法已与COEIROINK确认。
* 按钮图标使用[MingCute](https://github.com/MidraLab/MingCute)。

## 制作人员名单
* 模型: “アオゾラ”先生
* 背景音乐: MidraLab(eisuke)
* 软件图标: やむちゃ先生

## 第三方通知

查看[NOTICE](./NOTICE.md)。

## 赞助商
- Luna
- uezo