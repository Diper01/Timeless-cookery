using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    private NextScene nextScene;

    public string sceneName = "";

    public float loadTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        nextScene = FindObjectOfType<NextScene>();

        sceneName = nextScene.sceneName;

        Time.timeScale = 1;
        //IronSource.Agent.displayBanner();
        StartCoroutine(_LoadScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator _LoadScene()
    {
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadSceneAsync(sceneName);

        yield break;
    }

    //IEnumerator LoadAsynchronously()
    //{
    //    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
    //    yield return null;
    //}
}
