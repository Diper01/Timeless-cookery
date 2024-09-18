using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementNoticePopup : MonoBehaviour
{
    private PlayerStats playerStats;

    public WorldMapController worldMapController;

    public GameObject blackBG;

    public UILabel rewardText;
    public UILabel contentText;

    public int rewardValue = 0;
    public string achiveName = "";

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        _SetText();
    }

    public void _Active()
    {
        blackBG.transform.localScale = new Vector3(1, 1, 1);
        gameObject.GetComponent<TweenScale>().ResetToBeginning();
        gameObject.GetComponent<TweenScale>().from = new Vector3(1, 0, 1);
        gameObject.GetComponent<TweenScale>().to = new Vector3(1, 1, 1);
        gameObject.GetComponent<TweenScale>().PlayForward();


        StartCoroutine(_AutoDeactive());
    }

    public void _Deactive()
    {
        gameObject.GetComponent<TweenScale>().ResetToBeginning();
        gameObject.GetComponent<TweenScale>().from = new Vector3(1, 1, 1);
        gameObject.GetComponent<TweenScale>().to = new Vector3(1, 0, 1);
        gameObject.GetComponent<TweenScale>().PlayForward();

        blackBG.transform.localScale = new Vector3(1, 0, 1);
    }

    IEnumerator _AutoDeactive()
    {
        if (gameObject.transform.localScale.x >= 1)
        {
            yield return new WaitForSeconds(3f);
            {
                _Deactive();
            }
        }

        yield break;
    }

    public void _SetText()
    {
        if (gameObject.transform.localScale.y >= 1)
        {
            rewardText.text = rewardValue.ToString();
            contentText.text = "conggratulations, you have successfully to unlock the " + ((char)34) + achiveName.ToString() + ((char)34);
        }
    }

    public void _OKButton()
    {
        playerStats.achivtNoiticeActived = true;
        worldMapController.openAchievementDialog();
        _Deactive();
    }
}
