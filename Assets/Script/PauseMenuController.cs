using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;  // �Ͻ� ����â UI

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
        pauseMenuUI.SetActive(true);   // �Ͻ� ���� UI �ѱ�
        Time.timeScale = 0f;           // ���� �ð� ����
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);  // UI ����
        Time.timeScale = 1f;           // �ð� �簳
        isPaused = false;
    }

    public void QuitGame()
    {
        Debug.Log("���� ���� ó��");

        // ����� ���ӿ��� ����
        Application.Quit();

        // �����Ϳ��� �׽�Ʈ��
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
