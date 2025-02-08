Here is the translation of the provided text into French:

# uDesktopMascot

[![Version Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Versions](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Test CI Unity](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Remarque** : Les langues ci-dessus (English, 中文, Español, Français) ont été générées par une traduction automatique de GPT-4o-mini. Pour la précision et les nuances de la traduction, veuillez vous référer au texte original (日本語).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Aperçu](#aperçu)
  * [Liste des fonctionnalités](#liste-des-fonctionnalités)
  * [Exécution sous macOS](#exécution-sous-macos)
  * [exigences](#exigences)
  * [licence](#licence)
  * [À propos des ressources](#à-propos-des-ressources)
  * [Crédits des créateurs](#crédits-des-créateurs)
  * [Mentions légales des tiers](#mentions-légales-des-tiers)
  * [parrains](#parrains)
<!-- TOC -->

## Aperçu

« uDesktopMascot » est un projet open source d'application de mascotte de bureau sur le thème de `la liberté de création`. Une des fonctionnalités inclut la possibilité de charger des modèles au format VRM ou GLB/FBX et de les afficher sur le bureau. De plus, vous pouvez personnaliser les couleurs et les images de fond des écrans de menu et des fenêtres d'application. Pour une liste de fonctionnalités détaillée, consultez [Liste des fonctionnalités](#liste-des-fonctionnalités).

![](Docs/Image/AppImage.png)

**Plateformes prises en charge**
* Windows 10/11
* macOS

## Liste des fonctionnalités

L'application implémente les fonctionnalités suivantes. Consultez la liste ci-dessous pour plus de détails.

L'ajout d'assets externes peut être réalisé en les plaçant dans le dossier StreamingAssets.

<details>

<summary>Modèles et animations</summary>

* Affiche des fichiers de modèles dans le dossier StreamingAssets.
  * Prend en charge les modèles au format VRM (1.x, 0.x).
  * Prend en charge les modèles au format GLB/GLTF (les animations ne sont pas prises en charge).
  * Prend en charge les modèles au format FBX (certaines textures peuvent ne pas se charger. Les animations ne sont pas prises en charge).
    * Les textures peuvent être chargées en les plaçant dans StreamingAssets/textures/.

</details>

<details>

<summary>Voix et BGM</summary>

* Charge et joue les fichiers audio placés sous SteamingAssets/Voice/. Si plusieurs fichiers sont présents, un fichier est sélectionné au hasard.
  * Les sons joués lors d'un clic sont chargés à partir des fichiers audio placés dans StreamingAssets/Voice/Click/.
* Charge et joue les fichiers musicaux placés sous SteamingAssets/BGM/. Si plusieurs fichiers sont présents, un fichier est sélectionné au hasard.
* Ajout de la voix par défaut du personnage
  * La voix par défaut utilise l'audio de [COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Elle est jouée lors du lancement de l'application, de la fermeture de l'application et lors des clics.

</details>

<details>

<summary>Paramètres de l'application via un fichier texte</summary>
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
  * L'image de fond peut être chargée à partir de fichiers d'image placés dans StreamingAssets/Menu/. Les formats d'image pris en charge sont les suivants :
    * PNG
    * JPG (JPEG)
    * BMP
    * GIF (image fixe)
    * TGA
    * TIFF
  * La couleur de fond peut être spécifiée avec un code couleur.

</details>

## Exécution sous macOS

Lorsque vous exécutez l'application sur macOS, il se peut queGateKeeper bloque l'application. Dans ce cas, exécutez la commande suivante dans le terminal :

```sh
xattr -r -c uDesktopMascot.app
```

## exigences
* Unity 6000.0.31f1 (IL2CPP)

## licence
* Le code est sous [Apache License 2.0](LICENSE).
* Les assets suivants sont sous [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/) :
  * BGM
  * Modèles

## À propos des ressources
* Les animations de personnage par défaut sont créées à l'aide d'un ensemble de données d'animation pour [“VRMお人形遊び”](https://fumi2kick.booth.pm/items/1655686). L'inclusion dans le dépôt a été vérifiée.
* La police utilisée est [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). La redistribution de la police Noto Sans JP est sous [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Les droits d'auteur de cette police appartiennent à l'auteur original (Google).
* La voix par défaut est celle de [COEIROINK: つくよみちゃん](https://coeiroink.com/character/audio-character/tsukuyomi-chan). Son utilisation a été préalablement confirmée avec COEIROINK.
* Les icônes de bouton utilisent [MingCute](https://github.com/MidraLab/MingCute).

## Méthode de création d'installateur
### Windows
* Builder le projet Unity dans un dossier nommé `uDesktopMascot`.

* Installer [Inno Setup](https://www.jrsoftware.org/isdl.php).
  
* Cliquez sur `More files`, puis sélectionnez le fichier `setup.iss` dans le projet.
  
![](Docs/Image/SetupIss-1.png)
* Après sélection, cliquez sur le bouton de lecture.
  
![](Docs/Image/SetupIss-2.png)
* Une fois la construction terminée, l'installateur sera généré à la racine du projet.

### macOS
Seuls les PC macOS peuvent créer l'installateur.

* Builder le projet Unity dans un dossier nommé `uDesktopMascot`.

* Exécutez la commande suivante :
```sh
cd build
productbuild --component uDesktopMascot/uDesktopMascot.app /Applications ./uDesktopMascot_mac_installer.pkg
```
* Une fois la construction terminée, le fichier `uDesktopMascot_mac_installer.pkg` sera généré dans le dossier `build`.

## Crédits des créateurs
* Modèle : « アオゾラ » 
* BGM : MidraLab (eisuke)
* Icône du logiciel : やむちゃ 

## Mentions légales des tiers

Voir [NOTICE](./NOTICE.md).

## parrains
- Luna
- uezo