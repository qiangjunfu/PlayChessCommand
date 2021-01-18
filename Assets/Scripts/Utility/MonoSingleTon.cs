using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityUtility
{
    public class MonoSingleTon<T> : MonoBehaviour where T : MonoSingleTon<T>
    {
        private static bool isInit;
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(T).ToString());
                    go.AddComponent<T>();
                }
                return instance;
            }
        }
        
        protected virtual void Awake()
        {
            if (instance == null && !isInit)
            {
                isInit = true;
                instance = GetComponent<T>();
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        protected virtual void OnDestroy()
        {
            if (instance != null) instance = null;
        }

    }
}