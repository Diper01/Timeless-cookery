using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockStarBoard : MonoBehaviour
{
    private PlayerStats playerStats;
    private MapController mapController;
    private LevelManager levelManager;

    public SelectWorldPopup selectWorldPopup;
    public GameObject nextWorldCircle;
    public GameObject mapControllerScrollView;
    public GameObject noitice;

    public int currentUnlockStar;

    public UILabel starText;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        mapController = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapController>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();

        _SetText();
    }

    // Update is called once per frame
    void Update()
    {
        _ActiveNotice();
    }

    void _ActiveNotice()
    {
        if(mapControllerScrollView.transform.localPosition.x < -2430f && noitice.activeSelf == false)
        {
            noitice.SetActive(true);
            noitice.GetComponent<TweenScale>().enabled = true;
            noitice.GetComponent<TweenScale>().ResetToBeginning();
            noitice.GetComponent<TweenScale>().from = new Vector3(0f, 0f, 0f);
            noitice.GetComponent<TweenScale>().to = new Vector3(1f, 1f, 1f);
            noitice.GetComponent<TweenScale>().duration = 0.25f;
            noitice.GetComponent<TweenScale>().PlayForward();

            noitice.GetComponent<TweenRotation>().enabled = true;

            _SetText();
        }
        else if (mapControllerScrollView.transform.localPosition.x > -2430f && noitice.activeSelf == true)
        {
            noitice.GetComponent<TweenScale>().enabled = false;
            noitice.GetComponent<TweenRotation>().enabled = false;
            noitice.SetActive(false);
        }
    }

    void _SetText()
    {
        if (selectWorldPopup.selectWorldButtons.Length > mapController.selectedMap + 1 && selectWorldPopup.selectWorldButtons[mapController.selectedMap + 1] != null)
        {
            currentUnlockStar = selectWorldPopup.selectWorldButtons[mapController.selectedMap + 1].unlockStar;

            if (mapController.selectedMap == playerStats.unlockedWorlds)
            {
                if (levelManager.totalStar >= currentUnlockStar)
                {
                    starText.text = string.Format("{0:00}/{1:00}", currentUnlockStar, currentUnlockStar);
                }
                else
                {
                    starText.text = string.Format("{0:00}/{1:00}", levelManager.totalStar, currentUnlockStar);
                }

                nextWorldCircle.SetActive(true);
            }

            else if (mapController.selectedMap < playerStats.unlockedWorlds)
            {
                nextWorldCircle.SetActive(false);
            }
        }

        if (selectWorldPopup.selectWorldButtons.Length == mapController.selectedMap + 1 
            || selectWorldPopup.selectWorldButtons[mapController.selectedMap].unlocked == true 
            && selectWorldPopup.selectWorldButtons[mapController.selectedMap + 1].unlocked == true)
        {
            nextWorldCircle.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
