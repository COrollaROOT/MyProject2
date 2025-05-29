using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoOption : MonoBehaviour
{
    [SerializeField] private Dropdown resolutionDropdown; // ������ ����

    List<Resolution> resolutions = new List<Resolution>(); // �����ϴ� �ػ� ����Ʈ

    // Called when the script is started
    void Start()
    {
        InitResolutions();
    }

    void InitResolutions()
    {
        resolutions.Clear();
        resolutionDropdown.ClearOptions(); // ���� ����ٿ� ��ư ���� �����

        resolutions.AddRange(Screen.resolutions); // ����� ����Ͱ� �����ϴ� �ػ� ��� ��������


        List<string> options = new List<string>();
        int currentIndex = 0;

        for (int i = 0; i < resolutions.Count; i++)
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
        }

        void OnResolutionChage(int index) // �ػ� �����Ҷ� ȣ��
        {
            Resolution resolution = resolutions[index];

            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRateRatio); // �ش� �ػ󵵷� ����

            PlayerPrefs.SetInt("ResolutionIndex", index);
        }




    }
}