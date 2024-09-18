using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePopup : MonoBehaviour
{
    private PlayerStats playerStats;
    private ItemManager itemManager;

    public NotEnoughMoneyPopup notEnoughMoneyPopup;

    public string id = "";
    public int lv = 0;
    public int price = 0;
    public UILabel priceText;
    public GrillUpgrade grillUpgrade = null;
    public PlateUpgrade plateUpgrade = null;
    public MainFoodUpgrade mainFoodUpgrade = null;
    public SideFoodUpgrade sideFoodUpgrade = null;

    public AudioSource coinFX;

    public GameObject blackBG;

    [System.Serializable]
    public class Upgrade
    {
        public GameObject upgradeImg;
    }

    [System.Serializable]
    public class UpgradeInfo
    {
        public string name = "";
        public string id = "";
        public Upgrade[] upgrade;
    }

    public UpgradeInfo[] mainInfo;
    public UpgradeInfo[] sideInfo;
    public UpgradeInfo[] grillInfo;
    public UpgradeInfo[] plateInfo;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        itemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
    }

    private void Update()
    {
        priceText.text = price.ToString();

        _ActiveBlackBG();
    }

    void _ActiveBlackBG()
    {
        if(gameObject.activeSelf == true && blackBG.activeSelf == false)
        {
            blackBG.SetActive(true);
        }
    }

    void _DeactiveUpgradeImage()
    {
        for (int i = 0; i < grillInfo.Length; i++)
        {
            for(int j=0; j < grillInfo[i].upgrade.Length; j++)
            {
                grillInfo[i].upgrade[j].upgradeImg.SetActive(false);
            }
        }

        for (int i = 0; i < plateInfo.Length; i++)
        {
            for (int j = 0; j < plateInfo[i].upgrade.Length; j++)
            {
                plateInfo[i].upgrade[j].upgradeImg.SetActive(false);
            }
        }

        for (int i = 0; i < mainInfo.Length; i++)
        {
            for (int j = 0; j < mainInfo[i].upgrade.Length; j++)
            {
                mainInfo[i].upgrade[j].upgradeImg.SetActive(false);
            }
        }

        for (int i = 0; i < sideInfo.Length; i++)
        {
            for (int j = 0; j < sideInfo[i].upgrade.Length; j++)
            {
                sideInfo[i].upgrade[j].upgradeImg.SetActive(false);
            }
        }
    }

    public void _SetUpgradeImage()
    {
        _DeactiveUpgradeImage();

        if (grillUpgrade != null)
        {
            for (int i = 0; i < grillInfo.Length; i++)
            {
                if (grillInfo[i].id == id)
                {
                    grillInfo[i].upgrade[lv - 1].upgradeImg.SetActive(true);

                    break;
                }
            }
        }
        else
        if (plateUpgrade != null)
        {
            for (int i = 0; i < plateInfo.Length; i++)
            {
                if (plateInfo[i].id == id)
                {
                    plateInfo[i].upgrade[lv - 1].upgradeImg.SetActive(true);

                    break;
                }
            }
        }
        else
        if (mainFoodUpgrade != null)
        {
            for (int i = 0; i < mainInfo.Length; i++)
            {
                if (mainInfo[i].id == id)
                {
                    mainInfo[i].upgrade[lv - 1].upgradeImg.SetActive(true);

                    break;
                }
            }
        }
        else
        if (sideFoodUpgrade != null)
        {
            for (int i = 0; i < sideInfo.Length; i++)
            {
                if (sideInfo[i].id == id)
                {
                    sideInfo[i].upgrade[lv - 1].upgradeImg.SetActive(true);

                    break;
                }
            }
        }
    }

    public void _SetNull()
    {
        grillUpgrade = null;
        plateUpgrade = null;
        mainFoodUpgrade = null;
        sideFoodUpgrade = null;
    }

    public void _Upgrade()
    {
        if(playerStats.coin >= price || playerStats.freeTurn >0)
        {
            playerStats.freeTurn = 0;

            if (grillUpgrade != null)
            {
                for(int i=0; i<itemManager.grillsDatas.Length; i++)
                {
                    if(itemManager.grillsDatas[i].itemId == id)
                    {
                        itemManager.grillsDatas[i].currentLevel += 1;
                        itemManager.save = true;

                        _Pay();
                        grillUpgrade._Active();
                        _Close();

                        return;
                    }
                }
            }
            else
            if (plateUpgrade != null)
            {
                for (int i = 0; i < itemManager.platesDatas.Length; i++)
                {
                    if (itemManager.platesDatas[i].itemId == id)
                    {
                        itemManager.platesDatas[i].currentLevel += 1;
                        itemManager.save = true;

                        _Pay();
                        plateUpgrade._Active();
                        _Close();

                        return;
                    }
                }
            }
            else
            if (mainFoodUpgrade != null)
            {
                for (int i = 0; i < itemManager.mainFoodsDatas.Length; i++)
                {
                    if (itemManager.mainFoodsDatas[i].itemId == id)
                    {
                        itemManager.mainFoodsDatas[i].currentLevel += 1;
                        itemManager.save = true;

                        _Pay();
                        mainFoodUpgrade._Active();
                        _Close();

                        return;
                    }
                }
            }
            else
            if (sideFoodUpgrade != null)
            {
                for (int i = 0; i < itemManager.sideFoodsDatas.Length; i++)
                {
                    if (itemManager.sideFoodsDatas[i].itemId == id)
                    {
                        itemManager.sideFoodsDatas[i].currentLevel += 1;
                        itemManager.save = true;

                        _Pay();
                        sideFoodUpgrade._Active();
                        _Close();

                        return;
                    }
                }
            }
        }

        else if(playerStats.coin < price)
        {
            notEnoughMoneyPopup.gameObject.SetActive(true);
            notEnoughMoneyPopup.id = id;
            notEnoughMoneyPopup.coinPrice = price;
            notEnoughMoneyPopup.grillUpgrade = grillUpgrade;
            notEnoughMoneyPopup.plateUpgrade = plateUpgrade;
            notEnoughMoneyPopup.mainFoodUpgrade = mainFoodUpgrade;
            notEnoughMoneyPopup.sideFoodUpgrade = sideFoodUpgrade;

            _Close();
        }
    }

    void _Pay()
    {
        playerStats.DecreaseCoin(price);
        coinFX.Play();
    }

    public void _Close()
    {
        blackBG.SetActive(false);

        id = "";
        lv = 0;
        price = 0;
        _SetNull();

        gameObject.SetActive(false);
    }
}
