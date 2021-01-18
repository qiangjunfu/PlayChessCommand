using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityUtility
{
    public enum ReceiveDataStructType
    {
        None,
        ReceiveData,
        ReceiveData1,
        ReceiveData2
    }
    public enum SendDataStructType
    {
        None,
        SendData,
        SendData1,
        SendData2
    }


    [System.Serializable]
    public class SetConfig
    {
        /// <summary>
        /// 接收数据结构体类型
        /// </summary>
        public ReceiveDataStructType receiveDataStructType;
        /// <summary>
        /// 发送数据结构体类型
        /// </summary>
        public SendDataStructType sendDataStructType;
        /// <summary>
        /// 是否循环游戏
        /// </summary>
        public bool isLoopGame;
        /// <summary>
        /// 是否显示UI界面
        /// </summary>
        public bool isShowUI;
        /// <summary>
        /// 单独运行时的移动速度
        /// </summary>
        public float moveSpeed;

        public SetConfig() { }

        public SetConfig(ReceiveDataStructType receiveDataStructType, SendDataStructType sendDataStructType, bool isLoopGame, bool isShowUI, float moveSpeed)
        {
            this.receiveDataStructType = receiveDataStructType;
            this.sendDataStructType = sendDataStructType;
            this.isLoopGame = isLoopGame;
            this.isShowUI = isShowUI;
            this.moveSpeed = moveSpeed;
        }
    }
}