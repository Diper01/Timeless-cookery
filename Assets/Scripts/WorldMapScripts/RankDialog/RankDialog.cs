using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;

public class RankDialog : MonoBehaviour
{
    private PlayerStats playerStats;

    public GameObject createUserDialog, userExistedNotice;

    public UILabel userNameLabel;

    private string originText;

    class UserData
    {
        public string username = "";
        public int totalCoin = 0;
    }

    [System.Serializable]
    public class RankInfo
    {
        public string coin ="";
        public string userName = "";
    }

    public RankGO[] rankGOs;

    public List<RankInfo> rankInfos = new List<RankInfo>();

    private string url = "https://api.efoxstudio.com/api/rank";

    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();

        originText = userNameLabel.text;

        OpenCreateUserDialog();

        _SetSortIndex();

        _LoadFromPLayerData();

        StartCoroutine(_UpdateUser());

        StartCoroutine(_GetRankInfoFromWeb());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void _SetSortIndex()
    {
        for(int i=0; i<rankGOs.Length; i++)
        {
            rankGOs[i].sortIndex = i + 1;
            rankGOs[i]._SetRank();
        }
    }

    void _LoadFromPLayerData()
    {
        if(PlayerData.Instance.rankInfoData.Count > 0)
        {
            rankInfos.Clear();

            for (int i = 0; i < PlayerData.Instance.rankInfoData.Count; i++)
            {
                RankInfo rankInfo = new RankInfo();

                rankInfo.coin = PlayerData.Instance.rankInfoData[i].coin;

                rankInfo.userName = PlayerData.Instance.rankInfoData[i].userName;

                rankInfos.Add(rankInfo);
            }
        }
    }

    void _SaveToPlayerData()
    {
        PlayerData.Instance.rankInfoData.Clear();

        for (int i = 0; i < rankInfos.Count; i++)
        {
            RankInfo rankInfo = new RankInfo();

            rankInfo.coin = rankInfos[i].coin;

            rankInfo.userName = rankInfos[i].userName;

            PlayerData.Instance.rankInfoData.Add(rankInfo);
        }

        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        playerStats.save = true;
    }

    void _SetText()
    {
        for(int i=0; i<rankGOs.Length; i++)
        {
            if (rankInfos[i] != null)
            {
                rankGOs[i].userNamelbl.text = rankInfos[i].userName;
                rankGOs[i].goldLbl.text = rankInfos[i].coin;
            }
        }
    }

    public IEnumerator _GetRankInfoFromWeb()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        webRequest.SetRequestHeader("appPassword", "efoxstudio2019");
        webRequest.method = UnityWebRequest.kHttpVerbGET;

        //   webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log("www.error - " + webRequest.error + "Ranking");
        }
        else
        {
            JSONObject json = new JSONObject(webRequest.downloadHandler.text);

            rankInfos.Clear();

            for (int i = 0; i < json.list.Count; i++)
            {
                RankInfo rankInfo = new RankInfo();

                rankInfo.coin = json[i]["totalCoin"].ToString().Replace("\"", "");

                rankInfo.userName = json[i]["username"].ToString().Replace("\"", "");

                rankInfos.Add(rankInfo);
            }

            _SaveToPlayerData();

            _SetText();
        }

        yield break;
    }

    public void OpenCreateUserDialog()
    {
        playerStats = FindObjectOfType<PlayerStats>();

        if (playerStats.userName != "")
        {
            return;
        }

        createUserDialog.GetComponent<UIScaleAnimation>().enabled = true;
        createUserDialog.SetActive(true);
    }

    public void CloseCreateUserDialog()
    {
        createUserDialog.GetComponent<UIScaleAnimation>().enabled = false;
        createUserDialog.SetActive(false);
    }

    public void CreateUserName()
    {
        if(userNameLabel.text == "")
        {
            return;
        }

        if(userNameLabel.text == originText)
        {
            return;
        }

        StartCoroutine(_PostUser());
    }

    IEnumerator _PostUser()
    {
        userExistedNotice.SetActive(false);

        UserData userData = new UserData
        {
            totalCoin = playerStats.coin,
            username = userNameLabel.text
        };

        string userInfoJson = JsonUtility.ToJson(userData);

        UnityWebRequest webRequest = UnityWebRequest.Put(url, userInfoJson);
        webRequest.SetRequestHeader("appPassword", "efoxstudio2019");
        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.method = UnityWebRequest.kHttpVerbPOST;

        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log("www.error - " + webRequest.error + "_Post");
            userExistedNotice.SetActive(true);
        }
        else
        {
            playerStats.userName = userNameLabel.text;
            playerStats.save = true;

            CloseCreateUserDialog();
        }

        StartCoroutine(_GetRankInfoFromWeb());

        yield break;
    }

    IEnumerator _UpdateUser()
    {
        if(playerStats.userName == "")
        {
            yield break;
        }

        UserData userData = new UserData
        {
            totalCoin = playerStats.coin,
            username = playerStats.userName
        };

        string userInfoJson = JsonUtility.ToJson(userData);

        UnityWebRequest webRequest = UnityWebRequest.Put(url, userInfoJson);
        webRequest.SetRequestHeader("appPassword", "efoxstudio2019");
        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.method = UnityWebRequest.kHttpVerbPUT;

        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log("www.error - " + webRequest.error + "_UploadUser");
        }
        else
        {
            Debug.Log("Update user info Complete");
        }

        StartCoroutine(_GetRankInfoFromWeb());

        yield break;
    }
}
