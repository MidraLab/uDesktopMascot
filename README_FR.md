# uDesktopMascot

[![Version Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Releases](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Test CI Unity](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Remarque** : Les langues ci-dessus (English, 中文, Español, Français) ont été générées par traduction automatique via GPT-4o-mini. Pour la précision et les nuances de la traduction, veuillez vous référer au texte original (日本語).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Résumé](#résumé)
  * [Liste des fonctionnalités](#liste-des-fonctionnalités)
  * [Exécution sur macOS](#exécution-sur-macos)
  * [Exigences](#exigences)
  * [Licence](#licence)
  * [Matériel](#matériel)
  * [Méthodes de création d'installeur](#méthodes-de-création-dinstalleur)
    * [Windows](#windows)
    * [macOS](#macos)
  * [Crédits des créateurs](#crédits-des-créateurs)
  * [Avis de tiers](#avis-de-tiers)
  * [Sponsor](#sponsor)
<!-- TOC -->

## Résumé

« uDesktopMascot » est un projet open source de l'application de mascottes de bureau avec pour thème `la liberté de création`. 
En tant qu'exemple de fonctionnalité, il est capable de charger des modèles au format VRM ou GLB/FBX et de les afficher sur le bureau. De plus, il est possible de configurer librement les couleurs et les images de fond des menus et des fenêtres de l'application. Pour une liste détaillée des fonctionnalités, veuillez consulter la [liste des fonctionnalités](#liste-des-fonctionnalités).

![](Docs/Image/AppImage.png)

**Plateformes prises en charge**
* Windows 10/11
* macOS

## Liste des fonctionnalités

L'application dispose des fonctionnalités suivantes. Pour plus de détails, veuillez consulter la liste ci-dessous.

L'ajout d'actifs externes peut être réalisé en les plaçant dans le dossier StreamingAssets.

<details>

<summary>Modèles et animations</summary>

* Charge et affiche des fichiers de modèle placés dans StreamingAssets.
  * Prend en charge les modèles au format VRM (1.x, 0.x).
  * Prend en charge les modèles au format GLB/GLTF. (Les animations ne sont pas supportées)
  * Prend en charge les modèles au format FBX. (Cependant, certaines textures peuvent ne pas être chargées et les animations ne sont pas supportées)
    * Les textures peuvent être chargées en les plaçant dans StreamingAssets/textures/.
* Ajout de modèles VRM depuis l'écran de sélection et d'ajout de modèles
  * Ajout par spécification de chemin
  * Ajout via boîte de dialogue de sélection de fichiers

</details>

<details>

<summary>Voix et BGM</summary>

* Charge et joue les fichiers audio placés dans StreamingAssets/Voice/. Si plusieurs fichiers sont présents, il sera joué aléatoirement.
  * La voix jouée lors du clic est chargée à partir des fichiers audio placés dans StreamingAssets/Voice/Click/.
* Charge et joue les fichiers musicaux placés dans StreamingAssets/BGM/. Si plusieurs fichiers sont présents, il sera joué aléatoirement.
* Ajout de la voix par défaut du personnage
  * La voix par défaut utilise les sons de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Joue lors du démarrage de l'application, à sa fermeture et lors des clics.

</details>

<details>

<summary>Configuration de l'application par fichier texte</summary>
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

* Vous pouvez configurer l'image de fond et la couleur d'arrière-plan de l'écran de menu.
  * L'image de fond peut être chargée à partir de fichiers d'image placés dans StreamingAssets/Menu/. Les formats d'image pris en charge sont les suivants :
    * PNG
    * JPG(JPEG)
    * BMP
    * GIF (images fixes)
    * TGA
    * TIFF
  * La couleur d'arrière-plan peut être spécifiée par son code couleur.
* Depuis l'écran de menu, vous pouvez accéder aux fonctionnalités suivantes :
  * Écran de sélection et d'ajout de modèles
  * Fonction de chat AI
  * Fonction LocalWeb
  * Paramètres de l'application
  * Quitter l'application
* En appuyant sur le bouton de réduction de l'écran de menu, l'application peut être réduite dans la zone de notification sous Windows uniquement.
  * L'application réduite peut être à nouveau affichée en cliquant sur l'icône dans la zone de notification.

</details>

## Exécution sur macOS

Lorsque vous exécutez l'application sur macOS, il est possible que GateKeeper bloque l'application. Dans ce cas, exécutez la commande suivante dans le terminal :

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
* Les animations par défaut du personnage ont été créées à l'aide des données d'animation [« Pack d'animation pour les poupées VRM »](https://fumi2kick.booth.pm/items/1655686). Cela a été confirmé pour la distribution avec le dépôt.
* La police utilisée est [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). Elle est redistribuée sous [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Les droits d'auteur de la police sont détenus par l'auteur original (Google).
* La voix par défaut utilise les sons de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). L'utilisation a été préalablement confirmée auprès de COEIROINK.
* Les icônes de boutons utilisent [MingCute](https://github.com/MidraLab/MingCute).

## Méthodes de création d'installeur
### Windows
* Construisez dans Unity dans un dossier nommé `uDesktopMascot` dans le dossier `build`.

* Installez [Inno Setup](https://www.jrsoftware.org/isdl.php).
  
* Une fois ouvert, cliquez sur `More files` et sélectionnez `setup.iss` dans le projet.
  
![](Docs/Image/SetupIss-1.png)
* Après avoir sélectionné, cliquez sur le bouton de lecture.
  
![](Docs/Image/SetupIss-2.png)
* Une fois la construction terminée, l'installeur sera généré dans la racine du projet.

### macOS
L'installeur ne peut être créé que sur un PC macOS.

* Construisez dans Unity dans un dossier nommé `uDesktopMascot` dans le dossier `build`.

* Exécutez la commande suivante.
```sh
cd build
productbuild --component uDesktopMascot/uDesktopMascot.app /Applications ./uDesktopMascot_mac_installer.pkg
```
* Une fois la construction terminée, `uDesktopMascot_mac_installer.pkg` sera généré dans le dossier `build`.

## Crédits des créateurs
* Modèle : « Aozora » 
* BGM : MidraLab (eisuke)
* Icône de logiciel : Yamucha

## Avis de tiers

Voir [NOTICE](./NOTICE.md).

## Sponsor
- Luna
- uezo