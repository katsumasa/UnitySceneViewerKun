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

namespace Utj.UnitySceneViewerKun
{
    public class UnitySceneViewerKunEditor : EditorWindow
    {

        enum Compression
        {
            None,
            Normal,
            ChunkBasedCompression,
        };


        static readonly GUIContent m_plattoformGuiContent = new GUIContent("Plattform", "接続する機材のプラットフォーム");
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


        [MenuItem("Window/UnitySceneViewerKun")]
        static void Create()
        {
            var window = (UnitySceneViewerKunEditor)EditorWindow.GetWindow(typeof(UnitySceneViewerKunEditor));
            window.Show();
            window.titleContent = new GUIContent("UnitySceneViewerKunEditor");        
        }


#if !UNITY_2018_1_OR_NEWER
        void Reflection()
        {
            // この関数内の処理は全く、推奨出来ませんので参考にしないでください。
            // AttachProfilerUIとは
            // ProfilerやConsole WindowにあるTargetの選択用Pulldown UI
            // internal classの為、Relectionで無理やり
            if (AttachProfilerUI == null)
            {
                Assembly assembly = Assembly.Load("UnityEditor");
                AttachProfilerUI = assembly.GetType("UnityEditor.AttachProfilerUI");
            }
            if ((m_attachProfilerUI == null) && (AttachProfilerUI != null))
            {
                m_attachProfilerUI = AttachProfilerUI.InvokeMember(
                    null
                , BindingFlags.CreateInstance
                , null
                , null
                , new object[] { }
                );
            }
            if (m_attachProfilerUIOnGUILayOut == null)
            {
                m_attachProfilerUIOnGUILayOut = AttachProfilerUI.GetMethod("OnGUILayout");
            }
    }
#endif

        private void Initialize()
        {
            if (m_registered == false)
            {
                UnityEditor.Networking.PlayerConnection.EditorConnection.instance.Initialize();
                UnityEditor.Networking.PlayerConnection.EditorConnection.instance.Register(UnitySceneViewerKunPlayer.kMsgSendPlayerToEditor, OnMessageEvent);
                m_registered = true;
            }
        }


        private void OnDestroy()
        {
            if (m_registered == true)
            {
                // Registされているかどうか判断する術がない・・・
                UnityEditor.Networking.PlayerConnection.EditorConnection.instance.Unregister(UnitySceneViewerKunPlayer.kMsgSendPlayerToEditor, OnMessageEvent);
                UnityEditor.Networking.PlayerConnection.EditorConnection.instance.DisconnectAll();
                m_registered = false;
            }
        }


        // SampleのようにOnEnable/OnDisableで処理すると通信が不安体になる
        private void OnEnable()
        {
#if UNITY_2018_1_OR_NEWER
            if (attachProfilerState == null)
            {
                attachProfilerState = ConnectionUtility.GetAttachToPlayerState(this);
            }
#endif
        }


        private void OnDisable()
        {
#if UNITY_2018_1_OR_NEWER
            attachProfilerState.Dispose();
            attachProfilerState = null;
#endif
        }


        private void OnMessageEvent(MessageEventArgs args)
        {
            m_playerToEditorMessage = Encoding.ASCII.GetString(args.data);
        }

        
        private void OnGUI()
        {
            Initialize();
#if UNITY_2018_1_OR_NEWER
            if (attachProfilerState != null)
            {
                ConnectionGUILayout.AttachToPlayerDropdown(attachProfilerState, EditorStyles.toolbarDropDown);
            }        
#else
            Reflection();
            if (m_attachProfilerUIOnGUILayOut != null && m_attachProfilerUI != null)
            {
                m_attachProfilerUIOnGUILayOut.Invoke(m_attachProfilerUI, new object[] { this });
            }
#endif
            var playerCount = EditorConnection.instance.ConnectedPlayers.Count;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Format("{0} players connected.", playerCount));
            int i = 0;
            foreach (var p in EditorConnection.instance.ConnectedPlayers)
            {
                builder.AppendLine(string.Format("[{0}] - {1} {2}", i++, p.name, p.playerId));
            }
            EditorGUILayout.HelpBox(builder.ToString(), MessageType.Info);


            var scene = SceneManager.GetActiveScene();
            EditorGUILayout.LabelField("SceneName " + scene.name);
            EditorGUILayout.LabelField("ScenePath " + scene.path);


            m_buildTarget = (BuildTarget) EditorGUILayout.EnumPopup(m_plattoformGuiContent, m_buildTarget);
            m_compression = (Compression)EditorGUILayout.EnumPopup(m_compressionGuiContent, m_compression);

            EditorGUILayout.BeginHorizontal("Button");
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
                    EditorConnection.instance.Send(UnitySceneViewerKunPlayer.kMsgSendEditorToPlayer, bytes);
                    EditorUtility.ClearProgressBar();

                    m_playerToEditorMessage = "Send Message";
                }
            }
            EditorGUILayout.EndHorizontal();            
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
