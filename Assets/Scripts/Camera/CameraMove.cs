using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    public float speed = 0.125f;
    public float YPosition;
    public float[] distances;
    public Button CameraButton;

    private int currentDistanceIndex = 0;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        CameraButton.onClick.AddListener(ChangeCameraDistance);
        if (distances.Length > 0)
        {
            cam.orthographicSize = distances[currentDistanceIndex];
        }
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }
    public void ChangeCameraDistance()
    {
        currentDistanceIndex = (currentDistanceIndex + 1) % distances.Length;
        cam.orthographicSize = distances[currentDistanceIndex];
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector3 dir = player.transform.position;
        dir.y -= YPosition;
        Vector3 desiredPosition = dir;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed);
        smoothedPosition.z = transform.position.z;
        transform.position = smoothedPosition;
    }
}
