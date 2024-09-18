using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDetailPopup : MonoBehaviour
{
    public UILabel levelText;

    public void _OkButton()
    {
        gameObject.SetActive(false);
    }
}
