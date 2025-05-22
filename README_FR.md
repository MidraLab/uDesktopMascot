# uDesktopMascot

[![Version de Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Releases](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Unity Test CI](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)
[![Demander à DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/MidraLab/uDesktopMascot)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Remarque**: Les langues ci-dessus (English, 中文, Español, Français) ont été générées par une traduction automatique de GPT-4o-mini. Pour la précision et les nuances des traductions, veuillez vous référer au texte original (日本語).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Résumé](#résumé)
  * [Liste des fonctionnalités](#liste-des-fonctionnalités)
  * [Exécution sur macOS](#exécution-sur-macos)
  * [exigences](#exigences)
  * [licence](#licence)
  * [À propos des ressources](#à-propos-des-ressources)
  * [Comment créer un installateur](#comment-créer-un-installateur)
    * [Windows](#windows)
    * [macOS](#macos)
  * [Crédits des créateurs](#crédits-des-créateurs)
  * [Avis de tiers](#avis-de-tiers)
  * [parrain](#parrain)
<!-- TOC -->

## Résumé

« uDesktopMascot » est un projet open source d'application de mascotte de bureau sur le thème de la `liberté de création`. 
Parmi ses fonctionnalités, il est possible de charger et d'afficher des modèles aux formats VRM ou GLB/FBX sur le bureau. De plus, vous pouvez personnaliser les couleurs et les images de fond des menus et des fenêtres de l'application. 
Pour une liste complète des fonctionnalités, veuillez consulter [Liste des fonctionnalités](#liste-des-fonctionnalités).

![](Docs/Image/AppImage.png)

**Plateformes compatibles**
* Windows 10/11
* macOS

## Liste des fonctionnalités

L'application comprend les fonctionnalités suivantes. Veuillez consulter la liste ci-dessous pour plus de détails.

Vous pouvez ajouter des actifs externes en les plaçant dans le dossier StreamingAssets.

<details>

<summary>Modèles et animations</summary>

* Charge et affiche les fichiers modèles placés dans StreamingAssets.
  * Prend en charge les modèles au format VRM(1.x, 0.x).
  * Prend en charge les modèles au format GLB/GLTF. (Les animations ne sont pas prises en charge)
  * Prend en charge les modèles au format FBX. (Cependant, certaines textures peuvent ne pas se charger. Les animations ne sont pas prises en charge.)
    * Les textures peuvent être chargées en les plaçant dans StreamingAssets/textures/.
* Ajout de modèles VRM à partir de l'écran de sélection et d'ajout de modèles.
  * Ajout en spécifiant le chemin.
  * Ajout via la boîte de dialogue de sélection de fichiers.

</details>

<details>

<summary>Voix et BGM</summary>

* Charge et lit les fichiers audio placés dans SteamingAssets/Voice/. Si plusieurs fichiers sont présents, l'un d'eux sera joué au hasard.
  * Le son joué lors d'un clic est chargé à partir des fichiers audio placés dans StreamingAssets/Voice/Click/.
* Charge et lit les fichiers musicaux placés dans SteamingAssets/BGM/. Si plusieurs fichiers sont présents, l'un d'eux sera joué au hasard.
* Ajout d'une voix par défaut pour le personnage.
  * La voix par défaut utilise l'audio de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Elle sera jouée au démarrage de l'application, à la fermeture de l'application et lors d'un clic.

</details>

<details>

<summary>Configuration de l'application via un fichier texte</summary>
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

<summary>Menu</summary>

* Vous pouvez configurer l'image de fond et la couleur de fond du menu.
  * L'image de fond peut être chargée à partir des fichiers d'image placés dans StreamingAssets/Menu/. Les formats d'image pris en charge sont les suivants :
    * PNG
    * JPG(JPEG)
    * BMP
    * GIF (image fixe)
    * TGA
    * TIFF
  * La couleur de fond peut être spécifiée par un code couleur.
* Accessible via le menu :
  * Écran de sélection et d'ajout de modèles
  * Fonction de chat AI
  * Fonction LocalWeb
  * Paramètres de l'application
  * Quitter l'application
* En appuyant sur le bouton de réduction du menu, vous pouvez réduire l'application dans la zone de notification sur Windows.
  * L'application réduite peut être affichée à nouveau en cliquant sur l'icône dans la zone de notification.

</details>

## Exécution sur macOS

Lors de l'exécution de l'application sur macOS, l'application peut être bloquée par GateKeeper.
Dans ce cas, exécutez la commande suivante à partir du terminal :

```sh
xattr -r -c uDesktopMascot.app
```

## exigences
* Unity 6000.1.1f1 (IL2CPP)

## licence
* Le code est sous licence [Apache License 2.0](LICENSE).
* Les actifs suivants sont sous licence [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/) :
  * BGM
  * Modèles

## À propos des ressources
* Les animations par défaut du personnage sont créées à l'aide du [pack de données d'animation pour "VRM Puppet Play"](https://fumi2kick.booth.pm/items/1655686). Cela a été confirmé pour diffusion dans le référentiel.
* La police utilisée est [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). Elle est redistribuée sous la [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Les droits d'auteur de la police appartiennent à l'auteur original (Google).
* La voix par défaut utilise l'audio de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). Cela a été préalablement confirmé auprès de COEIROINK.
* L'icône des boutons utilise [MingCute](https://github.com/MidraLab/MingCute).

## Comment créer un installateur
### Windows
* Construisez dans Unity dans un dossier appelé `uDesktopMascot`.
```
uDesktopMasscot
└── build
    └── uDesktopMasscot
        ├── uDesktopMascot.exe
        └── README.txt
        ...
```

* Installez [Inno Setup](https://www.jrsoftware.org/isdl.php).

* Cliquez sur `More files` et sélectionnez le fichier `setup.iss` dans le projet.

![](Docs/Image/SetupIss-1.png)
* Une fois sélectionné, cliquez sur le bouton lecture.

![](Docs/Image/SetupIss-2.png)
* Une fois la construction terminée, l'installateur sera généré à la racine du projet.

### macOS
L'installateur ne peut être créé que sur un PC macOS.

* Construisez dans Unity dans un dossier `build/uDesktopMascot` sous le nom `uDesktopMascot`.
```
uDesktopMasscot
└── build
    └── uDesktopMascot
        ├── uDesktopMascot.app
        └── README.txt
```

* Exécutez la commande suivante :
```sh
cd build
productbuild --component uDesktopMascot/uDesktopMascot.app /Applications ./uDesktopMascot_mac_installer.pkg
```
* Une fois la construction terminée, `uDesktopMascot_mac_installer.pkg` sera généré dans le dossier `build`.

## Crédits des créateurs
* Modèle : « Aozora » 
* BGM : MidraLab (eisuke)
* Icône du logiciel : Yamu-cha

## Avis de tiers

Voir [NOTICE](./NOTICE.md).

## parrain
- Luna
- uezo