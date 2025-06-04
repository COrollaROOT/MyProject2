using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timeText; // TextMeshPro UI 연결
    public string targetScene = "GameScenes";

    private float startTime;
    private bool timerActive = false;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != targetScene)
        {
            gameObject.SetActive(false);
            return;
        }

        startTime = Time.time;
        timerActive = true;

    }

    void Update()
    {
        if (!timerActive) return;
        float elapsed = Time.time - startTime; // 게임 시작 후 경과 시간
        int minutes = Mathf.FloorToInt(elapsed / 60f);
        int seconds = Mathf.FloorToInt(elapsed % 60f);
        float fraction = elapsed % 1f;

        timeText.text = string.Format("{0:00}:{1:00}.{2:0}", minutes, seconds,fraction * 10f);
    }
}
