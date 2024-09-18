using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShopTopBar : MonoBehaviour
{
    private PlayerStats playerStats;
    private NextScene nextScene;
    private ItemManager itemManager;
    private MissionManager missionManager;

    public string sceneName = "WorldMap";

    public UILabel coinText;
    public UILabel crystalText;

    public void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        nextScene = GameObject.FindGameObjectWithTag("NextScene").GetComponent<NextScene>();
        itemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();

        Time.timeScale = 1;
    }

    private void Update()
    {
        _SetText();
    }

    void _SetText()
    {
        coinText.text = playerStats.coin.ToString();
        crystalText.text = playerStats.crystal.ToString();
    }

    public void _BuyCrystalButton()
    {

    }

    public void _ContinueButton()
    {
        itemManager.save = true;

        missionManager.scenePos = 0;
        nextScene.NextLevel(sceneName);
    }
}
