using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class VideoOption : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown resolutionDropdown; // 변수로 선언

    [SerializeField] private Toggle fullscreenToggle; // 전체화면용 변수 선언

    private List<Resolution> resolutions = new List<Resolution>(); // 지원하는 해상도 리스트

    // Called when the script is started
    void Start()
    {
        InitResolutions();
        InitFullscreen();
    }

    private void InitFullscreen()
    {
        bool isFullscereen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1; // 저장된 설정 불러오기
        fullscreenToggle.isOn = isFullscereen; // 토글로 상태 변환
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggle); // 토글 변경시 이벤트 발생
        Screen.fullScreen = isFullscereen; // 실제 화면에 적용
    }

    void OnFullscreenToggle(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    void InitResolutions()
    {
        resolutions.Clear();
        resolutionDropdown.ClearOptions(); // 기존 드랍다운 버튼 정보 지우기

        //resolutions.AddRange(Screen.resolutions); // 연결된 모니터가 지원하는 해상도 목록 가져오기

        HashSet<string> resolutionSet = new HashSet<string>(); // 해상도 목록 간소화를 위한 해쉬셋 사용


        List<string> options = new List<string>();

        int currentIndex = 0; // 현재 해상도
        int validIndex = 0; // Dropdown에 추가된 해상도


        /*for (int i = 0; i < resolutions.Count; i++)
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
        }*/

        foreach (var resolution in Screen.resolutions)
        {
            string key = $"{resolution.width}x{resolution.height}"; // 해상도 문자열로 변환
            if (!resolutionSet.Contains(key)) // 중복된 해상도 제거
            {
                resolutionSet.Add(key); // 중복 체크용
                resolutions.Add(resolution); // 해상도 정보 저장
                options.Add($"{resolution.width} x {resolution.height}"); // 사용자 표시용

                if (resolution.width == Screen.currentResolution.width &&
                resolution.height == Screen.currentResolution.height) // 현재 해상도와 동일한 인덱스 저장
                {
                    currentIndex = validIndex;
                }

                validIndex++;
            }
        }

        resolutionDropdown.AddOptions(options); // 드롭다운에 추가

        int savedIndex = PlayerPrefs.GetInt("ResolutionIndex", currentIndex);
        if (savedIndex < 0 || savedIndex >= resolutions.Count)
            savedIndex = currentIndex; // 저장된 인덱스 불러오기 같지 않으면 현재 해상도 적용

        resolutionDropdown.value = savedIndex;
        resolutionDropdown.RefreshShownValue(); // 인덱스 PlayerPrefs에 저장 및  불러오기

        resolutionDropdown.onValueChanged.AddListener(OnResolutionChage); // 값 변경시 호출

        OnResolutionChage(savedIndex); //해상도 적용




        void OnResolutionChage(int index) // 해상도 변경할때 호출
        {
            if (index < 0 || index >= resolutions.Count) return;

            Resolution resolution = resolutions[index];

            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRateRatio); // 해당 해상도로 변경

            PlayerPrefs.SetInt("ResolutionIndex", index);
        }


        
        

        




    }
}