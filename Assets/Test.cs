using System;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string inStr = "123nab";
        string outStr = "";

        for(int i=0; i<inStr.Length; i++)
        {
            int tempInt = 0;
            if (int.TryParse(inStr[i].ToString(), out tempInt))
            {
                outStr = outStr + tempInt.ToString();
            }
        }

        int outInt = int.Parse(outStr);

        Debug.Log("String lad: " + outInt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
