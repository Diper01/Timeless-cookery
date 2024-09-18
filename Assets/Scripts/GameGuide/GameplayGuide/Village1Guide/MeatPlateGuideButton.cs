using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatPlateGuideButton : MonoBehaviour
{
    private MissionManager missionManager;
    public GameplayGuideController gameplayGuideController;
    public FoodPlate foodPlate;
    public Customer selectedCus = null;

    public GameObject foodPlateGuideBG;
    public GameObject handGuide;
    public GameObject foodPlateGuideNotice;

    public bool isActived = false;

    private void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
    }

    private void Update()
    {
        _SelectCustomer();
        _Active();
    }

    void _Active()
    {
        if (selectedCus != null && selectedCus.ordered == true && isActived == false && foodPlate.isEmpty == false)
        {
            isActived = true;

            foodPlateGuideBG.SetActive(true);
            handGuide.SetActive(true);
            foodPlateGuideNotice.SetActive(true);

            Time.timeScale = 0;
        }
    }

    void _SelectCustomer()
    {
        if(gameObject.activeSelf == true && selectedCus == null && missionManager.customerCount > 0)
        {
            GameObject[] cus = GameObject.FindGameObjectsWithTag("Customer");

            selectedCus = cus[0].GetComponent<Customer>();
        }
    }

    public void _OnButton()
    {
        Time.timeScale = 1;

        //if (foodPlate.isEmpty == false)
        //{
        //    foodPlate._SelectCustomer();
        //    foodPlate._Discard();
        //}

        foodPlateGuideBG.SetActive(false);
        handGuide.SetActive(false);
        foodPlateGuideNotice.SetActive(false);

        gameplayGuideController._NextStep();

        gameObject.SetActive(false);
    }
}
