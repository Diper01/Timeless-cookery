using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    private PlayerStats playerStats;
    private MissionManager missionManager;

    public AudioMixer masterMixer;

    public Sound[] sounds;

    // Start is called before the first frame update
    private void Awake()
    {
        //_MakeSingleInstance();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = masterMixer.FindMatchingGroups(s.mixerGroup)[0];
            s.source.volume = s.volume;
            //s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void _MakeSingleInstance()
    {
        int numInstance = GameObject.FindObjectsOfType<AudioManager>().Length;
        if (numInstance > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats")?.GetComponent<PlayerStats>();
        missionManager = GameObject.FindGameObjectWithTag("MissionManager")?.GetComponent<MissionManager>();

        if (playerStats == null)
        {
            Debug.LogError("PlayerStats object not found in the scene.");
            return;
        }

        if (missionManager == null)
        {
            Debug.LogError("MissionManager object not found in the scene.");
            return;
        }

        _CheckAudioStats();
    }


    private void Update()
    {
        _GetTouch();
    }

    public void _SetMusic(float musicVol)
    {
        masterMixer.SetFloat("musicVol", musicVol);
    }
    
    public void _SetUIFX(float UIFxVol)
    {
        masterMixer.SetFloat("UIFxVol", UIFxVol);
    }

    public void _SetGameplayFX(float gameplayFxVol)
    {
        masterMixer.SetFloat("gameplayFxVol", gameplayFxVol);
    }

    public void _Pause()
    {
        _SetMusic(-80f);
        _SetGameplayFX(-80f);
    }

    public void _UnPause()
    {
        if (playerStats.musicOn)
        {
            _SetMusic(0f);
        }
        if (playerStats.soundOn)
        {
            _SetGameplayFX(0f);
        }
    }

    public void _CheckAudioStats()
    {
        Debug.Log("playerStats " + playerStats);
        if (playerStats.musicOn)
        {
            _SetMusic(0f);
        }
        else if (!playerStats.musicOn)
        {
            _SetMusic(-80f);
        }

        if (playerStats.soundOn)
        {
            _SetUIFX(0f);
            _SetGameplayFX(0f);
        }
        else if (!playerStats.soundOn)
        {
            _SetUIFX(-80f);
            _SetGameplayFX(-80f);
        }

    }

    public void _PLay(string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            return;
        }
        s.source.Play();
    }

    public int _IsPlaying(string name)
    {
        // = 0 sound is null, =1 sound is playing, =2 sond is not playing

        Sound s = Array.Find(sounds, sound => sound.name == name);

        int playing = 0;

        if (s != null)
        {
            if(s.source.isPlaying == true)
            {
                playing = 1;
            }
            else if (s.source.isPlaying == false)
            {
                playing = 2;
            }
        }

        return playing;
    }

    public float _AudioTime(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        float time = 0.0f;

        if (s != null)
        {
            time = s.source.time;
        }

        return time;
    }

    public float _CurrentAudioTime(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        float time = 0.0f;

        if (s != null)
        {
            time = s.source.timeSamples;
        }

        return time;
    }

    //public void _ThemeMusic()
    //{
    //    if(playerStats.musicOn == true)
    //    {
    //        if (missionManager.scenePos == 0)
    //        {
    //            _WorldMapSound();
    //        }
    //        else if (missionManager.scenePos == 1)
    //        {
    //            _GameplaySound();
    //        }
    //        else if (missionManager.scenePos == 2)
    //        {
    //            _UpgradeShopSound();
    //        }
    //    }
    //}

    public void _GetTouch()
    {
        if (playerStats.soundOn == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (missionManager.open == false)
                {
                    _UITapSound();
                }
                else if (missionManager.open == true)
                {
                    _GameplayTapSound();
                }
            }
        }
    }

    public void _UITapSound()
    {
        _PLay("UITap");
    }

    public void _GameplayTapSound()
    {
        _PLay("GameplayTap");
    }

    //public void _PopupOnSound()
    //{
    //    if (playerStats.soundOn == true)
    //    {
    //        _PLay("PopupOn");
    //    }
    //}

    //public void _WorldMapSound()
    //{
    //    _PLay("WorldMap");
    //}
    //public void _StopWorldMapSound()
    //{
    //    _Stop("WorldMap");
    //}

    //public void _GameplaySound()
    //{
    //    _PLay("Gameplay");
    //}
    //public void _StopGameplaySound()
    //{
    //    _Stop("Gameplay");
    //}
    //public void _PauseGameplaySound()
    //{
    //    _Pause("Gameplay");
    //}
    //public void _UnPauseGameplaySound()
    //{
    //    _UnPause("Gameplay");
    //}

    //public void _UpgradeShopSound()
    //{
    //    _PLay("Shop");
    //}

    //public void _WinPopup()
    //{
    //    _PLay("Win");
    //}

    //public void _LosePopup()
    //{
    //    _PLay("Lose");
    //}

    //public void _CoinSound()
    //{
    //    _PLay("Coin");
    //}
}
