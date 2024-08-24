using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemPool : ObjectPool<DropItemMovement>
{
    public DropItemMovement Prefab;

    public DropItemPool(DropItemMovement Prefab, Transform parentTransform, int initialCapacity = 10, int maxCapacity = 100)
        : base(Prefab, parentTransform, initialCapacity, maxCapacity)
    {
        this.Prefab = Prefab;
    }

    public DropItemMovement GetItem()
    {
        DropItemMovement item;
        if(pool.Count > 0 )
        {
            item = pool.Dequeue();
        }
        else
        {
            item = GameObject.Instantiate(Prefab, parentTransform);
        }

        if(item ==  null || item.gameObject == null)
        {
            return null;
        }

        item.gameObject.SetActive(true);
        OnGet(item);
        return item;
    }

    public void ReturnItem(DropItemMovement item)
    {
        OnReturn(item);
        item.gameObject.SetActive(false);
        if(pool.Count < MaxCapacity )
        {
            pool.Enqueue(item);
        }
        else
        {
            GameObject.Destroy(item.gameObject);
        }
    }

    public enum DropItemType
    {
        Gold,
        Dia,
    }
}
