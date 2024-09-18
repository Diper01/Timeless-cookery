using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TestSaveSystemManager : MonoBehaviour
{
    [HideInInspector]
    public TestSaveSystemManager instance;

    public string mapName = "Test";

    public List<TestList> testLists = new List<TestList>();

    public bool loaded = false;

    public bool save = false;

    private void Awake()
    {

    }

    private void Start()
    {
        _MakeSingleInstance();

        _SetlevelSettings();

    }

    void _MakeSingleInstance()
    {
        int numInstance = GameObject.FindObjectsOfType<TestSaveSystemManager>().Length;
        if (numInstance > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (save)
        {
            _SaveData();
            save = false;
        }
    }

    public void _SetlevelSettings()
    {
        TestSaveSystemData.mapDataFileName = mapName + "_LevelSettings_data.json";
        string filePath = Path.Combine(Application.persistentDataPath, TestSaveSystemData.mapDataFileName);
        if (File.Exists(filePath))
        {
            _LoadData();
            loaded = true;
        }
        else
        {
            _ResetLevels();
            loaded = false;
        }
    }

    public void _LoadData()
    {
        TestSaveSystemData.Load();

        testLists.Clear();

        for(int i=0; i< TestSaveSystemData.Instance.testLists.Count; i++)
        {
            testLists.Add(TestSaveSystemData.Instance.testLists[i]);
        }
    }

    public void _SaveData()
    {
        TestSaveSystemData.Instance.testLists.Clear();

        for (int i = 0; i < testLists.Count; i++)
        {
            TestSaveSystemData.Instance.testLists.Add(testLists[i]);
        }

        TestSaveSystemData.Save();
    }

    void _ResetLevels()
    {
        //levelDataSets.Clear();
    }
}
