using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideFoodController : MonoBehaviour
{
    private GamePlayMenuManager gamePlayMenuManager;
    private MissionManager missionManager;

    public int lv = 1;
    public string foodId = "";
    public int foodCost = 0;

    public GameObject cookClock;

    public Image cookProcessImg;

    public float processTime = 5f;
    public float currentProcessTime = 0f;
    public int turn = 0;
    public int currentTurn = 0;

    public GameObject foodPrefab;

    //public GameObject selectedGrill;

    public GameObject selectedPlates;

    //public GameObject[] grills;
    public GameObject[] plates;

    private int cloneIndex = 0;

    public bool isUnlocked = false;
    public bool needProcess = false;
    public bool isFirstFood = false;
    public bool isProcessing = false;

    // Start is called before the first frame update
    void Start()
    {
        gamePlayMenuManager = GameObject.FindGameObjectWithTag("GamePlayMenuManager").GetComponent<GamePlayMenuManager>();
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();

        currentProcessTime = processTime;
        _SetUnlockSidePlates();
        _Deactive();
    }

    void Update()
    {
        if (gamePlayMenuManager.win == true || gamePlayMenuManager.lose == true || missionManager.open == false)
        {
            return;
        }

        //_OnTouch();

        _SetTurn();
        _Timer();
        _ActiveClock();
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
                    if (needProcess == false)
                    {
                        _SetFoodPlate();
                    }
                }
            }
        }
    }

    void _SetUnlockSidePlates()
    {
        if (isUnlocked == true && gameObject.activeSelf == true)
        {
            for (int i = 0; i < lv; i++)
            {
                plates[i].GetComponent<SideFoodPos>().isUnlocked = true;
            }
        }
    }

    void _Deactive()
    {
        if (isUnlocked == false)
        {
            gameObject.SetActive(false);
        }
    }

    void _CheckEmptyGrill()
    {
        //for (int i = 0; i < grills.Length; i++)
        //{
        //    if (grills[i].GetComponent<Grill>().isEmpty == true && grills[i].GetComponent<Grill>().isUnlocked == true)
        //    {
        //        selectedGrill = grills[i];
        //        _SpawnRawFood();
        //        break;
        //    }
        //}
    }

    void _SpawnRawFood()
    {
        //string defaultName = foodPrefab.name;
        //foodPrefab.name += "_" + cloneIndex;
        //foodPrefab.GetComponent<FoodOnGrill>().foodId = foodId;
        //Instantiate(foodPrefab, selectedGrill.transform.position, Quaternion.identity);
        //cloneIndex++;
        //foodPrefab.name = defaultName;
    }

    void _SetTurn()
    {
        if(currentTurn == 0 && isProcessing == false)
        {
            for (int i = 0; i < plates.Length; i++)
            {
                if (plates[i].activeSelf == true
                    &&plates[i].GetComponent<SideFoodPos>().isUnlocked == true
                    && plates[i].GetComponent<SideFoodPos>().isEmpty == true)
                {
                    currentTurn = 1;
                    break;
                }
            }
        }
    }

    void _ActiveClock()
    {
        if (isProcessing == false)
        {
            cookClock.SetActive(false);
        }
        else
        if (isProcessing == true)
        {
            cookClock.SetActive(true);
        }
    }

    void _Timer()
    {
        if (currentTurn == 1)
        {
            isProcessing = true;

            if (currentProcessTime > 0)
            {
                cookProcessImg.fillAmount = 1 - (currentProcessTime / processTime);
                currentProcessTime -= Time.deltaTime;
            }

            if (currentProcessTime <= 0)
            {
                for (int i = 0; i < plates.Length; i++)
                {
                    if (plates[i].GetComponent<SideFoodPos>().isUnlocked == true
                        && plates[i].GetComponent<SideFoodPos>().isEmpty == true)
                    {
                        currentTurn = 0;
                        _SetFoodPlate();

                        isProcessing = false;

                        cookProcessImg.fillAmount = 0;

                        currentProcessTime = processTime;

                        break;
                    }
                }
            }
        }
    }

    void _SetFoodPlate()
    {
        for (int i = 0; i < plates.Length; i++)
        {
            if (plates[i].GetComponent<SideFoodPos>().isUnlocked == true)
            {
                if (isFirstFood == true && plates[i].GetComponent<SideFoodPos>().isEmpty == true
                    && plates[i].GetComponent<SideFoodPos>().emptySlotCount > 0)
                {
                    for (int j = 0; j < plates[i].GetComponent<SideFoodPos>().foodId.Length; j++)
                    {
                        if (plates[i].GetComponent<SideFoodPos>().foodId[j].id == foodId
                            && plates[i].GetComponent<SideFoodPos>().foodId[j].isEmpty == true)
                        {
                            plates[i].GetComponent<SideFoodPos>().isEmpty = false;
                            plates[i].GetComponent<SideFoodPos>().foodId[j].isEmpty = false;

                            plates[i].GetComponent<ScaleEffect>().init();

                            return;
                        }
                    }
                }
                else
                if (isFirstFood == false && plates[i].GetComponent<SideFoodPos>().isEmpty == false
                    && plates[i].GetComponent<SideFoodPos>().emptySlotCount > 0)
                {
                    for (int j = 0; j < plates[i].GetComponent<SideFoodPos>().foodId.Length; j++)
                    {
                        if (plates[i].GetComponent<SideFoodPos>().foodId[j].id == foodId
                            && plates[i].GetComponent<SideFoodPos>().foodId[j].isEmpty == true)
                        {
                            plates[i].GetComponent<SideFoodPos>().foodId[j].isEmpty = false;

                            plates[i].GetComponent<ScaleEffect>().init();

                            return;
                        }
                        else if (plates[i].GetComponent<SideFoodPos>().foodId[j].id == foodId
                             && plates[i].GetComponent<SideFoodPos>().foodId[j].isEmpty == false)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
