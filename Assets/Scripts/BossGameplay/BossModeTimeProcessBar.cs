using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossModeTimeProcessBar : MonoBehaviour
{
    private MissionManager missionManager;

    public TopBar topBar;

    public Image missionProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        missionManager = FindObjectOfType<MissionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _TimeProcess();
    }

    void _TimeProcess()
    {
        if (missionManager.currentTime < 0)
        {
            missionProgressBar.fillAmount = 1;
        }
        else
        {
            missionProgressBar.fillAmount = 1 - ((float)missionManager.currentTime / (float)missionManager.missionTimeLimit);
        }
    }
}
