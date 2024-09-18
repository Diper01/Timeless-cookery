using System.Collections.Generic;

public class ItemData
{
    public List<GrillsData> grillsDatas = new List<GrillsData>();
    public List<PlatesData> platesDatas = new List<PlatesData>();
    public List<MainFoodsData> mainFoodsDatas = new List<MainFoodsData>();
    public List<SideFoodsData> sideFoodsDatas = new List<SideFoodsData>();

    public List<ItemPriceRemote> itemPriceRemotesDatas = new List<ItemPriceRemote>();

    public static string shopDataFileName = "MapName_Shop_data.json";
    private static ItemData instance;

    public static ItemData Instance
    {
        get
        {
            if (instance == null)
            {
                Load();
            }
            return instance;
        }
    }

    public static void Save()
    {
        SaveSystem.Save(shopDataFileName, instance);
    }

    public static void Load()
    {
        instance = SaveSystem.Load<ItemData>(shopDataFileName);
    }
}
