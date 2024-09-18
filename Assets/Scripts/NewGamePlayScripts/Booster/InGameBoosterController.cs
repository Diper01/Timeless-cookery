using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameBoosterController : MonoBehaviour
{
    private PlayerStats playerStats;

    public GameObject x3CandleBooster, eggBooster;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        _SetActive();
    }

    public void _SetActive()
    {
        if(playerStats.x3CandleActivated == true)
        {
            x3CandleBooster.SetActive(true);
        }
        else if(playerStats.x3CandleActivated == false)
        {
            x3CandleBooster.SetActive(false);
        }

        if (playerStats.eggActivated == true)
        {
            eggBooster.SetActive(true);
        }
        else if (playerStats.eggActivated == false)
        {
            eggBooster.SetActive(false);
        }
    }
}
