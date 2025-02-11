Sure, here is the translation of the text into French:

# uDesktopMascot

[![Version Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Versions](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Test CI Unity](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Remarque** : Les langues ci-dessus (English, 中文, Español, Français) ont été générées par une traduction automatique de GPT-4o-mini. Pour la précision et les nuances de la traduction, veuillez vous référer au texte original (日本語).

<!-- TOC -->
- [uDesktopMascot](#udesktopmascot)
  - [Aperçu](#aperçu)
  - [Liste des fonctionnalités](#liste-des-fonctionnalités)
  - [Exécution sur macOS](#exécution-sur-macos)
  - [exigences](#exigences)
  - [licence](#licence)
  - [À propos des ressources](#à-propos-des-ressources)
  - [Comment créer un installateur](#comment-créer-un-installateur)
    - [Windows](#windows)
    - [macOS](#macos)
  - [Crédits de création](#crédits-de-création)
  - [Avis de tierce partie](#avis-de-tierce-partie)
  - [sponsor](#sponsor)
<!-- TOC -->

## Aperçu

« uDesktopMascot » est un projet open source d'application de mascotte de bureau sur le thème de la `liberté de création`.
Comme exemple d'une fonctionnalité, il est possible de charger des modèles au format VRM ou GLB/FBX et de les afficher sur le bureau. Vous pouvez également configurer librement les couleurs des menus et des fenêtres d'application ainsi que les images d'arrière-plan.
Pour une liste détaillée des fonctionnalités, veuillez vous référer à [Liste des fonctionnalités](#liste-des-fonctionnalités).

![](Docs/Image/AppImage.png)

**Plateformes supportées**
* Windows 10/11
* macOS

## Liste des fonctionnalités

L'application comporte les fonctionnalités suivantes. Pour plus de détails, veuillez consulter la liste ci-dessous.

Vous pouvez ajouter des ressources externes en les plaçant dans le dossier StreamingAssets.

<details>

<summary>Modèle et animation</summary>

* Charge et affiche n'importe quel fichier modèle placé dans StreamingAssets.
  * Support pour les modèles au format VRM (1.x, 0.x).
  * Support pour les modèles au format GLB/GLTF. (L'animation n'est pas prise en charge)
  * Support pour les modèles au format FBX. (Cependant, certains modèles peuvent ne pas pouvoir charger les textures. De plus, l'animation n'est pas prise en charge)
    * Les textures peuvent être chargées en les plaçant dans StreamingAssets/textures/.
* Ajout de modèles VRM depuis l'écran de sélection et d'ajout de modèles.
  * Ajout par spécification de chemin
  * Ajout à partir de la boîte de dialogue de sélection de fichiers

</details>

<details>

<summary>Voix et BGM</summary>

* Charge et joue les fichiers audio placés dans StreamingAssets/Voice/. S'il y en a plusieurs, il joue de manière aléatoire.
  * Le son joué lors d'un clic est chargé à partir des fichiers audio placés dans StreamingAssets/Voice/Click/.
* Charge et joue les fichiers musicaux placés dans StreamingAssets/BGM/. S'il y en a plusieurs, il joue de manière aléatoire.
* Ajout d'une voix par défaut pour le personnage.
  * La voix par défaut est basée sur les enregistrements de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Elle est jouée au lancement de l'application, à la fermeture de l'application et lors d'un clic.

</details>

<details>

<summary>Paramètres de l'application via un fichier texte</summary>
Vous pouvez modifier les paramètres de l'application via le fichier application_settings.txt.

La structure du fichier de paramètres est la suivante :

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

* Vous pouvez configurer l'image de fond et la couleur de l'écran de menu.
  * L'image de fond peut être chargée à partir des fichiers image placés dans StreamingAssets/Menu/. Les formats d'image pris en charge sont les suivants :
    * PNG
    * JPG (JPEG)
    * BMP
    * GIF (image fixe)
    * TGA
    * TIFF
  * La couleur de fond peut être spécifiée avec un code couleur.
* De l'écran de menu, vous pouvez accéder aux fonctionnalités suivantes :
  * Écran de sélection et d'ajout de modèle
  * Fonction de chat AI
  * Fonction LocalWeb
  * Paramètres de l'application
  * Quitter l'application
* Vous pouvez minimiser l'application dans la zone de notification de Windows en appuyant sur le bouton de rangement de l'écran de menu.
  * L'application minimisée peut être affichée à nouveau en cliquant sur l'icône dans la zone de notification.

</details>

## Exécution sur macOS

Lorsque vous exécutez l'application sur macOS, il peut être bloqué par GateKeeper.
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

## À propos des ressources
* Les animations par défaut du personnage sont créées avec [「VRMお人形遊び」用アニメーションデータ詰め合わせ](https://fumi2kick.booth.pm/items/1655686). Nous avons vérifié la possibilité de les inclure dans le dépôt.
* La police utilisée est [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). Nous redistribuons la police Noto Sans JP sous [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Les droits d'auteur de la police appartiennent à l'auteur original (Google).
* La voix par défaut est basée sur les enregistrements de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). Nous avons préalablement vérifié l'utilisation avec COEIROINK.
* Les icônes de boutons proviennent de [MingCute](https://github.com/MidraLab/MingCute).

## Comment créer un installateur
### Windows
* Construisez dans Unity un dossier `build` nommé `uDesktopMascot`.
```
uDesktopMasscot
└── build
    └── uDesktopMasscot
        ├── uDesktopMascot.exe
        └── README.txt
        ...
```

* Installez [Inno Setup](https://www.jrsoftware.org/isdl.php).
  
* Une fois ouvert, cliquez sur `More files` et sélectionnez le fichier `setup.iss` dans votre projet.
  
![](Docs/Image/SetupIss-1.png)
* Après la sélection, cliquez sur le bouton de lecture.
  
![](Docs/Image/SetupIss-2.png)
* Une fois la construction terminée, l'installateur sera généré à la racine du projet.

### macOS
L'installateur ne peut être créé que sur un PC macOS.

* Construisez dans Unity dans le dossier `build/uDesktopMascot` en le nommant `uDesktopMascot`.
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
* Une fois la construction terminée, le fichier `uDesktopMascot_mac_installer.pkg` sera généré dans le dossier `build`.

## Crédits de création
* Modèle : « Aozora »
* BGM : MidraLab(eisuke)
* Icône du logiciel : Yamucha

## Avis de tierce partie

Voir [NOTICE](./NOTICE.md).

## sponsor
- Luna
- uezo