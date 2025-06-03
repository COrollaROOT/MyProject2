using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TitleUI : MonoBehaviour
{

    public OptionUI optionUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickStart() // ��ư�� Ŭ�� �ϸ� ������
    {
        Debug.Log("���� ����");
    }

    public void OnClickOption() // ��ư�� Ŭ�� �ϸ� �ɼ�
    {
        Debug.Log("�ɼ�");
        optionUI.Open();
    }

    public void OnClickQuit() // ��ư�� Ŭ�� �ϸ� ����
    {
#if UNITY_EDITOR // ������ �󿡼� ���� ���� �׽�Ʈ�� ����
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

