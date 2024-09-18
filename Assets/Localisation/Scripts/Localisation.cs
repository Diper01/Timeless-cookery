using UnityEngine;
using System;
using System.Collections.Generic;
using System.Xml;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif


public static class Localisation
{
    public static event Action LanguageChanged; // 

    public static SystemLanguage CurrentLanguage = SystemLanguage.English;

    private static Dictionary<string, string> stringsDict;
    private static XmlDocument languageXMLFile;
    private static TextAsset languageAsset;

    private static bool isLanguageLoaded = false;
    public static bool IsLoaded() => isLanguageLoaded;

    public static TMP_FontAsset arabicFont;

    public static string PlayerPrefsKey { get; private set; } = "GameLanguage";
    public static void DetectLanguage()
    {
        CurrentLanguage = GetSavedLanguage();
        //CurrentLanguage = Application.systemLanguage;
        Debug.Log(CurrentLanguage);


#if UNITY_EDITOR
        if (EditorPrefs.HasKey("TestLanguage"))
        {
            CurrentLanguage = (SystemLanguage)Enum.Parse(typeof(SystemLanguage), EditorPrefs.GetString("TestLanguage"));
        }
#endif

        Debug.Log("LoadLanguage: " + CurrentLanguage);
    }
    public static TMP_FontAsset ArabicFont
    {
        get
        {
            return Resources.Load<TMP_FontAsset>("Universal Fonts/ArabicFont");
        }
        private set
        {

        }
    }
    public static TMP_FontAsset DefaultFont
    {
        get
        {
            return Resources.Load<TMP_FontAsset>("Universal Fonts/DefaultFont");
        }
        private set
        {

        }
    }


    public static void LoadLanguage()
    {
        languageXMLFile = new XmlDocument();
        stringsDict = new Dictionary<string, string>();

        arabicFont = Resources.Load<TMP_FontAsset>("Universal Fonts/ArabicFont");
        languageAsset = Resources.Load<TextAsset>("Localisation/" + CurrentLanguage);

        if (languageAsset == null) //if no localisation as system language, load english
        {
            Debug.LogErrorFormat("Файл с языком {0} не найден, загружен стандартный: English", CurrentLanguage);
            languageAsset = Resources.Load<TextAsset>("Localisation/English");
        }

        languageXMLFile.LoadXml(languageAsset.text);

        XmlElement root = languageXMLFile.DocumentElement;
        XmlNodeList nodes = root.SelectNodes("//string"); //ignore all nodes but <string> 

        foreach (XmlNode node in nodes)
        {
            string key = node.Attributes["name"].Value;
            if (!stringsDict.ContainsKey(key))
            {
                stringsDict.Add(key, node.InnerText);
            }
            else
            {
                Debug.LogErrorFormat("Alias '{0}' уже существует в файле {1}", key, CurrentLanguage);
                //throw new ArgumentException("An element with'"+key+"' key already exists in the dictionary");\
            }
        }
        isLanguageLoaded = true;
        SaveLanguage(CurrentLanguage);
        LanguageChanged?.Invoke();
    }

    public static string GetString(string searchString, UnityEngine.Object context = null)
    {
        if (!isLanguageLoaded)
        {
            DetectLanguage();
            LoadLanguage();
        }

        if (stringsDict.ContainsKey(searchString))
        {
            return stringsDict[searchString];
        }
        else
        {
            Debug.LogError("Unknown string: '" + searchString + "'", context);
            return "^" + searchString;
        }

    }

    public static SystemLanguage GetSavedLanguage()
    {
        //return SystemLanguage.French;
        if (PlayerPrefs.HasKey(PlayerPrefsKey))
        {
            CurrentLanguage = (SystemLanguage)PlayerPrefs.GetInt(PlayerPrefsKey);
            Debug.Log(CurrentLanguage);
            return CurrentLanguage;
        }

        CurrentLanguage = Application.systemLanguage;
        //CurrentLanguage = SystemLanguage.Ukrainian;
        return CurrentLanguage;
    }
    private static void SaveLanguage(SystemLanguage language)
    {
        PlayerPrefs.SetInt(PlayerPrefsKey, (int)language);
    }

    public static string GetStringWithValues(string searchString, params string[] values)
    {
        return string.Format(Localisation.GetString(searchString), values);
    }

    [RuntimeInitializeOnLoadMethod]
    private static void InitializeLocalisation()
    {
        CurrentLanguage = GetSavedLanguage();
        LoadLanguage();
    }
}