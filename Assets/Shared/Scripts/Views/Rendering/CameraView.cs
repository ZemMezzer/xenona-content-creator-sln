using System.Collections.Generic;
using UnityEngine;

namespace Shared.Views.Rendering
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private List<Camera> cameras;
        
        public Camera ActiveCamera { get; private set; }
        public bool HasActiveCamera => ActiveCamera != null;

        private void Awake()
        {
            if(cameras.Count<=0)
                return;

            ActiveCamera = cameras[0];
        }
    }
}
