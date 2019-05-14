using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLife : MonoBehaviour {

    public PleyerController Ship;
    public SoundManager sm;

    void Start ()
    {
        Ship = GameObject.Find("Player").GetComponent<PleyerController>();
        sm = GameObject.Find("GameZone").GetComponent<SoundManager>();
    }
	
	void OnCollisionEnter2D (Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            if (Ship.lifePoints < Ship.startLifePoints)
            {
                Ship.lifePoints += 1;
                Ship.sliderHP.value = Ship.lifePoints;
            }
            else
            {
                Ship.lifePoints = Ship.startLifePoints;
            }
            sm.PlaySound(6);
            //Ship.ChangeLife();
            Destroy(gameObject);
        }
	}
}
