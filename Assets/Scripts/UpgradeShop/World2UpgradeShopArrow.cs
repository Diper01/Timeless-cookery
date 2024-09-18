using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World2UpgradeShopArrow : MonoBehaviour
{
    public GameObject scrollViewGO, arrowImg;

    public float activePosY = 0f;
    public float deactivePosY = 0f;

    public bool lower = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lower == false)
        {
            if (arrowImg.activeSelf == false && scrollViewGO.transform.localPosition.y >= activePosY)
            {
                arrowImg.SetActive(true);
            }
            else if (arrowImg.activeSelf == true && scrollViewGO.transform.localPosition.y <= deactivePosY)
            {
                arrowImg.SetActive(false);
            }
        }
        else if (lower == true)
        {
            if (arrowImg.activeSelf == false && scrollViewGO.transform.localPosition.y <= activePosY)
            {
                arrowImg.SetActive(true);
            }
            else if (arrowImg.activeSelf == true && scrollViewGO.transform.localPosition.y >= deactivePosY)
            {
                arrowImg.SetActive(false);
            }
        }
    }
}
