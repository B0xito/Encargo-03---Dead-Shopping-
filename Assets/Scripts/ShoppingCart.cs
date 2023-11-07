using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShoppingCart : MonoBehaviour
{
    public List<ProductData> products = new List<ProductData>();
    public int totalPrice;

    public GameObject listPanel;
    public TMP_Text listText;

    #region MONEY TEXTS
    [SerializeField] private int initialMoney;
    [SerializeField] private int actualMoney;
    [SerializeField] private TMPro.TMP_Text moneyText;
    [SerializeField] private GameObject notMoney;

    [SerializeField] private TMP_Text totalText;
    [SerializeField] private TMP_Text cashText;
    [SerializeField] private TMP_Text changeText;
    #endregion

    private void Start()
    {
        products.Clear();
        listPanel.SetActive(false);
        actualMoney = initialMoney;
        UpdateMoneyText();

        totalPrice = 0;
    }

    public void AddProduct(ProductData item)
    {        
        products.Add(item);       
    }
    
    public void RemoveProduct(ProductData item)
    {
        if (products.Contains(item)) 
        { 
            products.Remove(item);
        }
    }

    public void UpdateMoneyText()
    {
        moneyText.text = "$" + actualMoney.ToString();
    }

    public void BillCalculate()
    {
        totalText.text = "$" + totalPrice.ToString();
        cashText.text = "$" + initialMoney.ToString();
        for (int i = 0; i < products.Count; i++)
        {
            listText.text += "<br>" + products[i].productName + "   " + "$" + products[i].productPrice + "<br>";            
        }

        actualMoney = totalPrice - initialMoney;   
        
        if (actualMoney < 0)
        {
            actualMoney *= -1;
        }
        
        if (initialMoney < totalPrice)
        {
            notMoney.SetActive(true);
        }

        changeText.text = "$" + actualMoney.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ProductData>())
        {           
            AddProduct(other.GetComponent<ProductData>());
            totalPrice += other.GetComponent<ProductData>().productPrice;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ProductData>())
        {
            RemoveProduct(other.GetComponent<ProductData>());
            totalPrice -= other.GetComponent<ProductData>().productPrice;
        }
    }
}
