using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Slime : MonoBehaviour
{
    [SerializeField] private float nextJumpTime = 0;
    [SerializeField] private float jumpRate = 2;
    [SerializeField] private float nextAttackTime = 0;
    [SerializeField] private float attackRate = 2;
    [SerializeField] private float smoothing = 1f;
    [SerializeField] private float attackRange;
    [SerializeField] private bool faceRight = true;
    [SerializeField] public int currentHP = 3;
    [SerializeField] public int damage = 1;
    [SerializeField] private Animator anime;
    [SerializeField] private Transform target;
    [SerializeField] private Transform attackZone;
    [SerializeField] private GameObject SlimeSpawner;
    [SerializeField] private LayerMask playerLayer;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
        SlimeSpawner = GameObject.Find("SlimeSpawner");
    }
    private void FixedUpdate()
    {
        ReflectEnemy();
        //transform.position = Vector3.Lerp(transform.position, target.position, smoothing * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, target.position, smoothing * Time.deltaTime);
        if (GetAbsDistanceBeetweenObjects(transform, target) < 15)
        {
            if (Time.time >= nextJumpTime && GetAbsDistanceBeetweenObjects(transform, target) > 1)
            {
                //Debug.Log(Mathf.Abs(transform.position.x - target.position.x));
                nextJumpTime = Time.time + jumpRate;
                anime.SetTrigger("Jump");
            }
            smoothing = IsAnimationPlaying("SlimeJump") ? 2f : 0f;

            if (Time.time >= nextAttackTime && GetAbsDistanceBeetweenObjects(transform, target) < 1)
            {
                anime.SetTrigger("Jump");
                Attack();
                nextAttackTime = Time.time + attackRate;
            }
        }
        if (GetComponent<TakeDamage>().isDead)
        {
            Die();
        }
    }
    public bool IsAnimationPlaying(string animationName)
    {
        var animatorStateInfo = anime.GetCurrentAnimatorStateInfo(0);
        if (animatorStateInfo.IsName(animationName)){
            return true;
        }
        return false;
    }
    private void ReflectEnemy()
    {
        if (transform.position.x - target.position.x > 0 && faceRight ||
        transform.position.x - target.position.x < 0 && !faceRight)
        {
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
            faceRight = !faceRight;
        }
    }
    private void Die()
    {
        anime.SetBool("isDead", true);
        this.enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        SlimeSpawner.GetComponent<RandomSpawner>().Spawned--;
    }

    private void Attack()
    {
        Collider2D[] PlayersInZone = Physics2D.OverlapCircleAll(attackZone.position, attackRange, playerLayer);
        foreach (Collider2D player in PlayersInZone)
        {
            player.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
    private float GetAbsDistanceBeetweenObjects(Transform firstObject, Transform secondObject)
    {
        return (Mathf.Abs(firstObject.position.x - secondObject.position.x) + Mathf.Abs(firstObject.position.y - secondObject.position.y)) / 2f;
    }
    private void OnDrawGizmosSelected()
    {
        if (attackZone == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackZone.position, attackRange);
    }
}
