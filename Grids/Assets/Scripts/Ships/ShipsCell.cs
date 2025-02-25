using UnityEngine;

public class ShipsCell : MonoBehaviour
{
    public bool busy = false;

    public void SetBusy(bool isBusy){
        busy=isBusy;
        if(isBusy){
            GetComponent<SpriteRenderer>().color = new Color(0.9f,0.9f,0.9f,1);
        }else{
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
        }
    }
    public bool IsBusy(){
        return busy;
    }
    public void MakeBusyNearCells(bool busy){
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach(Collider2D collider in hitColliders){
            if(collider.tag=="PlaceArea"){
                collider.GetComponent<ShipsCell>().SetBusy(busy);
            }
        }
    }
}
