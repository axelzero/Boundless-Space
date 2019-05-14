using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject LoadingBar;
    public string scene;

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level");
    }
    public void LoadSpeedLevel()
    {
        LoadingBar.GetComponent<Loading>().sceneName = scene;
        LoadingBar.SetActive(true);
    }
}
