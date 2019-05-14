using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetReadyTxt : MonoBehaviour
{
    public GameObject getReadyPrefab;
    private bool timerNeed; 
    private float timerToOff;

    public void GetReadyText()
    {
        getReadyPrefab.SetActive(true);
        timerNeed = true;
    }

    private void Update()
    {
        if (timerNeed)
        {
            timerToOff += Time.deltaTime;
            if (timerToOff >= 3f)
            {
                getReadyPrefab.SetActive(false);
                timerNeed = false;
                timerToOff = 0f;
            }
        }
    }
}
