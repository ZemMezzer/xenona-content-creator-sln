using ExternalSerializableData.Raw.World;
using UnityEngine;

namespace ExternalSerializableData.Persistent
{
    [CreateAssetMenu(menuName = ExternalPathAliases.ConfigsPath + "WorldDataConfig")]
    public class WorldDataConfig : ScriptableObject
    {
        [SerializeField] private WorldData worldData;
        [SerializeField] private GameObject worldGameObject;
        
        public WorldData WorldData => worldData;
        public GameObject WorldGameObject => worldGameObject;
    }
}