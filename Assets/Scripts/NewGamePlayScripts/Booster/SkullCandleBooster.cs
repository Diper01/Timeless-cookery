using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullCandleBooster : MonoBehaviour
{
    public BoosterHolder boosterHolder;

    public int amount = 0;

    private float coolDownTime = 2f;
    public float currentCoolDownTime;
    private bool isCoolDown = false;

    private void Start()
    {

    }

    private void Update()
    {
        _OnTouch();
        _CoolDown();
    }

    void _OnTouch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.gameObject.name == gameObject.name)
                {
                    if (amount > 0 && isCoolDown == false)
                    {
                        amount -= 1;
                        boosterHolder.isUsing = true;
                        _SkullCandleBooster();
                    }
                }
            }
        }
    }

    public void _SkullCandleBooster()
    {
        GameObject[] customers = GameObject.FindGameObjectsWithTag("Customer");

        float temp = 0f;

        for (int i = 0; i < customers.Length; i++)
        {
            if (customers[i].GetComponent<Customer>().ordered == true && customers[i].GetComponent<Customer>().orderIsOk == false)
            {
                customers[i].GetComponent<Customer>().currentWaitTime = customers[i].GetComponent<Customer>().customerWaitTime;
                temp = customers[i].GetComponent<Customer>().customerWaitTime;
            }
        }

        currentCoolDownTime = temp;
        isCoolDown = true;
    }

    public void _CoolDown()
    {
        if (isCoolDown == true)
        {
            if (currentCoolDownTime > 0)
            {
                currentCoolDownTime -= Time.deltaTime;
            }

            if (currentCoolDownTime <= 0)
            {
                isCoolDown = false;
                boosterHolder.isUsing = false;
                currentCoolDownTime = coolDownTime;
            }
        }
    }
}
