using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPool : ObjectPool<BaseSkill>
{
    public SkillPool(BaseSkill skillPrefab, Transform parentTransform, int initialCapacity = 20, int maxCapacity = 50)
        : base(skillPrefab, parentTransform, initialCapacity, maxCapacity) 
    { 

    }

    protected override void OnGet(BaseSkill baseSkill)
    {
        baseSkill.gameObject.SetActive(true);
    }

    protected override void OnReturn(BaseSkill baseSkill)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = baseSkill.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = baseSkill.transform.GetChild(i);
            list.Add(child.gameObject);
        }

        foreach(var child in list)
        {
            GameObject.Destroy(child);
        }

        baseSkill.gameObject.SetActive(false);
        baseSkill.Reset();
    }
}
