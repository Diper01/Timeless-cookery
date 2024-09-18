using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerOrder : MonoBehaviour
{
    private MissionManager missionManager;

    public Customer customer;

    public string orderID = "";
    public int orderCost = 0;

    public int orderStats = 0; // = 0 order is not ok, = 1 order isok, = 2 order is locked, = 3 order is paid

    public GameObject foodPos, coinTarget, cusTarget, dishTarget, likeTarget, ticker, targetIconPrefabs;

    [System.Serializable]
    public class OrderImg
    {
        public string id = "";
        public int currentLevel = 0;
        public GameObject[] foodImgs;
    }

    public OrderImg[] orderImgs;


    private void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();

        _DeactiveOrderImgs();
        _ActiveOrderImages();
    }

    private void Update()
    {
        
    }

    public void _ActiveOrderImages()
    {
        GameObject[] foodController = GameObject.FindGameObjectsWithTag("FoodController");
        GameObject[] sideFoodController = GameObject.FindGameObjectsWithTag("SideFoodController");

        for (int i = 0; i < foodController.Length; i++)
        {
            for (int j = 0; j < orderImgs.Length; j++)
            {
                if (foodController[i].GetComponent<FoodController>().active == true
                    && foodController[i].GetComponent<FoodController>().foodId == orderImgs[j].id)
                {
                    orderImgs[j].currentLevel = foodController[i].GetComponent<FoodController>().lv;

                    break;
                }
            }
        }

        for (int i = 0; i < sideFoodController.Length; i++)
        {
            for (int j = 0; j < orderImgs.Length; j++)
            {
                if (sideFoodController[i].GetComponent<SideFoodController>().isUnlocked == true
                    && sideFoodController[i].GetComponent<SideFoodController>().foodId == orderImgs[j].id)
                {
                    orderImgs[j].currentLevel = sideFoodController[i].GetComponent<SideFoodController>().lv;

                    break;
                }
            }
        }

        for (int i = 0; i < orderID.Length; i++)
        {
            for (int j = 0; j < orderImgs.Length; j++)
            {
                if (orderID[i].ToString() == orderImgs[j].id)
                {
                    orderImgs[j].foodImgs[orderImgs[j].currentLevel - 1].SetActive(true);
                }
            }
        }
    }

    public void _DeactiveOrderImgs()
    {
        for (int i = 0; i < orderImgs.Length; i++)
        {
            for (int j = 0; j < orderImgs[i].foodImgs.Length; j++)
            {
                orderImgs[i].foodImgs[j].SetActive(false);
            }
        }
    }

    public void _DeactiveFoodPos()
    {
        foodPos.SetActive(false);
    }

    public void _ActiveTargetIcon()
    {
        if (missionManager.firstTarget == MissionManager.FirstTarget.Time)
        {
            if (missionManager.secondTarget == MissionManager.SecondTarget.Null)
            {
                coinTarget.SetActive(true);
                cusTarget.SetActive(false);
                likeTarget.SetActive(false);
                dishTarget.SetActive(false);
            }
            else if (missionManager.secondTarget == MissionManager.SecondTarget.Coin)
            {
                coinTarget.SetActive(true);
                cusTarget.SetActive(false);
                likeTarget.SetActive(false);
                dishTarget.SetActive(false);
            }
            else if (missionManager.secondTarget == MissionManager.SecondTarget.Dish)
            {
                coinTarget.SetActive(false);
                cusTarget.SetActive(false);
                likeTarget.SetActive(false);
                dishTarget.SetActive(true);
            }

            else if (missionManager.secondTarget == MissionManager.SecondTarget.Like && customer.customerEmo == 1)
            {
                coinTarget.SetActive(false);
                cusTarget.SetActive(false);
                likeTarget.SetActive(true);
                dishTarget.SetActive(false);
            }
        }
        else if (missionManager.firstTarget == MissionManager.FirstTarget.Customer)
        {
            if (missionManager.secondTarget == MissionManager.SecondTarget.Null)
            {
                coinTarget.SetActive(false);
                cusTarget.SetActive(true);
                likeTarget.SetActive(false);
                dishTarget.SetActive(false);
            }
            else if (missionManager.secondTarget == MissionManager.SecondTarget.Coin)
            {
                coinTarget.SetActive(true);
                cusTarget.SetActive(false);
                likeTarget.SetActive(false);
                dishTarget.SetActive(false);
            }
            else if (missionManager.secondTarget == MissionManager.SecondTarget.Dish)
            {
                coinTarget.SetActive(false);
                cusTarget.SetActive(false);
                likeTarget.SetActive(false);
                dishTarget.SetActive(true);
            }
            else if (missionManager.secondTarget == MissionManager.SecondTarget.Like && customer.customerEmo == 1)
            {
                coinTarget.SetActive(false);
                cusTarget.SetActive(false);
                likeTarget.SetActive(true);
                dishTarget.SetActive(false);
            }
        }

        gameObject.GetComponent<TweenScale>().ResetToBeginning();
        gameObject.GetComponent<TweenScale>().from = new Vector3(0, 0, 0);
        gameObject.GetComponent<TweenScale>().to = new Vector3(1, 1, 1);
        gameObject.GetComponent<TweenScale>().duration = 0.2f;
        gameObject.GetComponent<TweenScale>().PlayForward();

        if(missionManager.secondTarget == MissionManager.SecondTarget.Dish
           || missionManager.secondTarget == MissionManager.SecondTarget.Null && missionManager.firstTarget == MissionManager.FirstTarget.Time)
        {
            StartCoroutine(_MoveToProcessBar(0.5f));
        }
        else if(missionManager.secondTarget == MissionManager.SecondTarget.Like && customer.customerEmo == 1
            || missionManager.secondTarget == MissionManager.SecondTarget.Null && missionManager.firstTarget == MissionManager.FirstTarget.Customer
            || missionManager.secondTarget == MissionManager.SecondTarget.Coin)
        {
            if (customer.orderIsOk == true)
            {
                StartCoroutine(_MoveToProcessBar(gameObject.GetComponent<TweenScale>().duration));
            }
            else if(customer.orderIsOk == false)
            {
                StartCoroutine(_ActiveTickerAfterTime(0.5f));
            }
        }

        else if(missionManager.secondTarget == MissionManager.SecondTarget.Like && customer.customerEmo != 1
            || missionManager.secondTarget == MissionManager.SecondTarget.Coin && customer.orderDoneCount < customer.orderSize)
        {
            _ActiveTicker();
        }
    }

    IEnumerator _MoveToProcessBar(float time)
    {
        yield return new WaitForSeconds(time);

        if (coinTarget.activeSelf == true)
        {
            targetIconPrefabs.GetComponent<SpriteRenderer>().sprite = coinTarget.GetComponent<SpriteRenderer>().sprite;
        }
        else if(cusTarget.activeSelf == true)
        {
            targetIconPrefabs.GetComponent<SpriteRenderer>().sprite = cusTarget.GetComponent<SpriteRenderer>().sprite;
        }
        else if (likeTarget.activeSelf == true)
        {
            targetIconPrefabs.GetComponent<SpriteRenderer>().sprite = likeTarget.GetComponent<SpriteRenderer>().sprite;
        }
        else if (dishTarget.activeSelf == true)
        {
            targetIconPrefabs.GetComponent<SpriteRenderer>().sprite = dishTarget.GetComponent<SpriteRenderer>().sprite;
        }

        Vector3 processBarPos = GameObject.FindGameObjectWithTag("ProcessBar").transform.position;

        targetIconPrefabs.GetComponent<GameplayPointTargetEffect>().pos = transform.position;
        targetIconPrefabs.GetComponent<GameplayPointTargetEffect>().targetPos = processBarPos;
        targetIconPrefabs.GetComponent<GameplayPointTargetEffect>().customerOrder = gameObject.GetComponent<CustomerOrder>();

        Instantiate(targetIconPrefabs, transform.position, Quaternion.identity);

        yield break;
    }

    public void _ActiveTicker()
    {
        coinTarget.SetActive(false);
        cusTarget.SetActive(false);
        likeTarget.SetActive(false);
        dishTarget.SetActive(false);

        ticker.SetActive(true);

        gameObject.GetComponent<TweenScale>().ResetToBeginning();
        gameObject.GetComponent<TweenScale>().from = new Vector3(0, 0, 0);
        gameObject.GetComponent<TweenScale>().to = new Vector3(1, 1, 1);
        gameObject.GetComponent<TweenScale>().duration = 0.2f;
        gameObject.GetComponent<TweenScale>().PlayForward();
    }

    IEnumerator _ActiveTickerAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        _ActiveTicker();

        yield break;
    }
}
