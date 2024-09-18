using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private MissionManager missionManager;
    private PlayerStats playerStats;
    private LevelManager levelManager;
    private ItemManager itemManager;
    private LevelSettingsDataManager levelSettingsDataManager;

    private Vector3 scrollViewOriginPos;

    public int selectedMap = 0;
    public bool change = false;
    //public GameObject centerobject;
    [System.Serializable]
    public class MapData
    {
        public string mapName;
        public GameObject map;
        public int startID = 1;
        public string settingUrl;
    }

    public MapData[] mapDatas;

    private void Awake()
    {
        missionManager = GameObject.FindGameObjectWithTag("MissionManager").GetComponent<MissionManager>();
        playerStats = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStats>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        itemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        levelSettingsDataManager = GameObject.FindObjectOfType<LevelSettingsDataManager>();

        selectedMap = playerStats.currentWorld;

        scrollViewOriginPos = transform.localPosition;

        _SelectMap();
    }

    private void Start()
    {
        //_SelectMap();
        //Invoke("gotocenterobject", 0.5f);
        //gameObject.GetComponent<UIPanel>().transform.localPosition = new Vector3((1280- gameObject.GetComponent<UIPanel>().GetViewSize().x)/2, 0, 0);
      //  SpringPanel.Begin(gameObject.GetComponent<UIScrollView>().panel.cachedGameObject,
      //                                       new Vector3((1280 - gameObject.GetComponent<UIPanel>().GetViewSize().x) / 2, 0, 0), 10f);
    }
    void gotocenterobject()
    {
        //Debug.Log("gotocenterobject");
        //GetComponent<UICenterOnChild>().CenterOn(centerobject.transform);
    }
    private void Update()
    {
        if(change == true)
        {
            change = false;
            _SelectMap();
            playerStats.currentWorld = selectedMap;
            playerStats.save = true;
        }
    }

    IEnumerator _SetLevelController()
    {
        yield return new WaitForSeconds(0.125f);

        GameObject[] levelControllers = GameObject.FindGameObjectsWithTag("LevelController");

        for(int i=0; i<levelControllers.Length; i++)
        {
            levelControllers[i].GetComponent<LevelController>()._SetDeactiveImage();
            levelControllers[i].GetComponent<LevelController>()._SetActiveImage();
        }

        yield break;
    }

    public IEnumerator _SetScrollViewCenter(float xValue, LevelController levelController)
    {
        yield return new WaitForSeconds(0.1f);
     //   SpringPanel.Begin(gameObject.GetComponent<UIScrollView>().panel.cachedGameObject,
    //                                 new Vector3((1280 - gameObject.GetComponent<UIPanel>().GetViewSize().x) / 2, 0, 0), 5f);

        gameObject.GetComponent<TweenPosition>().enabled = true;
        gameObject.GetComponent<TweenPosition>().from = transform.localPosition;
        gameObject.GetComponent<TweenPosition>().to = new Vector2((1280 - gameObject.GetComponent<UIPanel>().GetViewSize().x) / 2, 0);
        gameObject.GetComponent<TweenPosition>().duration = 0.1f;
        gameObject.GetComponent<TweenPosition>().PlayForward();

        yield return new WaitForSeconds(gameObject.GetComponent<TweenPosition>().duration);
        gameObject.GetComponent<UIPanel>().leftAnchor.absolute = 0;
        gameObject.GetComponent<UIPanel>().rightAnchor.absolute = 0;
       gameObject.GetComponent<TweenPosition>().enabled = false;


        yield return new WaitForSeconds(0.5f);
        //  SpringPanel.Begin(GameObject.Find("MapVillage"), Vector3.right, 0.1f);
        //   gameObject.GetComponent<UIPanel>().clipOffset = new Vector2(1f, 0f);
     //   Debug.Log("gameObject.GetComponent<UIPanel>().GetViewSize().x  " + gameObject.GetComponent<UIPanel>().GetViewSize().x);
      //  gameObject.GetComponent<UIPanel>().transform.localPosition = new Vector3((1280- gameObject.GetComponent<UIPanel>().GetViewSize().x)/2, 0, 0);
      //  gameObject.GetComponent<UIPanel>().clipOffset = Vector2.zero;
     //   SpringPanel.Begin(gameObject.GetComponent<UIScrollView>().panel.cachedGameObject,
     //                                           new Vector3((1280 - gameObject.GetComponent<UIPanel>().GetViewSize().x) / 2, 0, 0), 10f);
        //  SpringPanel.Begin(GameObject.Find("MapVillage"), Vector3.right, 0.1f);
        if (xValue > 568f)
        {
            Vector3 dirPos = new Vector3(transform.localPosition.x - xValue, transform.localPosition.y, transform.localPosition.z);

            //gameObject.GetComponent<UIPanel>().clipOffset = new Vector2((xValue), 0f);
          
            gameObject.GetComponent<TweenPosition>().enabled = true;
            gameObject.GetComponent<TweenPosition>().from = transform.localPosition;
            gameObject.GetComponent<TweenPosition>().to = dirPos;
            gameObject.GetComponent<TweenPosition>().duration = 0.25f;
            gameObject.GetComponent<TweenPosition>().PlayForward();

            yield return new WaitForSeconds(gameObject.GetComponent<TweenPosition>().duration);
            gameObject.GetComponent<UIPanel>().leftAnchor.absolute = 0;
            gameObject.GetComponent<UIPanel>().rightAnchor.absolute = 0;
            gameObject.GetComponent<TweenPosition>().enabled = false;
        }

        yield break;
    }

    void _SelectMap()
    {
        // Deactivate everyone, then just activate the one.
        for (int i = 0; i < mapDatas.Length; i++)
        {
            if (i != mapDatas.Length)
            {
                mapDatas[i].map.SetActive(false);
            }
        }
        mapDatas[selectedMap].map.SetActive(true);

        levelManager.startId = mapDatas[selectedMap].startID;

        levelManager.mapName = mapDatas[selectedMap].mapName;
        levelManager._SetMap();

        itemManager.mapName = mapDatas[selectedMap].mapName;
        itemManager._SetMapItem();

        if (levelSettingsDataManager != null)
        {
            missionManager.currentWorld = selectedMap;

            levelSettingsDataManager.mapName = mapDatas[selectedMap].mapName;
            levelSettingsDataManager._SetlevelSettings();

            //levelSettingsDataManager._SetToLevelSettingsData();

            levelSettingsDataManager.url = mapDatas[selectedMap].settingUrl;
            //StartCoroutine(levelSettingsDataManager.RequestWeb());
        }

        gameObject.GetComponent<UIScrollView>().ResetPosition();

        StartCoroutine(_SetLevelController());
    }
}
