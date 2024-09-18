using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Boss : MonoBehaviour
{
    private MissionManager missionManager;

    public BossOrder[] bossOrders;

    public SkeletonAnimation bossSpine;

    public AudioClip angryAudio, orderDoneAudio, happyAudio;

    //public string tempFoodId = "";
    //public int tempCost = 0;

    public int totalOrder = 0;
    public int currentOrdered = 0;
    public int maxOrderSize = 2;
    public int orderDone = 0;

    private float activeOrderTime;
    private bool orderActived = false;

    public Customer.FoodList[] foodLists;
    public List<FoodRate> canOrderFoodRates = new List<FoodRate>();

    public int totalOrderRate;
    [Range(0, 100)]
    public int mainRate = 50;

    // Start is called before the first frame update
    void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();

        _GetMissionManagerValues();
        _SetFoodCost();
        _SetCanUseFood();
        _SetCanOrderFoodRates();

        StartCoroutine(_WaitToActiveOrder());

        _animationAngry();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void _GetMissionManagerValues()
    {
        totalOrder = missionManager.dishCondition;
        maxOrderSize = missionManager.orderSize;
        mainRate = missionManager.mainRate;
    }

    void _SetFoodCost()
    {
        GameObject[] foodControllers = GameObject.FindGameObjectsWithTag("FoodController");
        GameObject[] sideFoodControllers = GameObject.FindGameObjectsWithTag("SideFoodController");

        for (int i = 0; i < foodLists.Length; i++)
        {
            for (int j = 0; j < foodControllers.Length; j++)
            {
                if (foodLists[i].id == foodControllers[j].GetComponent<FoodController>().foodId)
                {
                    foodLists[i].foodCost = foodControllers[j].GetComponent<FoodController>().foodCost;
                    break;
                }
            }
        }

        for (int i = 0; i < foodLists.Length; i++)
        {
            for (int j = 0; j < sideFoodControllers.Length; j++)
            {
                if (foodLists[i].id == sideFoodControllers[j].GetComponent<SideFoodController>().foodId)
                {
                    foodLists[i].foodCost = sideFoodControllers[j].GetComponent<SideFoodController>().foodCost;
                    break;
                }
            }
        }
    }

    void _SetCanUseFood()
    {
        GameObject[] foodController = GameObject.FindGameObjectsWithTag("FoodController");
        GameObject[] sideFoodController = GameObject.FindGameObjectsWithTag("SideFoodController");

        for (int i = 0; i < foodController.Length; i++)
        {
            if (foodController[i].gameObject.activeSelf == true)
            {
                for (int j = 0; j < foodLists.Length; j++)
                {
                    if (foodLists[j].foodType.ToString() == "MainFood"
                        && foodLists[j].id == foodController[i].GetComponent<FoodController>().foodId)
                    {
                        foodLists[j].fistFood = foodController[i].GetComponent<FoodController>().isFirstFood;
                        foodLists[j].canOrder = true;
                    }
                }
            }
        }

        for (int i = 0; i < sideFoodController.Length; i++)
        {
            if (sideFoodController[i].gameObject.activeSelf == true)
            {
                for (int j = 0; j < foodLists.Length; j++)
                {
                    if (foodLists[j].foodType.ToString() == "SideFood"
                        && foodLists[j].id == sideFoodController[i].GetComponent<SideFoodController>().foodId)
                    {
                        foodLists[j].fistFood = sideFoodController[i].GetComponent<SideFoodController>().isFirstFood;
                        foodLists[j].canOrder = true;
                    }
                }
            }
        }
    }

    void _SetCanOrderFoodRates()
    {
        for (int i = 0; i < missionManager.foodRates.Count; i++)
        {
            int correctCount = 0;

            for (int j = 0; j < missionManager.foodRates[i].id.Length; j++)
            {
                for (int k = 0; k < foodLists.Length; k++)
                {
                    if (missionManager.foodRates[i].id[j].ToString() == foodLists[k].id && foodLists[k].canOrder == true)
                    {
                        correctCount += 1;
                    }
                }

                if (correctCount == missionManager.foodRates[i].id.Length)
                {
                    FoodRate foodRate = new FoodRate();

                    foodRate.foodType = missionManager.foodRates[i].foodType;
                    foodRate.id = missionManager.foodRates[i].id;
                    foodRate.rate = missionManager.foodRates[i].rate;

                    canOrderFoodRates.Add(foodRate);
                }
            }
        }
    }

    IEnumerator _WaitToActiveOrder()
    {
        yield return new WaitForSeconds(activeOrderTime);

        _ActiveOrder();

        yield break;
    }

    public void _ActiveOrder()
    {
        if (currentOrdered < totalOrder)
        {
            for (int i = 0; i < bossOrders.Length; i++)
            {
                BossOrder bossOrder = bossOrders[i];

                for (int j = 0; j < bossOrder.bossOrderPos.Length; j++)
                {
                    if (j < maxOrderSize && currentOrdered < totalOrder)
                    {
                        BossOrderPos bossOrderPos = bossOrder.bossOrderPos[j];

                        bossOrderPos.boss = this;
                        bossOrderPos.bossOrder = bossOrder;

                        string tempFoodId = "";
                        int tempCost = 0;

                        tempFoodId = _SetCurrentOrder();
                        tempCost = _SetOrderCost(tempFoodId);

                        bossOrderPos.orderID = tempFoodId;
                        bossOrderPos.orderCost = tempCost;

                        bossOrderPos._DeactiveOrderImgs();
                        bossOrderPos._ActiveOrderImages();

                        bossOrderPos.foodPos.SetActive(true);
                        bossOrderPos.gameObject.SetActive(true);

                        if (bossOrder.gameObject.activeSelf == false)
                        {
                            bossOrder.gameObject.SetActive(true);
                            bossOrder.gameObject.GetComponent<TweenScale>().enabled = true;
                        }
                    }

                }

            }
        }
    }

    public void _RenewOrder()
    {
        if (currentOrdered < totalOrder)
        {
            for (int i = 0; i < bossOrders.Length; i++)
            {
                BossOrder bossOrder = bossOrders[i];

                for (int j = 0; j < bossOrder.bossOrderPos.Length; j++)
                {
                    if (j < maxOrderSize && currentOrdered < totalOrder)
                    {
                        BossOrderPos bossOrderPos = bossOrder.bossOrderPos[j];

                        if (bossOrderPos.orderID == "")
                        {
                            bossOrderPos.boss = this;

                            string tempFoodId = "";
                            int tempCost = 0;

                            tempFoodId = _SetCurrentOrder();
                            tempCost = _SetOrderCost(tempFoodId);

                            bossOrderPos.orderID = tempFoodId;
                            bossOrderPos.orderCost = tempCost;

                            bossOrderPos._DeactiveOrderImgs();
                            bossOrderPos._ActiveOrderImages();

                            bossOrderPos.foodPos.SetActive(true);
                            bossOrderPos.gameObject.SetActive(true);


                            if (bossOrder.gameObject.activeSelf == false)
                            {
                                bossOrder.gameObject.SetActive(true);
                                bossOrder.gameObject.GetComponent<TweenScale>().enabled = true;
                            }
                        }
                    }
                }
            }
        }
    }

    string _SetCurrentOrder()
    {
        currentOrdered += 1;

        int totalRate = 0;
        int rateTemp = 0;

        string currentId = "";

        int msRate = Random.Range(0, 100);

        if (msRate <= mainRate)
        {
            //lay total rate main food
            for (int i = 0; i < canOrderFoodRates.Count; i++)
            {
                if (canOrderFoodRates[i].rate > 0 && canOrderFoodRates[i].foodType == FoodType.MainFood)
                {
                    totalRate += canOrderFoodRates[i].rate;
                }
            }

            //Lua chon ket hop
            for (int i = 0; i < canOrderFoodRates.Count; i++)
            {
                if (canOrderFoodRates[i].rate > 0 && canOrderFoodRates[i].foodType == FoodType.MainFood)
                {
                    int rate = Random.Range(0, totalRate);

                    rateTemp += canOrderFoodRates[i].rate;

                    //Debug.Log("Rate = " + rate);
                    //Debug.Log("MainFood RateTemp = " + rateTemp);

                    if (rate <= rateTemp)
                    {
                        currentId = canOrderFoodRates[i].id;
                        break;
                    }
                    //Debug.Log("MainFood = " + canOrderFoodRates[i].id);
                }
            }
        }
        else if (msRate > mainRate)
        {
            //lay total rate side food
            for (int i = 0; i < canOrderFoodRates.Count; i++)
            {
                if (canOrderFoodRates[i].rate > 0 && canOrderFoodRates[i].foodType == FoodType.SideFood)
                {
                    totalRate += canOrderFoodRates[i].rate;
                }
            }

            //Lua chon ket hop
            for (int i = 0; i < canOrderFoodRates.Count; i++)
            {
                if (canOrderFoodRates[i].rate > 0 && canOrderFoodRates[i].foodType == FoodType.SideFood)
                {
                    int rate = Random.Range(0, totalRate);

                    rateTemp += canOrderFoodRates[i].rate;

                    //Debug.Log("Rate = " + rate);
                    //Debug.Log("SideFood RateTemp = " + rateTemp);

                    if (rate <= rateTemp)
                    {
                        currentId = canOrderFoodRates[i].id;
                        break;
                    }
                    //Debug.Log("SideFood = " + canOrderFoodRates[i].id);
                }
            }
        }

        if (currentId == "")
        {
            currentOrdered -= 1;
            return _SetCurrentOrder();
        }
        else
        {
            return currentId;
        }

        //return currentId;
    }



    public int _SetOrderCost(string currentOrderId)
    {
        int currentCost = 0;

        for (int i = 0; i < currentOrderId.Length; i++)
        {
            for (int j = 0; j < foodLists.Length; j++)
            {
                if (currentOrderId[i].ToString() == foodLists[j].id)
                {
                    currentCost += foodLists[j].foodCost;
                    break;
                }
            }
        }

        return currentCost;
    }

    public void _animationAngry()
    {
        bossSpine.AnimationState.SetAnimation(0, "angry", true);
        _audioAngry();
    }

    public IEnumerator _animHappy()
    {
        _audioOrderDone();

        bossSpine.AnimationState.SetAnimation(0, "happy", false);
        var myAnim = bossSpine.skeleton.Data.FindAnimation("happy");
        yield return new WaitForSeconds(myAnim.Duration);

        if(missionManager.dishCount < missionManager.dishCondition)
        {
            _animationAngry();
        }
        else
        {
            _audioOrderHappy();
        }

        yield break;
    }

    //PLay Audio

    public void _audioAngry()
    {
        gameObject.GetComponent<AudioSource>().clip = angryAudio;
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void _audioOrderDone()
    {
        gameObject.GetComponent<AudioSource>().clip = orderDoneAudio;
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void _audioOrderHappy()
    {
        gameObject.GetComponent<AudioSource>().clip = happyAudio;
        gameObject.GetComponent<AudioSource>().Play();
    }
}
