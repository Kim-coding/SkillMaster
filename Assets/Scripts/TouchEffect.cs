using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffect : MonoBehaviour
{
    public GameObject particlePrefab;
    public Camera mainCamera;
    //public Camera particleCamera;
    public float minTouchMovement = 10f;

    private Vector3 lastTouchPosition;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        //if (particleCamera == null)
        //{
        //    Debug.LogError("Particle Camera is not assigned.");
        //}
    }

    void Update()
    {
        ProcessTouch();
    }

    private void ProcessTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, mainCamera.nearClipPlane));
            touchPosition.z = 0;

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = touchPosition;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (Vector3.Distance(touchPosition, lastTouchPosition) >= minTouchMovement)
                {
                    SpawnTouchEffect(touchPosition);
                    lastTouchPosition = touchPosition;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            lastTouchPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane));
            lastTouchPosition.z = 0;
            SpawnTouchEffect(lastTouchPosition);
        }
    }

    void SpawnTouchEffect(Vector3 position)
    {
        GameObject particleEffect = Instantiate(particlePrefab, position, Quaternion.identity);
        ParticleSystem ps = particleEffect.GetComponent<ParticleSystem>();

        if (ps != null)
        {
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            ps.Play();
            Destroy(particleEffect, ps.main.duration + ps.main.startLifetime.constantMax);
        }
        else
        {
            Destroy(particleEffect, 1f);
        }
    }
}
