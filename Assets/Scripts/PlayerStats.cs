using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public int unlockedWorlds = 0;
    public int currentWorld = 0;
    public int lastPlayLevel = 0;

    public string userName = "";

    public int coin = 0;

    //public int energy = 10;
    public int crystal = 0;
    public int customerOkCount = 0;

    public int freeTurn = 0;
    public bool rated = false;
    public int playCount = 0;
    public int appOpen = 0;
    public string lastFreeCoinDate = "1/1/2020 4:47:50 PM";
    public int freeCoinLeft = 0;

    public bool biginerPurchased = false;
    public bool insurgenPurchased = false;

    public bool energyBundlePurchased = false;
    public bool startBundlePurchased = false;
    public bool midBundlePurchased = false;
    public bool economicBundlePurchased = false;

    [Header("Unity Remote")]
    public int levelAdShow = 0;
    public int winAdShow = 0;
    public int loseAdShow = 0;
    public bool tutMap = true;
    public bool tutUpgrade = true;
    public bool tutGameplay = true;

    [Header("Booster Amount")]
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

    [Header("Energy")]
    public string energyTime = "1/1/2020 4:47:50 PM";

    [Header("Daily Mission")]
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

    [Header("Player Stats/Day")]
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

    public string lastTime = "27/08/2019";

    [System.Serializable]
    public class AchivementData
    {
        public string achivName = "";
        public int currentLv = 0;
    }

    [Header("Player Stats/Achivement")]

    public List<AchivementData> achivementDatas = new List<AchivementData>();

    public int achivCoin = 0;
    public int achivSpenCoin = 0;
    public int achivCrystal = 0;
    public int achivSpenCrystal = 0;
    public int achivSpenEnergy = 0;

    public int achivWin = 0;
    public int achivLose = 0;
    public int achivGoodEmoCus = 0;
    public int achivBadCus = 0;
    public int achivOrderOk = 0;
    public int achivComLv = 0;
    public int achivBallonPoper = 0;
    public int achivDailyTaskCount = 0;
    public int achivBoosterUse = 0;
    public int achivInAppPuchase = 0;

    public bool achivtNoiticeActived = false;

    public string foxTime = "1/1/2020 4:47:50 PM";
    public bool foxTouched = false;

    [Header("Sound Setting")]
    public bool musicOn = true;
    public bool soundOn = true;

    public bool save = false;

    private void Awake()
    {
        string filePath = Path.Combine(Application.persistentDataPath, PlayerData.playerDataFileName);
        if (File.Exists(filePath) && PlayerPrefs.GetInt("ResetPlayerStats", 0) == 0)
        {
            _LoadData();
        }

        PlayerPrefs.SetInt("ResetPlayerStats", 0);
    }

    private void Start()
    {
        _MakeSingleInstance();
    }

    void _MakeSingleInstance()
    {
        int numInstance = GameObject.FindGameObjectsWithTag("PlayerStats").Length;
        if (numInstance > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        _SaveData();
    }

    void _LoadData()
    {
        PlayerData.Load();

        unlockedWorlds = PlayerData.Instance.unlockedWorlds;
        currentWorld = PlayerData.Instance.currentWorld;
        lastPlayLevel = PlayerData.Instance.lastPlayLevel;

        userName = PlayerData.Instance.userName;

        coin = PlayerData.Instance.coin;
        //energy = PlayerData.Instance.energy;
        crystal = PlayerData.Instance.crystal;

        playCount = PlayerData.Instance.playCount;
        rated = PlayerData.Instance.rated;
        appOpen = PlayerData.Instance.appOpen;

        lastFreeCoinDate = PlayerData.Instance.lastFreeCoinDate;
        freeCoinLeft = PlayerData.Instance.freeCoinLeft;

        biginerPurchased = PlayerData.Instance.biginerPurchased;
        insurgenPurchased = PlayerData.Instance.insurgenPurchased;

        energyBundlePurchased = PlayerData.Instance.energyBundlePurchased;
        startBundlePurchased = PlayerData.Instance.startBundlePurchased;
        midBundlePurchased = PlayerData.Instance.midBundlePurchased;
        economicBundlePurchased = PlayerData.Instance.economicBundlePurchased;

        levelAdShow = PlayerData.Instance.levelAdShow;
        winAdShow = PlayerData.Instance.winAdShow;
        loseAdShow = PlayerData.Instance.loseAdShow;
        tutMap = PlayerData.Instance.tutMap;
        tutUpgrade = PlayerData.Instance.tutUpgrade;
        tutGameplay = PlayerData.Instance.tutGameplay;

        customerOkCount = PlayerData.Instance.customerOkCount;

        mushroomAmount = PlayerData.Instance.mushroomAmount;
        mushroomActivated = PlayerData.Instance.mushroomActivated;
        waterAmount = PlayerData.Instance.waterAmount;
        waterActivated = PlayerData.Instance.waterActivated;
        eggAmount = PlayerData.Instance.eggAmount;
        eggActivated = PlayerData.Instance.eggActivated;
        x3CandleAmount = PlayerData.Instance.x3CandleAmount;
        x3CandleActivated = PlayerData.Instance.x3CandleActivated;
        x2GoldAmount = PlayerData.Instance.x2GoldAmount;
        x2GoldActivated = PlayerData.Instance.x2GoldActivated;

        energyTime = PlayerData.Instance.energyTime;

        typeTask1 = PlayerData.Instance.typeTask1;
        currentAmountTask1 = PlayerData.Instance.currentAmountTask1;
        statusTask1 = PlayerData.Instance.statusTask1;
        typeTask2 = PlayerData.Instance.typeTask2;
        currentAmountTask2 = PlayerData.Instance.currentAmountTask2;
        statusTask2 = PlayerData.Instance.statusTask2;
        typeTask3 = PlayerData.Instance.typeTask3;
        currentAmountTask3 = PlayerData.Instance.currentAmountTask3;
        statusTask3 = PlayerData.Instance.statusTask3;

        dailyBonusStats = PlayerData.Instance.dailyBonusStats;

        todayEarnCoin = PlayerData.Instance.todayEarnCoin;
        todaySpendCoin = PlayerData.Instance.todaySpendCoin;
        todayEarnCrystal = PlayerData.Instance.todayEarnCrystal;
        todaySpendCrystal = PlayerData.Instance.todaySpendCrystal;
        todayCusOk = PlayerData.Instance.todayCusOk;
        todayGoodEmoCusOk = PlayerData.Instance.todayGoodEmoCusOk;
        todayOrderOk = PlayerData.Instance.todayOrderOk;
        todayWin = PlayerData.Instance.todayWin;

        dailyTurn = PlayerData.Instance.dailyTurn;
        dailyStats = PlayerData.Instance.dailyStats;

        lastTime = PlayerData.Instance.lastTime;

        achivementDatas.Clear();

        for(int i=0; i< PlayerData.Instance.achivementDatas.Count; i++)
        {
            PlayerStats.AchivementData achivementData = new PlayerStats.AchivementData();

            achivementData.achivName = PlayerData.Instance.achivementDatas[i].achivName;
            achivementData.currentLv = PlayerData.Instance.achivementDatas[i].currentLv;

            achivementDatas.Add(achivementData);
        }

        achivCoin = PlayerData.Instance.achivCoin;
        achivSpenCoin = PlayerData.Instance.achivSpenCoin;
        achivCrystal = PlayerData.Instance.achivCrystal;
        achivSpenCrystal = PlayerData.Instance.achivSpenCrystal;
        achivSpenEnergy = PlayerData.Instance.achivSpenEnergy;

        achivWin = PlayerData.Instance.achivWin;
        achivLose = PlayerData.Instance.achivLose;
        achivGoodEmoCus = PlayerData.Instance.achivGoodCusOk;
        achivBadCus = PlayerData.Instance.achivBadCus;
        achivOrderOk = PlayerData.Instance.achivOrderOk;
        achivComLv = PlayerData.Instance.achivComLv;
        achivBallonPoper = PlayerData.Instance.achivBallonPoper;
        achivDailyTaskCount = PlayerData.Instance.achivDailyTaskCount;
        achivBoosterUse = PlayerData.Instance.achivBoosterUse;
        achivInAppPuchase = PlayerData.Instance.achivInAppPuchase;

        foxTime = PlayerData.Instance.foxTime;
        foxTouched = PlayerData.Instance.foxTouched;

        musicOn = PlayerData.Instance.musicOn;
        soundOn = PlayerData.Instance.soundOn;
    }

    public void _SaveData()
    {
        if(save == true)
        {
            PlayerData.Instance.unlockedWorlds = unlockedWorlds;
            PlayerData.Instance.currentWorld = currentWorld;
            PlayerData.Instance.lastPlayLevel = lastPlayLevel;

            PlayerData.Instance.userName = userName;

            PlayerData.Instance.coin = coin;
            //PlayerData.Instance.energy = energy;
            PlayerData.Instance.crystal = crystal;

            PlayerData.Instance.playCount = playCount;
            PlayerData.Instance.rated = rated;
            PlayerData.Instance.appOpen = appOpen;

            PlayerData.Instance.lastFreeCoinDate = lastFreeCoinDate;
            PlayerData.Instance.freeCoinLeft = freeCoinLeft;

            PlayerData.Instance.biginerPurchased = biginerPurchased;
            PlayerData.Instance.insurgenPurchased = insurgenPurchased;

            PlayerData.Instance.energyBundlePurchased = energyBundlePurchased;
            PlayerData.Instance.startBundlePurchased = startBundlePurchased;
            PlayerData.Instance.midBundlePurchased = midBundlePurchased;
            PlayerData.Instance.economicBundlePurchased = economicBundlePurchased;

            PlayerData.Instance.levelAdShow = levelAdShow;
            PlayerData.Instance.winAdShow = winAdShow;
            PlayerData.Instance.loseAdShow = loseAdShow;
            PlayerData.Instance.tutMap = tutMap;
            PlayerData.Instance.tutUpgrade = tutUpgrade;
            PlayerData.Instance.tutGameplay = tutGameplay;

            PlayerData.Instance.customerOkCount = customerOkCount;

            PlayerData.Instance.mushroomAmount = mushroomAmount;
            PlayerData.Instance.mushroomActivated = mushroomActivated;
            PlayerData.Instance.waterAmount = waterAmount;
            PlayerData.Instance.waterActivated = waterActivated;
            PlayerData.Instance.eggAmount = eggAmount;
            PlayerData.Instance.eggActivated = eggActivated;
            PlayerData.Instance.x3CandleAmount = x3CandleAmount;
            PlayerData.Instance.x3CandleActivated = x3CandleActivated;
            PlayerData.Instance.x2GoldAmount = x2GoldAmount;
            PlayerData.Instance.x2GoldActivated = x2GoldActivated;

            PlayerData.Instance.energyTime = energyTime;

            PlayerData.Instance.typeTask1 = typeTask1;
            PlayerData.Instance.currentAmountTask1 = currentAmountTask1;
            PlayerData.Instance.statusTask1 = statusTask1;
            PlayerData.Instance.typeTask2 = typeTask2;
            PlayerData.Instance.currentAmountTask2 = currentAmountTask2;
            PlayerData.Instance.statusTask2 = statusTask2;
            PlayerData.Instance.typeTask3 = typeTask3;
            PlayerData.Instance.currentAmountTask3 = currentAmountTask3;
            PlayerData.Instance.statusTask3 = statusTask3;

            PlayerData.Instance.dailyBonusStats = dailyBonusStats;

            PlayerData.Instance.todayEarnCoin = todayEarnCoin;
            PlayerData.Instance.todaySpendCoin = todaySpendCoin;
            PlayerData.Instance.todayEarnCrystal = todayEarnCrystal;
            PlayerData.Instance.todaySpendCrystal = todaySpendCrystal;
            PlayerData.Instance.todayCusOk = todayCusOk;
            PlayerData.Instance.todayGoodEmoCusOk = todayGoodEmoCusOk;
            PlayerData.Instance.todayOrderOk = todayOrderOk;
            PlayerData.Instance.todayWin = todayWin;
            PlayerData.Instance.achivBallonPoper = achivBallonPoper;
            PlayerData.Instance.achivDailyTaskCount = achivDailyTaskCount;
            PlayerData.Instance.achivBoosterUse = achivBoosterUse;
            PlayerData.Instance.achivInAppPuchase = achivInAppPuchase;

            PlayerData.Instance.dailyTurn = dailyTurn;
            PlayerData.Instance.dailyStats = dailyStats;

            PlayerData.Instance.lastTime = lastTime;

            PlayerData.Instance.achivementDatas.Clear();

            for (int i = 0; i < achivementDatas.Count; i++)
            {
                PlayerStats.AchivementData achivementData = new PlayerStats.AchivementData();

                achivementData.achivName = achivementDatas[i].achivName;
                achivementData.currentLv = achivementDatas[i].currentLv;

                PlayerData.Instance.achivementDatas.Add(achivementData);
            }

            PlayerData.Instance.achivCoin = achivCoin;
            PlayerData.Instance.achivSpenCoin = achivSpenCoin;
            PlayerData.Instance.achivCrystal = achivCrystal;
            PlayerData.Instance.achivSpenCrystal = achivSpenCrystal;
            PlayerData.Instance.achivSpenEnergy = achivSpenEnergy;

            PlayerData.Instance.achivWin = achivWin;
            PlayerData.Instance.achivLose = achivLose;
            PlayerData.Instance.achivGoodCusOk = achivGoodEmoCus;
            PlayerData.Instance.achivBadCus = achivBadCus;
            PlayerData.Instance.achivOrderOk = achivOrderOk;
            PlayerData.Instance.achivComLv = achivComLv;

            PlayerData.Instance.foxTime = foxTime;
            PlayerData.Instance.foxTouched = foxTouched;

            PlayerData.Instance.musicOn = musicOn;
            PlayerData.Instance.soundOn = soundOn;

            PlayerData.Save();
            save = false;
        }
    }

    // Increase
    public void IncreaseCrystal(int amount)
    {
        crystal += amount;
        todayEarnCrystal += amount;
        achivCrystal += amount;

        save = true;
    }

    /*public void IncreaseEnergy(int amount)
    {
        if (energy < 10)
        {
            energy += amount;
            if(energy > 10)
            {
                energy = 10;
            }

            save = true;
        }
    }*/

    public void IncreaseCoin(int amount)
    {
        coin += amount;
        todayEarnCoin += amount;
        achivCoin += amount;
        save = true;
    }

    //Decrease
    public void DecreaseCrystal(int amount)
    {
        if(crystal >= amount)
        {
            crystal -= amount;
            todaySpendCrystal += amount;
            achivSpenCrystal += amount;

            save = true;
        }
    }
    /*public void DecreaseEnergy(int amount)
    {
        if(energy >= amount)
        {
            energy -= amount;

            achivSpenEnergy += amount;

            save = true;
        }
    }*/

    public void DecreaseCoin(int amount)
    {
        if(coin >= amount)
        {
            coin -= amount;
            todaySpendCoin += amount;
            achivSpenCoin += amount;

            save = true;
        }
    }
}
