using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InsurgencyPackageMenu : MonoBehaviour
{
    private PlayerStats playerStats;

    public GamePlayMenuManager gamePlayMenuManager;
    public InsurgencyPackConfirmMenu insurgencyPackConfirmMenu;


    public GameplayNotEnoughCrystalMenu.GP_MENU_TYPE gpMenuType;

    private bool priceLoaded = false;

    public TextMeshProUGUI amountText;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI promoText;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();


        if (playerStats.insurgenPurchased == true)
        {
            amountText.text = "100";
            promoText.gameObject.SetActive(false);
        }
        else if (playerStats.insurgenPurchased == false)
        {
            amountText.text = "200";
            promoText.gameObject.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
    }

    public void _CloseButton()
    {
        if (gpMenuType == GameplayNotEnoughCrystalMenu.GP_MENU_TYPE.OUT_TIME)
        {
            gamePlayMenuManager.outOfTimeMenu.SetActive(true);
        }
        else if (gpMenuType == GameplayNotEnoughCrystalMenu.GP_MENU_TYPE.OUT_CUS)
        {
            gamePlayMenuManager.outOfCustomerMenu.SetActive(true);
        }
        else if (gpMenuType == GameplayNotEnoughCrystalMenu.GP_MENU_TYPE.LOST_CUS)
        {
            gamePlayMenuManager.lostCustomerMenu.SetActive(true);
        }
        else if (gpMenuType == GameplayNotEnoughCrystalMenu.GP_MENU_TYPE.FIRED_FOOD)
        {
            gamePlayMenuManager.friedFoodMenu.SetActive(true);
        }
        else if (gpMenuType == GameplayNotEnoughCrystalMenu.GP_MENU_TYPE.TRASH_FOOD)
        {
            gamePlayMenuManager.foodTrashMenu.SetActive(true);
        }

        gameObject.SetActive(false);
    }

    public void _ButtonOn()
    {
        insurgencyPackConfirmMenu.gameObject.SetActive(true);
        insurgencyPackConfirmMenu.insurgencyPackageMenu = gameObject.GetComponent<InsurgencyPackageMenu>();
        transform.localScale = new Vector3(0f, 0f, 0f);
    }
    
}
