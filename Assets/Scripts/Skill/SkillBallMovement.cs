using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBallMovement : MonoBehaviour
{
    public void Move(Vector3 startPosition, Vector3 endPosition, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
        }
        transform.position = endPosition;
    }
}
