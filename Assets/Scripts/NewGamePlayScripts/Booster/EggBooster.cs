using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggBooster : MonoBehaviour
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
        Customer cus;
        GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");

        for (int i = 0; i < customers.Length; i++)
        {
            cus = customers[i].GetComponent<Customer>();
            if (cus.ordered == true)
            {
                if(cus.currentFirstOrderId != "")
                {
                    cus.firstOrderStats = 1;
                    cus._PayFirstOrder();
                }
                if (cus.currentSecondOrderId != "")
                {
                    cus.secondOrderStats = 1;
                    cus._PaySecondOrder();
                }
                if (cus.currentThirdOrderId != "")
                {
                    cus.thirdOrderStats = 1;
                    cus._PayThirdOrder();
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
