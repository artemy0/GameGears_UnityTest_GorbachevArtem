using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteConfig", menuName = "ScriptableObjects/SpriteConfig")]
public class SpriteConfig : ScriptableObject
{
    [System.Serializable]
    public class KeySpritePare
    {
        public string Id;
        public Sprite Sprite;
    }

    [SerializeField] private List<KeySpritePare> keySpritePares;

    public Sprite GetSprite(string id)
    {
        foreach (var keySpritePare in keySpritePares)
        {
            if(keySpritePare.Id == id)
            {
                return keySpritePare.Sprite;
            }
        }

        return null;
    }
}