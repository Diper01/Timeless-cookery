using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankGO : MonoBehaviour
{
    public int sortIndex = 0;

    public GameObject bronzeCup, silverCup, goldCup;

    public UILabel userNamelbl, goldLbl, rankLbl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _SetRank()
    {
        if (sortIndex == 1)
        {
            goldCup.SetActive(true);
            silverCup.SetActive(false);
            bronzeCup.SetActive(false);
            rankLbl.gameObject.SetActive(false);
        }
        else if (sortIndex == 2)
        {
            goldCup.SetActive(false);
            silverCup.SetActive(true);
            bronzeCup.SetActive(false);
            rankLbl.gameObject.SetActive(false);
        }
        else if (sortIndex == 3)
        {
            goldCup.SetActive(false);
            silverCup.SetActive(false);
            bronzeCup.SetActive(true);
            rankLbl.gameObject.SetActive(false);
        }
        else
        {
            goldCup.SetActive(false);
            silverCup.SetActive(false);
            bronzeCup.SetActive(false);
            rankLbl.gameObject.SetActive(true);

            rankLbl.text = sortIndex.ToString() + "th";
        }
    }
}
