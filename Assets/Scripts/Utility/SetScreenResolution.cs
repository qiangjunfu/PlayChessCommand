using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UnityUtility
{
    public class SetScreenResolution
    {
        [DllImport("user32.dll")] public static extern bool SetWindowPos(System.IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);//导入设置窗口函数
        [DllImport("user32.dll")] public static extern IntPtr GetActiveWindow();//导入当前活动窗口
        public const uint SWP_SHOWWINDOW = 0x0040;  //显示窗口
        private const int displayNumber_2 = 2;


        public void InitScreenResolution(bool isUpDownDisplay = false)
        {
            int displayCount = Display.displays.Length;
            int width = Screen.currentResolution.width;
            int height = Screen.currentResolution.height;

            if (displayCount == 1)
            {
                Screen.SetResolution(width * displayCount, height, true);
                Screen.fullScreen = true;
                Debug.LogErrorFormat("设置屏幕分辨率: {0}个显示屏 , 宽:{1}  高:{2}", displayCount, width, height);
                return;
            }

            if (isUpDownDisplay)
            {
                if (displayCount == 2)
                {
                    Screen.SetResolution(width * displayCount / 2, height * 2, false);
                    SetWindowPos(GetActiveWindow(),0, 0, 0, width * displayCount / 2, height * 2, SWP_SHOWWINDOW);
                    Debug.LogErrorFormat("设置屏幕分辨率: {0}个显示屏 上下屏 , 宽:{1}  高:{2}", displayCount, width * displayCount / 2, height * 2);
                }
                else if (displayCount > 2 && displayCount <= 4)
                {
                    Screen.SetResolution(width * displayCount / 2, height * 2, false);
                    SetWindowPos(GetActiveWindow(), -1920, 0, -30, width * displayCount / 2, height * 2, SWP_SHOWWINDOW);
                    Debug.LogErrorFormat("设置屏幕分辨率: {0}个显示屏 上下屏 , 宽:{1}  高:{2}", displayCount, width * displayCount / 2, height * 2);
                }
            }
            else
            {
                Screen.SetResolution(width * displayCount, height, false);
                SetWindowPos(GetActiveWindow(), 0, 0, 0, width * displayCount, height, SWP_SHOWWINDOW);
                Debug.LogErrorFormat("设置屏幕分辨率: {0}个显示屏 , 宽:{1}  高:{2}", displayCount, width * displayCount, height);
            }
        }

    }
}