using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootL1 : MonoBehaviour                 // TODO stateMashine
{
    [HideInInspector]
    public static RootL1 rootL1;
    [HideInInspector]
    public enum L1 { Intro, StartGame};
    [HideInInspector]
    public L1 L1State;

    public GameObject intro;
    public GameObject startSpeak;



    private void Start()
    {
        rootL1 = this;
        intro.SetActive(true);
    }

    public void StartGame()
    {
        intro.SetActive(false);
        L1State = L1.StartGame;
        startSpeak.SetActive(false);
    }
}
