using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ShopTopUI
{
    PackageUI,
    PlayerCurrencyUI,
    OnedayShopUI,
    GachaUI,
    END
}

public class ShopUI : MonoBehaviour
{
    [SerializeField]
    private Button exitBtn;

    private PaymentCheckUI paymentUI;

    private GameObject shopTopUI;
    private GameObject shopItemUI;

    public void Init()
    {
        exitBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.AddListener(() => this.gameObject.SetActive(false));

        paymentUI = GetComponentInChildren<PaymentCheckUI>(true);
        paymentUI.Init();

        shopTopUI = transform.GetChild(0).gameObject;
        shopItemUI = transform.GetChild(1).gameObject;

        SetShopTopUIButtons();
    }

    private void SetShopTopUIButtons()
    {
        for (int i = 0; i < (int)ShopTopUI.END; i++)
        {
            Button button = shopTopUI.transform.GetChild(i).GetComponent<Button>();
            GameObject itemUI = shopItemUI.transform.GetChild(i).gameObject;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => SetShopItemUI(itemUI));
        }
    }

    private void SetShopItemUI(GameObject targetObject)
    {
        for (int i = 0; i < shopItemUI.transform.childCount; i++)
        {
            if (shopItemUI.transform.GetChild(i).gameObject == targetObject)
            {
                targetObject.SetActive(true);
            }
            else
            {
                shopItemUI.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
