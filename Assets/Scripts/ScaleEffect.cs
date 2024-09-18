using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleEffect : MonoBehaviour
{
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (EventSystem.current.IsPointerOverGameObject())
        //    {
        //        return;
        //    }

        //    RaycastHit hitInfo;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out hitInfo))
        //    {
        //        if (hitInfo.transform.gameObject.name == gameObject.name)
        //        {
        //            init();
        //        }
        //    }
        //}
    }

    private void OnMouseDown()
    {
        init();
    }

    float duration = 0.3f;
    public float startDelay = 0.1f;
    public Vector3 scaleTo = new Vector3(1f, 1f, 1f);


    AnimationCurve animationCurve = new AnimationCurve(
        new Keyframe(0f, 0f, 0f, 0.5f),
        new Keyframe(0.7f, 1.2f, 1f, 0.5f),
        new Keyframe(1f, 1f, 1f, 0f));


    public void init()
    {
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        TweenScale tween = TweenScale.Begin(gameObject, duration, scaleTo);
        tween.duration = duration;
        tween.delay = startDelay;

        tween.animationCurve = animationCurve;

    }

}
