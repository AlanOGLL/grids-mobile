using UnityEngine;
using System.Collections;

public class GrayOutScript : MonoBehaviour
{
    [SerializeField] float grayOutSpeed = 2f;

    SpriteRenderer spriteRenderer;
    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(GrayOut());
    }
    private IEnumerator GrayOut(){
        float tick = 0f;
        while(spriteRenderer.color.a != 0.5){
            tick += Time.deltaTime*grayOutSpeed;
            spriteRenderer.color = Color.Lerp(new Color(0,0,0,0), new Color(0,0,0,0.5f), tick);
            yield return null;
        }
    }
}
