using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSpeedLevel : MonoBehaviour
{
    [HideInInspector]
    public static RootSpeedLevel rootSpeedLevel;
    private float nTimer = 0f;
    private float nTimer2 = 0f;
    private bool isDone = false;

    public GameObject[] Bosses;
    public Transform BossStartPosition;
    private float nTimerBoss;

    public float TimerToInstatiateNewBoss = 60f;
    private int nBossNumber = 0;

    [HideInInspector]
    public enum Boss { WaitBoss, Boss, Idle };
    [HideInInspector]
    public Boss BossState;

    private void Start()
    {
        rootSpeedLevel = this;
    }

    void Update()
    {
        if (!isDone)
        {
            nTimer += Time.deltaTime;
            nTimer2 += Time.deltaTime;
            LevelDifficultyUp();
        }
        BossGo(nBossNumber);
    }

    private void BossGo(int numBoss)
    {
        if (Root.rootGame.GameState != Root.Game.Dead && nBossNumber < Bosses.Length)
        {
            if (BossState == Boss.WaitBoss)
            {
                nTimerBoss += Time.deltaTime;
                if (nTimerBoss >= TimerToInstatiateNewBoss)
                {
                    BossState = Boss.Boss;
                }
            }
            else if (BossState == Boss.Boss)
            {
                nTimerBoss += Time.deltaTime;

                if (nTimerBoss >= TimerToInstatiateNewBoss && nTimerBoss < TimerToInstatiateNewBoss + 4f)
                {
                    Root.rootGame.isBossComming = true;  // Вызовет предупреждение о боссе
                }
                else if (nTimerBoss >= TimerToInstatiateNewBoss + 5f)
                {
                    GameObject boss = Instantiate(Bosses[numBoss], BossStartPosition.position, Quaternion.identity) as GameObject;
                    nBossNumber++;
                    nTimerBoss = 0;
                    BossState = Boss.Idle;
                }
            }
        }
    }


    private void LevelDifficultyUp()
    {
        if (nTimer >= 120f)
        {
            if (PlayerPrefs.GetInt("lifePoints") <= 3)
            {
                int Enemylife = PlayerPrefs.GetInt("lifePoints") + 1; // lifePoints UP
                PlayerPrefs.SetInt("lifePoints", Enemylife);          // Seve
                Debug.Log("Enemylife" + Enemylife);
            }

            

            if (PlayerPrefs.GetInt("lifePoints") >= 4 && PlayerPrefs.GetFloat("speedEnemy") >= 5.5f && PlayerPrefs.GetFloat("shootDelay") <= 0.8f && EnemyGenerator.enemyGenerator.maxDelay <= EnemyGenerator.enemyGenerator.minDelay)
            {
                isDone = true;
                Debug.Log("Time Done!");
            }

            nTimer = 0f;
        }

        if (nTimer2 > 40f)
        {
            if (PlayerPrefs.GetFloat("speedEnemy") <= 5.5f)
            {
                float EnemySpeed = PlayerPrefs.GetFloat("speedEnemy") + 0.15f; //Speed UP
                PlayerPrefs.SetFloat("speedEnemy", EnemySpeed);               //Save
                Debug.Log("EnemySpeed" + EnemySpeed);
            }

            if (PlayerPrefs.GetFloat("shootDelay") > 1f)
            {
                float EnemyShoot = PlayerPrefs.GetFloat("shootDelay") - 0.05f; //Shoot Speed UP
                PlayerPrefs.SetFloat("shootDelay", EnemyShoot);               //Save
                Debug.Log("EnemyShoot" + EnemyShoot);
            }

            if (EnemyGenerator.enemyGenerator.maxDelay >= EnemyGenerator.enemyGenerator.minDelay)
            {
                EnemyGenerator.enemyGenerator.maxDelay -= 0.2f;
            }
            nTimer2 = 0f;
        }
    }

    void OnApplicationQuit()
    {
        CloudOnce.Cloud.SignOut();
        //PlayGamesPlatform.Instance.SignOut();
    }
}
