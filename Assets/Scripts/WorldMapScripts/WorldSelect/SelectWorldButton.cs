using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectWorldButton : MonoBehaviour
{
    private PlayerStats playerStats;
    private MapController mapController;
    private LevelManager levelManager;

    public SelectWorldPopup selectWorldPopup;
    public UnlockWorldPopup unlockWorldPopup;

    public int sortIndex = 0;
    public int unlockStar = 0;
    public int crystalReward = 0;
    public GameObject lockContent;
    public UILabel lockInfoText;
    public UILabel currentStarText;
    public bool unlocked = false;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        mapController = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapController>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    private void Update()
    {
        _SetUnlockedWorlds();
        _LockContent();
        _SetText();
    }

    void _SetText()
    {
        if(unlocked == false)
        {
            lockInfoText.text = String.Format("You need {0:00} stars to unlock this map", unlockStar);

            if(playerStats.unlockedWorlds < sortIndex)
            {
                currentStarText.text = String.Format("{0:00}", levelManager.totalStar);
            }
            else
            {
                currentStarText.text = "00";
            }
        }
    }

    void _LockContent()
    {
        if(unlocked == true && lockContent.activeSelf == true)
        {
            lockContent.SetActive(false);
        }
        else if(unlocked == false && lockContent.activeSelf == false)
        {
            lockContent.SetActive(true);
        }
    }

    void _SetUnlockedWorlds()
    {
        if(playerStats.unlockedWorlds >= sortIndex)
        {
            unlocked = true;
        }
    }

    public void _OnTouch()
    {
        if (unlocked == true)
        {
            mapController.selectedMap = sortIndex;
            playerStats.currentWorld = sortIndex;
            playerStats.save = true;
            mapController.change = true;
            FindObjectOfType<WorldMapController>().closeDialogWorldSelect();
            GameObject fading = GameObject.Find("fadeIn");
            fading.GetComponent<TweenAlpha>().ResetToBeginning();
            fading.GetComponent<TweenAlpha>().PlayForward();
        }
        //else if(unlocked == false)
        //{
        //    unlockWorldPopup.SetActive(true);
        //    unlockWorldPopup.GetComponent<UnlockWorldPopup>().index = sortIndex;
        //    unlockWorldPopup.GetComponent<UnlockWorldPopup>().unlockStar = unlockStar;

        //    FindObjectOfType<WorldMapController>().closeDialogWorldSelect();
        //}
    }
}
