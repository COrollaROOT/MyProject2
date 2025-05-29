using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoOption : MonoBehaviour
{
    [SerializeField] private Dropdown resolutionDropdown; // 변수로 선언

    List<Resolution> resolutions = new List<Resolution>(); // 지원하는 해상도 리스트

    // Called when the script is started
    void Start()
    {
        InitResolutions();
    }

    void InitResolutions()
    {
        resolutions.Clear();
        resolutionDropdown.ClearOptions(); // 기존 드랍다운 버튼 정보 지우기

        resolutions.AddRange(Screen.resolutions); // 연결된 모니터가 지원하는 해상도 목록 가져오기


        List<string> options = new List<string>();
        int currentIndex = 0;

        for (int i = 0; i < resolutions.Count; i++)
        {
            var resolution = resolutions[i];
            // 문자 생성
            string option = $"{resolution.width} x {resolution.height} @ {resolution.refreshRateRatio.value:F2}Hz";
            options.Add(option);

            // 해상도와 일치하는 인덱스 저장
            if (resolution.width == Screen.currentResolution.width &&
                resolution.height == Screen.currentResolution.height &&
                Mathf.Approximately((float)resolution.refreshRateRatio.value, (float)Screen.currentResolution.refreshRateRatio.value))
            {
                currentIndex = i;
            }

            resolutionDropdown.AddOptions(options); // 드롭다운에 추가

            int savedIndex = PlayerPrefs.GetInt("ResolutionIndex", currentIndex);
            resolutionDropdown.value = savedIndex;
            resolutionDropdown.RefreshShownValue(); // 인덱스 PlayerPrefs에 저장 및  불러오기

            resolutionDropdown.onValueChanged.AddListener(OnResolutionChage);

            OnResolutionChage(savedIndex);
        }

        void OnResolutionChage(int index) // 해상도 변경할때 호출
        {
            Resolution resolution = resolutions[index];

            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRateRatio); // 해당 해상도로 변경

            PlayerPrefs.SetInt("ResolutionIndex", index);
        }




    }
}