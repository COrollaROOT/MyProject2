using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    public Image brightnessOverlay; // 화면 덮는 이미지
    public Slider brightnessSlider; // 밝기 조절 슬라이더 연결

    private const string BRIGHTNESS_KEY = "Brightness"; // 저장


    // Start is called before the first frame update
    void Start()
    {
        float savedBrightness = PlayerPrefs.GetFloat(BRIGHTNESS_KEY, 1f); // 저장된값 불러오기

        brightnessSlider.value = savedBrightness; // 슬라이더 값 초기화

        ApplyBrightness(savedBrightness); // 초기 밝기

        brightnessSlider.onValueChanged.AddListener(OnBrightnessChanged);
    }

    public void OnBrightnessChanged(float value)
    {
        ApplyBrightness(value);
        PlayerPrefs.SetFloat(BRIGHTNESS_KEY, value);
    }

    private void ApplyBrightness(float savedBrightness)
    {
        float alpha = 1f - savedBrightness;
        brightnessOverlay.color = new Color(0, 0, 0, alpha); // 오버레이 알파값 != 밝기
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
