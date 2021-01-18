using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FactorySystem
{
    public class UIPanelFactory
    {
        public T CreateUIPanel<T>(string uiPanelName, Transform parent) where T : new()
        {
            T t = new T();
            GameObject go = AssetSystem.AssetManager.Instance.ResourcesAssetLoad.LoadUIPanel(uiPanelName);
            go.transform.SetParent(parent);
            go.name = uiPanelName;
            t = go.GetComponent<T>();
            //RectTransform rt = go.GetComponent<RectTransform>();
            //rt.sizeDelta = Vector2.zero;
            //rt.localScale = Vector3.one;
            return t;
        }


        public T CreateUIPanel<T>(string uiPanelName, Transform parent, Vector2 sizeDelta) where T : Component
        {
            GameObject go = AssetSystem.AssetManager.Instance.ResourcesAssetLoad.LoadUIPanel(uiPanelName);
            go.transform.SetParent(parent);
            go.name = uiPanelName;
            T t = go.GetComponent<T>();

            RectTransform rt = go.GetComponent<RectTransform>();
            rt.anchoredPosition3D = Vector3.zero;
            rt.sizeDelta = sizeDelta;
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.localRotation = Quaternion.Euler(Vector3.zero);
            rt.localScale = Vector3.one;
            return t;
        }
        
        public T CreateUIPanel<T>(string uiPanelName, Transform parent, Vector3 _anchoredPosition3D, Vector2 sizeDelta) where T : Component
        {
            GameObject go = AssetSystem.AssetManager.Instance.ResourcesAssetLoad.LoadUIPanel(uiPanelName);
            go.transform.SetParent(parent);
            go.name = uiPanelName;
            T t = go.GetComponent<T>();

            RectTransform rt = go.GetComponent<RectTransform>();
            rt.anchoredPosition3D = _anchoredPosition3D;
            rt.sizeDelta = sizeDelta;
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.localRotation = Quaternion.Euler(Vector3.zero);
            rt.localScale = Vector3.one;
            return t;
        }

    }
}