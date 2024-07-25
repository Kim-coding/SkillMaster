using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPopup : MonoBehaviour
{

    float timer = 0f;
    public float duration = 1f;
    private void OnEnable()
    {
        timer = 0f;
    }
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > duration)
        {
            gameObject.SetActive(false);
            
        }
    }
}
