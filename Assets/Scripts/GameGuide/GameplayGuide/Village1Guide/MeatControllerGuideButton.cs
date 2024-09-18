using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatControllerGuideButton : MonoBehaviour
{
    private MissionManager missionManager;
    public GameplayGuideController gameplayGuideController;
    public Customer selectedCus = null;

    public GameObject meatGuideBlockBG;
    public GameObject meatGuideBG;
    public GameObject handGuide;
    public GameObject meatGuideNotice;

    //public GameObject blackBG;

    public bool isActived;

    private void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();

        meatGuideBlockBG.SetActive(true);
    }

    private void Update()
    {
        _SelectCustomer();
        _Active();
    }

    void _Active()
    {
        if (selectedCus != null && isActived == false)
        {
            isActived = true;

            meatGuideBlockBG.SetActive(false);

            meatGuideBG.SetActive(true);
            handGuide.SetActive(true);
            meatGuideNotice.SetActive(true);

            Time.timeScale = 0;
        }
    }

    void _SelectCustomer()
    {
        if (gameObject.activeSelf == true && selectedCus == null && missionManager.customerCount > 0 && isActived == false)
        {
            GameObject[] cus = GameObject.FindGameObjectsWithTag("Customer");

            selectedCus = cus[0].GetComponent<Customer>();
        }
    }

    public void _OnButton()
    {
        Time.timeScale = 1;

        meatGuideBG.SetActive(false);
        handGuide.SetActive(false);
        meatGuideNotice.SetActive(false);

        gameObject.SetActive(false);
    }
}
