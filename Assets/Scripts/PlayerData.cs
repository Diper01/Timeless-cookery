using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerData
{
    public int unlockedWorlds = 0;
    public int currentWorld = 0;
    public int lastPlayLevel = 0;

    public string userName = "";

    public int coin = 0;

    public int energy = 0;
    public int crystal = 0;
    public int customerOkCount = 0;

    public int playCount = 0;
    public bool rated = false;
    public int appOpen = 0;

    public string lastFreeCoinDate = "8/27/2019 10:22:20 PM";
    public int freeCoinLeft = 0;

    public bool biginerPurchased = false;
    public bool insurgenPurchased = false;

    public bool energyBundlePurchased = false;
    public bool startBundlePurchased = false;
    public bool midBundlePurchased = false;
    public bool economicBundlePurchased = false;

    public int levelAdShow = 0;
    public int winAdShow = 0;
    public int loseAdShow = 0;
    public bool tutMap = true;
    public bool tutUpgrade = true;
    public bool tutGameplay = true;

    public int mushroomAmount = 0;
    public bool mushroomActivated = false;
    public int waterAmount = 0;
    public bool waterActivated = false;
    public int eggAmount = 0;
    public bool eggActivated = false;
    public int x3CandleAmount = 0;
    public bool x3CandleActivated = false;
    public int x2GoldAmount = 0;
    public bool x2GoldActivated = false;

    public string energyTime = "8/25/2019 10:22:20 PM";
    

    public DailyMissionController.MISSION_TYPE typeTask1;
    public int currentAmountTask1 = 0;
    public DailyTask.TASK_STATUS statusTask1 = DailyTask.TASK_STATUS.NOT_READY;
    public DailyMissionController.MISSION_TYPE typeTask2;
    public int currentAmountTask2 = 0;
    public DailyTask.TASK_STATUS statusTask2 = DailyTask.TASK_STATUS.NOT_READY;
    public DailyMissionController.MISSION_TYPE typeTask3;
    public int currentAmountTask3 = 0;
    public DailyTask.TASK_STATUS statusTask3 = DailyTask.TASK_STATUS.NOT_READY;
    public DailyTask.TASK_STATUS dailyBonusStats = DailyTask.TASK_STATUS.NOT_READY;

    public int todayEarnCoin = 0;
    public int todaySpendCoin = 0;
    public int todayEarnCrystal = 0;
    public int todaySpendCrystal = 0;
    public int todayCusOk = 0;
    public int todayGoodEmoCusOk = 0;
    public int todayOrderOk = 0;
    public int todayWin = 0;

    public int dailyTurn = 0;
    public DailyObject.DAILY_STATUS dailyStats = DailyObject.DAILY_STATUS.NOT_READY;

    public string lastTime = "25/08/2019";

    public List<PlayerStats.AchivementData> achivementDatas = new List<PlayerStats.AchivementData>();

    public int achivCoin = 0;
    public int achivSpenCoin = 0;
    public int achivCrystal = 0;
    public int achivSpenCrystal = 0;
    public int achivSpenEnergy = 0;

    public int achivWin = 0;
    public int achivLose = 0;
    public int achivOrder = 0;
    public int achivGoodCusOk = 0;
    public int achivBadCus = 0;
    public int achivOrderOk = 0;
    public int achivComLv = 0;
    public int achivBallonPoper = 0;
    public int achivDailyTaskCount = 0;
    public int achivBoosterUse = 0;
    public int achivInAppPuchase = 0;

    public string foxTime = "8/27/2019 10:22:20 PM";
    public bool foxTouched = false;

    public bool musicOn = true;
    public bool soundOn = true;

    public List<RankDialog.RankInfo> rankInfoData = new List<RankDialog.RankInfo>();

    public static string playerDataFileName = "player_data.json";
    private static PlayerData instance;

    public static PlayerData Instance
    {
        get
        {
            if(instance == null)
            {
                Load();
            }
            return instance;
        }
    }

    public static void Save()
    {
        SaveSystem.Save(playerDataFileName, instance);
    }

    public static void Load()
    {
        instance = SaveSystem.Load<PlayerData>(playerDataFileName);
    }
}
