using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayMusicController : MonoBehaviour
{
    private MissionManager missionManager;
    // Start is called before the first frame update
    void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(missionManager.open == true && gameObject.GetComponent<AudioSource>().enabled == false)
        {
            gameObject.GetComponent<AudioSource>().enabled = true;
        }
    }
}
