using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaymentCheckUI : MonoBehaviour
{
    [SerializeField]
    private Button cancelButton;
    [SerializeField]
    private Button purchaseButton;
    [SerializeField]
    private GameObject messageZone;

    private string itemPrice;
    private string playerCurrency;

    public void Init()
    {
        cancelButton.onClick.RemoveAllListeners();
        purchaseButton.onClick.RemoveAllListeners();

        cancelButton.onClick.AddListener(() => OnClickCancelBtn());
    }

    public void SetItemPrice(int price)
    {
        itemPrice = price.ToString();
    }

    public void SetItemPrice(string price)
    {
        itemPrice = price;
    }

    public void SetPlayerCurrency(int currency)
    {
        playerCurrency = currency.ToString();
    }

    public void SetPlayerCurrency(string currency)
    {
        playerCurrency = currency;
    }

    private void OnClickCancelBtn()
    {
        this.gameObject.SetActive(false);
    }

}
