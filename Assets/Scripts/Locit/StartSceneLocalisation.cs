using TMPro;
using UnityEngine;

namespace Locit
{
    public class StartSceneLocalisation: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playText;

        private void Start()
        {
            TransalteText();
        }

        private void TransalteText()
        {
            playText.text = Localisation.GetString(TranslateKey.TouchToPlay);
        }
    }
}