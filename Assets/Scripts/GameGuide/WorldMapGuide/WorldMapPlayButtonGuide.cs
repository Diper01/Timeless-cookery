using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapPlayButtonGuide : MonoBehaviour
{
    //private GoogleFireBaseEvents googleFireBaseEvents;

    public GameObject levelInfo;

    public GameObject guideBG, notice, guideButton;

    // Start is called before the first frame update
    void Start()
    {
        //googleFireBaseEvents = FindObjectOfType<GoogleFireBaseEvents>();
    }

    // Update is called once per frame
    void Update()
    {
        _Active();
    }

    void _Active()
    {
        if(levelInfo.transform.localScale.x >= 1)
        {
            if(guideBG.activeSelf == false && notice.activeSelf == false && guideButton.activeSelf == false)
            {
                guideBG.SetActive(true);
                notice.SetActive(true);
                guideButton.SetActive(true);
            }
        }
    }
}
