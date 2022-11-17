using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] public int currentHP;
    [SerializeField] public bool isDead = false;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Transform player;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }
    public void _TakeDamage()
    {
        if(currentHP <= 0)
        {
            isDead = true;
            DamageParticle();
            if(gameObject.name == "mushroom")
            {
                player.GetComponent<Inventory>().MushroomCount++;
            }
            if (gameObject.name == "stone")
            {
                player.GetComponent<Inventory>().StoneŅount++;
            }
        }
        currentHP -= 1;

    }
    private void DamageParticle()
    {
        if (particles) { 
            particles.Play();
        }
    }
}
