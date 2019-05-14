using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    //public static SimpleEnemy simpleEnemy;

    public int lifePoints;

    public GameObject bullet;
    public Transform[] shootPlace;

    public float shootDelay;
    private float shootDelayStart;  // Сюда будет записано значение, которое будет постоянным для определенного обьекта

    public bool isEnemyDead = false;

    public PleyerController ship;

    public SoundManager sm;

    public GameObject Coin;

    public GameObject bulletsPlaceHolder;

    private Root root;

    [Space (20)]
    [Header("BOSS")]
    public bool isBoss;
    public GameObject fxExplo;


    private void Start()
    {
        if (!isBoss)
        {
            lifePoints = PlayerPrefs.GetInt("lifePoints");
            shootDelay = PlayerPrefs.GetFloat("shootDelay");
        }


        sm = GameObject.Find("GameZone").GetComponent<SoundManager>();

        StartCoroutine(ShootCaroutine(shootDelayStart));

        ship = GameObject.Find("Player").GetComponent<PleyerController>();
    }

    IEnumerator ShootCaroutine(float time)
    {
        yield return new WaitForSeconds(time);
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(shootDelay);
        }
    }

    private void Update()
    {
        if (lifePoints == 0 && !isEnemyDead)
        {
            DestroyEnemy();
        }
    }

    void DestroyEnemy()
    {
        isEnemyDead = true;
        sm.PlaySound(0);
        SpawnCoin();
        if (isBoss)
        {
            GameObject p = Instantiate(fxExplo, transform.position, Quaternion.identity) as GameObject;
            p.GetComponent<ParticleSystem>().Play();
            Destroy(p, p.GetComponent<ParticleSystem>().main.duration);

            ship.AdSc(500);
            RootSpeedLevel.rootSpeedLevel.BossState = RootSpeedLevel.Boss.WaitBoss;
            RootSpeedLevel.rootSpeedLevel.TimerToInstatiateNewBoss *= 2f;
        }
        ship.AdSc(25);
        int countKilled = PlayerPrefs.GetInt("EnemyKilled", 0);
        countKilled++;
        PlayerPrefs.SetInt("EnemyKilled", countKilled);
        Destroy(gameObject);
    }

    void SpawnCoin()
    {
        GameObject c = Instantiate(Coin, transform.position, Quaternion.identity) as GameObject;
        c.transform.SetParent(GameObject.Find("Coins").transform);
    }

    void Shoot()
    {
        if (Root.rootGame.GameState == Root.Game.Play)
        {
            for (int i = 0; i < shootPlace.Length; i++)
            {
                GameObject b = Instantiate(bullet, shootPlace[i].position, Quaternion.identity) as GameObject;
                b.transform.SetParent(GameObject.Find("EnemyBullets").transform);
                sm.PlaySound(5);
                Destroy(b, 6f);
            }
        }
    }


    public void Damage(int dmg)
    {
        lifePoints -= dmg;
        if (lifePoints < 0)
        {
            lifePoints = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("playerBull"))
        {
            Damage(ship.bullDamage);
            sm.PlaySound(1);
            Destroy(col.gameObject);
        }
        if (isBoss)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                Destroy(col.gameObject);
            }
        }
    }
}
