using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SideFoodPanAnimController : MonoBehaviour
{
    public Grill grill;

    public Animator panAnimator;
    public Animator foodOnGrillAnimator;

    SkeletonAnimation spineAnim;

    // Start is called before the first frame update
    void Start()
    {
        spineAnim = gameObject.GetComponent<SkeletonAnimation>();
        panAnimator = gameObject.GetComponent<Animator>();

        StartCoroutine(_DeactivePanImg());
    }

    IEnumerator _DeactivePanImg()
    {
        yield return new WaitForSeconds(0.15f);
        if (grill.isUnlocked == false)
        {
            gameObject.SetActive(false);
        }
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        _PlayAnimation();
    }

    void _PlayAnimation()
    {
        if(grill.isEmpty == false)
        {
            Spine.Animation anim = gameObject.GetComponent<SkeletonAnimation>().skeleton.Data.FindAnimation("animation");

            if (spineAnim.state.GetCurrent(0).Animation != anim)
            {
                spineAnim.AnimationState.SetAnimation(0, "animation", true);
                panAnimator.Play("Pan", 0);
                foodOnGrillAnimator.Play("FoodOnPanAnim", 0);
            }
        }

        else if(grill.isEmpty == true)
        {
            spineAnim.AnimationState.SetEmptyAnimation(0, 0.2f);
            panAnimator.Rebind();
            foodOnGrillAnimator.Rebind();
        }
    }
}
