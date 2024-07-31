using UnityEngine;

public class TouchEffect : MonoBehaviour
{
    public GameObject touchEffectPrefab;

    void Update()
    {
        // 모바일 터치 입력 처리
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                SpawnTouchEffect(touch.position);
            }
        }

        // 마우스 입력 처리 (디버그용)
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            SpawnTouchEffect(mousePosition);
        }
    }

    void SpawnTouchEffect(Vector3 position)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        worldPosition.z = 0;
        Instantiate(touchEffectPrefab, worldPosition, Quaternion.identity);
    }
}
