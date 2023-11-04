using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillCalculator : MonoBehaviour
{
    [SerializeField] string[] product;
    [SerializeField] int[] productValue;

    private CashRegister cashRegister;

    private void Start()
    {      
        product = new string[10];
        productValue = new int[product.Length];

        cashRegister = GetComponentInParent<CashRegister>();
    }

    private void Update()
    {
        if (cashRegister != null && cashRegister.productCollided != null)
        {
            if (cashRegister.productCollided)
            {
                ProductData productData = cashRegister.productCollided.GetComponent<ProductData>();
                if (productData != null)
                {
                    CalculateBill();
                }
            }
        }
    }

    void CalculateBill()
    {
        for (int i = 0; i < product.Length; i++)
        {
            if (product[i] == null)
            {
                product[i] = ProductData.productName;
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (parent)
        CalculateBill();
    }
}
