# uDesktopMascot

[![Unity 版本](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![发布版本](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Unity 测试 CI](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**注意**: 上述语言（English、中文、Español、Français）是由 GPT-4o-mini 自动翻译生成的。关于翻译的准确性和细微差别，请参阅原文（日本语）。

<!-- TOC -->
- [uDesktopMascot](#udesktopmascot)
  - [概述](#概述)
  - [功能列表](#功能列表)
  - [在 macOS 上的运行说明](#在-macos-上的运行说明)
  - [requirements](#requirements)
  - [license](#license)
  - [素材说明](#素材说明)
  - [安装程序创建方法](#安装程序创建方法)
    - [Windows](#windows)
    - [macOS](#macos)
  - [制作人鸣谢](#制作人鸣谢)
  - [第三方通知](#第三方通知)
  - [赞助](#赞助)
<!-- TOC -->

## 概述

“uDesktopMascot”是一个以“创作的自由化”为主题的桌面吉祥物应用程序的开源项目。它的一个功能示例是能够读取 VRM 或 GLB/FBX 格式的模型并在桌面上显示。此外，您可以自由配置菜单界面和应用程序窗口等 GUI 的颜色和背景图像。详细的功能列表请参阅 [功能列表](#功能列表)。

![](Docs/Image/AppImage.png)

**支持平台**
* Windows 10/11
* macOS

## 功能列表

应用程序实现了以下功能。详细信息请参见下面的列表。

外部资产的添加可以通过将其放置在 StreamingAssets 文件夹中实现。

<details>

<summary>模型与动画</summary>

* 从 StreamingAssets 中加载并显示任意模型文件。
  * 支持 VRM（1.x、0.x）格式的模型。
  * 支持 GLB/GLTF 格式的模型。（不支持动画）
  * 支持 FBX 格式的模型。（部分模型可能无法加载纹理，且不支持动画）
    * 纹理可以通过放置在 StreamingAssets/textures/ 来加载。
* 从模型选择添加界面中添加 VRM 模型
  * 指定路径添加
  * 从文件选择对话框添加

</details>

<details>

<summary>语音与背景音乐</summary>

* 从 StreamingAssets/Voice/ 下加载语音文件并播放。如果有多个文件，将随机播放。
  * 在点击时播放的语音文件将从 StreamingAssets/Voice/Click/ 加载并播放。
* 从 StreamingAssets/BGM/ 下加载音乐文件并播放。如果有多个文件，将随机播放。
* 添加角色的默认语音
  * 默认语音使用 [COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan) 的声音。
  * 在应用启动时、应用结束时及点击时播放。

</details>

<details>

<summary>通过文本文件进行应用程序设置</summary>
可以通过 application_settings.txt 文件修改应用程序的设置。

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
  * 背景图像可以从 StreamingAssets/Menu/ 中加载的图像文件中获取。支持的图像格式如下：
    * PNG
    * JPG（JPEG）
    * BMP
    * GIF（静态图像）
    * TGA
    * TIFF
  * 背景颜色可以指定颜色代码。
* 从菜单界面可以访问以下功能：
  * 模型选择与添加界面
  * AI 聊天功能
  * LocalWeb 功能
  * 应用程序设置
  * 退出应用程序
* 通过按下菜单界面的收起按钮，在 Windows 上可以将应用程序收起到通知区域中。
  * 收起的应用程序可以通过点击通知区域的图标重新显示。

</details>

## 在 macOS 上的运行说明

在 macOS 上运行应用程序时，可能会被 GateKeeper 阻止。如果发生这种情况，请在终端中执行以下命令：

```sh
xattr -r -c uDesktopMascot.app
```

## requirements
* Unity 6000.1.1f1(IL2CPP)

## license
* 代码基于 [Apache License 2.0](LICENSE) 授权。
* 以下资产基于 [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/) 授权。
  * 背景音乐
  * 模型

## 素材说明
* 默认角色动画是采用 [『VRM お人形遊び』用动画数据集合](https://fumi2kick.booth.pm/items/1655686) 制作，已确认可以在仓库中进行分发。
* 字体为 [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan)，依据 [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan) 进行再分发，字体著作权属于原作者（Google）。
* 默认语音使用 [COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan) 的声音，使用方法已提前确认。
* 按钮图标采用 [MingCute](https://github.com/MidraLab/MingCute)。

## 安装程序创建方法
### Windows
* 在 Unity 中将应用程序生成到名为 `uDesktopMascot` 的 `build` 文件夹中。
```
uDesktopMasscot
└── build
    └── uDesktopMasscot
        ├── uDesktopMascot.exe
        └── README.txt
        ...
```

* 安装 [Inno Setup](https://www.jrsoftware.org/isdl.php)。

* 打开后点击 `More files`，选择项目中 `setup.iss` 文件。
  
![](Docs/Image/SetupIss-1.png)
* 选择后点击播放按钮。

![](Docs/Image/SetupIss-2.png)
* 构建完成后，安装程序将生成在项目的根目录中。

### macOS
可以仅在 macOS 计算机上创建安装程序。

* 在 Unity 中将应用程序生成到名为 `uDesktopMascot` 的 `build/uDesktopMascot` 文件夹中。
```
uDesktopMasscot
└── build
    └── uDesktopMascot
        ├── uDesktopMascot.app
        └── README.txt
```

* 执行以下命令：
```sh
cd build
productbuild --component uDesktopMascot/uDesktopMascot.app /Applications ./uDesktopMascot_mac_installer.pkg
```
* 构建完成后，`build` 文件夹中将生成 `uDesktopMascot_mac_installer.pkg`。

## 制作者鸣谢
* 模型: 「アオゾラ」称
* 背景音乐: MidraLab(eisuke)
* 软件图标: やむちゃ称

## 第三方通知

请参阅 [NOTICE](./NOTICE.md)。

## 赞助
- Luna
- uezo