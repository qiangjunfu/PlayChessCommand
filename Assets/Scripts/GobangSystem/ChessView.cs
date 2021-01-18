using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GobangSystem
{
    public class ChessView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Chess chess;

        public Chess GetChess { get => chess; } 


        public void SetChess(Chess _chess)
        {
            this.chess = _chess;

            switch (chess.chessType)
            {
                case ChessType.Null:
                    break;
                case ChessType.White:
                    image.color = Color.white;
                    break;
                case ChessType.Black:
                    image.color = Color.black;
                    break;
                default:
                    break;
            }
        }

    }
}