using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.SceneManagement;

// (C) UTJ
// Katsumasa.Kimura
namespace Utj.UnitySceneViewerKun
{
    // UnitySceneViewerKunのPlayer(実機)側のクラスです
    public class UnitySceneViewerKunPlayer : MonoBehaviour
    {
        

        public static readonly System.Guid kMsgSendEditorToPlayer = new System.Guid("7be7ee8b81e040fe906820341961de4c");
        public static readonly System.Guid kMsgSendPlayerToEditor = new System.Guid("63ab807d9485442689a55b16a85df1ca");
        static UnitySceneViewerKunPlayer m_instance;
        AssetBundleCreateRequest m_assetBundleCreateRequest;
        private string m_sceneName = null;



        private void Awake()
        {
            if(m_instance != null)
            {
                Destroy(gameObject);
            } else
            {
                m_instance = this;
            }
        }


        // Use this for initialization
        void Start()
        {           
        }


        // Update is called once per frame
        void Update()
        {
        }


        private void OnEnable()
        {
            Debug.Log("OnEnable");
            PlayerConnection.instance.Register(kMsgSendEditorToPlayer, OnMessageEvent);
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }


        private void OnDisable()
        {
            Debug.Log("OnDisable");
            PlayerConnection.instance.Unregister(kMsgSendEditorToPlayer, OnMessageEvent);
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }


        private void OnMessageEvent(MessageEventArgs args)
        {
            Debug.Log("OnMessageEvent");
            var text = string.Format("Recive Data");
            var datas = System.Text.Encoding.ASCII.GetBytes(text);
            PlayerConnection.instance.Send(kMsgSendPlayerToEditor, datas);


            m_assetBundleCreateRequest = AssetBundle.LoadFromMemoryAsync(args.data);
            m_assetBundleCreateRequest.completed += AssetBundleLoadCB;
        }


        void AssetBundleLoadCB(AsyncOperation asyncOperation)
        {
            Debug.Log("AssetBundleLoadCB");
            if (m_sceneName != null)
            {
                var text = string.Format("Unload Scene");
                var datas = System.Text.Encoding.ASCII.GetBytes(text);
                PlayerConnection.instance.Send(kMsgSendPlayerToEditor, datas);

                SceneManager.UnloadSceneAsync(m_sceneName);
            }
            else
            {
                OnSceneUnloaded(SceneManager.GetActiveScene());
            }
        }


        void OnSceneUnloaded(Scene current)
        {
            Debug.Log("OnSceneUnloaded");
            var text = string.Format("Reload Scene");
            var datas = System.Text.Encoding.ASCII.GetBytes(text);
            PlayerConnection.instance.Send(kMsgSendPlayerToEditor, datas);

            m_sceneName = System.IO.Path.GetFileNameWithoutExtension(m_assetBundleCreateRequest.assetBundle.GetAllScenePaths()[0]);
            Debug.Log("SceneName:" + m_sceneName);
            var asyncOperation = SceneManager.LoadSceneAsync(m_sceneName, LoadSceneMode.Additive);
            asyncOperation.allowSceneActivation = true;
        }


        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("OnSceneLoaded");
            if (m_assetBundleCreateRequest != null)
            {
                var text = string.Format("Success");
                var datas = System.Text.Encoding.ASCII.GetBytes(text);
                PlayerConnection.instance.Send(kMsgSendPlayerToEditor, datas);

                m_assetBundleCreateRequest.assetBundle.Unload(false);
                m_assetBundleCreateRequest = null;
                SceneManager.SetActiveScene(scene);
            }
        }


        private void ConnectionCB(int playerId)
        {
            Debug.Log("UnitySceneViewerKunPlayer:Connect");
        }


        private void DisconnectionCB(int playerId)
        {
            Debug.Log("UnitySceneViewerKunPlayer:DisConnect");
        }

        
    }
}
