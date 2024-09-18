using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShopGuideManager : MonoBehaviour
{
    private MissionManager missionManager;
    private LevelManager levelManager;
    private PlayerStats playerStats;

    public UpgradeShopGuideController[] upgradeShopGuideControllers;

    // Start is called before the first frame update
    void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
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
        if (playerStats.tutUpgrade == false)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator _SetActiveGuide()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < upgradeShopGuideControllers.Length; i++)
        {
            if (upgradeShopGuideControllers[i].activeLevel > 1
                && levelManager.levels[upgradeShopGuideControllers[i].activeLevel - 1].levelCompleted == true
                && levelManager.levels[upgradeShopGuideControllers[i].activeLevel - 1].star == upgradeShopGuideControllers[i].activeStar)
            {
                upgradeShopGuideControllers[i].gameObject.SetActive(true);
            }
            else if (upgradeShopGuideControllers[i].activeLevel == 1 
                && levelManager.levels[0].star == upgradeShopGuideControllers[i].activeStar)
            {
                upgradeShopGuideControllers[i].gameObject.SetActive(true);
            }
        }
    }
}
