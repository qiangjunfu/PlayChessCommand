using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FactorySystem
{
    public class SpriteFactory
    {

        public Sprite CreateSprite(string name)
        {
            return AssetSystem.AssetManager.Instance.ResourcesAssetLoad.LoadSprite(name);
        }

        public Sprite[] LoadAtlas(string path)
        {
            return AssetSystem.AssetManager.Instance.ResourcesAssetLoad.LoadAtlas(path);
        }
    }
}