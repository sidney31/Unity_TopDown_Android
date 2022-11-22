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
        Time.timeScale = 0;
        tradeMenu.SetActive(true);
        MenuButton.SetActive(false);
    }
    public void CloseTradeMenu()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        tradeMenu.SetActive(false);
        MenuButton.SetActive(true);
    }
}
