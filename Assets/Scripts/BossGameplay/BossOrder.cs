using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOrder : MonoBehaviour
{
    public BossOrderPos[] bossOrderPos;
    public int maxOrderSize = 1;
    public int currentOrderSize = 0;

    // Start is called before the first frame update
    void Start()
    {
        maxOrderSize = bossOrderPos.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
