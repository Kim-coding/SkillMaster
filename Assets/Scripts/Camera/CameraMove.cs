using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System.ComponentModel;
using Unity.VisualScripting;

public class CameraMove : MonoBehaviour
{

    public GameObject player;

    public float moveDuration = 0.5f; // 카메라 이동 시간
    public float zoomDuration = 0.5f; // 카메라 줌 시간
    public float YPosition;
    public float[] distances;
    public Button CameraButton;

    private int currentDistanceIndex = 0;
    [HideInInspector]
    public bool isToggle = true;

    public CinemachineVirtualCamera centerCameraNear;
    public CinemachineVirtualCamera centerCameraMiddle;
    public CinemachineVirtualCamera centerCameraFar;
    public CinemachineVirtualCamera topCameraNear;
    public CinemachineVirtualCamera topCameraMiddle;
    public CinemachineVirtualCamera topCameraFar;

    bool onBossView = false;
    float bossViewTimer = 0f;
    float bossViewDuration = 2f;

    private void Start()
    {
        CameraButton.onClick.AddListener(ChangeCameraDistance);
        CurrentCameraView();
        SetTarget(player);
    }

    private void Update()
    {
        if (onBossView)
        {
            bossViewTimer += Time.deltaTime;
        }

        if(bossViewTimer > bossViewDuration)
        {
            bossViewTimer = 0;
            onBossView = false;
            SetTarget();
        }
    }

    public void SetTarget()
    {
        centerCameraNear.Follow = player.transform;
        centerCameraMiddle.Follow = player.transform;
        centerCameraFar.Follow = player.transform;
        topCameraNear.Follow = player.transform;
        topCameraMiddle.Follow = player.transform;
        topCameraFar.Follow = player.transform;
    }
    public void SetTarget(GameObject target)
    {
        onBossView = true;
        centerCameraNear.Follow = target.transform;
        centerCameraMiddle.Follow = target.transform;
        centerCameraFar.Follow = target.transform;
        topCameraNear.Follow = target.transform;
        topCameraMiddle.Follow = target.transform;
        topCameraFar.Follow = target.transform;
    }

    public void ChangeCameraDistance()
    {
        currentDistanceIndex = (currentDistanceIndex + 1) % distances.Length;
        CurrentCameraView();
    }

    public void CurrentCameraView()
    {
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
