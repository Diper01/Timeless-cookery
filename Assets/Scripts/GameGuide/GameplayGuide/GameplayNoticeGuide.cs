using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayNoticeGuide : MonoBehaviour
{
    public GameplayGuideController gameplayGuideController;

    public GameObject lockUIButton, lockBG, guideBG, guideNotice;

    public float waitTime = 1.5f;
    public float currentTime = 0f;

    public bool isActived = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        timeCount();
    }

    void timeCount()
    {
        if(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else if(currentTime <=0 && isActived == false)
        {
            isActived = true;
            _Active();
        }
    }

    void _Active()
    {
        lockUIButton.SetActive(true);
        lockBG.SetActive(true);
        guideBG.SetActive(true);
        guideNotice.SetActive(true);

        Time.timeScale = 0;
    }

    public void _OnButton()
    {
        Time.timeScale = 1;
        gameplayGuideController._NextStep();
    }
}
