using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShakeAnimation : MonoBehaviour
{
    private bool shaking = false;
    [SerializeField]
    private float shakeAmount;

    private void Update(){
        if(shaking){
            UnityEngine.Vector3 newPos = Random.insideUnitSphere*(Time.deltaTime*shakeAmount);
            transform.position += newPos;
        }
    }
    public void ShakeMe(){
        StartCoroutine(Shake());
    }

    private IEnumerator Shake(){
        UnityEngine.Vector3 originalPosition = transform.position;
        if(shaking == false) shaking = true;
        yield return new WaitForSeconds(0.25f);
        shaking = false;
        transform.position = originalPosition;
    }
}
