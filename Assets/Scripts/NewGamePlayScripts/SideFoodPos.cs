using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SideFoodPos : MonoBehaviour
{
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

    private int discardCount = 0;
    private float discardTime = 0.5f;

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

        pos = transform.position;
        scale = transform.localScale;
        targetScale = new Vector3(0.3f, 0.3f, 1.0f);

        rate = (1.0f / durationTime) * speed;

    }

    private void Update()
    {
        //_OnTouch();

        if (missionManager.gamemode == MissionManager.GameMode.Normal)
        {
            _MoveToOrder();
        }
        else
        {
            _MoveToBossOrder();
        }


        _SetUnlock();
        _SetLevel();
        _SetCurrentId();
        _ActiveImage();
        _SetSmokeEffect();
    }

    void _OnTouch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.gameObject.name == gameObject.name)
                {
                    if (isEmpty == false && isMoving == false)
                    {
                        _SelectCustomer();

                        discardCount++;
                        if (discardCount == 1)
                        {
                            StartCoroutine(_DiscardTimer());
                        }
                    }
                }
            }
        }
    }

    void OnMouseDown()
    {
        if (isEmpty == false && isMoving == false)
        {
            if(missionManager.gamemode == MissionManager.GameMode.Normal)
            {
                _SelectCustomer();
            }
            else if (missionManager.gamemode == MissionManager.GameMode.Boss)
            {
                _SelectBoss();
            }
            //discardCount++;
            //if (discardCount == 1)
            //{
            //    StartCoroutine(_DiscardTimer());
            //}
        }
    }

    void _SetSmokeEffect()
    {
        if(smokeEffect == null)
        {
            return;
        }

        if (isEmpty == false && smokeEffect.activeSelf == false)
        {
            smokeEffect.SetActive(true);
        }
        else
            if (isEmpty == true && smokeEffect.activeSelf == true)
        {
            smokeEffect.SetActive(false);
        }
    }

    void _SetUnlock()
    {
        if (isUnlocked == false && plateImg != null && plateImg.activeSelf == true)
        {
            plateImg.SetActive(false);
        }
    }

    void _SetLevel()
    {
        GameObject[] sideFoodControllers = GameObject.FindGameObjectsWithTag("SideFoodController");
        for (int i = 0; i < sideFoodControllers.Length; i++)
        {
            if (sideFoodControllers[i].GetComponent<SideFoodController>().isUnlocked == true
                && sideFoodControllers[i].activeSelf == true)
            {
                for (int j = 0; j < foodId.Length; j++)
                {
                    if (sideFoodControllers[i].GetComponent<SideFoodController>().foodId == foodId[j].id)
                    {
                        foodId[j].lv = sideFoodControllers[i].GetComponent<SideFoodController>().lv;
                    }
                }
            }
        }
    }

    IEnumerator _DiscardTimer()
    {
        float temp = discardTime;
        while (discardCount > 0)
        {
            discardTime -= Time.deltaTime;

            if (discardCount > 1)
            {
                _ResetFoodPlate();
            }
            else
            if (discardTime < 0)
            {
                discardTime = temp;
                discardCount = 0;
            }
        }
        yield break;
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

    void _ActiveImage()
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
        GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");
        for (int i = 0; i < customers.Length; i++)
        {
            if(customers[i].GetComponent<Customer>() != null)
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

    void _SelectBoss()
    {
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");

        for (int i = 0; i < bosses.Length; i++)
        {
            Boss tBoss = bosses[i].GetComponent<Boss>();

            for (int j = 0; j < tBoss.bossOrders.Length; j++)
            {
                BossOrder bossOrder = tBoss.bossOrders[j];

                for (int k = 0; k < bossOrder.bossOrderPos.Length; k++)
                {
                    BossOrderPos bossOrderPos = bossOrder.bossOrderPos[k];

                    if (bossOrderPos.orderID == currentId && bossOrderPos.orderStats == 0)
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

    void _ResetFoodPlate()
    {
        currentId = "";

        for (int i = 0; i < foodId.Length; i++)
        {
            foodId[i].isEmpty = true;
        }

        _DeactiveImage();

        iMove = 0f;
        foodOrder = 0;
        customer = null;
        transform.position = pos;
        transform.localScale = scale;
        targetPos = null;
        isMoving = false;
        isEmpty = true;
    }

    void OnTriggerEnter(Collider _target)
    {

    }
}
