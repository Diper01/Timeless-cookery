using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOrderPos : MonoBehaviour
{
    private MissionManager missionManager;
    private Combo combo;

    public Boss boss;
    public BossOrder bossOrder;

    public string orderID = "";
    public int orderCost = 0;

    public int orderStats = 0; // = 0 order is not ok, = 1 order isok, = 2 order is locked, = 3 order is paid

    public GameObject foodPos, ticker;

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
        combo = GameObject.FindGameObjectWithTag("Combo").GetComponent<Combo>();

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

        ticker.SetActive(false);

        foodPos.SetActive(true);
        _TweenScale();
    }

    public void _Pay()
    {
        orderStats = 3;

        combo._SetCombo(orderCost);

        missionManager.currentPlayerCoin += orderCost;
        missionManager.orderOkCount += 1;
        missionManager.dishCount += 1;
        boss.orderDone += 1;

        _DeactiveOrderImgs();
        _ActiveTicker();

        boss.StartCoroutine(boss._animHappy());
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

    public void _ActiveTicker()
    {
        _DeactiveFoodPos();

        ticker.SetActive(true);

        _TweenScale();

        _Reset();

        StartCoroutine(_NewOrder());
    }

    void _Reset()
    {
        orderID = "";
        orderCost = 0;
        orderStats = 0;
    }

    void _TweenScale()
    {
        gameObject.GetComponent<TweenScale>().ResetToBeginning();
        gameObject.GetComponent<TweenScale>().from = new Vector3(0, 0, 0);
        gameObject.GetComponent<TweenScale>().to = new Vector3(1, 1, 1);
        gameObject.GetComponent<TweenScale>().duration = 0.2f;
        gameObject.GetComponent<TweenScale>().PlayForward();
    }

    public IEnumerator _NewOrder()
    {
        yield return new WaitForSeconds(1f);

        boss._RenewOrder();
    }

    IEnumerator _ActiveTickerAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        _ActiveTicker();

        yield break;
    }
}
