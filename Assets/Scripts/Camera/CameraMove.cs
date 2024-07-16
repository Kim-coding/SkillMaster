using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System.ComponentModel;

public class CameraMove : MonoBehaviour
{

    public float moveDuration = 0.5f; // 카메라 이동 시간
    public float zoomDuration = 0.5f; // 카메라 줌 시간
    public float YPosition;
    public float[] distances;
    public Button CameraButton;

    private int currentDistanceIndex = 0;
    public Button toggleWindowButton;
    [HideInInspector]
    public bool isToggle = true;

    public CinemachineVirtualCamera centerCameraNear;
    public CinemachineVirtualCamera centerCameraMiddle;
    public CinemachineVirtualCamera centerCameraFar;
    public CinemachineVirtualCamera topCameraNear;
    public CinemachineVirtualCamera topCameraMiddle;
    public CinemachineVirtualCamera topCameraFar;


    private void Start()
    {
        CameraButton.onClick.AddListener(ChangeCameraDistance);
        toggleWindowButton.onClick.AddListener(() => { isToggle = !isToggle; ChangeCameraDistance(); });
        ChangeCameraDistance();
    }

    public void ChangeCameraDistance()
    {
        currentDistanceIndex = (currentDistanceIndex + 1) % distances.Length;

        centerCameraNear.gameObject.SetActive(false);
        centerCameraMiddle.gameObject.SetActive(false);
        centerCameraFar.gameObject.SetActive(false);
        topCameraNear.gameObject.SetActive(false);
        topCameraMiddle.gameObject.SetActive(false);
        topCameraFar.gameObject.SetActive(false);

        if (!isToggle)
        {
            switch (currentDistanceIndex)
            {
                case 0:
                    centerCameraNear.gameObject.SetActive(true);
                    break;
                case 1:
                    centerCameraMiddle.gameObject.SetActive(true);
                    break;
                case 2:
                    centerCameraFar.gameObject.SetActive(true);
                    break;
            }
        }
        else
        {
            switch (currentDistanceIndex)
            {
                case 0:
                    topCameraNear.gameObject.SetActive(true);
                    break;
                case 1:
                    topCameraMiddle.gameObject.SetActive(true);
                    break;
                case 2:
                    topCameraFar.gameObject.SetActive(true);
                    break;
            }
        }
    }
}
