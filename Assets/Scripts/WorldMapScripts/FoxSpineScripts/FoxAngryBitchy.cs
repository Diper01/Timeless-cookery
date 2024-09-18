using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class FoxAngry : MonoBehaviour
{
    private SkeletonAnimation foxSpine;

    // Start is called before the first frame update
    void Start()
    {
        foxSpine = gameObject.GetComponent<SkeletonAnimation>();
        _SetAnimation();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void _SetAnimation()
    {
        int randomTemp = Random.Range(0, 7);

        switch (randomTemp)
        {
            case 0:
                {
                    foxSpine.AnimationState.SetAnimation(1, "beach hat_angry", true);
                    break;
                }
            case 1:
                {
                    foxSpine.AnimationState.SetAnimation(1, "chef hat_angry", true);
                    break;
                }
            case 2:
                {
                    foxSpine.AnimationState.SetAnimation(1, "mushroom hat_angry", true);
                    break;
                }
            case 3:
                {
                    foxSpine.AnimationState.SetAnimation(1, "no hat_angry", true);
                    break;
                }
            case 4:
                {
                    foxSpine.AnimationState.SetAnimation(1, "beach hat_angry", true);
                    break;
                }
            case 5:
                {
                    foxSpine.AnimationState.SetAnimation(1, "chef hat_angry", true);
                    break;
                }
            case 6:
                {
                    foxSpine.AnimationState.SetAnimation(1, "mushroom hat_angry", true);
                    break;
                }
            case 7:
                {
                    foxSpine.AnimationState.SetAnimation(1, "no hat_angry", true);
                    break;
                }
        }
    }
}
