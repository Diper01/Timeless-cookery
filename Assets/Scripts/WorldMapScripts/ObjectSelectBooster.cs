using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelectBooster : MonoBehaviour
{
    public enum StartBoosterType
    {
        mushroom, water, x2gold
    }

    public StartBoosterType boosterType
    {

        get { return m_boosterType; }
        set
        {
            m_boosterType = value;
            initBoosterType();
        }

    }
    public StartBoosterType m_boosterType;

    private MissionManager missionManager;
    private PlayerStats playerStats;

    public bool selected = false;
    public GameObject iconImg, checkImg;
    public UILabel amountText; 
 
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
    }

    void Update()
    {
        if(gameObject.activeSelf == true)
        {
            initBoosterType();
        }
    }

    public void initBoosterType()
    {
        switch (m_boosterType)
        {
            case StartBoosterType.mushroom:
                iconImg.GetComponent<UISprite>().spriteName = "mushroom";

                amountText.text = playerStats.mushroomAmount.ToString();
                break;

            case StartBoosterType.water:
                iconImg.GetComponent<UISprite>().spriteName = "water";

                amountText.text = playerStats.waterAmount.ToString();
                break;

            case StartBoosterType.x2gold:
                iconImg.GetComponent<UISprite>().spriteName = "item store";

                amountText.text = playerStats.x2GoldAmount.ToString();
                break;

        }

        iconImg.GetComponent<UISprite>().MakePixelPerfect();
        iconImg.transform.localScale = new Vector3(0.5f, 0.5f, 0);
    }

    public void _OnBoosterButton()
    {
        if (selected)
        {
            switch (m_boosterType)
            {
                case StartBoosterType.mushroom:
                    if (playerStats.mushroomAmount > 0)
                    {
                        iconImg.GetComponent<UISprite>().spriteName = "mushroom";
                        missionManager.useMushRoom = false;
                        checkImg.SetActive(false);
                    }
                    break;

                case StartBoosterType.water:
                    if (playerStats.waterAmount > 0)
                    {
                        iconImg.GetComponent<UISprite>().spriteName = "water";
                        missionManager.useWater = false;
                        checkImg.SetActive(false);
                    }
                    break;

                case StartBoosterType.x2gold:
                    if (playerStats.x2GoldAmount > 0)
                    {
                        iconImg.GetComponent<UISprite>().spriteName = "item store";
                        missionManager.useX2Gold = false;
                        checkImg.SetActive(false);
                    }
                    break;
            }
        }
        else
        {
            switch (m_boosterType)
            {
                case StartBoosterType.mushroom:
                    if (playerStats.mushroomAmount > 0)
                    {
                        iconImg.GetComponent<UISprite>().spriteName = "mushroom";
                        missionManager.useMushRoom = true;
                        checkImg.SetActive(true);
                    }
                    break;

                case StartBoosterType.water:
                    if (playerStats.waterAmount > 0)
                    {
                        iconImg.GetComponent<UISprite>().spriteName = "water";
                        missionManager.useWater = true;
                        checkImg.SetActive(true);
                    }
                    break;

                case StartBoosterType.x2gold:
                    if (playerStats.x2GoldAmount > 0)
                    {
                        iconImg.GetComponent<UISprite>().spriteName = "item store";
                        missionManager.useX2Gold = true;
                        checkImg.SetActive(true);
                    }
                    break;

            }
        }
        selected = !selected;
    }
}
