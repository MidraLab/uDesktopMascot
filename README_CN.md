# uDesktopMascot

[![Unity 版本](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![版本发布](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Unity 测试 CI](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)
[![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/MidraLab/uDesktopMascot)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**注意**: 上述语言（English、中文、Español、Français）由 GPT-4o-mini 自动翻译生成。翻译的准确性和语义方面请参照原文（日本語）。

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [概述](#概述)
  * [功能列表](#功能列表)
  * [在macOS上的执行](#在macos上的执行)
  * [要求](#requirements)
  * [许可证](#license)
  * [素材信息](#素材信息)
  * [安装程序创建方法](#安装程序创建方法)
    * [Windows](#windows)
    * [macOS](#macos)
  * [制作人员信用](#制作者クレジット)
  * [第三方通知](#3rd-party-notices)
  * [赞助商](#sponsor)
<!-- TOC -->

## 概述

“uDesktopMascot”是一个以`创作的自由化`为主题的桌面吉祥物应用程序的开源项目。
作为一个功能示例，它可以加载VRM和GLB/FBX格式的模型，并在桌面上显示。此外，还可以自由设置菜单界面和应用程序窗口等GUI的颜色和背景图像。
详细功能列表请参见 [功能列表](#功能列表)。

![](Docs/Image/AppImage.png)

**支持平台**
* Windows 10/11
* macOS

## 功能列表

应用程序实现了以下功能。详细信息请参阅下面的列表。

通过将外部资产放入StreamingAssets文件夹可以实现其添加。

<details>

<summary>模型・动画</summary>

* 加载并显示放置在StreamingAssets中的任意模型文件。
  * 支持VRM (1.x, 0.x)格式模型。
  * 支持GLB/GLTF格式模型。(不支持动画)
  * 支持FBX格式模型。(但部分模型的纹理无法加载。并且不支持动画)
    * 纹理可通过放置在StreamingAssets/textures/中加载。
* 从模型选择和添加界面添加VRM模型
  * 指定路径以添加
  * 从文件选择对话框添加

</details>

<details>

<summary>语音・背景音乐</summary>

* 加载并播放放置在SteamingAssets/Voice/中的音频文件。如果有多个，将随机播放。
  * 点击时播放的音频是从StreamingAssets/Voice/Click/中加载的音频文件。
* 加载并播放放置在SteamingAssets/BGM/中的音乐文件。如果有多个，将随机播放。
* 角色的默认语音添加
  * 默认语音使用[COEIROINK:つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan)的音频。
  * 在应用启动、应用结束和点击时播放。

</details>

<details>

<summary>语音识别・AI聊天</summary>

* 集成离线语音识别引擎 [Vosk](https://alphacephei.com/vosk/)，将麦克风输入实时转换为文本。
  * 在Unity控制台中显示中间结果`[STT][partial]`和确定结果`[STT][final]`。
  * 如果静音持续`VadSilenceSeconds`（默认1.0秒），文本将被确认并发送到LLM（`[STT][send]`日志）。
* 文本确认后，将音频消息传递给AI聊天功能，角色将朗读响应。
  * 在生成响应期间，麦克风将自动暂停以防止误识别。
* 在ChatDialog界面的麦克风按钮上，可以切换录音的开始/停止。
* 所需的原生DLL（`libvosk.dll`, `libstdc++-6.dll`, `libgcc_s_seh-1.dll`, `libwinpthread-1.dll`等）将放置在`Assets/Plugins/x86_64/`中，并将自动打包到构建中。
* 通过将音频模型（例如：`vosk-model-small-ja-0.22`）和量化的GGUF模型放置在`StreamingAssets`下，可以在没有网络连接的情况下处理多种语言（如日语/英语）。

</details>

<details>

<summary>通过文本文件进行应用程序设置</summary>
可以通过application_settings.txt文件更改应用程序的设置。

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

<summary>菜单界面</summary>

* 可设置菜单界面的背景图像和背景颜色。
  * 背景图像可加载放置在StreamingAssets/Menu/中的图像文件。支持的图像格式如下：
    * PNG
    * JPG(JPEG)
    * BMP
    * GIF(静态图)
    * TGA
    * TIFF
  * 背景颜色可以指定颜色代码。
* 从菜单界面可以访问以下功能：
  * 模型选择和添加界面
  * AI聊天功能
  * LocalWeb功能
  * 应用程序设置
  * 应用程序退出
* 通过点击菜单界面的收纳按钮，可以在Windows的通知区域隐藏应用程序。
  * 隐藏的应用程序可以通过点击通知区域的图标再次显示。

</details>

## 在macOS上的执行

在macOS上执行应用程序时，可能会被GateKeeper阻止。
在这种情况下，请在终端中运行以下命令。

```sh
xattr -r -c uDesktopMascot.app
```

## 要求
* Unity 6000.1.1f1(IL2CPP)

## 许可证
* 代码遵循[Apache License 2.0](LICENSE)许可证。
* 以下资产遵循[CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/)许可证。
  * BGM
  * 模型

## 素材信息
* 默认角色动画是基于[『VRMお人形遊び』用动画数据合集](https://fumi2kick.booth.pm/items/1655686)制作的。已经确认可以在仓库中捆绑发行。
* 字体为[Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan)。根据[SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan) 重新分发Noto Sans JP字体。字体版权归原作者（Google）所有。
* 默认语音使用[COEIROINK:つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan)的音频。使用方法已事先向COEIROINK确认。
* 按钮图标使用[MingCute](https://github.com/MidraLab/MingCute)。

## 安装程序创建方法
### Windows
* 在Unity中将`uDesktopMascot`命名为`build`文件夹并进行构建。
```
uDesktopMascot
└── build
    └── uDesktopMascot
        ├── uDesktopMascot.exe
        └── README.txt
        ...
```

* 安装[Inno Setup](https://www.jrsoftware.org/isdl.php)工具。
  
* 打开后，点击`More files`，选择项目下的`setup.iss`。
  
![](Docs/Image/SetupIss-1.png)
* 选择后，点击播放按钮。
  
![](Docs/Image/SetupIss-2.png)
* 构建完成后，安装程序将生成在项目的根目录下。

### macOS
仅可在macOS PC上创建安装程序。

* 在Unity中将`uDesktopMascot`命名为`build/uDesktopMascot`文件夹并进行构建。
```
uDesktopMascot
└── build
    └── uDesktopMascot
        ├── uDesktopMascot.app
        └── README.txt
```

* 执行以下命令。
```sh
cd build
productbuild --component uDesktopMascot/uDesktopMascot.app /Applications ./uDesktopMascot_mac_installer.pkg
```
* 构建完成后，`build`文件夹中将生成`uDesktopMascot_mac_installer.pkg`。

## 制作人员信用
* 模型: 「アオゾラ」様
* BGM: MidraLab(eisuke)
* 软件图标: やむちゃ様

## 第三方通知

请参见[NOTICE](./NOTICE.md)。

## 赞助商
- Luna
- uezo