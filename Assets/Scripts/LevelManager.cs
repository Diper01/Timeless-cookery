using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class LevelData
{
    public int levelID = 0;
    public int star = 0;
    public bool levelCompleted = false;
}

public class LevelManager : MonoBehaviour
{
    [HideInInspector]
    public LevelManager instance;

    public string mapName = "MapName";

    public int gpGuideSort = 0;
    public int wmGuideSort = 0;

    public int startId = 1;

    public int totalStar = 0;

    public LevelData[] levels;

    public bool save = false;

    private void Awake()
    {

    }

    private void Start()
    {
        _MakeSingleInstance();

        //_SetMap();

    }

    void _MakeSingleInstance()
    {
        int numInstance = GameObject.FindGameObjectsWithTag("LevelManager").Length;
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

    public void _SetMap()
    {
        MapData.mapDataFileName = mapName + "_data.json";
        string filePath = Path.Combine(Application.persistentDataPath, MapData.mapDataFileName);
        if (File.Exists(filePath))
        {
            _ResetLevels();
            _SetLevelID();
            _LoadData();
        }
        else
        {
            _SetLevelID();
            _ResetLevels();
        }
    }

    void _SetLevelID()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].levelID = startId + i;
        }
    }

    public void _LoadData()
    {
        MapData.Load();

        gpGuideSort = MapData.Instance.gpGuideSort;
        wmGuideSort = MapData.Instance.wmGuideSort;

        totalStar = MapData.Instance.totalStar;

        for (int i= 0; i<levels.Length; i++)
        {
            levels[i] = MapData.Instance.levelDatas[i];
        }
    }

    public void _SaveData()
    {
        MapData.Instance.levelDatas.Clear();

        MapData.Instance.gpGuideSort = gpGuideSort;
        MapData.Instance.wmGuideSort = wmGuideSort;

        MapData.Instance.totalStar = totalStar;

        for (int i = 0; i < levels.Length; i++)
        {
            MapData.Instance.levelDatas.Add(levels[i]);
        }

        MapData.Save();
    }

    void _ResetLevels()
    {

        gpGuideSort = 0;
        wmGuideSort = 0;
        totalStar = 0;

        for(int i=0; i<levels.Length; i++)
        {
            levels[i].star = 0;
            levels[i].levelCompleted = false;
        }
    }
}
