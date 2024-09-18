using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayGuideManager : MonoBehaviour
{
    private PlayerStats playerStats;
    private MissionManager missionManager;
    private LevelManager levelManager;

    public GameplayGuideController[] gameplayGuideControllers;
    
    // Start is called before the first frame update
    void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();

        _SetActive();

        _SetActiveGuide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void _SetActive()
    {
        if (playerStats.tutGameplay == false)
        {
            gameObject.SetActive(false);
        }
    }

    void _SetActiveGuide()
    {
        for (int i = 0; i < gameplayGuideControllers.Length; i++)
        {
            if (gameplayGuideControllers[i].guideSort == levelManager.gpGuideSort+1
                && missionManager.levelID == gameplayGuideControllers[i].activeLevel
                && missionManager.currentStar == gameplayGuideControllers[i].activeStar)
            {
                if (!MyConstant.isInGameTutorialViewed(i))
                {
                    gameplayGuideControllers[i].gameObject.SetActive(true);
                    MyConstant.setInGameTutorialStatus(i, true);
                }
            


            }
        }
    }
}
