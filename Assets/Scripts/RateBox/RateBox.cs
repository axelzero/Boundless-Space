using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateBox : MonoBehaviour
{
    public void BtnNoThanks()
    {
        RateManager.Instance.rateOff = true;
        PlayerPrefs.SetInt("rateOff", -1);
        gameObject.SetActive(false);
    }

    public void BtnLater()
    {
        gameObject.SetActive(false);
    }

    public void BtnRateNow()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.BigStudent.BoundlessSpace");
        gameObject.SetActive(false);
    }
}
