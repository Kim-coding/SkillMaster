using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextMgr : MonoBehaviour
{
    public DamageText damageTextPrefab;
    private DamageTextPool damageTextPool;

    private void Start()
    {
        damageTextPool = new DamageTextPool(damageTextPrefab, transform);
    }

    public void ShowDamageText(Vector3 position, string text, Color color, float fontSize)
    {
        DamageText damageText = damageTextPool.Get();
        damageText.transform.position = position;
        damageText.Initialize(text, color, fontSize);
    }

    public void ReturnDamageText(DamageText damageText)
    {
        damageTextPool.Return(damageText);
    }
}
