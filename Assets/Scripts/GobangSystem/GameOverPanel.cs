using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace GobangSystem
{
    public class GameOverPanel : BasePanel
    {
        [SerializeField] private Toggle toggle1;
        [SerializeField] private Toggle toggle2;
        [SerializeField] private Text text;
        [SerializeField] private bool isReset;


        private void Start()
        {
            toggle1.onValueChanged.AddListener((bool ison) =>
            {
                isReset = ison;
            });
            toggle2.onValueChanged.AddListener((bool ison) =>
            {
                Debug.Log("退出当前游戏-----------");
            });
        }

        public void OpenPanel(ChessType chessType)
        {
            base.OpenPanel();
            toggle1.isOn = false;
            toggle2.isOn = false;
            switch (chessType)
            {
                case ChessType.Null:
                    break;
                case ChessType.White:
                    text.text = "游戏结束, 白棋胜利 !!!";
                    break;
                case ChessType.Black:
                    text.text = "游戏结束, 黑棋胜利 !!!";
                    break;
                default:
                    break;
            }
        }

        public bool IsClickReset()
        {
            if (isReset)
            {
                bool temp = isReset;
                ClosePanel();
                isReset = false;
                return temp;
            }
            return false;
        }
    }
}