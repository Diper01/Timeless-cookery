using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTargetObject : MonoBehaviour
{
    private MissionManager missionManager;

    public UILabel amountText;

    public GameObject timeImg, cusImg;

    // Start is called before the first frame update
    void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _DeactiveImage()
    {
        timeImg.SetActive(false);
        cusImg.SetActive(false);
    }

    public void _SetActiveImage()
    {
        _DeactiveImage();

        if (missionManager.firstTarget == MissionManager.FirstTarget.Time)
        {
            timeImg.SetActive(true);
            amountText.text = ": " + missionManager.missionTimeLimit.ToString();
        }
        else if (missionManager.firstTarget == MissionManager.FirstTarget.Customer)
        {
            cusImg.SetActive(true);
            amountText.text = ": " + missionManager.customerLimit.ToString();

        }
    }
}
