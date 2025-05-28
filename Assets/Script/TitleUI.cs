using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickStart() // 버튼을 클릭 하면 새게임
    {
        Debug.Log("게임 시작");
    }

    public void OnClickOption() // 버튼을 클릭 하면 옵션
    {
        Debug.Log("옵션");
    }

    public void OnClickQuit() // 버튼을 클릭 하면 종료
    {
#if UNITY_EDITOR // 에디터 상에서 게임 구동 테스트를 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

