using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StoryBoardController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject scene1, scene2, scene3, scene4, scene5;
    //private GoogleFireBaseEvents googleFireBaseEvents;
    void Start()
    {
        //googleFireBaseEvents = FindObjectOfType<GoogleFireBaseEvents>();
        Time.timeScale = 1;
        //IronSource.Agent.hideBanner();

        scene1.SetActive(true);
        Invoke("toScene2", 7f);
        Invoke("toScene3", 14f);
        Invoke("toScene4", 18f);
        Invoke("toScene5", 21f);
     
    }
    public void toScene2()
    {
        scene1.SetActive(false);
        scene2.SetActive(true);

    }

    public void toScene3()
    {
        scene2.SetActive(false);
        scene3.SetActive(true);

    }

    public void toScene4()
    {
        scene3.SetActive(false);
        scene4.SetActive(true);

    }

    public void toScene5()
    {
        scene4.SetActive(false);
        scene5.SetActive(true);

    }

    public void toStartScene()
    {
        if (!scene5.activeSelf)
        {
            //googleFireBaseEvents.SkipStoryBoard();
        }
        SceneManager.LoadScene("WorldMap");
    }
    void OnApplicationPause(bool isPaused)
    {
        //IronSource.Agent.onApplicationPause(isPaused);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
