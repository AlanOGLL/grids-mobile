using UnityEngine;

public class GridCellPosition : MonoBehaviour
{
    [SerializeField] private int x;
    [SerializeField] private int y;

    public void OnMouseDown()
    {
        TicTacManager.Instance.ClickedOnGridPosition(x,y);
    }
}
