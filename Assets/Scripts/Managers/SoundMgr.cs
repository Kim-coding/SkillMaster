using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMgr : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioSource bgmSource;

    public List<AudioClip> clips;

    public Slider bgmSlider;
    public Slider sfxSlider;

    public Toggle damageTextToggle;
    public Toggle dropEffectToggle;

    void Start()
    {
        bgmSource = Camera.main.GetComponent<AudioSource>();

        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        var save = SaveLoadSystem.CurrSaveData.savePlay;
        if(save != null)
        {
            bgmSlider.value = save.bgm;
            sfxSlider.value = save.sfx;
            SetBGMVolume(bgmSlider.value);
            SetSFXVolume(sfxSlider.value);
        }
        else
        {
            bgmSlider.value = bgmSource.volume;
            sfxSlider.value = sfxSource.volume;
        }
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
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
