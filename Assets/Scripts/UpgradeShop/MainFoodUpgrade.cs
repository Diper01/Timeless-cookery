using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class MainFoodUpgrade : MonoBehaviour
{
    private LevelManager levelManager;
    private ItemManager itemManager;
    private PlayerStats playerStats;
    //private AdManager adManager;

    public GameObject upgradePopup;
    public GameObject lockPopup;
    public GameObject maxSpine;
    public GameObject watchAdBttn;

    public string id = "";
    public int unlockLevel = 0;
    public int maxLevel = 4;
    public int currentLevel = 1;
    public int currentCoin = 0;
    public bool watchAdClicked = false;

    public GameObject lockBox;
    public GameObject priceImg;
    public UILabel priceText;

    [System.Serializable]
    public class Upgrade
    {
        public GameObject upgradeImg;
        public int foodPrice = 0;
        public int upgradeCoin = 0;
    }

    public Upgrade[] upgrades;

    private void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        itemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        //adManager = FindObjectOfType<AdManager>();

        _SetItemManager();
        _Deactive();
        _Active();
    }

    private void Update()
    {
        _WatchAdUpgrade();
    }

    void _Deactive()
    {
        watchAdBttn.SetActive(false);
        priceImg.SetActive(false);
        maxSpine.SetActive(false);
        for (int i = 0; i < upgrades.Length; i++)
        {
            if(upgrades[i].upgradeImg.activeSelf == true)
            {
                upgrades[i].upgradeImg.SetActive(false);
            }
        }
    }

    public void _Active()
    {
        for (int i = 0; i < itemManager.mainFoodsDatas.Length; i++)
        {
            if (itemManager.mainFoodsDatas[i].itemId == id)
            {
                currentLevel = itemManager.mainFoodsDatas[i].currentLevel;

                if (currentLevel > 0)
                {
                    lockBox.SetActive(false);

                    if (currentLevel == maxLevel)
                    {
                        _Deactive();
                        upgrades[upgrades.Length - 1].upgradeImg.SetActive(true);
                        priceImg.SetActive(false);

                        if(maxSpine.activeSelf == false)
                        {
                            maxSpine.SetActive(true);
                        }
                    }

                    else
                    {
                        for (int j = 0; j < upgrades.Length; j++)
                        {
                            if (j == itemManager.mainFoodsDatas[i].currentLevel - 1)
                            {
                                _Deactive();

                                currentCoin = upgrades[j+1].upgradeCoin;
                                upgrades[j].upgradeImg.SetActive(true);

                                break;
                            }
                        }

                        //if(currentCoin <= 0)
                        //{
                        //    watchAdBttn.SetActive(true);
                        //}
                        //else
                        //{
                            priceText.text = currentCoin.ToString();
                            priceImg.SetActive(true);
                        //}
                    }
                }

                break;
            }
        }
    }

    void _SetItemManager()
    {
        int inList = 0;

        for (int i = 0; i < itemManager.mainFoodsDatas.Length; i++)
        {
            if (itemManager.mainFoodsDatas[i].itemId == id)
            {
                inList++;
            }
        }

        if(inList == 0)
        {
            for (int i = 0; i < itemManager.mainFoodsDatas.Length; i++)
            {
                if(itemManager.mainFoodsDatas[i].itemId == "")
                {
                    itemManager.mainFoodsDatas[i].itemId = id;
                    itemManager.mainFoodsDatas[i].currentLevel = currentLevel;

                    return;
                }
            }
        }
        else
        {
            return;
        }
    }

    public void _WatchAdUpgrade()
    {
        //if (adManager.rewarded == true && watchAdClicked == true)
        //{
        //    adManager.rewarded = false;
        //    watchAdClicked = false;

        //    playerStats.freeTurn = 0;

        //    for (int i = 0; i < itemManager.mainFoodsDatas.Length; i++)
        //    {
        //        if (itemManager.mainFoodsDatas[i].itemId == id)
        //        {
        //            itemManager.mainFoodsDatas[i].currentLevel += 1;
        //            itemManager.save = true;

        //            _Active();

        //            return;
        //        }
        //    }
        //}
    }

    public void _Buy()
    {
        //if (currentCoin <= 0 && IronSource.Agent.isRewardedVideoAvailable() && watchAdClicked == false)
        //{
        //    watchAdClicked = true;
        //    adManager.rewardType = AdManager.REWARD_TYPE.freeTurn;
        //    adManager.ShowRewardAd(AdManager.REWARD_AD_PLACEMENT.FREE_UPGRADE);
        //}
        //else
        if (currentCoin > 0)
        {
            if (currentLevel > 0 && currentLevel < maxLevel)
            {
                upgradePopup.GetComponent<UpgradePopup>().mainFoodUpgrade = gameObject.GetComponent<MainFoodUpgrade>();

                upgradePopup.GetComponent<UpgradePopup>().id = id;
                upgradePopup.GetComponent<UpgradePopup>().lv = currentLevel;
                upgradePopup.GetComponent<UpgradePopup>().price = currentCoin;

                upgradePopup.GetComponent<UpgradePopup>()._SetUpgradeImage();

                upgradePopup.SetActive(true);
            }
            if (currentLevel == 0)
            {
                lockPopup.GetComponent<UpgradeLockPopup>().level = unlockLevel;

                lockPopup.SetActive(true);
            }
        }
    }
}
