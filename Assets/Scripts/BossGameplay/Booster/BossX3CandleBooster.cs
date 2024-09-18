using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;

public class BossX3CandleBooster : MonoBehaviour
{
    private MissionManager missionManager;
    private PlayerStats playerStats;

    private SkeletonAnimation x3CandleBooster;

    public AudioSource x3CandleAudio;

    public Text amountText;

    public float freezeTime = 15f;
    public float currentFreezeTime = 0f;
    public bool isFreeze = false;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        missionManager = FindObjectOfType<MissionManager>();
        x3CandleBooster = gameObject.GetComponent<SkeletonAnimation>();

    }

    private void Update()
    {
        //_OnTouch();
        _SetText();
        _SetFreeze();
        _SetUnFreeze();
    }

    void _OnTouch()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit hitInfo;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out hitInfo))
        //    {
        //        if (hitInfo.transform.gameObject.name == gameObject.name)
        //        {
        //            if(playerStats.x3CandleAmount > 0 && isFreeze == false)
        //            {
        //                isFreeze = true;

        //                playerStats.x3CandleAmount -= 1;
        //                playerStats.save = true;

        //                currentFreezeTime = freezeTime;

        //                StartCoroutine(_ActiveTweenScale());

        //                _SetText();
        //            }
        //        }
        //    }
        //}
    }

    public void _ButtonTouch()
    {
        if (playerStats.x3CandleAmount > 0 && isFreeze == false)
        {
            isFreeze = true;

            x3CandleAudio.Play();

            playerStats.x3CandleAmount -= 1;

            playerStats.achivBoosterUse += 1;

            currentFreezeTime = freezeTime;

            StartCoroutine(_ActiveTweenScale());

            _SetText();
        }
    }

    public void _SetText()
    {
        amountText.text = playerStats.x3CandleAmount.ToString();
    }

    IEnumerator _ActiveTweenScale()
    {
        gameObject.GetComponent<TweenScale>().enabled = true;
        yield return new WaitForSeconds(freezeTime);
        gameObject.GetComponent<TweenScale>().enabled = false;
    }

    public void _SetFreeze()
    {
        if (isFreeze == true)
        {
            missionManager.isFreeze = true;
        }
    }

    public void _SetUnFreeze()
    {
        if (isFreeze == true)
        {
            currentFreezeTime -= Time.deltaTime;

            if (currentFreezeTime <= 0f)
            {
                missionManager.isFreeze = false;

                isFreeze = false;
            }
        }
    }
}
