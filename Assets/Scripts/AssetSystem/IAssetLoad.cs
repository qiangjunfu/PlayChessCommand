using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AssetSystem
{
    public abstract class IAssetLoad
    {
        public abstract GameObject LoadCharacters(string name);
        public abstract AudioClip LoadAudioClip(string name);
        public abstract Sprite LoadSprite(string name);
        public abstract GameObject LoadUIPanel(string name);
    }
}