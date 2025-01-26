uDesktopMascot 使い方ガイド

はじめに

uDesktopMascotをご利用いただきありがとうございます。このガイドでは、以下の3つの手順について説明します。

1. 任意のモデルを追加する方法
2. 任意のボイスデータを追加する方法
3. application_settings.txtで設定を変更する方法

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

ご不明な点がございましたら、お気軽にお問い合わせください。uDesktopMascotで楽しいデスクトップ体験をお楽しみください！

mail: asokhct@gmail.com
X:https://x.com/ayousanzuDesktopMascot 使い方ガイド

はじめに

uDesktopMascotをご利用いただきありがとうございます。このガイドでは、以下の3つの手順について説明します。

1. 任意のモデルを追加する方法
2. 任意のボイスデータを追加する方法
3. application_settings.txtで設定を変更する方法

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

ご不明な点がございましたら、お気軽にお問い合わせください。uDesktopMascotで楽しいデスクトップ体験をお楽しみください！

mail: asokhct@gmail.com
X:https://x.com/ayousanz