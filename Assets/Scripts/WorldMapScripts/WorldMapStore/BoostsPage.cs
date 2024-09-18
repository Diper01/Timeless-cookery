using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostsPage : MonoBehaviour
{
    private PlayerStats playerStats;

    //public enum BOOSTER_TYPE { X2GOLD, TIME, MUSHROOM, DRAGON_EGG, X3_CANDLE, WATER, CANDLE, CLIENT_TAGE}

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyBooster()
    {

    }
}
