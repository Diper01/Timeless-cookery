using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class MushroomBooster : MonoBehaviour
{
    private MissionManager missionManager;
    private PlayerStats playerStats;

    public GameObject mushroomEffect;

    private void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();

        if(playerStats.mushroomAmount > 0 && missionManager.useMushRoom == true)
        {
            playerStats.mushroomAmount -= 1;
            playerStats.achivBoosterUse += 1;
        }
    }

    private void Update()
    {
        _MushroomBooster();
    }

    public float _ActiveEffect()
    {
        mushroomEffect.SetActive(true);

        StartCoroutine(playAudio());

        var myAnim = mushroomEffect.GetComponent<SkeletonAnimation>().skeleton.Data.FindAnimation("animation");
        float destroyTime = myAnim.Duration;

        Destroy(mushroomEffect, destroyTime);

        return destroyTime;
    }

    public void _MushroomBooster()
    {
        GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");

        for (int i = 0; i < customers.Length; i++)
        {
            if (customers[i].GetComponent<Customer>().ordered == true && customers[i].GetComponent<Customer>().isMushroomed == false)
            {
                //customers[i].GetComponent<Customer>().animationUnique();
                customers[i].GetComponent<Customer>().isMushroomed = true;
                customers[i].GetComponent<Customer>().customerEmo = 1;
            }
        }
    }

    IEnumerator playAudio()
    {
        yield return new WaitForSeconds(0.3f);
        mushroomEffect.GetComponent<AudioSource>().Play();
    }
}
