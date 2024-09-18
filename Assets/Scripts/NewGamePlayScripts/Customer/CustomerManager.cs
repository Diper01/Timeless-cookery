using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    private PlayerStats playerStats;
    private MissionManager missionManager;

    public GamePlayMenuManager gamePlayMenuManager;

    public float customerSpawnTime = 5f;
    public float currentTime = 0f;
    public int customerCount = 0;
    public int currentCustomerCount = 0;
    public int customerPosAmount = 4;

    public int orderOkCount = 0;

    private int id = 0;
    private string defaultName;

    public int customerLimit = 0;

    public GameObject[] customerPrefabs;

    public GameObject[] spawnPoints;

    private GameObject[] customers;

    public bool canSpawn = true;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        gamePlayMenuManager = GameObject.FindGameObjectWithTag("GamePlayMenuManager").GetComponent<GamePlayMenuManager>();

        customerSpawnTime = missionManager.customerSpawnTime;
        customerLimit = missionManager.customerLimit;

        currentTime = customerSpawnTime;
    }

    void Update()
    {
        if (gamePlayMenuManager.win == true || gamePlayMenuManager.lose == true)
        {
            return;
        }
            _SetValuesToMissionManager();
            if (missionManager.close == false && missionManager.open == true
                || missionManager.close == true && missionManager.open == true
                && missionManager.customerLeft > 0 && missionManager.firstTarget == MissionManager.FirstTarget.Customer
                || missionManager.close == true && missionManager.open == true
                && missionManager.currentTime > 0 && missionManager.firstTarget == MissionManager.FirstTarget.Time)
            {
                if (missionManager.firstTarget == MissionManager.FirstTarget.Customer && customerCount >= customerLimit && customerLimit > 0)
                {
                    return;
                }
                else if (missionManager.firstTarget == MissionManager.FirstTarget.Customer && customerCount < customerLimit)
                {
                    _Timer();
                }
                else if (missionManager.firstTarget != MissionManager.FirstTarget.Customer || customerLimit <= 0)
                {
                    _Timer();
                }
            }
    }

    void _SetValuesToMissionManager()
    {
        missionManager.currentCustomerCount = currentCustomerCount;
        missionManager.customerCount = customerCount;
        missionManager.customerLeft = customerLimit - customerCount;
    }

    void _Timer()
    {
        if(currentCustomerCount == 0 && canSpawn == true)
        {
            canSpawn = false;
            _SpawnCustomer();
        }

        if (currentCustomerCount > 0 && currentCustomerCount < customerPosAmount )
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                _SpawnCustomer();
                canSpawn = false;
                currentTime = customerSpawnTime;
            }
        }
    }

    void _SpawnCustomer()
    {
        customerCount++;

        int randomCustomer = Random.Range(0, customerPrefabs.Length);
        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        Instantiate(customerPrefabs[randomCustomer], spawnPoints[randomSpawnPoint].transform.position, Quaternion.identity);
    }
}
