using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    public float moveDuration = 0.5f; // 카메라 이동 시간
    public float zoomDuration = 0.5f; // 카메라 줌 시간
    public float YPosition;
    public float[] distances;
    public Button CameraButton;

    private int currentDistanceIndex = 0;
    private Camera cam;
    private float initialOrthographicSize;
    private Vector3 initialPosition;

    private void Start()
    {
        cam = GetComponent<Camera>();
        CameraButton.onClick.AddListener(ChangeCameraDistance);
        if (distances.Length > 0)
        {
            cam.orthographicSize = distances[currentDistanceIndex];
            initialOrthographicSize = cam.orthographicSize;
            initialPosition = transform.position;
        }
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }

    public void ChangeCameraDistance()
    {
        currentDistanceIndex = (currentDistanceIndex + 1) % distances.Length;
        float targetOrthographicSize = distances[currentDistanceIndex];

        cam.DOOrthoSize(targetOrthographicSize, zoomDuration);
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector3 dir = player.transform.position;
        dir.y -= YPosition * (cam.orthographicSize / initialOrthographicSize);
        Vector3 desiredPosition = dir;
        desiredPosition.z = initialPosition.z;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, moveDuration);
    }
    private Vector3 GetTargetPosition(float targetOrthographicSize)
    {
        Vector3 dir = player.transform.position;
        dir.y -= YPosition * (targetOrthographicSize / initialOrthographicSize);
        return new Vector3(dir.x, dir.y, initialPosition.z);
    }
}
