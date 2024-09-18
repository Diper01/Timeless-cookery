using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourGlassBooster : MonoBehaviour
{
    private MissionManager missionManager;
    public BoosterHolder boosterHolder;

    public int amount = 0;
    public float currentCoolDownTime = 30f;
    private bool isCoolDown = false;

    private void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
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
                        _HourGlassBooster();
                    }
                }
            }
        }
    }

    public void _HourGlassBooster()
    {
        missionManager.currentTime += 30f;
        boosterHolder.isUsing = true;
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
                currentCoolDownTime = 30f;
            }
        }
    }
}
