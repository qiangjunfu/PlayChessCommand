using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GobangSystem
{
    public enum ChessType
    {
        Null,
        White,
        Black
    }

    [System.Serializable]
    /// <summary>
    /// 棋子类
    /// </summary>
    public class Chess
    {
        public string path;
        public ChessType chessType;
        public int x;
        public int y;
        
        public Chess(string path, ChessType chessType, int x, int y)
        {
            this.path = path;
            this.chessType = chessType;
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            string str = $"Chess -> chessType:{path}  {chessType}  x:{x}  y:{y}";
            return str;
        }
    }
}