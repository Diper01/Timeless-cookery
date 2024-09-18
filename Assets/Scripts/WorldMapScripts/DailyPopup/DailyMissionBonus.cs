using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DailyMissionBonus : MonoBehaviour
{
    private PlayerStats playerStats;
//    private GoogleFireBaseEvents googleFireBaseEvents;

    public UILabel timeText;

    public GameObject bttnDark;
    public GameObject collectBtn;
    public GameObject ticker;

    public GameObject dailyTask1;
    public GameObject dailyTask2;
    public GameObject dailyTask3;

    public GameObject coinPos;
    public GameObject coinEffect;

    public DateTime nextDay;

    public int rewardCoin = 250;

    public DailyTask.TASK_STATUS dailyBonusStats = DailyTask.TASK_STATUS.NOT_READY;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Daili bonus:" + gameObject.name);
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
     //   googleFireBaseEvents = FindObjectOfType<GoogleFireBaseEvents>();

        dailyBonusStats = playerStats.dailyBonusStats;

        //nextDay = DateTime.Now.AddDays(1).Date;
        //string tempStr = nextDay.ToShortDateString();
        //nextDay = DateTime.Parse(tempStr);
    }

    // Update is called once per frame
    void Update()
    {
        _DailyBonusStats();
        _SetActiveButton();
        _Timer();
    }

    void _DailyBonusStats()
    {
        if (dailyBonusStats == DailyTask.TASK_STATUS.NOT_READY)
        {
            if (dailyTask1.GetComponent<DailyTask>().taskStatus == DailyTask.TASK_STATUS.READY 
                && dailyTask2.GetComponent<DailyTask>().taskStatus == DailyTask.TASK_STATUS.READY
                && dailyTask3.GetComponent<DailyTask>().taskStatus == DailyTask.TASK_STATUS.READY
                || dailyTask1.GetComponent<DailyTask>().taskStatus == DailyTask.TASK_STATUS.CLAIMED
                && dailyTask2.GetComponent<DailyTask>().taskStatus == DailyTask.TASK_STATUS.CLAIMED
                && dailyTask3.GetComponent<DailyTask>().taskStatus == DailyTask.TASK_STATUS.CLAIMED)
            {
                dailyBonusStats = DailyTask.TASK_STATUS.READY;
            }
        }
        else if (dailyTask1.GetComponent<DailyTask>().taskStatus == DailyTask.TASK_STATUS.NOT_READY
                || dailyTask2.GetComponent<DailyTask>().taskStatus == DailyTask.TASK_STATUS.NOT_READY
                || dailyTask3.GetComponent<DailyTask>().taskStatus == DailyTask.TASK_STATUS.NOT_READY)
        {
            dailyBonusStats = DailyTask.TASK_STATUS.NOT_READY;
            playerStats.dailyBonusStats = DailyTask.TASK_STATUS.NOT_READY;
        }
    }

    void _SetActiveButton()
    {
        if(gameObject.activeSelf == true)
        {
            if(dailyBonusStats == DailyTask.TASK_STATUS.NOT_READY)
            {
                bttnDark.SetActive(true);
                collectBtn.SetActive(false);
                ticker.SetActive(false);
            }
            else if(dailyBonusStats == DailyTask.TASK_STATUS.READY)
            {
                bttnDark.SetActive(false);
                collectBtn.SetActive(true);
                ticker.SetActive(false);
            }
            else if (dailyBonusStats == DailyTask.TASK_STATUS.CLAIMED)
            {
                bttnDark.SetActive(false);
                collectBtn.SetActive(false);
                ticker.SetActive(true);
            }
        }
    }

    void _Timer()
    {
        if(dailyBonusStats == DailyTask.TASK_STATUS.NOT_READY || dailyBonusStats == DailyTask.TASK_STATUS.READY)
        {
            DateTime now = DateTime.Now;
            DateTime dateTemp = DateTime.Now.AddDays(1).Date;
            TimeSpan time = dateTemp - now;
            timeText.text = String.Format("{0:00}:{1:00}:{2:00}", time.Hours, time.Minutes, time.Seconds);
        }
        else if(dailyBonusStats == DailyTask.TASK_STATUS.CLAIMED)
        {
            timeText.text = String.Format("{0:00}:{1:00}:{2:00}", 00, 00, 00);
        }
    }

    public void _Collect()
    {
        Debug.Log("daily bonus stats " + dailyBonusStats);
        if(dailyBonusStats == DailyTask.TASK_STATUS.READY)
        {
            playerStats.coin += rewardCoin;
            playerStats.dailyBonusStats = DailyTask.TASK_STATUS.CLAIMED;
            dailyBonusStats = DailyTask.TASK_STATUS.CLAIMED;

            playerStats.save = true;

            //googleFireBaseEvents.DoneDaily();

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
