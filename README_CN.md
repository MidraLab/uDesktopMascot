# uDesktopMascot

[![Unity 版本](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)  
[![版本发布](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)  
[![Unity 测试 CI](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)  
[![询问 DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/MidraLab/uDesktopMascot)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**注意**: 上述语言（English、中文、Español、Français）是由GPT-4o-mini进行的自动翻译。有关翻译的准确性和细微差别，请参阅原文（日本語）。

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [概述](#概述)
  * [功能列表](#功能列表)
  * [关于在macOS上运行](#关于在macos上运行)
  * [要求](#要求)
  * [许可证](#许可证)
  * [素材说明](#素材说明)
  * [创建安装程序的方法](#创建安装程序的方法)
    * [Windows](#windows)
    * [macOS](#macos)
  * [制作者致谢](#制作者致谢)
  * [第三方通知](#第三方通知)
  * [赞助商](#赞助商)
<!-- TOC -->

## 概述

“uDesktopMascot”是一个以`创作自由化`为主题的桌面吉祥物应用程序的开源项目。  
作为一个功能示例，可以加载VRM和GLB/FBX格式的模型并在桌面上显示。此外，可以自由设置菜单画面和应用程序窗口等GUI的颜色和背景图像。  
详细的功能列表请参阅 [功能列表](#功能列表)。

![](Docs/Image/AppImage.png)

**支持的平台**
* Windows 10/11
* macOS

## 功能列表

应用程序实现了以下功能。详细信息请参阅以下列表。

通过将外部资产放置在StreamingAssets文件夹中来实现。

<details>

<summary>模型・动画</summary>

* 加载并显示放置在StreamingAssets中的任意模型文件。  
  * 支持VRM（1.x、0.x）格式的模型。  
  * 支持GLB/GLTF格式的模型（不支持动画）。  
  * 支持FBX格式的模型（部分模型的纹理可能无法加载，且不支持动画）。  
    * 通过放置纹理在StreamingAssets/textures/中可以加载。  
* 从模型选择・添加界面增加VRM模型  
  * 通过指定路径增加  
  * 从文件选择对话框中增加  

</details>

<details>

<summary>声音・BGM</summary>

* 加载并播放放置在SteamingAssets/Voice/中的音频文件。如果有多个，将随机播放。  
  * 点击时播放的声音将加载并播放放置在StreamingAssets/Voice/Click/中的音频文件。  
* 加载并播放放置在SteamingAssets/BGM/中的音乐文件。如果有多个，将随机播放。  
* 默认角色配音的添加  
  * 默认配音使用来自[COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan)的音频。  
  * 在应用启动、应用结束和点击时播放。  

</details>

<details>

<summary>通过文本文件进行应用程序设置</summary>
可以通过application_settings.txt文件更改应用程序的设置。

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

<summary>菜单画面</summary>

* 可以设置菜单画面的背景图像及背景颜色。  
  * 背景图像可以加载放置在StreamingAssets/Menu/中的图像文件。支持的图像格式如下：  
    * PNG  
    * JPG（JPEG）  
    * BMP  
    * GIF（静态图像）  
    * TGA  
    * TIFF  
  * 背景颜色可以指定颜色代码。  
* 从菜单画面可以访问以下功能：  
  * 模型选择・添加界面  
  * AI聊天功能  
  * LocalWeb功能  
  * 应用程序设置  
  * 退出应用程序  
* 按下菜单画面的收起按钮，Windows用户可以将应用程序收起到通知区域。  
  * 收起的应用程序可以通过点击通知区域的图标重新显示。  

</details>

## 关于在macOS上运行

在macOS上运行应用程序时，GateKeeper可能会阻止应用程序。  
在这种情况下，请从终端执行以下命令：

```sh
xattr -r -c uDesktopMascot.app
```

## 要求
* Unity 6000.1.1f1（IL2CPP）

## 许可证
* 代码依照[Apache License 2.0](LICENSE)进行许可。  
* 以下资产依照[CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/)进行许可：  
  * BGM  
  * 模型  

## 素材说明
* 默认角色动画使用[《VRM尘世游戏》用动画数据集合](https://fumi2kick.booth.pm/items/1655686)制作，已确认可以在仓库中分发。  
* 字体为[Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan)。依据[SIL OPEN FONT LICENSE 第1.1版](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan)重新分发Noto Sans JP字体。字体版权归原作者（Google）所有。  
* 默认配音使用[COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan)的音频，使用方法已提前与COEIROINK确认。  
* 按钮图标使用[MingCute](https://github.com/MidraLab/MingCute)。  

## 创建安装程序的方法
### Windows
* 在Unity中将应用程序构建到名为`uDesktopMascot`的文件夹中。  
```
uDesktopMasscot
└── build
    └── uDesktopMasscot
        ├── uDesktopMascot.exe
        └── README.txt
        ...
```

* 安装[Inno Setup](https://www.jrsoftware.org/isdl.php)。  

* 打开后，点击`More files`，选择项目目录下的`setup.iss`。  

![](Docs/Image/SetupIss-1.png)  
* 选择后，点击播放按钮。  

![](Docs/Image/SetupIss-2.png)  
* 构建完成后，安装程序将在项目的根目录生成。  

### macOS
只能在macOS的电脑上创建安装程序。

* 在Unity中将应用程序构建到名为`uDesktopMascot`的`build/uDesktopMascot`文件夹中。  
```
uDesktopMasscot
└── build
    └── uDesktopMasscot
        ├── uDesktopMascot.app
        └── README.txt
```

* 执行以下命令。  
```sh
cd build
productbuild --component uDesktopMascot/uDesktopMascot.app /Applications ./uDesktopMascot_mac_installer.pkg
```
* 构建完成后，`build`文件夹中会生成`uDesktopMascot_mac_installer.pkg`。

## 制作者致谢
* 模型: 「アオゾラ」様  
* BGM: MidraLab（eisuke）  
* 软件图标: やむちゃ様  

## 第三方通知

请参阅 [NOTICE](./NOTICE.md)。

## 赞助商
- Luna  
- uezo  