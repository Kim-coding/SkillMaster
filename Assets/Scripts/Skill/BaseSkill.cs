using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    public AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    public void ResetComponents()
    {
        MonoBehaviour[] components = GetComponents<MonoBehaviour>();
        foreach (var component in components)
        {
            if(component is ISkillComponent)
            {
                Destroy(component);
            }
        }
    }
}