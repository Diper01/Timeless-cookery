using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatOnGrillGuideButton : MonoBehaviour
{
    private MissionManager missionManager;

    public GameplayGuideController gameplayGuideController;
    public FoodOnGrill[] foodOnGrills;
    public Grill grill;

    public FoodPlate foodPlate;

    public GameObject lockBG;
    public GameObject meatOnGrillGuideBG;
    public GameObject handGuide;
    public GameObject meatOnGrillGuideNotice;

    public string cusOrderId = "";
    public bool orderSet = false;
    public Customer selectedCus;
    public bool cusSelected = false;

    public bool isActived = false;

    // Start is called before the first frame update
    void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();

        lockBG.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        _CheckFoodOnGrill();
        _SelectCustomer();
        _SetFoodId();
    }

    public void _SelectCustomer()
    {
        if (selectedCus == null && missionManager.customerCount > 0 && cusSelected == false)
        {
            GameObject[] cus = GameObject.FindGameObjectsWithTag("Customer");

            selectedCus = cus[0].GetComponent<Customer>();

            cusSelected = true;
        }
    }

    void _SetFoodId()
    {
        if (orderSet == false && selectedCus != null
            && selectedCus.ordered == true
            && selectedCus.firstOrderStats == 0
            && selectedCus.currentFirstOrderId != cusOrderId)
        {
            orderSet = true;

            selectedCus.currentFirstOrderId = 1.ToString();
            selectedCus.firstOrderCost = selectedCus._SetOrderCost(1.ToString());

            selectedCus.firstOrderPos.GetComponent<CustomerOrder>().orderID = selectedCus.currentFirstOrderId;
            selectedCus.firstOrderPos.GetComponent<CustomerOrder>().orderCost = selectedCus.firstOrderCost;
            selectedCus.firstOrderPos.GetComponent<CustomerOrder>()._ActiveOrderImages();
        }
    }

    void _CheckFoodOnGrill()
    {
        if(grill.isProcessed == true && isActived == false)
        {
            isActived = true;

            lockBG.SetActive(false);

            meatOnGrillGuideBG.SetActive(true);
            handGuide.SetActive(true);
            meatOnGrillGuideNotice.SetActive(true);

            grill.isFreeze = true;

            Time.timeScale = 0;
        }
    }

    public void _OnButton()
    {
        Time.timeScale = 1;

        if (grill.isProcessed == true && grill.isOverBurned == false)
        {
            for(int i=0; i<foodOnGrills.Length; i++)
            {
                if(foodOnGrills[i].gameObject.activeSelf == true)
                {
                    foodOnGrills[i]._ResetVaules();
                }
            }

            grill._ResetGrill();
        }

        grill.isFreeze = false;

        foodPlate.isEmpty = false;

        for(int i =0; i < foodPlate.foodId.Length; i++)
        {
            if(foodPlate.foodId[i].id == 1.ToString())
            {
                foodPlate.foodId[i].isEmpty = false;
                foodPlate._ActiveImage();
                break;
            }
        }

        meatOnGrillGuideBG.SetActive(false);
        handGuide.SetActive(false);
        meatOnGrillGuideNotice.SetActive(false);

        gameObject.SetActive(false);
    }
}
