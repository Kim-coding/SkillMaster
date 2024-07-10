using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly T prefab;
    private readonly Transform parentTransform;
    public readonly Queue<T> pool;
    private readonly int maxCapacity;
    public int MaxCapacity { get { return maxCapacity; } }

    public ObjectPool(T prefab, Transform parentTransform, int initialCapacity = 10, int maxCapacity = 100)
    {
        this.prefab = prefab;
        this.parentTransform = parentTransform;
        this.maxCapacity = maxCapacity;
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
        Debug.Log(pool.Count);
        T obj;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = GameObject.Instantiate(prefab, parentTransform);
        }
        if (obj == null || obj.gameObject == null)
        {
            return null;
        }
        obj.gameObject.SetActive(true);
        OnGet(obj);
        return obj;
    }

    public void Return(T obj)
    {
        if (pool.Count >= maxCapacity)
        {
            pool.Enqueue(obj);
            GameObject.Destroy(obj.gameObject);
        }
        else
        {
            //obj.gameObject.SetActive(false);
            OnReturn(obj);
            pool.Enqueue(obj);
        }
    }

    protected virtual void OnGet(T obj)
    {
        
    }

    protected virtual void OnReturn(T obj)
    {
        
    }
}
