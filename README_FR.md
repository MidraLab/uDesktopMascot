# uDesktopMascot

[![Version Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Releases](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Remarque** : Les langues ci-dessus (English, 中文, Español, Français) ont été générées par une traduction automatique de GPT-4o-mini. Pour la précision de la traduction et les nuances, veuillez vous référer à l'original (日本語).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Aperçu](#aperçu)
  * [Liste des fonctionnalités](#liste-des-fonctionnalités)
  * [Exécution sur macOS](#exécution-sur-macos)
  * [Exigences](#exigences)
  * [Licence](#licence)
  * [À propos des matériels](#à-propos-des-matériels)
  * [Crédits des créateurs](#crédits-des-créateurs)
  * [Mentions légales de tiers](#mentions-légales-de-tiers)
  * [Sponsoring](#sponsoring)
<!-- TOC -->

## Aperçu

"uDesktopMascot" est un projet open source qui affiche des personnages sur le bureau et joue des réactions et des sons en fonction des interactions de l'utilisateur. Ce projet a été développé en utilisant Unity et prend en charge les personnages au format VRM, vous permettant de profiter facilement de vos personnages préférés sur le bureau.

**Plateformes prises en charge**
* Windows 10/11
* macOS

## Liste des fonctionnalités

L'application dispose des fonctionnalités suivantes. Pour plus de détails, veuillez consulter la liste ci-dessous.

L'ajout d'actifs externes peut se faire en les plaçant dans le dossier StreamingAssets.

<details>

<summary>Modèles et animations</summary>
* Charge et affiche des fichiers modèles placés dans StreamingAssets.
  * Prise en charge des modèles au format VRM (1.x, 0.x).
  * Prise en charge des modèles au format GLB/GLTF. (Les animations ne sont pas prises en charge)
  * Prise en charge des modèles au format FBX. (Cependant, certains modèles peuvent avoir des textures qui ne se chargent pas. Les animations ne sont pas prises en charge)
    * Les textures peuvent être chargées en les plaçant dans StreamingAssets/textures/.

</details>

<details>

<summary>Voix et BGM</summary>
* Charge et joue des fichiers audio placés dans SteamingAssets/Voice/. En cas de multiples fichiers, la lecture se fait aléatoirement.
  * Les sons joués lors des clics sont chargés à partir des fichiers audio placés dans StreamingAssets/Voice/Click/. 
* Charge et joue des fichiers musicaux placés dans SteamingAssets/BGM/. En cas de multiple fichiers, la lecture se fait aléatoirement.
* Ajout de la voix par défaut du personnage
  * La voix par défaut utilise les sons de [COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Elle est jouée au démarrage de l'application, à la sortie de l'application et lors des clics.

</details>

<details>

<summary>Paramètres de l'application via un fichier texte</summary>
Vous pouvez modifier les paramètres de l'application via un fichier application_settings.txt.

La structure du fichier de configuration est la suivante :

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

<summary>Écran de menu</summary>

* Vous pouvez configurer l'image de fond et la couleur de fond de l'écran de menu.
  * L'image de fond peut être chargée à partir d'un fichier d'image placé dans StreamingAssets/Menu/. Les formats d'image pris en charge sont les suivants :
    * PNG
    * JPG (JPEG)
    * BMP
    * GIF (image fixe)
    * TGA
    * TIFF
  * La couleur de fond peut être spécifiée par un code couleur.

</details>

## Exécution sur macOS

Lorsque vous exécutez l'application sur macOS, il se peut que GateKeeper bloque l'application.
Dans ce cas, exécutez la commande suivante dans le terminal :

```sh
xattr -r -c uDesktopMascot.app
```

## Exigences
* Unity 6000.0.31f1 (IL2CPP)

## Licence
* Le code est sous [Apache License 2.0](LICENSE).
* Les actifs suivants sont sous [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/) :
  * BGM
  * Modèles

## À propos des matériels
* Les animations par défaut des personnages ont été créées à l'aide des [données d'animation pour "VRM お人形遊び"](https://fumi2kick.booth.pm/items/1655686). Cela a été confirmé pour la distribution dans le dépôt.
* La police utilisée est [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). La redistribution de la police Noto Sans JP est faite conformément à la [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Les droits d'auteur de la police appartiennent à l'auteur original (Google).
* La voix par défaut est basée sur les sons de [COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan). J'ai préalablement obtenu la confirmation de COEIROINK concernant l'utilisation.
* Les icônes des boutons proviennent de [MingCute](https://github.com/MidraLab/MingCute).

## Crédits des créateurs
* Modèle : "アオゾラ"
* BGM : MidraLab (eisuke)
* Icônes de logiciels : やむちゃ

## Mentions légales de tiers

Voir [NOTICE](./NOTICE.md).

## Sponsoring
- Luna
- uezo