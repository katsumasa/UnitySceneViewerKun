using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.SceneManagement;

// (C) UTJ
// Katsumasa.Kimura
namespace UTJ.UnitySceneViewerKun
{
    // UnitySceneViewerKunのPlayer(実機)側のクラスです
    public class UnitySceneViewerKunPlayer : UTJ.RemoteConnect.Player
    {                
        static UnitySceneViewerKunPlayer m_instance;
        AssetBundleCreateRequest m_assetBundleCreateRequest;
        private string m_sceneName = null;



        private void Awake()
        {
            if(m_instance != null)
            {
                Destroy(gameObject);
            } 
            else
            {
                m_instance = this;
                kMsgSendEditorToPlayer = new System.Guid("7be7ee8b81e040fe906820341961de4c");
                kMsgSendPlayerToEditor = new System.Guid("63ab807d9485442689a55b16a85df1ca");
                remoteMessageCB = MessageCB;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }


        protected override void OnDisable()
        {
            base.OnDisable();            
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }



        void MessageCB(UTJ.RemoteConnect.Message remoteMessageBase)
        {
            var message = (UTJ.UnitySceneViewerKun.UnitySceneViewerKunMessage)remoteMessageBase;
            m_assetBundleCreateRequest = AssetBundle.LoadFromMemoryAsync(message.assetBundle);
            m_assetBundleCreateRequest.completed += AssetBundleLoadCB;
        }

        
        void AssetBundleLoadCB(AsyncOperation asyncOperation)
        {
            Debug.Log("AssetBundleLoadCB");
            if (m_sceneName != null)
            {
                SceneManager.UnloadSceneAsync(m_sceneName);
            }
            else
            {
                OnSceneUnloaded(SceneManager.GetActiveScene());
            }
        }


        void OnSceneUnloaded(Scene current)
        {
            //Debug.Log("OnSceneUnloaded");
            m_sceneName = System.IO.Path.GetFileNameWithoutExtension(m_assetBundleCreateRequest.assetBundle.GetAllScenePaths()[0]);
            Debug.Log("SceneName:" + m_sceneName);
            var asyncOperation = SceneManager.LoadSceneAsync(m_sceneName, LoadSceneMode.Additive);
            asyncOperation.allowSceneActivation = true;
        }


        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //Debug.Log("OnSceneLoaded");
            if (m_assetBundleCreateRequest != null)
            {
                m_assetBundleCreateRequest.assetBundle.Unload(false);
                m_assetBundleCreateRequest = null;
                SceneManager.SetActiveScene(scene);
            }
        }
        
    }
}
