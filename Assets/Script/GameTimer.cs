using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timeText; // TextMeshPro UI 연결

    void Update()
    {
        float time = Time.time; // 게임 시작 후 경과 시간
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        float fraction = time % 1f;

        timeText.text = string.Format("{0:00}:{1:00}.{2:0}", minutes, seconds,fraction * 10f);
    }
}
