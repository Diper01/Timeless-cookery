using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideFoodManager : MonoBehaviour
{
    private MissionManager missionManager;
    private ItemManager itemManager;

    [System.Serializable]
    public class SideFoodSpawners
    {
        public string id = "";
        public int level = 0;
        public int unlockLevel = 0;
        public SideFoodController[] foodSpawner;
    }

    public SideFoodSpawners[] sideFoodSpawners;

    private void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        itemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();

        _GetSideFoodLevels();
        _SetUnlockItem();
        _UnlockSideFoodController();

        _SetItemManager();
    }

    private void Update()
    {
        
    }

    void _GetSideFoodLevels()
    {
        for (int i = 0; i < itemManager.sideFoodsDatas.Length; i++)
        {
            for (int j = 0; j < sideFoodSpawners.Length; j++)
            {
                if (itemManager.sideFoodsDatas[i].itemId == sideFoodSpawners[j].id)
                {
                    sideFoodSpawners[j].level = itemManager.sideFoodsDatas[i].currentLevel;
                }
            }
        }
    }

    void _UnlockSideFoodController()
    {
        for (int i = 0; i < sideFoodSpawners.Length; i++)
        {
            for (int j = 0; j < sideFoodSpawners[i].foodSpawner.Length; j++)
            {
                if (sideFoodSpawners[i].level > 0 && sideFoodSpawners[i].level - 1 == j)
                {
                    sideFoodSpawners[i].foodSpawner[j].isUnlocked = true;
                }
            }
        }
    }

    void _SetItemManager()
    {
        for (int i = 0; i < sideFoodSpawners.Length; i++)
        {
            for (int j = 0; j < itemManager.sideFoodsDatas.Length; j++)
            {
                if (sideFoodSpawners[i].id == itemManager.sideFoodsDatas[j].itemId)
                {
                    break;
                }

                if (itemManager.sideFoodsDatas[j].itemId == "")
                {
                    itemManager.sideFoodsDatas[j].itemId = sideFoodSpawners[i].id;
                    itemManager.sideFoodsDatas[j].currentLevel = sideFoodSpawners[i].level;
                    itemManager.sideFoodsDatas[j].unlockLevel = sideFoodSpawners[i].unlockLevel;

                    break;
                }
            }
        }
    }

    void _SetUnlockItem()
    {
        for (int i = 0; i < sideFoodSpawners.Length; i++)
        {
            if (sideFoodSpawners[i].unlockLevel == missionManager.levelID
                && sideFoodSpawners[i].level == 0)
            {
                sideFoodSpawners[i].level = 1;

                for (int j = 0; j < itemManager.sideFoodsDatas.Length; j++)
                {
                    if (itemManager.sideFoodsDatas[j].itemId == sideFoodSpawners[i].id)
                    {
                        itemManager.sideFoodsDatas[j].currentLevel = sideFoodSpawners[i].level;

                        itemManager.save = true;
                    }
                }
            }
        }
    }
}
