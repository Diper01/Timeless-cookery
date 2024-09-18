using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayWarningImg : MonoBehaviour
{
    private MissionManager missionManager;
    public GamePlayMenuManager gamePlayMenuManager;

    public GameObject warningImg;

    // Start is called before the first frame update
    void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        warningImg.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _SetActive();
    }

    void _SetActive()
    {
        if (missionManager.firstTarget == MissionManager.FirstTarget.Time)
        {
            if (missionManager.currentTime > 0f && missionManager.currentTime <= 5f && warningImg.activeSelf == false)
            {
                warningImg.SetActive(true);
#if UNITY_IPHONE
                Handheld.Vibrate();
#endif
            }
            else if (missionManager.currentTime > 5f && warningImg.activeSelf == true)
            {
                warningImg.SetActive(false);
            }
        }
    }
}
