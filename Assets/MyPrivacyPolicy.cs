using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPrivacyPolicy : MonoBehaviour
{
    public void disAgree()
    {
        Application.Quit();
    }
    public void agree()
    {
        PlayerPrefs.SetInt("PolicyAgreed", 1);
        PlayerPrefs.Save();
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("PolicyAgreed", 0) == 1) gameObject.SetActive(false);
        else gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
