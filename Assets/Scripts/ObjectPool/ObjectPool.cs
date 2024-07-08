using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly T prefab;
    private readonly Transform parentTransform;
    private readonly Queue<T> pool;

    public ObjectPool(T prefab, Transform parentTransform, int initialCapacity = 10)
    {
        this.prefab = prefab;
        this.parentTransform = parentTransform;
        pool = new Queue<T>(initialCapacity);

        for (int i = 0; i < initialCapacity; i++)
        {
            T obj = GameObject.Instantiate(prefab, parentTransform);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public T Get()
    {
        if (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            T obj = GameObject.Instantiate(prefab, parentTransform);
            return obj;
        }
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
