using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("���� ���� ��û��");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit(); // ���� ���� ���� ���Ͽ����� ����
#endif
    }

    
    public void GoToTitleScene()
    {
        Debug.Log("Ÿ��Ʋ ������ �̵�");

        
        SceneManager.LoadScene("TitleScenes");
    }
}
