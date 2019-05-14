using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class SimpleAds : MonoBehaviour
{
    public void ShowAd()
    {
        if (PlayerPrefs.GetInt("noads") != 1)
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
                PlayerPrefs.SetInt("DC", 0);
            }
        }
    }
}
