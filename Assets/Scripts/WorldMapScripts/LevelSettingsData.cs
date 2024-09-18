using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettingsData
{
    public List<LevelDataSet> LevelDataSets = new List<LevelDataSet>();

    public static string mapDataFileName = "MapName_data.json";
    private static LevelSettingsData instance;

    public static LevelSettingsData Instance
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
        instance = SaveSystem.Load<LevelSettingsData>(mapDataFileName);
    }
}
