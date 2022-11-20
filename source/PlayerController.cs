using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Vector2 inputVector;


    [SerializeField] private Animator anime;
    [SerializeField] private Transform AttackPoint;
    [SerializeField] private Transform tradeNPC;
    [SerializeField] private Transform talkNPC;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private GameObject TalkButton;
    [SerializeField] private LayerMask EnemyLayer;
    [SerializeField] private LayerMask NPCLayer;
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private bool FaceRight = true;
    [SerializeField] private float Speed = 5f;
    [SerializeField] private float NextAttackTime;
    [SerializeField] private float AttackRate = 0.5f;
    [SerializeField] private float AttackRange = 0.4f;
    [SerializeField] public int CurrentHP = 5;
    [SerializeField] private bool canTrade;
    [SerializeField] private bool canTalk;
    [SerializeField] private GameObject DieMenu;

    private void Start()
    {
        Application.targetFrameRate = 60;
        tradeNPC = GameObject.Find("tradeNPC").transform;
        talkNPC = GameObject.Find("talkNPC").transform;
        DieMenu.SetActive(false); ;
    }
    private void FixedUpdate()
    {
        Speed = anime.GetCurrentAnimatorStateInfo(0).IsName("Hit") ? 0f : 5f;
        inputVector.x = _joystick.Horizontal;
        inputVector.y = _joystick.Vertical;
        ReflectPlayer();
        anime.SetFloat("Speed", GetMaxOfVector(inputVector));
        transform.position += new Vector3(inputVector.x, inputVector.y, 0) * Speed * Time.deltaTime;
        CheckNPC();
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
        DieMenu.SetActive(true);
    }
    private void CheckNPC()
    {
        Collider2D[] NPCInZone = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, NPCLayer);
        foreach (Collider2D NPC in NPCInZone)
        {
            TalkButton.SetActive(true);
            if (NPC.name == "tradeNPC")
            {
                canTrade = true;
                canTalk = false;
            }else if(NPC.name == "talkNPC")
            {
                canTrade = false;
                canTalk = true;
            }
        }
        if (NPCInZone.Length < 1)
        {
            TalkButton.SetActive(false);
            canTrade = false;
            canTalk = false;
        }
    }
    public void talkButtonPressed()
    {
        if (canTalk)
        {
            talkNPC.GetComponent<Dialogue>().StartDialog();
        }
        if (canTrade)
        {
            tradeNPC.GetComponent<TradeSystem>().StartTrade();
        }
    }
}