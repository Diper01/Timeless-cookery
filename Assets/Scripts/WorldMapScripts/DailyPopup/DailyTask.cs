using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyTask : MonoBehaviour
{
    private PlayerStats playerStats;
  //  private GoogleFireBaseEvents googleFireBaseEvents;

    public UILabel taskInfoText;
    public UILabel processText;
    public UILabel rewardText;
    public UISprite processImg;

    public GameObject darkBttn;
    public GameObject collectBtn;
    public GameObject checkImg;

    public GameObject coinPos;
    public GameObject coinEffect;

    public string taskInfoStr = "";
    public int targetAmount = 0;
    public int currentAmount = 0;
    public int rewardCoin = 0;

    public int taskIndex = 0;

    public DailyMissionController.MISSION_TYPE missionType;

    public enum TASK_STATUS { NOT_READY, READY, CLAIMED }

    public TASK_STATUS taskStatus = TASK_STATUS.NOT_READY;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        //googleFireBaseEvents = FindObjectOfType<GoogleFireBaseEvents>();

        initMissionType();
    }

    private void Update()
    {
        initMissionType();
        _TaskStatus();
        _SetText();
        _ProcessBar();
    }

    void _SetText()
    {
        taskInfoText.text = taskInfoStr;

        if (currentAmount >= targetAmount)
        {
            processText.text = targetAmount.ToString() + "/" + targetAmount.ToString();
        }
        else
        {
            processText.text = currentAmount.ToString() + "/" + targetAmount.ToString();
        }

        rewardText.text = rewardCoin.ToString();
    }

    void _ProcessBar()
    {
        processImg.fillAmount = (float)currentAmount / targetAmount;
    }

    public void initMissionType()
    {
        if (missionType == DailyMissionController.MISSION_TYPE.Cus_Ok_30)
        {
            taskInfoStr = "Complete 30 Orders";
            targetAmount = 30;
            rewardCoin = 150;

            currentAmount = playerStats.todayCusOk;
        }
        else
        if (missionType == DailyMissionController.MISSION_TYPE.Cus_Ok_50)
        {
            taskInfoStr = "Complete 50 Orders";
            targetAmount = 50;
            rewardCoin = 250;

            currentAmount = playerStats.todayCusOk;
        }
        else
        if (missionType == DailyMissionController.MISSION_TYPE.Order_Ok_30)
        {
            taskInfoStr = "Complete 30 Orders With Good Emotion";
            targetAmount = 30;
            rewardCoin = 150;

            currentAmount = playerStats.todayOrderOk;
        }
        else
        if (missionType == DailyMissionController.MISSION_TYPE.Order_Ok_50)
        {
            taskInfoStr = "Complete 50 Orders With Good Emotion";
            targetAmount = 50;
            rewardCoin = 300;

            currentAmount = playerStats.todayOrderOk;
        }
        else
        if (missionType == DailyMissionController.MISSION_TYPE.Win_5)
        {
            taskInfoStr = "Win 5 times";
            targetAmount = 5;
            rewardCoin = 200;

            currentAmount = playerStats.todayWin;
        }
        else
        if (missionType == DailyMissionController.MISSION_TYPE.Win_10)
        {
            taskInfoStr = "Win 10 times";
            targetAmount = 10;
            rewardCoin = 300;

            currentAmount = playerStats.todayWin;
        }
        else
        if (missionType == DailyMissionController.MISSION_TYPE.Earn_Coin_200)
        {
            taskInfoStr = "Earn 200 Coin";
            targetAmount = 200;
            rewardCoin = 100;

            currentAmount = playerStats.todayEarnCoin;
        }
        else
        if (missionType == DailyMissionController.MISSION_TYPE.Earn_Coin_500)
        {
            taskInfoStr = "Earn 500 Coin";
            targetAmount = 500;
            rewardCoin = 250;

            currentAmount = playerStats.todayEarnCoin;
        }
    }

    void _TaskStatus()
    {
        if(taskStatus != TASK_STATUS.CLAIMED)
        {
            if (currentAmount >= targetAmount && taskStatus == TASK_STATUS.NOT_READY)
            {
                taskStatus = TASK_STATUS.READY;

                darkBttn.SetActive(false);
                collectBtn.SetActive(true);
                checkImg.SetActive(false);
            }
            else if (taskStatus == TASK_STATUS.NOT_READY)
            {
                darkBttn.SetActive(true);
                collectBtn.SetActive(false);
                checkImg.SetActive(false);
            }
        }

        
        else if (taskStatus == TASK_STATUS.CLAIMED)
        {
            darkBttn.SetActive(false);
            collectBtn.SetActive(false);
            checkImg.SetActive(true);
        } 
    }

    public void _CollectButton()
    {
        if (taskStatus == TASK_STATUS.READY)
        {
            playerStats.coin += rewardCoin;

            playerStats.achivDailyTaskCount += 1;

            taskStatus = TASK_STATUS.CLAIMED;

            if(missionType == playerStats.typeTask1)
            {
                playerStats.statusTask1 = taskStatus;
            }
            if (missionType == playerStats.typeTask2)
            {
                playerStats.statusTask2 = taskStatus;
            }
            if (missionType == playerStats.typeTask3)
            {
                playerStats.statusTask3 = taskStatus;
            }

            playerStats.save = true;

            //googleFireBaseEvents.DailyTask(taskIndex);

            StartCoroutine(_CoinEffect());
        }
    }

    IEnumerator _CoinEffect()
    {
        for (int i = 0; i < 3; i++)
        {
            coinEffect.GetComponent<WorldMapCoinEffect>().targetPos = coinPos.transform.position;
            Instantiate(coinEffect, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }

        yield break;
    }
}
