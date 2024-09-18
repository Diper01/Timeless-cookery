using TMPro;
using UnityEngine;

namespace Locit
{
    public class LoadingSceneLocalisation: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI loadintText;

        private void Start()
        {
            TranslateText();
        }

        private void TranslateText()
        {
            loadintText.text = Localisation.GetString(TranslateKey.Loading);
        }
    }
}