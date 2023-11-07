using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CashRegister : MonoBehaviour
{
    [SerializeField] ShoppingCart shoppingCart;

    void OnTriggerEnter(Collider other)
    {
        shoppingCart = other.GetComponentInChildren<ShoppingCart>();
        if (shoppingCart)
        {
            shoppingCart.listPanel.SetActive(true);
            shoppingCart.BillCalculate();
            shoppingCart.UpdateMoneyText();
            Time.timeScale = 0;
        }
    }

    


}
