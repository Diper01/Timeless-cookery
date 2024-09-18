using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdTargetObject : MonoBehaviour
{
    private MissionManager missionManager;

    public GameObject lostCusImg, firedFoodImg, trashFoodImg;

    // Start is called before the first frame update
    void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void _DeactiveImage()
    {
        lostCusImg.SetActive(false);
        firedFoodImg.SetActive(false);
        trashFoodImg.SetActive(false);
    }

    public void _SetActiveImage()
    {
        _DeactiveImage();

        if (missionManager.lostCutomerMission)
        {
            lostCusImg.SetActive(true);
        }
        if (missionManager.friedFoodMission)
        {
            firedFoodImg.SetActive(true);
        }
        if (missionManager.foodToTrashMission)
        {
            trashFoodImg.SetActive(true);
        }
    }
}
