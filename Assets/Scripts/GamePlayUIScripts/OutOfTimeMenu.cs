using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutOfTimeMenu : MonoBehaviour
{
    private PlayerStats playerStats;
    private MissionManager missionManager;
    private GamePlayMenuManager gamePlayMenuManager;
    //private AdManager adManager;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        gamePlayMenuManager = GameObject.FindGameObjectWithTag("GamePlayMenuManager").GetComponent<GamePlayMenuManager>();
        //adManager = FindObjectOfType<AdManager>();
    }

    private void Update()
    {
        if (gameObject.activeSelf == true)
        {
            FindObjectOfType<AudioManager>()._Pause();
            Time.timeScale = 0;

            _AdRewarded();
        }
    }

    public void _AdRewarded()
    {
        //if (adManager.rewarded == true)
        //{
        //    adManager.rewarded = false;

        //    playerStats.freeTurn = 0;

        //    FindObjectOfType<AudioManager>()._UnPause();

        //    Debug.Log("Da them thoi gian");
        //    missionManager.currentTime += 15;
        //    playerStats.DecreaseCrystal(15);

        //    gamePlayMenuManager._DeactiveBlackBG();

        //    missionManager.close = false;

        //    Time.timeScale = 1;

        //    gameObject.SetActive(false);
        //}
    }

    public void _OkButton()
    {
        if (playerStats.crystal >= 15 || playerStats.freeTurn == 1)
        {
            playerStats.freeTurn = 0;

            FindObjectOfType<AudioManager>()._UnPause();

            Debug.Log("Da them thoi gian");
            missionManager.currentTime += 15;
            playerStats.DecreaseCrystal(15);

            Time.timeScale = 1;

            gamePlayMenuManager._DeactiveBlackBG();

            missionManager.close = false;

            gameObject.SetActive(false);
        }
        else if (playerStats.crystal < 15)
        {
            gamePlayMenuManager._ActiveNotEnoughCrystalMenu();
            gamePlayMenuManager.notEnoughCrystalMenu.GetComponent<GameplayNotEnoughCrystalMenu>().gpMenuType = GameplayNotEnoughCrystalMenu.GP_MENU_TYPE.OUT_TIME;
            gameObject.SetActive(false);
        }
    }

    public void _WatchAd()
    {
        //if (IronSource.Agent.isRewardedVideoAvailable())
        //{
        //    adManager.rewardType = AdManager.REWARD_TYPE.freeTurn;
        //    adManager.ShowRewardAd(AdManager.REWARD_AD_PLACEMENT.GAME_OVER);
        //}
    }

    public void _CloseButton()
    {
        FindObjectOfType<AudioManager>()._UnPause();

        gamePlayMenuManager._DeactiveBlackBG();

        missionManager.outOfTimeClosed = true;
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
