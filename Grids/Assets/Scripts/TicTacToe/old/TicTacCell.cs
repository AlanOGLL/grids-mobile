using UnityEngine;

public class TicTacCell : MonoBehaviour
{

    [SerializeField] Sprite xSprite;
    [SerializeField] Sprite oSprite;
    private int movesToDelete = 0;
    private bool isX = false;
    private bool isEmpty = true;
    private TicTacToeManager tictacManager;
    private SpriteRenderer spriteRenderer;

    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        tictacManager = GetComponentInParent<TicTacToeManager>();
        isEmpty = true;
    }

    public void Clear(){
        isEmpty = true;
        spriteRenderer.color = new Color(1,1,1,0);
    }
    public void SetWarningColor(){
        spriteRenderer.color = new Color(1,0,0,1);
    }
    public void OnMouseDown(){
        if(tictacManager.IsGameEnded()){
            return;
        }
        if(!isEmpty){
            GetComponent<ShakeAnimation>().ShakeMe();
            return;
        }
        if(tictacManager.IsXTurn){
            spriteRenderer.sprite = xSprite;
            tictacManager.IsXTurn = false;
            isX = true;
        } 
        else{
            spriteRenderer.sprite = oSprite;
            tictacManager.IsXTurn = true;
            isX = false;
        }
        isEmpty = false;
        spriteRenderer.color = new Color(0,0,0,1);
        tictacManager.IncreaseMove(this);
        tictacManager.ResetCounting();
        tictacManager.CheckBoard();
    }
    public int GetMovesToDelete(){
        return movesToDelete;
    }
    public void SetMovesToDelete(int moves){
        movesToDelete = moves;
    }
    public bool IsX(){
        return isX;
    }
    public bool IsEmpty(){
        return isEmpty;
    }
}
