using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    public AudioSource audioSource;

    public List<AudioClip> clips;


    public void PlaySFX(string clipName)
    {
        foreach (AudioClip clip in clips)
        {
            if (clip.name == clipName)
            {
                audioSource.PlayOneShot(clip);
                break;
            }
        }
    }
    public void PlaySFX(AudioClip clip)
    {

        audioSource.PlayOneShot(clip);

    }

}
