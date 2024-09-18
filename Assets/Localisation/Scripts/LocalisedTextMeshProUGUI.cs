using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalisedTextMeshProUGUI : MonoBehaviour
{
	void Start()
	{
		// For UI Textes
		TextMeshProUGUI textMesh = GetComponent<TextMeshProUGUI>();
		if (textMesh != null)
		{
			textMesh.text = Localisation.GetString(textMesh.text, gameObject);
		}
	}
}
