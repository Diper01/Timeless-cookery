using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogFreeGift : MonoBehaviour
{
    private PlayerStats playerStats;

    public FoxBalloon foxBalloon;

    public REWARD_TYPE rewardType = REWARD_TYPE.Null;

    public GameObject coinImg, energyImg, crystalImg;

    public int rewardAmount;
    public UILabel rewardAmountText;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        _ActiveImgAndText();
    }

    public void _ActiveImgAndText()
    {
        coinImg.SetActive(false);
        energyImg.SetActive(false); 
        crystalImg.SetActive(false);
        // Fox baloon coin reward
        if (rewardType == REWARD_TYPE.Coin)
        {
            coinImg.SetActive(true);
            //rewardAmountText.text = Localisation.GetString("You got % Coin").Replace("%", rewardAmount.ToString());
        }
        else if (rewardType == REWARD_TYPE.Energy)
        {
            energyImg.SetActive(true);
            //rewardAmountText.text = Localisation.GetString("You got % Energy").Replace("%", rewardAmount.ToString());
        }
        else if (rewardType == REWARD_TYPE.Crystal)
        {
            crystalImg.SetActive(true);
            //rewardAmountText.text = Localisation.GetString("You got % Crystal").Replace("%", rewardAmount.ToString());
        }
        // Reward coin button in world map
        else if (rewardType == REWARD_TYPE.freeCoin)
        {
            coinImg.SetActive(true);
            //rewardAmountText.text = Localisation.GetString("You got % Coin").Replace("%", rewardAmount.ToString());
        }
    }

    public void OkButton()
    {
        //rewardType = AdManager.REWARD_TYPE.Null;
        foxBalloon.claimed = true;
        foxBalloon.StartAnimationPop();
        gameObject.SetActive(false);
    }
}

public enum REWARD_TYPE
{
    Null,
    Coin,
    Energy,
    Crystal,
    freeCoin
}
