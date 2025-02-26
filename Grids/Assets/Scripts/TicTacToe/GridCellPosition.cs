using UnityEngine;

public class GridCellPosition : MonoBehaviour
{
    [SerializeField] private int x;
    [SerializeField] private int y;

    public void OnMouseDown()
    {
        Debug.Log("GridCellPosition, OnMouseDown");
        TicTacManager.Instance.ClickedOnGridPositionRpc(x,y, TicTacManager.Instance.GetLocalPlayerType());
    }
}
