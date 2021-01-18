﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GobangSystem
{
    public delegate void PlayChessDelegate(Chess chess);
    public delegate void UndoChessDelegate(Chess chess);
    public delegate void RedoChessDelegate(Chess chess);
    public delegate void WinDelegate(Chess chess);


    [System.Serializable]
    public class GobangManager
    {
        public event PlayChessDelegate mPlayChessEvent;
        public event UndoChessDelegate mUndoChessEvent;
        public event RedoChessDelegate mRedoChessEvent;
        public event WinDelegate mWinEvent;

        [Header("相同类型棋子个数")] [SerializeField] private int count;
        [Header("所有的棋盘格子列表")] [SerializeField] private List<ChessGrid> chessGridList;
        [Header("落下的棋子横向对应的格子列表")] [SerializeField] private List<ChessGrid> horizontalChessGridList;
        [Header("落下的棋子纵向对应的格子列表")] [SerializeField] private List<ChessGrid> verticalChessGridList;
        [Header("左斜线对应的格子列表")] [SerializeField] private List<ChessGrid> leftDiagonalChessGridList;
        [Header("右斜线对应的格子列表")] [SerializeField] private List<ChessGrid> rightDiagonalChessGridList;
        [Header("白棋子列表")] [SerializeField] private List<Chess> whiteCheesList;
        [Header("黑棋子列表")] [SerializeField] private List<Chess> blackCheesList;
        private int henglie; //横列格子个数
        private int zonglie; //纵列格子个数
        /// <summary>
        /// 撤销栈
        /// </summary>
        private Stack<Chess> undoChessStack;
        /// <summary>
        /// 重做栈
        /// </summary>
        private Stack<Chess> redoChessStack;


        public GobangManager()
        {
            chessGridList = new List<ChessGrid>();
            horizontalChessGridList = new List<ChessGrid>();
            verticalChessGridList = new List<ChessGrid>();
            leftDiagonalChessGridList = new List<ChessGrid>();
            rightDiagonalChessGridList = new List<ChessGrid>();
            whiteCheesList = new List<Chess>();
            blackCheesList = new List<Chess>();
            henglie = 10;
            zonglie = 10;
            undoChessStack = new Stack<Chess>();
            redoChessStack = new Stack<Chess>();

            StartGame();
        }

        /// <summary>
        /// 下棋
        /// </summary>
        public void PlayChess(Chess chess)
        {
            ChessGrid chessGrid = GetChessGridByXY(chess.x, chess.y);
            if (chessGrid == null)
            {
                Debug.Log($"GobangManager -> PlayChess() -> 点位: {chess.x}，{chess.y}   不在格子列表中");
                return;
            }
            if (chessGrid.GetChess().chessType != ChessType.Null) //chessGrid.GetChess()一直不等于null 默认值???
            {
                Debug.Log($"GobangManager -> PlayChess() -> 点位: {chess.x}，{chess.y}   所在格子已经被占有{ chessGrid.GetChess().ToString() }");
                return;
            }

            chessGrid.SetChess(chess);
            switch (chess.chessType)
            {
                case ChessType.Null:
                    break;
                case ChessType.White:
                    whiteCheesList.Add(chess);
                    break;
                case ChessType.Black:
                    blackCheesList.Add(chess);
                    break;
                default:
                    break;
            }
            undoChessStack.Push(chess);
            redoChessStack.Clear(); //每次下棋清除重做栈的数据

            mPlayChessEvent?.Invoke(chess);

            Check(chess);
        }


        /// <summary>
        /// 撤销当前棋子
        /// </summary>
        public void UndoChess()
        {
            if (undoChessStack.Count > 0)
            {
                Chess chess = undoChessStack.Pop();
                redoChessStack.Push(chess);

                //删除棋子对应格子的东西
                for (int i = 0; i < chessGridList.Count; i++)
                {
                    if (chessGridList[i].GetChess() != null)
                    {
                        if (chessGridList[i].GetChess().chessType == chess.chessType &&
                           chessGridList[i].GetChess().x == chess.x && chessGridList[i].GetChess().y == chess.y)
                        {
                            chessGridList[i].SetChess(null);
                            mUndoChessEvent?.Invoke(chess);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 重做
        /// </summary>
        public void RedoChess()
        {
            if (redoChessStack.Count > 0)
            {
                Chess chess = redoChessStack.Pop();
                undoChessStack.Push(chess);

                ChessGrid chessGrid = GetChessGridByXY(chess.x, chess.y);
                if (chessGrid == null)
                {
                    Debug.Log($"GobangManager -> RedoChess() -> 点位: {chess.x}，{chess.y}   不在格子列表中");
                    return;
                }
                if (chessGrid.GetChess().chessType != ChessType.Null)
                {
                    Debug.Log($"GobangManager -> PlayChess() -> 点位: {chess.x}，{chess.y}   所在格子已经被占有{ chessGrid.GetChess().ToString() }");
                    return;
                }
                chessGrid.SetChess(chess);

                switch (chess.chessType)
                {
                    case ChessType.Null:
                        break;
                    case ChessType.White:
                        whiteCheesList.Add(chess);
                        break;
                    case ChessType.Black:
                        blackCheesList.Add(chess);
                        break;
                    default:
                        break;
                }
                mRedoChessEvent?.Invoke(chess);
            }
        }



        /// <summary>
        /// 开始游戏 创建棋盘
        /// </summary>
        public void StartGame()
        {
            int tempX = henglie / 2;
            int tempY = zonglie / 2;
            for (int i = 0 - tempX; i < henglie - tempX + 1; i++)
            {
                for (int j = 0 - tempY; j < zonglie - tempY + 1; j++)
                {
                    ChessGrid chessGrid = new ChessGrid(null, j, i);
                    chessGridList.Add(chessGrid);
                }
            }
        }

        /// <summary>
        /// 重置游戏数据
        /// </summary>
        public void ResetGame()
        {
            count = 0;
            chessGridList.Clear();
            horizontalChessGridList.Clear();
            verticalChessGridList.Clear();
            leftDiagonalChessGridList.Clear();
            rightDiagonalChessGridList.Clear();
            whiteCheesList.Clear();
            blackCheesList.Clear();

            undoChessStack.Clear();
            redoChessStack.Clear();

            StartGame();
        }


        /// <summary>
        /// 通过x y获取chessGridList列表对应格子
        /// </summary>
        ChessGrid GetChessGridByXY(int x, int y)
        {
            for (int i = 0; i < chessGridList.Count; i++)
            {
                if (chessGridList[i].PosX == x && chessGridList[i].PosY == y)
                {
                    return chessGridList[i];
                }
            }
            return null;
        }

        /// <summary>
        /// 检查是否连接5个棋子
        /// </summary>
        void Check(Chess chess)
        {
            horizontalChessGridList.Clear();
            verticalChessGridList.Clear();
            leftDiagonalChessGridList.Clear();
            rightDiagonalChessGridList.Clear();
            //筛选当前棋子对应的纵横ChessGrid列表
            for (int i = 0; i < chessGridList.Count; i++)
            {
                if (chessGridList[i].PosY == chess.y)
                {
                    horizontalChessGridList.Add(chessGridList[i]);
                }

                if (chessGridList[i].PosX == chess.x)
                {
                    verticalChessGridList.Add(chessGridList[i]);
                }

                //左上至右下斜线
                for (int k = 0; k < henglie; k++)
                {
                    if (chessGridList[i].PosX == chess.x - (k/** 80*/) && chessGridList[i].PosY == chess.y + (k))
                    {
                        //Debug.Log($"左上斜线 {chessGridList[i].ToString()} ---- currentChess: {currentChess.ToString() }");
                        leftDiagonalChessGridList.Add(chessGridList[i]);
                    }
                    if (chessGridList[i].PosX == chess.x + (k) && chessGridList[i].PosY == chess.y - (k))
                    {
                        if (k != 0)
                        {
                            //Debug.Log($"右下斜线  {chessGridList[i].ToString()} ---- currentChess: {currentChess.ToString() }");
                            leftDiagonalChessGridList.Add(chessGridList[i]);
                        }
                    }
                }

                //右上至左下斜线
                for (int j = 0; j < henglie; j++)
                {
                    if (chessGridList[i].PosX == chess.x + (j) && chessGridList[i].PosY == chess.y + (j))
                    {
                        //Debug.Log($"右上斜线 {chessGridList[i].ToString()} ---- currentChess: {currentChess.ToString() }");
                        rightDiagonalChessGridList.Add(chessGridList[i]);
                    }
                    if (chessGridList[i].PosX == chess.x - (j) && chessGridList[i].PosY == chess.y - (j))
                    {
                        if (j != 0)
                        {
                            //Debug.Log($"左下斜线 {chessGridList[i].ToString()} ---- currentChess: {currentChess.ToString() }");
                            rightDiagonalChessGridList.Add(chessGridList[i]);
                        }
                    }
                }
            }

            //横向 检测是否5子连接
            count = 0;
            for (int i = 0; i < horizontalChessGridList.Count; i++)
            {
                if (horizontalChessGridList[i].GetChess() != null && horizontalChessGridList[i].GetChess().chessType == chess.chessType)
                {
                    count++;
                    if (count >= 5)
                    {
                        Debug.Log($"{chess.chessType}  胜利-----------横向连接");

                        mWinEvent?.Invoke(chess);
                        return;
                    }
                }
                else
                {
                    count = 0;
                }
            }

            //纵向 检测是否5子连接
            count = 0;
            for (int i = 0; i < verticalChessGridList.Count; i++)
            {
                if (verticalChessGridList[i].GetChess() != null && verticalChessGridList[i].GetChess().chessType == chess.chessType)
                {
                    count++;
                    if (count >= 5)
                    {
                        Debug.Log($"{chess.chessType}  胜利-----------纵向连接");

                        mWinEvent?.Invoke(chess);
                        return;
                    }
                }
                else
                {
                    count = 0;
                }
            }

            //左上至右下斜线  
            count = 0;
            for (int i = 0; i < leftDiagonalChessGridList.Count; i++)
            {
                if (leftDiagonalChessGridList[i].GetChess() != null && leftDiagonalChessGridList[i].GetChess().chessType == chess.chessType)
                {
                    count++;
                    if (count >= 5)
                    {
                        Debug.Log($"{chess.chessType}  胜利-----------左斜线连接");

                        mWinEvent?.Invoke(chess);
                        return;
                    }
                }
                else
                {
                    count = 0;
                }
            }

            //右上至左下斜线  
            count = 0;
            for (int i = 0; i < rightDiagonalChessGridList.Count; i++)
            {
                if (rightDiagonalChessGridList[i].GetChess() != null && rightDiagonalChessGridList[i].GetChess().chessType == chess.chessType)
                {
                    count++;
                    if (count >= 5)
                    {
                        Debug.Log($"{chess.chessType}  胜利-----------右斜线连接");

                        mWinEvent?.Invoke(chess);
                        return;
                    }
                }
                else
                {
                    count = 0;
                }
            }
        }

    }
}