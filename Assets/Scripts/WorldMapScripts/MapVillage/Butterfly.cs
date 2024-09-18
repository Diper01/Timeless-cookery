using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Butterfly : MonoBehaviour
{
    public bool isStopped = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_PlayAnim());
    }

    private void Update()
    {
        if(isStopped == true)
        {
            isStopped = false;
            gameObject.GetComponent<Animator>().enabled = false;
            StartCoroutine(_PlayAnim());
        }
    }

    IEnumerator _PlayAnim()
    {
        float temp = 0f;
        temp = Random.Range(0.0f, 1.0f);
        yield return new WaitForSeconds(temp);

        gameObject.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "animation", true);

        temp = 0f;
        temp = Random.Range(1.0f, 5.0f);
        yield return new WaitForSeconds(temp);

        gameObject.GetComponent<Animator>().enabled = true;
    }

    public void ButterflyAnimStopEvent()
    {
        isStopped = true;
    }
}
