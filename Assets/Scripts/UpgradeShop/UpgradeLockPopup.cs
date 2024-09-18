using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeLockPopup : MonoBehaviour
{
    private PlayerStats playerStats;

    public UILabel levelText;
    public int level;

    public GameObject blackBG;

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
        _SetText();
        _ActiveBlackBG();
    }

    void _SetText()
    {
        levelText.text = "Level " + level.ToString();
    }

    public void _OkButton()
    {
        blackBG.SetActive(false);

        level = 0;
        gameObject.SetActive(false);
    }

    void _ActiveBlackBG()
    {
        if (gameObject.activeSelf == true && blackBG.activeSelf == false)
        {
            blackBG.SetActive(true);
        }
    }
}
