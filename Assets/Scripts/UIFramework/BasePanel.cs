using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIFramework
{
    public class BasePanel : MonoBehaviour
    {

        public virtual void OpenPanel()
        {
            this.gameObject.SetActive(true);
        }
        public virtual void ClosePanel()
        {
            this.gameObject.SetActive(false);
        }

        public virtual void BackStep()
        {

        }
        public virtual void NextStep()
        {

        }

        public virtual void MoveToTop()
        {
            this.transform.SetAsFirstSibling();
        }
        public virtual void MoveToBottom()
        {
            this.transform.SetAsLastSibling();
        }



        protected void InitUIResolution(bool isUpDownDisplay = false)
        {
            int displayCount = Display.displays.Length;
            int width = Screen.currentResolution.width;
            int height = Screen.currentResolution.height;
            if (isUpDownDisplay)
            {
                GetComponent<CanvasScaler>().referenceResolution = new Vector2(width * displayCount / 2, height * 2);
            }
            else
            {
                GetComponent<CanvasScaler>().referenceResolution = new Vector2(width * displayCount, height);
            }
            Debug.LogErrorFormat("StartUIManager 初始化UI分辨率----  {0} ", GetComponent<CanvasScaler>().referenceResolution);
        }
    }
}