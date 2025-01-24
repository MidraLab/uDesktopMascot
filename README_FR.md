Here is the translation of the provided text into French:

# uDesktopMascot

[![Version Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Releases](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Remarque** : Les langues ci-dessus (English, 中文, Español, Français) ont été générées par traduction automatique via GPT-4o-mini. Pour la précision et les nuances de la traduction, veuillez vous référer au texte original (日本語).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Résumé](#résumé)
  * [Liste des fonctionnalités](#liste-des-fonctionnalités)
  * [Pré-requis](#pré-requis)
  * [Licence](#licence)
  * [À propos des matériaux](#à-propos-des-matériaux)
  * [Crédits des créateurs](#crédits-des-créateurs)
  * [Mentions légales des tiers](#mentions-légales-des-tiers)
  * [Sponsoring](#sponsoring)
<!-- TOC -->

## Résumé

「uDesktopMascot」 est un projet open source qui permet d'afficher des personnages sur le bureau et de reproduire des réactions et des sons en fonction des interactions de l'utilisateur. Ce projet a été développé en utilisant Unity et supporte les personnages au format VRM, vous permettant de profiter facilement de vos personnages préférés sur votre bureau.

**Plateformes prises en charge**
* Windows 10/11
* macOS

## Liste des fonctionnalités

L'application comprend les fonctionnalités suivantes. Pour plus de détails, veuillez consulter la liste ci-dessous.

L'ajout d'assets externes peut être réalisé en les plaçant dans le dossier StreamingAssets.

<details>

<summary>Modèles et animations</summary>
* Charge et affiche des fichiers modèles placés dans le dossier StreamingAssets.
  * Supporte les modèles au format VRM (1.x, 0.x).
  * Supporte les modèles au format GLB/GLTF. (Les animations ne sont pas supportées)
  * Supporte les modèles au format FBX. (Certaines textures peuvent ne pas se charger, et les animations ne sont pas supportées)
    * Les textures peuvent être chargées en les plaçant dans StreamingAssets/textures/.

</details>

<details>

<summary>Voix et BGM</summary>
* Charge et reproduit les fichiers audio placés dans SteamingAssets/Voice/. Si plusieurs fichiers sont présents, l'un d'eux sera joué au hasard.
  * Les sons joués lors d'un clic sont chargés à partir des fichiers audio placés dans StreamingAssets/Voice/Click/.
* Charge et reproduit les fichiers musicaux placés dans SteamingAssets/BGM/. Si plusieurs fichiers sont présents, l'un d'eux sera joué au hasard.
* Ajout d'une voix par défaut pour le personnage
  * La voix par défaut utilise le son de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Elle est jouée au démarrage de l'application, à la fermeture de l'application et lors d'un clic.

</details>

<details>

<summary>Configuration de l'application via fichier texte</summary>
Le fichier application_settings.txt permet de modifier les paramètres de l'application.

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

## Pré-requis
* Unity 6000.0.31f1(IL2CPP)

## Licence
* Le code est sous [Apache License 2.0](LICENSE).
* Les assets suivants sont sous [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/) :
  * BGM
  * Modèles

## À propos des matériaux
* Les animations de personnages par défaut sont créées à partir de [“VRM Oningyou Asobi” Animation Data Set](https://fumi2kick.booth.pm/items/1655686). Il a été confirmé qu'elles peuvent être distribuées avec le dépôt.
* La police utilisée est [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). La redistribution de la police Noto Sans JP est basée sur la [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Les droits d'auteur de la police appartiennent à l'auteur original (Google).
* La voix par défaut provient de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). Il a été préalablement confirmé avec COEIROINK sur l'utilisation.
* Les icônes de boutons sont réalisées avec [MingCute](https://github.com/MidraLab/MingCute).

## Crédits des créateurs
* Modèle : « Aozora »
* BGM : MidraLab (eisuke)
* Icône du logiciel : Yamucha

## Mentions légales des tiers

Voir [NOTICE](./NOTICE.md).

## Sponsoring
- Luna
- uezo