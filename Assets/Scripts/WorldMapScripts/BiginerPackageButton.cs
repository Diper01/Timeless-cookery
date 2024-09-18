using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BiginerPackageButton : MonoBehaviour
{
    private PlayerStats playerStats;

    public UILabel timeCount;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();

        IncrementLaunchCount();
    }

    // Update is called once per frame
    void Update()
    {
        timer();

        _DeactiveBeginerPackage();
    }

    void IncrementLaunchCount()
    {
        if(playerStats.appOpen < 2)
        {
            timeCount.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    void timer()
    {
        DateTime now = DateTime.Now;
        DateTime dateTemp = DateTime.Now.AddDays(1).Date;
        TimeSpan time = dateTemp - now;
        timeCount.text = String.Format("{0:00}:{1:00}:{2:00}", time.Hours, time.Minutes, time.Seconds);
    }

    void _DeactiveBeginerPackage()
    {
        if (playerStats.biginerPurchased == true)
        {
            timeCount.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
