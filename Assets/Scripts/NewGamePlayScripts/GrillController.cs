using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrillController : MonoBehaviour
{
    private ItemManager itemManager; 

    [System.Serializable]
    public class GrillList
    {
        public string id = "";
        public int level = 0;
        public int unlockLevel = 0;
        public Grill[] grills;
    }

    public GrillList[] grillList;

    private void Start()
    {
        itemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();

        _GetGrillLevels();
        _UnlockGrills();
        _SetTime();

        _SetItemManager();
    }

    private void Update()
    {
        
    }

    void _GetGrillLevels()
    {
        for (int i = 0; i < itemManager.grillsDatas.Length; i++)
        {
            for (int j = 0; j < grillList.Length; j++)
            {
                if (itemManager.grillsDatas[i].itemId == grillList[j].id)
                {
                    grillList[j].level = itemManager.grillsDatas[i].currentLevel;
                }
            }
        }
    }

    void _UnlockGrills()
    {
        for(int i=0; i<grillList.Length; i++)
        {
            for(int j=0; j<grillList[i].level; j++)
            {
                grillList[i].grills[j].isUnlocked = true;
            }
        }
    }

    void _SetTime()
    {
        for (int i = 0; i < grillList.Length; i++)
        {
            for (int j = 0; j < grillList[i].level; j++)
            {
                if(grillList[i].level > 2)
                {
                    grillList[i].grills[j].processTime -= 1;
                    grillList[i].grills[j].overBurnedTime += 2;
                }
            }
        }
    }

    void _SetItemManager()
    {
        for (int i = 0; i < grillList.Length; i++)
        {
            for (int j = 0; j < itemManager.grillsDatas.Length; j++)
            {
                if (grillList[i].id == itemManager.grillsDatas[j].itemId)
                {
                    break;
                }

                if (itemManager.grillsDatas[j].itemId == "")
                {
                    itemManager.grillsDatas[j].itemId = grillList[i].id;
                    itemManager.grillsDatas[j].currentLevel = grillList[i].level;
                    itemManager.grillsDatas[j].unlockLevel = grillList[i].unlockLevel;

                    break;
                }
            }
        }
    }
}
