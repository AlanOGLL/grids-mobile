using Unity.VisualScripting;
using UnityEngine;

public class KeyboardCharacter : MonoBehaviour
{
    [SerializeField]private LayerMask layerMask;
    [SerializeField]private HangmanManager gameManager;

    void Update(){
        RayOnMouseDown();
    }
    void RayOnMouseDown(){
        if(Input.GetMouseButtonDown(0)){
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
            if(hit.collider!=null){
                if(hit.collider.transform == transform){
                    gameManager.ClickedCharacter(transform.name[0]);
                    transform.gameObject.SetActive(false);
                }
            }
        }
    }
}
