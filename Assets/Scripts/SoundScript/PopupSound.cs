using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _PlayMusic();
    }

    void _PlayMusic()
    {
        if (gameObject.activeSelf == true && gameObject.GetComponent<AudioSource>().enabled == false)
        {
            gameObject.GetComponent<AudioSource>().enabled = true;
        }
        //if (gameObject.activeSelf == false && gameObject.GetComponent<AudioSource>().enabled == true)
        //{
        //    gameObject.GetComponent<AudioSource>().enabled = false;
        //}

        if (gameObject.activeSelf == false && gameObject.GetComponent<AudioSource>().enabled == true)
        {
            gameObject.GetComponent<AudioSource>().enabled = false;
        }
    }
}
