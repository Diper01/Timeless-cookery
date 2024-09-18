using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TopBar : MonoBehaviour
{
    private MissionManager missionManager;

    public TextMeshPro progressBarText;
    public TextMeshPro counterText;
    public Image missionProgressBar;
    public Image timeMissionClockImg;
    public Image customerMissionImg;
    public GameObject openImg;
    public GameObject closeImg;
    public GameObject trashBinImg;
    public GameObject firedFoodImg;
    public GameObject lostCusImg;

    public GameObject coinImg, cusImg, likeImg, dishImg;

    void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();

        timeMissionClockImg.enabled = false;
        customerMissionImg.enabled = false;

        missionProgressBar.fillAmount = 0;

        _SetActiveImage();
        _SetBanMission();
    }

    // Update is called once per frame
    void Update()
    {
        _MissionProgressBar();

        _SetText();

        _SetImgOpenClose();

        _CounterTextTweenScale();
    }

    void _CounterTextTweenScale()
    {
        if(missionManager.firstTarget == MissionManager.FirstTarget.Time )
        {
            if(missionManager.currentTime <= 10 && counterText.gameObject.GetComponent<TweenScale>().enabled == false)
            {
                counterText.gameObject.GetComponent<TweenScale>().enabled = true;
            }
            else if (missionManager.currentTime > 10 && counterText.gameObject.GetComponent<TweenScale>().enabled == true)
            {
                counterText.gameObject.GetComponent<TweenScale>().enabled = false;
                counterText.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else if (missionManager.firstTarget == MissionManager.FirstTarget.Customer)
        {
            if (missionManager.customerLeft <= 3 && counterText.gameObject.GetComponent<TweenScale>().enabled == false)
            {
                counterText.gameObject.GetComponent<TweenScale>().enabled = true;
            }
            else if (missionManager.currentTime > 3 && counterText.gameObject.GetComponent<TweenScale>().enabled == true)
            {
                counterText.gameObject.GetComponent<TweenScale>().enabled = false;
                counterText.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }

    void _SetImgOpenClose()
    {
        if (missionManager.open == true)
        {
            openImg.SetActive(true);
            closeImg.SetActive(false);
        }
        if (missionManager.close == true)
        {
            openImg.SetActive(false);
            closeImg.SetActive(true);
        }
    }

    void _MissionProgressBar()
    {
        if(missionManager.firstTarget == MissionManager.FirstTarget.Time)
        {
            if(missionManager.secondTarget == MissionManager.SecondTarget.Null)
            {
                missionProgressBar.fillAmount = 1;
                progressBarText.text = missionManager.currentPlayerCoin.ToString();

                coinImg.SetActive(true);
                cusImg.SetActive(false);
                likeImg.SetActive(false);
                dishImg.SetActive(false);
            }
            else if(missionManager.secondTarget == MissionManager.SecondTarget.Coin)
            {
                if(missionManager.currentPlayerCoin > missionManager.coinCondition)
                {
                    missionProgressBar.fillAmount = 1;
                }
                else
                {
                    missionProgressBar.fillAmount = (float)missionManager.currentPlayerCoin / (float)missionManager.coinCondition;
                }

                progressBarText.text = missionManager.currentPlayerCoin.ToString() + "/" + missionManager.coinCondition.ToString();

                coinImg.SetActive(true);
                cusImg.SetActive(false);
                likeImg.SetActive(false);
                dishImg.SetActive(false);
            }
            else if(missionManager.secondTarget == MissionManager.SecondTarget.Dish)
            {
                if (missionManager.dishCount > missionManager.dishCondition)
                {
                    missionProgressBar.fillAmount = 1;
                }
                else
                {
                    missionProgressBar.fillAmount = (float)missionManager.dishCount / (float)missionManager.dishCondition;
                }

                progressBarText.text = missionManager.dishCount.ToString() + "/" + missionManager.dishCondition.ToString();

                coinImg.SetActive(false);
                cusImg.SetActive(false);
                likeImg.SetActive(false);
                dishImg.SetActive(true);
            }
            else if (missionManager.secondTarget == MissionManager.SecondTarget.Like)
            {
                if (missionManager.customerGoodEmoCount > missionManager.customerGoodCondition)
                {
                    missionProgressBar.fillAmount = 1;
                }
                else
                {
                    missionProgressBar.fillAmount = (float)missionManager.customerGoodEmoCount / (float)missionManager.customerGoodCondition;
                }

                progressBarText.text = missionManager.customerGoodEmoCount.ToString() + "/" + missionManager.customerGoodCondition.ToString();

                coinImg.SetActive(false);
                cusImg.SetActive(false);
                likeImg.SetActive(true);
                dishImg.SetActive(false);
            }
        }
        else if (missionManager.firstTarget == MissionManager.FirstTarget.Customer)
        {
            if (missionManager.secondTarget == MissionManager.SecondTarget.Null)
            {
                missionProgressBar.fillAmount = 1;
                progressBarText.text = missionManager.customerPaidCount.ToString();

                coinImg.SetActive(false);
                cusImg.SetActive(true);
                likeImg.SetActive(false);
                dishImg.SetActive(false);
            }
            else if (missionManager.secondTarget == MissionManager.SecondTarget.Coin)
            {
                if (missionManager.currentPlayerCoin > missionManager.coinCondition)
                {
                    missionProgressBar.fillAmount = 1;
                }
                else
                {
                    missionProgressBar.fillAmount = (float)missionManager.currentPlayerCoin / (float)missionManager.coinCondition;
                }

                progressBarText.text = missionManager.currentPlayerCoin.ToString() + "/" + missionManager.coinCondition.ToString();

                coinImg.SetActive(true);
                cusImg.SetActive(false);
                likeImg.SetActive(false);
                dishImg.SetActive(false);
            }
            else if (missionManager.secondTarget == MissionManager.SecondTarget.Dish)
            {
                if (missionManager.dishCount > missionManager.dishCondition)
                {
                    missionProgressBar.fillAmount = 1;
                }
                else
                {
                    missionProgressBar.fillAmount = (float)missionManager.dishCount / (float)missionManager.dishCondition;
                }

                progressBarText.text = missionManager.dishCount.ToString() + "/" + missionManager.dishCondition.ToString();

                coinImg.SetActive(false);
                cusImg.SetActive(false);
                likeImg.SetActive(false);
                dishImg.SetActive(true);
            }
            else if (missionManager.secondTarget == MissionManager.SecondTarget.Like)
            {
                if (missionManager.customerGoodEmoCount > missionManager.customerGoodCondition)
                {
                    missionProgressBar.fillAmount = 1;
                }
                else
                {
                    missionProgressBar.fillAmount = (float)missionManager.customerGoodEmoCount / (float)missionManager.customerGoodCondition;
                }

                progressBarText.text = missionManager.customerGoodEmoCount.ToString() + "/" + missionManager.customerGoodCondition.ToString();

                coinImg.SetActive(false);
                cusImg.SetActive(false);
                likeImg.SetActive(true);
                dishImg.SetActive(false);
            }
        }
    }

    void _SetActiveImage()
    {
        if(missionManager.firstTarget == MissionManager.FirstTarget.Time)
        {
            timeMissionClockImg.enabled = true;
        }
        else if(missionManager.firstTarget == MissionManager.FirstTarget.Customer)
        {
            customerMissionImg.enabled = true;
        }
    }

    void _SetText()
    {
        if (missionManager.firstTarget == MissionManager.FirstTarget.Time)
        {
            int minutes = (int)missionManager.currentTime /60;
            int seconds = (int)missionManager.currentTime % 60;
            counterText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        if (missionManager.firstTarget == MissionManager.FirstTarget.Customer)
        {
            counterText.text = missionManager.customerLeft.ToString();
        }
    }

    void _SetBanMission()
    {
        if (missionManager.foodToTrashMission)
        {
            trashBinImg.SetActive(true);
        }
        if (missionManager.friedFoodMission)
        {
            firedFoodImg.SetActive(true);
        }
        if (missionManager.lostCutomerMission)
        {
            lostCusImg.SetActive(true);
        }
    }
}
