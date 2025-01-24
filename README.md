# uDesktopMascot

[![Unity Version](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Releases](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**注意**: 上記の言語（English、中文、Español、Français）は、GPT-4o-miniによる自動翻訳で生成されています。翻訳の精度やニュアンスに関しては、原文（日本語）をご参照ください。

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [概要](#概要)
  * [機能一覧](#機能一覧)
  * [requirements](#requirements)
  * [license](#license)
  * [素材について](#素材について)
  * [制作者クレジット](#制作者クレジット)
  * [3rd Party Notices](#3rd-party-notices)
  * [sponsor](#sponsor)
<!-- TOC -->

## 概要

「uDesktopMascot」は、デスクトップ上にキャラクターを表示し、ユーザーのインタラクションに応じた反応や音声を再生するオープンソースのプロジェクトです。このプロジェクトは、Unityを用いて開発されており、VRM形式のキャラクターをサポートしていますので、自分の好きなキャラクターを簡単にデスクトップ上で楽しむことができます。

**対応プラットフォーム**
* Windows 10/11
* macOS

## 機能一覧

アプリには以下の機能が実装されています。詳細は以下のリストを参照してください。

外部アセットの追加は、StreamingAssetsフォルダに配置することで実現できます。

<details>

<summary>モデル・アニメーション</summary>
* StreamingAssetsに配置した任意モデルファイルを読み込んで表示します。
  * VRM(1.x, 0.x)形式のモデルをサポートしています。
  * GLB/GLTF形式のモデルをサポートしています。(アニメーションは対応していません)
  * FBX形式のモデルをサポートしています。(ただし一部のモデルではテクスチャーがロードができません。またアニメーションは対応していません)
    * テクスチャーは StreamingAssets/textures/ に配置することで読み込むことができます。

</details>

<details>

<summary>ボイス・BGM</summary>
* SteamingAssets/Voice/以下に配置した音声ファイルを読み込んで再生します。複数ある場合は、ランダムで再生します。
  * クリック時に再生される音声は、StreamingAssets/Voice/Click/に配置した音声ファイルを読み込んで再生します。 
* SteamingAssets/BGM/以下に配置した音楽ファイルを読み込んで再生します。複数ある場合は、ランダムで再生します。
* キャラクターのデフォルトのボイスの追加
  * デフォルトのボイスは、[COEIROINK:つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan)の音声を使用しています。
  * アプリ起動時、アプリ終了時、クリック時に再生されます。

</details>

<details>

<summary>テキストファイルによるアプリケーション設定</summary>
application_settings.txtファイルにより、アプリケーションの設定を変更できます。

設定ファイルの構造は以下になっています

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

## requirements
* Unity 6000.0.31f1(IL2CPP)

## license
* コードは[Apache License 2.0](LICENSE)に基づいてライセンスされています。
* 以下のアセットは、[CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/)に基づいてライセンスされています。
  * BGM
  * モデル

## 素材について
* デフォルトのキャラクターアニメーションは、[『VRMお人形遊び』用アニメーションデータ詰め合わせ](https://fumi2kick.booth.pm/items/1655686)を用いて作成されています。リポジトリに含めて配布することに関して、確認済みです。
* フォントは[Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan)です。[SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan)に基づきNoto Sans JPフォントを再配布するものです。フォントの著作権は元の作者（Google）にあります。
* デフォルトボイスは、[COEIROINK:つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan)の音声を使用しています。使用方法については、COEIROINK側に事前に確認済みです
* ボタンアイコンは、[MingCute](https://github.com/MidraLab/MingCute)を使用しています。

## 制作者クレジット
* モデル: 「アオゾラ」様
* BGM: MidraLab(eisuke)
* ソフトウェアアイコン: やむちゃ様

## 3rd Party Notices

See [NOTICE](./NOTICE.md).

## sponsor
- Luna
- uezo