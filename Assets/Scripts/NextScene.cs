using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    //private GoogleFireBaseEvents googleFireBaseEvents;

    public string sceneName = "";

    private void Awake()
    {
        _MakeSingleInstance();
    }

    void _MakeSingleInstance()
    {
        int numInstance = GameObject.FindGameObjectsWithTag("NextScene").Length;
        if (numInstance > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        //googleFireBaseEvents = FindObjectOfType<GoogleFireBaseEvents>();
    }

    public void NextLevel(string scene)
    {
        sceneName = scene;

        //googleFireBaseEvents.LogSceneNameEvent(sceneName);

        SceneManager.LoadScene("LoadingScene");
    }
}

