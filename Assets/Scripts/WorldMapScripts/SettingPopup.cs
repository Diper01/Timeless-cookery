using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPopup : MonoBehaviour
{
    private PlayerStats playerStats;

    public GameObject bttnSoundOn,bttnSoundOff,bttnMusicOn,bttnMusicOff;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();

        _ActiveButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void _ActiveButton()
    {
        FindObjectOfType<AudioManager>()._CheckAudioStats();

        //bttnSoundOn.SetActive(false);
        //bttnSoundOff.SetActive(false);
        //bttnMusicOn.SetActive(false);
        //bttnMusicOff.SetActive(false);

        if (playerStats.soundOn == true)
        {
            bttnSoundOn.SetActive(true);
            bttnSoundOff.SetActive(false);
        }
        else if (playerStats.soundOn == false)
        {
            bttnSoundOn.SetActive(false);
            bttnSoundOff.SetActive(true);
        }

        if (playerStats.musicOn == true)
        {
            bttnMusicOn.SetActive(true);
            bttnMusicOff.SetActive(false);
        }
        else if (playerStats.musicOn == false)
        {
            bttnMusicOn.SetActive(false);
            bttnMusicOff.SetActive(true);
        }
    }

    public void _SoundOn()
    {
        playerStats.soundOn = true;
        playerStats.save = true;

        _ActiveButton();
    }

    public void _SoundOff()
    {
        playerStats.soundOn = false;
        playerStats.save = true;

        _ActiveButton();
    }

    public void _MusicOn()
    {
        playerStats.musicOn = true;
        playerStats.save = true;
        _ActiveButton();
    }

    public void _MusicOff()
    {
        playerStats.musicOn = false;
        playerStats.save = true;
        _ActiveButton();
    }
}
