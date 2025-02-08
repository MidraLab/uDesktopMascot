# uDesktopMascot

[![Version de Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)  
[![Releases](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)  
[![CI de test Unity](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Remarque** : Les langues ci-dessus (English, 中文, Español, Français) sont générées par traduction automatique via GPT-4o-mini. Pour des précisions ou des nuances de traduction, veuillez vous référer au texte original (日本語).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Résumé](#résumé)
  * [Liste des fonctionnalités](#liste-des-fonctionnalités)
  * [Exécution sur macOS](#exécution-sur-macos)
  * [exigences](#exigences)
  * [licence](#licence)
  * [À propos des matériaux](#à-propos-des-matériaux)
  * [Comment créer un installateur](#comment-créer-un-installateur)
    * [Windows](#windows)
    * [macOS](#macos)
  * [Crédits des créateurs](#crédits-des-créateurs)
  * [Avis des tiers](#avis-des-tiers)
  * [sponsor](#sponsor)
<!-- TOC -->

## Résumé

« uDesktopMascot » est un projet open source d'application de mascotte de bureau sur le thème de la `liberté de création`.  
Comme exemple d'une fonctionnalité, il est possible de charger des modèles au format VRM ou GLB/FBX et de les afficher sur le bureau. De plus, il est possible de personnaliser les couleurs des menus et des fenêtres de l'application, ainsi que les images d'arrière-plan. Pour une liste plus détaillée des fonctionnalités, veuillez consulter la [Liste des fonctionnalités](#liste-des-fonctionnalités).

![](Docs/Image/AppImage.png)

**Plateformes prises en charge**
* Windows 10/11
* macOS

## Liste des fonctionnalités

L'application a les fonctionnalités suivantes. Pour plus de détails, veuillez consulter la liste ci-dessous.

L'ajout d'actifs externes peut être réalisé en plaçant les fichiers dans le dossier StreamingAssets.

<details>

<summary>Modèles et animations</summary>

* Charge et affiche des fichiers de modèle placés dans le dossier StreamingAssets.
  * Prise en charge des modèles au format VRM (1.x, 0.x).
  * Prise en charge des modèles au format GLB/GLTF. (L'animation n'est pas prise en charge)
  * Prise en charge des modèles au format FBX. (Cependant, certaines modèles ne peuvent pas charger des textures. De plus, l'animation n'est pas prise en charge.)
    * Les textures peuvent être chargées en les plaçant dans le dossier StreamingAssets/textures/.
* Il est possible d'ajouter des modèles de personnages VRM via l'écran d'ajout de modèle.
  * Ajout d'un chemin VRM
  * Sélection de fichiers via une boîte de dialogue

</details>

<details>

<summary>Voix et BGM</summary>

* Joue des fichiers audio placés dans le dossier SteamingAssets/Voice/. S'il y en a plusieurs, ils seront joués de manière aléatoire.
  * Les voix jouées lors du clic sont chargées à partir des fichiers audio placés dans StreamingAssets/Voice/Click/. 
* Joue des fichiers musicaux placés dans le dossier SteamingAssets/BGM/. S'il y en a plusieurs, ils seront joués de manière aléatoire.
* Ajout de la voix par défaut du personnage.
  * La voix par défaut utilise les sons de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Elle sera jouée au démarrage de l'application, à la fermeture de l'application, et lors des clics.

</details>

<details>

<summary>Configuration de l'application par fichier texte</summary>
Il est possible de modifier les paramètres de l'application via le fichier application_settings.txt.

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
  * L'image de fond peut être chargée à partir de fichiers placés dans StreamingAssets/Menu/. Les formats d'images pris en charge sont :
    * PNG
    * JPG (JPEG)
    * BMP
    * GIF (images fixes)
    * TGA
    * TIFF
  * La couleur de fond peut être spécifiée par un code couleur.

Les fonctionnalités suivantes sont disponibles à partir du menu :
* Ajout/Changement de modèle
* Modification des paramètres de l'application
* Fermeture de l'application
* (Windows uniquement) Rangement de l'application dans la zone de notification : vous pouvez minimiser l'application.
  * Lorsque l'application est rangée dans la zone de notification, l'icône de l'application sur le bureau sera affichée dans la zone de notification.
  * En cliquant sur l'icône de la zone de notification, vous pouvez faire réapparaître l'application sur le bureau.
* Fonction de chat AI : vous pouvez utiliser la fonction de chat AI.
  * Compatible avec le modèle local utilisant Qwen2

</details>

## Exécution sur macOS

Lors de l'exécution de l'application sur macOS, il est possible que GateKeeper bloque l'application.  
Dans ce cas, exécutez la commande suivante depuis le terminal :

```sh
xattr -r -c uDesktopMascot.app
```

## exigences
* Unity 6000.0.31f1(IL2CPP)

## licence
* Le code est sous [Apache License 2.0](LICENSE).
* Les actifs suivants sont sous [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/) :
  * BGM
  * Modèles

## À propos des matériaux
* Les animations par défaut des personnages sont créées en utilisant le [pack d'animation pour "Jeu de poupée VRM"](https://fumi2kick.booth.pm/items/1655686). Ceci a été vérifié pour être distribué dans le dépôt.
* La police utilisée est [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). Elle est redistribuée sous [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Les droits d'auteur de la police appartiennent à l'auteur original (Google).
* La voix par défaut est celle de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). L'utilisation a été préalablement confirmée avec COEIROINK.
* Les icônes des boutons utilisent [MingCute](https://github.com/MidraLab/MingCute).

## Comment créer un installateur
### Windows
* Construisez le projet avec Unity dans un dossier `build` nommé `uDesktopMascot`.

* Installez [Inno Setup](https://www.jrsoftware.org/isdl.php).
  
* Une fois ouvert, cliquez sur `More files` et sélectionnez le fichier `setup.iss` qui se trouve dans votre projet.
  
![](Docs/Image/SetupIss-1.png)
* Une fois sélectionné, cliquez sur le bouton de lecture.
  
![](Docs/Image/SetupIss-2.png)
* Une fois la construction terminée, un installateur sera généré dans la racine du projet.

### macOS
L'installateur ne peut être créé que sur un PC macOS.

* Construisez le projet avec Unity dans un dossier `build` nommé `uDesktopMascot`.

* Exécutez la commande suivante :
```sh
cd build
productbuild --component uDesktopMascot/uDesktopMascot.app /Applications ./uDesktopMascot_mac_installer.pkg
```
* Une fois la construction terminée, un fichier `uDesktopMascot_mac_installer.pkg` sera généré dans le dossier `build`.

## Crédits des créateurs
* Modèle : « Aozora » 
* BGM : MidraLab (eisuke) 
* Icône du logiciel : Yamucha

## Avis des tiers

Voir [NOTICE](./NOTICE.md).

## sponsor
- Luna
- uezo