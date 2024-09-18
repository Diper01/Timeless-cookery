using RTLTMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalisedRTLTextMeshProUGUI3D : MonoBehaviour
{
    private RTLTextMeshPro3D _text;

    private void OnEnable()
    {
        _text = GetComponent<RTLTextMeshPro3D>();

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
            _text.PreserveNumbers = true;
            _text.FixTags = true;
            _text.Farsi = false;

            if (Localisation.CurrentLanguage == SystemLanguage.Arabic)
            {
                _text.fontSize += 5;
                _text.ForceFix = true;
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
