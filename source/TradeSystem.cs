using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeSystem : MonoBehaviour
{
    [SerializeField] private GameObject tradeMenu;
    [SerializeField] private GameObject MenuButton;

    private void Start()
    {
        tradeMenu.SetActive(false);
    }
    public void StartTrade()
    {
        tradeMenu.SetActive(true);
        MenuButton.SetActive(false);
    }
    public void CloseTradeMenu()
    {
        tradeMenu.SetActive(false);
        MenuButton.SetActive(true);
    }
}
