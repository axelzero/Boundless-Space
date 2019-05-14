using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateManager : Singleton<RateManager>
{
    [SerializeField]
    private RateBox rateBox;

    [HideInInspector]
    public static RateManager rateManager;
    //[SerializeField]
    //private Text playCountText;

    private int countToRate = 6;

    [HideInInspector]
    public int playCount;
    [HideInInspector]
    public bool rateOff = false;

    private void Start()
    {
        rateManager = this;
    }

    public void ClicPlay()
    {
        playCount = PlayerPrefs.GetInt("CountToRate", 0);
       // playCountText.text = playCount.ToString();

        if (playCount % countToRate == 0 && !rateOff)
        {
            rateBox.gameObject.SetActive(true);
        }
    }
}
