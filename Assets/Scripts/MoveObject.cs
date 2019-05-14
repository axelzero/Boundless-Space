using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{


    public float speed;
    public Vector2 moveDir;

    public GameObject fx;

    public PleyerController Ship;
    public bool isOutOfScreen = false;

    public PauseMenu pauseMenu;

    private bool isExit;

    private void Start()
    {
        if (gameObject.tag == "Enemy")
        {
            speed = PlayerPrefs.GetFloat("speedEnemy");
        }
        else if (gameObject.tag == "enemyBull")
        {
            speed = 4f + PlayerPrefs.GetFloat("speedEnemy");
        }
        try
        {
            Ship = GameObject.Find("Player").GetComponent<PleyerController>();
            pauseMenu = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();
        }
        catch (System.Exception)
        {

        }
    }

    void Update()
    {
        transform.Translate(moveDir * Time.deltaTime * speed);
    }

    private void OnApplicationQuit()
    {
        isExit = true;
    }

    private void OnDestroy()
    {
        if (Ship != null  && Root.rootGame.GameState != Root.Game.Dead && Root.rootGame.GameState != Root.Game.Pause)
        {
            try
            {
                if (!isOutOfScreen)
                {
                    if (!isExit && Time.timeScale == 1 && !Ship.isOver && pauseMenu == null)
                    {
                        GameObject p = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
                        p.GetComponent<ParticleSystem>().Play();
                        p.transform.SetParent(GameObject.Find("FX").transform);
                        Destroy(p, p.GetComponent<ParticleSystem>().main.duration);
                    }
                    else if (!isExit && Time.timeScale == 1 && !Ship.isOver && pauseMenu.Pause == false)
                    {
                        GameObject p = Instantiate(fx, transform.position, Quaternion.identity) as GameObject;
                        p.GetComponent<ParticleSystem>().Play();
                        p.transform.SetParent(GameObject.Find("FX").transform);
                        Destroy(p, p.GetComponent<ParticleSystem>().main.duration);
                    }
                }
            }
            catch (System.Exception)
            {
                
            }
            
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("destroyer"))
        {
            isOutOfScreen = true;
            Destroy(gameObject);
        }
    }
}
