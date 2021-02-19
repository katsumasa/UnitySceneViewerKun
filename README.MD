# UnitySceneViewerKun
## 概要
UnityEditorで編集中のSceneを実機上で表示する（Sceneのホットリロードを行う）プロジェクトです。Sceneの変更内容を素早く実機上で確認する場合に使用します。

![alt text](doc/image/19a2fbac14b1d38f16ad853be9a6402b.gif)
画像の左側がAndroidで実行されている内容です。
## 動作確認環境
### 動作確認済みUnity
- Unity2017.4.24f1
- Unity2018.4.31f1
- Unity2019.4.19f1
### 動作確認済みプラットフォーム
- Android
- iOS

## 使い方
### 事前準備(Playerのビルド)
`Scenes/UnitySceneViewKun.unity`をDevelopmentと Autoconnect ProfilerをONにした状態でBuild & Runを実行して任意の端末で実行してください。 
![img](doc/image/2021-02-19%20144614.png)
### Sceneのリロード方法
下記の手順でUnityEditor上で編集しているSceneを実機上に転送します。
1. Unity Editor上で実機上で確認を行いたいSceneを開きます。</br>
2. Window->UnitySceneViewerKunを選択し、UnitySceneViewerKun Windowを開きます。<br/>
![img](doc/image/e0cfd85ee878a9e9108d618eb0c4a1cb.png) <br/>
3. Window左上のプルダウンメニューから事前準備で実行した、Playerを選択します。（※ProfilerやFrameDebuggerで接続先を選ぶ場合と同等です）
4. Window中央のプルダウンメニューからPlayerのPlatformを選択します。
5. Reloadボタンを押します。
6. 暫く待つとUnityEditorで開いているSceneが実機上で表示されます。(Sceneの内容のAssetの数に左右されます)

以上です。
## その他
不具合等ありましたら、issue Trackerより報告をお願いします。
フィードバックやコメントもお待ちしております。</br>
__木村 勝将：katsumasa@unity3d.com__


