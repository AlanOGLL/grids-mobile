using System;
using Unity.VisualScripting;
using UnityEngine;

public class TicTacToeUI : MonoBehaviour
{
    [SerializeField] private GameObject yourTurnText;

    private void Awake()
    {
        yourTurnText.SetActive(false);
    }

    private void Start()
    {
        TicTacManager.Instance.OnGameStarted += TicTacManager_OnGameStarted;
        TicTacManager.Instance.OnCurrentMovingPlayerChanged += TicTacManager_CurrentMovingPlayerChanged;
    }
    private void TicTacManager_OnGameStarted(object sender, EventArgs e)
    {
        UpdateYourTurnText();
    }
    private void TicTacManager_CurrentMovingPlayerChanged(object sender, EventArgs e)
    {
        UpdateYourTurnText();
    }
    private void UpdateYourTurnText(){
        if(TicTacManager.Instance.GetCurrentMovingPlayer()==TicTacManager.Instance.GetLocalPlayerType()){
            yourTurnText.SetActive(true);
        }else{
            yourTurnText.SetActive(false);
        }
    }

}
