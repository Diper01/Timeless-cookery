using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUpgradeGuide : MonoBehaviour
{
    private LevelManager levelManager;
    private MissionManager missionManager;

    public GameObject guideLockBG, upgradeGuideBG, handGuide, lockUpgradeButton, winMenu, loseMenu;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _SetActiveLockButtonGuide();
        _ActiveGuide();
    }

    void _SetActiveLockButtonGuide()
    {
        if (winMenu.activeSelf == true || loseMenu.activeSelf == true)
        {
            if (guideLockBG.activeSelf == false && lockUpgradeButton.activeSelf == false && upgradeGuideBG.activeSelf == false)
            {
                Time.timeScale = 1;
                guideLockBG.SetActive(true);
            }
        }
    }

    void _ActiveGuide()
    {
        if (winMenu.activeSelf == true && winMenu.GetComponent<WinMenu>().countTemp == missionManager.currentPlayerCoin
            || loseMenu.activeSelf == true)
        {
            if(lockUpgradeButton.activeSelf== true && upgradeGuideBG.activeSelf == false && handGuide.activeSelf == false)
            {
                lockUpgradeButton.SetActive(false);
                upgradeGuideBG.SetActive(true);
                handGuide.SetActive(true);

                Time.timeScale = 0;
            }
        }

    }

    public void _SaveLevelManager()
    {
        levelManager._SaveData();
    }
}
