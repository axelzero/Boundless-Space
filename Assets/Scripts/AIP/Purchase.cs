using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudOnce;

public class Purchase : MonoBehaviour
{
    public void BuyCompleteHelpDevNEW(UnityEngine.Purchasing.Product product)
    {
        Debug.Log("PURCHASE WELL");
        PlayerPrefs.SetInt("help", 1);
        MainMenu.mainMenu.HelpAd.SetActive(true);
        Achievements.Benefactor.Unlock();
        //AchivsCommands.GetTheAchiv(Achivs.Benefactor);
    }

    public void BuyFailedHelpDevNEW(UnityEngine.Purchasing.Product product, UnityEngine.Purchasing.PurchaseFailureReason failureReason)
    {
        Debug.Log("PURCHASE FAILED");
    }


    public void BuyCompleteNoAds(UnityEngine.Purchasing.Product product)
    {
        Debug.Log("PURCHASE WELL");
        PlayerPrefs.SetInt("noads", 1);
        //AchivsCommands.GetTheAchiv(Achivs.NoAd);
        Achievements.NoAds.Unlock();
        MainMenu.mainMenu.btnAd.transform.position = MainMenu.mainMenu.posBtnMoney;
        //MainMenu.mainMenu.btnAd.SetActive(false); //problems
    }

    public void BuyFailedNoAds(UnityEngine.Purchasing.Product product, UnityEngine.Purchasing.PurchaseFailureReason failureReason)
    {
        Debug.Log("PURCHASE FAILED");
    }



    public void BuyCompleteGetCoinsNew(UnityEngine.Purchasing.Product product)
    {
        Debug.Log("PURCHASE WELL");
        PlayerPrefs.SetInt("GetCoins", 1);
        int coin = PlayerPrefs.GetInt("coins") + 20000;
        PlayerPrefs.SetInt("coins", coin);
        MainMenu.mainMenu.txtCoins.text = coin.ToString();
        Achievements.Get20000coins.Unlock();
        CloudVariables.Coins += 20000;
        Cloud.Storage.Save();
        // AchivsCommands.GetTheAchiv(Achivs.Get20000Coins);
    }

    public void BuyFailedGetCoinsNew(UnityEngine.Purchasing.Product product, UnityEngine.Purchasing.PurchaseFailureReason failureReason)
    {
        Debug.Log("PURCHASE FAILED");
    }

    public void BuyCompleteSuperShip(UnityEngine.Purchasing.Product product)
    {
        MainMenu.mainMenu.btnHangar[2].transform.position = MainMenu.mainMenu.posBtnMoney;
        PlayerPrefs.SetInt("Ship4", 1);  //сохраняем, что самолет 4 куплен
        CloudVariables.Supership = 1;
        Cloud.Storage.Save();
        Debug.Log(CloudVariables.Supership + "!!!!!!!!!!!!!!!!!!!!!!!");
        MainMenu.mainMenu.shipUnlock[3] = true;
        MainMenu.mainMenu.ChangeShip(3);
        MainMenu.mainMenu.dollar.SetActive(false);
        MainMenu.mainMenu.txtPrices[3].text = "FREE";
        MainMenu.mainMenu.WeaponSpeedUp[3].SetActive(true);
        MainMenu.mainMenu.WeaponSpeedUp[7].SetActive(true);
        MainMenu.mainMenu.WeaponSpeedUp[11].SetActive(true);
        MainMenu.mainMenu.WeaponSpeedUp[15].SetActive(true);
        Achievements.Thirdship.Unlock();
        //AchivsCommands.GetTheAchiv(Achivs.ThirdShip);
    }

    public void BuyFailedSuperShip(UnityEngine.Purchasing.Product product, UnityEngine.Purchasing.PurchaseFailureReason failureReason)
    {
        Debug.Log("PURCHASE FAILED");
    }


    
}
