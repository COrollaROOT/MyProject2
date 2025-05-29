using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class VideoOption : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown resolutionDropdown; // ������ ����

    [SerializeField] private Toggle fullscreenToggle; // ��üȭ��� ���� ����

    private List<Resolution> resolutions = new List<Resolution>(); // �����ϴ� �ػ� ����Ʈ

    // Called when the script is started
    void Start()
    {
        InitResolutions();
        InitFullscreen();
    }

    private void InitFullscreen()
    {
        bool isFullscereen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1; // ����� ���� �ҷ�����
        fullscreenToggle.isOn = isFullscereen; // ��۷� ���� ��ȯ
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggle); // ��� ����� �̺�Ʈ �߻�
        Screen.fullScreen = isFullscereen; // ���� ȭ�鿡 ����
    }

    void OnFullscreenToggle(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    void InitResolutions()
    {
        resolutions.Clear();
        resolutionDropdown.ClearOptions(); // ���� ����ٿ� ��ư ���� �����

        //resolutions.AddRange(Screen.resolutions); // ����� ����Ͱ� �����ϴ� �ػ� ��� ��������

        HashSet<string> resolutionSet = new HashSet<string>(); // �ػ� ��� ����ȭ�� ���� �ؽ��� ���


        List<string> options = new List<string>();

        int currentIndex = 0; // ���� �ػ�
        int validIndex = 0; // Dropdown�� �߰��� �ػ�


        /*for (int i = 0; i < resolutions.Count; i++)
        {
            var resolution = resolutions[i];
            // ���� ����
            string option = $"{resolution.width} x {resolution.height} @ {resolution.refreshRateRatio.value:F2}Hz";
            options.Add(option);

            // �ػ󵵿� ��ġ�ϴ� �ε��� ����
            if (resolution.width == Screen.currentResolution.width &&
                resolution.height == Screen.currentResolution.height &&
                Mathf.Approximately((float)resolution.refreshRateRatio.value, (float)Screen.currentResolution.refreshRateRatio.value))
            {
                currentIndex = i;
            }

            resolutionDropdown.AddOptions(options); // ��Ӵٿ �߰�

            int savedIndex = PlayerPrefs.GetInt("ResolutionIndex", currentIndex);
            resolutionDropdown.value = savedIndex;
            resolutionDropdown.RefreshShownValue(); // �ε��� PlayerPrefs�� ���� ��  �ҷ�����

            resolutionDropdown.onValueChanged.AddListener(OnResolutionChage);

            OnResolutionChage(savedIndex);
        }*/

        foreach (var resolution in Screen.resolutions)
        {
            string key = $"{resolution.width}x{resolution.height}"; // �ػ� ���ڿ��� ��ȯ
            if (!resolutionSet.Contains(key)) // �ߺ��� �ػ� ����
            {
                resolutionSet.Add(key); // �ߺ� üũ��
                resolutions.Add(resolution); // �ػ� ���� ����
                options.Add($"{resolution.width} x {resolution.height}"); // ����� ǥ�ÿ�

                if (resolution.width == Screen.currentResolution.width &&
                resolution.height == Screen.currentResolution.height) // ���� �ػ󵵿� ������ �ε��� ����
                {
                    currentIndex = validIndex;
                }

                validIndex++;
            }
        }

        resolutionDropdown.AddOptions(options); // ��Ӵٿ �߰�

        int savedIndex = PlayerPrefs.GetInt("ResolutionIndex", currentIndex);
        if (savedIndex < 0 || savedIndex >= resolutions.Count)
            savedIndex = currentIndex; // ����� �ε��� �ҷ����� ���� ������ ���� �ػ� ����

        resolutionDropdown.value = savedIndex;
        resolutionDropdown.RefreshShownValue(); // �ε��� PlayerPrefs�� ���� ��  �ҷ�����

        resolutionDropdown.onValueChanged.AddListener(OnResolutionChage); // �� ����� ȣ��

        OnResolutionChage(savedIndex); //�ػ� ����




        void OnResolutionChage(int index) // �ػ� �����Ҷ� ȣ��
        {
            if (index < 0 || index >= resolutions.Count) return;

            Resolution resolution = resolutions[index];

            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRateRatio); // �ش� �ػ󵵷� ����

            PlayerPrefs.SetInt("ResolutionIndex", index);
        }


        
        

        




    }
}