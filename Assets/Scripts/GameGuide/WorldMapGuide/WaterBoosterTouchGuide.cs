using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBoosterTouchGuide : MonoBehaviour
{
    private PlayerStats playerStats;

    //public GameObject scrollView;
    //public Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        //_SetScrollViewPos();
        _SetPlayerStats();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void _SetPlayerStats()
    {
        playerStats.waterActivated = true;
        if (playerStats.waterAmount < 1)
        {
            playerStats.waterAmount = 1;
        }
    }

    //void _SetScrollViewPos()
    //{
    //    scrollView.transform.localPosition = pos;
    //}
}
