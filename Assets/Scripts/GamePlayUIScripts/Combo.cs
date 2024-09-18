using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combo : MonoBehaviour
{
    private MissionManager missionManager;
    private GamePlayMenuManager gamePlayMenuManager;

    public CameraShake cameraShake;

    public Image comboTimerImg;
    public Text comboText;
    public GameObject coinImg;

    public AudioSource comboAudio;
    public AudioSource comboFireAudio;

    public float comboTime = 2;
    public float currentTime = 0;

    public int comboLimit = 5;
    public int combo = 0;
    public int currentCombo = 0;

    public bool isCombo = false;

    // Start is called before the first frame update
    void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        gamePlayMenuManager = GameObject.FindGameObjectWithTag("GamePlayMenuManager").GetComponent<GamePlayMenuManager>();

        currentTime = comboTime;
        comboTimerImg.fillAmount = 0;

        _Deactive();
    }

    // Update is called once per frame
    void Update()
    {
        _SetImage();
        _Timer();
    }

    public void _SetCombo(int comboCoin)
    {
        isCombo = true;

        combo++;

        comboFireAudio.enabled = false;

        if (combo > currentCombo) 
        {
            if (combo > comboLimit)
            {
                combo = comboLimit;
            }
            currentCombo = combo;
            comboText.text = "x" + currentCombo.ToString() + " COMBO";

            if(currentCombo > 1)
            {
                //missionManager.currentPlayerCoin += comboCoin*currentCombo;
                missionManager.currentPlayerCoin += currentCombo;
            }

            currentTime = comboTime;
        }

        if(currentCombo > 1)
        {
            comboFireAudio.enabled = true;
            StartCoroutine(cameraShake._Shake(0.15f, 0.1f));
        }

        if (transform.localScale.x <= 0 && currentCombo > 1)
        {
            _Active();
        }

        _SetCoinImgEffect();
    }

    void _Timer()
    {
        if(isCombo == true)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            else if (currentTime <= 0 && currentCombo > 0)
            {
                isCombo = false;
                combo = 0;
                currentCombo = 0;
                currentTime = comboTime;
                comboText.text = "COMBO";

                if (transform.localScale.x >= 1)
                {
                    _Deactive();
                }
            }
        }
    }

    void _SetImage()
    {
        if (isCombo == true)
        {
            comboTimerImg.fillAmount = (currentTime / comboTime);
          
        }
    }

    void _SetCoinImgEffect()
    {
        coinImg.GetComponent<TweenScale>().ResetToBeginning();
        coinImg.GetComponent<TweenScale>().from = new Vector3(0, 0, 0);
        coinImg.GetComponent<TweenScale>().to = new Vector3(1, 1, 1);
        coinImg.GetComponent<TweenScale>().duration = 0.2f;
        coinImg.GetComponent<TweenScale>().PlayForward();
    }

    void _Active()
    {
        gameObject.GetComponent<TweenScale>().ResetToBeginning();
        gameObject.GetComponent<TweenScale>().from = new Vector3(0, 0, 0);
        gameObject.GetComponent<TweenScale>().to = new Vector3(1, 1, 1);
        gameObject.GetComponent<TweenScale>().duration = 0.2f;
        gameObject.GetComponent<TweenScale>().PlayForward();

        comboFireAudio.enabled = true;
    }

    void _Deactive()
    {
        gameObject.GetComponent<TweenScale>().ResetToBeginning();
        gameObject.GetComponent<TweenScale>().from = new Vector3(1, 1, 1);
        gameObject.GetComponent<TweenScale>().to = new Vector3(0, 0, 0);
        gameObject.GetComponent<TweenScale>().duration = 0.2f;
        gameObject.GetComponent<TweenScale>().PlayForward();

        comboFireAudio.enabled = false;
    }
}
