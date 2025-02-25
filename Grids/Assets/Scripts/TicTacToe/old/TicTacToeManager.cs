using System;
using UnityEngine;


public class TicTacToeManager : MonoBehaviour, IMinigameManager
{
    TicTacCell[,] cells = new TicTacCell[3,3];
    public bool IsXTurn = true;
    private bool isGameEnded = false;
    private int move = 0;
    [SerializeField] ProgressBar countBar;
    [SerializeField] GameObject noTimePopup;
    void Start()
    {
        AdjustBoardSize();
        TicTacCell[] ttc = GetComponentsInChildren<TicTacCell>();
        int row = 0;
        int column = 0;
        foreach(TicTacCell tt in ttc){
            cells[row,column] = tt;
            if(column==2){
                column=0;
                row++;
            }
            else column++;
        }
    }

    private void AdjustBoardSize(){
        float screenRatio = (float) Screen.width / Screen.height;
        float targetSize = 3f;
        Camera.main.orthographicSize = targetSize / screenRatio;
    }
    public void CheckBoard()
    {
        CheckCellsToDelete();
        int rowResult = CheckRows();
        int colResult = CheckColumns();
        int diagResult = CheckDiagonal();
        if(rowResult!=-1){
            GameObject.Find("LineR"+rowResult).GetComponent<LineAnimation>().AnimateLine();
            countBar.StopProgressBar();
            isGameEnded = true;
        }
        else if(colResult != -1){
            GameObject.Find("LineC"+colResult).GetComponent<LineAnimation>().AnimateLine();
            countBar.StopProgressBar();
            isGameEnded = true;
        }
        else if(diagResult != -1){
            GameObject.Find("LineD"+diagResult).GetComponent<LineAnimation>().AnimateLine();
            countBar.StopProgressBar();
            isGameEnded = true;
        }
    }
    private void CheckCellsToDelete(){
        foreach(TicTacCell ttc in cells){
            if(ttc.GetMovesToDelete()==move+2 && !ttc.IsEmpty()){
                ttc.SetWarningColor();
            }
            else if(ttc.GetMovesToDelete()<=move && !ttc.IsEmpty()){
                ttc.Clear();
            }
        }
    }
    private int CheckRows(){
        for(int i = 0; i<3; i++){
            if(!cells[i, 0].IsEmpty() && !cells[i, 1].IsEmpty() && !cells[i, 2].IsEmpty()){
                if(cells[i, 0].IsX() == cells[i, 1].IsX() && cells[i, 1].IsX() == cells[i, 2].IsX()){
                    return i;
                }
            }
        }
        return -1;
    }
    private int CheckColumns(){
        for(int i = 0; i<3; i++){
            if(!cells[0, i].IsEmpty() && !cells[1, i].IsEmpty() && !cells[2, i].IsEmpty()){
                if(cells[0, i].IsX() == cells[1, i].IsX() && cells[1, i].IsX() == cells[2, i].IsX()){
                    return i;
                }
            }
        }
        return -1;
    }
    private int CheckDiagonal(){
        if(!cells[0, 0].IsEmpty() && !cells[1, 1].IsEmpty() && !cells[2, 2].IsEmpty()){
            if(cells[0, 0].IsX() == cells[1, 1].IsX() && cells[1, 1].IsX() == cells[2, 2].IsX()){
                return 2;
            }
        }
        if(!cells[0, 2].IsEmpty() && !cells[1, 1].IsEmpty() && !cells[2, 0].IsEmpty()){
            if(cells[0, 2].IsX() == cells[1, 1].IsX() && cells[1, 1].IsX() == cells[2, 0].IsX()){
                return 1;
            }
        }
        return -1;
    }
    public void ResetCounting(){
        countBar.CountDownBar(4);
    }
    public void EndOfTime(){
        noTimePopup.SetActive(true);
    }
    public bool IsGameEnded(){
        return isGameEnded;
    }
    public void IncreaseMove(TicTacCell clickedCell){
        move++;
        clickedCell.SetMovesToDelete(move+6);
    }
}
