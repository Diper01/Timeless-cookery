using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBuyComfirmPopup : MonoBehaviour
{
    public GoldPageObject goldPageObject;
    public GoldPageObject.GOLD_TYPE gOLD_TYPE = GoldPageObject.GOLD_TYPE.NULL;
    public BoostsPageObject boostsPageObject;
    public BoostsPageObject.BOOSTER_TYPE bOOSTER_TYPE = BoostsPageObject.BOOSTER_TYPE.NULL;

    public UILabel titleText, itemNameText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //_SetText();
    }

    public void _SetText()
    {
        switch (gOLD_TYPE)
        {
            case GoldPageObject.GOLD_TYPE.GOLD_100:
                {
                    titleText.text = "buy coin";
                    itemNameText.text = "100 coin";
                    break;
                }
            case GoldPageObject.GOLD_TYPE.GOLD_600:
                {
                    titleText.text = "buy coin";
                    itemNameText.text = "600 coin";
                    break;
                }
            case GoldPageObject.GOLD_TYPE.GOLD_1300:
                {
                    titleText.text = "buy coin";
                    itemNameText.text = "1300 coin";
                    break;
                }
            case GoldPageObject.GOLD_TYPE.GOLD_3400:
                {
                    titleText.text = "buy coin";
                    itemNameText.text = "3400 coin";
                    break;
                }
            case GoldPageObject.GOLD_TYPE.GOLD_7000:
                {
                    titleText.text = "buy coin";
                    itemNameText.text = "7000 coin";
                    break;
                }
            case GoldPageObject.GOLD_TYPE.GOLD_14400:
                {
                    titleText.text = "buy coin";
                    itemNameText.text = "14400 coin";
                    break;
                }
        }

        switch (bOOSTER_TYPE)
        {
            case BoostsPageObject.BOOSTER_TYPE.DRAGON_EGG:
                {
                    titleText.text = "buy booster";
                    itemNameText.text = "dragon egg";
                    break;
                }
            case BoostsPageObject.BOOSTER_TYPE.MUSHROOM:
                {
                    titleText.text = "buy booster";
                    itemNameText.text = "mush room";
                    break;
                }
            case BoostsPageObject.BOOSTER_TYPE.TIME:
                {
                    titleText.text = "buy booster";
                    itemNameText.text = "time";
                    break;
                }
            case BoostsPageObject.BOOSTER_TYPE.WATER:
                {
                    titleText.text = "buy booster";
                    itemNameText.text = "water";
                    break;
                }
            case BoostsPageObject.BOOSTER_TYPE.X2GOLD:
                {
                    titleText.text = "buy booster";
                    itemNameText.text = "x2 gold";
                    break;
                }
            case BoostsPageObject.BOOSTER_TYPE.X3_CANDLE:
                {
                    titleText.text = "buy booster";
                    itemNameText.text = "Snow Flake";
                    break;
                }
        }
    }

    public void _BuyButton()
    {
        if (goldPageObject != null)
        {
            goldPageObject._BuyBooster();
        }

        if (boostsPageObject != null)
        {
            boostsPageObject._BuyBooster();
        }

        goldPageObject = null;
        boostsPageObject = null;

        gOLD_TYPE = GoldPageObject.GOLD_TYPE.NULL;
        bOOSTER_TYPE = BoostsPageObject.BOOSTER_TYPE.NULL;

        gameObject.SetActive(false);
    }

    public void _Cancel()
    {
        goldPageObject = null;
        boostsPageObject = null;

        gOLD_TYPE = GoldPageObject.GOLD_TYPE.NULL;
        bOOSTER_TYPE = BoostsPageObject.BOOSTER_TYPE.NULL;

        gameObject.SetActive(false);
    }
}
