using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ProgressBar : MonoBehaviour
{

    [SerializeField] private int changeToRedLeftSeconds = 2;
    
    [SerializeField] private UnityEngine.Object minigameManager;

    private IMinigameManager minigameObject;
    private GameObject line;
    private Coroutine coroutine;
    
    private void Awake(){
        line = GameObject.Find("ProgressLine");
        coroutine = StartCoroutine(CountDown(5));
        minigameObject = (IMinigameManager)minigameManager.GetComponent<IMinigameManager>();
    }
    public void CountDownBar(int seconds){
        StopProgressBar();
        coroutine = StartCoroutine(CountDown(seconds));
    }
    private IEnumerator CountDown(int seconds){
        line.GetComponent<Transform>().localScale = new Vector3(0, 1, 1);

        float startTime = Time.time;
        float currTime = startTime;
        float endTime = startTime+seconds;
        while(currTime <= endTime){
            if(seconds-(currTime-startTime)<=changeToRedLeftSeconds){
                line.GetComponent<UnityEngine.UI.Image>().color = new Color(0.9f,0.3f,0.2f,1);
            }else{
                line.GetComponent<UnityEngine.UI.Image>().color = new Color(1,1,1,1);
            }
            line.GetComponent<Transform>().localScale = new Vector3((currTime-startTime)/seconds, 1, 1);
            currTime = Time.time;
            yield return null;
        }
        if(currTime >= endTime){
            minigameObject.EndOfTime();
        }
    }
    public void StopProgressBar(){
        StopCoroutine(coroutine);
    }
    
}
