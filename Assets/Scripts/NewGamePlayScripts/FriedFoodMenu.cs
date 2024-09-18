using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriedFoodMenu : MonoBehaviour
{
    private MissionManager missionManager;
    private CustomerManager customerManager;
    private PlayerStats playerStats;
    private GamePlayMenuManager gamePlayMenuManager;
    //private AdManager adManager;

    // Start is called before the first frame update
    void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        customerManager = GameObject.FindGameObjectWithTag("CustomerManager").GetComponent<CustomerManager>();
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        gamePlayMenuManager = GameObject.FindGameObjectWithTag("GamePlayMenuManager").GetComponent<GamePlayMenuManager>();
        //adManager = FindObjectOfType<AdManager>();
    }

    // Update is called once per frame
    void Update()
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

        //    missionManager.friedFoodCount = 0;

        //    Debug.Log("Da phuc hoi nhiem vu Food To Trash " + missionManager.customerAngryCount);

        //    playerStats.DecreaseCrystal(15);

        //    gamePlayMenuManager._DeactiveBlackBG();

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

            missionManager.friedFoodCount = 0;

            Debug.Log("Da phuc hoi nhiem vu Food To Trash " + missionManager.customerAngryCount);

            playerStats.DecreaseCrystal(15);

            Time.timeScale = 1;

            gamePlayMenuManager._DeactiveBlackBG();

            gameObject.SetActive(false);
        }
        else if (playerStats.crystal < 15)
        {
            gamePlayMenuManager._ActiveNotEnoughCrystalMenu();
            gamePlayMenuManager.notEnoughCrystalMenu.GetComponent<GameplayNotEnoughCrystalMenu>().gpMenuType = GameplayNotEnoughCrystalMenu.GP_MENU_TYPE.FIRED_FOOD;
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

        missionManager.friedFoodClosed = true;

        Time.timeScale = 1;

        gamePlayMenuManager._DeactiveBlackBG();

        gameObject.SetActive(false);
    }
}
