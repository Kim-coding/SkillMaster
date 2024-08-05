using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBallMovement : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float duration;
    private float elapsed;

    private bool isMoving = false;
    private SkillBallController skillBallController;

    private void Start()
    {
        skillBallController = GetComponent<SkillBallController>();
    }

    public void Move(Vector3 startPosition, Vector3 endPosition, float duration)
    {
        this.startPosition = startPosition;
        this.endPosition = endPosition;
        this.duration = duration;
        elapsed = 0f;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            elapsed += Time.deltaTime;
            if (elapsed < duration)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
            }
            else
            {
                transform.position = endPosition;
                isMoving = false;
                if (skillBallController != null)
                {
                    skillBallController.AutoMerge();
                }
            }
        }
    }
}
