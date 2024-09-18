using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUsPopup : MonoBehaviour
{
    //public GoogleFireBaseEvents googleFireBaseEvents;
    private PlayerStats playerStats;
    private LevelManager levelManager;

    public bool autoActived = false;
    public float waitTime = 1.5f;
    public float currentTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        //googleFireBaseEvents = FindObjectOfType<GoogleFireBaseEvents>();

        currentTime = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        _AutoActive();
    }

    public void RateButton()
    {
#if UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
#elif UNITY_IOS
        Application.OpenURL("");
#endif
    }

    void _AutoActive()
    {
        if(autoActived == false && playerStats.rated == false)
        {
            if(currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            else if(currentTime <= 0)
            {
                autoActived = true;

                for (int i = 0; i < levelManager.levels.Length; i++)
                {
                    if (levelManager.levels[i].levelCompleted == true
                        && levelManager.levels[i].levelID % 3 == 0
                        && levelManager.levels[i + 1] != null
                        && levelManager.levels[i + 1].levelCompleted == false
                        || levelManager.levels[levelManager.levels.Length - 1].levelCompleted == true
                        && levelManager.levels[levelManager.levels.Length].levelID % 3 == 0)
                    {

                        gameObject.GetComponent<UIScaleAnimation>().enabled = true;
                    }
                }
            }
            
        }
    }

    public void RateBad()
    {
        //googleFireBaseEvents.RateBad();
        playerStats.rated = true;
        playerStats.save = true;
    }

    public void RateGood()
    {
#if UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
#elif UNITY_IOS
        Application.OpenURL("itms-apps:itunes.apple.com/app/id6447951196");
#endif
        //googleFireBaseEvents.RateGood();
        playerStats.rated = true;
        playerStats.save = true;
    }
}
