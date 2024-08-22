using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMgr : MonoBehaviour
{
    public AudioSource sfxSource;

    public List<AudioClip> clips;

    public Slider bgmSlider;
    public Slider sfxSlider;

    void Start()
    {
        //bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        //bgmSlider.value = bgmSource.volume;
        sfxSlider.value = sfxSource.volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void PlaySFX(string clipName)
    {
        foreach (AudioClip clip in clips)
        {
            if (clip.name == clipName)
            {
                sfxSource.PlayOneShot(clip);
                break;
            }
        }
    }
    public void PlaySFX(AudioClip clip)
    {

        sfxSource.PlayOneShot(clip);

    }

}
