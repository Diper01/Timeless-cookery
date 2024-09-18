//using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameController : MonoBehaviour
{
    private PlayerStats playerStats;
    private NextScene nextScene;
    public UILabel lblVersion;
    public string sceneName = "";

    public float loadTime = 2f;
    void Awake()
    {
        //if (FB.IsInitialized)
        //{
        //    FB.ActivateApp();
        //}
        //else
        //{
        //    //Handle FB.Init
        //    FB.Init(() =>
        //    {
        //        FB.ActivateApp();
        //    });
        //}
    }
    //void OnApplicationPause(bool pauseStatus)
    //{

    //    //Check the pauseStatus to see if we are in the foreground
    //    // or background
    //    if (!pauseStatus)
    //    {
    //        //app resume
    //        if (FB.IsInitialized)
    //        {
    //            FB.ActivateApp();
    //        }
    //        else
    //        {
    //            //Handle FB.Init
    //            FB.Init(() =>
    //            {
    //                FB.ActivateApp();
    //            });
    //        }
    //    }
    //    IronSource.Agent.onApplicationPause(pauseStatus);
    //}
    // Start is called before the first frame update
    void Start()
    {
       
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();

        nextScene = GameObject.FindObjectOfType<NextScene>();

        playerStats.appOpen += 1;

        lblVersion.text = "Tavern Cooking v"+Application.version + ". © eFox Studio. All Rights Reserved.";
        Time.timeScale = 1;

        //Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        //Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
    }
    //public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    //{
    //    UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
    //}

    //public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    //{
    //    UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
    //}

    // Update is called once per frame
    void Update()
    {

    }

    public void _PlayButton()
    {
      
        int showStory = PlayerPrefs.GetInt("story", 1);
        if (showStory == 0)
        {
            nextScene.NextLevel(sceneName);
        }
        else
        {
            PlayerPrefs.SetInt("story", 0);
            PlayerPrefs.Save();
            nextScene.NextLevel("StoryBoardScene");
         
        }

    }
}
