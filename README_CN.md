# uDesktopMascot

[![Unity 版本](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)  
[![发布](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)  
[![Unity 测试 CI](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)  

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)  

**注意**: 上述语言（English、中文、Español、Français）是通过GPT-4o-mini的自动翻译生成的。关于翻译的准确度和细微差别，请参考原文（日本語）。

<!-- TOC -->  
* [uDesktopMascot](#udesktopmascot)  
  * [概述](#概述)  
  * [功能列表](#功能列表)  
  * [在 macOS 上运行的注意事项](#在-macos-上运行的注意事项)  
  * [要求](#要求)  
  * [许可](#许可)  
  * [素材说明](#素材说明)  
  * [安装程序制作方法](#安装程序制作方法)  
    * [Windows](#windows)  
    * [macOS](#macos)  
  * [制作者致谢](#制作者致谢)  
  * [第三方通知](#第三方通知)  
  * [赞助商](#赞助商)  
<!-- TOC -->  

## 概述

“uDesktopMascot”是一款以`创作自由化`为主题的桌面吉祥物应用的开源项目。  
作为一个功能示例，它可以加载VRM和GLB/FBX格式的模型并在桌面上显示。此外，还可以自由设置菜单画面和应用窗口等GUI的颜色和背景图像。有关详细的功能列表，请参见 [功能列表](#功能列表)。

![](Docs/Image/AppImage.png)

**支持平台**  
* Windows 10/11  
* macOS  

## 功能列表

应用程序实现了以下功能。详细信息请参见以下列表。

通过将外部资产放置在StreamingAssets文件夹中来实现添加。

<details>

<summary>模型・动画</summary>

* 加载并显示放置在StreamingAssets中的任意模型文件。  
  * 支持VRM(1.x, 0.x)格式模型。  
  * 支持GLB/GLTF格式模型。（不支持动画）  
  * 支持FBX格式模型。（不过部分模型的纹理无法加载，且不支持动画）  
    * 纹理可以通过放置在StreamingAssets/textures/中进行加载。  
* 从模型选择・添加画面添加VRM模型  
  * 根据路径添加  
  * 从文件选择对话框中添加  

</details>

<details>

<summary>语音・背景音乐</summary>

* 加载并播放放置在SteamingAssets/Voice/中的音频文件。如有多个文件，将随机播放。  
  * 点击时播放的音频是，从StreamingAssets/Voice/Click/中加载并播放的音频文件。  
* 加载并播放放置在SteamingAssets/BGM/中的音乐文件。如有多个文件，将随机播放。  
* 增加角色的默认语音  
  * 默认语音使用的是[COEIROINK:つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan)的音频。  
  * 在应用启动时、应用退出时和点击时播放。  

</details>

<details>

<summary>通过文本文件进行应用设置</summary>
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
  * 背景图像可以从StreamingAssets/Menu/中加载图片文件。支持的图片格式包括：  
    * PNG  
    * JPG(JPEG)  
    * BMP  
    * GIF(静态图)  
    * TGA  
    * TIFF  
  * 背景色可以指定颜色代码。  
* 从菜单画面可以访问以下功能：  
  * 模型选择・添加画面  
  * AI聊天功能  
  * LocalWeb功能  
  * 应用程序设置  
  * 退出应用程序  
* 按下菜单画面的收起按钮，应用程序将仅在通知区域中进行收起（仅限Windows）。  
  * 收起的应用程序可以通过点击通知区域的图标再次显示。  

</details>

## 在 macOS 上运行的注意事项

在macOS上运行应用程序时，可能会被GateKeeper阻止。  
在这种情况下，请从终端执行以下命令。

```sh
xattr -r -c uDesktopMascot.app
```

## 要求
* Unity 6000.1.1f1(IL2CPP)  

## 许可
* 代码根据[Apache License 2.0](LICENSE)进行许可。  
* 以下资产根据[CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/)进行许可。  
  * 背景音乐  
  * 模型  

## 素材说明
* 默认角色动画使用[《VRM人偶游戏》用动画数据包](https://fumi2kick.booth.pm/items/1655686)制作。已确认可以包含并分发到仓库中。  
* 字体为[Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan)。根据[SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan)重新分发Noto Sans JP字体。字体的版权归原作者（Google）所有。  
* 默认语音使用的是[COEIROINK:つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan)的音频。使用方法已事先确认。  
* 按钮图标使用的是[MingCute](https://github.com/MidraLab/MingCute)。  

## 安装程序制作方法
### Windows
* 在Unity中，将项目构建为名为`uDesktopMascot`的文件夹。  
```
uDesktopMasscot  
└── build  
    └── uDesktopMasscot  
        ├── uDesktopMascot.exe  
        └── README.txt  
        ...  
```

* 安装[Inno Setup](https://www.jrsoftware.org/isdl.php)。  

* 打开后，点击`More files`并选择项目下的`setup.iss`。  

![](Docs/Image/SetupIss-1.png)  
* 选择后，点击播放按钮。  

![](Docs/Image/SetupIss-2.png)  
* 构建完成后，安装程序将在项目的根目录生成。  

### macOS
仅能在macOS PC上生成安装程序。

* 在Unity中，将项目构建为名为`uDesktopMascot`的构建文件夹。  
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
* 构建完成后，`build`文件夹中将生成`uDesktopMascot_mac_installer.pkg`。  

## 制作者致谢
* 模型: 「アオゾラ」様  
* 背景音乐: MidraLab(eisuke)  
* 软件图标: やむちゃ様  

## 第三方通知

请参见 [NOTICE](./NOTICE.md)。  

## 赞助商
- Luna  
- uezo  