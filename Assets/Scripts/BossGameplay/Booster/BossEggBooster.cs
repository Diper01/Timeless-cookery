using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEggBooster : MonoBehaviour
{
    private PlayerStats playerStats;

    public AudioSource boosterAudio;
    public Text amountText;

    private bool isCoolDown = false;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();


    }

    private void Update()
    {
        _SetText();
    }

    void _OnTouch()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit hitInfo;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out hitInfo))
        //    {
        //        if (hitInfo.transform.gameObject.name == gameObject.name)
        //        {
        //            if (playerStats.eggAmount > 0 && isCoolDown == false)
        //            {
        //                playerStats.eggAmount -= 1;
        //                playerStats.save = true;
        //                _EggBooster();
        //                StartCoroutine(_CoolDown());
        //                _SetText();
        //            }
        //        }
        //    }
        //}
    }

    public void _ButtonTouch()
    {
        if (playerStats.eggAmount > 0 && isCoolDown == false)
        {
            isCoolDown = true;

            boosterAudio.Play();

            playerStats.eggAmount -= 1;
            playerStats.achivBoosterUse += 1;

            _EggBooster();
            StartCoroutine(_CoolDown());
            _SetText();
        }
    }

    public void _SetText()
    {
        amountText.text = playerStats.eggAmount.ToString();
    }

    public void _EggBooster()
    {
        Boss boss = FindObjectOfType<Boss>();

        for (int i = 0; i < boss.bossOrders.Length; i++)
        {
            for(int j=0; j< boss.bossOrders[i].bossOrderPos.Length; j++)
            {
                BossOrderPos bossOrderPos = boss.bossOrders[i].bossOrderPos[j];

                if(bossOrderPos.gameObject.activeSelf == true && bossOrderPos.orderID != "")
                {
                    bossOrderPos.GetComponent<BossOrderPos>().orderStats = 1;
                    bossOrderPos.GetComponent<BossOrderPos>()._Pay();
                }
            }
        }
    }

    IEnumerator _CoolDown()
    {
        gameObject.GetComponent<TweenScale>().enabled = true;
        yield return new WaitForSeconds(2f);
        isCoolDown = false;
        gameObject.GetComponent<TweenScale>().enabled = false;

        yield break;
    }
}
