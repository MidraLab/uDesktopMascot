# uDesktopMascot

[![Unity Version](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Releases](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Unity Test CI](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**注意**: 上述语言（English、中文、Español、Français）是通过GPT-4o-mini自动翻译生成的。关于翻译的准确性和细微差别，请参考原文（日本語）。

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [概要](#概要)
  * [功能一覧](#功能一覧)
  * [在macOS上的运行](#在macos上的运行)
  * [要求](#要求)
  * [许可证](#许可证)
  * [素材说明](#素材说明)
  * [制作人员名单](#制作人员名单)
  * [第三方声明](#第三方声明)
  * [赞助](#赞助)
<!-- TOC -->

## 概要

「uDesktopMascot」是一个开源项目，可以在桌面上显示角色，并根据用户的互动做出反应和播放声音。该项目使用Unity开发，支持VRM格式的角色，用户可以轻松在桌面上享受自己喜欢的角色。

![](Docs/Image/AppImage.png)

**支持平台**
* Windows 10/11
* macOS

## 功能一覧

应用程序实现了以下功能。请参见以下列表以获取详细信息。

通过将外部资产放置在StreamingAssets文件夹中，可以实现功能扩展。

<details>

<summary>模型・动画</summary>

* 可以加载并显示放置在StreamingAssets中的任意模型文件。
  * 支持VRM（1.x，0.x）格式的模型。
  * 支持GLB/GLTF格式的模型。（不支持动画）
  * 支持FBX格式的模型。（但某些模型的纹理不能加载，且不支持动画）
    * 纹理可通过放置在StreamingAssets/textures/中来加载。

</details>

<details>

<summary>语音・背景音乐</summary>

* 可以加载并播放放置在SteamingAssets/Voice/中的音频文件。如果有多个文件，将随机播放。
  * 单击时播放的音频从StreamingAssets/Voice/Click/中的音频文件加载。
* 可以加载并播放放置在SteamingAssets/BGM/中的音乐文件。如果有多个文件，将随机播放。
* 角色的默认语音添加
  * 默认语音使用[COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan)的音频。
  * 应用启动时、应用退出时、单击时播放。

</details>

<details>

<summary>通过文本文件设置应用程序</summary>
可以通过application_settings.txt文件更改应用程序的设置。

设置文件的结构如下所示

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

<summary>菜单界面</summary>

* 可以设置菜单界面的背景图像及背景色。
  * 背景图像可以加载放置在StreamingAssets/Menu/中的图像文件。支持的图像格式如下：
    * PNG
    * JPG（JPEG）
    * BMP
    * GIF（静止图像）
    * TGA
    * TIFF
  * 背景色可指定颜色代码。

</details>

## 在macOS上的运行

在macOS上运行应用程序时，可能会被GateKeeper阻止。
在这种情况下，请从终端执行以下命令。

```sh
xattr -r -c uDesktopMascot.app
```

## 要求
* Unity 6000.0.31f1（IL2CPP）

## 许可证
* 代码根据[Apache License 2.0](LICENSE)授权。
* 以下资产根据[CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/)授权：
  * 背景音乐
  * 模型

## 素材说明
* 默认的角色动画是使用[『VRMお人形遊び』用动画数据包](https://fumi2kick.booth.pm/items/1655686)制作。分发至仓库的内容已经得到确认。
* 字体使用[Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan)，并根据[SIL开放字体许可 版本 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan)进行再分发。字体的版权归原作者（Google）所有。
* 默认语音使用[COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan)的音频，使用方法已提前与COEIROINK确认。
* 按钮图标使用[MingCute](https://github.com/MidraLab/MingCute)。

## 制作人员名单
* 模型: 「アオゾラ」 
* 背景音乐: MidraLab（eisuke）
* 软件图标: やむちゃ

## 第三方声明

请参见[NOTICE](./NOTICE.md)。

## 赞助
- Luna
- uezo