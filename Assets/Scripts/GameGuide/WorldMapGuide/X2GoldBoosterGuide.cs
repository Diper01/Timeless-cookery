using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X2GoldBoosterGuide : MonoBehaviour
{
    private PlayerStats playerStats;
    private MissionManager missionManager;

    public bool isActived = false;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();

    }

    // Update is called once per frame
    void Update()
    {
        ActiveBooster();
    }

    void ActiveBooster()
    {
        if(isActived == false && missionManager.levelID == 3)
        {
            isActived = true;
            playerStats.x2GoldActivated = true;
            if (playerStats.x2GoldAmount < 1)
            {
                playerStats.x2GoldAmount = 1;
            }
        }
    }

    public void _ActiveOtherBooster()
    {
        playerStats.mushroomActivated = true;
        playerStats.waterActivated = true;
        playerStats.x2GoldActivated = true;
    }
}
