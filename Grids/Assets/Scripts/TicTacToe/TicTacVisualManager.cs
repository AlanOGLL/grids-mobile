using Unity.Netcode;
using UnityEngine;

public class TicTacVisualManager : NetworkBehaviour
{
    private static float CELL_SPACE = 2.0f;
    [SerializeField] private Transform cross;
    [SerializeField] private Transform circle;

    private void Start()
    {
        TicTacManager.Instance.OnClickedOnGridPosition += ShowMovement;
    }
    public void ShowMovement(object sender, TicTacManager.OnClickedOnGridPositionEventArgs e){
        SpawnObjectRpc(e.x, e.y, TicTacManager.Instance.GetLocalPlayerType());
    }
    [Rpc(SendTo.Server)]
    private void SpawnObjectRpc(int x, int y, TicTacManager.PlayerType playerType){
        Transform spawned = Instantiate(playerType==TicTacManager.PlayerType.Cross?cross:circle, new Vector2(-CELL_SPACE+x*CELL_SPACE, -CELL_SPACE+y*CELL_SPACE), Quaternion.identity);
        spawned.GetComponent<NetworkObject>().Spawn(true);
    }
}
