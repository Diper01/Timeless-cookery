using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FreeCoinDialog : MonoBehaviour
{
    private PlayerStats playerStats;
    //private AdManager admanager;

    public UILabel turnLeft;
    public DialogFreeGift dialogFreeGift;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        //admanager = FindObjectOfType<AdManager>();

        _CheckTime();
    }

    // Update is called once per frame
    void Update()
    {
        _SetText();

        //if(gameObject.activeSelf == true && admanager.rewarded == true)
        //{
        //    admanager.rewarded = false;
          
        //    dialogFreeGift.gameObject.SetActive(true);
        //    dialogFreeGift.rewardType = AdManager.REWARD_TYPE.freeCoin;
        //    dialogFreeGift.rewardAmount = 50;
        //    dialogFreeGift._ActiveImgAndText();

        //    gameObject.SetActive(false);
        //}
    }

    public void _CheckTime()
    {
        DateTime lastTime = DateTime.Parse(playerStats.lastFreeCoinDate + "");

        int checkNewDay = DateTime.Compare(DateTime.Now.Date, lastTime.Date);

        if(checkNewDay > 0)
        {
            playerStats.lastFreeCoinDate = DateTime.Now.ToString();

            playerStats.freeCoinLeft = 10;

            playerStats.save = true;
        }
    }

    void _SetText()
    {
        turnLeft.text = String.Format("{0:00}/{1:00} Left", playerStats.freeCoinLeft, 10);
    }

    public void _WatchAd()
    {
        //if (playerStats.freeCoinLeft > 0 && IronSource.Agent.isRewardedVideoAvailable())
        //{
        //    admanager.rewardType = AdManager.REWARD_TYPE.freeCoin;

        //    admanager.ShowRewardAd(AdManager.REWARD_AD_PLACEMENT.FREE_COIN);
        //}
    }
}
