using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayNotEnoughCrystalMenu : MonoBehaviour
{
    //private AdManager adManager;

    public GamePlayMenuManager gamePlayMenuManager;

    public enum GP_MENU_TYPE { OUT_TIME, OUT_CUS, LOST_CUS, FIRED_FOOD, TRASH_FOOD}

    public GP_MENU_TYPE gpMenuType;

    // Start is called before the first frame update
    void Start()
    {
        //adManager = FindObjectOfType<AdManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _FreeCrystal()
    {
        //adManager = FindObjectOfType<AdManager>();

        //if (IronSource.Agent.isRewardedVideoAvailable())
        //{
        //    adManager.rewardType = AdManager.REWARD_TYPE.freeTurn;
        //    adManager.ShowRewardAd(AdManager.REWARD_AD_PLACEMENT.GAME_OVER);
        //    _CloseButton();
        //}
    }

    public void _BuyCrystal()
    {
        gamePlayMenuManager.insurgencyPackageMenu.SetActive(true);
        gamePlayMenuManager.insurgencyPackageMenu.GetComponent<InsurgencyPackageMenu>().gpMenuType = gpMenuType;
        gameObject.SetActive(false);
    }

    public void _CloseButton()
    {
        if(gpMenuType == GP_MENU_TYPE.OUT_TIME)
        {
            gamePlayMenuManager.outOfTimeMenu.SetActive(true);
        }
        else if (gpMenuType == GP_MENU_TYPE.OUT_CUS)
        {
            gamePlayMenuManager.outOfCustomerMenu.SetActive(true);
        }
        else if (gpMenuType == GP_MENU_TYPE.LOST_CUS)
        {
            gamePlayMenuManager.lostCustomerMenu.SetActive(true);
        }
        else if (gpMenuType == GP_MENU_TYPE.FIRED_FOOD)
        {
            gamePlayMenuManager.friedFoodMenu.SetActive(true);
        }
        else if (gpMenuType == GP_MENU_TYPE.TRASH_FOOD)
        {
            gamePlayMenuManager.foodTrashMenu.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
