using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class HangmanManager : MonoBehaviour
{
    private List<String> expressions = new List<String>();
    [SerializeField] TMP_Text characterObject;
    private String currentExpression;
    void Start(){
        AddAllExpressions();
        DrawExpression();
    }
    private void AddAllExpressions(){
        expressions.Add("Kostka rubika");
        expressions.Add("Cicha woda");
        expressions.Add("Szybki bieg");
        expressions.Add("Tajemny szyfr");
        expressions.Add("Smocza jaskinia");
        expressions.Add("Morska fala");
        expressions.Add("Ukryty skarb");
        expressions.Add("Lodowy szczyt");
        expressions.Add("Czarna owca");
        expressions.Add("Rycerski miecz");
        expressions.Add("Taniec cieni");
        expressions.Add("Szybowiec");
        expressions.Add("Rakieta");
        expressions.Add("Komputer");
        expressions.Add("Panorama");
        expressions.Add("Tkanina");
        expressions.Add("Szarlotka");
        expressions.Add("Turysta");
    }

    private void DrawExpression(){
        Random random = new Random();
        currentExpression = expressions[random.Next(0,expressions.Count-1)].ToUpper();
        Debug.Log(currentExpression);
        String rawText = "";
        foreach(char character in currentExpression){
            if(character==' ')rawText+=character;
            else rawText+='_';
        }
        characterObject.text = rawText;
    }
    public void ClickedCharacter(char clickedCharacter){
        while(currentExpression.Contains(clickedCharacter)){
            int indexToChange = currentExpression.IndexOf(clickedCharacter);
            char[] charTab = currentExpression.ToCharArray();
            charTab[indexToChange] = '_';
            currentExpression = new string(charTab);
            
            char[] charTab2 = characterObject.text.ToCharArray();
            charTab2[indexToChange] = clickedCharacter;
            characterObject.text = new string(charTab2);
        }
    }
}
