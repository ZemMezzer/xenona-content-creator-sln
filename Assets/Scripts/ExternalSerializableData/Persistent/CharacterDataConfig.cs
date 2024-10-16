using ExternalSerializableData.Raw.Character;
using UnityEngine;

namespace ExternalSerializableData.Persistent
{
    [CreateAssetMenu(menuName = ExternalPathAliases.ConfigsPath + "CharacterDataConfig")]
    public class CharacterDataConfig : ScriptableObject
    {
        [SerializeField] private CharacterData characterData;
        [SerializeField] private GameObject characterGameObject;

        public CharacterData CharacterData => characterData;
        public GameObject CharacterGameObject => characterGameObject;
    }
}