using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCrystalButton : MonoBehaviour
{
    //private IAPManager iAPManager;

    public enum CrystalType { crystal_100, crystal_600, crystal_1300, crystal_3400 }
    public CrystalType crystalType;

    public UILabel priceText;

   /* public void Start()
    {
        iAPManager = GameObject.FindGameObjectWithTag("IAPManager").GetComponent<IAPManager>();

        StartCoroutine(LoadPriceRoutine());
    }

    public void OnTouch()
    {
        switch (crystalType)
        {
            case CrystalType.crystal_100:
                iAPManager.Buy100Crystal();
                break;
            case CrystalType.crystal_600:
                iAPManager.Buy600Crystal();
                break;
            case CrystalType.crystal_1300:
                iAPManager.Buy1300Crystal();
                break;
            case CrystalType.crystal_3400:
                iAPManager.Buy3400Crystal();
                break;
        }
    }

    private IEnumerator LoadPriceRoutine()
    {
        while (!iAPManager.IsInitialized())
        {
            yield return null;
        }

        string loadedPrice = "";

        switch (crystalType)
        {
            case CrystalType.crystal_100:
                loadedPrice = iAPManager.GetProductPriceFromStore(iAPManager.crystal_100);
                break;
            case CrystalType.crystal_600:
                loadedPrice = iAPManager.GetProductPriceFromStore(iAPManager.crystal_600);
                break;
            case CrystalType.crystal_1300:
                loadedPrice = iAPManager.GetProductPriceFromStore(iAPManager.crystal_1300);
                break;
            case CrystalType.crystal_3400:
                loadedPrice = iAPManager.GetProductPriceFromStore(iAPManager.crystal_3400);
                break;
        }

        priceText.text = loadedPrice;
    }*/
}
