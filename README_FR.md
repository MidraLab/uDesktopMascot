# uDesktopMascot

[![Version de Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Sorties](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Remarque**: Les langues ci-dessus (English, 中文, Español, Français) ont été générées par une traduction automatique via GPT-4o-mini. Pour la précision et les nuances de la traduction, veuillez vous référer au texte original (日本語).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Aperçu](#aperçu)
  * [Liste des fonctionnalités](#liste-des-fonctionnalités)
  * [Exécution sur macOS](#exécution-sur-macos)
  * [exigences](#exigences)
  * [licence](#licence)
  * [À propos des matériaux](#à-propos-des-matériaux)
  * [Crédits des créateurs](#crédits-des-créateurs)
  * [Informations sur les tiers](#informations-sur-les-tiers)
  * [sponsor](#sponsor)
<!-- TOC -->

## Aperçu

"uDesktopMascot" est un projet open source qui affiche un personnage sur le bureau et joue des réactions et des voix en fonction des interactions de l'utilisateur. Ce projet est développé avec Unity et prend en charge des personnages au format VRM, vous permettant de profiter facilement de votre personnage préféré sur le bureau.

**Plateformes prises en charge**
* Windows 10/11
* macOS

## Liste des fonctionnalités

L'application propose les fonctionnalités suivantes. Veuillez consulter la liste ci-dessous pour plus de détails.

L'ajout de ressources externes peut être réalisé en plaçant des fichiers dans le dossier StreamingAssets.

<details>

<summary>Modèles et animations</summary>
* Charge et affiche des fichiers de modèle placés dans StreamingAssets.
  * Prend en charge des modèles au format VRM (1.x, 0.x).
  * Prend en charge des modèles au format GLB/GLTF. (Les animations ne sont pas prises en charge)
  * Prend en charge des modèles au format FBX. (Cependant, certains modèles peuvent ne pas charger les textures. De plus, les animations ne sont pas prises en charge.)
    * Les textures peuvent être chargées en les plaçant dans StreamingAssets/textures/.

</details>

<details>

<summary>Voix et BGM</summary>
* Charge et joue des fichiers audio placés dans SteamingAssets/Voice/. S'il y en a plusieurs, ils seront joués aléatoirement.
  * Les sons joués lors d'un clic sont chargés à partir des fichiers audio placés dans StreamingAssets/Voice/Click/.
* Charge et joue des fichiers musicaux placés dans SteamingAssets/BGM/. S'il y en a plusieurs, ils seront joués aléatoirement.
* Ajout de la voix par défaut du personnage
  * La voix par défaut utilise l'audio de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Elle est jouée au démarrage de l'application, à la fermeture de l'application et lors des clics.

</details>

<details>

<summary>Configuration de l'application par fichier texte</summary>
Vous pouvez modifier les paramètres de l'application à l'aide du fichier application_settings.txt.

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

## Exécution sur macOS

Lors de l'exécution de l'application sur macOS, il se peut que l'application soit bloquée par GateKeeper.
Dans ce cas, exécutez la commande suivante depuis le terminal :

```sh
xattr -r -c uDesktopMascot.app
```

## exigences
* Unity 6000.0.31f1 (IL2CPP)

## licence
* Le code est sous [Apache License 2.0](LICENSE).
* Les ressources suivantes sont sous [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/) :
  * BGM
  * Modèles

## À propos des matériaux
* Les animations par défaut des personnages sont créées à l'aide de [l'ensemble de données d'animation pour "VRM Omoidashi"](https://fumi2kick.booth.pm/items/1655686). Nous avons vérifié la distribution de celles-ci dans le dépôt.
* La police utilisée est [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). Elle est redistribuée sous la [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Les droits d'auteur de la police appartiennent à l'auteur d'origine (Google).
* La voix par défaut utilisée provient de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). L'utilisation a été préalablement vérifiée auprès de COEIROINK.
* Les icônes de bouton sont issues de [MingCute](https://github.com/MidraLab/MingCute).

## Crédits des créateurs
* Modèle : "Aozora" 
* BGM : MidraLab (eisuke)
* Icône du logiciel : Yamucha

## Informations sur les tiers

Voir [NOTICE](./NOTICE.md).

## sponsor
- Luna
- uezo