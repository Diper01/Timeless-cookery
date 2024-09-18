using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class Foods
{
    public int lv = 1;
    public GameObject foodImg;
}

[System.Serializable]
public class FoodId
{
    public string name = "";
    public string id = "";
    public int lv = 1;
    public bool isEmpty = true;
    public Foods[] foods;
}

public class FoodPlate : MonoBehaviour
{
    private GamePlayMenuManager gamePlayMenuManager;
    private MissionManager missionManager;

    private GameObject customer;
    private Boss boss;

    public string currentId = "";

    public GameObject plateImg;
    public GameObject smokeEffect;

    public int emptySlotCount = 0;

    private Vector3 pos;
    private GameObject targetPos;
    private Vector3 scale;
    private Vector3 targetScale;
    public float speed = 5.0f;
    public float durationTime = 3.0f;

    private float lastTapTime;

    public bool isUnlocked = false;
    public bool isEmpty = true;
    public bool isMoving = false;

    public FoodId[] foodId;

    public int foodOrder = 0;

    float iMove = 0f;
    float rate = 0f;

    private void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        gamePlayMenuManager = GameObject.FindGameObjectWithTag("GamePlayMenuManager").GetComponent<GamePlayMenuManager>();

        pos = transform.position;
        scale = transform.localScale;
        targetScale = new Vector3(0.3f, 0.3f, 1.0f);

        rate = (1.0f / durationTime) * speed;
    }

    private void Update()
    {
        if(gamePlayMenuManager.win == true || gamePlayMenuManager.lose == true)
        {
            return;
        }

        _OnTouch();

        if(missionManager.gamemode == MissionManager.GameMode.Normal)
        {
            _MoveToOrder();
        }
        else
        {
            _MoveToBossOrder();
        }

        _SetUnlock();
        _SetFoodLevel();
        _SetCurrentId();
        _ActiveImage();
        _SetSmokeEffect();
    }

    void _OnTouch()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (EventSystem.current.IsPointerOverGameObject())
        //    {
        //        return;
        //    }

        //    RaycastHit hitInfo;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out hitInfo))
        //    {
        //        if (hitInfo.transform.gameObject.name == gameObject.name)
        //        {
        //            if (isEmpty == false && isMoving == false)
        //            {
        //                _SelectCustomer();
        //                _Discard();
        //            }
        //        }
        //    }
        //}
    }

    public void OnMouseDown()
    {
        if (isEmpty == false && isMoving == false)
        {
            if(missionManager.gamemode == MissionManager.GameMode.Normal)
            {
                _SelectCustomer();
                _Discard();
            }
            else if (missionManager.gamemode == MissionManager.GameMode.Boss)
            {
                _SelectBoss();
                _Discard();
            }
        }

        TouchScaleEffect();
    }

    public void TouchScaleEffect()
    {
        if(isMoving == false)
        {
            gameObject.GetComponent<TweenScale>().enabled = false;
            gameObject.GetComponent<TweenScale>().ResetToBeginning();
            gameObject.gameObject.GetComponent<TweenScale>().enabled = true;
        }
    }

    void _SetSmokeEffect()
    {
        if(isEmpty == false && smokeEffect.activeSelf == false)
        {
            smokeEffect.SetActive(true);
        }
        else
            if(isEmpty == true && smokeEffect.activeSelf == true)
        {
            smokeEffect.SetActive(false);
        }
    }

    public void _Discard()
    {
        float timeFromLastTap = Time.time - lastTapTime;
        if (timeFromLastTap <= 0.3f)
        {
            gameObject.GetComponent<AudioSource>().Play();
            missionManager.foodToTrashCount += 1;
            _ResetFoodPlate();
        }

        lastTapTime = Time.time;
    }

    void _SetUnlock()
    {
        if(isUnlocked == false && plateImg != null)
        {
            plateImg.SetActive(false);
        }
    }

    void _SetFoodLevel()
    {
        GameObject[] foodControllers = GameObject.FindGameObjectsWithTag("FoodController");
        for(int i=0; i< foodControllers.Length; i++)
        {
            if(foodControllers[i].GetComponent<FoodController>().active == true 
                && foodControllers[i].activeSelf == true)
            {
                for (int j = 0; j < foodId.Length; j++)
                {
                    if (foodControllers[i].GetComponent<FoodController>().foodId == foodId[j].id)
                    {
                        foodId[j].lv = foodControllers[i].GetComponent<FoodController>().lv;
                    }
                }
            }
        }
    }

    void _SetCurrentId()
    {
        string temp = "";
        int slotTemp = foodId.Length;

        for (int i = 0; i < foodId.Length; i++)
        {
            if (foodId[i].isEmpty == false)
            {
                temp += foodId[i].id;
                slotTemp--;
            }
        }

        currentId = temp;
        emptySlotCount = slotTemp;
    }

    public void _ActiveImage()
    {
        _DeactiveImage();

        for (int i = 0; i < foodId.Length; i++)
        {
            if (foodId[i].isEmpty == false)
            {
                int index = foodId[i].lv - 1;
                foodId[i].foods[index].foodImg.SetActive(true);
            }
        }
    }

    void _DeactiveImage()
    {
        for (int i = 0; i < foodId.Length; i++)
        {
            for (int j = 0; j < foodId[i].foods.Length; j++)
            {
                foodId[i].foods[j].foodImg.SetActive(false);
            }
        }
    }

    public void _SelectCustomer()
    {
        if (missionManager.gamemode == MissionManager.GameMode.Normal)
        {
            GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");
            for (int i = 0; i < customers.Length; i++)
            {
                if (customers[i].GetComponent<Customer>() != null)
                {
                    if (customers[i].GetComponent<Customer>().ordered == true
                    && customers[i].GetComponent<Customer>().isLeaving == false
                    && customers[i].GetComponent<Customer>().currentFirstOrderId == currentId
                    && customers[i].GetComponent<Customer>().orderIsOk == false
                    && customers[i].GetComponent<Customer>().firstOrderStats == 0)
                    {
                        isMoving = true;

                        customer = customers[i];

                        customers[i].GetComponent<Customer>().firstOrderStats = 2;

                        targetPos = customer.GetComponent<Customer>().firstOrderPos;

                        foodOrder = 1;

                        return;
                    }
                    else
                if (customers[i].GetComponent<Customer>().ordered == true
                    && customers[i].GetComponent<Customer>().isLeaving == false
                    && customers[i].GetComponent<Customer>().currentSecondOrderId == currentId
                    && customers[i].GetComponent<Customer>().orderIsOk == false
                    && customers[i].GetComponent<Customer>().secondOrderStats == 0)
                    {
                        isMoving = true;

                        customer = customers[i];

                        customers[i].GetComponent<Customer>().secondOrderStats = 2;

                        targetPos = customer.GetComponent<Customer>().secondOrderPos;

                        foodOrder = 2;

                        return;
                    }
                    else
                if (customers[i].GetComponent<Customer>().ordered == true
                    && customers[i].GetComponent<Customer>().isLeaving == false
                    && customers[i].GetComponent<Customer>().currentThirdOrderId == currentId
                    && customers[i].GetComponent<Customer>().orderIsOk == false
                    && customers[i].GetComponent<Customer>().thirdOrderStats == 0)
                    {
                        isMoving = true;

                        customer = customers[i];

                        customers[i].GetComponent<Customer>().thirdOrderStats = 2;

                        targetPos = customer.GetComponent<Customer>().thirdOrderPos;

                        foodOrder = 3;

                        return;
                    }
                }

            }
        }
    }

    void _SelectBoss()
    {
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");

        for (int i = 0; i < bosses.Length; i++)
        {
            Boss tBoss = bosses[i].GetComponent<Boss>();

            for(int j=0; j< tBoss.bossOrders.Length; j++)
            {
                BossOrder bossOrder = tBoss.bossOrders[j];

                for(int k=0; k< bossOrder.bossOrderPos.Length; k++)
                {
                    BossOrderPos bossOrderPos = bossOrder.bossOrderPos[k];

                    if(bossOrderPos.orderID == currentId && bossOrderPos.orderStats == 0)
                    {
                        isMoving = true;
                        bossOrderPos.orderStats = 2;

                        boss = tBoss;

                        targetPos = bossOrderPos.gameObject;

                        return;
                    }
                }
            }
        }
    }

    void _MoveToBossOrder()
    {
        if (missionManager.gamemode == MissionManager.GameMode.Boss)
        {
            if (isMoving == true)
            {
                if (iMove < 1f)
                {
                    iMove += Time.deltaTime * rate;
                    transform.position = Vector3.Lerp(pos, targetPos.transform.position, iMove);
                    transform.localScale = Vector3.Lerp(scale, targetScale, iMove);
                }
                if (iMove >= 1)
                {
                    isMoving = false;

                    targetPos.GetComponent<BossOrderPos>().orderStats = 1;
                    targetPos.GetComponent<BossOrderPos>()._Pay();

                    _ResetFoodPlate();
                }
            }
        }

    }

    void _MoveToOrder()
    {
        if(missionManager.gamemode == MissionManager.GameMode.Normal)
        {
            if (isMoving == true)
            {
                if (iMove < 1f)
                {
                    iMove += Time.deltaTime * rate;
                    transform.position = Vector3.Lerp(pos, targetPos.transform.position, iMove);
                    transform.localScale = Vector3.Lerp(scale, targetScale, iMove);
                }
                if (iMove >= 1)
                {
                    isMoving = false;

                    if (foodOrder == 1)
                    {
                        customer.GetComponent<Customer>().firstOrderStats = 1;
                        customer.GetComponent<Customer>()._PayFirstOrder();
                    }
                    else if (foodOrder == 2)
                    {
                        customer.GetComponent<Customer>().secondOrderStats = 1;
                        customer.GetComponent<Customer>()._PaySecondOrder();
                    }
                    else if (foodOrder == 3)
                    {
                        customer.GetComponent<Customer>().thirdOrderStats = 1;
                        customer.GetComponent<Customer>()._PayThirdOrder();
                    }

                    _ResetFoodPlate();
                }
            }
        }
        
    }

    void _ResetFoodPlate()
    {
        currentId = "";

        for (int i=0; i < foodId.Length; i++)
        {
            foodId[i].isEmpty = true;
        }

        _DeactiveImage();

        iMove = 0f;
        foodOrder = 0;
        customer = null;
        boss = null;
        transform.position = pos;
        transform.localScale = scale;
        targetPos = null;
        isEmpty = true;
    }

    void OnTriggerEnter(Collider _target)
    {

    }
}
