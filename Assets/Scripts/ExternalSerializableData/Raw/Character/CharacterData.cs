using System;
using UnityEngine;

namespace ExternalSerializableData.Raw.Character
{
    [Serializable]
    public class CharacterData
    {
        [Tooltip("Character Name in api database")]
        [SerializeField] private string characterName;
        [Tooltip("Default character world name that will be loaded by default with character first time. Leave this field empty if you don't want load any world with the character")]
        [SerializeField] private string defaultCharacterWorld;

        public string CharacterName => characterName;
        public string DefaultCharacterWorld => defaultCharacterWorld;
    }
}
