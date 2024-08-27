using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    public AudioSource audioSource;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
    }

    public void PlaySound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    public void Reset()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        transform.localScale = initialScale;

        MonoBehaviour[] components = GetComponents<MonoBehaviour>();
        foreach (var component in components)
        {
            if(component is ISkillComponent)
            {
                Destroy(component);
            }
        }

        Collider[] colliders = GetComponents<Collider>();
        foreach (var collider in colliders)
        {
            Destroy(collider);
        }

        Collider2D[] colliders2D = GetComponents<Collider2D>();
        foreach (var collider2D in colliders2D)
        {
            Destroy(collider2D);
        }
    }
}