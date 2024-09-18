using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapBoosterSelectGuide : MonoBehaviour
{
    private MissionManager missionManager;

    public GameObject levelInfo;

    public int activeLevel = 0;

    public GameObject guideBG, handGuide, guideButton, notice;

    public bool actived = false;

    // Start is called before the first frame update
    void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _Active();
    }

    void _Active()
    {
        if (levelInfo.transform.localScale.x >= 1 && missionManager.levelID == activeLevel && actived == false)
        {
            actived = true;
            guideBG.SetActive(true);
            handGuide.SetActive(true);
            guideButton.SetActive(true);
            notice.SetActive(true);
        }
    }
}
