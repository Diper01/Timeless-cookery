using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinMenu : MonoBehaviour
{
    private MissionManager missionManager;
    private GamePlayMenuManager gamePlayMenuManager;
    private PlayerStats playerStats;
    private LevelManager levelManager;
    private ItemManager itemManager;
    private NextScene nextScene;

    private Combo combo;
    //private AdManager adManager;
    //private GoogleFireBaseEvents googleFireBaseEvents;

    public string sceneName = "WorldMap";

    //public Text levelText;
    public TextMeshPro coinText;
    public TextMeshPro crystalText;

    public GameObject lStar, mStar, rStar;

    public int countTemp = 0;

    private int coinTemp = 0;

    private bool adWatched = false;

    private void Awake()
    {
        //Time.timeScale = 0;
    }

    private void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        gamePlayMenuManager =
            GameObject.FindGameObjectWithTag("GamePlayMenuManager").GetComponent<GamePlayMenuManager>();
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        itemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        nextScene = GameObject.FindGameObjectWithTag("NextScene").GetComponent<NextScene>();
        combo = GameObject.FindGameObjectWithTag("Combo").GetComponent<Combo>();

        //googleFireBaseEvents = FindObjectOfType<GoogleFireBaseEvents>();
        //googleFireBaseEvents.WinLevel(missionManager.levelID, missionManager.currentStar);
        //adManager = FindObjectOfType<AdManager>();
        //IronSource.Agent.displayBanner();
        //facebookAppEvents = GameObject.FindGameObjectWithTag("FacebookAppEvents").GetComponent<FacebookAppEvents>();

        //_FacebookEvents();
        //_GoogleLogEvents();

        FindObjectOfType<AudioManager>()._Pause();

        //levelText.text = levelManager.mapName.ToString() + " Level " + missionManager.levelID;
        coinText.text = "+0";
        crystalText.text = "+0";

        //levelText.gameObject.SetActive(false);
        coinText.gameObject.SetActive(false);
        crystalText.gameObject.SetActive(false);

        _ActiveStars();

        StartCoroutine(_AnimateText());

        _SetToPlayerStats();
        _SetToLevelManager();
        _SetToItemManager();
    }

    private void Update()
    {
        _AddWatchAdCoin();
    }

    void _GoogleLogEvents()
    {
        Debug.Log("Da tai len Level Won");
    }

    void _ActiveStars()
    {
        if (missionManager.gamemode == MissionManager.GameMode.Boss)
        {
            mStar.SetActive(true);
            lStar.SetActive(true);
            rStar.SetActive(true);
        }
        else
        {
            if (missionManager.currentStar == 0)
            {
                mStar.SetActive(true);
            }
            else if (missionManager.currentStar == 1)
            {
                lStar.SetActive(true);
                mStar.SetActive(true);
            }
            else if (missionManager.currentStar == 2 || missionManager.currentStar == 3)
            {
                mStar.SetActive(true);
                lStar.SetActive(true);
                rStar.SetActive(true);
            }
        }
    }

    IEnumerator _AnimateText()
    {
        int crystalTemp = 0;
        float timeCount = 0.05f;

        //levelText.gameObject.SetActive(true);
        coinText.gameObject.SetActive(true);
        crystalText.gameObject.SetActive(true);

        for (int i = 0; i < missionManager.currentPlayerCoin.ToString().Length; i++)
        {
            timeCount = timeCount / 10;
        }

        while (countTemp < missionManager.currentPlayerCoin)
        {
            countTemp++;
            coinText.text = "+" + countTemp;

            yield return new WaitForSeconds(timeCount);
        }

        if(missionManager.hasCrystal)
        {
            while (crystalTemp < missionManager.currentPlayerCoin)
            {
                crystalTemp++;
                crystalText.text = "+" + crystalTemp;

                yield return new WaitForSeconds(0.05f);
            }
        }

        yield return new WaitForSeconds(FindObjectOfType<AudioManager>()._CurrentAudioTime("Win"));
        Time.timeScale = 0;

        yield break;
    }

    public void _NextButton()
    {
        _ShowAd();
        missionManager.scenePos = 0;
        missionManager._ResetMission();
        nextScene.NextLevel(sceneName);
    }

    public void _RestartButton()
    {
        //_FacebookEvents();
        _GoogleLogEvents();
        _ShowAd();
        missionManager._RetryMission();
        Debug.Log("Choi lai");
        nextScene.NextLevel(SceneManager.GetActiveScene().name);
    }

    public void _X2CoinButton()
    {
        //if (IronSource.Agent.isRewardedVideoAvailable() && adWatched == false)
        //{
        //    adWatched = true;

        //    adManager.rewardType = AdManager.REWARD_TYPE.Coin;
        //    adManager.rewardAmount = missionManager.currentPlayerCoin;
        //    adManager.ShowRewardAd(AdManager.REWARD_AD_PLACEMENT.X2_COIN);

        //    //Time.timeScale = 1;

        //    //StartCoroutine(X2CoinCountAnimate());

        //    Debug.Log("Double Coin");
        //}
    }

    void _AddWatchAdCoin()
    {
        //if(adWatched == true && adManager.rewarded == true)
        //{
        //    coinText.text = "+" + missionManager.currentPlayerCoin*2;
        //}
    }

    //IEnumerator X2CoinCountAnimate()
    //{
    //    if(admanager.rewarded == true)
    //    {
    //        float timeCount = 0.05f;

    //        coinText.gameObject.SetActive(true);
    //        crystalText.gameObject.SetActive(true);

    //        for (int i = 0; i < (missionManager.currentPlayerCoin*2).ToString().Length; i++)
    //        {
    //            timeCount = timeCount / 10;
    //        }

    //        while (countTemp < missionManager.currentPlayerCoin*2)
    //        {
    //            countTemp++;
    //            coinText.text = "+" + countTemp;

    //            yield return new WaitForSeconds(timeCount);
    //        }

    //        admanager.rewarded = false;

    //        Time.timeScale = 0;
    //    }
    //    else if(admanager.rewarded == false || admanager.rewardAdClosed == false)
    //    {
    //        StartCoroutine(X2CoinCountAnimate());
    //    }
    //}

    public void _SetToPlayerStats()
    {
        //Add money to player
        playerStats.IncreaseCoin(missionManager.currentPlayerCoin);

            //Add crystal to player
        playerStats.IncreaseCrystal(missionManager.rewardCrystal);

        //Add daily mission values
        if (missionManager.playAgain == false)
        {
            playerStats.todayCusOk += missionManager.customerPaidCount;
            playerStats.todayOrderOk += missionManager.orderOkCount;
            playerStats.todayGoodEmoCusOk += missionManager.customerGoodEmoCount;

            playerStats.todayWin += 1;
        }

        //Add Achivement mission values
        if (missionManager.playAgain == false)
        {
            playerStats.customerOkCount += missionManager.customerPaidCount;
            playerStats.achivOrderOk += missionManager.orderOkCount;
            playerStats.achivGoodEmoCus += missionManager.customerGoodEmoCount;
            playerStats.achivBadCus += missionManager.customerAngryCount;

            playerStats.achivWin += 1;
        }
    }

    void _SetToLevelManager()
    {
        if (missionManager.playAgain == false)
        {
            //save done guide and starindex
            //if (missionManager.levelID == levelManager.guideDone
            //    && missionManager.currentStar > levelManager.starindex
            //    || missionManager.levelID > levelManager.guideDone
            //    && missionManager.currentStar == 0)
            //{
            //    levelManager.guideDone = missionManager.levelID;
            //    levelManager.starindex = missionManager.currentStar + 1;
            //}

            //Add Star to player
            if (missionManager.playAgain == false && missionManager.currentStar < 3)
            {
                if (missionManager.gamemode == MissionManager.GameMode.Boss)
                {
                    levelManager.totalStar += 3;
                }
                else
                {
                    levelManager.totalStar += 1;
                }
            }

            // save level's data info star and level complete
            for (int i = 0; i < levelManager.levels.Length; i++)
            {
                if (levelManager.levels[i].levelID == missionManager.levelID)
                {
                    levelManager.levels[i].levelCompleted = true;

                    if (missionManager.gamemode == MissionManager.GameMode.Boss)
                    {
                        levelManager.levels[i].star = 3;
                    }
                    else
                    {
                        if (levelManager.levels[i].star < 3)
                        {
                            levelManager.levels[i].star += 1;
                        }
                    }

                    break;
                }
            }

            levelManager._SaveData();
        }
    }

    public void _SetToItemManager()
    {
        //if (itemManager.firstPlay == true)
        //{
        //    itemManager.firstPlay = false;
        //    itemManager._SaveData();
        //}

        itemManager._SaveData();
    }

    public void ClickUpgradeWin()
    {
        _ShowAd();
        //googleFireBaseEvents.ClickUpgradeWin();
    }

    void _ShowAd()
    {
        missionManager.winCount++;

        //if (missionManager.levelID >= playerStats.levelAdShow && missionManager.winCount == playerStats.winAdShow)
        //{
        //    missionManager.winCount = 0;

        //    if(adManager.fullScreenAdRequested == false)
        //    {
        //        adManager.RequestFullScreenAd();
        //        adManager.ShowFullScreenAd();
        //    }

        //    else if(adManager.fullScreenAdRequested == true)
        //    {
        //        adManager.ShowFullScreenAd();
        //    }
        //}
    }
}