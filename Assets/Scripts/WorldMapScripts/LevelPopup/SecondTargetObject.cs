using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTargetObject : MonoBehaviour
{
    private MissionManager missionManager;

    public UILabel coinText, dishText, likeText;

    public GameObject nullImg, coinImg, dishImg, likeImg;

    // Start is called before the first frame update
    void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void _DeactiveImage()
    {
        nullImg.SetActive(false);
        coinImg.SetActive(false);
        dishImg.SetActive(false);
        likeImg.SetActive(false);
    }

    public void _SetActiveImage()
    {
        _DeactiveImage();

        if(missionManager.secondTarget == MissionManager.SecondTarget.Null)
        {
            nullImg.SetActive(true);
        }
        else if (missionManager.secondTarget == MissionManager.SecondTarget.Coin)
        {
            coinImg.SetActive(true);
            coinText.text = missionManager.coinCondition.ToString();

        }
        else if (missionManager.secondTarget == MissionManager.SecondTarget.Dish)
        {
            dishImg.SetActive(true);
            dishText.text = missionManager.dishCondition.ToString();
        }
        else if (missionManager.secondTarget == MissionManager.SecondTarget.Like)
        {
            likeImg.SetActive(true);
            likeText.text = missionManager.customerGoodCondition.ToString();
        }
    }
}
