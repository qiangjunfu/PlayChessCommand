using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityUtility
{
    public   class UITool 
    {
        /// <summary>
        /// 获取UI组件在屏幕的坐标
        /// </summary>
        public static Vector2 GetUIInScreenPos(RectTransform rt, Camera uiCamera)
        {
            return RectTransformUtility.WorldToScreenPoint(uiCamera, rt.position);
        }

        /// <summary>
        /// 获取鼠标下T类型组件(仅最上层UI)
        /// </summary>
        public static T GetMouseUIComponent<T>(UnityEngine.UI.GraphicRaycaster canvasGraphic, EventSystem eventSystem)
        {
            PointerEventData eventData = new PointerEventData(eventSystem);
            eventData.pressPosition = Input.mousePosition;
            eventData.position = Input.mousePosition;

            List<RaycastResult> list = new List<RaycastResult>();
            canvasGraphic.Raycast(eventData, list);
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    T t = list[i].gameObject.GetComponent<T>();
                    if (t != null)
                    {
                        //Debug.LogFormat("鼠标下检测到的UI: {0}  {1} ", i, list[i].gameObject);
                        return t;
                    }
                }
            }
            return default(T);
        }


        /// <summary>
        /// 获取鼠标下UI
        /// </summary>
        public static void GetMouseUI(UnityEngine.UI.GraphicRaycaster canvasGraphic, EventSystem eventSystem)
        {
            PointerEventData eventData = new PointerEventData(eventSystem);
            eventData.pressPosition = Input.mousePosition;
            eventData.position = Input.mousePosition;

            List<RaycastResult> list = new List<RaycastResult>();
            canvasGraphic.Raycast(eventData, list);
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Debug.LogFormat("鼠标下检测到的UI: {0}  {1} ", i, list[i].gameObject);
                }
            }
        }

        /// <summary>
        /// 获取鼠标下的3d物体
        /// </summary>
        public static GameObject GetCastObjByCast()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isHit = Physics.Raycast(ray, out hit);
            if (isHit)
            {
                Debug.LogFormat("射线检测到鼠标下的3D物体: {0}", hit.transform.name);
                return hit.collider.gameObject;
            }
            return null;
        }


        /// <summary>
        /// 铺满父物体UI
        /// </summary>
        public static void FullRectParent(RectTransform rt)
        {
            rt.anchoredPosition3D = Vector3.zero;             //posX  posY  posZ         
            rt.sizeDelta = Vector2.zero;                      //width height
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.pivot = new Vector2(0.5f, 0.5f);               //中心点
            rt.localRotation = Quaternion.Euler(Vector3.zero);//局部旋转
            rt.localScale = Vector3.one;                      //局部缩放
        }
    }
}
