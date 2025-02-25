using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string SceneName;

    public void SwitchScene(string name){
        SceneManager.LoadScene(name);
    }

    public void OnPointerClick(PointerEventData eventData){
        SwitchScene(SceneName);
    }
}
