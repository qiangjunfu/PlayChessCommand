using GobangSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Player3 : MonoBehaviour
{
    [SerializeField] private GobangManager gobangManager;
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
    [Header("所有棋子V层列表")] [SerializeField] private List<ChessView> allChessViewList = new List<ChessView>();



    #region 回调
    private void OnEnable()
    {
        gobangManager = new GobangManager();
        gobangManager.mPlayChessEvent += PlayChessCallback;
        gobangManager.mUndoChessEvent += UndoCallback;
        gobangManager.mRedoChessEvent += RedoCallback;
        gobangManager.mWinEvent += WinCallback;
    }
    private void OnDisable()
    {
        gobangManager.mPlayChessEvent -= PlayChessCallback;
        gobangManager.mUndoChessEvent -= UndoCallback;
        gobangManager.mRedoChessEvent -= RedoCallback;
        gobangManager.mWinEvent -= WinCallback;
    }
    private void PlayChessCallback(Chess chess)
    {
        Debug.Log($"下棋回调-----  {chess.ToString() }");
        //  ChessItem chessItem = FactorySystem.FactoryManager.Instance.GetUIPanelFactory.CreateUIPanel<ChessItem>("ChessItem", this.transform, new Vector3(chessGrid.PosX, chessGrid.PosY, 0), new Vector2(80, 80));
        ChessView chessView = FactorySystem.FactoryManager.Instance.GetUIPanelFactory.CreateUIPanel<ChessView>("ChessView",
            graphicRaycaster.transform, new Vector3(chess.x * 80, chess.y * 80, 0), new Vector2(80, 80));
        chessView.SetChess(chess);

        allChessViewList.Add(chessView);
    }

    private void UndoCallback(Chess chess)
    {
        Debug.Log($"悔棋回调-----  {chess.ToString() }");
        for (int i = 0; i < allChessViewList.Count; i++)
        {
            if (allChessViewList[i].GetChess.chessType == chess.chessType &&
                allChessViewList[i].GetChess.x == chess.x && allChessViewList[i].GetChess.y == chess.y)
            {
                Destroy(allChessViewList[i].gameObject);
                allChessViewList.RemoveAt(i);
            }
        }
    }
    private void RedoCallback(Chess chess)
    {
        Debug.Log($"重做回调-----  {chess.ToString() }");
        ChessView chessView = FactorySystem.FactoryManager.Instance.GetUIPanelFactory.CreateUIPanel<ChessView>("ChessView",
          graphicRaycaster.transform, new Vector3(chess.x * 80, chess.y * 80, 0), new Vector2(80, 80));
        chessView.SetChess(chess);
        allChessViewList.Add(chessView);
    }

    private void WinCallback(Chess chess)
    {
        Debug.Log($"胜利回调---------  {chess.chessType.ToString() }");
        isGameOver = true;
        gameOverPanel.OpenPanel(chess.chessType);
        gameOverPanel.MoveToBottom();
    }
    #endregion 


    private void Start()
    {
        Init();
    }


    private void Update()
    {
        if (gameOverPanel.IsClickReset())
        {
            Init();
            gobangManager.ResetGame();
        }
        if (isGameOver) return;

        if (Input.GetKeyDown("z"))
        {
            gobangManager.UndoChess();
        }
        if (Input.GetKeyDown("y"))
        {
            gobangManager.RedoChess();
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
            gobangManager.PlayChess(currentChess);
        }
    }

    private void Init()
    {
        isGameOver = false;
        currenChessType = ChessType.Black;
        if (allChessViewList.Count > 0)
        {
            for (int i = 0; i < allChessViewList.Count; i++)
            {
                Destroy(allChessViewList[i].gameObject);
            }
            allChessViewList.Clear();
        }
    }


}