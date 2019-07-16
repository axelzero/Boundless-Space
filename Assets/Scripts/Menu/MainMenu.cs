using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CloudOnce;

public class MainMenu : MonoBehaviour
{
    public GameObject Login;

    public int hs;
    [HideInInspector]
    public int coins;
    public int shipNum;
    public Image[] Selector;
    public Color[] colors;

    public Text txtCoins;
    public Text txtHs;
    public bool[] shipUnlock;

    public GameObject[] gamePanels;

    public bool sound;
    public Color[] soundColor;
    public Text soundBtn;

    public Sprite[] ships;
    public Image PlayerAtStartMenu;

    public GameObject[] btnHangar; //кнопки выйти и купить
    public int[] intPrices = new int[3] {0 ,500, 1500 };
    public Text[] txtPrices;
    public GameObject[] WeaponSpeedUp;
    [Space(30)]
    public GameObject btnAd;

    [Space(20)]
    [Header("Animation")]
    public Animator anipStoryOrFastG;
    public Animator animPlayer;


    public GameObject dollar;
    private int countPress = 0;

    [HideInInspector]
    public Vector3 posBtnMoney;

    // public GameObject btnNoAds;

    [HideInInspector]
    public static MainMenu mainMenu;

    [Space(20)]
    [Header("Hangar Weapon")]
    public Text[] costOfWeaponSpeedTxt;
    public GameObject[] coinGoHangar;

    [Header("Hangar Shield")]
    public Text[] costOfShieldPrice;
    public Text[] txtShieldValHangar;
    public GameObject[] coinShieldGoHangar;

    [Header("Hangar HP")]
    public Text[] costOfHPPrice;
    public Text[] txtHPValHangar;
    public GameObject[] coinHPGoHangar;

    [Header("Hangar Rocket")]
    public Text[] costOfRocket;
    public Text[] txtRocketValHangar;
    public GameObject[] coinRocketGoHangar;

    private int numShip = -1;
    private int numShip2 = -1;
    private int numShip3 = -1;
    [Space(20)]
    public Animator animator;

    public GameObject HelpAd;

    private bool isSuperShipChaked = false;


    [Space(20)]
    [Header("LogIn")]
    public GameObject logInPanel;
    public GameObject errorMes;

    public void CloudOnceInitializeComplete()
    {
        Cloud.OnInitializeComplete -= CloudOnceInitializeComplete;
        Debug.LogWarning("Initialized");
        //load data
        Cloud.Storage.Load();
        
    }

    void CloudOnceLoadComplete(bool success)
    {
        UpdateUI();
        PlayerPrefs.SetInt("coins", CloudVariables.Coins);
        PlayerPrefs.SetInt("HS", CloudVariables.HighScore);
        PlayerPrefs.SetInt("Ship4", CloudVariables.Supership);
        logInPanel.SetActive(false);
    }

    private void UpdateUI()
    {
        try
        {
            txtCoins.text = CloudVariables.Coins.ToString();
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex);
        }
        
        txtHs.text = "HIGHT SCORE: " + CloudVariables.HighScore.ToString();
    }

    private void Save()
    {
        Cloud.Storage.Save();
    }

    public void BtnContinueErrorLogIn()
    {
        logInPanel.SetActive(false);
    }

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        Login.SetActive(true);
    }
    void Start()
    {
        try
        {
            Cloud.OnInitializeComplete += CloudOnceInitializeComplete;
            Cloud.OnCloudLoadComplete += CloudOnceLoadComplete;
            Cloud.Initialize(true, true);
            Leaderboards.HightScore.SubmitScore(PlayerPrefs.GetInt("HS"));
            Leaderboards.Richestman.SubmitScore(PlayerPrefs.GetInt("coins"));
        }
        catch (System.Exception ex)
        {
            errorMes.SetActive(true);
            Debug.Log("Login error: " + ex);
        }

        txtCoins.text = PlayerPrefs.GetInt("coins").ToString();
        hs = PlayerPrefs.GetInt("HS", 0);
        txtHs.text = "HIGHT SCORE: " + hs.ToString();

        if (Cloud.IsSignedIn == true)
        {
            logInPanel.SetActive(false);
        }
        posBtnMoney = btnHangar[2].transform.position;

        mainMenu = this;

        StartCheakAd();

        shipNum = PlayerPrefs.GetInt("ship", 0);

        Time.timeScale = 1;
        if (AudioListener.volume == 0)
        {
            soundBtn.color = soundColor[1];
            sound = false;
            WeaponSpeedUp[0].SetActive(true);
            WeaponSpeedUp[4].SetActive(true);
            WeaponSpeedUp[8].SetActive(true);
            WeaponSpeedUp[12].SetActive(true);
        }
        else
        {
            soundBtn.color = soundColor[0];
            sound = true;
        }

        
        //PlayerPrefs.SetInt("coins", 20000);////////////////////////////////////////////////////////////////////////////////////////////////////
        coins = PlayerPrefs.GetInt("coins");

        if (PlayerPrefs.GetInt("Ship2") == 1)
        {
            shipUnlock[1] = true;
            WeaponSpeedUp[1].SetActive(true);
            WeaponSpeedUp[5].SetActive(true);
            WeaponSpeedUp[9].SetActive(true);
            WeaponSpeedUp[13].SetActive(true);
        }
        else
        {
            shipUnlock[1] = false;
            WeaponSpeedUp[1].SetActive(false);
            WeaponSpeedUp[5].SetActive(false);
            WeaponSpeedUp[9].SetActive(false);
            WeaponSpeedUp[13].SetActive(false);
        }

        if (PlayerPrefs.GetInt("Ship3") == 1)
        {
            shipUnlock[2] = true;
            WeaponSpeedUp[2].SetActive(true);
            WeaponSpeedUp[6].SetActive(true);
            WeaponSpeedUp[10].SetActive(true);
            WeaponSpeedUp[14].SetActive(true);
        }
        else
        {
            shipUnlock[2] = false;
            WeaponSpeedUp[2].SetActive(false);
            WeaponSpeedUp[6].SetActive(false);
            WeaponSpeedUp[10].SetActive(false);
            WeaponSpeedUp[14].SetActive(false);
        }


        if (PlayerPrefs.GetInt("Ship4") == 1)
        {
            shipUnlock[3] = true;
            dollar.SetActive(false);
            WeaponSpeedUp[3].SetActive(true);
            WeaponSpeedUp[7].SetActive(true);
            WeaponSpeedUp[11].SetActive(true);
            WeaponSpeedUp[15].SetActive(true);
            btnHangar[2].SetActive(false);
        }
        else
        {
            shipUnlock[3] = false;
            WeaponSpeedUp[3].SetActive(false);
            WeaponSpeedUp[7].SetActive(false);
            WeaponSpeedUp[11].SetActive(false);
            WeaponSpeedUp[15].SetActive(false);
        }
        ChangeShip(0);

        //Запись цен
        txtPrices[1].text = intPrices[1].ToString();
        txtPrices[2].text = intPrices[2].ToString();

        StartUpdate();

        if (PlayerPrefs.GetInt("noads") == 1)
        {
            btnAd.SetActive(false);
        }

        if (PlayerPrefs.GetInt("BoolOnce") != 1)
        {
            //-----------------------------------------------------//
            PlayerPrefs.SetInt("Ship1_WeaponSpeed",100);
            PlayerPrefs.SetInt("Ship2_WeaponSpeed",100);
            PlayerPrefs.SetInt("Ship3_WeaponSpeed",100);
            PlayerPrefs.SetInt("Ship4_WeaponSpeed",100);  //Price weapon

            PlayerPrefs.SetFloat("Ship1_ShootDelay", 0.35f);
            PlayerPrefs.SetFloat("Ship2_ShootDelay", 0.55f);
            PlayerPrefs.SetFloat("Ship3_ShootDelay", 0.55f);
            PlayerPrefs.SetFloat("Ship4_ShootDelay", 0.55f);  //Start value
            //------------------------------------------------------//

            //------------------------------------------------------//
            PlayerPrefs.SetInt("Ship1_ShieldPrice", 100);
            PlayerPrefs.SetInt("Ship2_ShieldPrice", 100);
            PlayerPrefs.SetInt("Ship3_ShieldPrice", 100);
            PlayerPrefs.SetInt("Ship4_ShieldPrice", 100);  //Price Shield

            PlayerPrefs.SetInt("Ship1_ShieldValue", 1);
            PlayerPrefs.SetInt("Ship2_ShieldValue", 2);
            PlayerPrefs.SetInt("Ship3_ShieldValue", 3);
            PlayerPrefs.SetInt("Ship4_ShieldValue", 5);  //Start value
            //------------------------------------------------------//

            //------------------------------------------------------//
            PlayerPrefs.SetInt("Ship1_HPPrice", 100);
            PlayerPrefs.SetInt("Ship2_HPPrice", 100);
            PlayerPrefs.SetInt("Ship3_HPPrice", 100);
            PlayerPrefs.SetInt("Ship4_HPPrice", 100);  //Price HP

            PlayerPrefs.SetInt("Ship1_HPValue", 5);
            PlayerPrefs.SetInt("Ship2_HPValue", 7);
            PlayerPrefs.SetInt("Ship3_HPValue", 10);
            PlayerPrefs.SetInt("Ship4_HPValue", 15);  //Start value
            //------------------------------------------------------//

            //------------------------------------------------------//
            PlayerPrefs.SetInt("Ship1_RocketPrice", 100);
            PlayerPrefs.SetInt("Ship2_RocketPrice", 100);
            PlayerPrefs.SetInt("Ship3_RocketPrice", 100);
            PlayerPrefs.SetInt("Ship4_RocketPrice", 100);  //Price Rocket

            PlayerPrefs.SetInt("Ship1_RocketValue", 5);
            PlayerPrefs.SetInt("Ship2_RocketValue", 10);
            PlayerPrefs.SetInt("Ship3_RocketValue", 15);
            PlayerPrefs.SetInt("Ship4_RocketValue", 20);  //Start value
            //------------------------------------------------------//


            PlayerPrefs.SetFloat("shootDelay", 2f); // Value for enemys
            PlayerPrefs.SetInt("lifePoints", 1);
            PlayerPrefs.SetFloat("speedEnemy", 2f);
            PlayerPrefs.SetFloat("speedEnemyBullet", 4f);

            PlayerPrefs.SetInt("BoolOnce", 1);
        }

        costOfWeaponSpeedTxt[0].text = PlayerPrefs.GetInt("Ship1_WeaponSpeed").ToString();
        costOfWeaponSpeedTxt[1].text = PlayerPrefs.GetInt("Ship2_WeaponSpeed").ToString();
        costOfWeaponSpeedTxt[2].text = PlayerPrefs.GetInt("Ship3_WeaponSpeed").ToString();
        costOfWeaponSpeedTxt[3].text = PlayerPrefs.GetInt("Ship4_WeaponSpeed").ToString();


        costOfShieldPrice[0].text = PlayerPrefs.GetInt("Ship1_ShieldPrice").ToString();
        costOfShieldPrice[1].text = PlayerPrefs.GetInt("Ship2_ShieldPrice").ToString();
        costOfShieldPrice[2].text = PlayerPrefs.GetInt("Ship3_ShieldPrice").ToString();
        costOfShieldPrice[3].text = PlayerPrefs.GetInt("Ship4_ShieldPrice").ToString();


        costOfHPPrice[0].text = PlayerPrefs.GetInt("Ship1_HPPrice").ToString();
        costOfHPPrice[1].text = PlayerPrefs.GetInt("Ship2_HPPrice").ToString();
        costOfHPPrice[2].text = PlayerPrefs.GetInt("Ship3_HPPrice").ToString();
        costOfHPPrice[3].text = PlayerPrefs.GetInt("Ship4_HPPrice").ToString();


        costOfRocket[0].text = PlayerPrefs.GetInt("Ship1_RocketPrice").ToString();
        costOfRocket[1].text = PlayerPrefs.GetInt("Ship2_RocketPrice").ToString();
        costOfRocket[2].text = PlayerPrefs.GetInt("Ship3_RocketPrice").ToString();
        costOfRocket[3].text = PlayerPrefs.GetInt("Ship4_RocketPrice").ToString();

        CheakOnMaxValueSpeedWeapon();

        RateApp();
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("Ship4") == 1 && isSuperShipChaked == false)
        {
            shipUnlock[3] = true;
            btnHangar[2].transform.position = new Vector2(-3, -203);
            dollar.SetActive(false);
            txtPrices[3].text = "FREE";
            WeaponSpeedUp[3].SetActive(true);
            WeaponSpeedUp[7].SetActive(true);
            WeaponSpeedUp[11].SetActive(true);
            WeaponSpeedUp[15].SetActive(true);
            isSuperShipChaked = true;
            btnHangar[2].SetActive(false);
        }
    }

    private void CheakOnMaxValueSpeedWeapon()  // Ограничения по покупкам
    {
        if (PlayerPrefs.GetInt("Ship1_WeaponSpeed") >= 600)
        {
            costOfWeaponSpeedTxt[0].text = "MAX";
            coinGoHangar[0].SetActive(false);
        }

        if (PlayerPrefs.GetInt("Ship2_WeaponSpeed") >= 600)
        {
            costOfWeaponSpeedTxt[1].text = "MAX";
            coinGoHangar[1].SetActive(false);
        }

        if (PlayerPrefs.GetInt("Ship3_WeaponSpeed") >= 600)
        {
            costOfWeaponSpeedTxt[2].text = "MAX";
            coinGoHangar[2].SetActive(false);
        }

        if (PlayerPrefs.GetInt("Ship4_WeaponSpeed") >= 600)
        {
            costOfWeaponSpeedTxt[3].text = "MAX";
            coinGoHangar[3].SetActive(false);
        }



        if (PlayerPrefs.GetInt("Ship1_ShieldPrice") >= 600) // Shield
        {
            costOfShieldPrice[0].text = "MAX";
            
            coinShieldGoHangar[0].SetActive(false);
        }
        txtShieldValHangar[0].text = PlayerPrefs.GetInt("Ship1_ShieldValue").ToString();

        if (PlayerPrefs.GetInt("Ship2_ShieldPrice") >= 600)
        {
            costOfShieldPrice[1].text = "MAX";
            
            coinShieldGoHangar[1].SetActive(false);
        }
        txtShieldValHangar[1].text = PlayerPrefs.GetInt("Ship2_ShieldValue").ToString();
        if (PlayerPrefs.GetInt("Ship3_ShieldPrice") >= 600)
        {
            costOfShieldPrice[2].text = "MAX";
            
            coinShieldGoHangar[2].SetActive(false);
        }
        txtShieldValHangar[2].text = PlayerPrefs.GetInt("Ship3_ShieldValue").ToString();
        if (PlayerPrefs.GetInt("Ship4_ShieldPrice") >= 600)
        {
            costOfShieldPrice[3].text = "MAX";
            
            coinShieldGoHangar[3].SetActive(false);
        }
        txtShieldValHangar[3].text = PlayerPrefs.GetInt("Ship4_ShieldValue").ToString();


        if (PlayerPrefs.GetInt("Ship1_HPPrice") >= 600)  //HP
        {
            costOfHPPrice[0].text = "MAX";

            coinHPGoHangar[0].SetActive(false);
        }
        txtHPValHangar[0].text = PlayerPrefs.GetInt("Ship1_HPValue").ToString();

        if (PlayerPrefs.GetInt("Ship2_HPPrice") >= 600)
        {
            costOfHPPrice[1].text = "MAX";

            coinHPGoHangar[1].SetActive(false);
        }
        txtHPValHangar[1].text = PlayerPrefs.GetInt("Ship2_HPValue").ToString();
        if (PlayerPrefs.GetInt("Ship3_HPPrice") >= 600)
        {
            costOfHPPrice[2].text = "MAX";

            coinHPGoHangar[2].SetActive(false);
        }
        txtHPValHangar[2].text = PlayerPrefs.GetInt("Ship3_HPValue").ToString();
        if (PlayerPrefs.GetInt("Ship4_HPPrice") >= 600)
        {
            costOfHPPrice[3].text = "MAX";

            coinHPGoHangar[3].SetActive(false);
        }
        txtHPValHangar[3].text = PlayerPrefs.GetInt("Ship4_HPValue").ToString();


        if (PlayerPrefs.GetInt("Ship1_RocketPrice") >= 2100)  //ROCKET
        {
            costOfRocket[0].text = "MAX";

            coinRocketGoHangar[0].SetActive(false);
        }
        txtRocketValHangar[0].text = PlayerPrefs.GetInt("Ship1_RocketValue").ToString();

        if (PlayerPrefs.GetInt("Ship2_RocketPrice") >= 2100)
        {
            costOfRocket[1].text = "MAX";

            coinRocketGoHangar[1].SetActive(false);
        }
        txtRocketValHangar[1].text = PlayerPrefs.GetInt("Ship2_RocketValue").ToString();
        if (PlayerPrefs.GetInt("Ship3_RocketPrice") >= 2100)
        {
            costOfRocket[2].text = "MAX";

            coinRocketGoHangar[2].SetActive(false);
        }
        txtRocketValHangar[2].text = PlayerPrefs.GetInt("Ship3_RocketValue").ToString();
        if (PlayerPrefs.GetInt("Ship4_RocketPrice") >= 2100)
        {
            costOfRocket[3].text = "MAX";

            coinRocketGoHangar[3].SetActive(false);
        }
        txtRocketValHangar[3].text = PlayerPrefs.GetInt("Ship4_RocketValue").ToString();
    }

    public void RateApp()
    {
        if (PlayerPrefs.GetInt("rateOff") != -1)
        {
            int countToRate = 1;
            countToRate += PlayerPrefs.GetInt("CountToRate", 0);
            PlayerPrefs.SetInt("CountToRate", countToRate);
            //RateManager rateManager = new RateManager();
            this.GetComponent<RateManager>().ClicPlay();
           // RateManager.rateManager.ClicPlay();
        }
    }

    public void ChangeShip(int num)
    {
        switch (num)
        {
            case 0:
                Selector[0].color = colors[0];
                Selector[1].color = colors[1];
                Selector[2].color = colors[1];
                Selector[3].color = colors[1];
                PlayerAtStartMenu.sprite = ships[num];
                SetBtnActiveInHangar(true, false);

                shipNum = 0;
                break;
            case 1:
                Selector[0].color = colors[1];
                Selector[2].color = colors[1];
                Selector[3].color = colors[1];
                shipNum = 1;
                PlayerAtStartMenu.sprite = ships[num];
                if (shipUnlock[1] == false)
                {
                    Selector[1].color = colors[2];          //Если не куплено - задник другого цвета
                    SetBtnActiveInHangar(false, false);
                }
                else
                {
                    Selector[1].color = colors[0];
                    SetBtnActiveInHangar(true, false);
                }
                break;
            case 2:
                Selector[0].color = colors[1];
                Selector[1].color = colors[1];

                if (shipUnlock[2] == false)
                {
                    Selector[2].color = colors[2];
                    SetBtnActiveInHangar(false, false);
                }
                else
                {
                    Selector[2].color = colors[0];
                    SetBtnActiveInHangar(true, false);
                }
                Selector[3].color = colors[1];
                PlayerAtStartMenu.sprite = ships[num];
                shipNum = 2;
                break;

            case 3:
                Selector[0].color = colors[1];
                Selector[1].color = colors[1];
                Selector[2].color = colors[1];

                if (shipUnlock[3] == false)
                {
                    Selector[3].color = colors[2];
                    SetBtnActiveInHangar(false, true);
                }
                else
                {
                    Selector[3].color = colors[0];
                    SetBtnActiveInHangar(true,false);
                }
                PlayerAtStartMenu.sprite = ships[num];
                shipNum = 3;
                break;
        }
    }

    private void SetBtnActiveInHangar(bool isBack, bool isPayNew)
    {
        if (isBack)
        {
            btnHangar[0].SetActive(true);
            btnHangar[1].SetActive(false);
            btnHangar[2].transform.position = posBtnMoney;
        }
        else
        {
            
            btnHangar[0].SetActive(false);          //Кнопка назад исчезнет
            if (isPayNew)
            {
                btnHangar[1].SetActive(false);
                btnHangar[2].transform.position = btnHangar[0].transform.position;       //Появится кнопку купить платный корабль
            }
            else
            {
                btnHangar[2].transform.position = posBtnMoney;
                btnHangar[1].SetActive(true);           //Появится кнопку купить
            }
            
        }
    }

    public void BtnBuy()
    {
        switch (shipNum)
        {
            case 1:
                if (shipUnlock[1] == false)
                {
                    if (PlayerPrefs.GetInt("coins", 0) >= intPrices[1])
                    {
                        coins = PlayerPrefs.GetInt("coins");
                        coins -= intPrices[1];
                        PlayerPrefs.SetInt("coins", coins);
                        CloudVariables.Coins = coins;
                        shipUnlock[1] = true;
                        PlayerPrefs.SetInt("Ship2", 1);  //сохраняем, что самолет 2 куплен
                        ChangeShip(1);
                        txtPrices[1].text = "FREE";
                        WeaponSpeedUp[1].SetActive(true);
                        WeaponSpeedUp[5].SetActive(true);
                        WeaponSpeedUp[9].SetActive(true);
                        WeaponSpeedUp[13].SetActive(true);
                        Achievements.Firstship.Unlock();
                        //AchivsCommands.GetTheAchiv(Achivs.FirstShip);
                    }
                }
                break;
            case 2:
                if (shipUnlock[2] == false)
                {
                    if (PlayerPrefs.GetInt("coins", 0) >= intPrices[2])
                    {
                        coins = PlayerPrefs.GetInt("coins");
                        coins -= intPrices[2];
                        PlayerPrefs.SetInt("coins", coins);
                        CloudVariables.Coins = coins;
                        shipUnlock[2] = true;
                        PlayerPrefs.SetInt("Ship3", 1);  //сохраняем, что самолет 3 куплен
                        ChangeShip(2);
                        txtPrices[2].text = "FREE";
                        WeaponSpeedUp[2].SetActive(true);
                        WeaponSpeedUp[6].SetActive(true);
                        WeaponSpeedUp[10].SetActive(true);
                        WeaponSpeedUp[14].SetActive(true);
                        Achievements.Secondship.Unlock();
                        //AchivsCommands.GetTheAchiv(Achivs.SecondShip);
                    }
                }
                break;
            case 3:
                if (shipUnlock[3] == false)
                {
                    GetComponent<IapManager>().BuyShip();
                }
                break;
        }
        txtCoins.text = PlayerPrefs.GetInt("coins").ToString();
        hs = PlayerPrefs.GetInt("HS", 0);
        txtHs.text = "HIGHT SCORE: " + hs.ToString();

        Save();
    }

    public void BtnGameMode()
    {
        anipStoryOrFastG.SetBool("GameMode", true);
    }
    public void BtnBackMenu()
    {
        anipStoryOrFastG.SetBool("GameMode", false);
    }

    public void BtnNewGame()   //Запуск анимации, а старт игры в конце анимации. Весит на игровом самолете
    {
        animPlayer.SetBool("StartGame", true);
        PlayerPrefs.SetInt("ship", shipNum);
    }

    public void BtnHungar()
    {
        gamePanels[0].SetActive(false);//Кнопка назад исчезнет
        gamePanels[1].SetActive(true);//Появится кнопку купить
        animator.GetComponent<Animator>().SetBool("isStart", true);
    }

    public void BtnBackHungar()
    {
        gamePanels[0].SetActive(true);
        gamePanels[1].SetActive(false);
    }

    public void SoundBtn()
    {
        if (sound)
        {
            AudioListener.volume = 0;
            soundBtn.color = soundColor[1];
            sound = false;
            return;
        }

        if (!sound)
        {
            AudioListener.volume = 1;
            soundBtn.color = soundColor[0];
            sound = true;
            return;
        }
    }

    public void BtnExit()
    {
        CloudOnce.Cloud.SignOut();
        //PlayGamesPlatform.Instance.SignOut();
        Application.Quit();
    }

    private void StartUpdate()
    {
        PlayerAtStartMenu.sprite = ships[PlayerPrefs.GetInt("ship", 0)];
        PlayerPrefs.SetInt("ship", PlayerPrefs.GetInt("ship", 0));

        ChangeShip(PlayerPrefs.GetInt("ship", 0));

        for (int i = 0; i < shipUnlock.Length; i++)
        {
            if (shipUnlock[i] == true)
            {
                txtPrices[i].text = "FREE";
            }
        }
    }

    public void OpenBestShip()
    {
        countPress++;
        Debug.Log("pressed");
        if (countPress >= 50)
        {
            btnHangar[2].transform.position = posBtnMoney;
            shipUnlock[3] = true;
            PlayerPrefs.SetInt("Ship4", 1);  //сохраняем, что самолет 4 куплен
            CloudVariables.Supership = 1;
            Save();
            ChangeShip(3);
            dollar.SetActive(false);
            txtPrices[3].text = "FREE";
            WeaponSpeedUp[3].SetActive(true);
            WeaponSpeedUp[7].SetActive(true);
            WeaponSpeedUp[11].SetActive(true);
            WeaponSpeedUp[15].SetActive(true);
            Achievements.Thirdship.Unlock();
            txtCoins.text = PlayerPrefs.GetInt("coins").ToString();
            hs = PlayerPrefs.GetInt("HS", 0);
            txtHs.text = "HIGHT SCORE: " + hs.ToString();
        }
    }

    private void StartCheakAd()
    {
        if (PlayerPrefs.GetInt("noads") == 1)
        {
            btnAd.SetActive(false);
        }

        if (PlayerPrefs.GetInt("EnemyKilled") == 10)
        {
            Achievements.Kill10enemys.Unlock();
           // AchivsCommands.GetTheAchiv(GPGSIds.achievement_kill_10_enemys);
        }

        if (PlayerPrefs.GetInt("EnemyKilled") == 50)
        {
            Achievements.Kill50enemys.Unlock();
            //AchivsCommands.GetTheAchiv(GPGSIds.achievement_kill_50_enemys);
        }

        if (PlayerPrefs.GetInt("EnemyKilled") == 100)
        {
            Achievements.Kill100enemys.Unlock();
            //AchivsCommands.GetTheAchiv(GPGSIds.achievement_kill_100_enemys);
        }
    }


    public void BtnWeaponSpeedUp(string nameShipPlayerPrefs)  //Покупка ускорения оружия
    {
        //IF coins > Price
        if (PlayerPrefs.GetInt(nameShipPlayerPrefs) > 599)
        {
            return;
        }

        switch (nameShipPlayerPrefs)
        {
            case "Ship1_WeaponSpeed":
                numShip = 0;
                break;
            case "Ship2_WeaponSpeed":
                numShip = 1;
                break;
            case "Ship3_WeaponSpeed":
                numShip = 2;
                break;
            case "Ship4_WeaponSpeed":
                numShip = 3;
                break;
        }

        if (PlayerPrefs.GetInt("coins") >= PlayerPrefs.GetInt(nameShipPlayerPrefs))
        {
            Debug.Log("Click");
            
            if (nameShipPlayerPrefs == "Ship1_WeaponSpeed")
            {
                // SpeedUp
                float delay = PlayerPrefs.GetFloat("Ship1_ShootDelay");
                delay -= 0.1f;
                if (delay < 0.1f) delay = 0.1f;
                PlayerPrefs.SetFloat("Ship1_ShootDelay", delay);
                // SpeedUp
            }
            else if (nameShipPlayerPrefs == "Ship2_WeaponSpeed")
            {
                // SpeedUp
                float delay = PlayerPrefs.GetFloat("Ship2_ShootDelay");
                delay -= 0.1f;
                if (delay < 0.1f) delay = 0.1f;
                PlayerPrefs.SetFloat("Ship2_ShootDelay", delay);
                // SpeedUp
            }
            else if (nameShipPlayerPrefs == "Ship3_WeaponSpeed")
            {
                // SpeedUp
                float delay = PlayerPrefs.GetFloat("Ship3_ShootDelay");
                delay -= 0.1f;
                if (delay < 0.1f) delay = 0.1f;
                PlayerPrefs.SetFloat("Ship3_ShootDelay", delay);
                // SpeedUp
            }
            else if (nameShipPlayerPrefs == "Ship4_WeaponSpeed")
            {
                // SpeedUp
                float delay = PlayerPrefs.GetFloat("Ship4_ShootDelay");
                delay -= 0.1f;
                if (delay < 0.1f) delay = 0.1f;
                PlayerPrefs.SetFloat("Ship4_ShootDelay", delay);
                // SpeedUp
            }

            int _coins = PlayerPrefs.GetInt("coins");
            _coins -= PlayerPrefs.GetInt(nameShipPlayerPrefs);
            PlayerPrefs.SetInt("coins", _coins);
            CloudVariables.Coins = _coins;
            Save();
            txtCoins.text = coins.ToString();
            PlayerPrefs.SetInt(nameShipPlayerPrefs, PlayerPrefs.GetInt(nameShipPlayerPrefs) + 100);

            costOfWeaponSpeedTxt[numShip].text = PlayerPrefs.GetInt(nameShipPlayerPrefs).ToString();

            if (PlayerPrefs.GetInt(nameShipPlayerPrefs) >= 600)
            {
                costOfWeaponSpeedTxt[numShip].text = "MAX";
                coinGoHangar[numShip].SetActive(false);
            }
        }
        txtCoins.text = PlayerPrefs.GetInt("coins").ToString();
        hs = PlayerPrefs.GetInt("HS", 0);
        txtHs.text = "HIGHT SCORE: " + hs.ToString();
        
    }

    public void BtnShieldUp(string nameShipPlayerPrefs)  //Покупка улучшения щита
    {
        //IF coins > Price
        if (PlayerPrefs.GetInt(nameShipPlayerPrefs) > 599)
        {
            return;
        }

        switch (nameShipPlayerPrefs)
        {
            case "Ship1_ShieldPrice":
                numShip2 = 0;
                break;
            case "Ship2_ShieldPrice":
                numShip2 = 1;
                break;
            case "Ship3_ShieldPrice":
                numShip2 = 2;
                break;
            case "Ship4_ShieldPrice":
                numShip2 = 3;
                break;
        }

        if (PlayerPrefs.GetInt("coins") >= PlayerPrefs.GetInt(nameShipPlayerPrefs))
        {
            Debug.Log("Click");

            if (nameShipPlayerPrefs == "Ship1_ShieldPrice")
            {
                // ShieldUp
                int shield = PlayerPrefs.GetInt("Ship1_ShieldValue");
                shield ++;
                PlayerPrefs.SetInt("Ship1_ShieldValue", shield);
                // ShieldUp
            }
            else if (nameShipPlayerPrefs == "Ship2_ShieldPrice")
            {
                // ShieldUp
                int shield = PlayerPrefs.GetInt("Ship2_ShieldValue");
                shield++;
                PlayerPrefs.SetInt("Ship2_ShieldValue", shield);
                // ShieldUp
            }
            else if (nameShipPlayerPrefs == "Ship3_ShieldPrice")
            {
                // ShieldUp
                int shield = PlayerPrefs.GetInt("Ship3_ShieldValue");
                shield++;
                PlayerPrefs.SetInt("Ship3_ShieldValue", shield);
                // ShieldUp
            }
            else if (nameShipPlayerPrefs == "Ship4_ShieldPrice")
            {
                // ShieldUp
                int shield = PlayerPrefs.GetInt("Ship4_ShieldValue");
                shield++;
                PlayerPrefs.SetInt("Ship4_ShieldValue", shield);
                // ShieldUp
            }

            

            switch (numShip2)
            {
                case 0:
                    txtShieldValHangar[numShip2].text = PlayerPrefs.GetInt("Ship1_ShieldValue").ToString();
                    break;
                case 1:
                    txtShieldValHangar[numShip2].text = PlayerPrefs.GetInt("Ship2_ShieldValue").ToString();
                    break;
                case 2:
                    txtShieldValHangar[numShip2].text = PlayerPrefs.GetInt("Ship3_ShieldValue").ToString();
                    break;
                case 3:
                    txtShieldValHangar[numShip2].text = PlayerPrefs.GetInt("Ship4_ShieldValue").ToString();
                    break;
            }

            int _coins = PlayerPrefs.GetInt("coins");
            _coins -= PlayerPrefs.GetInt(nameShipPlayerPrefs);
            PlayerPrefs.SetInt("coins", _coins);
            CloudVariables.Coins = _coins;
            Save();
            txtCoins.text = _coins.ToString();
            PlayerPrefs.SetInt(nameShipPlayerPrefs, PlayerPrefs.GetInt(nameShipPlayerPrefs) + 100);

            costOfShieldPrice[numShip2].text = PlayerPrefs.GetInt(nameShipPlayerPrefs).ToString();

            if (PlayerPrefs.GetInt(nameShipPlayerPrefs) >= 600)
            {
                costOfShieldPrice[numShip2].text = "MAX";
                coinShieldGoHangar[numShip2].SetActive(false);
            }
        }
        txtCoins.text = PlayerPrefs.GetInt("coins").ToString();
        hs = PlayerPrefs.GetInt("HS", 0);
        txtHs.text = "HIGHT SCORE: " + hs.ToString();
    }


    public void BtnHPUp(string nameShipPlayerPrefs)  //Покупка улучшения HP
    {
        //IF coins > Price
        if (PlayerPrefs.GetInt(nameShipPlayerPrefs) > 599)
        {
            return;
        }

        switch (nameShipPlayerPrefs)
        {
            case "Ship1_HPPrice":
                numShip2 = 0;
                break;
            case "Ship2_HPPrice":
                numShip2 = 1;
                break;
            case "Ship3_HPPrice":
                numShip2 = 2;
                break;
            case "Ship4_HPPrice":
                numShip2 = 3;
                break;
        }

        if (PlayerPrefs.GetInt("coins") >= PlayerPrefs.GetInt(nameShipPlayerPrefs))
        {
            Debug.Log("Click");

            if (nameShipPlayerPrefs == "Ship1_HPPrice")
            {
                // HP
                int hp = PlayerPrefs.GetInt("Ship1_HPValue");
                hp++;
                PlayerPrefs.SetInt("Ship1_HPValue", hp);
                // HP
            }
            else if (nameShipPlayerPrefs == "Ship2_HPPrice")
            {
                // HP
                int hp = PlayerPrefs.GetInt("Ship2_HPValue");
                hp++;
                PlayerPrefs.SetInt("Ship2_HPValue", hp);
                // HP
            }
            else if (nameShipPlayerPrefs == "Ship3_HPPrice")
            {
                // HP
                int hp = PlayerPrefs.GetInt("Ship3_HPValue");
                hp++;
                PlayerPrefs.SetInt("Ship3_HPValue", hp);
                // HP
            }
            else if (nameShipPlayerPrefs == "Ship4_HPPrice")
            {
                // HP
                int hp = PlayerPrefs.GetInt("Ship4_HPValue");
                hp++;
                PlayerPrefs.SetInt("Ship4_HPValue", hp);
                // HP
            }



            switch (numShip2)
            {
                case 0:
                    txtHPValHangar[numShip2].text = PlayerPrefs.GetInt("Ship1_HPValue").ToString();
                    break;
                case 1:
                    txtHPValHangar[numShip2].text = PlayerPrefs.GetInt("Ship2_HPValue").ToString();
                    break;
                case 2:
                    txtHPValHangar[numShip2].text = PlayerPrefs.GetInt("Ship3_HPValue").ToString();
                    break;
                case 3:
                    txtHPValHangar[numShip2].text = PlayerPrefs.GetInt("Ship4_HPValue").ToString();
                    break;
            }

            int _coins = PlayerPrefs.GetInt("coins");
            _coins -= PlayerPrefs.GetInt(nameShipPlayerPrefs);
            PlayerPrefs.SetInt("coins", _coins);
            CloudVariables.Coins = _coins;
            Save();
            txtCoins.text = _coins.ToString();
            PlayerPrefs.SetInt(nameShipPlayerPrefs, PlayerPrefs.GetInt(nameShipPlayerPrefs) + 100);

            costOfHPPrice[numShip2].text = PlayerPrefs.GetInt(nameShipPlayerPrefs).ToString();

            if (PlayerPrefs.GetInt(nameShipPlayerPrefs) >= 600)
            {
                costOfHPPrice[numShip2].text = "MAX";
                coinHPGoHangar[numShip2].SetActive(false);
            }
        }
        txtCoins.text = PlayerPrefs.GetInt("coins").ToString();
        hs = PlayerPrefs.GetInt("HS", 0);
        txtHs.text = "HIGHT SCORE: " + hs.ToString();
    }

    public void BtnRocketUp(string nameShipPlayerPrefs)  //Покупка улучшения Rocket
    {
        //IF coins > Price
        if (PlayerPrefs.GetInt(nameShipPlayerPrefs) > 2099)
        {
            return;
        }

        switch (nameShipPlayerPrefs)
        {
            case "Ship1_RocketPrice":
                numShip3 = 0;
                break;
            case "Ship2_RocketPrice":
                numShip3 = 1;
                break;
            case "Ship3_RocketPrice":
                numShip3 = 2;
                break;
            case "Ship4_RocketPrice":
                numShip3 = 3;
                break;
        }

        if (PlayerPrefs.GetInt("coins") >= PlayerPrefs.GetInt(nameShipPlayerPrefs))
        {
            Debug.Log("Click ROCKET");

            if (nameShipPlayerPrefs == "Ship1_RocketPrice")
            {
                int rocket = PlayerPrefs.GetInt("Ship1_RocketValue");
                rocket++;
                PlayerPrefs.SetInt("Ship1_RocketValue", rocket);
            }
            else if (nameShipPlayerPrefs == "Ship2_RocketPrice")
            {
                int rocket = PlayerPrefs.GetInt("Ship2_RocketValue");
                rocket++;
                PlayerPrefs.SetInt("Ship2_RocketValue", rocket);
            }
            else if (nameShipPlayerPrefs == "Ship3_RocketPrice")
            {
                int rocket = PlayerPrefs.GetInt("Ship3_RocketValue");
                rocket++;
                PlayerPrefs.SetInt("Ship3_RocketValue", rocket);
            }
            else if (nameShipPlayerPrefs == "Ship4_RocketPrice")
            {
                int rocket = PlayerPrefs.GetInt("Ship4_RocketValue");
                rocket++;
                PlayerPrefs.SetInt("Ship4_RocketValue", rocket);
            }



            switch (numShip3)
            {
                case 0:
                    txtRocketValHangar[numShip3].text = PlayerPrefs.GetInt("Ship1_RocketValue").ToString();
                    break;
                case 1:
                    txtRocketValHangar[numShip3].text = PlayerPrefs.GetInt("Ship2_RocketValue").ToString();
                    break;
                case 2:
                    txtRocketValHangar[numShip3].text = PlayerPrefs.GetInt("Ship3_RocketValue").ToString();
                    break;
                case 3:
                    txtRocketValHangar[numShip3].text = PlayerPrefs.GetInt("Ship4_RocketValue").ToString();
                    break;
            }

            int _coins = PlayerPrefs.GetInt("coins");
            _coins -= PlayerPrefs.GetInt(nameShipPlayerPrefs);
            PlayerPrefs.SetInt("coins", _coins);
            CloudVariables.Coins = _coins;
            Save();
            txtCoins.text = _coins.ToString();
            PlayerPrefs.SetInt(nameShipPlayerPrefs, PlayerPrefs.GetInt(nameShipPlayerPrefs) + 100);

            costOfRocket[numShip3].text = PlayerPrefs.GetInt(nameShipPlayerPrefs).ToString();

            if (PlayerPrefs.GetInt(nameShipPlayerPrefs) >= 2100)
            {
                costOfRocket[numShip3].text = "MAX";
                coinRocketGoHangar[numShip3].SetActive(false);
            }
        }
        txtCoins.text = PlayerPrefs.GetInt("coins").ToString();
        hs = PlayerPrefs.GetInt("HS", 0);
        txtHs.text = "HIGHT SCORE: " + hs.ToString();
    }

    public void BtnHelpExit()
    {
        HelpAd.SetActive(false);
    }

    void OnApplicationQuit()
    {
        CloudOnce.Cloud.SignOut();
    }
}