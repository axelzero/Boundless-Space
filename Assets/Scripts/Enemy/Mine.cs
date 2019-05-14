using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{

    public float distToActive;
    public float speed;
    public int mineDamage = 3;
    public GameObject Ship;

    public SoundManager sm;

    private float pipMineTimer = 1f;
    private float pipMineTimerStart;

    private int nHP = 3;
    void Start()
    {
        pipMineTimerStart = pipMineTimer;
        Ship = GameObject.Find("Player");
        sm = GameObject.Find("GameZone").GetComponent<SoundManager>();
    }


    void Update()
    {
        if (Vector2.Distance(transform.position, Ship.transform.position) <= distToActive && !Ship.GetComponent<PleyerController>().isOver)
        {
            transform.position = Vector2.MoveTowards(transform.position, Ship.transform.position, Time.deltaTime * speed);
            pipMineTimer -= Time.deltaTime;
            if (pipMineTimer <= 0)
            {
                sm.PlaySound(4);
                pipMineTimer = pipMineTimerStart;
            }
        }
        
        if (Vector2.Distance(transform.position, Ship.transform.position) <= 0.7f && !Ship.GetComponent<PleyerController>().isOver)
        {
            Ship.GetComponent<PleyerController>().Damage(mineDamage);
            sm.PlaySound(0);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("playerBull"))
        {
            nHP--;
            Destroy(coll.gameObject);
            if (nHP <= 0)
            {
                sm.PlaySound(0);
                Destroy(gameObject);
                Ship.GetComponent<PleyerController>().AdSc(35);
                Destroy(coll.gameObject);
            }            
        }
    }
}
