using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayKingIconTopbar : MonoBehaviour
{
    private MissionManager missionManager;

    public GameObject icon;
    public GameObject startP, endP;

    // Start is called before the first frame update
    void Start()
    {
        missionManager = FindObjectOfType<MissionManager>();
    }

    // Update is called once per frame
    void Update()
    {
      if(transform.position.x >= 0 && missionManager.currentTime >= 0)
        {
            icon.transform.position = new Vector3(endP.transform.position.x, icon.transform.position.y, icon.transform.position.z);

            float scale = (float)missionManager.currentTime / (float)missionManager.missionTimeLimit;

            startP.transform.localScale = new Vector3(scale, startP.transform.localScale.y, startP.transform.localScale.z);
        }   
    }
}
