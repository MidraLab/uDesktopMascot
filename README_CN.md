# uDesktopMascot

[![Unity Version](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Releases](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**注意**: 上述语言（English、中文、Español、Français）是通过GPT-4o-mini自动翻译生成的。翻译的准确性和细微差别请参考原文（日本語）。

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [概要](#概要)
  * [功能列表](#功能列表)
  * [要求](#要求)
  * [许可证](#许可证)
  * [素材说明](#素材说明)
  * [制作者致谢](#制作者致谢)
  * [第三方声明](#第三方声明)
  * [赞助商](#赞助商)
<!-- TOC -->

## 概要

「uDesktopMascot」是一个开源项目，可以在桌面上显示角色，并根据用户的互动进行响应和播放声音。该项目使用Unity开发，支持VRM格式的角色，使得用户可以轻松在桌面上享受自己喜欢的角色。

**支持平台**
* Windows 10/11
* macOS

## 功能列表

该应用实现了以下功能。详细信息请参见以下列表。

通过将外部资源放置在StreamingAssets文件夹中可以实现添加。

<details>

<summary>模型与动画</summary>
* 读取并显示放置在StreamingAssets中的任意模型文件。
  * 支持VRM(1.x, 0.x)格式的模型。
  * 支持GLB/GLTF格式的模型。（不支持动画）
  * 支持FBX格式的模型。（但某些模型可能无法加载纹理，并且不支持动画）
    * 纹理可以通过放置在StreamingAssets/textures/中读取。

</details>

<details>

<summary>声音与背景音乐</summary>
* 读取并播放放置在StreamingAssets/Voice/中的音频文件。如果有多个，则随机播放。
  * 点击时播放的声音，将读取并播放放置在StreamingAssets/Voice/Click/中的音频文件。
* 读取并播放放置在StreamingAssets/BGM/中的音乐文件。如果有多个，则随机播放。
* 角色的默认音效
  * 默认音效使用[COEIROINK:つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan)的声音。
  * 在应用启动时、应用结束时和点击时播放。

</details>

<details>

<summary>通过文本文件进行应用设置</summary>
可以通过application_settings.txt文件更改应用的设置。

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

## 要求
* Unity 6000.0.31f1(IL2CPP)

## 许可证
* 代码根据[Apache License 2.0](LICENSE)授权。
* 以下资产根据[CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/)授权。
  * BGM
  * 模型

## 素材说明
* 默认的角色动画使用了[《VRM人形玩具》用动画数据合集](https://fumi2kick.booth.pm/items/1655686)制作。经过确认可以在此库中提供分发。
* 字体使用[Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan)。根据[SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan)进行Noto Sans JP字体的再发布。字体的版权归原作者（Google）所有。
* 默认音效使用了[COEIROINK:つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan)的声音。使用方法已事先确认。
* 按钮图标使用了[MingCute](https://github.com/MidraLab/MingCute)。

## 制作者致谢
* 模型: 「アオゾラ」先生
* BGM: MidraLab(eisuke)
* 软件图标: やむちゃ先生

## 第三方声明

请参见[NOTICE](./NOTICE.md)。

## 赞助商
- Luna
- uezo