using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DailyBonusController : MonoBehaviour
{
    private PlayerStats playerStats;
    private LevelManager levelManager;

    public DailyObject[] dailyObjects;

    public bool newDay = false;
    public static bool dailyDialogAlreadyOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        levelManager = FindObjectOfType<LevelManager>();

        _SetDailyObjectStats();
        if (!dailyDialogAlreadyOpened)
        {

            if(levelManager.levels[1].levelID == 2 && levelManager.levels[1].levelCompleted == false)
            {
                return;
            }

            for (int i = 0; i < dailyObjects.Length; i++)
            {
                if (dailyObjects[i].dailyStatus == DailyObject.DAILY_STATUS.READY)
                {
                    GameObject.Find("WorldMapController").GetComponent<WorldMapController>().openDialogDailyGift();
                    dailyDialogAlreadyOpened = true;
                    return;
                }
            }

        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void _SetDailyObjectStats()
    {
        if (playerStats.dailyTurn >= 7)
        {
            playerStats.dailyTurn = 0;
        }

        for (int i = 0; i < dailyObjects.Length; i++)
        {
            dailyObjects[i].index = i + 1;

            if (i > playerStats.dailyTurn)
            {
                dailyObjects[i].dailyStatus = DailyObject.DAILY_STATUS.NOT_READY;
            }
            else if(i< playerStats.dailyTurn)
            {
                dailyObjects[i].dailyStatus = DailyObject.DAILY_STATUS.CLAIMED;
            }
            else if(i == playerStats.dailyTurn)
            {
                if(playerStats.dailyStats == DailyObject.DAILY_STATUS.READY)
                {
                    dailyObjects[i].dailyStatus = DailyObject.DAILY_STATUS.READY;
                }
                else
                if(playerStats.dailyStats == DailyObject.DAILY_STATUS.CLAIMED)
                {
                    if (i > 0)
                    {
                        dailyObjects[i - 1].dailyStatus = DailyObject.DAILY_STATUS.CLAIMED;
                    }
                }
            }
        }
    }
}
