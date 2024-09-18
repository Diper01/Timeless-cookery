using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class UpgradeShopItemPrice_World2 : MonoBehaviour
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
        if (itemManager.itemPriceRemotes.Count < 1)
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
            if (itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.mainFood.ToString())
            {
                for (int j = 0; j < mainFoodUpgrades.Length; j++)
                {
                    if (mainFoodUpgrades[j].id == itemManager.itemPriceRemotes[i].itemId)
                    {
                        for (int k = 0; k < mainFoodUpgrades[j].upgrades.Length; k++)
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

            for (int i = 0; i < itemManager.itemPriceRemotes.Count; i++)
            {
                // chicken 
                if (itemManager.itemPriceRemotes[i].itemId == 1.ToString()
                    && itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.mainFood.ToString())
                {
                    itemManager.itemPriceRemotes[i].upgradePrice[0] = Convert.ToInt32(json["W2_ChickenUpgrade1"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[1] = Convert.ToInt32(json["W2_ChickenUpgrade2"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[2] = Convert.ToInt32(json["W2_ChickenUpgrade3"].ToString().Replace("\"", ""));
                }

                // Potato
                if (itemManager.itemPriceRemotes[i].itemId == 2.ToString()
                    && itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.mainFood.ToString())
                {
                    itemManager.itemPriceRemotes[i].upgradePrice[0] = Convert.ToInt32(json["W2_PotatosUpgrade1"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[1] = Convert.ToInt32(json["W2_PotatosUpgrade2"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[2] = Convert.ToInt32(json["W2_PotatosUpgrade3"].ToString().Replace("\"", ""));
                }

                // Bean (pea)
                if (itemManager.itemPriceRemotes[i].itemId == 3.ToString()
                    && itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.mainFood.ToString())
                {
                    itemManager.itemPriceRemotes[i].upgradePrice[0] = Convert.ToInt32(json["W2_PeaUpgrade1"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[1] = Convert.ToInt32(json["W2_PeaUpgrade2"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[2] = Convert.ToInt32(json["W2_PeaUpgrade3"].ToString().Replace("\"", ""));
                }

                //Mushroom
                if (itemManager.itemPriceRemotes[i].itemId == 4.ToString()
                    && itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.mainFood.ToString())
                {
                    itemManager.itemPriceRemotes[i].upgradePrice[0] = Convert.ToInt32(json["W2_MushroomUpgrade1"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[1] = Convert.ToInt32(json["W2_MushroomUpgrade2"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[2] = Convert.ToInt32(json["W2_MushroomUpgrade3"].ToString().Replace("\"", ""));
                }

                //Violet Sauce (purple)
                if (itemManager.itemPriceRemotes[i].itemId == 5.ToString()
                    && itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.mainFood.ToString())
                {
                    itemManager.itemPriceRemotes[i].upgradePrice[0] = Convert.ToInt32(json["W2_PurpleSauceUpgrade1"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[1] = Convert.ToInt32(json["W2_PurpleSauceUpgrade2"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[2] = Convert.ToInt32(json["W2_PurpleSauceUpgrade3"].ToString().Replace("\"", ""));
                }

                //Green Sauce 
                if (itemManager.itemPriceRemotes[i].itemId == 6.ToString()
                    && itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.mainFood.ToString())
                {
                    itemManager.itemPriceRemotes[i].upgradePrice[0] = Convert.ToInt32(json["W2_PurpleSauceUpgrade1"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[1] = Convert.ToInt32(json["W2_GreenSauceUpgrade2"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[2] = Convert.ToInt32(json["W2_GreenSauceUpgrade3"].ToString().Replace("\"", ""));
                }

                //Red Sauce 
                if (itemManager.itemPriceRemotes[i].itemId == 7.ToString()
                    && itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.mainFood.ToString())
                {
                    itemManager.itemPriceRemotes[i].upgradePrice[0] = Convert.ToInt32(json["W2_RedSauceUpgrade1"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[1] = Convert.ToInt32(json["W2_RedSauceUpgrade2"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[2] = Convert.ToInt32(json["W2_RedSauceUpgrade3"].ToString().Replace("\"", ""));
                }

                // Wine (Beer)
                if (itemManager.itemPriceRemotes[i].itemId == 8.ToString()
                    && itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.sideFood.ToString())
                {
                    itemManager.itemPriceRemotes[i].upgradePrice[0] = Convert.ToInt32(json["W2_BeerUpgrade1"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[1] = Convert.ToInt32(json["W2_BeerUpgrade2"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[2] = Convert.ToInt32(json["W2_BeerUpgrade3"].ToString().Replace("\"", ""));
                }

                //ChickenGrill
                if (itemManager.itemPriceRemotes[i].itemId == 9.ToString()
                    && itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.grills.ToString())
                {
                    itemManager.itemPriceRemotes[i].upgradePrice[0] = Convert.ToInt32(json["W2_GrillUpgrade1"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[1] = Convert.ToInt32(json["W2_GrillUpgrade2"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[2] = Convert.ToInt32(json["W2_GrillUpgrade3"].ToString().Replace("\"", ""));
                }

                //ChickenGrill
                if (itemManager.itemPriceRemotes[i].itemId == "a"
                    && itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.grills.ToString())
                {
                    itemManager.itemPriceRemotes[i].upgradePrice[0] = Convert.ToInt32(json["W2_PanGrillUpgrade1"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[1] = Convert.ToInt32(json["W2_PanGrillUpgrade2"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[2] = Convert.ToInt32(json["W2_PanGrillUpgrade3"].ToString().Replace("\"", ""));
                }

                //Chicken Plate
                if (itemManager.itemPriceRemotes[i].itemId == "b"
                    && itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.plates.ToString())
                {
                    itemManager.itemPriceRemotes[i].upgradePrice[0] = Convert.ToInt32(json["W2_DishChickenUpgrade1"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[1] = Convert.ToInt32(json["W2_DishChickenUpgrade2"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[2] = Convert.ToInt32(json["W2_DishChickenUpgrade3"].ToString().Replace("\"", ""));
                }

                //Chicken Plate
                if (itemManager.itemPriceRemotes[i].itemId == "c"
                    && itemManager.itemPriceRemotes[i].iTEM_TYPE == ItemManager.ITEM_TYPE.plates.ToString())
                {
                    itemManager.itemPriceRemotes[i].upgradePrice[0] = Convert.ToInt32(json["W2_DishMushroomUpgrade1"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[1] = Convert.ToInt32(json["W2_DishMushroomUpgrade2"].ToString().Replace("\"", ""));
                    itemManager.itemPriceRemotes[i].upgradePrice[2] = Convert.ToInt32(json["W2_DishMushroomUpgrade3"].ToString().Replace("\"", ""));
                }

            }

            //Sdet to upgrades
            for (int i = 0; i < mainFoodUpgrades.Length; i++)
            {
                if (mainFoodUpgrades[i].id == 1.ToString())
                {
                    mainFoodUpgrades[i].upgrades[0].upgradeCoin = Convert.ToInt32(json["W2_ChickenUpgrade1"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[1].upgradeCoin = Convert.ToInt32(json["W2_ChickenUpgrade2"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[2].upgradeCoin = Convert.ToInt32(json["W2_ChickenUpgrade3"].ToString().Replace("\"", ""));

                    mainFoodUpgrades[i]._Active();
                }

                if (mainFoodUpgrades[i].id == 2.ToString())
                {
                    mainFoodUpgrades[i].upgrades[0].upgradeCoin = Convert.ToInt32(json["W2_PotatosUpgrade1"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[1].upgradeCoin = Convert.ToInt32(json["W2_PotatosUpgrade2"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[2].upgradeCoin = Convert.ToInt32(json["W2_PotatosUpgrade3"].ToString().Replace("\"", ""));

                    mainFoodUpgrades[i]._Active();
                }

                if (mainFoodUpgrades[i].id == 3.ToString())
                {
                    mainFoodUpgrades[i].upgrades[0].upgradeCoin = Convert.ToInt32(json["W2_PeaUpgrade1"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[1].upgradeCoin = Convert.ToInt32(json["W2_PeaUpgrade2"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[2].upgradeCoin = Convert.ToInt32(json["W2_PeaUpgrade3"].ToString().Replace("\"", ""));

                    mainFoodUpgrades[i]._Active();
                }

                if (mainFoodUpgrades[i].id == 4.ToString())
                {
                    mainFoodUpgrades[i].upgrades[0].upgradeCoin = Convert.ToInt32(json["W2_MushroomUpgrade1"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[1].upgradeCoin = Convert.ToInt32(json["W2_MushroomUpgrade2"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[2].upgradeCoin = Convert.ToInt32(json["W2_MushroomUpgrade3"].ToString().Replace("\"", ""));

                    mainFoodUpgrades[i]._Active();
                }

                if (mainFoodUpgrades[i].id == 5.ToString())
                {
                    mainFoodUpgrades[i].upgrades[0].upgradeCoin = Convert.ToInt32(json["W2_PurpleSauceUpgrade1"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[1].upgradeCoin = Convert.ToInt32(json["W2_PurpleSauceUpgrade2"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[2].upgradeCoin = Convert.ToInt32(json["W2_PurpleSauceUpgrade3"].ToString().Replace("\"", ""));

                    mainFoodUpgrades[i]._Active();
                }

                if (mainFoodUpgrades[i].id == 6.ToString())
                {
                    mainFoodUpgrades[i].upgrades[0].upgradeCoin = Convert.ToInt32(json["W2_PurpleSauceUpgrade1"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[1].upgradeCoin = Convert.ToInt32(json["W2_GreenSauceUpgrade2"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[2].upgradeCoin = Convert.ToInt32(json["W2_GreenSauceUpgrade3"].ToString().Replace("\"", ""));

                    mainFoodUpgrades[i]._Active();
                }

                if (mainFoodUpgrades[i].id == 7.ToString())
                {
                    mainFoodUpgrades[i].upgrades[0].upgradeCoin = Convert.ToInt32(json["W2_RedSauceUpgrade1"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[1].upgradeCoin = Convert.ToInt32(json["W2_RedSauceUpgrade2"].ToString().Replace("\"", ""));
                    mainFoodUpgrades[i].upgrades[2].upgradeCoin = Convert.ToInt32(json["W2_RedSauceUpgrade3"].ToString().Replace("\"", ""));

                    mainFoodUpgrades[i]._Active();
                }
            }

            for (int i = 0; i < sideFoodUpgrades.Length; i++)
            {
                if (sideFoodUpgrades[i].id == 8.ToString())
                {
                    sideFoodUpgrades[i].upgrades[0].upgradeCoin = Convert.ToInt32(json["W2_BeerUpgrade1"].ToString().Replace("\"", ""));
                    sideFoodUpgrades[i].upgrades[1].upgradeCoin = Convert.ToInt32(json["W2_BeerUpgrade2"].ToString().Replace("\"", ""));
                    sideFoodUpgrades[i].upgrades[2].upgradeCoin = Convert.ToInt32(json["W2_BeerUpgrade3"].ToString().Replace("\"", ""));

                    sideFoodUpgrades[i]._Active();
                }
            }

            for (int i = 0; i < grillUpgrades.Length; i++)
            {
                if (grillUpgrades[i].id == 9.ToString())
                {
                    grillUpgrades[i].upgrades[0].upgradeCoin = Convert.ToInt32(json["W2_GrillUpgrade1"].ToString().Replace("\"", ""));
                    grillUpgrades[i].upgrades[1].upgradeCoin = Convert.ToInt32(json["W2_GrillUpgrade2"].ToString().Replace("\"", ""));
                    grillUpgrades[i].upgrades[2].upgradeCoin = Convert.ToInt32(json["W2_GrillUpgrade3"].ToString().Replace("\"", ""));

                    grillUpgrades[i]._Active();
                }

                if (grillUpgrades[i].id == "a")
                {
                    grillUpgrades[i].upgrades[0].upgradeCoin = Convert.ToInt32(json["W2_PanGrillUpgrade1"].ToString().Replace("\"", ""));
                    grillUpgrades[i].upgrades[1].upgradeCoin = Convert.ToInt32(json["W2_PanGrillUpgrade2"].ToString().Replace("\"", ""));
                    grillUpgrades[i].upgrades[2].upgradeCoin = Convert.ToInt32(json["W2_PanGrillUpgrade3"].ToString().Replace("\"", ""));

                    grillUpgrades[i]._Active();
                }
            }

            for (int i = 0; i < plateUpgrades.Length; i++)
            {
                if (grillUpgrades[i].id == "b")
                {
                    plateUpgrades[i].upgrades[1].upgradeCoin = Convert.ToInt32(json["W2_DishChickenUpgrade1"].ToString().Replace("\"", ""));
                    plateUpgrades[i].upgrades[2].upgradeCoin = Convert.ToInt32(json["W2_DishChickenUpgrade2"].ToString().Replace("\"", ""));
                    plateUpgrades[i].upgrades[3].upgradeCoin = Convert.ToInt32(json["W2_DishChickenUpgrade3"].ToString().Replace("\"", ""));

                    plateUpgrades[i]._Active();
                }

                if (grillUpgrades[i].id == "c")
                {
                    plateUpgrades[i].upgrades[1].upgradeCoin = Convert.ToInt32(json["W2_DishMushroomUpgrade1"].ToString().Replace("\"", ""));
                    plateUpgrades[i].upgrades[2].upgradeCoin = Convert.ToInt32(json["W2_DishMushroomUpgrade2"].ToString().Replace("\"", ""));
                    plateUpgrades[i].upgrades[3].upgradeCoin = Convert.ToInt32(json["W2_DishMushroomUpgrade3"].ToString().Replace("\"", ""));

                    plateUpgrades[i]._Active();
                }
            }

        }

        _SetToItemManager();

        itemManager.save = true;

        yield break;

    }
}
