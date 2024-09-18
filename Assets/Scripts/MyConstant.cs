using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyConstant : MonoBehaviour
{
    public static  string  TUTORIAL_KEY="tutorial";
    public static string IN_GAME_TUTORIAL_KEY = "in_game_tutorial";
    public static string GAME_VERSION = "version";
    public static void setTutorialStatus(int tutorialNumber, bool isViewed)
    {
        PlayerPrefs.SetInt(TUTORIAL_KEY + tutorialNumber, isViewed ? 1 : 0);
        PlayerPrefs.Save();
    }
    public static bool isTutorialViewed(int tutorialNumber)
    {
        return PlayerPrefs.GetInt(TUTORIAL_KEY + tutorialNumber,0)==1?true:false;
    }

    public static void setInGameTutorialStatus(int tutorialNumber, bool isViewed)
    {
        PlayerPrefs.SetInt(IN_GAME_TUTORIAL_KEY + tutorialNumber, isViewed ? 1 : 0);
        PlayerPrefs.Save();
    }
    public static bool isInGameTutorialViewed(int tutorialNumber)
    {
        return PlayerPrefs.GetInt(IN_GAME_TUTORIAL_KEY + tutorialNumber, 0) == 1 ? true : false;
    }

    public static string getGameVersion()
    {
        return PlayerPrefs.GetString(GAME_VERSION,"0");
    }
    public static void setGameVersion(string version)
    {    
        PlayerPrefs.SetString(GAME_VERSION, version);
        PlayerPrefs.Save();
    }
}
