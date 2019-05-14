using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [Header("Loading Scene")]
    public string sceneName;
    [Space(20)]
    [Header("Objects")]
    public Image loadingImg;
    public Text txtprogress;

    private void Start()
    {
        StartCoroutine(AsyncLoad());
    }

    IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            loadingImg.fillAmount = progress;
            txtprogress.text = string.Format("{0:0} %", progress * 100);
            yield return null;
        }
    }
}
