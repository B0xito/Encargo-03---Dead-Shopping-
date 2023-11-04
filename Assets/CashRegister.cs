using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CashRegister : MonoBehaviour
{
    [SerializeField] private ShoppingCart shoppingCart;
    #region MONEY TEXTS
    [SerializeField] private int initialMoney;
    [SerializeField] private int actualMoney;
    [SerializeField] private TMPro.TMP_Text moneyText;
    [SerializeField] private GameObject notMoney;
    #endregion

    [SerializeField] private GameObject bill;

    void Start()
    {
        actualMoney = initialMoney;
        UpdateMoneyText();
    }

    void UpdateMoneyText()
    {
        moneyText.text = "Money: " + actualMoney.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        ProductData productData = other.gameObject.GetComponent<ProductData>();
        if (productData != null && actualMoney >= productData.productPrice)
        {
            actualMoney -= productData.productPrice;
            UpdateMoneyText();
            shoppingCart.AddProduct(other.gameObject.name);
            bill.SetActive(true);
        }
        else
        {
            notMoney.SetActive(true);
        }
    }

    
}
