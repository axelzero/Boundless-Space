using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CloudOnce;

public  class Root : MonoBehaviour
{
    [HideInInspector]
    public static Root rootGame;
    [HideInInspector]
    public enum Game { Play, Dead, Pause };
    [HideInInspector]
    public Game GameState;

    [Space (20)]
    [Header ("Boss")]
    public bool isBossComming = false;
    public GameObject bossMessage;
    [HideInInspector]
    public float timerMessage = 0f;



    private void Awake()
    {
        PlayerPrefs.SetInt("lifePoints", 2);
        PlayerPrefs.SetFloat("speedEnemy", 2f);
        PlayerPrefs.SetFloat("shootDelay", 3f);
        rootGame = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameState = Game.Play;
        
        AchivkiGameCount();
    }

    private void Update()
    {
        if (isBossComming)
        {
            timerMessage += Time.deltaTime;
            bossMessage.SetActive(true);

            if (timerMessage >= 3f)
            {
                isBossComming = false;
                bossMessage.SetActive(false);
                timerMessage = 0;
            }
        }

        if (PlayerPrefs.GetInt("EnemyKilled") == 10)
        {
            Achievements.Kill10enemys.Unlock();
        }

        if (PlayerPrefs.GetInt("EnemyKilled") == 50)
        {
            Achievements.Kill50enemys.Unlock();
        }

        if (PlayerPrefs.GetInt("EnemyKilled") == 100)
        {
            Achievements.Kill100enemys.Unlock();
        }

        if (PlayerPrefs.GetInt("EnemyKilled") == 200)
        {
            Achievements.Kill200enemys.Unlock();
        }

        if (PlayerPrefs.GetInt("EnemyKilled") == 500)
        {
            Achievements.Kill500enemys.Unlock();
        }

        if (PlayerPrefs.GetInt("EnemyKilled") == 1000)
        {
            Achievements.Kill1000enemys.Unlock();
        }
    }

    public void AchivkiGameCount()
    {
        Achievements.Firstgame.Unlock();

        int countGames = 0;
        countGames = PlayerPrefs.GetInt("countGames", 0) + 1;   //Запись сколько было сыграно раз
        PlayerPrefs.SetInt("countGames", countGames);

        if (PlayerPrefs.GetInt("countGames") == 10)        //Очивки
        {
            Achievements.games10.Unlock();
        }

        if (PlayerPrefs.GetInt("countGames") == 100)        //Очивки
        {
            Achievements.games100.Unlock();
        }

        if (PlayerPrefs.GetInt("countGames") == 150)        //Очивки
        {
            Achievements.games150.Unlock();
        }

        if (PlayerPrefs.GetInt("countGames") == 500)        //Очивки
        {
            Achievements.games500.Unlock();
        }
    }
}
