# uDesktopMascot

[![Version Unity](https://img.shields.io/badge/Unity-6000.0%2B-blueviolet?logo=unity)](https://unity.com/releases/editor/archive)
[![Versions](https://img.shields.io/github/release/MidraLab/uDesktopMascot.svg)](https://github.com/MidraLab/uDesktopMascot/releases)
[![Test CI Unity](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml/badge.svg)](https://github.com/MidraLab/uDesktopMascot/actions/workflows/edit-test.yml)
[![Demandez à DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/MidraLab/uDesktopMascot)

日本語 | [English](README_EN.md) | [中文](README_CN.md) | [Español](README_ES.md) | [Français](README_FR.md)

**Remarque** : Les langues ci-dessus (English, 中文, Español, Français) ont été générées par une traduction automatique avec GPT-4o-mini. Pour la précision de la traduction et les nuances, veuillez vous référer au texte d'origine (日本語).

<!-- TOC -->
* [uDesktopMascot](#udesktopmascot)
  * [Aperçu](#aperçu)
  * [Liste des fonctionnalités](#liste-des-fonctionnalités)
  * [Exécution sur macOS](#exécution-sur-macos)
  * [Exigences](#exigences)
  * [Licence](#licence)
  * [Matériaux](#matériaux)
  * [Comment créer un installateur](#comment-créer-un-installateur)
    * [Windows](#windows)
    * [macOS](#macos)
  * [Crédits des créateurs](#crédits-des-créateurs)
  * [Avis tiers](#avis-tiers)
  * [Sponsorship](#sponsorship)
<!-- TOC -->

## Aperçu

« uDesktopMascot » est un projet open source d'application de mascotte de bureau sur le thème de 'la liberté créative'.
Comme exemple de fonctionnalité, il est possible de charger des modèles au format VRM ou GLB/FBX et de les afficher sur le bureau. De plus, vous pouvez librement configurer les couleurs et les images de fond des GUI, telles que l'écran de menu ou la fenêtre de l'application.
Veuillez consulter la [liste des fonctionnalités](#liste-des-fonctionnalités) pour plus de détails.

![](Docs/Image/AppImage.png)

**Plateformes prises en charge**
* Windows 10/11
* macOS

## Liste des fonctionnalités

L'application inclut les fonctionnalités suivantes. Veuillez consulter la liste ci-dessous pour plus de détails.

L'ajout d'actifs externes peut être réalisé en les plaçant dans le dossier StreamingAssets.

<details>

<summary>Modèles et animations</summary>

* Charge et affiche des fichiers de modèle placés dans StreamingAssets.
  * Support des modèles au format VRM (1.x, 0.x).
  * Support des modèles au format GLB/GLTF. (Les animations ne sont pas supportées)
  * Support des modèles au format FBX. (Cependant, certains modèles peuvent ne pas charger les textures. Les animations ne sont pas prises en charge)
    * Les textures peuvent être chargées en étant placées dans StreamingAssets/textures/.
* Ajout de modèles VRM depuis l'écran de sélection et d'ajout de modèles.
  * Ajout par spécification de chemin
  * Ajout depuis une boîte de dialogue de sélection de fichiers

</details>

<details>

<summary>Voix et BGM</summary>

* Charge et joue les fichiers audio placés sous SteamingAssets/Voice/. S'il y en a plusieurs, ils sont joués de manière aléatoire.
  * Le son joué lors d'un clic est chargé à partir des fichiers audio placés dans StreamingAssets/Voice/Click/.
* Charge et joue les fichiers musicaux placés sous SteamingAssets/BGM/. S'il y en a plusieurs, ils sont joués de manière aléatoire.
* Ajout de la voix par défaut du personnage.
  * La voix par défaut utilise des sons de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan).
  * Elle est jouée au démarrage de l'application, à la fermeture de l'application et lors des clics.

</details>

<details>

<summary>Reconnaissance vocale et chat AI</summary>

* Intégration du moteur de reconnaissance vocale hors ligne [Vosk](https://alphacephei.com/vosk/) pour convertir les entrées microphone en texte en temps réel.
  * Affiche les résultats intermédiaires `[STT][partial]` et les résultats confirmés `[STT][final]` dans la console Unity.
  * Lorsqu'un silence dure `VadSilenceSeconds` (par défaut 1,0 seconde), le texte est confirmé et envoyé à LLM (`[STT][send]` log).
* Une fois le texte confirmé, la fonction de chat AI reçoit le message vocal et le personnage lit la réponse à voix haute.
  * Pendant la génération de la réponse, le microphone est automatiquement mis en pause pour éviter les erreurs de reconnaissance.
* Le bouton microphone sur l'écran ChatDialog permet de basculer l'enregistrement en marche/arrêt.
* Les DLL natives nécessaires (`libvosk.dll`, `libstdc++-6.dll`, `libgcc_s_seh-1.dll`, `libwinpthread-1.dll`, etc.) sont placées dans `Assets/Plugins/x86_64/` et sont automatiquement emballées lors de la compilation.
* Les modèles acoustiques (exemple : `vosk-model-small-ja-0.22`) ou les modèles GGUF quantifiés placés dans `StreamingAssets` vous permettent de traiter plusieurs langues, y compris le japonais et l'anglais, sans connexion réseau.

</details>

<details>

<summary>Configuration de l'application via fichiers texte</summary>
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

<details>

<summary>Écran de menu</summary>

* Vous pouvez configurer l'image d'arrière-plan et la couleur d'arrière-plan de l'écran de menu.
  * L'image d'arrière-plan peut être chargée à partir des fichiers d'image placés dans StreamingAssets/Menu/. Les formats d'image pris en charge sont les suivants :
    * PNG
    * JPG(JPEG)
    * BMP
    * GIF (image fixe)
    * TGA
    * TIFF
  * La couleur de fond peut être spécifiée par le code couleur.
* Accès aux fonctionnalités suivantes depuis l'écran de menu :
  * Écran de sélection et d'ajout de modèles
  * Fonction de chat AI
  * Fonction LocalWeb
  * Paramètres de l'application
  * Fermeture de l'application
* En appuyant sur le bouton de réduction de l'écran du menu, l'application peut être minimisée dans la zone de notification sous Windows.
  * L'application réduite peut être affichée à nouveau en cliquant sur l'icône de la zone de notification.

</details>

## Exécution sur macOS

Lors de l'exécution de l'application sur macOS, il se peut que l'application soit bloquée par GateKeeper. Dans ce cas, exécutez la commande suivante à partir du terminal :

```sh
xattr -r -c uDesktopMascot.app
```

## Exigences
* Unity 6000.1.1f1(IL2CPP)

## Licence
* Le code est sous licence [Apache License 2.0](LICENSE).
* Les actifs suivants sont sous licence [CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/) :
  * BGM
  * Modèles

## Matériaux
* Les animations de personnage par défaut sont créées à l'aide de [données d'animation pour 'VRM Oningyou Asobi'](https://fumi2kick.booth.pm/items/1655686). J'ai vérifié la distribution dans le repository.
* La police utilisée est [Noto Sans Japanese](https://fonts.google.com/noto/specimen/Noto+Sans+JP?lang=ja_Jpan). Cela redistribue la police Noto Sans JP sous [SIL OPEN FONT LICENSE Version 1.1](https://fonts.google.com/noto/specimen/Noto+Sans+JP/license?lang=ja_Jpan). Les droits d'auteur de la police appartiennent à l'auteur d'origine (Google).
* La voix par défaut utilise les sons de [COEIROINK: Tsukuyomi-chan](https://coeiroink.com/character/audio-character/tsukuyomi-chan). J'ai préalablement vérifié avec COEIROINK pour l'utilisation.
* Les icônes des boutons utilisent [MingCute](https://github.com/MidraLab/MingCute).

## Comment créer un installateur
### Windows
* Construisez avec Unity dans un dossier `build` nommé `uDesktopMascot`.
```
uDesktopMasscot
└── build
    └── uDesktopMasscot
        ├── uDesktopMascot.exe
        └── README.txt
        ...
```

* Installez [Inno Setup](https://www.jrsoftware.org/isdl.php).
  
* Ensuite, cliquez sur `More files` et sélectionnez `setup.iss` dans le projet.
  
![](Docs/Image/SetupIss-1.png)
* Après avoir sélectionné, cliquez sur le bouton de lecture.
  
![](Docs/Image/SetupIss-2.png)
* Une fois la construction terminée, l'installateur sera généré à la racine du projet.

### macOS
L'installateur peut être créé uniquement sur un PC macOS.

* Construisez avec Unity dans un dossier `build/uDesktopMascot` nommé `uDesktopMascot`.
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
* Une fois la construction terminée, `uDesktopMascot_mac_installer.pkg` sera généré dans le dossier `build`.

## Crédits des créateurs
* Modèle : « Aozora ».
* BGM : MidraLab (eisuke).
* Icône du logiciel : Yumcha.

## Avis tiers

Voir [NOTICE](./NOTICE.md).

## Sponsorship
- Luna
- uezo