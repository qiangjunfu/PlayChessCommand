using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GobangSystem
{
    [System.Serializable]
    public class ChessGrid
    {
        [SerializeField] private Chess chess;
        [SerializeField] private int posX;
        [SerializeField] private int posY;

        public int PosX { get => posX; }
        public int PosY { get => posY; }

        public ChessGrid(Chess chess ,int posX, int posY)
        {
            this.chess = chess;
            this.posX = posX;
            this.posY = posY;
        }


        public void SetChess(Chess chess)
        {
            this.chess = chess;
        }
        public Chess GetChess()
        {
            return chess;
        }


        public override string ToString()
        {
            string str = $"ChessGrid -> chess:{chess}  posX:{posX }  posY:{posY } ";
            //Debug.Log(str);
            return str;
        }
    }
}