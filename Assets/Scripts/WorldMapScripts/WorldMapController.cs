using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapController : MonoBehaviour
{
    //private GoogleFireBaseEvents googleFireBaseEvents;

    public GameObject dialogAchievement, dialogSetting, dialogEnergy, dialogEnergyNotEnough, dialogRate, dialogThank, dialogFreeCoin;
    public GameObject dialogStore;
    public GameObject dialogGift, dialogFreeGift;
    public GameObject dialogWorldSelect, unlockWorldPopup;
    public GameObject dialogLevelDetail;
    public GameObject dialogPackage;
    public GameObject dialogRanking;
    public GameObject dialogBossClear;

    public GameObject shareBg, btFb, btEmail;

    // Start is called before the first frame update
    void Start()
    {
       // IronSource.Agent.validateIntegration();
        //googleFireBaseEvents = FindObjectOfType<GoogleFireBaseEvents>();
        //IronSource.Agent.hideBanner();
    }
     public void quitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void activeShareBg()
    {
        if (shareBg.transform.localScale.x <= 0)
        {
            shareBg.GetComponent<TweenScale>().ResetToBeginning();
            shareBg.GetComponent<TweenScale>().from = new Vector3(0, 1, 1);
            shareBg.GetComponent<TweenScale>().to = new Vector3(1, 1, 1);
            shareBg.GetComponent<TweenScale>().PlayForward();

            StartCoroutine(_AutoDeactive());
        }
        else if (shareBg.transform.localScale.x >= 1)
        {
            shareBg.GetComponent<TweenScale>().ResetToBeginning();
            shareBg.GetComponent<TweenScale>().from = new Vector3(1, 1, 1);
            shareBg.GetComponent<TweenScale>().to = new Vector3(0, 1, 1);
            shareBg.GetComponent<TweenScale>().PlayForward();
        }
    }

    IEnumerator _AutoDeactive()
    {
        yield return new WaitForSeconds(3f);
        {
            if (shareBg.transform.localScale.x >= 1)
            {
                shareBg.GetComponent<TweenScale>().ResetToBeginning();
                shareBg.GetComponent<TweenScale>().from = new Vector3(1, 1, 1);
                shareBg.GetComponent<TweenScale>().to = new Vector3(0, 1, 1);
                shareBg.GetComponent<TweenScale>().PlayForward();
            }
        }

        yield break;
    }

    public void openAchievementDialog()
    {
        dialogAchievement.transform.localScale = new Vector3(1f, 1f, 1f);
        dialogAchievement.GetComponent<UIScaleAnimation>().enabled = true;
        dialogAchievement.GetComponent<AudioSource>().enabled = true;
    }
    public void closeDialogAchievement()
    {
        dialogAchievement.transform.localScale = new Vector3(0f, 0f, 0f);
        dialogAchievement.GetComponent<UIScaleAnimation>().enabled = false;
        dialogAchievement.GetComponent<AudioSource>().enabled = false;
    }

    public void openDialogStore()
    {
        dialogStore.SetActive(true);
    }
    public void closeDialogStore()
    {
        dialogStore.SetActive(false);
    }
    public void openDialogWorldSelect()
    {
        //dialogWorldSelect.transform.localScale = new Vector3(1f, 1f, 1f);
        dialogWorldSelect.GetComponent<UIScaleAnimation>().enabled = true;
        dialogWorldSelect.GetComponent<AudioSource>().enabled = true;
    }
    public void closeDialogWorldSelect()
    {
        dialogWorldSelect.transform.localScale = new Vector3(0f, 0f, 0f);
        dialogWorldSelect.GetComponent<UIScaleAnimation>().enabled = false;
        dialogWorldSelect.GetComponent<AudioSource>().enabled = false;
    }

    public void openDialogEnergyNotEnough()
    {
        dialogEnergyNotEnough.SetActive(true);
    }
    public void closeDialogEnergyNotEnough()
    {
        dialogEnergyNotEnough.SetActive(false);
    }

    public void openDialogDailyGift()
    {
        dialogGift.GetComponent<UIScaleAnimation>().enabled = true;
        dialogGift.GetComponent<AudioSource>().enabled = true;
        dialogGift.GetComponent<DialogGiftHandle>().openDailyBonusPage();
    }
    public void closeDialogDailyGift()
    {
        dialogGift.GetComponent<UIScaleAnimation>().enabled = false;
        dialogGift.GetComponent<AudioSource>().enabled = false;
        dialogGift.transform.localScale = new Vector3(0f, 0f, 0f);
    }
    void OnApplicationPause(bool isPaused)
    {
        //IronSource.Agent.onApplicationPause(isPaused);
  
    }
    public void openDialogFreeGift()
    {
        dialogFreeGift.SetActive(true);
    }
    public void closeDialogFreeGift()
    {
        dialogFreeGift.SetActive(false);
    }

    public void openDialogSetting()
    {
        dialogSetting.SetActive(true);
    }
    public void closeDialogSetting()
    {
        dialogSetting.SetActive(false);
    }
    public void openDialogAddEnergy()
    {
        dialogEnergy.GetComponent<UIScaleAnimation>().enabled = true;
        dialogEnergy.GetComponent<AudioSource>().enabled = true;
    }
    public void closeDialogAddEnergy()
    {
        dialogEnergy.GetComponent<UIScaleAnimation>().enabled = false;
        dialogEnergy.GetComponent<AudioSource>().enabled = false;
        dialogEnergy.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public void openDialogUnlockWorld()
    {
        unlockWorldPopup.SetActive(true);
    }
    public void closeDialogUnlockWorld()
    {
        unlockWorldPopup.SetActive(false);
    }

    public void openDialogRateUs()
    {
        dialogRate.GetComponent<UIScaleAnimation>().enabled = true;
    }
    public void closeDialogRateUs()
    {
        dialogRate.GetComponent<UIScaleAnimation>().enabled = false;
        dialogRate.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public void openDialogThank()
    {
        dialogThank.GetComponent<UIScaleAnimation>().enabled = true;
    }
    public void closeDialogThank()
    {
        dialogThank.GetComponent<UIScaleAnimation>().enabled = false;
        dialogThank.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public void openDialogPackage()
    {
        dialogPackage.GetComponent<UIScaleAnimation>().enabled = true;
        //googleFireBaseEvents.ClickBeginerPack();
    }

    public void closeDialogPackage()
    {
        dialogPackage.GetComponent<UIScaleAnimation>().enabled = false;
        dialogPackage.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public void openDialogRanking()
    {
        //googleFireBaseEvents.OpenRanking();
        dialogRanking.GetComponent<UIScaleAnimation>().enabled = true;
        dialogRanking.GetComponent<RankDialog>().OpenCreateUserDialog();
        dialogRanking.SetActive(true);
    }
    public void closeDialogRanking()
    {
        dialogRanking.GetComponent<UIScaleAnimation>().enabled = false;
        dialogRanking.SetActive(false);
    }

    public void openDialogBossClear()
    {
        dialogBossClear.GetComponent<UIScaleAnimation>().enabled = true;
        dialogBossClear.SetActive(true);
    }
    public void closeDialogBossClear()
    {
        dialogBossClear.GetComponent<UIScaleAnimation>().enabled = false;
        dialogBossClear.SetActive(false);
    }

    public void openDialogFreeCoin()
    {
        //googleFireBaseEvents.ClickWatchFreeCoin();
        dialogFreeCoin.GetComponent<UIScaleAnimation>().enabled = true;
        dialogFreeCoin.SetActive(true);
    }
    public void closeDialogFreeCoin()
    {
        dialogFreeCoin.GetComponent<UIScaleAnimation>().enabled = false;
        dialogFreeCoin.SetActive(false);
    }
}
