using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetting : MonoBehaviour
{
    private MissionManager missionManager;
    private PlayerStats playerStats;
    private LevelManager levelManager;
    public LevelSettingsDataManager levelSettingsDataManager;

    //private GoogleFireBaseEvents googleFireBaseEvents;

    public LevelController levelController;
    public GameObject levelDetailUi;

    [Header("Level Settings")]
    public int levelID = 1;
    public int starSet = 0;
    public int currentStar = 0;

    [Header("Crystal rewards")]
    public bool hasCrystal;
    public int rewardCrystal = 2;

    public MissionManager.GameMode gameMode = MissionManager.GameMode.Normal;

    [Header("FirstMission")]
    public MissionManager.FirstTarget firstTarget;

    [Header("Time")]
    public float missionTimeLimit = 90f;

    [Header("Customer")]
    public float customerSpawnTime = 5f;
    public int customerLimit = 15;
    public int customerCondition = 10;

    [Header("SecondMission")]
    public MissionManager.SecondTarget secondTarget;

    [Header("Coin")]
    public int coinCondition = 70;

    [Header("Dish")]
    public int dishCondition = 10;

    [Header("Like")]
    public int customerGoodCondition = 10;

    [Header("Banned Mission Settings")]
    public bool lostCutomerMission = false;
    public int allowAngryCustomer = 0;

    public bool foodToTrashMission = false;
    public int allowTrashFood = 0;

    public bool friedFoodMission = false;
    public int allowFriedFood = 0;

    [Header("Customer Order Rate")]
    public List<FoodRate> foodRates = new List<FoodRate>();

    [Range(1, 3)]
    public int orderSize = 2;

    public int oneOrderRate;
    public int twoOrderRate;
    public int threeOrderRate;
    public int mainRate;

    private void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        levelSettingsDataManager = GameObject.FindGameObjectWithTag("LevelSettingsDataManager").GetComponent<LevelSettingsDataManager>();

        //googleFireBaseEvents = FindObjectOfType<GoogleFireBaseEvents>();
    }

    private void Update()
    {

    }

    void _SetLevelDetailUI()
    {
        levelDetailUi.GetComponent<LevelDetailUI>().star = currentStar;
        levelDetailUi.GetComponent<LevelDetailUI>().firstTarget.GetComponent<FirstTargetObject>()._SetActiveImage();
        levelDetailUi.GetComponent<LevelDetailUI>().secondTarget.GetComponent<SecondTargetObject>()._SetActiveImage();
        levelDetailUi.GetComponent<LevelDetailUI>().thirdTarget.GetComponent<ThirdTargetObject>()._SetActiveImage();
    }

    public void _OnButtonDown()
    {
        _GetSetting();

        missionManager._ResetMission();
        _SetValuesToMissionManager();

        if(gameMode == MissionManager.GameMode.Boss && currentStar >=3)
        {
            _ActiveBossClearDialog();
        }
        else
        {
            _SetLevelDetailUI();
            levelDetailUi.GetComponent<UIScaleAnimation>().enabled = true;
        }
    }

    public void FireBaseChecker()
    {
        if(levelID == 1 && starSet == 0)
        {
            //googleFireBaseEvents.ClickPlayMainMenu();
        }
        else
        {
            //googleFireBaseEvents.ClickPlayWorldMap();
        }
    }

    void _SetValuesToMissionManager()
    {

        missionManager.levelID = levelID;
        missionManager.starSet = starSet;

        missionManager.gamemode = gameMode;

        //Key and Crystal rewards
        missionManager.currentStar = currentStar;
        missionManager.hasCrystal = hasCrystal;
        missionManager.rewardCrystal = rewardCrystal;

        //first target
        missionManager.firstTarget = firstTarget;

        //Time Mission
        missionManager.missionTimeLimit = missionTimeLimit;
        missionManager.currentTime = missionTimeLimit;

        //Customer Mission
        missionManager.customerSpawnTime = customerSpawnTime;
        missionManager.customerLimit = customerLimit;
        missionManager.customerCondition = customerCondition;

        //second target
        missionManager.secondTarget = secondTarget;

        //Coin Mission
        missionManager.coinCondition = coinCondition;

        //Dish Mission
        missionManager.dishCondition = dishCondition;

        //Like Mission
        missionManager.customerGoodCondition = customerGoodCondition;
        missionManager.customerLeft = customerLimit;

        //Banned Mission
        missionManager.lostCutomerMission = lostCutomerMission;
        missionManager.allowAngryCustomer = allowAngryCustomer;

        missionManager.foodToTrashMission = foodToTrashMission;
        missionManager.allowTrashFood = allowTrashFood;

        missionManager.friedFoodMission = friedFoodMission;
        missionManager.allowFriedFood = allowFriedFood;

        missionManager.orderSize = orderSize;
        missionManager.oneOrderRate = oneOrderRate;
        missionManager.twoOrderRate = twoOrderRate;
        missionManager.threeOrderRate = threeOrderRate;
        missionManager.mainRate = mainRate;

        _SetBossModeDefault();

        missionManager.foodRates.Clear();
        for(int i=0; i<foodRates.Count; i++)
        {
            FoodRate foodRate = new FoodRate();

            foodRate.foodType = foodRates[i].foodType;
            foodRate.id = foodRates[i].id;
            foodRate.rate = foodRates[i].rate;

            missionManager.foodRates.Add(foodRate);
        }
    }

    void _GetSetting()
    {
        for (int i = 0; i < levelSettingsDataManager.levelDataSets.Count; i++)
        {
            LevelDataSet levelDataSet = levelSettingsDataManager.levelDataSets[i];

            if (levelID == levelDataSet.levelID && starSet == levelDataSet.starSet)
            {
                hasCrystal = levelDataSet.hasCrystal;
                rewardCrystal = levelDataSet.rewardCrystal;

                //First Target
                if (levelDataSet.firstTarget == MissionManager.FirstTarget.Time.ToString())
                {
                    firstTarget = MissionManager.FirstTarget.Time;
                }
                else if (levelDataSet.firstTarget == MissionManager.FirstTarget.Customer.ToString())
                {
                    firstTarget = MissionManager.FirstTarget.Customer;
                }
                //Time
                missionTimeLimit = levelDataSet.missionTimeLimit;
                //Customer
                customerSpawnTime = levelDataSet.customerSpawnTime;
                customerLimit = levelDataSet.customerLimit;
                customerCondition = levelDataSet.customerCondition;

                //Second Target
                if (levelDataSet.secondTarget == MissionManager.SecondTarget.Null.ToString())
                {
                    secondTarget = MissionManager.SecondTarget.Null;
                }
                else if (levelDataSet.secondTarget == MissionManager.SecondTarget.Coin.ToString())
                {
                    secondTarget = MissionManager.SecondTarget.Coin;
                }
                else if (levelDataSet.secondTarget == MissionManager.SecondTarget.Dish.ToString())
                {
                    secondTarget = MissionManager.SecondTarget.Dish;
                }
                else if (levelDataSet.secondTarget == MissionManager.SecondTarget.Like.ToString())
                {
                    secondTarget = MissionManager.SecondTarget.Like;
                }
                //Coin
                coinCondition = levelDataSet.coinCondition;
                //Dish
                dishCondition = levelDataSet.dishCondition;
                //Like
                customerGoodCondition = levelDataSet.customerGoodCondition;

                lostCutomerMission = levelDataSet.lostCutomerMission;
                allowAngryCustomer = levelDataSet.allowAngryCustomer;

                foodToTrashMission = levelDataSet.foodToTrashMission;
                allowTrashFood = levelDataSet.allowTrashFood;

                friedFoodMission = levelDataSet.friedFoodMission;
                allowFriedFood = levelDataSet.allowFriedFood;

                foodRates.Clear();

                for (int m = 0; m < levelDataSet.foodRates.Count; m++)
                {
                    FoodRate foodRate = new FoodRate();

                    if (levelDataSet.foodRates[m].foodType == FoodType.MainFood.ToString())
                    {
                        foodRate.foodType = FoodType.MainFood;
                    }
                    else if (levelDataSet.foodRates[m].foodType == FoodType.SideFood.ToString())
                    {
                        foodRate.foodType = FoodType.SideFood;
                    }

                    foodRate.id = levelDataSet.foodRates[m].id;
                    foodRate.rate = levelDataSet.foodRates[m].rate;

                    foodRates.Add(foodRate);
                }

                orderSize = levelDataSet.orderSize;
                oneOrderRate = levelDataSet.oneOrderRate;
                twoOrderRate = levelDataSet.twoOrderRate;
                threeOrderRate = levelDataSet.threeOrderRate;
                mainRate = levelDataSet.mainRate;
            }
        }
    }

    void _SetBossModeDefault()
    {
        if (gameMode == MissionManager.GameMode.Boss)
        {
            _GetBosssModeSetting();

            if (firstTarget != MissionManager.FirstTarget.Time)
            {
                firstTarget = MissionManager.FirstTarget.Time;
                missionManager.firstTarget = firstTarget;

                missionManager.missionTimeLimit = 45f;
                missionManager.currentTime = 45f;
            }

            if(secondTarget != MissionManager.SecondTarget.Dish)
            {
                secondTarget = MissionManager.SecondTarget.Dish;
                missionManager.secondTarget = MissionManager.SecondTarget.Dish;

                dishCondition = 15;
                missionManager.dishCondition = 15;
            }
        }
    }

    void _GetBosssModeSetting()
    {
        for(int i=0; i< levelController.levelSettings.Length; i++)
        {
            LevelSetting lvSet = levelController.levelSettings[i].GetComponent<LevelSetting>();

            if (lvSet.gameMode == MissionManager.GameMode.Boss && lvSet.starSet != starSet)
            {
                missionManager.gamemode = lvSet.gameMode;

                //first target
                missionManager.firstTarget = lvSet.firstTarget;

                //Time Mission
                missionManager.missionTimeLimit = lvSet.missionTimeLimit;
                missionManager.currentTime = lvSet.missionTimeLimit;

                //Customer Mission
                missionManager.customerSpawnTime = lvSet.customerSpawnTime;
                missionManager.customerLimit = lvSet.customerLimit;
                missionManager.customerCondition = lvSet.customerCondition;

                //second target
                missionManager.secondTarget = lvSet.secondTarget;

                //Coin Mission
                missionManager.coinCondition = lvSet.coinCondition;

                //Dish Mission
                missionManager.dishCondition = lvSet.dishCondition;

                //Like Mission
                missionManager.customerGoodCondition = lvSet.customerGoodCondition;
                missionManager.customerLeft = lvSet.customerLimit;

                //Banned Mission
                missionManager.lostCutomerMission = lvSet.lostCutomerMission;
                missionManager.allowAngryCustomer = lvSet.allowAngryCustomer;

                missionManager.foodToTrashMission = lvSet.foodToTrashMission;
                missionManager.allowTrashFood = lvSet.allowTrashFood;

                missionManager.friedFoodMission = lvSet.friedFoodMission;
                missionManager.allowFriedFood = lvSet.allowFriedFood;

                missionManager.orderSize = lvSet.orderSize;
                missionManager.oneOrderRate = lvSet.oneOrderRate;
                missionManager.twoOrderRate = lvSet.twoOrderRate;
                missionManager.threeOrderRate = lvSet.threeOrderRate;
                missionManager.mainRate = lvSet.mainRate;

                missionManager.foodRates.Clear();
                for (int j = 0; j < lvSet.foodRates.Count; j++)
                {
                    FoodRate foodRate = new FoodRate();

                    foodRate.foodType = lvSet.foodRates[j].foodType;
                    foodRate.id = lvSet.foodRates[j].id;
                    foodRate.rate = lvSet.foodRates[j].rate;

                    missionManager.foodRates.Add(foodRate);
                }

                return;
            }
        }
    }

    public void _ActiveBossClearDialog()
    {
        WorldMapController worldMapController = FindObjectOfType<WorldMapController>();

        worldMapController.openDialogBossClear();
    }
}
