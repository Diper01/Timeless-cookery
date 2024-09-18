using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GrillsData
{
    public string itemId;
    public int currentLevel = 1;
    public int unlockLevel = 0;
}

[System.Serializable]
public class PlatesData
{
    public string itemId = "";
    public int currentLevel = 1;
    public int unlockLevel = 0;
}

[System.Serializable]
public class MainFoodsData
{
    public string itemId = "";
    public int currentLevel = 1;
    public int unlockLevel = 0;
}

[System.Serializable]
public class SideFoodsData
{
    public string itemId = "";
    public int currentLevel = 1;
    public int unlockLevel = 0;
}

[System.Serializable]
public class ItemPriceRemote
{
    public string iTEM_TYPE = "";
    public string itemId = "";
    public List<int> upgradePrice = new List<int>();
    public List<int> gpPrice = new List<int>();
}

public class ItemManager : MonoBehaviour
{
    [HideInInspector]
    public ItemManager instance;

    private PlayerStats playerStats;
    private LevelManager levelManager;

    public string mapName = "MapName";

    //public bool firstPlay = false;

    public GrillsData[] grillsDatas;
    public PlatesData[] platesDatas;
    public MainFoodsData[] mainFoodsDatas;
    public SideFoodsData[] sideFoodsDatas;

    public List<ItemPriceRemote> itemPriceRemotes = new List<ItemPriceRemote>();

    public enum ITEM_TYPE { grills, plates, mainFood, sideFood}

    public bool save = false;

    private void Awake()
    {

    }

    private void Start()
    {
        _MakeSingleInstance();

        //_SetMapItem();

    }

    private void Update()
    {
        if (save == true)
        {
            _SaveData();
            save = false;
        }
    }

    void _MakeSingleInstance()
    {
        int numInstance = GameObject.FindGameObjectsWithTag("ItemManager").Length;
        if (numInstance > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void _SetMapItem()
    {
        ItemData.shopDataFileName = mapName + "_Shop_data.json";
        string filePath = Path.Combine(Application.persistentDataPath, ItemData.shopDataFileName);
        if (File.Exists(filePath))
        {
            _Reset();
            _LoadData();
        }
        else
        {
            _Reset();
        }
    }

    public void _LoadData()
    {
        ItemData.Load();

        for (int i = 0; i < grillsDatas.Length; i++)
        {
            grillsDatas[i] = ItemData.Instance.grillsDatas[i];
        }

        for (int i = 0; i < platesDatas.Length; i++)
        {
            platesDatas[i] = ItemData.Instance.platesDatas[i];
        }

        for (int i = 0; i < mainFoodsDatas.Length; i++)
        {
            mainFoodsDatas[i] = ItemData.Instance.mainFoodsDatas[i];
        }

        for (int i = 0; i < sideFoodsDatas.Length; i++)
        {
            sideFoodsDatas[i] = ItemData.Instance.sideFoodsDatas[i];
        }

         if(ItemData.Instance.itemPriceRemotesDatas.Count > 0)
        {
            itemPriceRemotes.Clear();

            for (int i=0; i< ItemData.Instance.itemPriceRemotesDatas.Count; i++)
            {
                itemPriceRemotes.Add(ItemData.Instance.itemPriceRemotesDatas[i]);
            }
        }
    }

    public void _SaveData()
    {
        ItemData.Instance.grillsDatas.Clear();
        for (int i = 0; i < grillsDatas.Length; i++)
        {
            ItemData.Instance.grillsDatas.Add(grillsDatas[i]);
        }

        ItemData.Instance.platesDatas.Clear();
        for (int i = 0; i < platesDatas.Length; i++)
        {
            ItemData.Instance.platesDatas.Add(platesDatas[i]);
        }

        ItemData.Instance.mainFoodsDatas.Clear();
        for (int i = 0; i < mainFoodsDatas.Length; i++)
        {
            ItemData.Instance.mainFoodsDatas.Add(mainFoodsDatas[i]);
        }

        ItemData.Instance.sideFoodsDatas.Clear();
        for (int i = 0; i < sideFoodsDatas.Length; i++)
        {
            ItemData.Instance.sideFoodsDatas.Add(sideFoodsDatas[i]);
        }

        if (itemPriceRemotes.Count > 0)
        {
            ItemData.Instance.itemPriceRemotesDatas.Clear();

            for (int i = 0; i < itemPriceRemotes.Count; i++)
            {
                ItemData.Instance.itemPriceRemotesDatas.Add(itemPriceRemotes[i]);
            }
        }

        ItemData.Save();
    }

    void _Reset()
    {
        itemPriceRemotes.Clear();

        for (int i = 0; i < grillsDatas.Length; i++)
        {
            grillsDatas[i].itemId = "";
            grillsDatas[i].currentLevel = 0;
            grillsDatas[i].unlockLevel = 0;
        }

        for (int i = 0; i < platesDatas.Length; i++)
        {
            platesDatas[i].itemId = "";
            platesDatas[i].currentLevel = 0;
            platesDatas[i].unlockLevel = 0;
        }

        for (int i = 0; i < mainFoodsDatas.Length; i++)
        {
            mainFoodsDatas[i].itemId = "";
            mainFoodsDatas[i].currentLevel = 0;
            mainFoodsDatas[i].unlockLevel = 0;
        }

        for (int i = 0; i < sideFoodsDatas.Length; i++)
        {
            sideFoodsDatas[i].itemId = "";
            sideFoodsDatas[i].currentLevel = 0;
            sideFoodsDatas[i].unlockLevel = 0;
        }
    }
}
