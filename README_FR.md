# uDesktopMascot

[![Version Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Versions](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Test CI Unity](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Remarque** : Les langues ci-dessus (English, 中文, Español, Français) ont été générées par traduction automatique de GPT-4o-mini. Pour la précision et les nuances de la traduction, veuillez vous référer au texte original (japonais).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Aperçu](#aperçu)
  * [Liste des fonctionnalités](#liste-des-fonctionnalités)
  * [Exécution sur macOS](#exécution-sur-macos)
  * [Exigences](#exigences)
  * [Licence](#licence)
  * [À propos des ressources](#à-propos-des-ressources)
  * [Crédit des créateurs](#crédit-des-créateurs)
  * [Avis de tiers](#avis-de-tiers)
  * [Sponsors](#sponsors)
<!-- TOC -->

## Aperçu

"uDesktopMascot" est un projet open source qui affiche un personnage sur le bureau et joue des réactions ou des sons en fonction des interactions de l'utilisateur. Ce projet a été développé avec Unity et prend en charge des personnages au format VRM, permettant ainsi de profiter facilement de vos personnages préférés sur votre bureau.

![](Docs/Image/AppImage.png)

**Plateformes prises en charge**
* Windows 10/11
* macOS

## Liste des fonctionnalités

L'application dispose des fonctionnalités suivantes. Veuillez consulter la liste ci-dessous pour plus de détails.

L'ajout d'actifs externes peut être effectué en plaçant des fichiers dans le dossier StreamingAssets.

<details>

<summary>Modèles et animations</summary>

* Charge et affiche des fichiers de modèle placés dans StreamingAssets.
  * Prend en charge les modèles au format VRM (1.x, 0.x).
  * Prend en charge les modèles au format GLB/GLTF (les animations ne sont pas prises en charge).
  * Prend en charge les modèles au format FBX (mais certains modèles peuvent ne pas charger les textures. Les animations ne sont pas prises en charge).
    * Les textures peuvent être chargées en les plaçant dans StreamingAssets/textures/.

</details>

<details>

<summary>Voix et BGM</summary>

* Charge et joue des fichiers audio placés dans SteamingAssets/Voice/. S'il y en a plusieurs, ils seront joués au hasard.
  * Les sons joués lors des clics sont chargés à partir de fichiers audio placés dans StreamingAssets/Voice/Click/.
* Charge et joue des fichiers musicaux placés dans SteamingAssets/BGM/. S'il y en a plusieurs, ils seront joués au hasard.
* Ajout d'une voix par défaut pour le personnage :
  * La voix par défaut utilise des voix de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Elle est jouée lors du lancement de l'application, de la fermeture de l'application et des clics.

</details>

<details>

<summary>Configuration de l'application par fichier texte</summary>
Vous pouvez changer les paramètres de l'application via le fichier application_settings.txt.

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

* Vous pouvez définir l'image de fond et la couleur de fond de l'écran de menu.
  * L'image de fond peut être chargée à partir de fichiers image placés dans StreamingAssets/Menu/. Les formats d'image pris en charge sont les suivants :
    * PNG
    * JPG (JPEG)
    * BMP
    * GIF (images fixes)
    * TGA
    * TIFF
  * Vous pouvez spécifier une couleur de fond en utilisant un code couleur.

</details>

## Exécution sur macOS

Lors de l'exécution de l'application sur macOS, il est possible que l'application soit bloquée par GateKeeper.
Dans ce cas, exécutez la commande suivante depuis le terminal :

```sh
xattr -r -c uDesktopMascot.app
```

## Exigences
* Unity 6000.0.31f1 (IL2CPP)

## Licence
* Le code est sous licence [Apache License 2.0](LICENSE).
* Les ressources suivantes sont sous licence [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/) :
  * BGM
  * Modèles

## À propos des ressources
* L'animation du personnage par défaut est créée à partir des [données d'animation pour "VRM Oningyou Asobi"](https://fumi2kick.booth.pm/items/1655686). En ce qui concerne la distribution dans le référentiel, cela a été vérifié.
* La police utilisée est [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). Elle est redistribuée sous la [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Les droits d'auteur de la police appartiennent à l'auteur original (Google).
* La voix par défaut utilise des voix de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). L'utilisation a été préalablement vérifiée auprès de COEIROINK.
* Les icônes des boutons utilisent [MingCute](https://github.com/MidraLab/MingCute).

## Crédit des créateurs
* Modèle : "Aozora" 
* BGM : MidraLab (eisuke)
* Icône logicielle : Yamucha

## Avis de tiers

Voir [NOTICE](./NOTICE.md).

## Sponsors
- Luna
- uezo