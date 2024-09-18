using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPageObject : MonoBehaviour
{
    private PlayerStats playerStats;

    public CrystalBuyComfirmPopup crystalBuyComfirmPopup;
    public WorldMapNotEnoughCrystal worldMapNotEnoughCrystal;

    public enum GOLD_TYPE { NULL, GOLD_100, GOLD_600, GOLD_1300, GOLD_3400, GOLD_7000, GOLD_14400 }

    public GOLD_TYPE gOLD_TYPE = GOLD_TYPE.NULL;

    public int price = 0;
    public UILabel priceText;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        _SetText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void _SetText()
    {
        priceText.text = price.ToString();
    }

    void _SetToPlayerStats()
    {
        playerStats.DecreaseCrystal(price);
        playerStats.save = true;
    }

    public void _OnButton()
    {
        crystalBuyComfirmPopup.gameObject.SetActive(true);
        crystalBuyComfirmPopup.goldPageObject = gameObject.GetComponent<GoldPageObject>();
        crystalBuyComfirmPopup.gOLD_TYPE = gOLD_TYPE;
        crystalBuyComfirmPopup._SetText();
    }

    public void _BuyBooster()
    {
        if (playerStats.crystal >= price)
        {
            if (gOLD_TYPE == GOLD_TYPE.GOLD_100)
            {
                playerStats.IncreaseCoin(100);
                _SetToPlayerStats();
            }
            else if (gOLD_TYPE == GOLD_TYPE.GOLD_600)
            {
                playerStats.IncreaseCoin(600);
                _SetToPlayerStats();
            }
            else if (gOLD_TYPE == GOLD_TYPE.GOLD_1300)
            {
                playerStats.IncreaseCoin(1300);
                _SetToPlayerStats();
            }
            else if (gOLD_TYPE == GOLD_TYPE.GOLD_3400)
            {
                playerStats.IncreaseCoin(3400);
                _SetToPlayerStats();
            }
            else if (gOLD_TYPE == GOLD_TYPE.GOLD_7000)
            {
                playerStats.IncreaseCoin(7000);
                _SetToPlayerStats();
            }
            else if (gOLD_TYPE == GOLD_TYPE.GOLD_14400)
            {
                playerStats.IncreaseCoin(14400);
                _SetToPlayerStats();
            }
        }
        else if(playerStats.crystal < price)
        {
            worldMapNotEnoughCrystal.gameObject.SetActive(true);
        }
    }
}
