using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    public Image brightnessOverlay; // ȭ�� ���� �̹���
    public Slider brightnessSlider; // ��� ���� �����̴� ����

    private const string BRIGHTNESS_KEY = "Brightness"; // ����


    // Start is called before the first frame update
    void Start()
    {
        float savedBrightness = PlayerPrefs.GetFloat(BRIGHTNESS_KEY, 1f); // ����Ȱ� �ҷ�����

        brightnessSlider.value = savedBrightness; // �����̴� �� �ʱ�ȭ

        ApplyBrightness(savedBrightness); // �ʱ� ���

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
        brightnessOverlay.color = new Color(0, 0, 0, alpha); // �������� ���İ� != ���
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
