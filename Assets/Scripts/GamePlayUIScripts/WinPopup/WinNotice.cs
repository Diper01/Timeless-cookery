using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinNotice : MonoBehaviour
{
    public GamePlayMenuManager gamePlayMenuManager;

    private float waitTime = 2.5f;
    private float currentTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        _ActiveWinMenu();
    }

    void _ActiveWinMenu()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else if (currentTime <= 0 && gamePlayMenuManager.winMenu.activeSelf == false)
        {
            gameObject.GetComponent<TweenScale>().ResetToBeginning();
            gameObject.GetComponent<TweenScale>().from = new Vector3(1, 1, 1);
            gameObject.GetComponent<TweenScale>().to = new Vector3(0, 0, 0);
            gameObject.GetComponent<TweenScale>().duration = 0.25f;
            gameObject.GetComponent<TweenScale>().PlayForward();

            gamePlayMenuManager.winMenu.SetActive(true);
        }

        else if(gamePlayMenuManager.winMenu.activeSelf == true && transform.localScale.x <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
