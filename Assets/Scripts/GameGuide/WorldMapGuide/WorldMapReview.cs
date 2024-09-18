using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapReview : MonoBehaviour
{
    private LevelManager levelManager;

    public GameObject scrollView;
    public WorldMapGuideController worldMapGuideController;

    public Vector3 startPos;
    public Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        StartCoroutine(_Review());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator _Review()
    {
        scrollView.gameObject.GetComponent<TweenPosition>().ResetToBeginning();
        scrollView.gameObject.GetComponent<TweenPosition>().from = startPos;
        scrollView.gameObject.GetComponent<TweenPosition>().to = endPos;
        scrollView.gameObject.GetComponent<TweenPosition>().duration = 10f;
        scrollView.gameObject.GetComponent<TweenPosition>().enabled = true;
        scrollView.gameObject.GetComponent<TweenPosition>().PlayForward();

        yield return new WaitForSeconds(scrollView.gameObject.GetComponent<TweenPosition>().duration);

        scrollView.gameObject.GetComponent<UIPanel>().leftAnchor.absolute = 0;
        scrollView.gameObject.GetComponent<UIPanel>().rightAnchor.absolute = 0;
        scrollView.gameObject.GetComponent<TweenPosition>().enabled = false;
        scrollView.transform.localPosition = startPos;
        worldMapGuideController._NextStep();
        levelManager.save = true;

        yield break;
    }
}
