using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipDragger : MonoBehaviour
{
    private Transform transform;
    private Collider2D collider;
    public string destinationTag = "PlaceArea";
    [SerializeField]private int shipSize;
    [SerializeField]private bool isRotated = false;
    [SerializeField]private GameObject rotatedObject;
    private List<GameObject> activeCells = new List<GameObject>();
    private Vector3 startPosition;
    private bool isDragging = false;
    private LayerMask layerMask;
    void Start()
    {
        transform = GetComponent<Transform>();
        collider = GetComponent<Collider2D>();
        startPosition = transform.position;
        layerMask = LayerMask.GetMask("DraggableLayer");
    }
    void Update(){
        RayOnMouseDown();
        RayOnMouseDrag();
        RayOnMouseUp();
    }
    Vector3 MouseWorldPosition(){
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    /*void OnMouseUp(){
        if(activeCells.Count==shipSize){
            AlignShipToCells();
        }else{
            CancelDragging();
        }
    }*/
    void RayOnMouseUp(){
        if(Input.GetMouseButtonUp(0)){
            if(isDragging){
                if(activeCells.Count==shipSize && AllCellsInOneRow()){
                    AlignShipToCells();
                }else{
                    CancelDragging();
                }
                isDragging = false;
            }
        }
    }
    private bool AllCellsInOneRow(){
        if(activeCells.Count<=1)return true;
        float x = activeCells[0].transform.position.x;
        float y = activeCells[0].transform.position.y;
        for(int index = 1; index<activeCells.Count; index++){
            if(activeCells[index].transform.position.x != x && activeCells[index].transform.position.y != y){
                return false;
            }
        }
        return true;
    }
    private void CancelDragging(){
        rotatedObject.SetActive(true);
        transform.position = startPosition;
        activeCells.Clear();
    }
    private void AlignShipToCells(){
        Vector3 newPosition;
        if(!isRotated){
            newPosition = getBottomGameObject().transform.position;
            newPosition.y-=0.15f;
        }else{
            newPosition = getLeftGameObject().transform.position;
            newPosition.x+=0.15f;
        }
        transform.position = newPosition;
        if(isAnyCellBusy()){
            CancelDragging();
        }else{
            MakeCellsBusy();
        }
    }
    private void MakeCellsBusy(){
        foreach(GameObject activeCellGameObject in activeCells){
            activeCellGameObject.GetComponent<ShipsCell>().SetBusy(true);
            activeCellGameObject.GetComponent<ShipsCell>().MakeBusyNearCells(true);
        }
    }
    private bool isAnyCellBusy(){
        foreach(GameObject go in activeCells){
            if(go.GetComponent<ShipsCell>().IsBusy()){
                return true;
            }
        }
        return false;
    }
    private GameObject getBottomGameObject(){
        GameObject toReturn = activeCells[0];
        foreach(GameObject go in activeCells){
            if(toReturn.transform.position.y>go.transform.position.y) toReturn = go;
        }  
        return toReturn;
    }
    private GameObject getLeftGameObject(){
        GameObject toReturn = activeCells[0];
        foreach(GameObject go in activeCells){
            if(toReturn.transform.position.x<go.transform.position.x) toReturn = go;
        }  
        return toReturn;
    }
    /*void OnMouseDown(){
        Debug.Log("Works");
        rotatedObject.SetActive(false);
        MakeCellsNotBusy();
        //activeCells.Clear();
    }*/
    void RayOnMouseDown(){
        if(Input.GetMouseButtonDown(0)){
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
            if(hit.collider!=null){
                if(hit.collider.transform == transform){
                    rotatedObject.SetActive(false);
                    MakeCellsNotBusy();
                    isDragging = true;
                }
            }
            
            
        }
    }
    void RayOnMouseDrag(){
        if(isDragging){
            Vector3 newPos = MouseWorldPosition();
            if(isRotated)newPos.x += 0.3f;
            else newPos.y -= 0.3f;
            transform.position = newPos;
        }else{
            MakeCellsBusy();
        }
    }
    private void MakeCellsNotBusy(){
        foreach(GameObject activeCellGameObject in activeCells){
            activeCellGameObject.GetComponent<ShipsCell>().SetBusy(false);
            activeCellGameObject.GetComponent<ShipsCell>().MakeBusyNearCells(false);
        }
    }
    /*void OnMouseDrag(){
        transform.position = MouseWorldPosition();
    }*/
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == destinationTag){
            ShipsCell cell = collision.gameObject.GetComponent<ShipsCell>();
            if(!cell.IsBusy()){
                activeCells.Add(collision.gameObject);
                collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.8f,0.8f,0.8f,1);
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.tag == destinationTag){
            ShipsCell cell = collision.gameObject.GetComponent<ShipsCell>();
            if(!cell.IsBusy()){
                activeCells.Remove(collision.gameObject);
                collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
            }
        }
    }
    

}