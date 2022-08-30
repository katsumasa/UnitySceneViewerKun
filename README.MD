# UnitySceneViewerKun
![GitHub package.json version](https://img.shields.io/github/package-json/v/katsumasa/UnitySceneViewerKun)

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
   - https://github.com/katsumasa/RemoteConnect.git
   - https://github.com/katsumasa/UnitySceneViewerKun.git

## 使い方

### 事前準備(Playerのビルド)


[UnitySceneViewerKunPlayerPrefab.prefab](https://github.com/katsumasa/UnitySceneViewerKun/blob/master/Runtime/Prefabs/UnitySceneViewerKunPlayerPrefab.prefab)のみが含まれたSceneだけで構成されたアプリケーションをビルドします。具体的な基準は下記の通りです。

1. File > New Scene > Emptyで新しい空のSceneを生成する
2. [UnitySceneViewerKunPlayerPrefab.prefab](https://github.com/katsumasa/UnitySceneViewerKun/blob/master/Runtime/Prefabs/UnitySceneViewerKunPlayerPrefab.prefab)をSceneに配置します。
3. File > SaveでSceneを保存します。
4. File > Build SettingsでBuild Settings Windowを開きます。
5. Add Open Sceneから先程作ったSceneをScene In Buildに追加します。この時他のSceneが含まれている場合は、チェックボックスを外してビルドの対象から取り除きます。
6. DevelopmentとAutoconnect Profilerのチェックボックスにチェックを入れます。
7. Build And Runでビルドを実行します。


![2807ab20c3c301b8cb835c713f1aa5cf](https://user-images.githubusercontent.com/29646672/187325733-67380597-91a1-47de-95b9-3d52ff8e672b.gif)



### Sceneのリロード方法

下記の手順でUnityEditor上で編集しているSceneを実機上に転送します。

1. Unity Editor上で実機上で確認を行いたいSceneを開きます。</br>
2. Window > UTJ > UnitySceneViewerKunを選択し、UnitySceneViewerKun Windowを開きます。<br/>
   <img width="234" alt="e0cfd85ee878a9e9108d618eb0c4a1cb" src="https://user-images.githubusercontent.com/29646672/137443973-c75b969f-0a01-4fce-bcbe-93f80e857374.png">

3. Window左上のプルダウンメニューから事前準備で実行した、Playerを選択します。（※ProfilerやFrameDebuggerで接続先を選ぶ場合と同等です）
4. Window中央のプルダウンメニューからPlayerのPlatformを選択します。
5. Reloadボタンを押します。
6. 暫く待つとUnityEditorで開いているSceneが実機上で表示されます。(Sceneの内容のAssetの数に左右されます)

以上です。

## その他

不具合等ありましたら、[issue](https://github.com/katsumasa/UnitySceneViewerKun/issues)より報告をお願いします。
フィードバックやコメントもお待ちしております。</br>
__木村 勝将：katsumasa@unity3d.com__


