using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeEnergyButton : MonoBehaviour
{
    //private AdManager adManager;
    private PlayerStats playerStats;

    private void Start()
    {
        //adManager = GameObject.FindGameObjectWithTag("AdManager").GetComponent<AdManager>();
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
    }

    public void _OnTouch()
    {
        //adManager.energyReward = true;
        //adManager.ShowRewardAd();

        //if(adManager.fullScreenAdRequested == true && adManager.fullScreenAdShowed == false)
        //{
        //    adManager.ShowFullScreenAd();
        //}

        //playerStats.energy += 3;
        //playerStats.save = true;
    }
}
