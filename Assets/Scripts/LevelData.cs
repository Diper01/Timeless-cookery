using System.Collections.Generic;

public class MapData
{
    public int gpGuideSort = 0;
    public int wmGuideSort = 0;
    public int totalStar = 0;

    public List<LevelData> levelDatas = new List<LevelData>();

    public static string mapDataFileName = "MapName_data.json";
    private static MapData instance;

    public static MapData Instance
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
        SaveSystem.Save(mapDataFileName, instance);
    }

    public static void Load()
    {
        instance = SaveSystem.Load<MapData>(mapDataFileName);
    }
}