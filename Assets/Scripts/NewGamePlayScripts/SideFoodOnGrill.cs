using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideFoodOnGrill : MonoBehaviour
{
    public string foodId = "";
    public int foodCost = 0;

    public bool isFirstFood = false;

    [Header("Prefabs status")]
    public GameObject foodRaw;
    public GameObject foodProcessed;
    public GameObject foodOverBurned;

    private GameObject target;

    public float processTime;
    public float overBurnedTime;
    public float currentProcessTime = 0;
    public float currentOverBurnedTime = 0;

    public bool isProcessed = false;
    public bool isOverBurned = false;
    public bool isOnPlate = false;
    public bool isOnGrill = false;

    // Start is called before the first frame update
    void Start()
    {
        currentProcessTime = processTime;
        currentOverBurnedTime = overBurnedTime;
    }

    void Update()
    {
        _OnTouch();
        _ProcessFood();
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

    void _ProcessFood()
    {
        currentProcessTime -= Time.deltaTime;

        if (currentProcessTime <= 0)
        {
            isProcessed = true;

            if (isProcessed == true && isOverBurned == false)
            {
                currentOverBurnedTime -= Time.deltaTime;

                if (currentOverBurnedTime <= 0)
                {
                    isOverBurned = true;
                }
            }
        }

        _ProcessImages();
    }

    void _ProcessImages()
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

    void _Discard()
    {
        Debug.Log("Da bo vao thung rac");
        _SetGrillEmpty();
        Destroy(gameObject);
    }

    void _SetFoodPlate()
    {
        GameObject[] plates = GameObject.FindGameObjectsWithTag("SideFoodPos");
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

                            _SetGrillEmpty();
                            Destroy(gameObject);
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

                            _SetGrillEmpty();
                            Destroy(gameObject);
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

    void _SetGrillEmpty()
    {
        target.GetComponent<SideGrill>().isEmpty = true;
    }

    void OnTriggerEnter(Collider _target)
    {
        if (_target != null && _target.tag == "SideGrill")
        {
            target = _target.gameObject;
        }
    }
}
