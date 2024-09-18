using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRootResize : MonoBehaviour
{
    float targetAspect;
    // Start is called before the first frame update
    void Start()
    {
        targetAspect = 1280f / 720f;
        if (Camera.main.aspect < 1.6f)
        {
            // respect width (modify default behavior)
            GetComponent<UIRoot>().fitHeight = true;
            GetComponent<UIRoot>().fitWidth = false;
        }
        else
        {
            // respect height (change back to default behavior)
            Camera.main.orthographicSize = Camera.main.orthographicSize;
            GetComponent<UIRoot>().fitHeight = false;
            GetComponent<UIRoot>().fitWidth = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
