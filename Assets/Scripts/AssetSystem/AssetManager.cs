using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace AssetSystem
{
    public class AssetManager 
    {
        #region 
        private AssetManager() { }
        private static AssetManager instance;
        public static AssetManager Instance
        {
            get
            {
                if (instance == null)
                {
                    var ctor = typeof(AssetManager).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new System.Type[0], null);
                    if (ctor == null)
                    {
                        throw new NullReferenceException("这个类必须有一个私有的无参的构造函数!!!");
                    }
                    instance = (AssetManager)ctor.Invoke(null);
                }
                return instance;
            }
        }

        //private static AssetManager instance;
        //public static AssetManager Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            GameObject go = new GameObject(typeof(AssetManager).ToString());
        //            go.AddComponent<AssetManager>();
        //        }

        //        return instance;
        //    }
        //}

        //public virtual void Awake()
        //{
        //    if (instance != null)
        //    {
        //        Debug.LogError("已经存在 " + this.GetType() + " 类型实例  删除当前物体");
        //        Destroy(gameObject);
        //        return;
        //    }

        //    instance = GetComponent<AssetManager>();
        //    DontDestroyOnLoad(this.gameObject);
        //}


        //private void OnDestroy()
        //{
        //    if (instance != this)
        //    {
        //        instance = null;
        //        //print(typeof(T) + "  实例已经被删除!");
        //    }
        //}
        #endregion


        [SerializeField ] private ResourcesAssetLoad mResourcesAssetLoad;
        public ResourcesAssetLoad ResourcesAssetLoad
        {
            get
            {
                if (mResourcesAssetLoad == null)
                {
                    mResourcesAssetLoad = new ResourcesAssetLoad();
                }
                return mResourcesAssetLoad;
            }
        }
        

    }
}