using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodOnGrill : MonoBehaviour
{
    public string foodId = "";

    public bool isFirstFood = false;

    public float durationTime = 0.5f;
    public float speed = 3f;

    [Header("Prefabs status")]
    public GameObject foodRaw;
    public GameObject foodProcessed;
    public GameObject foodOverBurned;

    public GameObject[] plates;

    public GameObject selectedGrill;

    //public float processTime;
    //public float overBurnedTime;
    ////public float currentProcessTime = 0;
    ////public float currentOverBurnedTime = 0;

    //public bool isFreeze = false;
    public bool isProcessed = false;
    public bool isOverBurned = false;
    public bool isOnPlate = false;
    public bool isOnGrill = false;

    // Start is called before the first frame update
    void Start()
    {
        //currentProcessTime = processTime;
        //currentOverBurnedTime = overBurnedTime;

        gameObject.name = gameObject.name + selectedGrill.name;

        _AnimScale();
    }

    void Update()
    {
        //_OnTouch();
        //_ProcessFood();
        _ProcessImages();
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
                    if (isProcessed == true && isOverBurned == false)
                    {
                        _SetFoodPlate();
                    }
                    if (isProcessed == true && isOverBurned == true)
                    {
                        _Discard();
                    }
                }
            }
        }
    }

    public void OnMouseDown()
    {
        if (isProcessed == true && isOverBurned == false)
        {
            _SetFoodPlate();
        }
        if (isProcessed == true && isOverBurned == true)
        {
            _Discard();
        }
    }

    //void _ProcessFood()
    //{
    //    if(isFreeze == false && isOnGrill == true)
    //    {
    //        currentProcessTime -= Time.deltaTime;

    //        if (currentProcessTime <= 0)
    //        {
    //            isProcessed = true;

    //            if (isProcessed == true && isOverBurned == false)
    //            {
    //                currentOverBurnedTime -= Time.deltaTime;

    //                if (currentOverBurnedTime <= 0)
    //                {
    //                    isOverBurned = true;
    //                }
    //            }
    //        }
    //    }

    //    _ProcessImages();
    //}

    void _AnimScale()
    {
        gameObject.GetComponent<ScaleEffect>().init();
    }

    void _ProcessImages()
    {
        if(isOnGrill == true)
        {
            if (isProcessed == true && isOverBurned == true)
            {
                foodProcessed.SetActive(false);
                foodOverBurned.SetActive(true);
            }
            else if (isProcessed == true && isOverBurned == false)
            {
                foodRaw.SetActive(false);
                foodProcessed.SetActive(true);
            }
            else if (isProcessed == false && isOverBurned == false)
            {
                foodRaw.SetActive(true);
                foodProcessed.SetActive(false);
                foodOverBurned.SetActive(false);
            }
        }
    }

    void _Discard()
    {
        _ResetVaules();
    }

    public void _SetFoodPlate()
    {
        for(int i=0; i<plates.Length; i++)
        {
            if (plates[i].GetComponent<FoodPlate>().isUnlocked == true)
            {
                if (isFirstFood == true && plates[i].GetComponent<FoodPlate>().isEmpty == true
                    && plates[i].GetComponent<FoodPlate>().emptySlotCount > 0)
                {
                    for (int j = 0; j < plates[i].GetComponent<FoodPlate>().foodId.Length; j++)
                    {
                        if (plates[i].GetComponent<FoodPlate>().foodId[j].id == foodId
                            && plates[i].GetComponent<FoodPlate>().foodId[j].isEmpty == true)
                        {
                            plates[i].GetComponent<FoodPlate>().isEmpty = false;
                            plates[i].GetComponent<FoodPlate>().foodId[j].isEmpty = false;

                            plates[i].GetComponent<FoodPlate>().TouchScaleEffect();

                            _ResetVaules();
                            return;
                        }
                    }
                }
                else
                if (isFirstFood == false && plates[i].GetComponent<FoodPlate>().isEmpty == false
                    && plates[i].GetComponent<FoodPlate>().emptySlotCount > 0)
                {
                    for (int j = 0; j < plates[i].GetComponent<FoodPlate>().foodId.Length; j++)
                    {
                        if (plates[i].GetComponent<FoodPlate>().foodId[j].id == foodId
                            && plates[i].GetComponent<FoodPlate>().foodId[j].isEmpty == true)
                        {
                            plates[i].GetComponent<FoodPlate>().foodId[j].isEmpty = false;

                            plates[i].GetComponent<FoodPlate>().TouchScaleEffect();

                            _ResetVaules();
                            return;
                        }
                        else if (plates[i].GetComponent<FoodPlate>().foodId[j].id == foodId
                            && plates[i].GetComponent<FoodPlate>().foodId[j].isEmpty == false)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }

    public void _ResetVaules()
    {
        isProcessed = false;
        isOverBurned = false;
        isOnPlate = false;
        isOnGrill = false;
        selectedGrill.GetComponent<Grill>()._ResetGrill();
    }

    void OnTriggerEnter(Collider _target)
    {
        //if (_target != null && _target.tag == "Grill")
        //{
        //    GrillTarget = _target.gameObject;

        //    isOnGrill = true;
        //}
    }
}
