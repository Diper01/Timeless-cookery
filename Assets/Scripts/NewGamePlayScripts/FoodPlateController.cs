using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPlateController : MonoBehaviour
{
    private ItemManager itemManager;

    [System.Serializable]
    public class PlateList
    {
        public string id = "";
        public int level = 0;
        public int unlockLevel = 0;
        public FoodPlate[] foodPlates;
    }

    public PlateList[] plateList;

    private void Start()
    {
        itemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();

        _GetPlateLevels();
        _UnlockPlate();

        _SetItemManager();
    }

    private void Update()
    {

    }

    void _GetPlateLevels()
    {
        for (int i = 0; i < itemManager.platesDatas.Length; i++)
        {
            for (int j = 0; j < plateList.Length; j++)
            {
                if (itemManager.platesDatas[i].itemId == plateList[j].id)
                {
                    plateList[j].level = itemManager.platesDatas[i].currentLevel;
                }
            }
        }
    }

    void _UnlockPlate()
    {
        for (int i = 0; i < plateList.Length; i++)
        {
            for (int j = 0; j < plateList[i].level; j++)
            {
                plateList[i].foodPlates[j].isUnlocked = true;
            }
        }
    }

    void _SetItemManager()
    {
        for (int i = 0; i < plateList.Length; i++)
        {
            for (int j = 0; j < itemManager.platesDatas.Length; j++)
            {
                if (plateList[i].id == itemManager.platesDatas[j].itemId)
                {
                    break;
                }

                if (itemManager.platesDatas[j].itemId == "")
                {
                    itemManager.platesDatas[j].itemId = plateList[i].id;
                    itemManager.platesDatas[j].currentLevel = plateList[i].level;
                    itemManager.platesDatas[j].unlockLevel = plateList[i].unlockLevel;

                    break;
                }
            }
        }
    }
}
