using UnityEngine;

public class MoveEffect : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float duration;

    private float elapsed = 0f;

    public void Initialize(Vector3 start, Vector3 end, float duration)
    {
        this.startPosition = start;
        this.endPosition = end;
        this.duration = duration;
        this.elapsed = 0f;

        Vector3 direction = (end - start).normalized;

        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            var mainModule = particleSystem.main;
            mainModule.startRotation = Mathf.Atan2(-direction.y, direction.x);
        }
    }

    void Update()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.position = Vector3.Lerp(startPosition, endPosition, t);

            if (elapsed >= duration)
            {
                Destroy(gameObject);
            }
        }
    }
}
