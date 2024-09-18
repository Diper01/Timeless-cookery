using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grill : MonoBehaviour
{
    private MissionManager missionManager;
    private GamePlayMenuManager gamePlayMenuManager;

    public string id = "";

    public GameObject lockerImg = null;

    public GameObject cookClock;
    public GameObject fireClock;
    public GameObject waterIcon;

    public Image cookProcessImg;
    public Image fireProcessImg;

    public GameObject smokeSpine;
    public GameObject fireParticle;

    public bool isEmpty = true;
    public bool isUnlocked = false;

    public bool isFreeze = false;
    public bool isProcessed = false;
    public bool isOverBurned = false;

    public float processTime = 5f;
    public float overBurnedTime = 3f;
    public float currentProcessTime = 0;
    public float currentOverBurnedTime = 0;

    public GameObject selectedFood = null;
    public string selectedFoodID = "";
    public int selectedFoodLv = 0;
    public bool isFirstFood = false;

    public FoodId[] foodOnGrills;

    void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        gamePlayMenuManager = GameObject.FindGameObjectWithTag("GamePlayMenuManager").GetComponent<GamePlayMenuManager>();

        currentProcessTime = processTime;
        currentOverBurnedTime = overBurnedTime;

        _DeactiveFoodOnGrillImg();
    }

    void Update()
    {
        if(gamePlayMenuManager.win == true || gamePlayMenuManager.lose == true)
        {
            return;
        }

        _SelectOnGrillFood();
        _SetFoodOnGrillStats();
        _Unlock();
        _ProcessFood();
        _ActiveClock();

        _PlayAudio();

        _Effect();
    }

    public void _ResetGrill()
    {
        currentProcessTime = processTime;
        currentOverBurnedTime = overBurnedTime;

        cookClock.SetActive(false);
        fireClock.SetActive(false);
        cookProcessImg.fillAmount = 1;
        fireProcessImg.fillAmount = 1;

        isEmpty = true;
        isFreeze = false;
        isProcessed = false;
        isOverBurned = false;

        _DeactiveFoodOnGrillImg();

        selectedFood = null;
    }

    void _ProcessFood()
    {
        if(isFreeze == false && waterIcon.activeSelf == true)
        {
            waterIcon.SetActive(false);
        }
        else if (isFreeze == true && waterIcon.activeSelf == false)
        {
            waterIcon.SetActive(true);
        }

        if (isFreeze == false && isEmpty == false)
        {
            currentProcessTime -= Time.deltaTime;

            cookProcessImg.fillAmount = 1 - (currentProcessTime / processTime);

            if (currentProcessTime <= 0)
            {
                isProcessed = true;

                if (isProcessed == true && isOverBurned == false)
                {
                    currentOverBurnedTime -= Time.deltaTime;

                    fireProcessImg.fillAmount = 1 - (currentOverBurnedTime / overBurnedTime);

                    if (currentOverBurnedTime <= 0 && isOverBurned == false)
                    {
                        isOverBurned = true;

                        missionManager.friedFoodCount += 1;
                    }
                }
            }
        }
    }

    void _ActiveClock()
    {
        if(isEmpty == true)
        {
            cookClock.SetActive(false);
            fireClock.SetActive(false);
        }
        else
        if (isEmpty == false)
        {
            if (isProcessed == false && isOverBurned == false)
            {
                cookClock.SetActive(true);
                fireClock.SetActive(false);
            }
            else if (isProcessed == true && isOverBurned == false)
            {
                cookClock.SetActive(false);
                fireClock.SetActive(true);
            }
        }
    }

    void _DeactiveFoodOnGrillImg()
    {
        for (int i = 0; i < foodOnGrills.Length; i++)
        {
            for(int j=0; j< foodOnGrills[i].foods.Length; j++)
            {
                foodOnGrills[i].foods[j].foodImg.SetActive(false);
            }
        }
    }

    void _SelectOnGrillFood()
    {
        if(isEmpty == false && selectedFood == null)
        {
            for(int i=0; i<foodOnGrills.Length; i++)
            {
                if(foodOnGrills[i].id == selectedFoodID)
                {
                    selectedFood = foodOnGrills[i].foods[selectedFoodLv - 1].foodImg;
                    selectedFood.GetComponent<FoodOnGrill>().isFirstFood = isFirstFood;
                    selectedFood.GetComponent<FoodOnGrill>().isOnGrill = true;
                    selectedFood.SetActive(true);

                    return;
                }
            }
        }
    }

    void _SetFoodOnGrillStats()
    {
        if(isEmpty == false)
        {
            selectedFood.GetComponent<FoodOnGrill>().isProcessed = isProcessed;
            selectedFood.GetComponent<FoodOnGrill>().isOverBurned = isOverBurned;
        }
    }

    void _Unlock()
    {
        if(isUnlocked == true && lockerImg != null)
        {
            lockerImg.SetActive(false);
        }
    }

    void _Effect()
    {
        if(isEmpty == false && smokeSpine.activeSelf ==false && fireParticle.activeSelf == false)
        {
            smokeSpine.SetActive(true);
            fireParticle.SetActive(true);
        }
        else if (isEmpty == true && smokeSpine.activeSelf == true && fireParticle.activeSelf == true)
        {
            smokeSpine.SetActive(false);
            fireParticle.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider _target)
    {
        //if (_target != null && _target.tag == "FoodOnGrill")
        //{
        //    foodOnGrillTarget = _target.gameObject;
        //    isEmpty = false;
        //}
    }

    void _PlayAudio()
    {
        if(isProcessed == true && isEmpty == false && gameObject.GetComponent<AudioSource>().enabled == false)
        {
            gameObject.GetComponent<AudioSource>().enabled = true;
        }
        else if (isProcessed == false && isEmpty == true && gameObject.GetComponent<AudioSource>().enabled == true)
        {
            gameObject.GetComponent<AudioSource>().enabled = false;
        }
    }
}
