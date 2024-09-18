using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodController : MonoBehaviour
{
    public int lv = 1;
    public string foodId = "";
    public int foodCost = 0;

    public GameObject foodPrefab;

    public GameObject selectedGrill;

    public GameObject selectedPlates;

    public GameObject[] grills;
    public GameObject[] plates;

    private int cloneIndex = 0;

    public bool active = false;
    public bool needProcess = false;
    public bool isFirstFood = false;

    // Start is called before the first frame update
    void Start()
    {
        //_Deactive();
    }

    void Update()
    {
        //_OnTouch();
    }

    void _OnTouch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {

                if (hitInfo.transform.gameObject.name == gameObject.name)
                {
                    if (needProcess == true)
                    {
                        _CheckEmptyGrill();
                    }
                    else if (needProcess == false)
                    {
                        _SetFoodPlate();
                    }
                }
            }
        }
    }

    public void OnMouseDown()
    {
        if (needProcess == true)
        {
            _CheckEmptyGrill();
        }
        else if (needProcess == false)
        {
            _SetFoodPlate();
        }
    }

    public void _Deactive()
    {
        if (active == false)
        {
            gameObject.SetActive(false);
        }
    }

    public void _CheckEmptyGrill()
    {
        for (int i = 0; i < grills.Length; i++)
        {
            if (grills[i].GetComponent<Grill>().isEmpty == true && grills[i].GetComponent<Grill>().isUnlocked == true)
            {
                selectedGrill = grills[i];
                _SpawnRawFood();
                break;
            }
        }
    }

    void _SpawnRawFood()
    {
        selectedGrill.GetComponent<Grill>().selectedFoodID = foodId;
        selectedGrill.GetComponent<Grill>().selectedFoodLv = lv;
        selectedGrill.GetComponent<Grill>().isFirstFood = isFirstFood;
        selectedGrill.GetComponent<Grill>().isEmpty = false;
    }

    public void _SetFoodPlate()
    {
        for (int i = 0; i < plates.Length; i++)
        {
            if (plates[i].GetComponent<FoodPlate>().isUnlocked == true)
            {
                if (isFirstFood == true && plates[i].GetComponent<FoodPlate>().isEmpty == true 
                    && plates[i].GetComponent<FoodPlate>().emptySlotCount >0)
                {
                    for (int j = 0; j < plates[i].GetComponent<FoodPlate>().foodId.Length; j++)
                    {
                        if (plates[i].GetComponent<FoodPlate>().foodId[j].id == foodId
                            && plates[i].GetComponent<FoodPlate>().foodId[j].isEmpty == true)
                        {
                            plates[i].GetComponent<FoodPlate>().isEmpty = false;
                            plates[i].GetComponent<FoodPlate>().foodId[j].isEmpty = false;

                            return;
                        }
                    }
                }
                else
                if (isFirstFood == false && plates[i].GetComponent<FoodPlate>().isEmpty == false && plates[i].GetComponent<FoodPlate>().emptySlotCount > 0)
                {
                    for (int j = 0; j < plates[i].GetComponent<FoodPlate>().foodId.Length; j++)
                    {
                        if (plates[i].GetComponent<FoodPlate>().foodId[j].id == foodId
                            && plates[i].GetComponent<FoodPlate>().foodId[j].isEmpty == true)
                        {
                            plates[i].GetComponent<FoodPlate>().foodId[j].isEmpty = false;

                            return;
                        }
                       else if(plates[i].GetComponent<FoodPlate>().foodId[j].id == foodId
                            && plates[i].GetComponent<FoodPlate>().foodId[j].isEmpty == false)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
