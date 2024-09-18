using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitMissionMenu : MonoBehaviour
{
    private GamePlayMenuManager gamePlayMenuManager;
    private PlayerStats playerStats;
    private NextScene nextScene;
    private MissionManager missionManager;
    //private AdManager adManager;
    public Text levelText;

    public string sceneName = "WorldMap";

    private void Update()
    {
        if (gameObject.activeSelf == true)
        {
            FindObjectOfType<AudioManager>()._Pause();

            levelText.text = "LEVEL " + missionManager.levelID.ToString();

           // Time.timeScale = 0;
        }
    }

    private void Start()
    {
        gamePlayMenuManager = GameObject.FindGameObjectWithTag("GamePlayMenuManager").GetComponent<GamePlayMenuManager>();
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        nextScene = GameObject.FindGameObjectWithTag("NextScene").GetComponent<NextScene>();
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        //adManager = FindObjectOfType<AdManager>();
    }

    public void _YesButton()
    {
        missionManager.scenePos = 0;
        _ShowAd();
        nextScene.NextLevel(sceneName);
    }

    public void _CloseButton()
    {
        FindObjectOfType<AudioManager>()._UnPause();

        gamePlayMenuManager._DeactiveBlackBG();
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    void _ShowAd()
    {
        missionManager.loseCount++;

        //if (missionManager.levelID >= playerStats.levelAdShow && missionManager.loseCount == playerStats.loseAdShow)
        //{
        //    missionManager.loseCount = 0;

        //    if (adManager.fullScreenAdRequested == false)
        //    {
        //        adManager.RequestFullScreenAd();
        //        adManager.ShowFullScreenAd();
        //    }

        //    else if (adManager.fullScreenAdRequested == true)
        //    {
        //        adManager.ShowFullScreenAd();
        //    }
        //}
    }
}
