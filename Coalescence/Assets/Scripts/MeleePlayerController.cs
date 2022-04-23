using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePlayerController : MonoBehaviour
{
    public float damage;
    public float attackRange = 0.5f;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    private bool canAttack = true;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (canAttack)
            {
                Attack();
            }
        }
    }

    void Attack()
    {

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        for (int i = 0; i < enemies.Length; i++)
        {
            HealthController healthController = enemies[i].GetComponent<HealthController>();
            healthController.TakeDamage(damage);
        }
    }





}
    