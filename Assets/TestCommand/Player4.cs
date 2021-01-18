using GobangSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player4 : MonoBehaviour
{
    [SerializeField] private ChessManager chessManager;
    [SerializeField] private CommandManager commandManager;
    [Header("---------------")] [SerializeField] private GraphicRaycaster graphicRaycaster;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameOverPanel gameOverPanel;
    [SerializeField] private bool isGameOver;
    /// <summary>
    /// 当前回合棋子类型
    /// </summary>
    [Header("当前回合棋子类型")] [SerializeField] private ChessType currenChessType;
    /// <summary>
    /// 所有棋子V层列表
    /// </summary>
    [Header("所有棋子V层列表")] [SerializeField] private List<ChessView> chessViewList = new List<ChessView>();



    #region 回调
    private void OnEnable()
    {
        chessManager = new ChessManager();
        chessManager.mPlayChessEvent += PlayChessCallback;
        chessManager.mUndoChessEvent += UndoCallback;
        chessManager.mRedoChessEvent += RedoCallback;
        chessManager.mWinEvent += WinCallback;
    }
    private void OnDisable()
    {
        chessManager.mPlayChessEvent -= PlayChessCallback;
        chessManager.mUndoChessEvent -= UndoCallback;
        chessManager.mRedoChessEvent -= RedoCallback;
        chessManager.mWinEvent -= WinCallback;
    }
    private void PlayChessCallback(Chess chess)
    {
        //Debug.Log($"下棋回调-----  {chess.ToString() }");
        CreateChessView(chess);
        ChangeChessType();
    }
    private void UndoCallback(Chess chess)
    {
        //Debug.Log($"悔棋回调-----  {chess.ToString() }");
        for (int i = 0; i < chessViewList.Count; i++)
        {
            if (chessViewList[i].GetChess.chessType == chess.chessType &&
                chessViewList[i].GetChess.x == chess.x && chessViewList[i].GetChess.y == chess.y)
            {
                Destroy(chessViewList[i].gameObject);
                chessViewList.RemoveAt(i);
                ChangeChessType(); 
            }
        }
    }
    private void RedoCallback(Chess chess)
    {
        //Debug.Log($"重做回调-----  {chess.ToString() }");
        CreateChessView(chess);
        ChangeChessType();
    }

    private void WinCallback(Chess chess)
    {
        Debug.Log($"胜利回调---------  {chess.chessType }");
        isGameOver = true;
        gameOverPanel.OpenPanel(chess.chessType);
        gameOverPanel.MoveToBottom();
    }
    #endregion 


    private void Start()
    {
        commandManager = new CommandManager();
        Init();
    }


    private void Update()
    {
        if (gameOverPanel.IsClickReset())
        {
            Init();
            chessManager.ResetGame();
        }
        if (isGameOver) return;

        if (Input.GetKeyDown("z"))
        {
            commandManager.UnDo();
        }
        if (Input.GetKeyDown("y"))
        {
            commandManager.ReDo();
        }

        if (Input.GetMouseButtonDown(0))
        {
            ChessGridView currentChessGridView = UnityUtility.UITool.GetMouseUIComponent<ChessGridView>(graphicRaycaster, eventSystem);
            if (currentChessGridView == null)
            {
                Debug.Log($"Player -> Update() currentChessGridView == null");
                return;
            }
            int x = currentChessGridView.PosX / 80;
            int y = currentChessGridView.PosY / 80;
            Chess currentChess = new Chess("ChessView", currenChessType, x, y);
            PlayChessCommand playChessCommand = new PlayChessCommand(chessManager, currentChess);
            commandManager.Execute(playChessCommand);
        }
    }

    private void Init()
    {
        isGameOver = false;
        currenChessType = ChessType.Black;
        if (chessViewList.Count > 0)
        {
            for (int i = 0; i < chessViewList.Count; i++)
            {
                Destroy(chessViewList[i].gameObject);
            }
            chessViewList.Clear();
        }
    }

    /// <summary>
    /// 根据Chess创建对应棋子物体
    /// </summary>
    void CreateChessView(Chess chess)
    {
        string path = chess.path;
        Transform parent = graphicRaycaster.transform;
        Vector3 anchoredPosition3D = new Vector3(chess.x * 80, chess.y * 80, 0);
        Vector2 sizeData = new Vector2(80, 80);
        ChessView chessView = FactorySystem.FactoryManager.Instance.GetUIPanelFactory.CreateUIPanel<ChessView>(path, parent, anchoredPosition3D, sizeData);
        chessView.SetChess(chess);

        chessViewList.Add(chessView);
    }

    void ChangeChessType()
    {
        switch (currenChessType)
        {
            case ChessType.Null:
                break;
            case ChessType.White:
                currenChessType = ChessType.Black;
                break;
            case ChessType.Black:
                currenChessType = ChessType.White;
                break;
            default:
                break;
        }
    }
}