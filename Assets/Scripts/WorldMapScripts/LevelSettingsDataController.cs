using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettingsDataController : MonoBehaviour
{
    public LevelSettingsDataManager levelSettingsDataManager;

    private GameObject[] levelControllers;

    // Start is called before the first frame update
    void Start()
    {
        levelControllers = GameObject.FindGameObjectsWithTag("LevelController");

        //_SetToLevelSettingsData();
        //_SetToLevelController();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void _SetToLevelSettingsData()
    //{
    //    if (levelSettingsDataManager.loaded == false)
    //    {
    //        levelSettingsDataManager.levelDataSets.Clear();

    //        for(int i=0; i< levelControllers.Length; i++)
    //        {
    //            LevelController lvController = levelControllers[i].GetComponent<LevelController>();

    //            for (int j = 0; j < lvController.levelSettings.Length; j++)
    //            {
    //                LevelDataSet levelDataSet = new LevelDataSet();

    //                LevelSetting lvSetting = lvController.levelSettings[j].GetComponent<LevelSetting>();

    //                levelDataSet.levelID = lvSetting.levelID;

    //                levelDataSet.hasCrystal = lvSetting.hasCrystal;
    //                levelDataSet.rewardCrystal = lvSetting.rewardCrystal;

    //                levelDataSet.moneyMission = lvSetting.moneyMission;
    //                levelDataSet.coinCondition = lvSetting.coinCondition;

    //                levelDataSet.timeMission = lvSetting.timeMission;
    //                levelDataSet.missionTimeLimit = lvSetting.missionTimeLimit;

    //                levelDataSet.customerMission = lvSetting.customerMission;
    //                levelDataSet.customerSpawnTime = lvSetting.customerSpawnTime;
    //                levelDataSet.customerLimit = lvSetting.customerLimit;
    //                levelDataSet.customerCondition = lvSetting.customerCondition;
    //                levelDataSet.customerGoodCondition = lvSetting.customerGoodCondition;

    //                levelDataSet.lostCutomerMission = lvSetting.lostCutomerMission;
    //                levelDataSet.allowBadCustomer = lvSetting.allowBadCustomer;

    //                levelDataSet.foodToTrashMission = lvSetting.foodToTrashMission;
    //                levelDataSet.allowTrashFood = lvSetting.allowTrashFood;

    //                levelDataSet.friedFoodMission = lvSetting.friedFoodMission;
    //                levelDataSet.allowFriedFood = lvSetting.allowFriedFood;

    //                //for (int k = 0; k < lvSetting.foodRates.Length; k++)
    //                //{
    //                //    LevelDataFoodRate foodRate = new LevelDataFoodRate();

    //                //    foodRate.foodType = lvSetting.foodRates[k].foodType.ToString();
    //                //    foodRate.id = lvSetting.foodRates[k].id;
    //                //    foodRate.rate = lvSetting.foodRates[k].rate;

    //                //    levelDataSet.foodRates.Add(foodRate);
    //                //}

    //                levelDataSet.oneOrderRate = lvSetting.oneOrderRate;
    //                levelDataSet.twoOrderRate = lvSetting.twoOrderRate;
    //                levelDataSet.mainRate = lvSetting.mainRate;

    //                levelSettingsDataManager.levelDataSets.Add(levelDataSet);

    //                if(i == levelControllers.Length-1 && j == lvController.levelSettings.Length - 1)
    //                {
    //                    levelSettingsDataManager._SaveData();
    //                    Debug.Log("Da hoan tat luu levelSettingsDataManager");
    //                }
    //            }
    //        }
    //    }
    //}

    //void _SetToLevelController()
    //{
    //    if (levelSettingsDataManager.loaded == true)
    //    {
    //        for (int i = 0; i < levelControllers.Length; i++)
    //        {
    //            LevelController lvController = levelControllers[i].GetComponent<LevelController>();

    //            for (int j = 0; j < lvController.levelSettings.Length; j++)
    //            {
    //                LevelSetting lvSetting = lvController.levelSettings[j].GetComponent<LevelSetting>();

    //                lvSetting.levelID = levelSettingsDataManager.levelDataSets[i].levelID;

    //                lvSetting.hasCrystal = levelSettingsDataManager.levelDataSets[i].hasCrystal;
    //                lvSetting.rewardCrystal = levelSettingsDataManager.levelDataSets[i].rewardCrystal;

    //                lvSetting.moneyMission = levelSettingsDataManager.levelDataSets[i].moneyMission;
    //                lvSetting.coinCondition = levelSettingsDataManager.levelDataSets[i].coinCondition;

    //                lvSetting.timeMission = levelSettingsDataManager.levelDataSets[i].timeMission;
    //                lvSetting.missionTimeLimit = levelSettingsDataManager.levelDataSets[i].missionTimeLimit;

    //                lvSetting.customerMission = levelSettingsDataManager.levelDataSets[i].customerMission;
    //                lvSetting.customerSpawnTime = levelSettingsDataManager.levelDataSets[i].customerSpawnTime;
    //                lvSetting.customerLimit = levelSettingsDataManager.levelDataSets[i].customerLimit;
    //                lvSetting.customerCondition = levelSettingsDataManager.levelDataSets[i].customerCondition;
    //                lvSetting.customerGoodCondition = levelSettingsDataManager.levelDataSets[i].customerGoodCondition;

    //                lvSetting.lostCutomerMission = levelSettingsDataManager.levelDataSets[i].lostCutomerMission;
    //                lvSetting.allowBadCustomer = levelSettingsDataManager.levelDataSets[i].allowBadCustomer;

    //                lvSetting.foodToTrashMission = levelSettingsDataManager.levelDataSets[i].foodToTrashMission;
    //                lvSetting.allowTrashFood = levelSettingsDataManager.levelDataSets[i].allowTrashFood;

    //                lvSetting.friedFoodMission = levelSettingsDataManager.levelDataSets[i].friedFoodMission;
    //                lvSetting.allowFriedFood = levelSettingsDataManager.levelDataSets[i].allowFriedFood;

    //                //for (int k = 0; k < lvSetting.foodRates.Length; k++)
    //                //{
    //                //    if (levelSettingsDataManager.levelDataSets[i].foodRates[k].foodType == FoodType.MainFood.ToString())
    //                //    {
    //                //        lvSetting.foodRates[k].foodType = FoodType.MainFood;
    //                //    }
    //                //    else if (levelSettingsDataManager.levelDataSets[i].foodRates[k].foodType == FoodType.SideFood.ToString())
    //                //    {
    //                //        lvSetting.foodRates[k].foodType = FoodType.SideFood;
    //                //    }

    //                //    lvSetting.foodRates[k].id = levelSettingsDataManager.levelDataSets[i].foodRates[k].id;
    //                //    lvSetting.foodRates[k].rate = levelSettingsDataManager.levelDataSets[i].foodRates[k].rate;
    //                //}

    //                lvSetting.oneOrderRate = levelSettingsDataManager.levelDataSets[i].oneOrderRate;
    //                lvSetting.twoOrderRate = levelSettingsDataManager.levelDataSets[i].twoOrderRate;
    //                lvSetting.mainRate = levelSettingsDataManager.levelDataSets[i].mainRate;
    //            }
    //        }

    //        Debug.Log("Da hoan tat doc levelSettingsDataManager");
    //    }
    //}
}
