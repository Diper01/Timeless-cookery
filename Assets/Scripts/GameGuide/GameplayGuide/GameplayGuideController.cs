using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayGuideController : MonoBehaviour
{
    private MissionManager missionManager;
    private LevelManager levelManager;
    //private GoogleFireBaseEvents googleFireBaseEvents;

    public int guideSort = 0;
    public int activeLevel = 0;
    public int activeStar = 0;

    public GameObject[] guideSteps;

    public int currentStep = 0;

    // Start is called before the first frame update
    void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();

        //googleFireBaseEvents = GameObject.FindObjectOfType<GoogleFireBaseEvents>();

        guideSteps[0].SetActive(true);
    }

    public void _NextStep()
    {
        currentStep++;

        if(currentStep < guideSteps.Length)
        {
            guideSteps[currentStep - 1].SetActive(false);
            guideSteps[currentStep].SetActive(true);
        }
        else if(currentStep == guideSteps.Length)
        {
            guideSteps[currentStep - 1].SetActive(false);
            levelManager.gpGuideSort += 1;
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            //googleFireBaseEvents.TutorialQuit("WorldMap_" + "activeLevel_" + activeLevel + "activeStar" + activeStar);
        }
    }
}
