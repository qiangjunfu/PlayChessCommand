using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityUtility
{
    public class FindChildObject
    {
        public static T FindChildComponent<T>(GameObject parent, string childName)
        {
            GameObject GO = FindChild(parent, childName);
            if (GO == null)
            {
                Debug.LogError("在游戏物体" + parent + "下面查找不到" + childName);
                return default(T);
            }
            return GO.GetComponent<T>();
        }


        public static GameObject FindChild(GameObject parent, string childName)
        {
            Transform[] childrens = parent.GetComponentsInChildren<Transform>();
            bool isFinded = false;
            Transform child = null;
            for (int i = 0; i < childrens .Length ; i++)
            {
                if (childrens [i] .name  == childName)
                {
                    if (isFinded)
                    {
                        Debug.LogWarning("在游戏物体:" + parent + " 下存在不止一个同名子物体:" + childName);
                    }
                    isFinded = true;
                    child = childrens[i];
                }
            }
            if (isFinded)
            {
                return child.gameObject;
            }
            else
            {
                Debug.LogErrorFormat("找不到----");
                return null;
            }
        }
        

        public static void AttachGameObject(GameObject parent, GameObject child)
        {
            child.transform.SetParent(parent.transform);
            child.transform.localPosition = Vector3.zero;
            child.transform.localScale = Vector3.one;
            child.transform.localEulerAngles = Vector3.zero;

        }
    }
}