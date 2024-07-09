using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    public float speed = 0.125f;
    public float YPosition;

    void FixedUpdate()
    {
        Vector3 dir = player.transform.position;
        dir.y -= YPosition;
        Vector3 desiredPosition = dir;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed);
        smoothedPosition.z = transform.position.z;
        transform.position = smoothedPosition;
    }
}
