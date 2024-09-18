using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShopGuideController : MonoBehaviour
{
    //private GoogleFireBaseEvents googleFireBaseEvents;

    public int activeWorld = 0;
    public int activeLevel = 0;
    public int activeStar = 0;
    public int activeItemLevel = 0;

    private int currentItemLevel = 0;

    public enum ItemType { MainFood, SideFood, Grill, Plate};
    public ItemType itemType;

    public MainFoodUpgrade mainFoodUpgrade;
    public SideFoodUpgrade sideFoodUpgrade;
    public GrillUpgrade grillUpgrade;
    public PlateUpgrade plateUpgrade;

    public GameObject[] guideSteps;

    public int currentStep = 0;

    // Start is called before the first frame update
    void Start()
    {
        //googleFireBaseEvents = GameObject.FindObjectOfType<GoogleFireBaseEvents>();

        _SetCurrentItemLevel();
        _ActiveGuide();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void _SetCurrentItemLevel()
    {
        if(itemType == ItemType.MainFood)
        {
            currentItemLevel = mainFoodUpgrade.currentLevel;
        }
        else if (itemType == ItemType.SideFood)
        {
            currentItemLevel = sideFoodUpgrade.currentLevel;
        }
        else if (itemType == ItemType.Grill)
        {
            currentItemLevel = grillUpgrade.currentLevel;
        }
        else if (itemType == ItemType.MainFood)
        {
            currentItemLevel = grillUpgrade.currentLevel;
        }
    }

    public void _ActiveGuide()
    {
        if(currentItemLevel != activeItemLevel)
        {
            gameObject.SetActive(false);
        }
        else
        {
            guideSteps[0].SetActive(true);
        }
    }

    public void _NextStep()
    {
        currentStep++;

        if (currentStep < guideSteps.Length)
        {
            if (guideSteps[currentStep - 1] != null)
            {
                guideSteps[currentStep - 1].SetActive(false);
            }

            guideSteps[currentStep].SetActive(true);
        }
        else
        if (currentStep == guideSteps.Length)
        {
            guideSteps[currentStep - 1].SetActive(false);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            //googleFireBaseEvents.TutorialQuit("UpgradeShop_" + "activeLevel_" + activeLevel + "activeStar" + activeStar);
        }
    }
}
