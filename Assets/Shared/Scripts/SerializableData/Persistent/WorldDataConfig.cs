using Shared.SerializableData.Raw.World;
using UnityEngine;

namespace Shared.SerializableData.Persistent
{
    [CreateAssetMenu(menuName = SharedPathAliases.ConfigsPath + "WorldDataConfig")]
    public class WorldDataConfig : ScriptableObject
    {
        [SerializeField] private WorldData worldData;
        [SerializeField] private GameObject worldGameObject;
        
        public WorldData WorldData => worldData;
        public GameObject WorldGameObject => worldGameObject;
    }
}