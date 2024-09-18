using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggGuideButton : MonoBehaviour
{
    private PlayerStats playerStats;
    private MissionManager missionManager;
    public InGameBoosterController inGameBoosterController;
    public EggBooster eggBooster;

    public GameObject guideBG, handGuide, guideNotice;

    public Customer selectedCus;
    public bool cusSelected = false;

    public bool isAvtived = false;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _SelectCustomer();
        ActiveBooster();
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

    void ActiveBooster()
    {
        if (isAvtived == false && selectedCus!= null && selectedCus.ordered == true)
        {
            isAvtived = true;

            playerStats.eggActivated = true;
            
            if(playerStats.eggAmount < 1)
            {
                playerStats.eggAmount = 1;
            }

            inGameBoosterController._SetActive();

            guideBG.SetActive(true);
            handGuide.SetActive(true);
            guideNotice.SetActive(true);
        }
    }

    public void _OnButton()
    {
        guideBG.SetActive(false);
        handGuide.SetActive(false);
        guideNotice.SetActive(false);

        gameObject.SetActive(false);
    }
}
