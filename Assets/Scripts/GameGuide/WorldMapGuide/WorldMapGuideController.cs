using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapGuideController : MonoBehaviour
{
    private LevelManager levelManager;
    //private GoogleFireBaseEvents googleFireBaseEvents;

    public int activeWorld = 0;
    public int guideSort = 0;
    public int activeLevel = 0;
    public int activeStar = 0;

    public GameObject[] guideSteps;

    public int currentStep = 0;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        //googleFireBaseEvents = FindObjectOfType<GoogleFireBaseEvents>();

        guideSteps[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void _NextStep()
    {
        currentStep++;

        if (currentStep < guideSteps.Length)
        {
            if (guideSteps[currentStep - 1] != null)
            {
                guideSteps[currentStep - 1].SetActive(false);
            }

            guideSteps[currentStep].SetActive(true);
        }
        else
        if (currentStep == guideSteps.Length)
        {
            guideSteps[currentStep-1].SetActive(false);
            levelManager.wmGuideSort += 1;
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
