using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

public class DialogGiftHandle : MonoBehaviour
{
    private PlayerStats playerStats;
    DateTime lastTime;
    public GameObject dailyBonusPage, dailyTaskPage;
    public UIButton btDailyBonus, btDailyTask;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();

        //_DailyTimeStatsController();
        //openDailyBonusPage();
    }

    // Update is called once per frame
    void Update()
    {
        _DailyTimeStatsController();
    }

    public void openDailyBonusPage()
    {
        dailyBonusPage.transform.localScale = new Vector3(1, 1, 1);
        dailyTaskPage.transform.localScale = new Vector3(0, 0, 0);
        btDailyBonus.defaultColor = Color.white;
        btDailyTask.defaultColor = Color.grey;
    }

    public void openDailyTaskPage()
    {
        dailyBonusPage.transform.localScale = new Vector3(0, 0, 0);
        dailyTaskPage.transform.localScale = new Vector3(1, 1, 1);
        btDailyBonus.defaultColor = Color.grey;
        btDailyTask.defaultColor = Color.white;
    }

    void _DailyTimeStatsController()
    {
       
        CultureInfo[] cultures = { new CultureInfo("en-US"), new CultureInfo("ru-RU") };
        bool isDateParsed = false;

        foreach (var culture in cultures)
        {
            if (DateTime.TryParse(playerStats.lastTime, culture, DateTimeStyles.None, out lastTime))
            {
                isDateParsed = true;
                break;
            }
        }

        if (!isDateParsed)
        {
            Debug.LogError("Failed to parse lastTime: " + playerStats.lastTime);
            return;
        }

        DateTime nextDay = DateTime.Now.AddDays(1);

        int checkNewDay = DateTime.Compare(DateTime.Now.Date, lastTime.Date);
        int nextDayCheck = DateTime.Compare(nextDay.Date, lastTime.Date);

        if (checkNewDay > 0)
        {
            playerStats.lastTime = DateTime.Now.ToString("dd/MM/yyyy");

            dailyTaskPage.GetComponent<DailyMissionController>()._ResetDailyValues();
            dailyTaskPage.GetComponent<DailyMissionController>()._GetRandomMission();

            playerStats.dailyStats = DailyObject.DAILY_STATUS.READY;

            dailyBonusPage.GetComponent<DailyBonusController>()._SetDailyObjectStats();

            playerStats.save = true;
        }
        else if (nextDayCheck == 0)
        {
            playerStats.lastTime = DateTime.Now.ToString("dd/MM/yyyy");

            dailyTaskPage.GetComponent<DailyMissionController>()._ResetDailyValues();
            dailyTaskPage.GetComponent<DailyMissionController>()._GetRandomMission();

            playerStats.dailyStats = DailyObject.DAILY_STATUS.READY;

            dailyBonusPage.GetComponent<DailyBonusController>()._SetDailyObjectStats();

            playerStats.save = true;
        }
    }
}
