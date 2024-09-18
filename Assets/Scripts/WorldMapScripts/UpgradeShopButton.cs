using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShopButton : MonoBehaviour
{
    private NextScene nextScene;
    private LevelManager levelManager;
    private MissionManager missionManager;
    public string sceneName = "UpgradeShop";

    // Start is called before the first frame update
    void Start()
    {
        nextScene = GameObject.FindGameObjectWithTag("NextScene").GetComponent<NextScene>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _OnTouch()
    {
        missionManager.scenePos = 2;

        sceneName = levelManager.mapName.ToString() + "_UpgradeShop";
        nextScene.NextLevel(sceneName);
    }
}
