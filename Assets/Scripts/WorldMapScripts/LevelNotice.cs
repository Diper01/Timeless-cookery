using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNotice : MonoBehaviour
{
    public LevelController levelActive;
    public LevelController currentLevel;

    public GameObject mapControllerScrollView;
    public GameObject notice;

    public Vector3 activePos;
    public Vector3 deactivePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _ActiveNotice();
    }

    public void _ActiveNotice()
    {
        if(levelActive.unlocked == true && currentLevel.currentStar == 0)
        {
            if (mapControllerScrollView.transform.localPosition.x < activePos.x 
                && mapControllerScrollView.transform.localPosition.x > deactivePos.x
                && notice.activeSelf == false)
            {
                notice.SetActive(true);
                notice.GetComponent<TweenScale>().ResetToBeginning();
                notice.GetComponent<TweenScale>().from = new Vector3(0f, 0f, 0f);
                notice.GetComponent<TweenScale>().to = new Vector3(1f, 1f, 1f);
                notice.GetComponent<TweenScale>().duration = 0.25f;
                notice.GetComponent<TweenScale>().PlayForward();
            }
            else if (mapControllerScrollView.transform.localPosition.x > activePos.x && notice.activeSelf == true
                || mapControllerScrollView.transform.localPosition.x < deactivePos.x && notice.activeSelf == true)
            {
                notice.SetActive(false);
            }
        }
    }
}
