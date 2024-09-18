using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoseTargetObject : MonoBehaviour
{
    private MissionManager missionManager;
    private GamePlayMenuManager gamePlayMenuManager;

    public enum TargetType { NULL, CUSTOMER, TIME, COIN, DISH, LIKE}

    public TargetType targetType = TargetType.NULL;

    public TextMeshPro targetText;

    public GameObject bg, cusImg, timeImg, coinImg, dishImg, likeImg;
    public GameObject ticker, xTicker;

    // Start is called before the first frame update
    void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        gamePlayMenuManager = GameObject.FindGameObjectWithTag("GamePlayMenuManager").GetComponent<GamePlayMenuManager>();

        _DeactiveImage();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void _DeactiveImage()
    {
        bg.SetActive(false);
        coinImg.SetActive(false);
        cusImg.SetActive(false);
        timeImg.SetActive(false);
        dishImg.SetActive(false);
        likeImg.SetActive(false);
        ticker.SetActive(false);
        xTicker.SetActive(false);
    }

    public void _SetActiveImage()
    {
        _DeactiveImage();

        if (targetType == TargetType.NULL)
        {
            targetText.gameObject.SetActive(false);
        }
        else if(targetType != TargetType.NULL)
        {
            bg.SetActive(true);

            if (targetType == TargetType.CUSTOMER)
            {
                cusImg.SetActive(true);
                targetText.gameObject.SetActive(true);
                targetText.text = missionManager.customerCount.ToString() + "/" + missionManager.customerLimit.ToString();
            }
            else if (targetType == TargetType.TIME)
            {
                timeImg.SetActive(true);
                targetText.gameObject.SetActive(true);
                int minutes = (int)missionManager.missionTimeLimit / 60;
                int seconds = (int)missionManager.missionTimeLimit % 60;
                targetText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else if (targetType == TargetType.COIN)
            {
                coinImg.SetActive(true);

                if(missionManager.currentPlayerCoin < missionManager.coinCondition)
                {
                    targetText.gameObject.SetActive(true);
                    targetText.text = missionManager.currentPlayerCoin.ToString() + "/" + missionManager.coinCondition.ToString();
                }
                else
                {
                    ticker.SetActive(true);
                }
            }
            else if (targetType == TargetType.DISH)
            {
                dishImg.SetActive(true);

                if(missionManager.dishCount < missionManager.dishCondition)
                {
                    targetText.gameObject.SetActive(true);
                    targetText.text = missionManager.dishCount.ToString() + "/" + missionManager.dishCondition.ToString();
                }
                else
                {
                    ticker.SetActive(true);
                }
            }
            else if (targetType == TargetType.LIKE)
            {
                likeImg.SetActive(true);

                if(missionManager.customerGoodEmoCount < missionManager.customerGoodCondition)
                {
                    targetText.gameObject.SetActive(true);
                    targetText.text = missionManager.customerGoodEmoCount.ToString() + "/" + missionManager.customerGoodCondition.ToString();
                }
                else
                {
                    ticker.SetActive(true);
                }
            }
        }
    }

    public void _Close()
    {
        _DeactiveImage();
        targetType = TargetType.NULL;
    }
}
