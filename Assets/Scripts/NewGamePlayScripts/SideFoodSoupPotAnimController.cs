using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SideFoodSoupPotAnimController : MonoBehaviour
{
    private SideFoodController SideFoodController;
    private SkeletonAnimation soupPotSpine;

    public int animStats = 0;

    // Start is called before the first frame update
    void Start()
    {
        SideFoodController = gameObject.GetComponent<SideFoodController>();

        soupPotSpine = gameObject.GetComponent<SkeletonAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        _PlayAnimation();
        _PlayAudio();
    }

    void _PlayAnimation()
    {
        if (SideFoodController.isProcessing == false && animStats == 0)
        {
            animationNoiKhongThia();
            animStats = 1;
        }
        else if (SideFoodController.isProcessing == true && animStats == 1)
        {
            animationNoi();
            animStats = 0;
        }
    }

    void _PlayAudio()
    {
        if (gameObject.GetComponent<AudioSource>() == null)
        {
            return;
        }

        if (SideFoodController.isProcessing == true && gameObject.GetComponent<AudioSource>().enabled == false)
        {
            gameObject.GetComponent<AudioSource>().enabled = true;
        }
        else if (SideFoodController.isProcessing == false && gameObject.GetComponent<AudioSource>().enabled == true)
        {
            gameObject.GetComponent<AudioSource>().enabled = false;
        }
    }

    public void animationNoiKhongThia()
    {
        if (soupPotSpine == null)
        {
            return;
        }

        soupPotSpine.AnimationState.SetAnimation(0, "noi " + SideFoodController.lv + "(khong thia)", true);
    }

    public void animationNoi()
    {
        if (soupPotSpine == null)
        {
            return;
        }

        soupPotSpine.AnimationState.SetAnimation(0, "noi " + SideFoodController.lv, true);
    }
}
