using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DailyMissionController : MonoBehaviour
{
    private PlayerStats playerStats;

    public enum MISSION_TYPE {
        Cus_Ok_30, Cus_Ok_50,
        Order_Ok_30, Order_Ok_50,
        Win_5, Win_10,
        Earn_Coin_200, Earn_Coin_500, Spen_Coin_200, Spen_Coin_500,
        Earn_Crystal_200, Earn_Crystal_500, SpendCrystal_200, SpendCrystal_500,
    }

    public GameObject dailyTask1;
    public GameObject dailyTask2;
    public GameObject dailyTask3;

    public MISSION_TYPE typeTask1;
    public MISSION_TYPE typeTask2;
    public MISSION_TYPE typeTask3;

    public bool renewMission = false;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();

        initDailyTask();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf == true)
        {
            //initDailyTask();
        }
    }

    public void _GetRandomMission()
    {
        StartCoroutine(_SetTypeTask());
    }

    IEnumerator _SetTypeTask()
    {
        playerStats.typeTask1 = (MISSION_TYPE)UnityEngine.Random.Range(0, 3);

        do
        {
            playerStats.typeTask2 = (MISSION_TYPE)UnityEngine.Random.Range(0, 3);
        }
        while (playerStats.typeTask2 == playerStats.typeTask1);

        do
        {
            playerStats.typeTask3 = (MISSION_TYPE)UnityEngine.Random.Range(0, 3);
        }
        while (playerStats.typeTask3 == playerStats.typeTask1 || playerStats.typeTask3 == playerStats.typeTask2);

        yield break;
    }

    public void _ResetDailyValues()
    {
        playerStats.statusTask1 = DailyTask.TASK_STATUS.NOT_READY;
        playerStats.currentAmountTask1 = 0;
        playerStats.statusTask2 = DailyTask.TASK_STATUS.NOT_READY;
        playerStats.currentAmountTask2 = 0;
        playerStats.statusTask3 = DailyTask.TASK_STATUS.NOT_READY;
        playerStats.currentAmountTask3 = 0;

        playerStats.dailyBonusStats = DailyTask.TASK_STATUS.NOT_READY;

        playerStats.todayEarnCoin = 0;
        playerStats.todayEarnCrystal = 0;
        playerStats.todaySpendCoin = 0;
        playerStats.todaySpendCrystal = 0;

        playerStats.todayCusOk = 0;
        playerStats.todayGoodEmoCusOk = 0;
        playerStats.todayOrderOk = 0;

        playerStats.todayWin = 0;
    }

    void initDailyTask()
    {
        typeTask1 = playerStats.typeTask1;
        dailyTask1.GetComponent<DailyTask>().missionType = typeTask1;
        dailyTask1.GetComponent<DailyTask>().currentAmount = playerStats.currentAmountTask1;
        dailyTask1.GetComponent<DailyTask>().taskStatus = playerStats.statusTask1;
        dailyTask1.GetComponent<DailyTask>().taskIndex = 1;

        typeTask2 = playerStats.typeTask2;
        dailyTask2.GetComponent<DailyTask>().missionType = typeTask2;
        dailyTask2.GetComponent<DailyTask>().currentAmount = playerStats.currentAmountTask2;
        dailyTask2.GetComponent<DailyTask>().taskStatus = playerStats.statusTask2;
        dailyTask1.GetComponent<DailyTask>().taskIndex = 2;

        typeTask3 = playerStats.typeTask3;
        dailyTask3.GetComponent<DailyTask>().missionType = typeTask3;
        dailyTask3.GetComponent<DailyTask>().currentAmount = playerStats.currentAmountTask3;
        dailyTask3.GetComponent<DailyTask>().taskStatus = playerStats.statusTask3;
        dailyTask1.GetComponent<DailyTask>().taskIndex = 3;
    }

    void init()
    {
        dailyTask1.GetComponent<DailyTask>().missionType = typeTask1;

        dailyTask2.GetComponent<DailyTask>().missionType = typeTask2;

        dailyTask3.GetComponent<DailyTask>().missionType = typeTask3;
    }
}
