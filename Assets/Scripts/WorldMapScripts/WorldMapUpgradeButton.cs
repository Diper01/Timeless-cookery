using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapUpgradeButton : MonoBehaviour
{
    private NextScene nextScene;
    private LevelManager levelManager;
    private MissionManager missionManager;

    //private GoogleFireBaseEvents googleFireBaseEvents;

    public string sceneName = "UpgradeShop";

    // Start is called before the first frame update
    void Start()
    {
        nextScene = GameObject.FindGameObjectWithTag("NextScene").GetComponent<NextScene>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();

        //googleFireBaseEvents = FindObjectOfType<GoogleFireBaseEvents>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void _OnTouch()
    {
        //googleFireBaseEvents.ClickUpgradeWorldMap();

        sceneName = levelManager.mapName.ToString() + "_UpgradeShop";
        nextScene.NextLevel(sceneName);
    }
}
