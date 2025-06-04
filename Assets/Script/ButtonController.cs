using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("게임 종료 요청됨");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit(); // 실제 게임 실행 파일에서는 종료
#endif
    }

    
    public void GoToTitleScene()
    {
        Debug.Log("타이틀 씬으로 이동");

        
        SceneManager.LoadScene("TitleScenes");
    }
}
