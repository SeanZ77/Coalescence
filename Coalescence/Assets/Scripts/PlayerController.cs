using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float attackRange = 0.5f;
    public GameObject sword;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    private Animator swordAnimator;

    void Start() 
    {
        swordAnimator = sword.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        swordAnimator.ResetTrigger("attack");
        swordAnimator.SetTrigger("attack");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        for (int i = 0; i < enemies.Length; i++)
        {
            Debug.Log(enemies[i]);
        }
    }
}
    