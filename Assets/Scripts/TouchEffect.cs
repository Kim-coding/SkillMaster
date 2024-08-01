using UnityEngine;
using Coffee.UIExtensions;

public class TouchEffect : MonoBehaviour
{
    public GameObject particlePrefab;
    public Canvas uiCanvas;
    public float minTouchMovement = 10f;

    private Vector3 lastTouchPosition;

    void Start()
    {
        if (uiCanvas == null)
        {
            uiCanvas = FindObjectOfType<Canvas>();
            if (uiCanvas == null)
            {
                Debug.LogError("UI Canvas not found!");
                return;
            }
        }

        if (uiCanvas.renderMode == RenderMode.ScreenSpaceCamera && uiCanvas.worldCamera == null)
        {
            uiCanvas.worldCamera = Camera.main;
            if (uiCanvas.worldCamera == null)
            {
                Debug.LogError("World Camera not set on UI Canvas and no main camera found!");
                return;
            }
        }
    }

    void Update()
    {
        if (uiCanvas == null)
        {
            return;
        }

        ProcessTouch();
    }

    private void ProcessTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition;

            if (uiCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                touchPosition = new Vector3(touch.position.x, touch.position.y, 0);
            }
            else
            {
                touchPosition = uiCanvas.worldCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, uiCanvas.worldCamera.nearClipPlane));
                touchPosition.z = 0;
            }

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
            Vector3 mousePosition;

            if (uiCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            }
            else
            {
                mousePosition = uiCanvas.worldCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, uiCanvas.worldCamera.nearClipPlane));
                mousePosition.z = 0;
            }

            lastTouchPosition = mousePosition;
            SpawnTouchEffect(lastTouchPosition);
        }
    }

    void SpawnTouchEffect(Vector3 position)
    {
        GameObject particleEffect = Instantiate(particlePrefab, position, Quaternion.identity, uiCanvas.transform);
        UIParticle uiParticle = particleEffect.GetComponent<UIParticle>();
        if (uiParticle != null)
        {
            uiParticle.Play();
            ParticleSystem particleSystem = particleEffect.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                Destroy(particleEffect, particleSystem.main.duration + particleSystem.main.startLifetime.constantMax);
            }
            else
            {
                Destroy(particleEffect, 1f);
            }
        }
        else
        {
            Destroy(particleEffect, 1f);
        }
    }
}
