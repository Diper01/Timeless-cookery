using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DailyObject : MonoBehaviour
{
    private PlayerStats playerStats;
    //private GoogleFireBaseEvents googleFireBaseEvents;

    public int index = 0;

    public enum REWARD_TYPE
    {
        GOLD,DRAGON_EGG,MUSHROOM,CRYSTAL,X3CANDLE
    }
    public enum DAILY_STATUS
    {
        NOT_READY,READY,CLAIMED
    }
    public REWARD_TYPE rewardType;
    public DAILY_STATUS dailyStatus= DAILY_STATUS.NOT_READY;
    //  public int rewardType;  // 0 gold  1 dragon egg
    public int value;

    public GameObject coinPos, crystalPos;

    public GameObject txtValue,claimedImg,avaiableImg;

    public GameObject coinEffect, crystalEffect;


    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        //googleFireBaseEvents = FindObjectOfType<GoogleFireBaseEvents>();
    }

    private void Update()
    {
        _DailyObjStats();
        _SetText();
    }

    void _SetText()
    {
        if (rewardType == REWARD_TYPE.GOLD || rewardType == REWARD_TYPE.CRYSTAL)
        {
            txtValue.GetComponent<UILabel>().text = "" + value;
        }
        else
        {
            txtValue.GetComponent<UILabel>().text = "x " + value;
        }
    }

    void _DailyObjStats()
    {
        if (dailyStatus == DAILY_STATUS.READY)
        {
            avaiableImg.SetActive(true);
            claimedImg.SetActive(false);
        }
        else if (dailyStatus == DAILY_STATUS.CLAIMED)
        {
            avaiableImg.SetActive(false);
            claimedImg.SetActive(true);
        }
        else
        {
            avaiableImg.SetActive(false);
            claimedImg.SetActive(false);
        }
    }

    public void _OnButton()
    {
        if (dailyStatus == DAILY_STATUS.READY)
        {
            if(playerStats.dailyTurn <= 7)
            {
                playerStats.dailyTurn += 1;

                playerStats.dailyStats = DAILY_STATUS.CLAIMED;

                _GetReward();
                playerStats.save = true;

                dailyStatus = playerStats.dailyStats;
            }

            if (rewardType == REWARD_TYPE.GOLD)
            {
                StartCoroutine(_CoinEffect());
            }
            else if (rewardType == REWARD_TYPE.CRYSTAL)
            {
                StartCoroutine(_CrystalEffect());
            }
        }
    }

    void _GetReward()
    {
        if(rewardType == REWARD_TYPE.GOLD)
        {
            playerStats.coin += value;
        }
        else if(rewardType == REWARD_TYPE.CRYSTAL)
        {
            playerStats.crystal += value;
        }
        else if (rewardType == REWARD_TYPE.DRAGON_EGG)
        {
            playerStats.eggAmount += value;
        }
        else if (rewardType == REWARD_TYPE.MUSHROOM)
        {
            playerStats.mushroomAmount += value;
        }
        else if (rewardType == REWARD_TYPE.X3CANDLE)
        {
            playerStats.x3CandleAmount += value;
        }

        //googleFireBaseEvents.DailyDay(index);
    }

    IEnumerator _CoinEffect()
    {
        for (int i = 0; i < 3; i++)
        {
            coinEffect.GetComponent<WorldMapCoinEffect>().targetPos = coinPos.transform.position;
            Instantiate(coinEffect, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }

        yield break;
    }

    IEnumerator _CrystalEffect()
    {
        for (int i = 0; i < 3; i++)
        {
            crystalEffect.GetComponent<WorldMapCrystalEffect>().targetPos = crystalPos.transform.position;
            Instantiate(crystalEffect, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }

        yield break;
    }
}
