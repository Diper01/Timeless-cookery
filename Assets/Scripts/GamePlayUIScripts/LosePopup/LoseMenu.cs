using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    private GamePlayMenuManager gamePlayMenuManager;
    private MissionManager missionManager;
    private LevelManager levelManager;
    private ItemManager itemManager;
    private PlayerStats playerStats;
    private NextScene nextScene;

    //private GoogleFireBaseEvents googleFireBaseEvents;
    //private AdManager adManager;

    public string sceneName = "WorldMap";

    public GameObject chefImg;

    //public Text levelText;
    public UIGrid targetGrid;

    public GameObject[] targetObject;

    public GameObject lostCusImg, firedFoodImg, trashImg;

    private void Awake()
    {

    }

    private void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        itemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        gamePlayMenuManager = GameObject.FindGameObjectWithTag("GamePlayMenuManager").GetComponent<GamePlayMenuManager>();
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        nextScene = GameObject.FindGameObjectWithTag("NextScene").GetComponent<NextScene>();
        //IronSource.Agent.displayBanner();
        //googleFireBaseEvents = FindObjectOfType<GoogleFireBaseEvents>();
        //adManager = FindObjectOfType<AdManager>();

        FindObjectOfType<AudioManager>()._Pause();

        missionManager.currentLoseCount++;

        _SetStatsTargetObject();

        //levelText.text = levelManager.mapName.ToString() + " Level " + missionManager.levelID;

       // googleFireBaseEvents.LogLevelFailedEvent(missionManager.levelID, missionManager.currentPlayerCoin);
        //googleFireBaseEvents.LoseLevel(missionManager.levelID, missionManager.starSet);
        _SetToItemManager();
        //_SetToLevelManager();

        _SetToPlayerStats();

        chefImg.SetActive(true);

     

        Time.timeScale = 0;
    }

    private void Update()
    {

    }

    public void _RetryButton()
    {
        /*_ShowAd();
        if (playerStats.energy > 0)
        {
            //_FacebookEvents();
            //googleFireBaseEvents.ReplayLevel(missionManager.levelID, missionManager.starSet);
         //   googleFireBaseEvents.LogLevelRetryEvent(missionManager.levelID, missionManager.currentPlayerCoin);

            Debug.Log("Choi lai");
            missionManager._RetryMission();
            nextScene.NextLevel(SceneManager.GetActiveScene().name);
        }
        else if(playerStats.energy <= 0)
        {
            gamePlayMenuManager._ActiveNotEnoughEnergyMenu();
            gameObject.SetActive(false);
        }*/
    }

    public void _Add100Coin()
    {
        //AdManager admanager = FindObjectOfType<AdManager>();
        //admanager.rewardType = AdManager.REWARD_TYPE.Coin;
        //admanager.rewardAmount = 100;

        //FindObjectOfType<AdManager>().ShowRewardAd();
    }

    public void _CloseButton()
    {
        //_FacebookEvents();

        //_SetToPlayerStats();
        _ShowAd();
        missionManager.scenePos = 0;
        missionManager._ResetMission();
        nextScene.NextLevel(sceneName);
    }

    public void _SetToPlayerStats()
    {
        //Decrease Energy
      

        //Add money to player
        playerStats.IncreaseCoin(missionManager.currentPlayerCoin);

        playerStats.todayCusOk += missionManager.customerPaidCount;
        playerStats.todayOrderOk += missionManager.orderOkCount;

        //Add Achivement mission values
        playerStats.customerOkCount += missionManager.customerPaidCount;
        playerStats.achivOrderOk += missionManager.orderOkCount;
        playerStats.achivGoodEmoCus += missionManager.customerGoodEmoCount;
        playerStats.achivBadCus += missionManager.customerAngryCount;

        playerStats.achivLose += 1;
    }

    void _SetToLevelManager()
    {
        if (missionManager.playAgain == false)
        {
            //save done guide and starindex
            //if (missionManager.levelID == levelManager.guideDone
            //    && missionManager.currentStar > levelManager.starindex)
            //{
            //    levelManager.guideDone = missionManager.levelID;
            //}

            levelManager._SaveData();
        }
    }

    void _SetToItemManager()
    {
        //if (itemManager.firstPlay == true)
        //{
        //    itemManager.firstPlay = false;
        //    itemManager._SaveData();
        //}

        itemManager._SaveData();

    }

    public void _SetStatsTargetObject()
    {
        targetGrid.maxPerLine = targetObject.Length;

        for (int i = 0; i < targetObject.Length; i++)
        {

            targetObject[i].SetActive(true);

            if (targetObject[i].GetComponent<LoseTargetObject>().targetType == LoseTargetObject.TargetType.NULL)
            {
                targetObject[i].GetComponent<LoseTargetObject>()._SetActiveImage();
            }
        }

        if (missionManager.firstTarget == MissionManager.FirstTarget.Time)
        {
            for (int i = 0; i < targetObject.Length; i++)
            {
                if (targetObject[i].GetComponent<LoseTargetObject>().targetType == LoseTargetObject.TargetType.NULL)
                {
                    targetObject[i].GetComponent<LoseTargetObject>().targetType = LoseTargetObject.TargetType.TIME;
                    targetObject[i].GetComponent<LoseTargetObject>()._SetActiveImage();
                    break;
                }
            }
        }
        if (missionManager.firstTarget == MissionManager.FirstTarget.Customer)
        {
            for (int i = 0; i < targetObject.Length; i++)
            {
                if (targetObject[i].GetComponent<LoseTargetObject>().targetType == LoseTargetObject.TargetType.NULL)
                {
                    targetObject[i].GetComponent<LoseTargetObject>().targetType = LoseTargetObject.TargetType.CUSTOMER;
                    targetObject[i].GetComponent<LoseTargetObject>()._SetActiveImage();
                    break;
                }
            }
        }
        if (missionManager.secondTarget == MissionManager.SecondTarget.Coin)
        {
            for (int i = 0; i < targetObject.Length; i++)
            {
                if (targetObject[i].GetComponent<LoseTargetObject>().targetType == LoseTargetObject.TargetType.NULL)
                {
                    targetObject[i].GetComponent<LoseTargetObject>().targetType = LoseTargetObject.TargetType.COIN;
                    targetObject[i].GetComponent<LoseTargetObject>()._SetActiveImage();
                    break;
                }
            }
        }
        if (missionManager.secondTarget == MissionManager.SecondTarget.Dish)
        {
            for (int i = 0; i < targetObject.Length; i++)
            {
                if (targetObject[i].GetComponent<LoseTargetObject>().targetType == LoseTargetObject.TargetType.NULL)
                {
                    targetObject[i].GetComponent<LoseTargetObject>().targetType = LoseTargetObject.TargetType.DISH;
                    targetObject[i].GetComponent<LoseTargetObject>()._SetActiveImage();
                    break;
                }
            }
        }
        if (missionManager.secondTarget == MissionManager.SecondTarget.Like)
        {
            for (int i = 0; i < targetObject.Length; i++)
            {
                if (targetObject[i].GetComponent<LoseTargetObject>().targetType == LoseTargetObject.TargetType.NULL)
                {
                    targetObject[i].GetComponent<LoseTargetObject>().targetType = LoseTargetObject.TargetType.LIKE;
                    targetObject[i].GetComponent<LoseTargetObject>()._SetActiveImage();
                    break;
                }
            }
        }
        if (missionManager.friedFoodMission && missionManager.friedFoodCount > missionManager.allowFriedFood)
        {
            firedFoodImg.SetActive(true);
        }
        if (missionManager.foodToTrashMission && missionManager.foodToTrashCount > missionManager.allowTrashFood)
        {
            trashImg.SetActive(true);
        }
        if (missionManager.lostCutomerMission && missionManager.customerAngryCount > missionManager.allowAngryCustomer)
        {
            lostCusImg.SetActive(true);
        }

        for (int i = 0; i < targetObject.Length; i++)
        {
            if (targetObject[i].GetComponent<LoseTargetObject>().targetType == LoseTargetObject.TargetType.NULL)
            {
                targetObject[i].SetActive(false);

                targetGrid.maxPerLine--;
            }
        }

        targetGrid.enabled = true;
    }

    public void ClickUpgradeLose()
    {
        _ShowAd();
        //googleFireBaseEvents.ClickUpgradeLose();
    }
    public void ReplayLevel()
    {
        //googleFireBaseEvents.ReplayLevel(missionManager.levelID, missionManager.starSet);
    }
    public void LoseCloseLevel()
    {
        //googleFireBaseEvents.LoseCloseLevel(missionManager.levelID, missionManager.starSet);
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            //googleFireBaseEvents.AppPauseLose(missionManager.levelID, missionManager.starSet);
        }
    }

    void _ShowAd()
    {
        missionManager.loseCount++;

        if(missionManager.levelID >= playerStats.levelAdShow && missionManager.loseCount == playerStats.loseAdShow)
        {
            missionManager.loseCount = 0;

            //if (adManager.fullScreenAdRequested == false)
            //{
            //    adManager.RequestFullScreenAd();
            //    adManager.ShowFullScreenAd();
            //}

            //else if (adManager.fullScreenAdRequested == true)
            //{
            //    adManager.ShowFullScreenAd();
            //}
        }
    }
}
