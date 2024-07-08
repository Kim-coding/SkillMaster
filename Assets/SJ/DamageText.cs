using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float timer = 0f;
    private float duration = 1f;

    void Update()
    {
        timer += Time.deltaTime;
        var pos = transform.position;
        pos.y += 0.01f;
        transform.position = pos;

        if (timer > duration)
        {
            Destroy(gameObject);
        }
    }
}
