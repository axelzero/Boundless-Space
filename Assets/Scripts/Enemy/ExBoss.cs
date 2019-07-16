using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExBoss : MonoBehaviour
{
    public int lifePoints;

    public GameObject bullet;
    public Transform[] shootPlace;

    public float shootDelay;

    public bool isEnemyDead = false;

    public PleyerController ship;

    public SoundManager sm;

    public GameObject Coin;

    private Root root;

    private void Start()
    {
        sm = GameObject.Find("GameZone").GetComponent<SoundManager>();

        StartCoroutine(ShootCaroutine(shootDelay));

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
        SpawnCoin(5);
        ship.AdSc(50);

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
    }
}
