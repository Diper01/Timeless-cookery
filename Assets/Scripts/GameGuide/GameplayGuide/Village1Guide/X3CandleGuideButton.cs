using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X3CandleGuideButton : MonoBehaviour
{
    private PlayerStats playerStats;
    public InGameBoosterController inGameBoosterController;
    public X3CandleBooster x3CandleBooster;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        StartCoroutine(ActiveBooster());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ActiveBooster()
    {
        yield return new WaitForSeconds(0.25f);
        playerStats.x3CandleActivated = true;

        if(playerStats.x3CandleAmount < 1)
        {
            playerStats.x3CandleAmount = 1;
        }

        inGameBoosterController._SetActive();
    }
}
