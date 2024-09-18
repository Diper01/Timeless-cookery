using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKingLevelIcon : MonoBehaviour
{
    public LevelSetting set0,set1,set2;
    public GameObject icon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(activeLevelSetting.gameObject.activeSelf == false && icon.activeSelf == true)
        //{
        //    icon.SetActive(false);
        //}

        //if (activeLevelSetting.gameObject.activeSelf == true && icon.activeSelf == false)
        //{
        //    icon.SetActive(true);
        //}
    }

    void _SetBossSetting()
    {
        if(set0.gameMode == MissionManager.GameMode.Boss)
        {
            set1 = set0;
            set2 = set0;
        }
    }
}
