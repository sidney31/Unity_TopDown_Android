using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 inputVector;


    [SerializeField] private Animator anime;
    [SerializeField] private Transform AttackPoint;
    [SerializeField] private Transform NPC;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private GameObject TalkButton;
    [SerializeField] private LayerMask EnemyLayer;
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private bool FaceRight = true;
    [SerializeField] private float Speed = 5f;
    [SerializeField] private float NextAttackTime;
    [SerializeField] private float AttackRate = 0.5f;
    [SerializeField] private float AttackRange = 0.4f;
    [SerializeField] public int CurrentHP = 5;

    //inventory
    [SerializeField] public int mushroom = 0;

    private void Start()
    {
        Application.targetFrameRate = 300;
        TalkButton.SetActive(false);
        NPC = GameObject.Find("NPC").transform;
    }
    private void FixedUpdate()
    {
        Speed = anime.GetCurrentAnimatorStateInfo(0).IsName("Hit") ? 0f : 5f;
        inputVector.x = _joystick.Horizontal;
        inputVector.y = _joystick.Vertical;
        ReflectPlayer();
        anime.SetFloat("Speed", GetMaxOfVector(inputVector));
        transform.position += new Vector3(inputVector.x, inputVector.y, 0) * Speed * Time.deltaTime;
        if (Mathf.Abs(transform.position.x - NPC.position.x) < 1 &&
            Mathf.Abs(transform.position.y - NPC.position.y) < 1)
        {
            TalkButton.SetActive(true);
        }
        else
        {
            TalkButton.SetActive(false);
        }
    }

    float GetMaxOfVector(Vector2 vector, float max = 0)
    {
        if (Mathf.Abs(vector.y) != 0)
        {
            max = vector.y;
        }
        else if (Mathf.Abs(vector.x) != 0)
        {
            max = vector.x;
        }
        return Mathf.Abs(max);
    }

    void ReflectPlayer()
    {
        if (inputVector.x < 0 && FaceRight == false ||
            inputVector.x > 0 && FaceRight == true)
        {
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
            FaceRight = !FaceRight;
            
            CreateDust();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other && other.gameObject.layer == 6)
        {
            Color tempColor = other.GetComponent<SpriteRenderer>().color;
            tempColor.a = 0.3f;
            other.GetComponent<SpriteRenderer>().color = tempColor;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other && other.gameObject.layer == 6)
        {
            Color tempColor = other.GetComponent<SpriteRenderer>().color;
            tempColor.a = 1f;
            other.GetComponent<SpriteRenderer>().color = tempColor;
        }
    }
    public void Hit()
    {
        if (Time.time >= NextAttackTime && !anime.GetBool("isDead")) { 
            anime.SetTrigger("Hit");
            NextAttackTime = Time.time + AttackRate;
            Collider2D[] EnemysInZone = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayer);
            foreach (Collider2D Enemy in EnemysInZone)
            {
                Enemy.GetComponent<TakeDamage>()._TakeDamage();
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
    private void CreateDust()
    {
        dust.Play();
    }
    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
        if (CurrentHP <= 0)
        {
            Die();
        }

        //Debug.Log("take damage. Current HP = " + CurrentHP);
    }
    private void Die()
    {
        anime.SetBool("isDead", true);
        this.enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
    }
}