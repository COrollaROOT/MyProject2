using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;  // 일시 정지창 UI

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);   // 일시 정지 UI 켜기
        Time.timeScale = 0f;           // 게임 시간 정지
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);  // UI 끄기
        Time.timeScale = 1f;           // 시간 재개
        isPaused = false;
    }

    public void QuitGame()
    {
        Debug.Log("게임 종료 처리");

        // 빌드된 게임에서 종료
        Application.Quit();

        // 에디터에서 테스트용
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
