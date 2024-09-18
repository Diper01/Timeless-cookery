using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestChildList
{
    public int testInt;
    public string testString;
}

[System.Serializable]
public class TestList
{
    public string testName;
    public List<TestChildList> testChildLists = new List<TestChildList>();
}

public class TestSaveSystemData
{
    public List<TestList> testLists = new List<TestList>();

    public static string mapDataFileName = "MapName_data.json";
    private static TestSaveSystemData instance;

    public static TestSaveSystemData Instance
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
        instance = SaveSystem.Load<TestSaveSystemData>(mapDataFileName);
    }
}
