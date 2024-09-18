using System;
using System.Collections;
using System.Collections.Generic;

//using System.Linq;
//using System.Text;
//using System.Net;

using UnityEngine;
using UnityEngine.Networking;

//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

public class TestReadFromWeb : MonoBehaviour
{
    public List<LevelDataSet> levelDataSets = new List<LevelDataSet>();

    public string url;

    public bool load = false;

    private void Update()
    {
        if (load == true)
        {
            load = false;
            StartCoroutine(RequestWeb());
        }
    }

    IEnumerator RequestWeb()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
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

            for(int i=0; i<json.list.Count; i++)
            {
                LevelDataSet levelDataSet = new LevelDataSet();

                levelDataSet.levelID = Convert.ToInt32(json[i]["levelID"].ToString().Replace("\"", ""));

                JSONObject foodRates = json[i]["foodRates"];

                for(int j=0; j< foodRates.list.Count; j++)
                {
                    LevelDataFoodRate levelDataFoodRate = new LevelDataFoodRate();

                    levelDataFoodRate.foodType = foodRates[j]["foodType"].ToString().Replace("\"", "");
                    levelDataFoodRate.id = foodRates[j]["id"].ToString().Replace("\"", "");
                    levelDataFoodRate.rate = Convert.ToInt32(foodRates[j]["rate"].ToString().Replace("\"", ""));

                    levelDataSet.foodRates.Add(levelDataFoodRate);
                }

                levelDataSets.Add(levelDataSet);
            }
        }
    }
}
