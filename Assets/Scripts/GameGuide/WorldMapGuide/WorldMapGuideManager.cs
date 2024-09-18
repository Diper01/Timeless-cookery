using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapGuideManager : MonoBehaviour
{
    private MissionManager missionManager;
    private LevelManager levelManager;
    private MapController mapController;
    private PlayerStats playerStats;

    public WorldMapGuideController[] worldMapGuideControllers;

    // Start is called before the first frame update
    void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        mapController = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapController>();
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();

        _SetActive();

        StartCoroutine(_SetActiveGuide());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void _SetActive()
    {
        if(playerStats.tutMap == false)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator _SetActiveGuide()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < worldMapGuideControllers.Length; i++)
        {
            if (mapController.selectedMap == worldMapGuideControllers[i].activeWorld)
            {
                if (worldMapGuideControllers[i].guideSort == levelManager.wmGuideSort
                    && worldMapGuideControllers[i].activeLevel > 1
                    && levelManager.levels[worldMapGuideControllers[i].activeLevel - 2].levelCompleted == true
                    && levelManager.levels[worldMapGuideControllers[i].activeLevel - 1].star == worldMapGuideControllers[i].activeStar) 
                {
                    if (!MyConstant.isTutorialViewed(i))
                    {
                        worldMapGuideControllers[i].gameObject.SetActive(true);
                        MyConstant.setTutorialStatus(i, true);
                    } 

                }
                else if (worldMapGuideControllers[i].guideSort == levelManager.wmGuideSort
                    && worldMapGuideControllers[i].activeLevel == 1 
                    && levelManager.levels[0].star == worldMapGuideControllers[i].activeStar)
                {
                    if (!MyConstant.isTutorialViewed(i))
                    {
                        worldMapGuideControllers[i].gameObject.SetActive(true);
                        MyConstant.setTutorialStatus(i, true);
                    }
                }
            }


        }
    }
}
