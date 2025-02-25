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
    }

    public enum PlayerType{
        None,
        Cross,
        Circle
    }

    private PlayerType localPlayerType;

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
    }
    public PlayerType GetLocalPlayerType(){
        return localPlayerType;
    }
    public void ClickedOnGridPosition(int x, int y){
        OnClickedOnGridPosition?.Invoke(this, new OnClickedOnGridPositionEventArgs{
            x = x,
            y = y
        });
    }
    
}
