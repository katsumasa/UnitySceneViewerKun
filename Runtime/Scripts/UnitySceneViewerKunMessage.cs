using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTJ.UnitySceneViewerKun
{
    [System.Serializable]
    public class UnitySceneViewerKunMessage : UTJ.RemoteConnect.Message
    {
            
        [SerializeField] public byte[] assetBundle;
    }
}