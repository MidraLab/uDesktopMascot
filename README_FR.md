Certainly! Here is the translated text into French:

# uDesktopMascot

[![Version de Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Versions](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Test CI Unity](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Remarque** : Les langues ci-dessus (English, 中文, Español, Français) ont été générées par une traduction automatique via GPT-4o-mini. Pour la précision et les nuances de la traduction, veuillez vous référer au texte original (日本語).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Aperçu](#aperçu)
  * [Liste des fonctionnalités](#liste-des-fonctionnalités)
  * [Exécution sur macOS](#exécution-sur-macos)
  * [Exigences](#exigences)
  * [Licence](#licence)
  * [Matériel](#matériel)
  * [Crédits des créateurs](#crédits-des-créateurs)
  * [Avis de tiers](#avis-de-tiers)
  * [Sponsors](#sponsors)
<!-- TOC -->

## Aperçu

« uDesktopMascot » est un projet open source d'application de mascotte de bureau sur le thème de `la liberté de création`. Comme exemple d'une fonctionnalité, il est possible de charger des modèles au format VRM ou GLB/FBX et de les afficher sur le bureau. Vous pouvez également configurer librement les couleurs de l'interface graphique, telles que les menus et les fenêtres d'application, ainsi que les images d'arrière-plan.
Pour une liste détaillée des fonctionnalités, veuillez consulter la [liste des fonctionnalités](#liste-des-fonctionnalités).

![](Docs/Image/AppImage.png)

**Plateformes prises en charge**
* Windows 10/11
* macOS

## Liste des fonctionnalités

L'application dispose des fonctionnalités suivantes. Pour plus de détails, veuillez consulter la liste ci-dessous.

L'ajout d'actifs externes peut être réalisé en plaçant les fichiers dans le dossier StreamingAssets.

<details>

<summary>Modèles et animations</summary>

* Affichez les fichiers modèle que vous avez placés dans StreamingAssets.
  * Prend en charge les modèles au format VRM (1.x, 0.x).
  * Prend en charge les modèles au format GLB/GLTF. (Les animations ne sont pas prises en charge)
  * Prend en charge les modèles au format FBX. (Cependant, certaines modèles peuvent avoir des problèmes de chargement des textures et les animations ne sont pas prises en charge)
    * Les textures peuvent être chargées en les plaçant dans StreamingAssets/textures/.

</details>

<details>

<summary>Voix et BGM</summary>

* Charge et joue les fichiers audio que vous avez placés dans StreamingAssets/Voice/. En cas de plusieurs fichiers, ils seront lus de manière aléatoire.
  * Le son joué lors du clic est chargé et joué à partir des fichiers audio placés dans StreamingAssets/Voice/Click/. 
* Charge et joue les fichiers de musique placés dans StreamingAssets/BGM/. En cas de plusieurs fichiers, ils seront lus de manière aléatoire.
* Ajout de la voix par défaut du personnage.
  * La voix par défaut utilise l'audio de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Elle est jouée au démarrage de l'application, à leur fermeture et lors des clics. 

</details>

<details>

<summary>Configuration de l'application par fichier texte</summary>
Vous pouvez modifier la configuration de l'application à l'aide du fichier application_settings.txt.

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

* Vous pouvez configurer l'image d'arrière-plan et la couleur de fond de l'écran de menu.
  * L'image d'arrière-plan peut être chargée à partir des fichiers d'image placés dans StreamingAssets/Menu/. Les formats d'image pris en charge sont les suivants :
    * PNG
    * JPG (JPEG)
    * BMP
    * GIF (image fixe)
    * TGA
    * TIFF
  * La couleur de fond peut être spécifiée à l'aide d'un code couleur.

</details>

## Exécution sur macOS

Lors de l'exécution de l'application sur macOS, il se peut que GateKeeper bloque l'application. Dans ce cas, exécutez la commande suivante depuis le terminal :

```sh
xattr -r -c uDesktopMascot.app
```

## Exigences
* Unity 6000.0.31f1 (IL2CPP)

## Licence
* Le code est sous licence [Apache License 2.0](LICENSE).
* Les actifs suivants sont sous licence [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/) :
  * BGM
  * Modèles

## Matériel
* L'animation par défaut du personnage a été créée à l'aide des [données d'animation pour "VRM Oningyou Asobi"](https://fumi2kick.booth.pm/items/1655686). La distribution incluse dans le dépôt est confirmée.
* La police est [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). La police Noto Sans JP est redistribuée sous la [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Les droits d'auteur de la police appartiennent à l'auteur original (Google).
* La voix par défaut utilise l'audio de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). Son utilisation a été vérifiée auprès de COEIROINK au préalable.
* Les icônes des boutons utilisent [MingCute](https://github.com/MidraLab/MingCute).

## Crédits des créateurs
* Modèle : « Aozora »
* BGM : MidraLab (eisuke)
* Icône du logiciel : Yamu-cha

## Avis de tiers

Voir [NOTICE](./NOTICE.md).

## Sponsors
- Luna
- uezo