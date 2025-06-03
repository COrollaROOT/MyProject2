using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneChanger : MonoBehaviour
{
    public void GameScenesCtrl()
    {
        LoadingScene.LoadScene("GameScenes");
    }
}
