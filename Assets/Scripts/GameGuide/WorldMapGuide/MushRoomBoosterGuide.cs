using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoomBoosterGuide : MonoBehaviour
{
    private PlayerStats playerStats;
    private MissionManager missionManager;

    //public GameObject scrollView;
    //public Vector3 pos;

    public bool isActived = false;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();

        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        //_SetScrollViewPos();
        _SetPlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        _SetPlayerStats();
    }

    void _SetPlayerStats()
    {
        if(isActived == false && missionManager.levelID == 7)
        {
            isActived = true;

            playerStats.mushroomActivated = true;

            if (playerStats.mushroomAmount < 1)
            {
                playerStats.mushroomAmount = 1;
            }
        }
    }

    //void _SetScrollViewPos()
    //{
    //    scrollView.transform.localPosition = pos;
    //}
}
