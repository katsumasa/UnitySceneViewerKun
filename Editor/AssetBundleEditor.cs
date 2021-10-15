using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Utj.UnitySceneViewerKun
{
    public class AssetBundleEditor
    {
        //[MenuItem("Window/UnitySceneViewKun/Build")]
        static void BuildAssetBundle()
        {
            AssetBundleBuild[] assetBundleBuilds = new AssetBundleBuild[1];
            assetBundleBuilds[0].assetBundleName = "unitysceneviewerkunsubscene";
            string[] assets = new string[1];
            assets[0] = "Assets/Scenes/SubScene.unity";
            assetBundleBuilds[0].assetNames = assets;

            BuildPipeline.BuildAssetBundles("Temp",assetBundleBuilds, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        }
    
    }
}