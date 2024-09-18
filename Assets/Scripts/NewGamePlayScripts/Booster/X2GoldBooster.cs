using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class X2GoldBooster : MonoBehaviour
{
    private MissionManager missionManager;
    private GamePlayMenuManager gamePlayMenuManager;
    private PlayerStats playerStats;
    private Combo combo;

    public GameObject x2CoinEffect;

    private bool x2Gold = false;

    private void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        gamePlayMenuManager = GameObject.FindGameObjectWithTag("GamePlayMenuManager").GetComponent<GamePlayMenuManager>();
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        combo = GameObject.FindGameObjectWithTag("Combo").GetComponent<Combo>();

        if (missionManager.useX2Gold && playerStats.x2GoldAmount >0)
        {
            playerStats.x2GoldAmount -= 1;
            playerStats.achivBoosterUse += 1;
        }
    }

    private void Update()
    {
        if(gameObject.activeSelf == true)
        {
            _X2GoldBooster();
        }
    }

    public void _X2GoldBooster()
    {
        if(x2Gold == false && gamePlayMenuManager.win == true || x2Gold == false && gamePlayMenuManager.lose == true)
        {
            //playerStats.coin += (missionManager.currentPlayerCoin + combo.comboCoin)*2;
            playerStats.coin += missionManager.currentPlayerCoin * 2;
            x2Gold = true;
        }
    }

    public float _ActiveEffect()
    {
        x2CoinEffect.SetActive(true);

        StartCoroutine(playAudio());

        var myAnim = x2CoinEffect.GetComponent<SkeletonAnimation>().skeleton.Data.FindAnimation("123");
        float destroyTime = myAnim.Duration;

        Destroy(x2CoinEffect, destroyTime);

        return destroyTime;
    }

    IEnumerator playAudio()
    {
        yield return new WaitForSeconds(0.3f);
        x2CoinEffect.GetComponent<AudioSource>().Play();

        yield break;
    }
}
