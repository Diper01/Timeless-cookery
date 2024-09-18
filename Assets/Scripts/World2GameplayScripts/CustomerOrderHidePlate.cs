using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerOrderHidePlate : MonoBehaviour
{
    private CustomerOrder customerOrder;

    public GameObject plateImg;

    public string[] hideIds;
    // Start is called before the first frame update
    void Start()
    {
        customerOrder = gameObject.GetComponent<CustomerOrder>();

        HidePlate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HidePlate()
    {
        for(int i = 0; i< hideIds.Length; i++)
        {
            if(customerOrder.orderID == hideIds[i])
            {
                plateImg.SetActive(false);

                return;
            }
        }
    }
}
