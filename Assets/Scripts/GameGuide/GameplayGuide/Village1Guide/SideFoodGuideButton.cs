using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideFoodGuideButton : MonoBehaviour
{
    private MissionManager missionManager;

    public SideFoodPos sideFoodPos;

    public GameObject lockBG;
    public GameObject fishGuideLockBG;
    public GameObject fishGuideBG;
    public GameObject handGuide;

    public string cusOrderId = "";

    public bool isActived = false;

    // Start is called before the first frame update
    void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        lockBG.SetActive(true);
        fishGuideLockBG.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        _ActiveGuide();
    }
    float delayTimeCount;
    void _ActiveGuide()
    {
        if (sideFoodPos.isEmpty == false
            && isActived == false
            && missionManager.customerCount > 0)
        {
            GameObject[] cus = GameObject.FindGameObjectsWithTag("Customer");

            for (int i = 0; i < cus.Length; i++)
            {
                if (cus[i].GetComponent<Customer>().ordered == true)
                {

                    if (cus[i].GetComponent<Customer>().currentFirstOrderId == cusOrderId
                        || cus[i].GetComponent<Customer>().currentSecondOrderId == cusOrderId
                        || cus[i].GetComponent<Customer>().currentThirdOrderId == cusOrderId)
                    {
                        delayTimeCount += Time.deltaTime;
                        if (delayTimeCount >= 0.5f)
                        {
                            isActived = true;

                            fishGuideLockBG.SetActive(false);

                            fishGuideBG.SetActive(true);
                            handGuide.SetActive(true);

                            Time.timeScale = 0;
                        }

                       
                      

                        break;
                    }
                }
            }
        }
    }

    public void _OnButton()
    {
        Time.timeScale = 1;

        sideFoodPos._SelectCustomer();

        fishGuideBG.SetActive(false);
        handGuide.SetActive(false);

        gameObject.SetActive(false);
    }
}
