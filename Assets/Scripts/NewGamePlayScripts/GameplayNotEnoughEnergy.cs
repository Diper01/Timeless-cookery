using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayNotEnoughEnergy : MonoBehaviour
{
    //private AdManager adManager;

    public GamePlayMenuManager gamePlayMenuManager;

    // Start is called before the first frame update
    void Start()
    {
        //adManager = FindObjectOfType<AdManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _GetEnergy()
    {
        //adManager.rewardType = AdManager.REWARD_TYPE.Energy;
        //adManager.rewardAmount = 1;
        //adManager.ShowRewardAd(AdManager.REWARD_AD_PLACEMENT.GAME_OVER);
        _Close();
    }

    public void _Close()
    {
        gamePlayMenuManager.loseMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
