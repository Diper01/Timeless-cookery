using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGameplay : MonoBehaviour
{
    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();

        _ActiveSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _ActiveSound()
    {
        if(playerStats.soundOn == true)
        {
            AudioSource[] audios = gameObject.GetComponents<AudioSource>();

            for (int i = 0; i < audios.Length; i++)
            {
                audios[i].enabled = true;
            }
        }
    }

    public void _DeactiveSound()
    {
        if(playerStats.soundOn == true)
        {
            AudioSource[] audios = gameObject.GetComponents<AudioSource>();

            for (int i = 0; i < audios.Length; i++)
            {
                audios[i].enabled = false;
            }
        }
    }
}
