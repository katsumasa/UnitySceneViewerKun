using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking.PlayerConnection;
using UnityEditor;
using UnityEditor.Networking.PlayerConnection;
using UnityEditor.SceneManagement;
using System;


#if UNITY_2018_1_OR_NEWER
using UnityEngine.Experimental.Networking.PlayerConnection;
using ConnectionUtility = UnityEditor.Experimental.Networking.PlayerConnection.EditorGUIUtility;
using ConnectionGUILayout = UnityEditor.Experimental.Networking.PlayerConnection.EditorGUILayout;
#endif

namespace UTJ.UnitySceneViewerKun
{
    public class UnitySceneViewerKunEditor : UTJ.RemoteConnect.Editor.RemoteConnectEditorWindow
    {

        enum Compression
        {
            None,
            Normal,
            ChunkBasedCompression,
        };


        static readonly GUIContent m_plattoformGuiContent = new GUIContent("Platform", "接続する機材のプラットフォーム");
        static readonly GUIContent m_compressionGuiContent = new GUIContent("Compression", "実機に転送するデータの圧縮形式");
        static readonly string assetBundlePath = "Temp";
        static readonly string assetBundleName = "unitysceneviewerkunsubscene";

        bool m_registered = false;
        BuildTarget m_buildTarget = BuildTarget.Android;
        Compression m_compression = Compression.None;

#if UNITY_2018_1_OR_NEWER
        IConnectionState attachProfilerState;
#else
        Type AttachProfilerUI;
#endif
        MethodInfo m_attachProfilerUIOnGUILayOut;
        System.Object m_attachProfilerUI;
        string m_playerToEditorMessage ="";

        


        [MenuItem("Window/UTJ/UnitySceneViewerKun")]
        static void Create()
        {
            var window = (UnitySceneViewerKunEditor)EditorWindow.GetWindow(typeof(UnitySceneViewerKunEditor));
            window.titleContent = new GUIContent("UnitySceneViewerKunEditor");            
            window.Show();           
        }



        protected override void OnEnable()
        {
            kMsgSendEditorToPlayer = new System.Guid("7be7ee8b81e040fe906820341961de4c");
            kMsgSendPlayerToEditor = new System.Guid("63ab807d9485442689a55b16a85df1ca");
              
            base.OnEnable();
        }


        
        private void OnGUI()
        {
            base.ConnectionTargetSelectionDropdown();

            var scene = SceneManager.GetActiveScene();
            EditorGUILayout.LabelField("SceneName " + scene.name);
            EditorGUILayout.LabelField("ScenePath " + scene.path);

            m_buildTarget = (BuildTarget) EditorGUILayout.EnumPopup(m_plattoformGuiContent, m_buildTarget);
            m_compression = (Compression)EditorGUILayout.EnumPopup(m_compressionGuiContent, m_compression);
            
            if (GUILayout.Button("Reload") == true)
            {              
                var isSaveResult = EditorSceneManager.SaveScene(scene);
                if (isSaveResult)
                {
                    BuildAssetBundle();

                    UnityEditor.EditorUtility.DisplayProgressBar("UnitySceneViewerKun", "...", 0.0f);
                    byte[] bytes;
                    LoadAssetBundle(out bytes);
                    EditorUtility.ClearProgressBar();

                    UnityEditor.EditorUtility.DisplayProgressBar("UnitySceneViewerKun", "...", 0.0f);


                    var message = new UnitySceneViewerKunMessage();
                    message.assetBundle = bytes;

                    SendRemoteMessage(UTJ.UnitySceneViewerKun.UnitySceneViewerKunMessage.Serialize(message));
                    
                    EditorUtility.ClearProgressBar();

                    m_playerToEditorMessage = "Send Message";
                }
            }            
        }


        void BuildAssetBundle()
        {
            AssetBundleBuild[] assetBundleBuilds = new AssetBundleBuild[1];
            assetBundleBuilds[0].assetBundleName = assetBundleName;
            string[] assetNames = new string[1];


            var scene = SceneManager.GetActiveScene();           
            assetNames[0] = scene.path;
            assetBundleBuilds[0].assetNames = assetNames;

            BuildAssetBundleOptions buildAssetBundleOptions = BuildAssetBundleOptions.None;
            if(m_compression == Compression.None)
            {
                buildAssetBundleOptions = BuildAssetBundleOptions.UncompressedAssetBundle;
            } else if(m_compression == Compression.ChunkBasedCompression)
            {
                buildAssetBundleOptions = BuildAssetBundleOptions.ChunkBasedCompression;
            }

            BuildPipeline.BuildAssetBundles(assetBundlePath, assetBundleBuilds, buildAssetBundleOptions, m_buildTarget);
        }


        void LoadAssetBundle(out byte[] bytes)
        {            
            var fpath = System.IO.Directory.GetCurrentDirectory() + "/" + assetBundlePath + "/" + assetBundleName;
            bytes = System.IO.File.ReadAllBytes(fpath);

        }
    }
}
