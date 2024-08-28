using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPool
{
    private readonly GameObject soundMgr;
    public readonly Queue<AudioSource> pool;
    private readonly int maxCapacity;
    private int currentIndex = 0;

    public SoundPool(GameObject soundMgr, int initialCapacity = 5, int maxCapacity = 20)
    {
        this.soundMgr = soundMgr;
        this.maxCapacity = maxCapacity;
        pool = new Queue<AudioSource>(initialCapacity);

        for (int i = 0; i < initialCapacity; i++)
        {
            var source = soundMgr.AddComponent<AudioSource>();
            source.playOnAwake = false;
            pool.Enqueue(source);
        }
    }

    public AudioSource Get()
    {
        AudioSource source;

        if (pool.Count > 0)
        {
            source = pool.Dequeue();
        }
        else
        {
            if (currentIndex < maxCapacity)
            {
                source = soundMgr.AddComponent<AudioSource>();
                source.playOnAwake = false;
                currentIndex++;
            }
            else
            {
                source = pool.Dequeue();
            }
        }

        return source;
    }

    public void Return(AudioSource source)
    {
        pool.Enqueue(source);
    }
}
