using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private MissionManager missionManager;
    private LevelManager levelManager;

    public Text levelText;

    public GameObject quitMissionMenu;

    private void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();

        levelText.text ="LEVEL " + missionManager.levelID;
    }

    private void Update()
    {
        if(gameObject.activeSelf == true && Time.timeScale == 1)
        {
            FindObjectOfType<AudioManager>()._Pause();
            Time.timeScale = 0;
        }
    }

    public void _ContinueButton()
    {
        Time.timeScale = 1;
        FindObjectOfType<AudioManager>()._UnPause();
        gameObject.SetActive(false);
    }

    public void _Exit()
    {
        quitMissionMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
