using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class WaterSplash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_PlayAnim());
    }

    IEnumerator _PlayAnim()
    {
        float temp = Random.RandomRange(0f, 3.5f);
        yield return new WaitForSeconds(temp);

        gameObject.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "1", true);
    }
}
