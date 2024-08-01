using UnityEngine;

public class TouchEffect : MonoBehaviour
{
    public GameObject touchEffectPrefab;
    public Camera touchEffectCamera;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                SpawnTouchEffect(touch.position);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            SpawnTouchEffect(mousePosition);
        }
    }

    void SpawnTouchEffect(Vector3 position)
    {
        Vector3 worldPosition = touchEffectCamera.ScreenToWorldPoint(position);
        worldPosition.z = 0;
        Instantiate(touchEffectPrefab, worldPosition, Quaternion.identity, transform);
    }
}
