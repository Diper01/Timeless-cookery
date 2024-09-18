using TMPro;
using UnityEngine;

namespace Locit
{
    public class WorldMapLocalisation: MonoBehaviour
    {
        [Header("MainScreen")] 
        [SerializeField] private TextMeshProUGUI upgradeText;
        [SerializeField] private TextMeshProUGUI theNumberOfSateText;
        [SerializeField] private TextMeshProUGUI currentlyHave;
        [Header("DialogWorldSelect")] 
        [SerializeField] private TextMeshProUGUI selectWordsTitleText;
        [SerializeField] private TextMeshProUGUI needStarsToOnlock;

        [Header("DialogAchievement")] 
        [SerializeField]
        private TextMeshProUGUI titleAchievementT;
        

        

        private void Start()
        {
            TranslateText();
        }

        private void TranslateText()
        {
            titleAchievementT.text = Localisation.GetString(TranslateKey.AchievementT);
            upgradeText.text = Localisation.GetString(TranslateKey.Upgrade);
            theNumberOfSateText.text = Localisation.GetString(TranslateKey.TheNumberOfState);
            currentlyHave.text = Localisation.GetString(TranslateKey.CurrentlyHave);
            selectWordsTitleText.text = Localisation.GetString(TranslateKey.SelectWords);
            needStarsToOnlock.text = Localisation.GetString(TranslateKey.YouNeed) + " 24 " + Localisation.GetString(TranslateKey.StarsToUnclock) ;
        }
    }
}