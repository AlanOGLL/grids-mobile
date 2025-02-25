using UnityEngine;
using System.Collections;

public class LineAnimation : MonoBehaviour
{

    private LineRenderer lineRenderer;
    
    private void Start(){
        lineRenderer = GetComponent<LineRenderer>();
    }
    public void AnimateLine(){
        lineRenderer.enabled = true;
        StartCoroutine(Animate());
    }
    private IEnumerator Animate(){
        Vector3 endPos = lineRenderer.GetPosition(0);
        Vector3 startPos = new Vector3(0,0,0);
        Vector3 pos = startPos;
        Vector3 endPos2 = lineRenderer.GetPosition(1);
        Vector3 startPos2 = new Vector3(0,0,0);
        Vector3 pos2 = startPos2;
        float startTime = Time.time;
        while(pos != endPos){
            float time = (Time.time - startTime) / 0.5f;
            pos = Vector3.Lerp(startPos, endPos, time);
            pos2 = Vector3.Lerp(startPos2, endPos2, time);
            lineRenderer.SetPosition(0, pos);
            lineRenderer.SetPosition(1, pos2);
            yield return null;
        }
    }
}
