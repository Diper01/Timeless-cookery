using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrillUpgradeGuide : MonoBehaviour
{
    private PlayerStats playerStats;

    public GrillUpgrade grillUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        _AddMoney();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _AddMoney()
    {
        playerStats.freeTurn += 1;

        grillUpgrade.priceText.text = "FREE";

    }
}
