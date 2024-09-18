using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;

[System.Serializable]
public class LevelDataFoodRate
{
    public string foodType;
    public string id;
    public int rate;
}

[System.Serializable]
public class LevelDataSet
{
    public int levelID = 0;
    public int starSet = 0;

    public bool hasCrystal = false;
    public int rewardCrystal = 5;

    public string firstTarget;

    public float missionTimeLimit = 90f;

    public float customerSpawnTime = 5f;
    public int customerLimit = 15;
    public int customerCondition = 10;

    public string secondTarget;

    public int coinCondition = 70;

    public int dishCondition = 10;

    public int customerGoodCondition = 10;

    public bool lostCutomerMission = false;
    public int allowAngryCustomer = 0;

    public bool foodToTrashMission = false;
    public int allowTrashFood = 0;

    public bool friedFoodMission = false;
    public int allowFriedFood = 0;

    public List<LevelDataFoodRate> foodRates = new List<LevelDataFoodRate>();

    public int orderSize = 1;
    public int oneOrderRate = 0;
    public int twoOrderRate = 0;
    public int threeOrderRate = 0;
    public int mainRate = 0;
}

public class LevelSettingsDataManager : MonoBehaviour
{
    [HideInInspector]
    public LevelSettingsDataManager instance;

    public string mapName = "MapName";

    public string url = "";

    public List<LevelDataSet> levelDataSets = new List<LevelDataSet>();

    public bool loaded = false;

    public bool save = false;

    private void Awake()
    {
        //_MakeSingleInstance();
    }

    private void Start()
    {

    }

    void _MakeSingleInstance()
    {
        int numInstance = GameObject.FindObjectsOfType<LevelSettingsDataManager>().Length;
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
        LevelSettingsData.mapDataFileName = mapName + "_LevelSettings_data.json";
        string filePath = Path.Combine(Application.persistentDataPath, LevelSettingsData.mapDataFileName);
        Debug.Log("Current Version "+Application.version);
        if (File.Exists(filePath) && MyConstant.getGameVersion()==Application.version)
        {
            _LoadData();
            loaded = true;
            //_SetToLevelController();
        }
        else
        {
           _LoadFromResource();
            MyConstant.setGameVersion(Application.version);
        }
    }

    public void _LoadData()
    {
        LevelSettingsData.Load();

        levelDataSets = LevelSettingsData.Instance.LevelDataSets;
    }

    public void _SaveData()
    {
        //LevelSettingsData.Instance.LevelDataSets.Clear();

        LevelSettingsData.Instance.LevelDataSets = levelDataSets;

        LevelSettingsData.Save();
    }

    public IEnumerator RequestWeb()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        webRequest.SetRequestHeader("appPassword", "efoxstudio2019");
        webRequest.method = UnityWebRequest.kHttpVerbGET;

        //   webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log("www.error - " + webRequest.error);
        }
        else
        {
            JSONObject json = new JSONObject(webRequest.downloadHandler.text);

            levelDataSets.Clear();

            for (int i = 0; i < json.list.Count; i++)
            {
                LevelDataSet levelDataSet = new LevelDataSet();

                levelDataSet.levelID = Convert.ToInt32(json[i]["levelID"].ToString().Replace("\"", ""));
                levelDataSet.starSet = Convert.ToInt32(json[i]["starSet"].ToString().Replace("\"", ""));

                levelDataSet.hasCrystal = Convert.ToBoolean(json[i]["hasCrystal"].ToString().Replace("\"", ""));
                levelDataSet.rewardCrystal = Convert.ToInt32(json[i]["rewardCrystal"].ToString().Replace("\"", "")); //5;
                //First Target
                levelDataSet.firstTarget = json[i]["firstTarget"].ToString().Replace("\"", "");
                //Time
                levelDataSet.missionTimeLimit = Convert.ToSingle(json[i]["missionTimeLimit"].ToString().Replace("\"", ""));
                //Customer
                levelDataSet.customerSpawnTime = Convert.ToSingle(json[i]["customerSpawnTime"].ToString().Replace("\"", ""));
                levelDataSet.customerLimit = Convert.ToInt32(json[i]["customerLimit"].ToString().Replace("\"", ""));
                levelDataSet.customerCondition = Convert.ToInt32(json[i]["customerCondition"].ToString().Replace("\"", ""));

                //Second Target
                levelDataSet.secondTarget = json[i]["secondTarget"].ToString().Replace("\"", "");
                //Coin
                levelDataSet.coinCondition = Convert.ToInt32(json[i]["coinCondition"].ToString().Replace("\"", ""));
                //Dish
                levelDataSet.dishCondition = Convert.ToInt32(json[i]["dishCondition"].ToString().Replace("\"", ""));
                //Like
                levelDataSet.customerGoodCondition = Convert.ToInt32(json[i]["customerGoodCondition"].ToString().Replace("\"", ""));

                levelDataSet.lostCutomerMission = Convert.ToBoolean(json[i]["lostCutomerMission"].ToString().Replace("\"", ""));
                levelDataSet.allowAngryCustomer = Convert.ToInt32(json[i]["allowAngryCustomer"].ToString().Replace("\"", ""));

                levelDataSet.foodToTrashMission = Convert.ToBoolean(json[i]["foodToTrashMission"].ToString().Replace("\"", ""));
                levelDataSet.allowTrashFood = Convert.ToInt32(json[i]["allowTrashFood"].ToString().Replace("\"", ""));

                levelDataSet.friedFoodMission = Convert.ToBoolean(json[i]["friedFoodMission"].ToString().Replace("\"", ""));
                levelDataSet.allowFriedFood = Convert.ToInt32(json[i]["allowFriedFood"].ToString().Replace("\"", ""));


                JSONObject foodRates = json[i]["foodRates"];

                for (int j = 0; j < foodRates.list.Count; j++)
                {
                    LevelDataFoodRate levelDataFoodRate = new LevelDataFoodRate();

                    levelDataFoodRate.foodType = foodRates[j]["foodType"].ToString().Replace("\"", "");
                    levelDataFoodRate.id = foodRates[j]["id"].ToString().Replace("\"", "");
                    levelDataFoodRate.rate = Convert.ToInt32(foodRates[j]["rate"].ToString().Replace("\"", ""));

                    levelDataSet.foodRates.Add(levelDataFoodRate);
                }

                levelDataSet.orderSize = Convert.ToInt32(json[i]["orderSize"].ToString().Replace("\"", ""));
                levelDataSet.oneOrderRate = Convert.ToInt32(json[i]["oneOrderRate"].ToString().Replace("\"", ""));
                levelDataSet.twoOrderRate = Convert.ToInt32(json[i]["twoOrderRate"].ToString().Replace("\"", ""));
                levelDataSet.threeOrderRate = Convert.ToInt32(json[i]["threeOrderRate"].ToString().Replace("\"", ""));
                levelDataSet.mainRate = Convert.ToInt32(json[i]["mainRate"].ToString().Replace("\"", ""));

                levelDataSets.Add(levelDataSet);
            }

            Debug.Log("Setting from web is loaded");
            //_SetToLevelController();
            _SaveData();
        }

        yield break;
    }

    public void _LoadFromResource()
    {
        TextAsset txtAsset = (TextAsset)Resources.Load(mapName + "_LevelSettings_data", typeof(TextAsset));
        
        if(txtAsset != null)
        {
            string data = txtAsset.text;
            LevelSettingsData output;

            //Debug.Log("Loaded From resource file path =" + data);

            output = JsonUtility.FromJson<LevelSettingsData>(data);

            levelDataSets.Clear();

            for (int i = 0; i < output.LevelDataSets.Count; i++)
            {
                levelDataSets.Add(output.LevelDataSets[i]);
            }

            Debug.Log("Loaded From resource");

            //_SetToLevelController();
            _SaveData();
        }
    }


    public void _SetToLevelSettingsData()
    {
        if (loaded == false)
        {
            GameObject[] levelControllers = GameObject.FindGameObjectsWithTag("LevelController");

            levelDataSets.Clear();

            for (int i = 0; i < levelControllers.Length; i++)
            {
                LevelController lvController = levelControllers[i].GetComponent<LevelController>();

                for (int j = 0; j < lvController.levelSettings.Length; j++)
                {
                    LevelDataSet levelDataSet = new LevelDataSet();

                    LevelSetting lvSetting = lvController.levelSettings[j].GetComponent<LevelSetting>();

                    levelDataSet.levelID = lvController.levelID;

                    levelDataSet.hasCrystal = lvSetting.hasCrystal;
                    levelDataSet.rewardCrystal = lvSetting.rewardCrystal;

                    //First Target
                    levelDataSet.firstTarget = lvSetting.firstTarget.ToString();
                    //Time
                    levelDataSet.missionTimeLimit = lvSetting.missionTimeLimit;
                    //Customer
                    levelDataSet.customerSpawnTime = lvSetting.customerSpawnTime;
                    levelDataSet.customerLimit = lvSetting.customerLimit;
                    levelDataSet.customerCondition = lvSetting.customerCondition;

                    //Second Target
                    levelDataSet.secondTarget = lvSetting.secondTarget.ToString();
                    //Coin
                    levelDataSet.coinCondition = lvSetting.coinCondition;
                    //Dish
                    levelDataSet.dishCondition = lvSetting.dishCondition;
                    //Like
                    levelDataSet.customerGoodCondition = lvSetting.customerGoodCondition;

                    levelDataSet.lostCutomerMission = lvSetting.lostCutomerMission;
                    levelDataSet.allowAngryCustomer = lvSetting.allowAngryCustomer;

                    levelDataSet.foodToTrashMission = lvSetting.foodToTrashMission;
                    levelDataSet.allowTrashFood = lvSetting.allowTrashFood;

                    levelDataSet.friedFoodMission = lvSetting.friedFoodMission;
                    levelDataSet.allowFriedFood = lvSetting.allowFriedFood;

                    for (int k = 0; k < lvSetting.foodRates.Count; k++)
                    {
                        LevelDataFoodRate foodRate = new LevelDataFoodRate();

                        foodRate.foodType = lvSetting.foodRates[k].foodType.ToString();
                        foodRate.id = lvSetting.foodRates[k].id;
                        foodRate.rate = lvSetting.foodRates[k].rate;

                        levelDataSet.foodRates.Add(foodRate);
                    }

                    levelDataSet.orderSize = lvSetting.orderSize;
                    levelDataSet.oneOrderRate = lvSetting.oneOrderRate;
                    levelDataSet.twoOrderRate = lvSetting.twoOrderRate;
                    levelDataSet.threeOrderRate = lvSetting.threeOrderRate;
                    levelDataSet.mainRate = lvSetting.mainRate;

                    levelDataSets.Add(levelDataSet);
                }
            }

            //_SetToLevelController();
            _SaveData();
        }
    }

    //public void _SetToLevelController()
    //{
    //    GameObject[] levelControllers = GameObject.FindGameObjectsWithTag("LevelController");

    //    for (int i = 0; i < levelControllers.Length; i++)
    //    {
    //        LevelController lvController = levelControllers[i].GetComponent<LevelController>();

    //        for (int j = 0; j < lvController.levelSettings.Length; j++)
    //        {
    //            LevelSetting lvSetting = lvController.levelSettings[j].GetComponent<LevelSetting>();

    //            for (int k = 0; k < levelDataSets.Count; k++)
    //            {
    //                if (lvSetting.levelID == levelDataSets[k].levelID
    //                    && lvSetting.starSet == levelDataSets[k].starSet)
    //                {
    //                    lvSetting.hasCrystal = levelDataSets[k].hasCrystal;
    //                    lvSetting.rewardCrystal = levelDataSets[k].rewardCrystal;

    //                    //First Target
    //                    if (levelDataSets[k].firstTarget == MissionManager.FirstTarget.Time.ToString())
    //                    {
    //                        lvSetting.firstTarget = MissionManager.FirstTarget.Time;
    //                    }
    //                    else if (levelDataSets[k].firstTarget == MissionManager.FirstTarget.Customer.ToString())
    //                    {
    //                        lvSetting.firstTarget = MissionManager.FirstTarget.Customer;
    //                    }
    //                    //Time
    //                    lvSetting.missionTimeLimit = levelDataSets[k].missionTimeLimit;
    //                    //Customer
    //                    lvSetting.customerSpawnTime = levelDataSets[k].customerSpawnTime;
    //                    lvSetting.customerLimit = levelDataSets[k].customerLimit;
    //                    lvSetting.customerCondition = levelDataSets[k].customerCondition;

    //                    //Second Target
    //                    if (levelDataSets[k].secondTarget == MissionManager.SecondTarget.Null.ToString())
    //                    {
    //                        lvSetting.secondTarget = MissionManager.SecondTarget.Null;
    //                    }
    //                    else if (levelDataSets[k].secondTarget == MissionManager.SecondTarget.Coin.ToString())
    //                    {
    //                        lvSetting.secondTarget = MissionManager.SecondTarget.Coin;
    //                    }
    //                    else if (levelDataSets[k].secondTarget == MissionManager.SecondTarget.Dish.ToString())
    //                    {
    //                        lvSetting.secondTarget = MissionManager.SecondTarget.Dish;
    //                    }
    //                    else if (levelDataSets[k].secondTarget == MissionManager.SecondTarget.Like.ToString())
    //                    {
    //                        lvSetting.secondTarget = MissionManager.SecondTarget.Like;
    //                    }
    //                    //Coin
    //                    lvSetting.coinCondition = levelDataSets[k].coinCondition;
    //                    //Dish
    //                    lvSetting.dishCondition = levelDataSets[k].dishCondition;
    //                    //Like
    //                    lvSetting.customerGoodCondition = levelDataSets[k].customerGoodCondition;

    //                    lvSetting.lostCutomerMission = levelDataSets[k].lostCutomerMission;
    //                    lvSetting.allowAngryCustomer = levelDataSets[k].allowAngryCustomer;

    //                    lvSetting.foodToTrashMission = levelDataSets[k].foodToTrashMission;
    //                    lvSetting.allowTrashFood = levelDataSets[k].allowTrashFood;

    //                    lvSetting.friedFoodMission = levelDataSets[k].friedFoodMission;
    //                    lvSetting.allowFriedFood = levelDataSets[k].allowFriedFood;

    //                    lvSetting.foodRates.Clear();

    //                    for (int m = 0; m < levelDataSets[k].foodRates.Count; m++)
    //                    {
    //                        FoodRate foodRate = new FoodRate();

    //                        if (levelDataSets[k].foodRates[m].foodType == FoodType.MainFood.ToString())
    //                        {
    //                            foodRate.foodType = FoodType.MainFood;
    //                        }
    //                        else if (levelDataSets[k].foodRates[m].foodType == FoodType.SideFood.ToString())
    //                        {
    //                            foodRate.foodType = FoodType.SideFood;
    //                        }

    //                        foodRate.id = levelDataSets[k].foodRates[m].id;
    //                        foodRate.rate = levelDataSets[k].foodRates[m].rate;

    //                        lvSetting.foodRates.Add(foodRate);
    //                    }

    //                    lvSetting.orderSize = levelDataSets[k].orderSize;
    //                    lvSetting.oneOrderRate = levelDataSets[k].oneOrderRate;
    //                    lvSetting.twoOrderRate = levelDataSets[k].twoOrderRate;
    //                    lvSetting.threeOrderRate = levelDataSets[k].threeOrderRate;
    //                    lvSetting.mainRate = levelDataSets[k].mainRate;
    //                }
    //            }
    //        }
    //    }

    //    Debug.Log("Setting Done");
    //}
}
