using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWorldPopup : MonoBehaviour
{
    private PlayerStats playerStats;
    private LevelManager levelManager;

    public MapController mapController;

    public SelectWorldPopup selectWorldPopup;

    public GameObject[] lockContent;

    public GameObject unlockButton;

    public UILabel crystalAmoutText; 

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void _ActiveLockContent()
    {
        for (int i = 0; i < lockContent.Length; i++)
        {
            lockContent[i].SetActive(false);
        }

        if(lockContent[mapController.selectedMap]!= null)
        {
            lockContent[mapController.selectedMap].SetActive(true);
        }

        if(selectWorldPopup.selectWorldButtons[mapController.selectedMap + 1] != null)
        {
            crystalAmoutText.text = selectWorldPopup.selectWorldButtons[mapController.selectedMap + 1].crystalReward.ToString();
        }

        unlockButton.SetActive(false);

        levelManager = FindObjectOfType<LevelManager>();

        if (selectWorldPopup.selectWorldButtons[mapController.selectedMap + 1] != null
            && selectWorldPopup.selectWorldButtons[mapController.selectedMap + 1].unlockStar <= levelManager.totalStar
            && mapController.mapDatas[mapController.selectedMap + 1] != null)
        {
            unlockButton.SetActive(true);
        }
    }

    public void _UnlockButton()
    {
        mapController.selectedMap = playerStats.unlockedWorlds + 1;
        mapController.change = true;

        playerStats.unlockedWorlds += 1;

        playerStats.crystal += selectWorldPopup.selectWorldButtons[playerStats.unlockedWorlds].crystalReward;

        playerStats._SaveData();

        _Close();
    }

    public void _Close()
    {
        gameObject.SetActive(false);
    }
}
