uDesktopMascot 使い方ガイド

はじめに

uDesktopMascotをご利用いただきありがとうございます。このガイドでは、以下の3つの手順について説明します。

1. 任意のモデルを追加する方法
2. 任意のボイスデータを追加する方法
3. application_settings.txtで設定を変更する方法
4. メニュー画面の背景画像と背景色、ボタンの色やアイコンを変更する方法

----------------------------------------

1. 任意のモデルを追加する方法

uDesktopMascotでは、VRM形式のモデルを使用してデスクトップ上にキャラクターを表示できます。

手順

1. モデルファイルを準備する

   - 使用したいモデルのVRMファイルを用意してください。

2. モデルファイルを配置する

   - アプリケーションの実行ファイルと同じフォルダにあるStreamingAssetsフォルダを開きます。
   - StreamingAssetsフォルダ内にモデルファイル（例: my_model.vrm）をコピーします。

3. モデルを指定する

   - application_settings.txtファイルを開きます。
   - [Character]セクションのModelPathを、追加したモデルファイル名に変更します。

[Character]
ModelPath=my_model.vrm


4. アプリケーションを再起動する

- uDesktopMascotを再起動すると、新しいモデルが表示されます。

----------------------------------------

2. 任意のボイスデータを追加する方法

キャラクターの音声をカスタマイズできます。

手順

1. ボイスデータを準備する

- 使用したい音声ファイル（.wavや.mp3形式）を用意してください。

2. ボイスデータを配置する

- アプリケーションのStreamingAssetsフォルダ内のVoiceフォルダを開きます。

- クリック時の音声を追加する場合:

  - Voice/Clickフォルダに音声ファイルをコピーします。

- ドラッグ時の音声を追加する場合:

  - Voice/Dragフォルダに音声ファイルをコピーします。

- その他の音声を追加する場合:

  - Voiceフォルダに音声ファイルをコピーします。

3. アプリケーションを再起動する

- uDesktopMascotを再起動すると、新しいボイスが適用されます。

----------------------------------------

3. application_settings.txtで設定を変更する方法

application_settings.txtファイルを編集することで、キャラクターの表示やアプリの動作をカスタマイズできます。

手順

1. 設定ファイルを開く

- アプリケーションの実行ファイルと同じフォルダにあるapplication_settings.txtファイルをテキストエディタで開きます。

2. 設定を変更する

- 必要に応じて以下の設定を変更します。

[Character]
ModelPath=default.vrm       ; 使用するモデルファイル名
Scale=3                     ; キャラクターの大きさ（倍率）
PositionX=0                 ; X座標位置
PositionY=0                 ; Y座標位置
PositionZ=0                 ; Z座標位置
RotationX=0                 ; X軸の回転角度
RotationY=0                 ; Y軸の回転角度
RotationZ=0                 ; Z軸の回転角度

[Sound]
VoiceVolume=1               ; ボイスの音量（0～1）
BGMVolume=0.5               ; BGMの音量（0～1）
SEVolume=1                  ; 効果音の音量（0～1）

[Display]
Opacity=1                   ; キャラクターの透明度（0～1）
AlwaysOnTop=True            ; 常に前面に表示するか（True/False）

[Performance]
TargetFrameRate=60          ; フレームレート設定
QualityLevel=2              ; クオリティ設定（0:低～5:高）


3. 設定を保存する

- 編集内容を保存します。

4. アプリケーションを再起動する

- uDesktopMascotを再起動すると、設定が反映されます。


----------------------------------------

4. メニュー画面の背景画像と背景色、ボタンの色やアイコンを変更する方法
uDesktopMascotでは、メニュー画面の背景画像や背景色、ボタンの色、ボタンのアイコンをカスタマイズすることができます。

手順
カスタム画像や色を準備する

メニュー背景に使用したい画像ファイル（.pngや.jpg形式）を用意してください。
各ボタンの背景画像やアイコンに使用する画像ファイルを用意してください。
色を指定する場合は、カラーコード（例: #FFFFFF）を用意してください。
画像ファイルを配置する

アプリケーションのStreamingAssetsフォルダを開きます。
画像ファイルを以下のように配置します。
メニューの背景画像の場合:
StreamingAssetsフォルダ内に画像ファイルを配置します。
例: MenuBackground.png
ボタンの背景画像やアイコンの場合:
必要に応じてサブフォルダを作成し、画像ファイルを配置します。
例: StreamingAssets/Icons/ModelIcon.png
application_settings.txtファイルを編集する

application_settings.txtファイルを開きます。

以下の設定セクションを追加または編集します。

[MenuUI]
BackgroundColor=#333333                    ; メニュー背景の色（カラーコード）
BackgroundImagePath=MenuBackground.png     ; メニュー背景の画像ファイルパス

[MenuButtonUI]
; ボタン全体の設定がある場合はここに記述できます

[Button_modelSettingButton]
BackgroundColor=#FFCCCC                      ; モデル設定ボタンの背景色
BackgroundImagePath=ModelButtonBackground.png ; 背景画像ファイルパス
IconPath=Icons/ModelIcon.png                 ; ボタンアイコンのファイルパス

[Button_helpButton]
BackgroundColor=#CCFFCC
BackgroundImagePath=HelpButtonBackground.png
IconPath=Icons/HelpIcon.png

[Button_appSettingButton]
BackgroundColor=#CCCCFF
BackgroundImagePath=AppButtonBackground.png
IconPath=Icons/AppIcon.png

[Button_quitButton]
BackgroundColor=#FFFFCC
BackgroundImagePath=QuitButtonBackground.png
IconPath=Icons/QuitIcon.png
注意点:

BackgroundColorは省略可能です。指定しない場合、デフォルトの色が使用されます。
BackgroundImagePathやIconPathは、StreamingAssetsフォルダからの相対パスで指定します。
カラーコードは#RRGGBBまたは#RRGGBBAAの形式で指定します。
アプリケーションを再起動する

uDesktopMascotを再起動すると、メニュー画面やボタンの外観が変更されます。
補足情報
画像形式について

使用できる画像形式は、.pngや.jpgなどの一般的な画像形式です。
透過を扱いたい場合は、.png形式を使用してください。
カラーコードについて

カラーコードは、16進数のカラーコード（例: #FF0000）を使用してください。
不透明度（アルファ値）を指定したい場合は、#RRGGBBAAの形式で指定します（例: #FF000080は50%の透明度の赤色）。
画像ファイルの配置例

メニュー背景画像: StreamingAssets/MenuBackground.png
モデル設定ボタンの背景画像: StreamingAssets/ModelButtonBackground.png
ヘルプボタンのアイコン: StreamingAssets/Icons/HelpIcon.png
設定の例

メニュー背景を白色に設定し、背景画像を指定しない場合:

[MenuUI]
BackgroundColor=#FFFFFF
BackgroundImagePath=
ボタンの背景色のみを変更し、画像を指定しない場合:

[Button_helpButton]
BackgroundColor=#00FF00

----------------------------------------

ご不明な点がございましたら、お気軽にお問い合わせください。uDesktopMascotで楽しいデスクトップ体験をお楽しみください！

mail: asokhct@gmail.com
X:https://x.com/ayousanz