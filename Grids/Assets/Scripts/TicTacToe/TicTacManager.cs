using System;
using Unity.Mathematics;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class TicTacManager : NetworkBehaviour
{
    public static TicTacManager Instance {get; private set;}

    public event EventHandler<OnClickedOnGridPositionEventArgs> OnClickedOnGridPosition;
    public class OnClickedOnGridPositionEventArgs : EventArgs{
        public int x;
        public int y;
        public PlayerType playerType;
    }
    public event EventHandler OnGameStarted;
    public event EventHandler OnCurrentMovingPlayerChanged;

    public enum PlayerType{
        None,
        Cross,
        Circle
    }

    private PlayerType localPlayerType;
    private PlayerType currentMovingPlayer;
    

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    public override void OnNetworkSpawn(){
        if(NetworkManager.Singleton.LocalClientId == 0){
            localPlayerType = PlayerType.Cross;
        }else{
            localPlayerType = PlayerType.Circle;
        }
        if(IsServer){
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
        }
    }

    private void OnClientConnectedCallback(ulong obj)
    {
        if(NetworkManager.Singleton.ConnectedClientsList.Count == 2){
            currentMovingPlayer = PlayerType.Cross;
            OnGameStarted?.Invoke(this, EventArgs.Empty);
        }
    }

    public PlayerType GetLocalPlayerType(){
        return localPlayerType;
    }
    public PlayerType GetCurrentMovingPlayer(){
        return currentMovingPlayer;
    }
    [Rpc(SendTo.Server)]
    public void ClickedOnGridPositionRpc(int x, int y, PlayerType playerType){
        Debug.Log("TicTacManager, ClickedOnGridPositionRpc");
        if(playerType != currentMovingPlayer){
            return;
        }
        if(playerType == PlayerType.Circle)currentMovingPlayer = PlayerType.Cross;
        else currentMovingPlayer = PlayerType.Circle;
        OnClickedOnGridPosition?.Invoke(this, new OnClickedOnGridPositionEventArgs{
            x = x,
            y = y,
            playerType = playerType
        });
        OnCurrentMovingPlayerChanged?.Invoke(this, EventArgs.Empty);
    }
    
}
