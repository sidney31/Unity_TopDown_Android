using UnityEngine;
public class TakeDamage : MonoBehaviour
{
    [SerializeField] public int currentHP;
    [SerializeField] public bool isDead = false;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Transform player;
    [SerializeField] private bool transperent;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }
    public void _TakeDamage()
    {
        if(currentHP <= 0)
        {
            isDead = true;

            if (transperent)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.0f);
            }
            /*if (gameObject.name == "mushroom")
            {
                player.GetComponent<Inventory>().MushroomCount++;
            }
            if (gameObject.name == "stone")
            {
                player.GetComponent<Inventory>().Stone—ount++;
            }
            if (gameObject.name == "stick")
            {
                player.GetComponent<Inventory>().Stick—ount++;
            }*/

        }
        currentHP -= 1;
        DamageParticle();

    }
    private void DamageParticle()
    {
        if (particles) { 
            particles.Play();
        }
    }

}
