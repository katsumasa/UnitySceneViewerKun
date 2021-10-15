# UnitySceneViewerKun

## Summary

This project allows you to display the scene being edited inside Unity editor on actual device. It's also capable of hot reloading, which comes in handy for a quick check of the result you edited in the scene to the actual device. 

![19a2fbac14b1d38f16ad853be9a6402b](https://user-images.githubusercontent.com/29646672/137443854-7a37ff5e-3d0d-4524-9011-2c6a666daceb.gif)

The left side of the image is the screen displayed on Adroid device.
## Operating Environment 
### Unity Version


- Unity2019.4.19f1
- 
### Platform

- Android
- iOS

## Install

### git

```:console
git clone https://github.com/katsumasa/RemoteConnect.git
git clone https://github.com/katsumasa/UnitySceneViewerKun.git
```

### UnityPackageManager

1. Click the add  button in the status bar.
2. The options for adding packages appear.
3. Select Add package from git URL from the add menu. A text box and an Add button appear.
4. Enter a next Git URL in the text box and click Add.
   https://github.com/katsumasa/RemoteConnect.git
   https://github.com/katsumasa/UnitySceneViewerKun.git

## How to use
### Things to prepare in advance (Player Build)

Execute Build & Run `Scenes/UnitySceneViewKun.unity` with Development and Autoconnect Profiler turned ON on the device.

<img width="430" alt="2021-02-19 144614" src="https://user-images.githubusercontent.com/29646672/137443916-4dd655ec-e675-41be-8c20-baf4c9aad2fe.png">

### How to reload scene
Follow the procedure below to transfer the Scene edited on Unity Editor to the actual machine:
1. Open the Scene you want to check on the actual machine in Unity Editor.</br>
2. Select Window->UnitySceneViewerKun、Then open UnitySceneViewerKun Window.<br/>
   <img width="234" alt="e0cfd85ee878a9e9108d618eb0c4a1cb" src="https://user-images.githubusercontent.com/29646672/137443973-c75b969f-0a01-4fce-bcbe-93f80e857374.png">
3. Select Player which was executed earlier from the pull-down menu on the upper left window.(It's equivalent to selecting where to connect in Profiler or Frame Debugger)
4. Select Player's Platform in the center of Window pull-down menu.
5. Press Reload button.
6. The Scene opened in Unity Editor will be displayed on the actual device. (It'll take sometime to update depending on the number of Assets the scene holds)

That'll be all
## Other

If you have any problems, please report them from the Issue Tracker.
Comments and feedback are welcome!</br>
__Katsumasa Kimura：katsumasa@unity3d.com__


