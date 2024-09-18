using System.Collections;
using System.Collections.Generic;
using Locit;
using TMPro;
using UnityEngine;

public class AchivementObject : MonoBehaviour
{
    public AchivementController achivementController;
    public PlayerStats playerStats;

    public string achiveName = "";

    public enum ACHIVE_TYPE
    {
        Gold_Collector, Unstoppable, Customers_Is_King, World_Traveler,
        Supporter, Ballon_Poper, Mission_Clear, Boosters_Lover
    }
    public ACHIVE_TYPE achiveType;

    public UILabel nameText;
    public UILabel contentText;
    public UILabel collectText;

    public UISprite processImg;

    public GameObject bronzeCup;
    public GameObject silverCup;
    public GameObject goldCup;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject bttnCollectD;
    public GameObject bttnCollectL;

    public int lv = 0;

    public int lv1Reward = 0;
    public int lv2Reward = 0;
    public int lv3Reward = 0;

    public int lv1TargetAmount = 0;
    public int lv2TargetAmount = 0;
    public int lv3TargetAmount = 0;

    public int targetAmount = 0;
    public int currentAmount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //_SetValues();
        //_ActiveImage();
        //_ActiveButton();
    }

    public void _SetValues()
    {
        if (achiveType == ACHIVE_TYPE.Gold_Collector)
        {
            if(lv == 0)
            {
                targetAmount = lv1TargetAmount;
            }
            else if(lv== 1)
            {
                targetAmount = lv2TargetAmount;
            }
            else if (lv == 2)
            {
                targetAmount = lv3TargetAmount;
            }

            currentAmount = playerStats.achivCoin;

            nameText.text = achiveName = Localisation.GetString(TranslateKey.GoldCollector);
            contentText.text = "Collect " + targetAmount + " Gold to get this";
        }
        else if (achiveType == ACHIVE_TYPE.Unstoppable)
        {
            if (lv == 0)
            {
                targetAmount = lv1TargetAmount;
            }
            else if (lv == 1)
            {
                targetAmount = lv2TargetAmount;
            }
            else if (lv == 2)
            {
                targetAmount = lv3TargetAmount;
            }

            currentAmount = playerStats.achivWin;

            nameText.text = achiveName = "Unstoppable";
            contentText.text = "Win " + targetAmount + " games to get this";
        }
        else if (achiveType == ACHIVE_TYPE.Customers_Is_King)
        {
            if (lv == 0)
            {
                targetAmount = lv1TargetAmount;
            }
            else if (lv == 1)
            {
                targetAmount = lv2TargetAmount;
            }
            else if (lv == 2)
            {
                targetAmount = lv3TargetAmount;
            }

            currentAmount = playerStats.achivGoodEmoCus;

            nameText.text = achiveName = "Customers are Kings";
            contentText.text = "Collect " + targetAmount + " heart when serve dishes";
        }
        else if (achiveType == ACHIVE_TYPE.World_Traveler)
        {
            if (lv == 0)
            {
                targetAmount = lv1TargetAmount;
            }
            else if (lv == 1)
            {
                targetAmount = lv2TargetAmount;
            }
            else if (lv == 2)
            {
                targetAmount = lv3TargetAmount;
            }

            currentAmount = playerStats.unlockedWorlds;

            nameText.text = achiveName = "World Traveler";
            contentText.text = "Complete World " + targetAmount + " to unlock";
        }
        else if (achiveType == ACHIVE_TYPE.Supporter)
        {
            if (lv == 0)
            {
                targetAmount = lv1TargetAmount;
            }
            else if (lv == 1)
            {
                targetAmount = lv2TargetAmount;
            }
            else if (lv == 2)
            {
                targetAmount = lv3TargetAmount;
            }

            currentAmount = playerStats.achivInAppPuchase;

            nameText.text = achiveName = "Supporter";
            contentText.text = "Purchase " + targetAmount + " in-app item to unlock";
        }
        else if (achiveType == ACHIVE_TYPE.Ballon_Poper)
        {
            if (lv == 0)
            {
                contentText.text = "Pop " + targetAmount + " balloon in world map";
                targetAmount = lv1TargetAmount;
            }
            else if (lv == 1)
            {
                targetAmount = lv2TargetAmount;
                contentText.text = "Pop " + targetAmount + " balloons in world map";
            }
            else if (lv == 2)
            {
                targetAmount = lv3TargetAmount;
                contentText.text = "Pop " + targetAmount + " balloons in world map";
            }

            currentAmount = playerStats.achivBallonPoper;

            nameText.text = achiveName = "Balloon Hunter";
        }
        else if (achiveType == ACHIVE_TYPE.Mission_Clear)
        {
            if (lv == 0)
            {
                targetAmount = lv1TargetAmount;
            }
            else if (lv == 1)
            {
                targetAmount = lv2TargetAmount;
            }
            else if (lv == 2)
            {
                targetAmount = lv3TargetAmount;
            }

            currentAmount = playerStats.achivDailyTaskCount;

            nameText.text = achiveName = "Born to Conquer";
            contentText.text = "Clear " + targetAmount + " daily missions to unlock";
        }
        else if (achiveType == ACHIVE_TYPE.Boosters_Lover)
        {
            if (lv == 0)
            {
                targetAmount = lv1TargetAmount;
            }
            else if (lv == 1)
            {
                targetAmount = lv2TargetAmount;
            }
            else if (lv == 2)
            {
                targetAmount = lv3TargetAmount;
            }

            currentAmount = playerStats.achivBoosterUse;

            nameText.text = achiveName = "Boosters Lover";
            contentText.text = "Use " + targetAmount + " boosters to unlock";
        }

    }

    public void _ActiveImage()
    {
        if(lv == 0)
        {
            bronzeCup.SetActive(false);
            silverCup.SetActive(false);
            goldCup.SetActive(false);
            star1.SetActive(false);
            star2.SetActive(false);
            star3.SetActive(false);
        }
        else if(lv == 1)
        {
            bronzeCup.SetActive(true);
            silverCup.SetActive(false);
            goldCup.SetActive(false);
            star1.SetActive(true);
            star2.SetActive(false);
            star3.SetActive(false);
        }
        else if (lv == 2)
        {
            bronzeCup.SetActive(false);
            silverCup.SetActive(true);
            goldCup.SetActive(false);
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(false);
        }
        else if (lv == 3)
        {
            bronzeCup.SetActive(false);
            silverCup.SetActive(false);
            goldCup.SetActive(true);
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }

        if (currentAmount >= targetAmount || lv >= 3)
        {
            processImg.fillAmount = 1;
        }
        else if (currentAmount < targetAmount)
        {
            processImg.fillAmount = (float)currentAmount / targetAmount;
        }
    }

    public void _ActiveButton()
    {
        if (lv < 3)
        {
            if (currentAmount >= targetAmount)
            {
                bttnCollectD.SetActive(false);
                bttnCollectL.SetActive(true);
            }
            else
            {
                bttnCollectD.SetActive(true);
                bttnCollectL.SetActive(false);
            }
        }
        else
        {
            bttnCollectD.SetActive(false);
            bttnCollectL.SetActive(false);
        }
    }

    public void _Collect()
    {
        if(lv < 3)
        {
            if (lv == 0 && currentAmount >= targetAmount)
            {
                playerStats.crystal += lv1Reward;
                lv = 1;
                _SetPlayerStatsValues();
            }
            else if (lv == 1 && currentAmount >= targetAmount)
            {
                playerStats.crystal += lv2Reward;
                lv = 2;
                _SetPlayerStatsValues();
            }
            else if (lv == 2 && currentAmount >= targetAmount)
            {
                playerStats.crystal += lv3Reward;
                lv = 3;
                _SetPlayerStatsValues();
            }

            _SetValues();
            _ActiveImage();
            _ActiveButton();

            achivementController._ActiveAttention();
        }
    }

    void _SetPlayerStatsValues()
    {
        for (int j = 0; j < playerStats.achivementDatas.Count; j++)
        {
            if (achiveType.ToString() == playerStats.achivementDatas[j].achivName)
            {
                playerStats.achivementDatas[j].currentLv = lv;
            }
        }

        playerStats.save = true;
    }
}
