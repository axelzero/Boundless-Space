using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [HideInInspector]
    public static EnemyGenerator enemyGenerator;

    public GameObject[] enemys;

    public GameObject bonus;

    public float minDelay;
    public float maxDelay;

    public float minYPos;
    public float maxYPos;

    public GameObject placeToWhereGenerate;

    void Start ()
    {
        enemyGenerator = this;
        StartCoroutine(Spawn());
	}
	
	void Repeat()
    {
        StartCoroutine(Spawn());

    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        Vector2 pos = new Vector2(transform.position.x, Random.Range(minYPos, maxYPos));
        GameObject e = Instantiate(enemys[Random.Range(0, enemys.Length)], pos, Quaternion.identity) as GameObject;

        e.transform.SetParent(placeToWhereGenerate.transform);

        int r = Random.Range(0, 100);
        if (r <= 10)
        {
            Vector2 Bonus_pos = new Vector2(transform.position.x, Random.Range(minYPos, maxYPos));
            GameObject b = Instantiate(bonus, Bonus_pos, Quaternion.identity) as GameObject;
            b.transform.SetParent(GameObject.Find("Bonuses").transform);
        }

        Repeat();
    }
}
