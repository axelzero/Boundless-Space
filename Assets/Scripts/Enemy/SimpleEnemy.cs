using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
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
    [HideInInspector]
    public int countExBosses = -1;  // для того, что бы отслеживать сколько ексБоссов доступно в DefeatedBossesGeneration

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

        if (Input.GetMouseButtonDown(0) && !isBoss)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit && hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<SimpleEnemy>().Damage(1);
            }
        }
    }

    void DestroyEnemy()
    {
        isEnemyDead = true;
        sm.PlaySound(0);
        if (isBoss)
        {
            GameObject p = Instantiate(fxExplo, transform.position, Quaternion.identity) as GameObject;
            p.GetComponent<ParticleSystem>().Play();
            Destroy(p, p.GetComponent<ParticleSystem>().main.duration);
            SpawnCoin(20);
            ship.AdSc(500);
            RootSpeedLevel.rootSpeedLevel.BossState = RootSpeedLevel.Boss.WaitBoss;
            RootSpeedLevel.rootSpeedLevel.TimerToInstatiateNewBoss *= 2f;
            sm.PlaySound(0);
            sm.PlaySound(0);
            countExBosses++;
            DefeatedBossesGeneration.defeatedBossesGeneration.countExBoss++;
            if (DefeatedBossesGeneration.defeatedBossesGeneration.countExBoss >= 5)
            {
                DefeatedBossesGeneration.defeatedBossesGeneration.generatorToActive.SetActive(true);
            }
        }
        else
        {
            SpawnCoin();
            ship.AdSc(25);
        }
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
    void SpawnCoin(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject c = Instantiate(Coin, transform.position, Quaternion.identity) as GameObject;
            c.transform.localScale = new Vector3(3f, 3f, 3f);
            c.transform.SetParent(GameObject.Find("Coins").transform);
        }
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
