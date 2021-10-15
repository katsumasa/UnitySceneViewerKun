# UnitySceneViewerKun

[English Ver. README](Documentation~/UnitySceneViewerKun.md)

## 概要

UnityEditorで編集中のSceneを実機上で表示する（Sceneのホットリロードを行う）プロジェクトです。Sceneの変更内容を素早く実機上で確認する場合に使用します。

![19a2fbac14b1d38f16ad853be9a6402b](https://user-images.githubusercontent.com/29646672/137443854-7a37ff5e-3d0d-4524-9011-2c6a666daceb.gif)

画像の左側がAndroidで実行されている内容です。

## 動作確認環境

### 動作確認済みUnity

- Unity2019.4.19f1

### 動作確認済みプラットフォーム

- Android
- iOS

## インストール方法

### git

```:console
git clone https://github.com/katsumasa/RemoteConnect.git
git clone https://github.com/katsumasa/UnitySceneViewerKun.git
```

### UnityPackageManager

1. ステータスバーの Add  ボタンをクリックします。
2. パッケージを加えるためのオプションが表示されます。
3. ドロップダウンから Add package from git URL を選択します。テキストフィールドと Add ボタンが表示されます。
4. テキストフィールドに次のGit URL を順番に入力し、Add をクリックします。
   https://github.com/katsumasa/RemoteConnect.git
   https://github.com/katsumasa/UnitySceneViewerKun.git

## 使い方

### 事前準備(Playerのビルド)

`Scenes/UnitySceneViewKun.unity`をDevelopmentと Autoconnect ProfilerをONにした状態でBuild & Runを実行して任意の端末で実行してください。

<img width="430" alt="2021-02-19 144614" src="https://user-images.githubusercontent.com/29646672/137443916-4dd655ec-e675-41be-8c20-baf4c9aad2fe.png">


### Sceneのリロード方法

下記の手順でUnityEditor上で編集しているSceneを実機上に転送します。

1. Unity Editor上で実機上で確認を行いたいSceneを開きます。`Scenes/UnitySceneViewKun.unity`以外を開いて下さい。</br>
2. Window->UnitySceneViewerKunを選択し、UnitySceneViewerKun Windowを開きます。<br/>
   <img width="234" alt="e0cfd85ee878a9e9108d618eb0c4a1cb" src="https://user-images.githubusercontent.com/29646672/137443973-c75b969f-0a01-4fce-bcbe-93f80e857374.png">

3. Window左上のプルダウンメニューから事前準備で実行した、Playerを選択します。（※ProfilerやFrameDebuggerで接続先を選ぶ場合と同等です）
4. Window中央のプルダウンメニューからPlayerのPlatformを選択します。
5. Reloadボタンを押します。
6. 暫く待つとUnityEditorで開いているSceneが実機上で表示されます。(Sceneの内容のAssetの数に左右されます)

以上です。

## その他

不具合等ありましたら、issue Trackerより報告をお願いします。
フィードバックやコメントもお待ちしております。</br>
__木村 勝将：katsumasa@unity3d.com__


