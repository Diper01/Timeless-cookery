using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementController : MonoBehaviour
{
    private PlayerStats playerStats;
    private LevelManager levelManager;

    public AchivementObject[] achivObjs;

    public AchivementNoticePopup achivNoticePopup;

    public GameObject atten;
    private float attenAcTime = 3f;
    private float currentTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        levelManager = FindObjectOfType<LevelManager>();

        currentTime = attenAcTime;

        _LoadToAchivement();
        _SetToPlayerStats();
        _ActiveAttention();

        if(levelManager.totalStar > 0)
        {
            StartCoroutine(_ActiveNoticePopup());
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //_ActiveNoticePopup();
        //_ActiveAttention();
    }

    void _LoadToAchivement()
    {
        if (playerStats.achivementDatas.Count > 0)
        {
            for (int i = 0; i < achivObjs.Length; i++)
            {
                for (int j = 0; j < playerStats.achivementDatas.Count; j++)
                {
                    if (achivObjs[i].achiveType.ToString()
                        == playerStats.achivementDatas[j].achivName)
                    {
                        achivObjs[i].playerStats = playerStats;
                        achivObjs[i].achivementController = this;
                        achivObjs[i].lv = playerStats.achivementDatas[j].currentLv;

                        achivObjs[i]._SetValues();
                        achivObjs[i]._ActiveImage();
                        achivObjs[i]._ActiveButton();

                        if (atten.activeSelf == false)
                        {
                            _ActiveAttention();
                        }

                        if (playerStats.achivtNoiticeActived == false)
                        {
                            StartCoroutine(_ActiveNoticePopup());
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < achivObjs.Length; i++)
            {
                PlayerStats.AchivementData achivementData = new PlayerStats.AchivementData();

                achivementData.achivName = achivObjs[i].achiveType.ToString();
                achivementData.currentLv = achivObjs[i].lv;

                playerStats.achivementDatas.Add(achivementData);

                playerStats.save = true;
            }

        }
    }

    void _SetToPlayerStats()
    {
        for (int i = 0; i < achivObjs.Length; i++)
        {
            for (int j = 0; j < playerStats.achivementDatas.Count; j++)
            {
                if (achivObjs[i].achiveType.ToString() == playerStats.achivementDatas[j].achivName)
                {
                    achivObjs[i].lv = playerStats.achivementDatas[j].currentLv;
                }
            }
        }
    }

    public void _ActiveAttention()
    {
        for (int i = 0; i < achivObjs.Length; i++)
        {
            if (achivObjs[i].bttnCollectL.activeSelf == true)
            {
                atten.SetActive(true);
                return;
            }
            else if (i + 1 == achivObjs.Length)
            {
                atten.SetActive(false);
            }
        }
    }

    IEnumerator _ActiveNoticePopup()
    {
        playerStats.achivtNoiticeActived = true;

        yield return new WaitForSeconds(1.5f);

        if (achivNoticePopup.gameObject.transform.localScale.y == 0)
        {
            for (int i = 0; i < achivObjs.Length; i++)
            {
                if (achivObjs[i].lv < 3)
                {
                    if (achivObjs[i].lv == 0 && achivObjs[i].currentAmount >= achivObjs[i].targetAmount)
                    {
                        Debug.Log(achivObjs[i].gameObject.name + "level = " + achivObjs[i].lv);
                        achivNoticePopup.rewardValue = achivObjs[i].lv1Reward;
                        achivNoticePopup.achiveName = achivObjs[i].achiveName;
                        achivNoticePopup._Active();
                        yield break;
                    }
                    else if (achivObjs[i].lv == 1 && achivObjs[i].currentAmount >= (achivObjs[i].targetAmount * 2))
                    {
                        Debug.Log(achivObjs[i].gameObject.name + "level = " + achivObjs[i].lv);
                        achivNoticePopup.rewardValue = achivObjs[i].lv2Reward;
                        achivNoticePopup.achiveName = achivObjs[i].achiveName;
                        achivNoticePopup._Active();
                        yield break;
                    }
                    else if (achivObjs[i].lv == 2 && achivObjs[i].currentAmount >= (achivObjs[i].targetAmount * 3))
                    {
                        Debug.Log(achivObjs[i].gameObject.name + "level = " + achivObjs[i].lv);
                        achivNoticePopup.rewardValue = achivObjs[i].lv3Reward;
                        achivNoticePopup.achiveName = achivObjs[i].achiveName;
                        achivNoticePopup._Active();
                        yield break;
                    }
                }
            }
        }

        yield break;
    }
}
