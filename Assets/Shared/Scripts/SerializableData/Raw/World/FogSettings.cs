using System;
using UnityEngine;

namespace Shared.SerializableData.Raw.World
{
    [Serializable]
    public struct FogSettings
    {
        [SerializeField] private Color fogColor;
        [SerializeField] private float fogStart;
        [SerializeField] private float fogEnd;

        public Color Color => fogColor;
        public float Start => fogStart;
        public float End => fogEnd;
    }
}