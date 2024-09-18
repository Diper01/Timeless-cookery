using RTLTMPro;
using TMPro;
using UnityEngine;

public class LocalisedRTLTextMeshProUGUI : MonoBehaviour
{
    private RTLTextMeshPro _text;

    // [Header("Key")]
    //[SerializeField] private string _key;
    //[Space(5)]
    //[Header("Alignment")]
    //[SerializeField] private TextAlignmentOptions _arabicAlignment;

    private void OnEnable()
    {
        _text = GetComponent<RTLTextMeshPro>();

        InitTextValue();
        Localisation.LanguageChanged += InitTextValue;
    }

    private void OnDisable()
    {
        Localisation.LanguageChanged -= InitTextValue;
    }

    private void InitTextValue()
    {
        if (_text != null)
        {
            //_text.text = Localisation.GetString(_key, gameObject);

            _text.PreserveNumbers = true;
            _text.FixTags = true;
            _text.Farsi = false;

            if (Localisation.CurrentLanguage == SystemLanguage.Arabic)
            {
                _text.fontSize += 5;
                _text.ForceFix = true;
                //_text.alignment = _arabicAlignment;
                _text.font = Localisation.ArabicFont;
            }
            else
            {
                _text.ForceFix = false;
                _text.font = Localisation.DefaultFont;
            }

            _text.UpdateText();
        }
    }
}
