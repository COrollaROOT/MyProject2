using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    [SerializeField] private Toggle musicMuteToggle;
    [SerializeField] private Toggle SFXMuteToggle;
    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        { LoadVolume(); }
        else
        { SetMusicVolume(); SetSFXVolume(); }

        musicMuteToggle.isOn = PlayerPrefs.GetInt("MusicMutet", 0) == 1;
        SFXMuteToggle.isOn = PlayerPrefs.GetInt("SFXMutet", 0) == 1; // 음소거 불러오기

        SetMusicMute(musicMuteToggle.isOn);
        SetSFXMute(SFXMuteToggle.isOn);

        musicMuteToggle.onValueChanged.AddListener(SetMusicMute);
        SFXMuteToggle.onValueChanged.AddListener(SetSFXMute); // 이벤트 연결
    }

    private void SetSFXMute(bool isMuted)
    {
        if (isMuted)
        {
            myMixer.SetFloat("SFX", -80f);
        }
        else
        {
            SetSFXVolume(); // 기존 볼륨
        }
        PlayerPrefs.SetInt("SFXMuted", isMuted ? 1 : 0);
    }

    private void SetMusicMute(bool isMuted)
    {
        if (isMuted)
        {
            myMixer.SetFloat("Music", -80f);
        }
        else
        {
            SetMusicVolume(); // 기존 볼륨
        }
        PlayerPrefs.SetInt("MusicMuted", isMuted ? 1 : 0);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetMusicVolume();
        SetSFXVolume();
    }

    
}
