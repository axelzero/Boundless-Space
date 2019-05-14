using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBullet : MonoBehaviour {
    public SoundManager sm;
    private PleyerController ship;

    private void Start()
    {
        sm = GameObject.Find("GameZone").GetComponent<SoundManager>();
        ship = GameObject.Find("Player").GetComponent<PleyerController>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("enemyBull"))
        {
            Destroy(coll.gameObject);
            sm.PlaySound(1);
            ship.AdSc(5);
            Destroy(gameObject);
        }
    }
}
