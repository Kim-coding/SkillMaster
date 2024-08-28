using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMgr : MonoBehaviour
{
    //public AudioSource sfxSource;
    public AudioSource bgmSource;

    public List<AudioClip> clips;

    public Slider bgmSlider;
    public Slider sfxSlider;

    public Toggle damageTextToggle;
    public Toggle dropEffectToggle;

    private SoundPool sfxPool;
    private float sfxVolume = 1f;

    private void Awake()
    {
        sfxPool = new SoundPool(gameObject);
    }

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
            sfxSlider.value = sfxVolume;
        }
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;

        foreach (var source in sfxPool.pool)
        {
            source.volume = sfxVolume;
        }
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
                var sfxSource = sfxPool.Get();
                sfxSource.volume = sfxVolume;
                sfxSource.PlayOneShot(clip);
                sfxPool.Return(sfxSource);
                break;
            }
        }
    }
    public void PlaySFX(AudioClip clip)
    {
        var sfxSource = sfxPool.Get();
        sfxSource.volume = sfxVolume;
        sfxSource.PlayOneShot(clip);
        sfxPool.Return(sfxSource);
    }

    public void StopSFX()
    {
        foreach (var source in sfxPool.pool)
        {
            source.Stop();
        }
    }
}
