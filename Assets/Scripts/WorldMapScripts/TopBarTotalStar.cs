using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TopBarTotalStar : MonoBehaviour
{
    private LevelManager levelManager;

    public UILabel uILabel;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        uILabel.text = String.Format("{0:00}", levelManager.totalStar);
    }
}
