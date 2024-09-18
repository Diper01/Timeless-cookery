using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine;
using Spine.Unity;

[System.Serializable]
public enum FoodType { MainFood, SideFood }


[RequireComponent(typeof(AudioSource))]
public class Customer : MonoBehaviour
{
    private GamePlayMenuManager gamePlayMenuManager;
    private CustomerManager customerManager;
    private MissionManager missionManager;
    private Combo combo;

    private GameObject[] customerPos;

    public GameObject[] leavePositions;

    public GameObject posToLeave;

    private GameObject target;

    public GameObject order;

    public GameObject coinImage;

    public SkeletonAnimation customerSpine;

    public int oneOrderRate = 30;
    public int twoOderRate = 35;
    public int threeOrderRate = 35;

    public int mainRate = 50;

    public int orderSize = 0;
    public int currentOrderSize = 0;
    public int orderDoneCount = 0;

    public float speed = 1f;
    private Vector3 pos;
    private Vector3 currentLocalScale;
    public bool rightFace;

    public bool isFreeze = false;
    public bool isMushroomed = false;
    public bool isOnPos = false;
    public bool ordered = false;

    public bool orderIsOk = false;
    public bool isLeaving = false;
    public bool moveFromLeft = false;
    public bool moveFromRight = false;

    public string customerName;
    public float customerWaitTime = 15f;
    public float currentWaitTime = 0f;
    public float normalTime = 10f;
    public float angryTime = 5f;

    // customerEmo =0 notthing, customerEmo =1 good, customerEmo =2 normal , customerEmo =3 bad, customerEmo =4 angry
    public int customerEmo = 0;
    public GameObject likeImg;
    // customerAnimStat = 0 move, customerAnimStat
    public int customerAnimStat = 0;

    [Header("Audio")]
    public AudioClip goodEmoAudio;
    public AudioClip angryAudio;

    [Header("Order")]
    public GameObject freezeSpine;
    public int freezeStats = 0; //=1 dong bang =0 het dong bang
    public Vector3 freezeSpineScale = Vector3.zero;

    public GameObject firstOrderPos;
    public string currentFirstOrderId = "";
    public int firstOrderCost = 0;
    public int firstOrderStats = 0; // = 0 order is not ok, = 1 order isok, = 2 order is locked, = 3 order is paid, = 4 deactive

    public GameObject secondOrderPos;
    public string currentSecondOrderId = "";
    public int secondOrderCost = 0;
    public int secondOrderStats = 0; // = 0 order is not ok, = 1 order isok, = 2 order is locked, = 3 order is paid, = 4 deactive

    public GameObject thirdOrderPos;
    public string currentThirdOrderId = "";
    public int thirdOrderCost = 0;
    public int thirdOrderStats = 0; // = 0 order is not ok, = 1 order isok, = 2 order is locked, = 3 order is paid, = 4 deactive

    public int tempCoin = 0;

    public GameObject timerFiller;
    public GameObject timeProcessbar1;
    public GameObject timeProcessbar2;
    public GameObject timeProcessbar3;

    // wait time to leave after orderIsOk = true
    private float waitToLeave = 1f;
    private float hideOrderTime = 1f;

    [System.Serializable]
    public class FoodList
    {
        public FoodType foodType;
        public string id = "";
        public int foodCost = 0;
        public int rate = 0;
        public bool fistFood = false;
        public bool canOrder = false;
    }
    public FoodList[] foodLists;

    public List<FoodRate> canOrderFoodRates = new List<FoodRate>();

    public int totalOrderRate;

    void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        combo = GameObject.FindGameObjectWithTag("Combo").GetComponent<Combo>();
        customerManager = GameObject.FindGameObjectWithTag("CustomerManager").GetComponent<CustomerManager>();
        gamePlayMenuManager = GameObject.FindGameObjectWithTag("GamePlayMenuManager").GetComponent<GamePlayMenuManager>();

        freezeSpine.SetActive(false);

        if (missionManager.secondTarget != MissionManager.SecondTarget.Like)
        {
            likeImg.SetActive(false);
        }

        customerEmo = 1;

        customerManager.currentCustomerCount++;

        animationHeadIdle();
        animationBodyMove();

        pos = transform.position;
        currentLocalScale = transform.localScale;

        currentWaitTime = customerWaitTime;

        timeProcessbar1.SetActive(true);

        _GetMissionManagerValues();

        _DeactiveOrder();

        _SetFoodCost();
        _SetCanUseFood();
        //_SetFoodListRate();
        _SetCanOrderFoodRates();

        _CheckEmptyPos();
        _SetLeavePos();
    }

    void Update()
    {
        if (gamePlayMenuManager.win == true || gamePlayMenuManager.lose == true)
        {
            return;
        }
            _FreezeSpineAnimation();
            _CustomerWaiting();
            _ActiveOrder();
            _MoveCustomerToPos();

            _Leave();
            _DeactiveOrder();
    }

    void _GetMissionManagerValues()
    {
        orderSize = missionManager.orderSize;

        oneOrderRate = missionManager.oneOrderRate;
        twoOderRate = missionManager.twoOrderRate;
        threeOrderRate = missionManager.twoOrderRate;
        mainRate = missionManager.mainRate;
    }

    void _CustomerWaiting()
    {
        if (isOnPos == true && isLeaving == false && ordered == true && isFreeze == false)
        {
            currentWaitTime -= Time.deltaTime;
            float temp = currentWaitTime / customerWaitTime;
            Vector3 scale = new Vector3(1, temp, 1);

            timerFiller.transform.localScale = scale;

            if (isMushroomed == false)
            {
                if (currentWaitTime > customerWaitTime / 2)
                {
                    customerEmo = 1;
                }

                if (currentWaitTime < customerWaitTime / 2)
                {
                    if (likeImg.activeSelf == true)
                    {
                        likeImg.SetActive(false);
                    }

                    if (customerEmo == 1)
                    {
                        customerEmo = 2;
                    }
                }

                if (currentWaitTime < customerWaitTime / 4)
                {
                    if (customerEmo == 2)
                    {
                        customerEmo = 3;

                        _animFreezeWarning();
                        animationHeadAngry();
                        animationBodyAngry();
                    }
                }

                if (currentWaitTime <= 0f)
                {
                    customerEmo = 4;
                }
            }

            if (customerEmo == 3 && currentWaitTime < customerWaitTime / 4)
            {
                timeProcessbar1.SetActive(false);
                timeProcessbar2.SetActive(false);
                timeProcessbar3.SetActive(true);
            }
            else if (customerEmo == 2 && currentWaitTime < customerWaitTime / 2 && currentWaitTime > customerWaitTime / 4)
            {
                timeProcessbar1.SetActive(false);
                timeProcessbar2.SetActive(true);
                timeProcessbar3.SetActive(false);
            }
            else if (customerEmo == 1 && currentWaitTime > customerWaitTime / 2)
            {
                timeProcessbar1.SetActive(true);
                timeProcessbar2.SetActive(false);
                timeProcessbar3.SetActive(false);
            }
        }
    }

    void _CheckEmptyPos()
    {
        customerPos = GameObject.FindGameObjectsWithTag("CustomerPositions");

        for (int i = 0; i < customerPos.Length; i++)
        {
            if (customerPos[i].GetComponent<CustomerPos>().isEmpty == true)
            {
                target = customerPos[i].GetComponent<CustomerPos>().gameObject;
                customerPos[i].GetComponent<CustomerPos>().isEmpty = false;
                //Debug.Log("vi tri " + customerPos[i].GetComponent<CustomerPos>().name + " da co khach va isEmpty = " + customerPos[i].GetComponent<CustomerPos>().isEmpty);
                break;
            }
        }
    }

    void _MoveCustomerToPos()
    {
        if (moveFromRight == true && transform.position.x <= target.transform.position.x)
        {
            if (!isOnPos)
            {
                isOnPos = true;
                // Set Animation
                animationBodyIdle();
            }

        }
        else if (moveFromLeft == true && transform.position.x >= target.transform.position.x)
        {
            if (!isOnPos)
            {
                isOnPos = true;
                // Set Animation
                animationBodyIdle();
            }
        }

        if (isOnPos == false && target != null && orderIsOk == false)
        {
            if (transform.position.x > target.GetComponent<CustomerPos>().gameObject.transform.position.x)
            {
                moveFromRight = true;
                currentLocalScale.x = -1f;
                pos -= transform.right * Time.deltaTime * speed;
                transform.position = pos;
                //transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
            }
            else
            if (transform.position.x < target.GetComponent<CustomerPos>().gameObject.transform.position.x)
            {
                moveFromLeft = true;
                currentLocalScale.x = 1f;
                pos += transform.right * Time.deltaTime * speed;
                transform.position = pos;
                //transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
            }

            transform.localScale = currentLocalScale;
        }

        else if (isOnPos == true && target != null)
        {
            transform.position = target.transform.position;
            currentLocalScale.x = 1f;
            transform.localScale = currentLocalScale;

            if (ordered == false)
            {
                _SetRandomOrder();
            }
        }
    }

    void _ActiveOrder()
    {
        if (isOnPos == true)
        {
            order.SetActive(true);
            order.GetComponent<TweenScale>().from = new Vector3(0f, 0f, 0f);
            order.GetComponent<TweenScale>().to = new Vector3(1f, 1f, 1f);
            order.GetComponent<TweenScale>().duration = 0.5f;
            order.GetComponent<TweenScale>().enabled = true;
            order.GetComponent<TweenScale>().PlayForward();
        }
    }

    void _DeactiveOrder()
    {
        if (isOnPos == false && ordered == false || currentWaitTime <= 0 && ordered == true)
        {
            order.SetActive(false);
            firstOrderPos.SetActive(false);
            secondOrderPos.SetActive(false);
            thirdOrderPos.SetActive(false);
        }
        else
        if (orderIsOk == true)
        {
            hideOrderTime -= Time.deltaTime;
            if (hideOrderTime <= 0)
            {
                order.SetActive(false);
                firstOrderPos.SetActive(false);
                secondOrderPos.SetActive(false);
                thirdOrderPos.SetActive(false);
            }
        }
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

    //void _SetFoodListRate()
    //{
    //    for(int i=0; i<missionManager.foodRates.Length; i++)
    //    {
    //        for (int j=0; j<foodLists.Length; j++)
    //        {
    //            if(missionManager.foodRates[i].id == foodLists[j].id)
    //            {
    //                foodLists[j].rate = missionManager.foodRates[i].rate;
    //                break;
    //            }
    //        }
    //    }
    //}

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

    void _SetRandomOrder()
    {
        ordered = true;

        if (orderSize == 1)
        {
            currentFirstOrderId = _SetCurrentOrder();
            firstOrderCost = _SetOrderCost(currentFirstOrderId);

            firstOrderPos.GetComponent<CustomerOrder>().orderID = currentFirstOrderId;
            firstOrderPos.GetComponent<CustomerOrder>().orderCost = firstOrderCost;
            firstOrderPos.GetComponent<CustomerOrder>().customer = gameObject.GetComponent<Customer>();
            firstOrderPos.SetActive(true);
        }

        if (orderSize == 2)
        {
            int totalRate = oneOrderRate + twoOderRate;
            int randomIndex = Random.Range(0, totalRate);

            if (randomIndex <= oneOrderRate)
            {
                currentFirstOrderId = _SetCurrentOrder();
                firstOrderCost = _SetOrderCost(currentFirstOrderId);

                firstOrderPos.GetComponent<CustomerOrder>().orderID = currentFirstOrderId;
                firstOrderPos.GetComponent<CustomerOrder>().orderCost = firstOrderCost;
                firstOrderPos.GetComponent<CustomerOrder>().customer = gameObject.GetComponent<Customer>();
                firstOrderPos.SetActive(true);
            }
            else
            if (randomIndex <= totalRate)
            {
                currentFirstOrderId = _SetCurrentOrder();
                firstOrderCost = _SetOrderCost(currentFirstOrderId);

                firstOrderPos.GetComponent<CustomerOrder>().orderID = currentFirstOrderId;
                firstOrderPos.GetComponent<CustomerOrder>().orderCost = firstOrderCost;
                firstOrderPos.GetComponent<CustomerOrder>().customer = gameObject.GetComponent<Customer>();
                firstOrderPos.SetActive(true);

                currentSecondOrderId = _SetCurrentOrder();
                secondOrderCost = _SetOrderCost(currentSecondOrderId);

                secondOrderPos.GetComponent<CustomerOrder>().orderID = currentSecondOrderId;
                secondOrderPos.GetComponent<CustomerOrder>().orderCost = secondOrderCost;
                secondOrderPos.GetComponent<CustomerOrder>().customer = gameObject.GetComponent<Customer>();
                secondOrderPos.SetActive(true);
            }
        }

        if (orderSize >= 3)
        {
            int totalRate = oneOrderRate + twoOderRate + threeOrderRate;
            int randomIndex = Random.Range(0, totalRate);

            if (randomIndex <= oneOrderRate)
            {
                currentFirstOrderId = _SetCurrentOrder();
                firstOrderCost = _SetOrderCost(currentFirstOrderId);

                firstOrderPos.GetComponent<CustomerOrder>().orderID = currentFirstOrderId;
                firstOrderPos.GetComponent<CustomerOrder>().orderCost = firstOrderCost;
                firstOrderPos.GetComponent<CustomerOrder>().customer = gameObject.GetComponent<Customer>();
                firstOrderPos.SetActive(true);
            }
            else
            if (randomIndex <= oneOrderRate + twoOderRate)
            {
                currentFirstOrderId = _SetCurrentOrder();
                firstOrderCost = _SetOrderCost(currentFirstOrderId);

                firstOrderPos.GetComponent<CustomerOrder>().orderID = currentFirstOrderId;
                firstOrderPos.GetComponent<CustomerOrder>().orderCost = firstOrderCost;
                firstOrderPos.GetComponent<CustomerOrder>().customer = gameObject.GetComponent<Customer>();
                firstOrderPos.SetActive(true);

                currentSecondOrderId = _SetCurrentOrder();
                secondOrderCost = _SetOrderCost(currentSecondOrderId);

                secondOrderPos.GetComponent<CustomerOrder>().orderID = currentSecondOrderId;
                secondOrderPos.GetComponent<CustomerOrder>().orderCost = secondOrderCost;
                secondOrderPos.GetComponent<CustomerOrder>().customer = gameObject.GetComponent<Customer>();
                secondOrderPos.SetActive(true);
            }
            else
            if (randomIndex <= totalRate)
            {
                currentFirstOrderId = _SetCurrentOrder();
                firstOrderCost = _SetOrderCost(currentFirstOrderId);

                firstOrderPos.GetComponent<CustomerOrder>().orderID = currentFirstOrderId;
                firstOrderPos.GetComponent<CustomerOrder>().orderCost = firstOrderCost;
                firstOrderPos.GetComponent<CustomerOrder>().customer = gameObject.GetComponent<Customer>();
                firstOrderPos.SetActive(true);

                currentSecondOrderId = _SetCurrentOrder();
                secondOrderCost = _SetOrderCost(currentSecondOrderId);

                secondOrderPos.GetComponent<CustomerOrder>().orderID = currentSecondOrderId;
                secondOrderPos.GetComponent<CustomerOrder>().orderCost = secondOrderCost;
                secondOrderPos.GetComponent<CustomerOrder>().customer = gameObject.GetComponent<Customer>();
                secondOrderPos.SetActive(true);

                currentThirdOrderId = _SetCurrentOrder();
                thirdOrderCost = _SetOrderCost(currentSecondOrderId);

                thirdOrderPos.GetComponent<CustomerOrder>().orderID = currentThirdOrderId;
                thirdOrderPos.GetComponent<CustomerOrder>().orderCost = thirdOrderCost;
                thirdOrderPos.GetComponent<CustomerOrder>().customer = gameObject.GetComponent<Customer>();
                thirdOrderPos.SetActive(true);
            }
        }
    }

    //string _SetCurrentOrder()
    //{
    //    currentOrderSize += 1;

    //    int totalRate = 0;
    //    int rateTemp = 0;
    //    string selectedFirstId = "";
    //    string currentId = "";
    //    int msRate = Random.Range(0, 100);

    //    if(msRate <= mainRate)
    //    {
    //        //lay total rate first food
    //        for (int i = 0; i < foodLists.Length; i++)
    //        {
    //            if (foodLists[i].fistFood == true 
    //                && foodLists[i].foodType.ToString() == "MainFood" 
    //                && foodLists[i].rate >0 
    //                && foodLists[i].canOrder == true)
    //            {
    //                totalRate += foodLists[i].rate;
    //            }
    //        }

    //        //Lua chon mon chinh
    //        for (int i=0; i<foodLists.Length; i++)
    //        {
    //            if (foodLists[i].fistFood == true 
    //                && foodLists[i].foodType.ToString() == "MainFood" 
    //                && foodLists[i].rate > 0
    //                && foodLists[i].canOrder == true)
    //            {
    //                int rate = Random.Range(0, totalRate);
    //                rateTemp += foodLists[i].rate;
    //                if (rate <= rateTemp)
    //                {
    //                    currentId = foodLists[i].id;
    //                    selectedFirstId = foodLists[i].id;

    //                    totalRate = 0;
    //                    rateTemp = 0;

    //                    break;
    //                }
    //            }
    //        }

    //        //lay total rate cua thuc pham ket hon voi mon chinh
    //        for (int i = 0; i < foodLists.Length; i++)
    //        {
    //            if (/*foodLists[i].firstId == selectedFirstId
    //                && /*foodLists[i].fistFood == false
    //                &&*/ foodLists[i].foodType.ToString() == "MainFood"
    //                && foodLists[i].rate > 0
    //                && foodLists[i].canOrder == true)
    //            {
    //                totalRate += foodLists[i].rate;
    //            }
    //        }

    //        Debug.Log("MainFood = " + currentId);

    //        //Lua chon ket hop
    //        for (int i = 0; i < foodLists.Length; i++)
    //        {
    //            if (foodLists[i].firstId == selectedFirstId
    //                && foodLists[i].fistFood == false
    //                && foodLists[i].foodType.ToString() == "MainFood"
    //                && foodLists[i].rate > 0
    //                && foodLists[i].canOrder == true)
    //            {
    //                int rate = Random.Range(0, totalRate)+1;
    //                rateTemp += foodLists[i].rate;
    //                if (rateTemp <= rate)
    //                {
    //                    currentId += foodLists[i].id;
    //                }
    //            }
    //        }
    //    }
    //    else if(msRate > mainRate)
    //    {
    //        //lay total rate
    //        for (int i = 0; i < foodLists.Length; i++)
    //        {
    //            if (foodLists[i].fistFood == true 
    //                && foodLists[i].foodType.ToString() == "SideFood"
    //                && foodLists[i].rate > 0
    //                && foodLists[i].canOrder == true)
    //            {
    //                totalRate += foodLists[i].rate;
    //            }
    //        }

    //        //Lua chon mon chinh
    //        for (int i = 0; i < foodLists.Length; i++)
    //        {
    //            if (foodLists[i].fistFood == true
    //                && foodLists[i].foodType.ToString() == "SideFood"
    //                 && foodLists[i].rate > 0
    //                 && foodLists[i].canOrder == true)
    //            {
    //                int rate = Random.Range(0, totalRate);
    //                rateTemp += foodLists[i].rate;
    //                if (rate <= rateTemp)
    //                {
    //                    currentId = foodLists[i].id;
    //                    selectedFirstId = foodLists[i].id;

    //                    totalRate = 0;
    //                    rateTemp = 0;

    //                    break;
    //                }
    //            }
    //        }

    //        //lay total rate cua thuc pham ket hon voi mon chinh
    //        for (int i = 0; i < foodLists.Length; i++)
    //        {
    //            if (/*foodLists[i].firstId == selectedFirstId
    //                && /*foodLists[i].fistFood == false
    //                &&*/ foodLists[i].foodType.ToString() == "SideFood" 
    //                && foodLists[i].rate > 0
    //                && foodLists[i].canOrder == true)
    //            {
    //                totalRate += foodLists[i].rate;
    //            }
    //        }

    //        //Lua chon ket hop
    //        for (int i = 0; i < foodLists.Length; i++)
    //        {
    //            if (foodLists[i].firstId == selectedFirstId 
    //                && foodLists[i].fistFood == false
    //                && foodLists[i].foodType.ToString() == "SideFood" 
    //                && foodLists[i].rate > 0
    //                && foodLists[i].canOrder == true)
    //            {
    //                int rate = Random.Range(0, totalRate)+1;
    //                rateTemp += foodLists[i].rate;
    //                if (rateTemp <= rate)
    //                {
    //                    currentId += foodLists[i].id;
    //                }
    //            }
    //        }

    //        Debug.Log("SideFood = " + currentId);
    //    }

    //    return currentId;
    //}



    //public int _SetOrderCost(string currentOrderId)
    //{
    //    int currentCost = 0;

    //    for(int i=0; i< currentOrderId.Length; i++)
    //    {
    //        for(int j=0; j< foodLists.Length; j++)
    //        {
    //            if (currentOrderId[i].ToString() == foodLists[j].id)
    //            {
    //                currentCost += foodLists[j].foodCost;
    //                break;
    //            }
    //        }
    //    }

    //    return currentCost;
    //}

    string _SetCurrentOrder()
    {
        currentOrderSize += 1;

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

        if(currentId == "")
        {
            currentOrderSize -= 1;
            return _SetCurrentOrder();
        }
        else
        {
            return currentId;
        }
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

    void _OrderIsOk()
    {
        if (orderDoneCount == currentOrderSize && orderIsOk == false)
        {
            if (!orderIsOk)
            {
                orderIsOk = true;

                missionManager.currentPlayerCoin += tempCoin;

                missionManager.customerPaidCount += 1;

                if (customerEmo == 1)
                {
                    missionManager.customerGoodEmoCount += 1;

                    animationBodyHappy();
                    animationHeadHappy();
                }
            }
        }
    }

    public void _PayFirstOrder()
    {
        if (ordered == true && firstOrderStats == 1)
        {
            tempCoin += firstOrderCost;
            missionManager.orderOkCount += 1;
            missionManager.dishCount += 1;

            combo._SetCombo(firstOrderCost);

            firstOrderStats = 3;

            orderDoneCount += 1;

            _OrderIsOk();

            firstOrderPos.GetComponent<CustomerOrder>()._DeactiveFoodPos();
            firstOrderPos.GetComponent<CustomerOrder>()._ActiveTargetIcon();
        }
    }

    public void _PaySecondOrder()
    {
        if (ordered == true && secondOrderStats == 1)
        {
            tempCoin += secondOrderCost;
            missionManager.orderOkCount += 1;
            missionManager.dishCount += 1;

            combo._SetCombo(secondOrderCost);

            secondOrderStats = 3;

            orderDoneCount += 1;

            _OrderIsOk();

            secondOrderPos.GetComponent<CustomerOrder>()._DeactiveFoodPos();
            secondOrderPos.GetComponent<CustomerOrder>()._ActiveTargetIcon();
        }
    }

    public void _PayThirdOrder()
    {
        if (ordered == true && thirdOrderStats == 1)
        {
            tempCoin += thirdOrderCost;
            missionManager.orderOkCount += 1;
            missionManager.dishCount += 1;

            combo._SetCombo(thirdOrderCost);

            thirdOrderStats = 3;

            orderDoneCount += 1;

            _OrderIsOk();

            thirdOrderPos.GetComponent<CustomerOrder>()._DeactiveFoodPos();
            thirdOrderPos.GetComponent<CustomerOrder>()._ActiveTargetIcon();
        }
    }

    void _SetLeavePos()
    {
        leavePositions = GameObject.FindGameObjectsWithTag("CustomerLeavePos");
        int randomIndex = Random.Range(0, leavePositions.Length);
        posToLeave = leavePositions[randomIndex];
    }

    void _Leave()
    {
        if (orderIsOk == true)
        {
            waitToLeave -= Time.deltaTime;

            if (waitToLeave <= 0)
            {
                _SetEmptyPos();
                if (!isLeaving)
                {
                    isLeaving = true;
                    animationBodyMove();
                }

                if (transform.position.x > posToLeave.transform.position.x)
                {
                    currentLocalScale.x = -1f;
                    pos -= transform.right * Time.deltaTime * speed;
                    transform.position = pos;
                }
                else
                if (transform.position.x < posToLeave.transform.position.x)
                {
                    currentLocalScale.x = 1f;
                    pos += transform.right * Time.deltaTime * speed;
                    transform.position = pos;
                }

                transform.localScale = currentLocalScale;
            }
        }

        else if (currentWaitTime <= 0 && orderIsOk == false)
        {
            _SetEmptyPos();
            if (!isLeaving)
            {
                isLeaving = true;

                if (customerEmo == 4)
                {
                    missionManager.customerAngryCount += 1;
                }

                animationBodyMove();
            }

            if (transform.position.x > posToLeave.transform.position.x)
            {
                currentLocalScale.x = -1f;
                pos -= transform.right * Time.deltaTime * speed;
                transform.position = pos;
            }
            else
            if (transform.position.x < posToLeave.transform.position.x)
            {
                currentLocalScale.x = 1f;
                pos += transform.right * Time.deltaTime * speed;
                transform.position = pos;
            }

            transform.localScale = currentLocalScale;
        }
    }

    void _SetEmptyPos()
    {
        if (orderIsOk == true && isLeaving == false || customerEmo == 4 && isLeaving == false)
        {
            target.GetComponent<CustomerPos>().isEmpty = true;
            //Debug.Log("vi tri " + target.name + " isEmpty = " + target.GetComponent<CustomerPos>().isEmpty);
        }
    }

    void OnTriggerEnter(Collider _target)
    {
        if (target != null)
        {
            if (_target.gameObject.name == target.name)
            {
                customerManager.canSpawn = true;
            }
        }

        if (orderIsOk == true || isLeaving == true)
        {
            if (_target.gameObject.tag == posToLeave.tag)
            {
                customerManager.canSpawn = true;
                customerManager.currentCustomerCount--;
                Destroy(gameObject);
            }
        }
    }

    void _FreezeSpineAnimation()
    {
        if (ordered == true && isOnPos == true && order.activeSelf == true && freezeSpine != null)
        {
            if (isFreeze == true && freezeSpine.activeSelf == false && freezeStats == 0)
            {
                _animFreezeStart();
            }

            if (isFreeze == false && freezeSpine.activeSelf == true && freezeStats == 1)
            {
                StartCoroutine(_animFreezeEnd());
            }
        }
    }

    void _animFreezeStart()
    {
        freezeSpine.SetActive(true);
        freezeStats = 1;
        freezeSpine.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "start", false);
    }

    IEnumerator _animFreezeEnd()
    {
        freezeStats = 0;
        freezeSpine.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "end", false);
        var myAnim = freezeSpine.GetComponent<SkeletonAnimation>().skeleton.Data.FindAnimation("end");
        yield return new WaitForSeconds(myAnim.Duration);

        if (currentWaitTime < customerWaitTime / 4)
        {
            _animFreezeWarning();
        }
        else
        {
            freezeSpine.SetActive(false);
        }

        yield break;
    }

    void _animFreezeWarning()
    {
        freezeSpine.SetActive(true);
        freezeSpine.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "warning", true);
    }

    public void animationBodyMove()
    {
        customerSpine.AnimationState.SetAnimation(0, "move_body", true);
    }
    public void animationBodyIdle()
    {
        customerSpine.AnimationState.SetAnimation(0, "idle_body", true);
    }
    public void animationBodyAngry()
    {
        customerSpine.AnimationState.SetAnimation(0, "angry_body", true);
    }
    public void animationBodyHappy()
    {
        customerSpine.AnimationState.SetAnimation(0, "happy_body", false);
    }

    public void animationHeadIdle()
    {
        customerSpine.AnimationState.SetAnimation(1, "idle_head", true);
    }
    public void animationHeadAngry()
    {
        customerSpine.AnimationState.SetAnimation(1, "angry_head", true);

        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = angryAudio;
        audioSource.Play();
    }
    public void animationHeadHappy()
    {
        customerSpine.AnimationState.SetAnimation(1, "happy_head", true);

        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = goodEmoAudio;
        audioSource.Play();
    }
}
