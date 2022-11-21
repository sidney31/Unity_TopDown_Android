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
    [SerializeField] private GameObject[] desc;
    //[SerializeField] private TMP_Text[] textQueue;

    private void Start()
    {
        for (int i = 0; i < desc.Length; i++)
        {
            desc[i].SetActive(false);
        }
    }
    private void Update()
    {
        MushroomCountText.text = MushroomCount.ToString();
        Stone—ountText.text = Stone—ount.ToString();
    }
    public void PressButton(GameObject button)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            //Debug.Log(buttons[i] + " / " + button.name);
            desc[i].SetActive(false);
            if (buttons[i].name == button.name)
            {
                //Debug.Log(buttons[i] + " / " + button.name);
                desc[i].SetActive(true);
            }
        }
    }

}
