# uDesktopMascot

[![Version Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Versions](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Test CI Unity](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Remarque** : Les langues ci-dessus (English, 中文, Español, Français) ont été générées par une traduction automatique via GPT-4o-mini. Veuillez vous référer au texte original (日本語) pour la précision et le nuance de la traduction.

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Résumé](#résumé)
  * [Liste des fonctionnalités](#liste-des-fonctionnalités)
  * [Exécution sur macOS](#exécution-sur-macos)
  * [exigences](#exigences)
  * [licence](#licence)
  * [À propos des ressources](#à-propos-des-ressources)
  * [Méthode de création de l'installateur](#méthode-de-création-de-linstallateur)
    * [Windows](#windows)
    * [macOS](#macos)
  * [Crédits des créateurs](#crédits-des-créateurs)
  * [Avis de tiers](#avis-de-tiers)
  * [parrain](#parrain)
<!-- TOC -->

## Résumé

« uDesktopMascot » est un projet open source d'application de mascotte de bureau sur le thème de `la liberté de création`. En tant qu'exemple d'une fonctionnalité, il est possible de charger des modèles au format VRM ou GLB/FBX et de les afficher sur le bureau. Il est également possible de définir librement les couleurs et les images d'arrière-plan de l'interface graphique, telles que l'écran de menu et les fenêtres d'application. Pour une liste détaillée des fonctionnalités, veuillez consulter [Liste des fonctionnalités](#liste-des-fonctionnalités).

![](Docs/Image/AppImage.png)

**Plateformes prises en charge**
* Windows 10/11
* macOS

## Liste des fonctionnalités

L'application comprend les fonctionnalités suivantes. Pour plus de détails, veuillez consulter la liste ci-dessous.

L'ajout de ressources externes peut être réalisé en les plaçant dans le dossier StreamingAssets.

<details>

<summary>Modèles et animations</summary>

* Charge et affiche un fichier modèle placé dans StreamingAssets.
  * Il prend en charge les modèles au format VRM(1.x, 0.x).
  * Il prend en charge les modèles au format GLB/GLTF (les animations ne sont pas prises en charge).
  * Il prend en charge les modèles au format FBX (mais certaines textures peuvent ne pas se charger. Les animations ne sont pas prises en charge).
    * Les textures peuvent être chargées en les plaçant dans StreamingAssets/textures/.
* Écran de sélection d'un modèle et ajout de modèles VRM.
  * Ajout en spécifiant le chemin.
  * Ajout à partir de la boîte de dialogue de sélection de fichiers.

</details>

<details>

<summary>Voix et BGM</summary>

* Charge et joue des fichiers audio placés dans StreamingAssets/Voice/. Si plusieurs fichiers existent, ils sont joués de manière aléatoire.
  * Les sons joués au clic sont chargés à partir des fichiers audio placés dans StreamingAssets/Voice/Click/.
* Charge et joue des fichiers musicaux placés dans StreamingAssets/BGM/. S'il y en a plusieurs, ils sont également joués au hasard.
* Ajout d'une voix par défaut pour le personnage.
  * La voix par défaut utilise des enregistrements de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Elle est jouée au démarrage de l'application, à la fermeture de l'application et au clic.

</details>

<details>

<summary>Configuration de l'application via un fichier texte</summary>
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

<details>

<summary>Écran de menu</summary>

* Vous pouvez configurer l'image de fond et la couleur de fond de l'écran de menu.
  * L'image de fond peut être chargée à partir d'un fichier image placé dans StreamingAssets/Menu/. Les formats d'image pris en charge sont :
    * PNG
    * JPG(JPEG)
    * BMP
    * GIF (image fixe)
    * TGA
    * TIFF
  * La couleur de fond peut être spécifiée par un code couleur.
* À partir de l'écran de menu, vous pouvez accéder aux fonctionnalités suivantes :
  * Écran de sélection et d'ajout de modèles
  * Fonction de chat AI
  * Fonction LocalWeb
  * Paramètres de l'application
  * Fermeture de l'application
* En appuyant sur le bouton de réduction de l'écran de menu, vous pouvez réduire l'application dans la zone de notification uniquement sur Windows.
  * L'application réduite peut être à nouveau affichée en cliquant sur l'icône de la zone de notification.

</details>

## Exécution sur macOS

Lorsque vous exécutez l'application sur macOS, il se peut que l'application soit bloquée par GateKeeper. Dans ce cas, exécutez la commande suivante depuis le terminal :

```sh
xattr -r -c uDesktopMascot.app
```

## exigences
* Unity 6000.1.1f1(IL2CPP)

## licence
* Le code est sous [Apache License 2.0](LICENSE).
* Les ressources suivantes sont sous [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/).
  * BGM
  * Modèle

## À propos des ressources
* L'animation par défaut du personnage a été créée à l'aide des [données d'animation pour 'VRM Dolls' disponibles ici](https://fumi2kick.booth.pm/items/1655686). Nous avons vérifié qu'elle peut être distribuée dans le dépôt.
* La police utilisée est [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). La redistribution de la police Noto Sans JP est conforme à la [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan) Les droits d'auteur de la police appartiennent à l'auteur original (Google).
* La voix par défaut est celle de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). Nous avons préalablement confirmé auprès de COEIROINK son utilisation.
* Les icônes des boutons proviennent de [MingCute](https://github.com/MidraLab/MingCute).

## Méthode de création de l'installateur
### Windows
* Construire dans Unity dans un dossier `build` sous le nom `uDesktopMascot`.
```
uDesktopMasscot
└── build
    └── uDesktopMasscot
        ├── uDesktopMascot.exe
        └── README.txt
        ...
```

* Installer [Inno Setup](https://www.jrsoftware.org/isdl.php).
  
* Ouvrez-le, cliquez sur `More files` et sélectionnez le fichier `setup.iss` dans le projet.
  
![](Docs/Image/SetupIss-1.png)
* Après avoir fait votre sélection, cliquez sur le bouton de lecture.
  
![](Docs/Image/SetupIss-2.png)
* Une fois le build terminé, l'installateur sera généré à la racine du projet.

### macOS
L'installateur ne peut être créé que sur un PC macOS.

* Construire dans Unity dans un dossier `build/uDesktopMascot` sous le nom `uDesktopMascot`.
```
uDesktopMasscot
└── build
    └── uDesktopMasscot
        ├── uDesktopMascot.app
        └── README.txt
```

* Exécutez la commande suivante.
```sh
cd build
productbuild --component uDesktopMascot/uDesktopMascot.app /Applications ./uDesktopMascot_mac_installer.pkg
```
* Une fois la construction terminée, un fichier `uDesktopMascot_mac_installer.pkg` sera généré dans le dossier `build`.

## Crédits des créateurs
* Modèle : « Aozora »
* BGM : MidraLab (eisuke)
* Icône du logiciel : Yamucha

## Avis de tiers

Voir [NOTICE](./NOTICE.md).

## parrain
- Luna
- uezo