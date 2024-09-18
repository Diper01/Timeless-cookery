using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnoughMoneyPopup : MonoBehaviour
{
    private PlayerStats playerStats;
    private ItemManager itemManager;

    public string id = "";
    public int coinPrice = 0;
    public int crystalPrice = 0;
    public UILabel coinText;
    public UILabel crystalText;
    public GrillUpgrade grillUpgrade = null;
    public PlateUpgrade plateUpgrade = null;
    public MainFoodUpgrade mainFoodUpgrade = null;
    public SideFoodUpgrade sideFoodUpgrade = null;

    public AudioSource coinFX;

    public GameObject blackBG;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        itemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
    }

    private void Update()
    {
        coinText.text = playerStats.coin.ToString();
        crystalPrice = (coinPrice - playerStats.coin) / 10;
        crystalText.text = crystalPrice.ToString();

        _ActiveBlackBG();
    }

    void _ActiveBlackBG()
    {
        if (gameObject.activeSelf == true && blackBG.activeSelf == false)
        {
            blackBG.SetActive(true);
        }
    }

    public void _SetNull()
    {
        grillUpgrade = null;
        plateUpgrade = null;
        mainFoodUpgrade = null;
        sideFoodUpgrade = null;
    }

    public void _Upgrade()
    {
        if (playerStats.crystal >= crystalPrice)
        {
            if (grillUpgrade != null)
            {
                for (int i = 0; i < itemManager.grillsDatas.Length; i++)
                {
                    if (itemManager.grillsDatas[i].itemId == id)
                    {
                        itemManager.grillsDatas[i].currentLevel += 1;
                        itemManager.save = true;

                        _Pay();
                        grillUpgrade._Active();
                        _Close();

                        return;
                    }
                }
            }
            else
            if (plateUpgrade != null)
            {
                for (int i = 0; i < itemManager.platesDatas.Length; i++)
                {
                    if (itemManager.platesDatas[i].itemId == id)
                    {
                        itemManager.platesDatas[i].currentLevel += 1;
                        itemManager.save = true;

                        _Pay();
                        plateUpgrade._Active();
                        _Close();

                        return;
                    }
                }
            }
            else
            if (mainFoodUpgrade != null)
            {
                for (int i = 0; i < itemManager.mainFoodsDatas.Length; i++)
                {
                    if (itemManager.mainFoodsDatas[i].itemId == id)
                    {
                        itemManager.mainFoodsDatas[i].currentLevel += 1;
                        itemManager.save = true;

                        _Pay();
                        mainFoodUpgrade._Active();
                        _Close();

                        return;
                    }
                }
            }
            else
            if (sideFoodUpgrade != null)
            {
                for (int i = 0; i < itemManager.sideFoodsDatas.Length; i++)
                {
                    if (itemManager.sideFoodsDatas[i].itemId == id)
                    {
                        itemManager.sideFoodsDatas[i].currentLevel += 1;
                        itemManager.save = true;

                        _Pay();
                        sideFoodUpgrade._Active();
                        _Close();

                        return;
                    }
                }
            }
        }

        else if(playerStats.crystal < crystalPrice)
        {
            _Close();
        }
    }

    void _Pay()
    {
        playerStats.DecreaseCoin(playerStats.coin);
        playerStats.DecreaseCrystal(crystalPrice);

        coinFX.Play();
    }

    public void _Close()
    {
        blackBG.SetActive(false);

        id = "";
        _SetNull();

        gameObject.SetActive(false);
    }
}
