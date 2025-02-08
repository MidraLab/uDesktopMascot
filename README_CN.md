# uDesktopMascot

[![Unity Version](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Releases](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Unity Test CI](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**注意**: 上記の言語（English、中文、Español、Français）は、GPT-4o-miniによる自動翻訳で生成されています。翻訳の精度やニュアンスに関しては、原文（日本語）をご参照ください。

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [概述](#概述)
  * [功能列表](#功能列表)
  * [在macOS上运行](#在macos上运行)
  * [要求](#要求)
  * [许可证](#许可证)
  * [素材信息](#素材信息)
  * [制作人致谢](#制作人致谢)
  * [第三方公告](#第三方公告)
  * [赞助商](#赞助商)
<!-- TOC -->

## 概述

“uDesktopMascot”是一个以`创作自由`为主题的桌面吉祥物应用程序的开源项目。
作为功能的一个例子，它能够加载VRM和GLB/FBX格式的模型，并将其显示在桌面上。此外，用户还可以自由设置菜单界面和应用程序窗口的颜色和背景图像。
有关详细的功能列表，请参阅[功能列表](#功能列表)。

![](Docs/Image/AppImage.png)

**支持平台**
* Windows 10/11
* macOS

## 功能列表

该应用程序实现了以下功能。详细信息请参考以下列表。

外部资产的添加可以通过将其放置在StreamingAssets文件夹中实现。

<details>

<summary>模型与动画</summary>

* 读取并显示存放在StreamingAssets中的任意模型文件。
  * 支持VRM(1.x, 0.x)格式的模型。
  * 支持GLB/GLTF格式的模型。（不支持动画）
  * 支持FBX格式的模型。（但某些模型可能无法加载纹理，且不支持动画）
    * 可以通过将纹理放置在StreamingAssets/textures/中来加载。

</details>

<details>

<summary>声音与背景音乐</summary>

* 读取并播放位于StreamingAssets/Voice/中的音频文件。若有多个文件，则随机播放。
  * 点击时播放的声音来自于StreamingAssets/Voice/Click/中的音频文件。
* 读取并播放位于StreamingAssets/BGM/中的音乐文件。若有多个文件，则随机播放。
* 增加角色的默认声音
  * 默认声音使用的是[COEIROINK:つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan)的音频。
  * 在应用启动时、应用退出时以及点击时播放。

</details>

<details>

<summary>通过文本文件进行应用设置</summary>
可以通过application_settings.txt文件来修改应用程序的设置。

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

* 可以设置菜单界面的背景图像及背景颜色。
  * 背景图片可以通过读取放置在StreamingAssets/Menu/中的图像文件来加载。支持的图像格式如下：
    * PNG
    * JPG(JPEG)
    * BMP
    * GIF（静态图）
    * TGA
    * TIFF
  * 可以指定背景颜色的代码。

</details>

## 在macOS上运行

在macOS上运行应用时，可能会被GateKeeper拦截。
在这种情况下，请从终端运行以下命令：

```sh
xattr -r -c uDesktopMascot.app
```

## 要求
* Unity 6000.0.31f1（IL2CPP）

## 许可证
* 代码基于[Apache License 2.0](LICENSE)进行许可。
* 以下资产基于[CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/)进行许可。
  * 背景音乐
  * 模型

## 素材信息
* 默认角色动画使用了[《VRM人偶游戏》用动画数据包](https://fumi2kick.booth.pm/items/1655686)制作。确认为可以在存储库中包含并分发。
* 字体为[Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan)。根据[SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan)再发行Noto Sans JP字体。字体的版权归原作者（Google）所有。
* 默认声音使用了[COEIROINK:つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan)的音频。使用方法已经提前与COEIROINK核实。
* 按钮图标使用[MingCute](https://github.com/MidraLab/MingCute)。

## 创建安装程序的方法
### Windows
* 在Unity中，将项目构建为名为`uDesktopMascot`的文件夹。

* 安装[Inno Setup](https://www.jrsoftware.org/isdl.php)。

* 打开后，点击`More files`，选择项目目录下的`setup.iss`。

![](Docs/Image/SetupIss-1.png)
* 选择后，点击播放按钮。

![](Docs/Image/SetupIss-2.png)
* 构建完成后，安装程序将在项目根目录生成。

### macOS
只能在macOS PC上创建安装程序。

* 在Unity中，将项目构建为名为`uDesktopMascot`的文件夹。

* 执行以下命令。
```sh
cd build
productbuild --component uDesktopMascot/uDesktopMascot.app /Applications ./uDesktopMascot_mac_installer.pkg
```
* 构建完成后，`build`文件夹中将生成`uDesktopMascot_mac_installer.pkg`。

## 制作者致谢
* 模型: “アオゾラ”様
* 背景音乐: MidraLab(eisuke)
* 软件图标: やむちゃ様

## 第三方公告

参见[NOTICE](./NOTICE.md)。

## 赞助商
- Luna
- uezo