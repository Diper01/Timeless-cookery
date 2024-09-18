using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPackage : MonoBehaviour
{
    private PlayerStats playerStats;

    public UILabel priceText;

    private bool priceLoaded = false;

    public GameObject packageBuyConfirm;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();

    }

    // Update is called once per frame

    public void _DeactivePackageBuyConfirm()
    {
        packageBuyConfirm.SetActive(false);
    }

    public void _OnButton()
    {
        packageBuyConfirm.SetActive(true);
    }

 
}
