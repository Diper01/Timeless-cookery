using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreButton : MonoBehaviour
{
    public GameObject storePopup;

    public void OnClick()
    {
        storePopup.SetActive(true);
    }

    public void Close()
    {
        storePopup.SetActive(false);
    }
}
