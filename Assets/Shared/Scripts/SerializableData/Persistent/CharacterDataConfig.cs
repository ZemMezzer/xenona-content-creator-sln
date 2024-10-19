using Shared.SerializableData.Raw.Character;
using UnityEngine;

namespace Shared.SerializableData.Persistent
{
    [CreateAssetMenu(menuName = SharedPathAliases.ConfigsPath + "CharacterDataConfig")]
    public class CharacterDataConfig : ScriptableObject
    {
        [SerializeField] private CharacterData characterData;
        [SerializeField] private GameObject characterGameObject;

        public CharacterData CharacterData => characterData;
        public GameObject CharacterGameObject => characterGameObject;
    }
}