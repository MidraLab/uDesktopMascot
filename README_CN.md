# uDesktopMascot

[![Unity 版本](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![版本发布](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Unity 测试 CI](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**注意**: 上述语言（English、中文、Español、Français）是由 GPT-4o-mini 自动翻译生成的。对于译文的准确性和语气，建议参考原文（日语）。

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [概述](#概述)
  * [功能列表](#功能列表)
  * [在 macOS 上运行说明](#在-macos-上运行说明)
  * [需求](#需求)
  * [许可证](#许可证)
  * [素材说明](#素材说明)
  * [安装程序创建方法](#安装程序创建方法)
    * [Windows](#windows)
    * [macOS](#macos)
  * [制作者信用](#制作者信用)
  * [第三方通知](#第三方通知)
  * [赞助商](#赞助商)
<!-- TOC -->

## 概述

“uDesktopMascot” 是一个以`创作自由` 为主题的桌面吉祥物应用程序的开源项目。
例如，它可以加载 VRM、GLB/FBX 格式的模型并在桌面上显示。此外，您还可以自由设置菜单画面和应用程序窗口等 GUI 的颜色和背景图像。
详细的功能列表请参见 [功能列表](#功能列表)。

![](Docs/Image/AppImage.png)

**支持平台**
* Windows 10/11
* macOS

## 功能列表

该应用程序实现了以下功能。详细信息请参见以下列表。

通过将外部资产放置在 StreamingAssets 文件夹中来实现。

<details>

<summary>模型与动画</summary>

* 加载并显示放置在 StreamingAssets 中的任意模型文件。
  * 支持 VRM(1.x, 0.x) 格式的模型。
  * 支持 GLB/GLTF 格式的模型。（不支持动画）
  * 支持 FBX 格式的模型。（但某些模型的纹理可能无法加载。同时不支持动画）
    * 纹理可以通过放置在 StreamingAssets/textures/ 中来加载。
* 可以从模型添加画面中添加 VRM 的角色模型
  * 添加 VRM 路径
  * 从文件选择对话框中选择文件

</details>

<details>

<summary>语音与背景音乐</summary>

* 加载并播放放置在 SteamingAssets/Voice/ 中的音频文件。如果有多个，将随机播放。
  * 点击时播放的音频从被放在 StreamingAssets/Voice/Click/ 中的音频文件加载。
* 加载并播放放置在 SteamingAssets/BGM/ 中的音乐文件。如果有多个，将随机播放。
* 添加角色的默认语音
  * 默认语音使用 [COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan) 的音频。
  * 在应用启动时、应用结束时和点击时播放。

</details>

<details>

<summary>通过文本文件进行应用设置</summary>
可以通过 application_settings.txt 文件更改应用程序的设置。

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
  * 背景图像可以从放置在 StreamingAssets/Menu/ 中的图像文件加载。支持的图像格式如下：
    * PNG
    * JPG(JPEG)
    * BMP
    * GIF(静止图像)
    * TGA
    * TIFF
  * 背景颜色可以通过指定颜色代码来实现。

菜单中可以使用以下功能。
* 添加或更改模型
* 更改应用设置
* 退出应用
* （仅限 Windows）将应用程序存放在通知区域：可以将应用程序最小化。
  * 将应用存放到通知区域时，桌面上显示的应用图标将显示在通知区域。
  * 通过点击通知区域的图标，可以将应用显示在桌面上。
* AI 聊天功能：可以使用 AI 聊天功能。
  * 使用 Qwen2 模型的本地模型支持

</details>

## 在 macOS 上运行说明

在 macOS 上运行应用程序时，GateKeeper 可能会阻止应用程序。
在这种情况下，请从终端执行以下命令。

```sh
xattr -r -c uDesktopMascot.app
```

## 需求
* Unity 6000.0.31f1(IL2CPP)

## 许可证
* 代码根据 [Apache License 2.0](LICENSE) 进行授权。
* 以下资产根据 [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/) 进行授权。
  * BGM
  * 模型

## 素材说明
* 默认的角色动画使用 [『VRM お人形遊び』用动画数据包](https://fumi2kick.booth.pm/items/1655686) 制作。确认过在库中分发。
* 字体为 [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan)。根据 [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan) 重新分发 Noto Sans JP 字体。字体版权归原作者（Google）所有。
* 默认语音使用 [COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan) 的音频。使用方法已在使用前确认。
* 按钮图标使用 [MingCute](https://github.com/MidraLab/MingCute)。

## 安装程序创建方法
### Windows
* 在 Unity 中将其构建为名为 `uDesktopMascot` 的文件夹。

* 安装 [Inno Setup](https://www.jrsoftware.org/isdl.php)。
  
* 打开后，点击 `More files`，选择项目下的 `setup.iss` 文件。
  
![](Docs/Image/SetupIss-1.png)
* 选择后，点击播放按钮。
  
![](Docs/Image/SetupIss-2.png)
* 构建完成后，安装程序将生成在项目的根目录。

### macOS
只能在 macOS PC 上创建安装程序。

* 在 Unity 中将其构建为名为 `uDesktopMascot` 的文件夹。

* 执行以下命令。
```sh
cd build
productbuild --component uDesktopMascot/uDesktopMascot.app /Applications ./uDesktopMascot_mac_installer.pkg
```
* 构建完成后，`uDesktopMascot_mac_installer.pkg` 将在 `build` 文件夹中生成。

## 制作者信用
* 模型: 「アオゾラ」様
* BGM: MidraLab(eisuke)
* 软件图标: やむちゃ様

## 第三方通知

请见 [NOTICE](./NOTICE.md)。

## 赞助商
- Luna
- uezo