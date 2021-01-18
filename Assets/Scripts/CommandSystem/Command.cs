using GobangSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Command
{
    /// <summary>
    /// 执行命令 
    /// </summary>
    bool Execute();
    /// <summary>
    /// 撤销
    /// </summary>
    void UnDo();
    /// <summary>
    /// 重做
    /// </summary>
    void ReDo();
}
