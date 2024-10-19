using System;
using UnityEngine;

namespace Shared.SerializableData.Raw.World
{
    [Serializable]
    public class WorldData
    {
        [SerializeField] private bool useAmbientColor;
        [SerializeField] private Color ambientColor;
        [SerializeField] private Material skybox;
        [SerializeField] private bool useFog;
        [SerializeField] private FogSettings fogSettings;

        public bool UseAmbientColor => useAmbientColor;
        public Color AmbientColor => ambientColor;
        public Material Skybox => skybox;
        public bool UseFog => useFog;
        public FogSettings FogSettings => fogSettings;
    }
}