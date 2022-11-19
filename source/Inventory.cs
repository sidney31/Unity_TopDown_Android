using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] private TMP_Text MushroomCountText;
    [SerializeField] public int MushroomCount;
    [SerializeField] private TMP_Text StoneÑountText;
    [SerializeField] public int StoneÑount;

    private void Start()
    {

    }
    private void Update()
    {
        MushroomCountText.text = MushroomCount.ToString();
        StoneÑountText.text = StoneÑount.ToString();
    }

}
