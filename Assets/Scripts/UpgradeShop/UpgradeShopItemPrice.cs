using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class UpgradeShopItemPrice : MonoBehaviour
{
    private ItemManager itemManager;

    private string url = "https://api.efoxstudio.com/api/remoteconfig?name=upgradeConfig";

    public MainFoodUpgrade[] mainFoodUpgrades;
    public SideFoodUpgrade[] sideFoodUpgrades;
    public GrillUpgrade[] grillUpgrades;
    public PlateUpgrade[] plateUpgrades;

    // Start is called before the first frame update
    void Start()
    {
        itemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        //IronSource.Agent.hideBanner();

        _SetToItemManager();
        _LoadLocalRemotePrice();
        StartCoroutine(_LoadRemotePrice());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void _SetToItemManager()
    {
        if(itemManager.itemPriceRemotes.Count < 1)
        {
            for (int i = 0; i < mainFoodUpgrades.Length; i++)
            {
                ItemPriceRemote itemPriceRemote = new ItemPriceRemote();

                itemPriceRemote.iTEM_TYPE = ItemManager.ITEM_TYPE.mainFood.ToString();
                itemPriceRemote.itemId = mainFoodUpgrades[i].id;
                for (int k = 0; k < mainFoodUpgrades[i].upgrades.Length; k++)
                {
                    itemPriceRemote.upgradePrice.Add(mainFoodUpgrades[i].upgrades[k].upgradeCoin);
                }

                itemManager.itemPriceRemotes.Add(itemPriceRemote);
            }

            for (int i = 0; i < sideFoodUpgrades.Length; i++)
            {
                ItemPriceRemote itemPriceRemote = new ItemPriceRemote();

                itemPriceRemote.iTEM_TYPE = ItemManager.ITEM_TYPE.sideFood.ToString();
                itemPriceRemote.itemId = sideFoodUpgrades[i].id;
                for (int k = 0; k < sideFoodUpgrades[i].upgrades.Length; k++)
                {
                    itemPriceRemote.upgradePrice.Add(sideFoodUpgrades[i].upgrades[k].upgradeCoin);
                }

                itemManager.itemPriceRemotes.Add(itemPriceRemote);
            }

            for (int i = 0; i < grillUpgrades.Length; i++)
            {
                ItemPriceRemote itemPriceRemote = new ItemPriceRemote();

                itemPriceRemote.iTEM_TYPE = ItemManager.ITEM_TYPE.grills.ToString();
                itemPriceRemote.itemId = grillUpgrades[i].id;
                for (int k = 0; k < grillUpgrades[i].upgrades.Length; k++)
                {
                    itemPriceRemote.upgradePrice.Add(grillUpgrades[i].upgrades[k].upgradeCoin);
                }

                itemManager.itemPriceRemotes.Add(itemPriceRemote);
            }

            for (int i = 0; i < plateUpgrades.Length; i++)
            {
                ItemPriceRemote itemPriceRemote = new ItemPriceRemote();

                itemPriceRemote.iTEM_TYPE = ItemManager.ITEM_TYPE.plates.ToString();
                itemPriceRemote.itemId = plateUpgrades[i].id;
                for (int k = 0; k < plateUpgrades[i].upgrades.Length; k++)
                {
                    itemPriceRemote.upgradePrice.Add(plateUpgrades[i].upgrades[k].upgradeCoin);
                }

                itemManager.itemPriceRemotes.Add(itemPriceRemote);
            }
        }

        itemManager.save = true;

    }

    void _LoadLocalRemotePrice()
    {

        for (int i = 0; i < itemManager.itemPriceRemotes.Count; i++)
        {
            if(itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.mainFood.ToString())
            {
                for (int j = 0; j < mainFoodUpgrades.Length; j++)
                {
                    if (mainFoodUpgrades[j].id == itemManager.itemPriceRemotes[i].itemId)
                    {
                        for(int k=0; k< mainFoodUpgrades[j].upgrades.Length; k++)
                        {
                            mainFoodUpgrades[j].upgrades[k].upgradeCoin = itemManager.itemPriceRemotes[i].upgradePrice[k];
                        }
                    }
                }
            }

            if (itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.sideFood.ToString())
            {
                for (int j = 0; j < sideFoodUpgrades.Length; j++)
                {
                    if (sideFoodUpgrades[j].id == itemManager.itemPriceRemotes[i].itemId)
                    {
                        for (int k = 0; k < sideFoodUpgrades[j].upgrades.Length; k++)
                        {
                            sideFoodUpgrades[j].upgrades[k].upgradeCoin = itemManager.itemPriceRemotes[i].upgradePrice[k];
                        }
                    }
                }
            }

            if (itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.grills.ToString())
            {
                for (int j = 0; j < grillUpgrades.Length; j++)
                {
                    if (grillUpgrades[j].id == itemManager.itemPriceRemotes[i].itemId)
                    {
                        for (int k = 0; k < grillUpgrades[j].upgrades.Length; k++)
                        {
                            grillUpgrades[j].upgrades[k].upgradeCoin = itemManager.itemPriceRemotes[i].upgradePrice[k];
                        }
                    }
                }
            }

            if (itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.plates.ToString())
            {
                for (int j = 0; j < plateUpgrades.Length; j++)
                {
                    if (plateUpgrades[j].id == itemManager.itemPriceRemotes[i].itemId)
                    {
                        for (int k = 0; k < plateUpgrades[j].upgrades.Length; k++)
                        {
                            plateUpgrades[j].upgrades[k].upgradeCoin = itemManager.itemPriceRemotes[i].upgradePrice[k];
                        }
                    }
                }
            }
        }
    }

    IEnumerator _LoadRemotePrice()
    {

        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        webRequest.SetRequestHeader("appPassword", "efoxstudio2019");
        webRequest.method = UnityWebRequest.kHttpVerbGET;

        //   webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log("www.error - " + webRequest.error);
        }
        else
        {

            JSONObject json = new JSONObject(webRequest.downloadHandler.text);

            for(int i=0; i< itemManager.itemPriceRemotes.Count; i++)
            {
                if(itemManager.itemPriceRemotes[i].itemId == 1.ToString() 
                    && itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.mainFood.ToString())
                {
                    itemManager.itemPriceRemotes[i].upgradePrice[0] = Convert.ToInt32(json["MeatUpgrade1"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[1] = Convert.ToInt32(json["MeatUpgrade2"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[2] = Convert.ToInt32(json["MeatUpgrade3"].ToString().Replace("\"", ""));
                }
                if (itemManager.itemPriceRemotes[i].itemId == 2.ToString()
                    && itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.mainFood.ToString())
                {
                    itemManager.itemPriceRemotes[i].upgradePrice[0] = Convert.ToInt32(json["VegeUpgrade1"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[1] = Convert.ToInt32(json["VegeUpgrade2"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[2] = Convert.ToInt32(json["VegeUpgrade3"].ToString().Replace("\"", ""));
                }

                if (itemManager.itemPriceRemotes[i].itemId == 3.ToString()
                    && itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.mainFood.ToString())
                {
                    itemManager.itemPriceRemotes[i].upgradePrice[0] = Convert.ToInt32(json["Jar2Upgrade1"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[1] = Convert.ToInt32(json["Jar2Upgrade2"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[2] = Convert.ToInt32(json["Jar2Upgrade3"].ToString().Replace("\"", ""));
                }

                if (itemManager.itemPriceRemotes[i].itemId == 4.ToString()
                    && itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.mainFood.ToString())
                {
                    itemManager.itemPriceRemotes[i].upgradePrice[0] = Convert.ToInt32(json["Jar1Upgrade1"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[1] = Convert.ToInt32(json["Jar1Upgrade2"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[2] = Convert.ToInt32(json["Jar1Upgrade3"].ToString().Replace("\"", ""));
                }

                if (itemManager.itemPriceRemotes[i].itemId == 5.ToString()
                    && itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.sideFood.ToString())
                {
                    itemManager.itemPriceRemotes[i].upgradePrice[1] = Convert.ToInt32(json["PotUpgrade1"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[2] = Convert.ToInt32(json["PotUpgrade2"].ToString().Replace("\"", ""));
                }

                if (itemManager.itemPriceRemotes[i].itemId == 1.ToString()
                    && itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.grills.ToString())
                {
                    itemManager.itemPriceRemotes[i].upgradePrice[1] = Convert.ToInt32(json["GrillUpgrade1"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[2] = Convert.ToInt32(json["GrillUpgrade2"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[3] = Convert.ToInt32(json["GrillUpgrade3"].ToString().Replace("\"", ""));
                }

                if (itemManager.itemPriceRemotes[i].itemId == 1.ToString()
                    && itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.plates.ToString())
                {
                    itemManager.itemPriceRemotes[i].upgradePrice[1] = Convert.ToInt32(json["PlateUpgrade1"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[2] = Convert.ToInt32(json["PlateUpgrade2"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[3] = Convert.ToInt32(json["PlateUpgrade3"].ToString().Replace("\"", ""));
                }

            }

            //Sdet to upgrades
            for (int i = 0; i < mainFoodUpgrades.Length; i++)
            {
                if (mainFoodUpgrades[i].id == 1.ToString())
                {
                    mainFoodUpgrades[i].upgrades[0].upgradeCoin = Convert.ToInt32(json["MeatUpgrade1"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[1].upgradeCoin = Convert.ToInt32(json["MeatUpgrade2"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[2].upgradeCoin = Convert.ToInt32(json["MeatUpgrade3"].ToString().Replace("\"", ""));

                    mainFoodUpgrades[i]._Active();
                }

                if (mainFoodUpgrades[i].id == 2.ToString())
                {
                    mainFoodUpgrades[i].upgrades[0].upgradeCoin = Convert.ToInt32(json["VegeUpgrade1"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[1].upgradeCoin = Convert.ToInt32(json["VegeUpgrade2"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[2].upgradeCoin = Convert.ToInt32(json["VegeUpgrade3"].ToString().Replace("\"", ""));

                    mainFoodUpgrades[i]._Active();
                }

                if (mainFoodUpgrades[i].id == 3.ToString())
                {
                    mainFoodUpgrades[i].upgrades[0].upgradeCoin = Convert.ToInt32(json["Jar2Upgrade1"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[1].upgradeCoin = Convert.ToInt32(json["Jar2Upgrade2"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[2].upgradeCoin = Convert.ToInt32(json["Jar2Upgrade3"].ToString().Replace("\"", ""));

                    mainFoodUpgrades[i]._Active();
                }

                if (mainFoodUpgrades[i].id == 4.ToString())
                {
                    mainFoodUpgrades[i].upgrades[0].upgradeCoin = Convert.ToInt32(json["Jar1Upgrade1"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[1].upgradeCoin = Convert.ToInt32(json["Jar1Upgrade2"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[2].upgradeCoin = Convert.ToInt32(json["Jar1Upgrade3"].ToString().Replace("\"", ""));

                    mainFoodUpgrades[i]._Active();
                }
            }

            for (int i = 0; i < sideFoodUpgrades.Length; i++)
            {
                if (sideFoodUpgrades[i].id == 5.ToString())
                {
                    sideFoodUpgrades[i].upgrades[1].upgradeCoin = Convert.ToInt32(json["PotUpgrade1"].ToString().Replace("\"", ""));
                    sideFoodUpgrades[i].upgrades[2].upgradeCoin = Convert.ToInt32(json["PotUpgrade2"].ToString().Replace("\"", ""));

                    sideFoodUpgrades[i]._Active();
                }
            }

            for (int i = 0; i < grillUpgrades.Length; i++)
            {
                if (grillUpgrades[i].id == 1.ToString())
                {
                    grillUpgrades[i].upgrades[1].upgradeCoin = Convert.ToInt32(json["GrillUpgrade1"].ToString().Replace("\"", ""));
                    grillUpgrades[i].upgrades[2].upgradeCoin = Convert.ToInt32(json["GrillUpgrade2"].ToString().Replace("\"", ""));
                    grillUpgrades[i].upgrades[3].upgradeCoin = Convert.ToInt32(json["GrillUpgrade3"].ToString().Replace("\"", ""));

                    grillUpgrades[i]._Active();
                }
            }

            for (int i = 0; i < plateUpgrades.Length; i++)
            {
                if (grillUpgrades[i].id == 1.ToString())
                {
                    plateUpgrades[i].upgrades[1].upgradeCoin = Convert.ToInt32(json["PlateUpgrade1"].ToString().Replace("\"", ""));
                    plateUpgrades[i].upgrades[2].upgradeCoin = Convert.ToInt32(json["PlateUpgrade2"].ToString().Replace("\"", ""));
                    plateUpgrades[i].upgrades[3].upgradeCoin = Convert.ToInt32(json["PlateUpgrade3"].ToString().Replace("\"", ""));

                    plateUpgrades[i]._Active();
                }
            }

        }

        itemManager.save = true;

        yield break;

    }
}