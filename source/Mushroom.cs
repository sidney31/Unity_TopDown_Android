using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{

    private void LateUpdate()
    {
        if (GetComponent<TakeDamage>().isDead)
        {
            Die();
        }
    }
    private void Die()
    {
        this.enabled = false;
        Destroy(gameObject);
    }
}
