using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogStoreHandle : MonoBehaviour
{
    public int storeToOpen = 0;
    public GameObject diamondPage, boostPage, bundlePage;
    public UIButton buttonBundle,buttonBoost,buttonDiamond;
    // Start is called before the first frame update
    void Start()
    {
        if (storeToOpen == 0)
            openBundlePage();
        else
            openDiamondPage();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openDiamondPage()
    {
       
        buttonBundle.defaultColor = Color.gray;
        buttonBoost.defaultColor = Color.gray;
        buttonDiamond.defaultColor = Color.white;
        diamondPage.SetActive(true);
        boostPage.SetActive(false);
        bundlePage.SetActive(false);
    }
    public void openBoostPage()
    {
        //boostPage.transform.Find("BoostPage").GetComponent<UIScrollView>().ResetPosition();
        buttonBundle.defaultColor = Color.gray;
        buttonBoost.defaultColor = Color.white;
        buttonDiamond.defaultColor = Color.gray;
        diamondPage.SetActive(false);
        boostPage.SetActive(true);
        bundlePage.SetActive(false);
    }
    public void openBundlePage()
    {
       // bundlePage.transform.Find("BundlePage").GetComponent<UIScrollView>().ResetPosition();
        buttonBundle.defaultColor = Color.white;
        buttonBoost.defaultColor = Color.gray;
        buttonDiamond.defaultColor = Color.gray;
        diamondPage.SetActive(false);
        boostPage.SetActive(false);
        bundlePage.SetActive(true);
    }
}
