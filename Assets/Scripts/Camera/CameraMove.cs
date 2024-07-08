using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject player;
    public float speed = 0.125f;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.transform.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed);
        smoothedPosition.z = transform.position.z;
        transform.position = smoothedPosition;
    }
}
