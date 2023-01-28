using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class HP_System : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int numberOfLives;
    
    [SerializeField] private Image[] lives;
    [SerializeField] private Sprite FillHeart;
    [SerializeField] private Sprite EmptyHeart;

    private void Update()
    {
        health = GameObject.Find("Player").GetComponent<PlayerController>().CurrentHP;

        if (health > numberOfLives)
        {
            health = numberOfLives;
        }
        for (int i = 0; i < lives.Length; i++)
        {
            if (i < health)
            {
                lives[i].sprite = FillHeart;
            }
            else
            {
                lives[i].sprite = EmptyHeart;
            }

            if (i < numberOfLives)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }
        }        
    }
}
