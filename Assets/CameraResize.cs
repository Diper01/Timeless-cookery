using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResize : MonoBehaviour
{
    float targetAspect;
    void Start()
    {
        targetAspect = 1280f / 720f;
        if (Camera.main.aspect < 1.6f)
        {
            // respect width (modify default behavior)
            Camera.main.orthographicSize = Camera.main.orthographicSize * (targetAspect / Camera.main.aspect);
        }
        else
        {
            // respect height (change back to default behavior)
            Camera.main.orthographicSize = Camera.main.orthographicSize;
        }
    }

    
}
