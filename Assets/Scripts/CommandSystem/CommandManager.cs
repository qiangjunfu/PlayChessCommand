using GobangSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CommandManager
{
    /// <summary>
    /// 撤销栈
    /// </summary>
    private Stack<Command> undoCommandStack;
    /// <summary>
    /// 重做栈
    /// </summary>
    private Stack<Command> redoCommandStack;


    public CommandManager()
    {
        undoCommandStack = new Stack<Command>();
        redoCommandStack = new Stack<Command>();
    }


    /// <summary>
    /// 执行命令  返回真才能保存命令
    /// </summary>
    public void Execute(Command command)
    {
        if (command.Execute())
        {
            undoCommandStack.Push(command);
            redoCommandStack.Clear();
        }
    }

    /// <summary>
    /// 撤销
    /// </summary>
    public void UnDo()
    {
        if (undoCommandStack.Count > 0)
        {
            Command comm = undoCommandStack.Pop();
            comm.UnDo();
            redoCommandStack.Push(comm);
        }
    }

    /// <summary>
    /// 重做
    /// </summary>
    public void ReDo()
    {
        if (redoCommandStack.Count > 0)
        {
            Command comm = redoCommandStack.Pop();
            comm.ReDo();
            undoCommandStack.Push(comm);
        }
    }
}
