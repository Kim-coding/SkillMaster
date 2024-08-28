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
    public void Initialize(string text, Color color, float fontSize, bool applyOutline)
    {
        timer = 0f;
        textMesh.text = text;
        textMesh.color = color;
        textMesh.fontSize = fontSize;

        if (applyOutline)
        {
            var newMaterial = Instantiate(textMesh.fontSharedMaterial);
            newMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, 0.25f);
            newMaterial.SetColor(ShaderUtilities.ID_OutlineColor, Color.black);

            textMesh.fontSharedMaterial = newMaterial;
        }

    }

    void Update()
    {
        timer += Time.deltaTime;
        var pos = transform.position;
        pos.y += Time.deltaTime;
        transform.position = pos;

        if (timer > duration)
        {
            timer = 0f;
            GameMgr.Instance.sceneMgr.damageTextMgr.ReturnDamageText(this);
            gameObject.SetActive(false);
        }
    }
}
