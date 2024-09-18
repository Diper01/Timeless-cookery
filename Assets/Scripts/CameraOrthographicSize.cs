using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrthographicSize : MonoBehaviour
{
    [SerializeField] private float apect = 1280f;
    [SerializeField] private float horizontal = 720f;
    void Start()
    {
        float windowaspect = (float)Screen.width / (float)Screen.height;
        Camera.main.orthographicSize = 3.6f * ((apect/horizontal) / Camera.main.aspect);
        Debug.Log("windowaspect "+ windowaspect);
        Debug.Log("Camera.main.aspect "+ Camera.main.aspect);

    }

    // Update is called once per frame
    void Update()
    {
        float windowaspect = (float)Screen.width / (float)Screen.height;
        Camera.main.orthographicSize = 3.6f * ((1280f/720f) / Camera.main.aspect);
        Debug.Log("windowaspect "+ windowaspect);
        Debug.Log("Camera.main.aspect "+ Camera.main.aspect); 
    }
}
