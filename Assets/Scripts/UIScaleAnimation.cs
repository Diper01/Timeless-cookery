using UnityEngine;
using System.Collections;

public class UIScaleAnimation : MonoBehaviour {

	void OnEnable()
	{
		open();
	}

	public void open()
	{
		init();
		
	}
	
	float duration = 0.3f;
	float startDelay = 0.1f;
	public Vector3 scaleTo = new Vector3(1f, 1f, 1f);
	
	
	AnimationCurve animationCurve = new AnimationCurve(
		new Keyframe(0f, 0f, 0f, 1f), 
		new Keyframe(0.7f, 1.2f, 1f, 1f), 
		new Keyframe(1f, 1f, 1f, 0f)); 
	

	void init()
	{
		gameObject.transform.localScale = new Vector3(0, 0, 0);
		TweenScale tween = TweenScale.Begin(gameObject, duration, scaleTo);
		tween.duration = duration;
		tween.delay = startDelay;

		tween.animationCurve = animationCurve;

	}
}
