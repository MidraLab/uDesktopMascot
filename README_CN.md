# uDesktopMascot

[![Unity 版本](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![版本发布](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)

日本语 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**注意**: 上述语言（English、中文、Español、Français）是由 GPT-4o-mini 自动翻译生成的。有关翻译的准确性及细微差别，请参阅原文（日本语）。

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [概述](#概述)
  * [功能列表](#功能列表)
  * [在 macOS 上运行](#在-macos-上运行)
  * [要求](#要求)
  * [许可](#许可)
  * [素材说明](#素材说明)
  * [制作人员致谢](#制作人员致谢)
  * [第三方通知](#第三方通知)
  * [赞助商](#赞助商)
<!-- TOC -->

## 概述

“uDesktopMascot” 是一个开源项目，旨在桌面上显示角色，并根据用户的交互作出反应和播放声音。该项目是使用 Unity 开发的，支持 VRM 格式的角色，使您能够轻松地在桌面上享受自己喜欢的角色。

**支持的平台**
* Windows 10/11
* macOS

## 功能列表

应用程序实现了以下功能。详细内容请参见以下列表。

您可以通过将外部资产放置在 StreamingAssets 文件夹中来添加。

<details>

<summary>模型与动画</summary>
* 加载并显示放置在 StreamingAssets 中的任意模型文件。
  * 支持 VRM (1.x, 0.x) 格式的模型。
  * 支持 GLB/GLTF 格式的模型（不支持动画）。
  * 支持 FBX 格式的模型（但某些模型可能无法加载纹理，且不支持动画）。
    * 纹理可以通过放置在 StreamingAssets/textures/ 中进行加载。

</details>

<details>

<summary>声音与背景音乐</summary>
* 加载并播放放置在 StreamingAssets/Voice/ 中的音频文件。如果有多个文件，将随机播放。
  * 点击时播放的声音将从 StreamingAssets/Voice/Click/ 中加载音频文件。
* 加载并播放放置在 StreamingAssets/BGM/ 中的音乐文件。如果有多个文件，将随机播放。
* 默认角色语音的添加：
  * 默认语音使用的是 [COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan) 的声音。
  * 在应用启动、应用退出和点击时播放。

</details>

<details>

<summary>通过文本文件进行应用设置</summary>
可以通过 application_settings.txt 文件更改应用程序设置。

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

<details>

<summary>菜单界面</summary>

* 可以设置菜单界面的背景图像和背景颜色。
  * 背景图像可以从放置在 StreamingAssets/Menu/ 中的图像文件加载。支持的图像格式如下：
    * PNG
    * JPG (JPEG)
    * BMP
    * GIF（静止图像）
    * TGA
    * TIFF
  * 背景颜色可以指定颜色代码。

</details>

## 在 macOS 上运行

在 macOS 上运行应用时，可能会因为 GateKeeper 拦截应用。
在这种情况下，请从终端执行以下命令。

```sh
xattr -r -c uDesktopMascot.app
```

## 要求
* Unity 6000.0.31f1 (IL2CPP)

## 许可
* 代码依据 [Apache License 2.0](LICENSE) 进行许可。
* 以下资产依据 [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/) 进行许可：
  * 背景音乐
  * 模型

## 素材说明
* 默认角色动画使用了 [『VRM お人形遊び』用动画数据合集](https://fumi2kick.booth.pm/items/1655686) 制作。已确认可以在仓库中包含并分发。
* 字体为 [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan)。依据 [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan) 进行再分发。字体版权归原作者（Google）所有。
* 默认语音使用 [COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan) 的音频。使用方法已事先与 COEIROINK 确认。
* 按钮图标使用的是 [MingCute](https://github.com/MidraLab/MingCute)。

## 制作人员致谢
* 模型: 「アオゾラ」先生
* 背景音乐: MidraLab（eisuke）
* 软件图标: やむちゃ先生

## 第三方通知

请参阅 [NOTICE](./NOTICE.md)。

## 赞助商
- Luna
- uezo