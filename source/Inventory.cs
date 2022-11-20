using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] private TMP_Text MushroomCountText;
    [SerializeField] public int MushroomCount;
    [SerializeField] private TMP_Text Stone—ountText;
    [SerializeField] public int Stone—ount;
    [SerializeField] private GameObject[] buttons;
    //[SerializeField] private TMP_Text[] textQueue;
    [SerializeField] private TMP_Text textField;

    private void Update()
    {
        MushroomCountText.text = MushroomCount.ToString();
        Stone—ountText.text = Stone—ount.ToString();
    }
    public void PressButton(GameObject button)
    {
        Debug.Log(gameObject.name);
        for (int i = 0; i < buttons.Length; i++)
        {
            if (button.name == buttons[i].name)
            {
                textField.text = buttons[i].name;
            } 
        }
    }

}
