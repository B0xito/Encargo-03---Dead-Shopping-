using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShoppingCart : MonoBehaviour
{
    [SerializeField] string[] shoppingList;

    private void Start()
    {
        shoppingList = new string[10];
    }

    public void AddProduct(string productName)
    {
        for (int i = 0; i < shoppingList.Length; i++)
        {
            if (shoppingList[i] == null)
            {
                shoppingList[i] = productName;
                break;
            }
        }
    }

    public void RemoveProduct(string productName)
    {
        for (int i = 0; i < shoppingList.Length; i++)
        {
            if (shoppingList[i] == productName)
            {
                shoppingList[i] = null;
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Product"))
        {
            if (other.gameObject.CompareTag("Product"))
            {
                AddProduct(other.GetComponent<ProductData>().productName);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Product"))
        {
            RemoveProduct(other.GetComponent<ProductData>().productName);
        }
    }
}
