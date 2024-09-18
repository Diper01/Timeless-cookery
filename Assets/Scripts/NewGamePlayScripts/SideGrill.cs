using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideGrill : MonoBehaviour
{
    private GameObject target;

    public GameObject cookClock;
    public GameObject fireClock;
    public GameObject lockerImg = null;

    public Image cookProcessImg;
    public Image fireProcessImg;

    public bool isEmpty = true;
    public bool isUnlocked = false;

    public bool isProcessed = false;
    public bool isOverBurned = false;

    private float processTime;
    private float overBurnedTime;
    private float currentProcessTime = 0;
    private float currentOverBurnedTime = 0;

    void Start()
    {
        currentProcessTime = processTime;
        currentOverBurnedTime = overBurnedTime;

        _Unlock();
    }

    void Update()
    {
        //_ProcessFood();
    }

    /*void _ProcessFood()
    {
        if(isEmpty == true)
        {
            currentProcessTime = processTime;
            currentOverBurnedTime = overBurnedTime;
            cookProcessImg.fillAmount = 1;
            fireProcessImg.fillAmount = 1;
            isProcessed = false;
            isOverBurned = false;
        }
        else
        if (target != null && isEmpty == false)
        {
            currentProcessTime -= Time.deltaTime;
            cookProcessImg.fillAmount = 1 - (currentProcessTime / processTime);

            if (currentProcessTime <= 0)
            {
                isProcessed = true;

                if (isProcessed == true &&  isOverBurned == false)
                {
                    currentOverBurnedTime -= Time.deltaTime;
                    fireProcessImg.fillAmount = 1 - (currentOverBurnedTime / overBurnedTime);

                    if (currentOverBurnedTime <= 0)
                    {
                        isProcessed = false;
                        isOverBurned = true;
                    }
                }
            }
        }
    } */

    void _Unlock()
    {
        if (isUnlocked == true && lockerImg != null)
        {
            lockerImg.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider _target)
    {
        if (_target != null && _target.tag == "SideFood")
        {
            target = _target.gameObject;
            isEmpty = false;
        }
    }
}
