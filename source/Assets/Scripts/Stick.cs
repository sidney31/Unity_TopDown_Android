using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
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
        Invoke("_Destroy", 0.3f);
    }
    private void _Destroy()
    {
        Destroy(gameObject);
    }
}
