using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    private MissionManager missionManager;
    private ItemManager itemManager;

    [System.Serializable]
    public class MainFoodSpawners
    {
        public string id = "";
        public int level = 0;
        public int unlockLevel = 0;
        public FoodController[] foodSpawner;
    }

    public MainFoodSpawners[] mainFoodSpawners;

    private void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        itemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();

        _GetLevels();
        _SetUnlockItem();
        _ActiveFoodController();

        _SetItemManager();
    }

    private void Update()
    {
        
    }

    void _GetLevels()
    {
        for (int i = 0; i < itemManager.mainFoodsDatas.Length; i++)
        {
            for (int j = 0; j < mainFoodSpawners.Length; j++)
            {
                if (itemManager.mainFoodsDatas[i].itemId == mainFoodSpawners[j].id)
                {
                    mainFoodSpawners[j].level = itemManager.mainFoodsDatas[i].currentLevel;
                }
            }
        }
    }

    void _ActiveFoodController()
    {
        for (int i = 0; i < mainFoodSpawners.Length; i++)
        {
            for (int j = 0; j < mainFoodSpawners[i].foodSpawner.Length; j++)
            {
                if (mainFoodSpawners[i].level > 0 && mainFoodSpawners[i].level - 1 == j)
                {
                    mainFoodSpawners[i].foodSpawner[j].active = true;
                }
                
                if(mainFoodSpawners[i].foodSpawner[j].active == false)
                {
                    mainFoodSpawners[i].foodSpawner[j]._Deactive();
                }
            }
        }
    }

    void _SetItemManager()
    {
        for (int i = 0; i < mainFoodSpawners.Length; i++)
        {
            for(int j=0; j<itemManager.mainFoodsDatas.Length; j++)
            {
                if(mainFoodSpawners[i].id == itemManager.mainFoodsDatas[j].itemId)
                {
                    break;
                }
                
                if(itemManager.mainFoodsDatas[j].itemId == "")
                {
                    itemManager.mainFoodsDatas[j].itemId = mainFoodSpawners[i].id;
                    itemManager.mainFoodsDatas[j].currentLevel = mainFoodSpawners[i].level;
                    itemManager.mainFoodsDatas[j].unlockLevel = mainFoodSpawners[i].unlockLevel;

                    break;
                }
            }
        }
    }

    void _SetUnlockItem()
    {
        for (int i = 0; i < mainFoodSpawners.Length; i++)
        {
            if (mainFoodSpawners[i].unlockLevel == missionManager.levelID
                && mainFoodSpawners[i].level == 0)
            {
                mainFoodSpawners[i].level = 1;

                for (int j = 0; j < itemManager.mainFoodsDatas.Length; j++)
                {
                    if (itemManager.mainFoodsDatas[j].itemId == mainFoodSpawners[i].id)
                    {
                        itemManager.mainFoodsDatas[j].currentLevel = mainFoodSpawners[i].level;

                        itemManager.save = true;
                    }
                }
            }
        }
    }
}
