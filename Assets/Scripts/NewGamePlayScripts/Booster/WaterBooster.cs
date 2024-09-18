using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class WaterBooster : MonoBehaviour
{
    private PlayerStats playerStats;
    private MissionManager missionManager;

    public GameObject waterEffect;

    private void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();

        if (playerStats.waterAmount > 0 && missionManager.useWater == true)
        {
            playerStats.waterAmount -= 1;
            playerStats.achivBoosterUse += 1;
        }
    }

    private void Update()
    {
        _WaterBooster();
    }

    public float _ActiveEffect()
    {
        waterEffect.SetActive(true);

        StartCoroutine(playAudio());

        var myAnim = waterEffect.GetComponent<SkeletonAnimation>().skeleton.Data.FindAnimation("123");
        float destroyTime = myAnim.Duration;

        Destroy(waterEffect, destroyTime);

        return destroyTime;
    }

    public void _WaterBooster()
    {
        GameObject[] grills = GameObject.FindGameObjectsWithTag("Grill");

        for (int i = 0; i < grills.Length; i++)
        {
            if (grills[i].GetComponent<Grill>().isProcessed == true
                && grills[i].GetComponent<Grill>().isOverBurned == false)
            {
                grills[i].GetComponent<Grill>().isFreeze = true;
            }
        }
    }

    IEnumerator playAudio()
    {
        yield return new WaitForSeconds(0.3f);
        waterEffect.GetComponent<AudioSource>().Play();

        yield break;
    }
}