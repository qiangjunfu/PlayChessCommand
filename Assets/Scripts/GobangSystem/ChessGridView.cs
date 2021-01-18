using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GobangSystem
{
    public class ChessGridView : MonoBehaviour
    {
        /// <summary>
        /// 在UI坐标中的x
        /// </summary>
        [Header("在UI坐标中的X")] [SerializeField] private int posX;
        /// <summary>
        /// 在UI坐标中的Y
        /// </summary>
        [Header("在UI坐标中的Y")] [SerializeField] private int posY;

        public int PosX { get => posX; }
        public int PosY { get => posY; }


        void Start()
        {
            posX = (int)transform.localPosition.x;
            posY = (int)transform.localPosition.y;
        }
    }
}