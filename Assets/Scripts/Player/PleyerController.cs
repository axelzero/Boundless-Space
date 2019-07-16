using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CloudOnce;

public class PleyerController : MonoBehaviour
{

    public GameObject bullet;
    public GameObject rocket;


    public int rocketCount;
    public int lifePoints;
    [HideInInspector]
    public int startLifePoints; //для аптечьки
    public int shield;
    public float timeReload;
    private int maxPower;
    private float nTimerShield = 0;

    public Text txtRocketCount;

    public float shootDelay;


    public Image[] imgLifePoints;
    public Color[] colorLife;

    public float speed;

    public Transform[] shootPoints;
    public Transform[] shootPointsShip3;
    public Transform[] shootPointsShip4;

    public bool isFire;
    public bool isReadyToShoot;

    public int bullDamage;

    public SoundManager sm;

    public int coinsCount;
    public Text coinCountTxt;

    public int scoreCount;
    public Text txtScoreCount;
    public Sprite[] ships;

    public bool isOver;

    public GameObject gameOverPanel;

    public ParticleSystem deadFX;

    public SimpleAds Ad;
    public int deadCount = 0;

    [Space(20)]
    [Header("HP & Shield & Weapon UI")]
    public Slider sliderHP;
    public Slider sliderShield;
    public Slider sliderWeapon;
    public int WeaponMaxValue = 50;
    private float nTimerWeaponReload;
    public float timeReloadWeapon;
    private float nTimeToStartReload;

    private float timer = 0f;
    private bool isPlayed;

    private void Start()
    {
        deadCount = PlayerPrefs.GetInt("DC", 0);

        if (deadCount >= 4)
        {
            PlayerPrefs.SetInt("DC", 0);
        }

        int shipNum = PlayerPrefs.GetInt("ship");
        GetComponent<SpriteRenderer>().sprite = ships[shipNum];
        isReadyToShoot = true;
        isFire = false;
        coinsCount = PlayerPrefs.GetInt("coins", 0);

        switch (PlayerPrefs.GetInt("ship"))
        {
            case 0:
                StartOptionsShip(PlayerPrefs.GetInt("Ship1_HPValue"), PlayerPrefs.GetInt("Ship1_ShieldValue"), PlayerPrefs.GetInt("Ship1_RocketValue"), 3.5f, 40, 0.1f);
                //
                shootDelay = PlayerPrefs.GetFloat("Ship1_ShootDelay");

                //
                break;
            case 1:
                StartOptionsShip(PlayerPrefs.GetInt("Ship2_HPValue"), PlayerPrefs.GetInt("Ship2_ShieldValue"), PlayerPrefs.GetInt("Ship2_RocketValue"), 4f, 40, 0.1f);
                shootDelay = PlayerPrefs.GetFloat("Ship2_ShootDelay");
                break;
            case 2:
                StartOptionsShip(PlayerPrefs.GetInt("Ship3_HPValue"), PlayerPrefs.GetInt("Ship3_ShieldValue"), PlayerPrefs.GetInt("Ship3_RocketValue"), 5f, 40, 0.2f);
                shootDelay = PlayerPrefs.GetFloat("Ship3_ShootDelay");
                break;
            case 3:
                StartOptionsShip(PlayerPrefs.GetInt("Ship4_HPValue"), PlayerPrefs.GetInt("Ship4_ShieldValue"), PlayerPrefs.GetInt("Ship4_RocketValue"), 3.5f, 40, 0.2f);
                shootDelay = PlayerPrefs.GetFloat("Ship4_ShootDelay");
                break;
        }
        startLifePoints = lifePoints;
        sliderWeapon.maxValue = WeaponMaxValue;
        sliderWeapon.value = WeaponMaxValue;

        Debug.Log(" HP:" + startLifePoints + " Shield:" + shield + " Shoot Deley:" + shootDelay);
    }

    private void StartOptionsShip(int lifeVal, int shieldVal, int rocketVal, float shieldRealoadVal, int weaponMaxValue, float timeReloadWeapon)
    {
        lifePoints = lifeVal;
        shield = shieldVal;
        rocketCount = rocketVal;
        maxPower = shield;
        WeaponMaxValue = weaponMaxValue;
        this.timeReloadWeapon = timeReloadWeapon;
        if (shieldRealoadVal > 0f) timeReload = shieldRealoadVal;

        sliderHP.maxValue = lifePoints;
        sliderHP.value = lifePoints;
        sliderShield.maxValue = shield;
        sliderShield.value = shield;
    }

    public void Move(Vector2 dir)
    {
        transform.Translate(dir * Time.deltaTime * speed);
    }

    private void Update()
    {
        coinCountTxt.text = coinsCount.ToString();
        txtRocketCount.text = rocketCount.ToString();
        txtScoreCount.text = scoreCount.ToString();

        if (isFire && isReadyToShoot)
        {
            Shoot();
        }

        if (lifePoints <= 0 && !isOver)
        {
            GameOver();
        }

        if (shield < maxPower && shield >= 0)
        {
            ShieldReload(maxPower);
        }
        StartTimeToReload();
        ReloadWeapon();

        if (Root.rootGame.GameState == Root.Game.Dead && !isPlayed)
        {
            timer += Time.deltaTime;
            if (timer >= 2f)
            {
                sm.PlaySound(7, 1f);
                isPlayed = true;
            }
        }
    }

    private void ReloadWeapon()
    {
        if (sliderWeapon.value < sliderWeapon.maxValue && nTimeToStartReload >= 1f && Root.rootGame.GameState != Root.Game.Dead)
        {
            nTimerWeaponReload += Time.deltaTime;

            if (nTimerWeaponReload >= timeReloadWeapon)
            {
                sliderWeapon.value++;
                nTimerWeaponReload = 0f;
            }
        }
    }

    void GameOver()
    {
        Root.rootGame.GameState = Root.Game.Dead;
        sm.PlaySound(8, 1f);
        sliderWeapon.value = 0;
        deadCount++;
        isOver = true;
        Save();
        deadFX.Play();
        Hide();
        gameOverPanel.SetActive(true);

        if (deadCount >= 3)
        {
            Ad.ShowAd();
        }

        Achievements.Firstdeath.Unlock();
    }

    void Hide()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void Shoot()
    {
        if (sliderWeapon.value > 0)
        {
            if (PlayerPrefs.GetInt("ship") == 2)
            {
                foreach (Transform sp in shootPointsShip3)
                {
                    GameObject b = Instantiate(bullet, sp.position, Quaternion.identity) as GameObject;
                    b.transform.SetParent(GameObject.Find("PlayerBullets").transform);
                    Destroy(b, 4f);
                    if (sp == shootPoints[shootPoints.Length - 1])
                    {
                        StartCoroutine(ShootDelay());
                    }
                }
                sm.PlaySound(3);
            }
            else if (PlayerPrefs.GetInt("ship") == 3)
            {
                foreach (Transform sp in shootPointsShip4)
                {
                    GameObject b = Instantiate(bullet, sp.position, Quaternion.identity) as GameObject;
                    b.transform.SetParent(GameObject.Find("PlayerBullets").transform);
                    Destroy(b, 4f);
                    if (sp == shootPoints[shootPoints.Length - 1])
                    {
                        StartCoroutine(ShootDelay());
                    }
                }
                sm.PlaySound(3);
            }
            else
            {
                foreach (Transform sp in shootPoints)
                {
                    GameObject b = Instantiate(bullet, sp.position, Quaternion.identity) as GameObject;
                    b.transform.SetParent(GameObject.Find("PlayerBullets").transform);
                    Destroy(b, 4f);
                    if (sp == shootPoints[shootPoints.Length - 1])
                    {
                        StartCoroutine(ShootDelay());
                    }
                }
                sm.PlaySound(3);
            }
            sliderWeapon.value--;
            nTimeToStartReload = 0f;
        }
    }

    private void StartTimeToReload()
    {
        if (nTimeToStartReload < 2f)
        {
            nTimeToStartReload += Time.deltaTime;
        }
    }

    public void RocketShoot()
    {
        if (rocketCount > 0)
        {
            GameObject r = Instantiate(rocket, transform.position, Quaternion.identity) as GameObject;
            r.transform.SetParent(GameObject.Find("PlayerBullets").transform);
            rocketCount--;
            sm.PlaySound(2);
        }
    }

    IEnumerator ShootDelay()
    {
        isReadyToShoot = false;
        yield return new WaitForSeconds(shootDelay);
        isReadyToShoot = true;
    }


    public void Fire(bool fire)
    {
        isFire = fire;
    }

    public void Damage(int dmg)
    {
        int raznitsa = shield - dmg;
        shield -= dmg;
        if (shield <= 0) shield = 0;
        sliderShield.value = shield;

        if (shield == 0)
        {
            lifePoints += raznitsa;
            sliderHP.value = lifePoints;
        }
        Debug.Log(shield + " " + lifePoints);
    }

    public void AdSc(int scoreToAdd)
    {
        scoreCount += scoreToAdd;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("enemyBull"))
        {
            Damage(1);
            sm.PlaySound(1);
            Destroy(col.gameObject);
        }

        if (col.gameObject.CompareTag("Coin"))
        {
            coinsCount++;
            CloudVariables.Coins++;
            sm.PlaySound(6);
            Destroy(col.gameObject);
        }

        if (col.gameObject.CompareTag("Enemy"))
        {
            Damage(2);
            sm.PlaySound(0);
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("ExBoss"))
        {
            Damage(5);
            sm.PlaySound(0);
            Destroy(col.gameObject);
        }
    }

    void Save()
    {
        PlayerPrefs.SetInt("coins", coinsCount);

        PlayerPrefs.SetInt("HS", scoreCount);
        CloudVariables.HighScore = PlayerPrefs.GetInt("HS");
        Cloud.Storage.Save();
        PlayerPrefs.SetInt("DC", deadCount);
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void ShieldReload(int maxPower)
    {
        if (this.maxPower != 0 && !isOver)
        {
            nTimerShield += Time.deltaTime;

            if (nTimerShield >= timeReload && shield < maxPower)
            {
                shield++;
                sliderShield.value = shield;
                nTimerShield = 0;
            }
        }
    }
}