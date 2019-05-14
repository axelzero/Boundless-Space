using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject Ship;
    public int damage;
    private int health = 3;

    public SoundManager sm;

    private void Start()
    {
        sm = GameObject.Find("GameZone").GetComponent<SoundManager>();
        Ship = GameObject.Find("Player");
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy"))
        {
            coll.gameObject.GetComponent<SimpleEnemy>().Damage(damage);
            sm.PlaySound(0);
            Destroy(gameObject);
        }
        else if (coll.gameObject.CompareTag("Boss"))
        {
            coll.gameObject.GetComponent<SimpleEnemy>().Damage(damage);
            sm.PlaySound(0);
            Destroy(gameObject);
        }
        else if (coll.gameObject.CompareTag("enemyBull"))
        {
            health--;
            Destroy(coll.gameObject);
            if (health <= 0)
            {
                sm.PlaySound(0);
                Destroy(gameObject);
            }
        }
        else if (coll.gameObject.CompareTag("mine"))
        {
            Destroy(coll.gameObject);
            sm.PlaySound(0);
            Ship.GetComponent<PleyerController>().AdSc(35);
            Destroy(gameObject);
        }
    }
}
