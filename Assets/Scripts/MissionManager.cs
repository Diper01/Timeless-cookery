using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FoodRate
{
    public FoodType foodType;
    public string id = "";
    public int rate = 0;
}

public class MissionManager : MonoBehaviour
{
    private CustomerManager customerManager;
    //private AdManager adManager;

    public int scenePos = 0; //=0 worldmap, =1 gameplay, =2 upgradeshop
    public int currentWorld = 0;

    public bool open = false;
    public bool close = false;

    public int orderOkCount = 0;

    public int winCount = 0;
    public int loseCount = 0;

    [Header("MainSetting")]
    public int levelID = 1;
    public int currentStar = 0;
    public int starSet = 0;
    public bool hasCrystal;
    public int rewardCrystal = 2;
    public bool playAgain = false;

    [System.Serializable]
    public enum GameMode { Normal, Boss }
    [System.Serializable]
    public enum FirstTarget { Time, Customer }
    [System.Serializable]
    public enum SecondTarget { Null, Coin, Dish, Like }

    public GameMode gamemode;

    [Header("First Target")]
    public FirstTarget firstTarget;
    public int firstTargetStats = 0; //= 0 target is not done, = 1 target is done, = 3 target is failed
    [Header("Time")]
    public float missionTimeLimit = 90f;
    public float currentTime = 1f;
    public bool outOfTimeClosed = false;

    [Header("Customer")]
    public float customerSpawnTime = 5f;
    public int customerLimit = 15;
    public int customerCondition = 10;
    public int customerLeft = 1;
    public int currentCustomerCount = 0;
    public int customerCount = 0;
    public int customerPaidCount = 0;
    public bool outOfCustomerClosed = false;

    [Header("Second Target")]
    public SecondTarget secondTarget;
    public int secondTargetStats = 0; //= 0 target is not done, = 1 target is done, = 3 target is failed
    [Header("Coin")]
    public int coinCondition = 70;
    public int currentPlayerCoin = 0;

    [Header("Dish")]
    public int dishCondition = 0;
    public int dishCount = 0;

    [Header("Like")]
    public int customerGoodCondition = 0;
    public int customerGoodEmoCount = 0;

    [Header("Banned Mission")]

    public bool lostCutomerMission = false;
    public bool lostCustomerClosed = false;
    public int allowAngryCustomer = 0;
    public int customerAngryCount = 0;

    public bool foodToTrashMission = false;
    public bool foodToTrashClosed = false;
    public int allowTrashFood = 0;
    public int foodToTrashCount = 0;

    public bool friedFoodMission = false;
    public bool friedFoodClosed = false;
    public int friedFoodCount = 0;
    public int allowFriedFood = 0;

    [Header("Available Boosters")]
    public bool useWater = false;
    public bool useMushRoom = false;
    public bool useX2Gold = false;

    [Header("Customer Order Rate")]
    public List<FoodRate> foodRates = new List<FoodRate>();

    public int orderSize = 2;

    public int oneOrderRate;
    public int twoOrderRate;
    public int threeOrderRate;
    public int mainRate;

    [Header("Other")]
    public int currentLoseCount = 0;

    public bool isFreeze = false;

    private void Awake()
    {
        _MakeSingleInstance();
    }

    void _MakeSingleInstance()
    {
        int numInstance = GameObject.FindGameObjectsWithTag("MissionManager").Length;
        if (numInstance > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        currentTime = missionTimeLimit + 1;
    }

    private void Update()
    {
        if (open == true)
        {
            _Timer();
        }
    }

    void _Timer()
    {

        if(open == false)
        {
            return;
        }

        if(isFreeze == true)
        {
            return;
        }

        if (firstTarget == FirstTarget.Time && open == true /*&& close == false*/)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            if(currentTime <= 0)
            {
                currentTime = 0;
            }
        }
    }

    public void _ResetMission()
    {
        playAgain = false;

        open = false;
        close = false;

        currentStar = 0;

        orderOkCount = 0;

        currentTime = missionTimeLimit;
        outOfTimeClosed = false;

        customerSpawnTime = 0;
        customerLeft = customerLimit;
        currentCustomerCount = 0;
        customerCount = 0;
        customerPaidCount = 0;
        customerGoodEmoCount = 0;
        outOfCustomerClosed = false;

        currentPlayerCoin = 0;

        dishCount = 0;

        customerGoodEmoCount = 0;

        lostCutomerMission = false;
        lostCustomerClosed = false;
        allowAngryCustomer = 0;
        customerAngryCount = 0;

        foodToTrashMission = false;
        foodToTrashClosed = false;
        allowTrashFood = 0;
        foodToTrashCount = 0;

        friedFoodMission = false;
        friedFoodClosed = false;
        friedFoodCount = 0;
        allowFriedFood = 0;

        useWater = false;
        useMushRoom = false;
        useX2Gold = false;
    }

    public void _RetryMission()
    {
        open = false;
        close = false;

        orderOkCount = 0;

        currentTime = missionTimeLimit;
        outOfTimeClosed = false;

        customerLeft = customerLimit;
        currentCustomerCount = 0;
        customerCount = 0;
        customerPaidCount = 0;
        customerGoodEmoCount = 0;
        outOfCustomerClosed = false;

        currentPlayerCoin = 0;

        dishCount = 0;

        customerGoodEmoCount = 0;

        lostCustomerClosed = false;
        customerAngryCount = 0;

        foodToTrashClosed = false;
        foodToTrashCount = 0;

        friedFoodClosed = false;
        friedFoodCount = 0;
    }
}
