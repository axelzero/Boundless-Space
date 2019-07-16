using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {

    public GameObject[] bonuses;
    public int lifePoints = 5;

    private bool isDead = false;

    public SoundManager sm;

    void Start()
    {
        sm = GameObject.Find("GameZone").GetComponent<SoundManager>();
    }

    private void Update()
    {
        if (lifePoints <= 0 && !isDead)
        {
            Boom();
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit && hit.collider.tag == "Bonus")
            {
                hit.collider.GetComponent<Bonus>().lifePoints--;
            }
        }
    }

    void Boom ()
    {
        isDead = true;
        GameObject bonus = bonuses[Random.Range(0, bonuses.Length)];
        GameObject b = Instantiate(bonus, transform.position, Quaternion.identity) as GameObject;
        b.transform.SetParent(GameObject.Find("Bonuses").transform);
        sm.PlaySound(0);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("playerBull"))
        {
            lifePoints--;
            sm.PlaySound(1);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("shipR"))
        {
            lifePoints = 0;
            Destroy(collision.gameObject);
        }
    }
}
