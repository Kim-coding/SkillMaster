using TMPro;
using UnityEngine;

public class DisplayText : MonoBehaviour
{
    private float timer = 0f;
    private float duration = 1f;
    public TextMeshPro textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        if (textMesh == null)
        {
            Debug.LogError("TextMeshProUGUI component is missing on DamageText prefab.");
        }
    }
    public void Initialize(string text, Color color, float fontSize)
    {
        timer = 0f;
        textMesh.text = text;
        textMesh.color = color;
        textMesh.fontSize = fontSize;
    }

    void Update()
    {
        timer += Time.deltaTime;
        var pos = transform.position;
        pos.y += 0.001f;
        transform.position = pos;

        if (timer > duration)
        {
            timer = 0f;
            GameMgr.Instance.sceneMgr.damageTextMgr.ReturnDamageText(this);
            gameObject.SetActive(false);
        }
    }
}
