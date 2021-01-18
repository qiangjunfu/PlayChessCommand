using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AssetSystem
{
    [System.Serializable]
    public class ResourcesAssetLoad : IAssetLoad
    {
        public const  string CharactersPath = "Characters/";
        public const string AudioPath = "Audios/";
        public const string SpritePath = "Sprites/";
        public const string AtlasPath = "Atlas/";
        public const string UIPanelPath = "UiPanels/";
        public const string ItemViewPath = "ItemViews/";



        public override GameObject LoadUIPanel(string name)
        {
            return LoadGameObject(UIPanelPath + name);
        }
        public override GameObject LoadCharacters(string name)
        {
            return LoadGameObject(CharactersPath + name);
        }
        public override AudioClip LoadAudioClip(string name)
        {
            return Resources.Load(AudioPath + name, typeof(AudioClip)) as AudioClip;
        }
        public override Sprite LoadSprite(string name)
        {
            return Resources.Load<Sprite>(SpritePath + name);
        }
        public Sprite[] LoadAtlas (string path )
        {
            return Resources.LoadAll<Sprite>(AtlasPath + path);
        }
        public GameObject LoadItemViewObj(string name)
        {
            return LoadGameObject(ItemViewPath + name);
        }


        private GameObject LoadGameObject(string path)
        {
            GameObject g = Resources.Load<GameObject>(path);
            if (g == null)
            {
                Debug.LogError("无法加载资源，路径:" + path);
                return null;
            }
            GameObject go = LoadGameObject(g);
            return go;
        }
        private GameObject LoadGameObject(GameObject g)
        {
            return GameObject.Instantiate(g);
        }

    }
}