using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsurgencyPackConfirmMenu : MonoBehaviour
{
    public InsurgencyPackageMenu insurgencyPackageMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _BuyButton()
    {
        gameObject.SetActive(false);
    }

    public void _Cancel()
    {
        insurgencyPackageMenu.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        gameObject.SetActive(false);
    }
}
