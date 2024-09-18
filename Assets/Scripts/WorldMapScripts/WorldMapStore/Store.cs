using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    private PlayerStats playerStats;

    public int storeToOpen = 0;
    public GameObject buttonBundle, buttonBoost, buttonDiamond, buttonGold;
    public GameObject bundlePage, boostPage, diamondPage, goldPage;

    public UILabel coinText, crystalText, energyText;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();

        openBundlePage();

    }

    private void Update()
    {
        _SetText();
    }

    void _SetText()
    {
        coinText.text = playerStats.coin.ToString();
        crystalText.text = playerStats.crystal.ToString();
        //energyText.text = playerStats.energy.ToString();
    }

    public void openBundlePage()
    {
        bundlePage.gameObject.SetActive(true);
        boostPage.gameObject.SetActive(false);
        diamondPage.gameObject.SetActive(false);
        goldPage.gameObject.SetActive(false);

        buttonBundle.SetActive(false);
        buttonBoost.SetActive(true);
        buttonDiamond.SetActive(true);
        buttonGold.SetActive(true);
    }

    public void openBoostPage()
    {
        bundlePage.gameObject.SetActive(false);
        boostPage.gameObject.SetActive(true);
        diamondPage.gameObject.SetActive(false);
        goldPage.gameObject.SetActive(false);

        buttonBundle.SetActive(true);
        buttonBoost.SetActive(false);
        buttonDiamond.SetActive(true);
        buttonGold.SetActive(true);
    }

    public void openDiamondPage()
    {
        bundlePage.gameObject.SetActive(false);
        boostPage.gameObject.SetActive(false);
        diamondPage.gameObject.SetActive(true);
        goldPage.gameObject.SetActive(false);

        buttonBundle.SetActive(true);
        buttonBoost.SetActive(true);
        buttonDiamond.SetActive(false);
        buttonGold.SetActive(true);
    }

    public void openGoldPage()
    {
        bundlePage.gameObject.SetActive(false);
        boostPage.gameObject.SetActive(false);
        diamondPage.gameObject.SetActive(false);
        goldPage.gameObject.SetActive(true);

        buttonBundle.SetActive(true);
        buttonBoost.SetActive(true);
        buttonDiamond.SetActive(true);
        buttonGold.SetActive(false);
    }
}
