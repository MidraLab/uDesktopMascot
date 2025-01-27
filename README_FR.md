# uDesktopMascot

[![Version Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Releases](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Unity Test CI](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Avertissement**: Les langues ci-dessus (English, 中文, Español, Français) ont été générées par une traduction automatique de GPT-4o-mini. Pour la précision et les nuances de la traduction, veuillez vous référer au texte original (japonais).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Aperçu](#aperçu)
  * [Liste des fonctionnalités](#liste-des-fonctionnalités)
  * [Exécution sur macOS](#exécution-sur-macos)
  * [Exigences](#exigences)
  * [Licence](#licence)
  * [Matériaux](#matériaux)
  * [Crédits des créateurs](#crédits-des-créateurs)
  * [Avis sur les tiers](#avis-sur-les-tiers)
  * [Commanditaire](#commanditaire)
<!-- TOC -->

## Aperçu

« uDesktopMascot » est un projet open source qui affiche des personnages sur le bureau et joue des réactions et des sons en fonction des interactions des utilisateurs. Ce projet est développé avec Unity et prend en charge des personnages au format VRM, vous permettant de profiter facilement de vos personnages préférés sur votre bureau.

**Plateformes prises en charge**
* Windows 10/11
* macOS

## Liste des fonctionnalités

L'application comprend les fonctionnalités suivantes. Pour plus de détails, veuillez consulter la liste ci-dessous.

L'ajout d'actifs externes peut être réalisé en les plaçant dans le dossier StreamingAssets.

<details>

<summary>Modèles et animations</summary>
* Charge et affiche des fichiers de modèle placés dans StreamingAssets.
  * Prend en charge les modèles au format VRM (1.x, 0.x).
  * Prend en charge les modèles au format GLB/GLTF. (pas de support pour l'animation).
  * Prend en charge les modèles au format FBX. (certains modèles peuvent ne pas réussir à charger des textures. Pas de support pour l'animation).
    * Les textures peuvent être chargées en les plaçant dans StreamingAssets/textures/.

</details>

<details>

<summary>Voix et musique de fond</summary>
* Charge et joue des fichiers audio placés sous SteamingAssets/Voice/. Si plusieurs fichiers sont présents, ils seront joués aléatoirement.
  * Le son joué lors du clic est chargé à partir de fichiers audio placés dans StreamingAssets/Voice/Click/.
* Charge et joue des fichiers musicaux placés sous SteamingAssets/BGM/. S'il y en a plusieurs, ils seront joués aléatoirement.
* Ajout d'une voix par défaut pour le personnage
  * La voix par défaut utilise les sons de [COEIROINK : Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Elle est jouée au démarrage de l'application, à la fermeture de l'application et lors des clics.

</details>

<details>

<summary>Configuration de l'application via un fichier texte</summary>
Vous pouvez modifier les paramètres de l'application via le fichier application_settings.txt.

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
  * L'image de fond peut être chargée à partir des fichiers image placés dans StreamingAssets/Menu/. Les formats d'images pris en charge sont les suivants :
    * PNG
    * JPG (JPEG)
    * BMP
    * GIF (image fixe)
    * TGA
    * TIFF
  * La couleur de fond peut être spécifiée en utilisant un code couleur.

</details>

## Exécution sur macOS

Lors de l'exécution de l'application sur macOS, GateKeeper peut bloquer l'application. Dans ce cas, exécutez la commande suivante depuis le terminal.

```sh
xattr -r -c uDesktopMascot.app
```

## Exigences
* Unity 6000.0.31f1(IL2CPP)

## Licence
* Le code est sous [Apache License 2.0](LICENSE).
* Les actifs suivants sont sous la licence [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/) :
  * BGM
  * Modèle

## Matériaux
* L'animation de personnage par défaut est créée à partir de [la collection de données d'animation pour « VRM Oningyou Asobi »](https://fumi2kick.booth.pm/items/1655686). Cela a été vérifié pour distribution dans le dépôt.
* La police utilisée est [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). Elle est redistribuée sous la [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Les droits d'auteur de la police appartiennent à l'auteur original (Google).
* La voix par défaut provient de [COEIROINK : Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). L'utilisation a été préalablement vérifiée auprès de COEIROINK.
* Les icônes de boutons utilisent [MingCute](https://github.com/MidraLab/MingCute).

## Crédits des créateurs
* Modèle : « Aozora » 
* BGM : MidraLab(eisuke)
* Icône de logiciel : Yamucha

## Avis sur les tiers

Voir [NOTICE](./NOTICE.md).

## Commanditaire
- Luna
- uezo