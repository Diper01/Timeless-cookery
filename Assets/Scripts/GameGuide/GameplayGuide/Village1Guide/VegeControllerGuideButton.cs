using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegeControllerGuideButton : MonoBehaviour
{
    private MissionManager missionManager;

    public FoodPlate[] foodPlates;

    public GameObject lockButton, LockGuideBG, vegeGuideBG, handGuide;

    public string cusOrderId = "";
    public bool orderSet = false;
    public Customer selectedCus;
    public bool cusSelected = false;

    public bool isActived = false;

    private void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
    }

    private void Update()
    {
        _Active();
        _SelectCustomer();
        _SetFoodId();
    }

    public void _SelectCustomer()
    {
        if (selectedCus == null && missionManager.customerCount > 0 && cusSelected == false)
        {
            GameObject[] cus = GameObject.FindGameObjectsWithTag("Customer");

            for (int i = 0; i < cus.Length; i++)
            {
                if (cus[i].GetComponent<Customer>().ordered == false)
                {
                    selectedCus = cus[i].GetComponent<Customer>();
                    cusSelected = true;
                    break;
                }
            }
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

            selectedCus.currentFirstOrderId = 12.ToString();
            selectedCus.firstOrderCost = selectedCus._SetOrderCost(12.ToString());

            selectedCus.firstOrderPos.GetComponent<CustomerOrder>().orderID = selectedCus.currentFirstOrderId;
            selectedCus.firstOrderPos.GetComponent<CustomerOrder>().orderCost = selectedCus.firstOrderCost;
            selectedCus.firstOrderPos.GetComponent<CustomerOrder>()._ActiveOrderImages();
        }
    }

    void _Active()
    {
        if(isActived == false)
        {
            for (int i = 0; i < foodPlates.Length; i++)
            {
                if (foodPlates[i].isEmpty == false && foodPlates[i].currentId == 1.ToString())
                {
                    isActived = true;

                    lockButton.SetActive(true);
                    LockGuideBG.SetActive(true);
                    vegeGuideBG.SetActive(true);
                    handGuide.SetActive(true);

                    Time.timeScale = 0;

                    break;
                }
            }
        }
    }

    public void _OnButton()
    {
        Time.timeScale = 1;

        lockButton.SetActive(false);
        LockGuideBG.SetActive(false);
        vegeGuideBG.SetActive(false);
        handGuide.SetActive(false);

        gameObject.SetActive(false);
    }
}
