using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWorldPopup : MonoBehaviour
{
    private PlayerStats playerStats;
    private MapController mapController;

    public SelectWorldButton[] selectWorldButtons;

    public void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        mapController = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapController>();

        _SetUnlockWorld();
    }

    public void Update()
    {
        //_SetUnlockWorld();
    }

    public void _SetUnlockWorld()
    {
        for (int i = 0; i < selectWorldButtons.Length; i++)
        {
            selectWorldButtons[i].sortIndex = i;
        }

        //for (int i = 0; i < playerStats.unlockedWorlds; i++)
        //{
        //    selectWorldButtons[i].unlocked = true;
        //}
    }
}