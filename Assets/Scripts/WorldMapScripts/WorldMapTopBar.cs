using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapTopBar : MonoBehaviour
{
    private PlayerStats playerStats;

    public UILabel energyText;
    public UILabel coinText;
    public UILabel crystalText;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInfo();
    }

    void GetInfo()
    {
        //energyText.text = playerStats.energy.ToString();
        coinText.text = playerStats.coin.ToString();
        crystalText.text = playerStats.crystal.ToString();
    }
}
