using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRocket : MonoBehaviour {

    public PleyerController Ship;
    public SoundManager sm;

    void Start()
    {
        Ship = GameObject.Find("Player").GetComponent<PleyerController>();
        sm = GameObject.Find("GameZone").GetComponent<SoundManager>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            Ship.rocketCount++;
            Destroy(gameObject);
            sm.PlaySound(6);
        }
    }
}
