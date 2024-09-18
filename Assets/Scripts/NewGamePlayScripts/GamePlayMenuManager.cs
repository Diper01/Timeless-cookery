using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayMenuManager : MonoBehaviour
{
    private MissionManager missionManager;
    //private AdManager adManager;

    public GameObject blackBG;

    [Header("Menu")]
    public GameObject winNotice;
    public GameObject winMenu;
    public GameObject loseNotice;
    public GameObject loseMenu;
    public GameObject pauseMenu;
    public GameObject outOfCustomerMenu;
    public GameObject outOfTimeMenu;
    public GameObject lostCustomerMenu;
    public GameObject foodTrashMenu;
    public GameObject friedFoodMenu;
    public GameObject notEnoughCrystalMenu;
    public GameObject notEnoughEnergyMenu;

    public GameObject insurgencyPackageMenu;

    public bool missionDone = false;
    public bool win = false;
    public bool lose = false;
    //private GoogleFireBaseEvents googleFireBaseEvents;
    private void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        //googleFireBaseEvents = FindObjectOfType<GoogleFireBaseEvents>();
        //IronSource.Agent.hideBanner();
        Time.timeScale = 1;
        //missionManager.open = true;
        missionManager.close = false;
      //  adManager = FindObjectOfType<AdManager>();
      //  adManager.RequestFullScreenAd();
        //googleFireBaseEvents.PlayLevel(missionManager.levelID, missionManager.starSet);
    }
    void OnApplicationPause(bool isPaused)
    {
        //IronSource.Agent.onApplicationPause(isPaused);
    }
    private void Update()
    {
        if(missionManager.gamemode  == MissionManager.GameMode.Normal)
        {
            _winMenuControll();
            _LoseMenuControll();
            _OutOfCustomerMenu();
            _OutOfTimeMenu();
            _LostCustomerMissionMenu();
            _FoodToTrashMissionMenu();
            _FriedFoodMissionMenu();
        }
        else if(missionManager.gamemode == MissionManager.GameMode.Boss)
        {
            _BossWinMenu();
            _BossLoseMenu();
        }
    }

    void _winMenuControll()
    {
        if (missionManager.firstTarget == MissionManager.FirstTarget.Time)
        {
            if (missionManager.secondTarget == MissionManager.SecondTarget.Null)
            {
                if(missionManager.currentTime <= 0)
                {
                    missionDone = true;
                    win = true;
                }
            }
            else if (missionManager.secondTarget == MissionManager.SecondTarget.Coin)
            {
                if(missionManager.currentPlayerCoin >= missionManager.coinCondition)
                {
                    missionDone = true;
                    missionManager.close = true;
                }

                if (missionDone && missionManager.currentTime <= 0)
                {
                    win = true;
                }
            }
            else if (missionManager.secondTarget == MissionManager.SecondTarget.Dish)
            {
                if (missionManager.dishCount >= missionManager.dishCondition)
                {
                    missionDone = true;
                    missionManager.close = true;
                }

                if (missionDone &&  missionManager.currentTime <= 0)
                {
                    win = true;
                }
            }
            else if (missionManager.secondTarget == MissionManager.SecondTarget.Like)
            {
                if (missionManager.customerGoodEmoCount >= missionManager.customerGoodCondition)
                {
                    missionDone = true;
                    missionManager.close = true;
                }

                if (missionDone && missionManager.currentTime <= 0)
                {
                    win = true;
                }
            }
        }
        else if (missionManager.firstTarget == MissionManager.FirstTarget.Customer)
        {
            if (missionManager.secondTarget == MissionManager.SecondTarget.Null)
            {
                if (missionManager.customerCount >= missionManager.customerLimit)
                {
                    missionDone = true;
                    missionManager.close = true;
                }

                if (missionDone == true && missionManager.currentCustomerCount <= 0 && missionManager.customerLeft <= 0)
                {
                    win = true;
                }
            }
            else if (missionManager.secondTarget == MissionManager.SecondTarget.Coin)
            {
                if (missionManager.currentPlayerCoin >= missionManager.coinCondition 
                    && missionManager.customerCount <= missionManager.customerLimit)
                {
                    missionDone = true;
                    missionManager.close = true;
                }

                if (missionDone == true && missionManager.currentCustomerCount <= 0 && missionManager.customerLeft <= 0)
                {
                    win = true;
                }
            }
            else if (missionManager.secondTarget == MissionManager.SecondTarget.Dish)
            {
                if (missionManager.dishCount >= missionManager.dishCondition
                    && missionManager.customerCount <= missionManager.customerLimit)
                {
                    missionDone = true;
                    missionManager.close = true;
                }

                if (missionDone == true && missionManager.currentCustomerCount <= 0 && missionManager.customerLeft <= 0)
                {
                    win = true;
                }
            }
            else if (missionManager.secondTarget == MissionManager.SecondTarget.Like)
            {
                if (missionManager.customerGoodEmoCount >= missionManager.customerGoodCondition
                    && missionManager.customerCount <= missionManager.customerLimit)
                {
                    missionDone = true;
                    missionManager.close = true;
                }

                if (missionDone && missionManager.currentCustomerCount <= 0 && missionManager.customerLeft <= 0)
                {
                    win = true;
                }
            }
        }

        if (win  && winMenu.activeSelf == false 
            && lose == false && loseNotice.activeSelf == false && loseMenu.activeSelf == false)
        {
            _ActiveBlackBG();
            winNotice.SetActive(true);
        }
    }

    void _LoseMenuControll()
    {
        if (missionManager.firstTarget == MissionManager.FirstTarget.Time)
        {
            if (missionManager.secondTarget == MissionManager.SecondTarget.Coin)
            {
                if (missionManager.currentPlayerCoin < missionManager.coinCondition
                    && missionManager.currentTime <= 0
                    && missionManager.outOfTimeClosed == true)
                {
                    lose = true;
                }
            }
            else if (missionManager.secondTarget == MissionManager.SecondTarget.Dish)
            {
                if (missionManager.dishCount < missionManager.dishCondition
                    && missionManager.currentTime <= 0
                    && missionManager.outOfTimeClosed == true)
                {
                    lose = true;
                }
            }
            else if (missionManager.secondTarget == MissionManager.SecondTarget.Like)
            {
                if (missionManager.customerGoodEmoCount < missionManager.customerGoodCondition
                    && missionManager.currentTime <= 0
                    && missionManager.outOfTimeClosed == true)
                {
                    lose = true;
                }
            }
        }
        else if (missionManager.firstTarget == MissionManager.FirstTarget.Customer)
        {
            if (missionManager.secondTarget == MissionManager.SecondTarget.Coin)
            {
                if (missionManager.currentPlayerCoin < missionManager.coinCondition
                    && missionManager.customerLeft <=0 && missionManager.currentCustomerCount <=0 
                    && missionManager.outOfCustomerClosed == true 
                    && missionManager.currentCustomerCount <= 0)
                {
                    lose = true;
                }
            }
            else if (missionManager.secondTarget == MissionManager.SecondTarget.Dish)
            {
                if (missionManager.dishCount < missionManager.dishCondition 
                    && missionManager.customerLeft <= 0 
                    && missionManager.currentCustomerCount <= 0 
                    && missionManager.outOfCustomerClosed == true
                    && missionManager.currentCustomerCount <= 0)
                {
                    lose = true;
                }
            }
            else if (missionManager.secondTarget == MissionManager.SecondTarget.Like)
            {
                if (missionManager.customerGoodEmoCount < missionManager.customerGoodCondition 
                    && missionManager.customerLeft <= 0 
                    && missionManager.currentCustomerCount <= 0 
                    && missionManager.outOfCustomerClosed == true
                    && missionManager.currentCustomerCount <= 0)
                {
                    lose = true;
                }
            }
        }

        if (missionManager.lostCutomerMission == true && missionManager.lostCustomerClosed == true)
        {
            lose = true;
        }
        if (missionManager.friedFoodMission == true && missionManager.friedFoodClosed == true)
        {
            lose = true;
        }
        if (missionManager.foodToTrashMission == true && missionManager.foodToTrashClosed == true)
        {
            lose = true;
        }

        if (lose == true && loseMenu.activeSelf == false && notEnoughEnergyMenu.activeSelf == false
            && win == false && winNotice.activeSelf == false && winMenu.activeSelf == false)
        {
            _ActiveBlackBG();
            loseNotice.SetActive(true);
        }
    }

    void _OutOfTimeMenu()
    {
        if (missionManager.firstTarget == MissionManager.FirstTarget.Time
            && missionManager.secondTarget != MissionManager.SecondTarget.Null
            && missionManager.firstTargetStats == 0
            && missionManager.outOfTimeClosed == false
            && missionManager.currentTime <= 0
            && notEnoughCrystalMenu.activeSelf == false
            && insurgencyPackageMenu.activeSelf == false
            && winNotice.activeSelf == false
            && winMenu.activeSelf == false
            && loseNotice.activeSelf == false
            && loseMenu.activeSelf == false
            && missionDone == false)
        {
            if (outOfTimeMenu.activeSelf == false)
            {
                _ActiveBlackBG();
                outOfTimeMenu.SetActive(true);
            }
        }
    }

    void _OutOfCustomerMenu()
    {
        if (missionManager.firstTarget == MissionManager.FirstTarget.Customer
            && missionManager.secondTarget != MissionManager.SecondTarget.Null
            && missionManager.firstTargetStats == 0
            && missionManager.currentCustomerCount <= 0
            && missionManager.outOfCustomerClosed == false
            && missionManager.customerCount > 0
            && missionManager.customerLimit > 0
            && missionManager.customerCount == missionManager.customerLimit
            && notEnoughCrystalMenu.activeSelf == false 
            && insurgencyPackageMenu.activeSelf == false
            && winNotice.activeSelf == false
            && winMenu.activeSelf == false
            && loseNotice.activeSelf == false
            && loseMenu.activeSelf == false
            && missionDone == false)
        {
            if (outOfCustomerMenu.activeSelf == false)
            {
                _ActiveBlackBG();
                outOfCustomerMenu.SetActive(true);
            }
        }
    }

    void _LostCustomerMissionMenu()
    {
        if (missionManager.lostCutomerMission == true
            && missionManager.customerAngryCount > missionManager.allowAngryCustomer
            && missionManager.lostCustomerClosed == false
            && notEnoughCrystalMenu.activeSelf == false 
            && insurgencyPackageMenu.activeSelf == false
            && winNotice.activeSelf == false
            && winMenu.activeSelf == false
            && loseNotice.activeSelf == false
            && loseMenu.activeSelf == false)
        {
            if(lostCustomerMenu.activeSelf == false)
            {
                _ActiveBlackBG();
                lostCustomerMenu.SetActive(true);
            }
        }
    }

    void _FriedFoodMissionMenu()
    {
        if (missionManager.friedFoodMission == true
            && missionManager.friedFoodCount > missionManager.allowFriedFood
            && missionManager.friedFoodClosed == false
            && notEnoughCrystalMenu.activeSelf == false 
            && insurgencyPackageMenu.activeSelf == false
            && winNotice.activeSelf == false
            && winMenu.activeSelf == false
            && loseNotice.activeSelf == false
            && loseMenu.activeSelf == false)
        {
            if (friedFoodMenu.activeSelf == false)
            {
                _ActiveBlackBG();
                friedFoodMenu.SetActive(true);
            }
        }
    }

    void _FoodToTrashMissionMenu()
    {
        if(missionManager.foodToTrashMission == true
            && missionManager.foodToTrashCount > missionManager.allowTrashFood
            && missionManager.foodToTrashClosed == false
            && notEnoughCrystalMenu.activeSelf == false 
            && insurgencyPackageMenu.activeSelf == false
            && winNotice.activeSelf == false
            && winMenu.activeSelf == false
            && loseNotice.activeSelf == false
            && loseMenu.activeSelf == false)
        {
            if(foodTrashMenu.activeSelf == false)
            {
                _ActiveBlackBG();
                foodTrashMenu.SetActive(true);
            }
        }
    }

    void _BossWinMenu()
    {
        if(missionManager.currentTime > 0 && missionManager.dishCount >= missionManager.dishCondition)
        {
            missionDone = true;
            win = true;

            if (win == true && winMenu.activeSelf == false
            && lose == false && loseNotice.activeSelf == false && loseMenu.activeSelf == false)
            {
                _ActiveBlackBG();
                winNotice.SetActive(true);
            }
        }
    }

    void _BossLoseMenu()
    {
        if (missionManager.currentTime <= 0 && missionManager.dishCount < missionManager.dishCondition)
        {

            lose = true;

            if (lose == true && loseMenu.activeSelf == false && notEnoughEnergyMenu.activeSelf == false
            && win == false && winNotice.activeSelf == false && winMenu.activeSelf == false)
            {
                _ActiveBlackBG();
                loseNotice.SetActive(true);
            }
        }
    }

    public void _PauseButton()
    {
        _ActiveBlackBG();
        pauseMenu.SetActive(true);
    }

    public void _ActiveNotEnoughEnergyMenu()
    {
        notEnoughEnergyMenu.SetActive(true);
    }
    public void _DeactiveNotEnoughEnergyMenu()
    {
        notEnoughEnergyMenu.SetActive(false);
    }

    public void _ActiveNotEnoughCrystalMenu()
    {
        notEnoughCrystalMenu.SetActive(true);
    }
    public void _DeactiveNotEnoughCrystalMenu()
    {
        notEnoughCrystalMenu.SetActive(false);
    }

    public void _ActiveBlackBG()
    {
        blackBG.SetActive(true);
    }

    public void _DeactiveBlackBG()
    {
        blackBG.SetActive(false);
    }
}
