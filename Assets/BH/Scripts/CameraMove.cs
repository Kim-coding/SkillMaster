using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    public float speed = 0.125f;

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed);
        smoothedPosition.z = transform.position.z;
        transform.position = smoothedPosition;
    }
}
