using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStringType : MonoBehaviour
{
    public string testStr = "123,abc";

    // Start is called before the first frame update
    void Start()
    {
        string[] tempStr = testStr.Split(',');
        for(int i=0; i<tempStr.Length; i++)
        {
            Debug.Log("Chuoi la " + tempStr[i]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
