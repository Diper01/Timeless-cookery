using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostsPageObject : MonoBehaviour
{
    private PlayerStats playerStats;

    public CrystalBuyComfirmPopup crystalBuyComfirmPopup;
    public WorldMapNotEnoughCrystal worldMapNotEnoughCrystal;

    public enum BOOSTER_TYPE { NULL, X2GOLD, TIME, MUSHROOM, DRAGON_EGG, X3_CANDLE, WATER}

    public BOOSTER_TYPE bOOSTER_TYPE = BOOSTER_TYPE.NULL;

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
        crystalBuyComfirmPopup.boostsPageObject = gameObject.GetComponent<BoostsPageObject>();
        crystalBuyComfirmPopup.bOOSTER_TYPE = bOOSTER_TYPE;
        crystalBuyComfirmPopup._SetText();
    }

    public void _BuyBooster()
    {
        if(playerStats.crystal >= price)
        {
            if(bOOSTER_TYPE == BOOSTER_TYPE.X2GOLD)
            {
                playerStats.x2GoldAmount += 1;
                _SetToPlayerStats();
            }
            else if (bOOSTER_TYPE == BOOSTER_TYPE.MUSHROOM)
            {
                playerStats.mushroomAmount += 1;
                _SetToPlayerStats();
            }
            else if (bOOSTER_TYPE == BOOSTER_TYPE.DRAGON_EGG)
            {
                playerStats.eggAmount += 1;
                _SetToPlayerStats();
            }
            else if (bOOSTER_TYPE == BOOSTER_TYPE.X3_CANDLE)
            {
                playerStats.x3CandleAmount += 1;
                _SetToPlayerStats();
            }
            else if (bOOSTER_TYPE == BOOSTER_TYPE.WATER)
            {
                playerStats.waterAmount += 1;
                _SetToPlayerStats();
            }
        }
        else if(playerStats.crystal < price)
        {
            worldMapNotEnoughCrystal.gameObject.SetActive(true);
        }
    }
}
