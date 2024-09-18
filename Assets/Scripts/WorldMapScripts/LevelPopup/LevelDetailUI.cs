using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class LevelDetailUI : MonoBehaviour
{
    private NextScene nextScene;
    private MissionManager missionManager;
    private MapController mapController;
    private LevelManager levelManager;
    private PlayerStats playerStats;

    //private GoogleFireBaseEvents googleFireBaseEvents;

    public string sceneName = "GamePlay";

    //public GameObject keyImg;

    public int star = 0;
    public UILabel levelText;

    public GameObject[] stars;

    public GameObject smallBoard;
    public bool sBoardActive = false;

    public GameObject firstTarget;
    public GameObject secondTarget;
    public GameObject thirdTarget;

    public GameObject waterBooster;
    public GameObject mushRoomBooster;
    public GameObject x2GoldBooster;

    public GameObject bossPanel;

    private void Start()
    {
        nextScene = GameObject.FindGameObjectWithTag("NextScene").GetComponent<NextScene>();
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        mapController = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapController>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();

        //googleFireBaseEvents = FindObjectOfType<GoogleFireBaseEvents>();

        smallBoard.GetComponent<TweenPosition>().ResetToBeginning();

        _PopupSound();
    }

    private void Update()
    {
        _SetStarRate();
        _SetLevelText();
        _PopupSound();

        if(gameObject.transform.localScale.x == 1 && sBoardActive == false)
        {
            sBoardActive = true;
            _ActiveBooster();
            _ActiveSmallBoard();
        }
    }

    public void _ActiveBooster()
    {
        if(playerStats.waterActivated == false)
        {
            waterBooster.SetActive(false);
        }
        else if (playerStats.waterActivated == true)
        {
            waterBooster.SetActive(true);
        }

        if (playerStats.mushroomActivated == false)
        {
            mushRoomBooster.SetActive(false);
        }
        else if (playerStats.mushroomActivated == true)
        {
            mushRoomBooster.SetActive(true);
        }

        if (playerStats.x2GoldActivated == false)
        {
            x2GoldBooster.SetActive(false);
        }
        else if (playerStats.x2GoldActivated == true)
        {
            x2GoldBooster.SetActive(true);
        }

        _SetBoosterGameMode();
    }

    public void _SetBoosterGameMode()
    {
        if(missionManager.gamemode == MissionManager.GameMode.Boss)
        {
            waterBooster.SetActive(false);
            x2GoldBooster.SetActive(false);
            mushRoomBooster.SetActive(false);

            bossPanel.SetActive(true);
        }
        else
        {
            bossPanel.SetActive(false);
        }
    }

    public void _PlayButton()
    {
        // Зберігаємо останній рівень, на якому грав гравець
        playerStats.lastPlayLevel = missionManager.levelID;

        // Налаштовуємо місце розташування сцени
        missionManager.scenePos = 1;

        // Визначаємо ім'я сцени залежно від ігрового режиму
        if (missionManager.gamemode == MissionManager.GameMode.Boss)
        {
            sceneName = levelManager.mapName.ToString() + "_Boss";
        }
        else
        {
            sceneName = levelManager.mapName.ToString() + "_GamePlay";
        }

        // Переходимо до наступного рівня
        nextScene.NextLevel(sceneName);
    }


    public void _CloseButton()
    {
        smallBoard.GetComponent<TweenPosition>().enabled = false;
        smallBoard.GetComponent<TweenPosition>().ResetToBeginning();
        sBoardActive = false;

        gameObject.GetComponent<UIScaleAnimation>().enabled = false;
        gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    void _SetStarRate()
    {
        if (missionManager.currentStar > 0)
        {
            _DeactiveImage();
            for (int i = 0; i < star; i++)
            {
                stars[i].SetActive(true);
            }
        }
        else if (missionManager.currentStar == 0)
        {
            _DeactiveImage();
        }
    }

    void _SetLevelText()
    {
        levelText.text = "LEVEL"+" - "+missionManager.levelID.ToString();
    }

    void _DeactiveImage()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }
    }

    public void _ActiveTarget()
    {
        firstTarget.GetComponent<FirstTargetObject>()._DeactiveImage();
        secondTarget.GetComponent<SecondTargetObject>()._DeactiveImage();
        thirdTarget.GetComponent<ThirdTargetObject>()._DeactiveImage();
    }

    void _PopupSound()
    {
        if(gameObject.transform.localScale.x >= 0.1f && gameObject.transform.localScale.x < 0.9f)
        {
            gameObject.GetComponent<AudioSource>().enabled = true;
        }
        else if(gameObject.transform.localScale.x >= 1f)
        {
            gameObject.GetComponent<AudioSource>().enabled = false;
        }
    }

    void _ActiveSmallBoard()
    {
        smallBoard.GetComponent<TweenPosition>().ResetToBeginning();
        smallBoard.GetComponent<TweenPosition>().enabled = true;
        smallBoard.GetComponent<TweenPosition>().PlayForward();
    }

    public void ClickUpgradeLevelDetail()
    {
        //googleFireBaseEvents.ClickUpgradeLevelDetail();
    }
}
