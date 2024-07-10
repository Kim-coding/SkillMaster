using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasOrder : MonoBehaviour
{
    private void Start()
    {
        Canvas rd = GetComponentInParent<Canvas>();
        rd.sortingLayerName= "Defalut";
        rd.sortingOrder = 20;
    }
}
