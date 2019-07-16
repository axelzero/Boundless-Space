using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatedBossesGeneration : MonoBehaviour
{
    public List<GameObject> smallBosses;

    [HideInInspector]
    public static DefeatedBossesGeneration defeatedBossesGeneration;
    public int countExBoss = 0;

    public float minDelay;
    public float maxDelay;

    public float minYPos;
    public float maxYPos;

    public GameObject placeToWhereGenerate;
    public GameObject generatorToActive;

    void Start()
    {
        defeatedBossesGeneration = this;

        StartCoroutine(Spawn());
    }

    void Repeat()
    {
        StartCoroutine(Spawn());

    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        if (countExBoss >= 1)
        {
            Vector2 pos = new Vector2(transform.position.x, Random.Range(minYPos, maxYPos));
            GameObject e = Instantiate(smallBosses[Random.Range(0, countExBoss)], pos, Quaternion.identity) as GameObject;
            e.transform.SetParent(placeToWhereGenerate.transform);
        }
        Repeat();
    }
}
