using Assets.SimpleAndroidNotifications;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notify : MonoBehaviour
{
    private string title = "Come back to the game :)";

    private string content = "";
    void OnApplicationPause(bool pause)
    {
        NotificationManager.CancelAll();
        if (pause)
        {
            DateTime timeToNotify = DateTime.Now.AddMinutes(60); // время до сообщения
            TimeSpan time = timeToNotify - DateTime.Now;
            NotificationManager.SendWithAppIcon(time, title, content, Color.red, NotificationIcon.Heart);
        }
    }
}
