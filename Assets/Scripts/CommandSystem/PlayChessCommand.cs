using GobangSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 下棋命令
/// </summary>
public class PlayChessCommand : Command
{
    /// <summary>
    /// 执行目标
    /// </summary>
    private ChessManager chessManager;
    /// <summary>
    /// 执行的值(棋子)
    /// </summary>
    private Chess chess;



    public PlayChessCommand(ChessManager chessManager, Chess chess)
    {
        this.chessManager = chessManager;
        this.chess = chess;
    }

    /// <summary>
    /// 执行下棋命令, 返回是否真的能在当前位置下棋
    /// </summary>
    public bool Execute()
    {
        return chessManager.PlayChess(chess);
    }

    /// <summary>
    /// 撤销
    /// </summary>
    public void UnDo()
    {
        chessManager.UndoChess(chess);
    }

    /// <summary>
    /// 重做
    /// </summary>
    public void ReDo()
    {
        chessManager.RedoChess(chess);
    }

}
