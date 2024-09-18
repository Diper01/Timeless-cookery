using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private PlayerStats playerStats;
    private LevelManager levelManager;
    private MapController mapController;
    public LevelSettingsDataManager levelSettingsDataManager;

    public int levelID;
    public int currentStar = 0;

    public bool unlocked = false;

    public UILabel levelText;

    public GameObject lockImg;
    public GameObject unCompleteImg;
    public GameObject completeLevelImg;
    public GameObject oneStarImg;
    public GameObject twoStarImg;
    public GameObject threeStarImg;

    public GameObject[] levelSettings;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        mapController = FindObjectOfType<MapController>();

        _SetDeactiveImage();
        _SetUnlock();

        _SetToLevelSetting();
        _SetBossMode();

        _DeactiveLevelSetting();
        _ActiveLevelSetting();

        _SetText();
        _SetActiveImage();

        _FocusToLastLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void _FocusToLastLevel()
    {
        if(playerStats.lastPlayLevel == levelID)
        {
            mapController.StartCoroutine(mapController._SetScrollViewCenter(transform.localPosition.x, this));
        }
    }

    void _SetToLevelSetting()
    {
        for (int i = 0; i < levelSettings.Length; i++)
        {
            levelSettings[i].GetComponent<LevelSetting>().levelID = levelID;
            levelSettings[i].GetComponent<LevelSetting>().starSet = i;
            for (int j=0; j< levelManager.levels.Length; j++)
            {
                if(levelID == levelManager.levels[j].levelID)
                {
                    currentStar = levelManager.levels[j].star;
                    levelSettings[i].GetComponent<LevelSetting>().currentStar = currentStar;
                }
            }
        }
    }

    void _SetBossMode()
    {
        if(levelSettings[0].GetComponent<LevelSetting>().gameMode == MissionManager.GameMode.Boss)
        {
            levelSettings[1] = levelSettings[0];
            levelSettings[2] = levelSettings[0];
        }
    }

    void _DeactiveLevelSetting()
    {
        for (int i = 0; i < levelSettings.Length; i++)
        {
            levelSettings[i].gameObject.SetActive(false);
        }
    }

    void _ActiveLevelSetting()
    {
        if(currentStar < 3)
        {
            levelSettings[currentStar].gameObject.SetActive(true);
        }
        else if(currentStar == 3)
        {
            levelSettings[2].gameObject.SetActive(true);
        }
    }

    void _SetUnlock()
    {
        if (levelID > mapController.mapDatas[mapController.selectedMap].startID)
        {
            for (int i = 0; i < levelManager.levels.Length; i++)
            {
                if (levelManager.levels[i].levelID == levelID - 1 && levelManager.levels[i].star > 0)
                {
                    unlocked = true;
                    break;
                }
            }
        }

        else if (levelID == mapController.mapDatas[mapController.selectedMap].startID)
        {
            unlocked = true;
        }
    }

    void _SetText()
    {
        levelText.text = levelID.ToString();
    }

    public void _SetActiveImage()
    {
        if (unlocked == false)
        {
            lockImg.SetActive(true);
        }
        else
        if (unlocked == true)
        {
            //if (currentStar == 0)
            //{
            //    unCompleteImg.SetActive(true);
            //}
            //else

            completeLevelImg.SetActive(true);

            if (currentStar > 0)
            {
                //completeLevelImg.SetActive(true);

                if (currentStar == 1 && oneStarImg.activeSelf == false)
                {
                    oneStarImg.SetActive(true);
                }
                else if (currentStar == 2 && twoStarImg.activeSelf == false)
                {
                    twoStarImg.SetActive(true);
                }
                else if (currentStar == 3 && threeStarImg.activeSelf == false)
                {
                    threeStarImg.SetActive(true);
                }
            }
        }
    }

    public void _SetDeactiveImage()
    {
        lockImg.SetActive(false);
        unCompleteImg.SetActive(false);
        completeLevelImg.SetActive(false);

        oneStarImg.SetActive(false);
        twoStarImg.SetActive(false);
        threeStarImg.SetActive(false);
    }

    public void _OnTouch()
    {
        //if(unlocked == true && currentStar < 3)
        if (unlocked == true)
        {
            for (int i = 0; i < levelSettings.Length; i++)
            {
                if (levelSettings[i].gameObject.activeSelf == true)
                {
                    levelSettings[i].GetComponent<LevelSetting>().levelController = this;
                    levelSettings[i].GetComponent<LevelSetting>()._OnButtonDown();
                }
            }
        }
    }
}
