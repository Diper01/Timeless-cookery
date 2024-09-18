using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using System;

public class FoxBalloon : MonoBehaviour
{
    private PlayerStats playerStats;
    //AdManager adManager;
    public WorldMapController worldMapController;
    public SkeletonAnimation foxSpine;

    public Image rewardImage;
    public Sprite coinSprite, crystalSprite, energySprite;

    public LevelController activeLevel;

    public int coinReward = 0;
    public int energyReward = 0;
    public int crystalReward = 0;

    public Vector3 firstPos;
    public Vector3 secondPos;
    private Vector3 targetPos;

    public bool claimed = false;
    public bool poped = false;

    public bool rightFace;
    public int timeForNewBallon=3;

    private float moveSpeed = 0.25f;

    private int rewardIndex;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        //adManager = FindObjectOfType<AdManager>();

        _ChooseRandomReward();

        //if (transform.localPosition.x > secondPos.x)
        //{
        //    rightFace = false;
        //}

        targetPos = firstPos;

        gameObject.GetComponent<TweenPosition>().enabled = false;

        _CheckTime();
        //if (!IronSource.Agent.isRewardedVideoAvailable())
        //{
        //    gameObject.SetActive(false);
        //}
    
    }

    // Update is called once per frame
    void Update()
    {
        _CheckPos();
        //_ActiveDialogFreeGift();
    }

    void _CheckTime()
    {
        DateTime lastTime = DateTime.Parse(playerStats.foxTime);

        DateTime now = DateTime.Now;

        TimeSpan time = now - lastTime;

        int totalMinutes = (int)time.TotalMinutes;

        if (totalMinutes >= timeForNewBallon)
        {
            playerStats.foxTouched = false;

            PlayAnimationFly();

            _CheckPos();
        }

        if (playerStats.foxTouched == true || activeLevel.currentStar < 1)
        {
            gameObject.SetActive(false);
        }
    }

    void _CheckPos()
    {
        if (transform.localPosition.x <= firstPos.x)
        {
            if (rightFace == false)
            {
                rightFace = true;

                Vector3 scale = new Vector3(-foxSpine.transform.localScale.x, foxSpine.transform.localScale.y, foxSpine.transform.localScale.z);

                foxSpine.transform.localScale = scale;

                targetPos = secondPos;
            }
        }
        else
        if (transform.localPosition.x >= secondPos.x)
        {
            if (rightFace == true)
            {
                rightFace = false;

                Vector3 scale = new Vector3(-foxSpine.transform.localScale.x, foxSpine.transform.localScale.y, foxSpine.transform.localScale.z);

                foxSpine.transform.localScale = scale;

                targetPos = firstPos;
            }
        }

        _Move();
    }

    void _Move()
    {
        float step = moveSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
    }

    void PlayAnimationFly()
    {
        foxSpine.AnimationState.SetAnimation(0, "no hat_fly", true);
    }

    public void StartAnimationPop()
    {
        StartCoroutine(PlayAnimationPop());
    }

    IEnumerator PlayAnimationPop()
    {
        rewardImage.gameObject.SetActive(false);

        foxSpine.AnimationState.SetAnimation(0, "no hat_pop", false);

        var myAnim = foxSpine.GetComponent<SkeletonAnimation>().skeleton.Data.FindAnimation("no hat_pop");

        gameObject.GetComponent<TweenPosition>().enabled = false;

        yield return new WaitForSeconds(myAnim.Duration);

        gameObject.GetComponent<TweenPosition>().enabled = true;

        Vector3 dropPos = new Vector3(transform.localPosition.x, -1.5f, transform.localPosition.z);
        
        gameObject.GetComponent<TweenPosition>().from = transform.localPosition;
        gameObject.GetComponent<TweenPosition>().to = dropPos;
        gameObject.GetComponent<TweenPosition>().duration = 1f;
        gameObject.GetComponent<TweenPosition>().PlayForward();
        yield return new WaitForSeconds(gameObject.GetComponent<TweenPosition>().duration);

        gameObject.GetComponent<TweenPosition>().enabled = false;

        gameObject.SetActive(false);
    }

    public void _OnTouch()
    {
        gameObject.GetComponent<AudioSource>().enabled = true;

        moveSpeed = 0f;

        _SetRandomReward();

        playerStats.foxTouched = true;
        playerStats.foxTime = DateTime.Now.ToString();

        worldMapController.dialogFreeGift.GetComponent<DialogFreeGift>().foxBalloon = this;
        worldMapController.openDialogFreeGift();

        playerStats.achivBallonPoper += 1;

        playerStats.save = true;
    }

    void _ActiveDialogFreeGift()
    {
        /*if (adManager.rewarded == true && adManager.reward_placement == AdManager.REWARD_AD_PLACEMENT.FOX_BALOON)
        {
            adManager.rewarded = false;

            playerStats.foxTouched = true;
            playerStats.foxTime = DateTime.Now.ToString();

            worldMapController.dialogFreeGift.GetComponent<DialogFreeGift>().foxBalloon = this;
            worldMapController.openDialogFreeGift();

            playerStats.achivBallonPoper += 1;

            playerStats.save = true;
        }

        else if (playerStats.foxTouched == true && claimed == true)
        {
            claimed = false;

            StartCoroutine(PlayAnimationPop());
        }

        else if (adManager.rewardAdClosed == true
            && gameObject.GetComponent<TweenPosition>().enabled == false
            && worldMapController.dialogFreeGift.activeSelf == false)
        {
            adManager.rewardAdClosed = false;

            gameObject.GetComponent<TweenPosition>().enabled = true;
        }*/
    }

    private void _ChooseRandomReward()
    {
        /*rewardIndex = UnityEngine.Random.Range(0, playerStats.energy < 10 ? 2 : 1);

        switch (rewardIndex)
        {
            case 0:
                rewardImage.sprite = coinSprite; break;
            case 1:
                rewardImage.sprite = crystalSprite; break;
            case 2:
                rewardImage.sprite = energySprite; break;
        }
        */
    }

    void _SetRandomReward()
    {
        //int rdIndex = UnityEngine.Random.Range(0, playerStats.energy < 10 ? 2 : 1);

        switch (rewardIndex)
        {
            case 0:
                {
                    worldMapController.dialogFreeGift.GetComponent<DialogFreeGift>().rewardType = REWARD_TYPE.Coin;
                    worldMapController.dialogFreeGift.GetComponent<DialogFreeGift>().rewardAmount = coinReward;
                    break;
                }
            case 1:
                {
                    worldMapController.dialogFreeGift.GetComponent<DialogFreeGift>().rewardType = REWARD_TYPE.Crystal;
                    worldMapController.dialogFreeGift.GetComponent<DialogFreeGift>().rewardAmount = crystalReward;
                    break;
                }
            case 3:
                {                    
                    worldMapController.dialogFreeGift.GetComponent<DialogFreeGift>().rewardType = REWARD_TYPE.Energy;
                    worldMapController.dialogFreeGift.GetComponent<DialogFreeGift>().rewardAmount = energyReward;
                    break;
                }
        }
    }
}
