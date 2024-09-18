using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterController : MonoBehaviour
{
    private MissionManager missionManager;

    public GameObject mushroomBooster;
    public GameObject waterBooster;
    public GameObject x2CoinBooster;

    private void Start()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();

        if (missionManager.useMushRoom)
        {
            mushroomBooster.SetActive(true);
        }
        if (missionManager.useWater)
        {
            waterBooster.SetActive(true);
        }
        if (missionManager.useX2Gold)
        {
            x2CoinBooster.SetActive(true);
        }

        missionManager.open = false;
        Time.timeScale = 0;

        StartCoroutine(_EffectControll());
    }

    IEnumerator _EffectControll()
    {
        if(mushroomBooster.activeSelf == true)
        {
            float temp = mushroomBooster.GetComponent<MushroomBooster>()._ActiveEffect();
            yield return new WaitForSeconds(temp);
        }
        if(waterBooster.activeSelf == true)
        {
            float temp = waterBooster.GetComponent<WaterBooster>()._ActiveEffect();
            yield return new WaitForSeconds(temp);
        }
        if(x2CoinBooster.activeSelf == true)
        {
            float temp = x2CoinBooster.GetComponent<X2GoldBooster>()._ActiveEffect();
            yield return new WaitForSeconds(temp);
        }

        Time.timeScale = 1;
        missionManager.open = true;
    }
}
