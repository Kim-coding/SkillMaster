using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextMgr : MonoBehaviour
{
    public DisplayText damageTextPrefab;
    private DamageTextPool damageTextPool;

    private void Start()
    {
        damageTextPool = new DamageTextPool(damageTextPrefab, transform);
    }

    public void ShowDamageText(Vector3 position, string text, Color color, float fontSize, bool applyOutline)
    {
        DisplayText damageText = damageTextPool.Get();
        damageText.transform.position = position;
        damageText.Initialize(text, color, fontSize, applyOutline);
    }

    public void ReturnDamageText(DisplayText damageText)
    {
        damageTextPool.Return(damageText);
    }
}
