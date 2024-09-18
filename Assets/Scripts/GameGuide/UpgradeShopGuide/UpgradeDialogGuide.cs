using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDialogGuide : MonoBehaviour
{
    public GameObject upgradeDialog;

    public GameObject guideBG, handGuide, guideButton, notice, foxImg;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _Active();
    }

    void _Active()
    {
        if (upgradeDialog.transform.localScale.x >= 1)
        {
            if (guideBG.activeSelf == false && handGuide.activeSelf == false && guideButton.activeSelf == false && notice.activeSelf == false)
            {
                guideBG.SetActive(true);
                handGuide.SetActive(true);
                guideButton.SetActive(true);
                notice.SetActive(true);
                foxImg.SetActive(true);
            }
        }
    }
}
