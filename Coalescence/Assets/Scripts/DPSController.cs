using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DPSController : MonoBehaviour
{
    public float damage, attackRange, attackRate, attackAnimationLength, offensiveDamage, offensiveSpeed, offensiveRange, offensiveCooldown;
    public GameObject offensiveIndicator;
    public Text offensiveCooldownText;
    public GameObject sprite;
    public Transform leftAttackPoint;
    public Transform rightAttackPoint;
    public Transform upAttackPoint;
    public Transform downAttackPoint;

    public LayerMask enemyLayers;
    public PlayerMovementController movementController;


    private Animator animator;
    private bool offensiveAbilitySelected = false;
    private float nextOffensive = 0f;
    private float nextAttack = 0f;
    private Vector3 offensiveTargetPos;

    void Start()
    {
        animator = sprite.GetComponent<Animator>();
    }

    void Update()
    {



        if (Input.GetMouseButtonDown(0))
        {
            if (offensiveAbilitySelected)
            {
                offensiveTargetPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
                StartCoroutine(OffensiveAbility(offensiveTargetPos));
                nextOffensive = Time.time + offensiveCooldown;
            }
            else 
            {
                if (Time.time >= nextAttack)
                {
                    StartCoroutine(Attack());
                    nextAttack = Time.time + 1f / attackRate;
                }
            }
        }

        if (Input.GetKeyDown("q"))
        {
            if (Time.time >= nextOffensive)
            {
                offensiveAbilitySelected = !offensiveAbilitySelected;
            }
            
        }

        if (nextOffensive - Time.time < 0)
        {
            offensiveCooldownText.text = "0";
        }
        else
        {
            offensiveCooldownText.text = Math.Round(nextOffensive - Time.time).ToString();
        }

        offensiveIndicator.SetActive(offensiveAbilitySelected);
        offensiveIndicator.transform.position = Input.mousePosition;

    }

    IEnumerator Attack()
    {
        if (movementController.facingLeft)
        {
            animator.SetTrigger("attackLeft");
            yield return new WaitForSeconds(attackAnimationLength);
            Collider2D[] enemies = Physics2D.OverlapCircleAll(leftAttackPoint.position, attackRange, enemyLayers);
            for (int i = 0; i < enemies.Length; i++)
            {
                HealthController healthController = enemies[i].GetComponent<HealthController>();
                healthController.TakeDamage(damage);
            }
        }

        if (movementController.facingRight)
        {
            animator.SetTrigger("attackRight");
            yield return new WaitForSeconds(attackAnimationLength);
            Collider2D[] enemies = Physics2D.OverlapCircleAll(rightAttackPoint.position, attackRange, enemyLayers);
            for (int i = 0; i < enemies.Length; i++)
            {
                HealthController healthController = enemies[i].GetComponent<HealthController>();
                healthController.TakeDamage(damage);
            }
        }

        if (movementController.facingUp)
        {
            animator.SetTrigger("attackUp");
            yield return new WaitForSeconds(attackAnimationLength);
            Collider2D[] enemies = Physics2D.OverlapCircleAll(upAttackPoint.position, attackRange, enemyLayers);
            for (int i = 0; i < enemies.Length; i++)
            {
                HealthController healthController = enemies[i].GetComponent<HealthController>();
                healthController.TakeDamage(damage);
            }
        }

        if (movementController.facingDown)
        {
            animator.SetTrigger("attackDown");
            yield return new WaitForSeconds(attackAnimationLength);
            Collider2D[] enemies = Physics2D.OverlapCircleAll(downAttackPoint.position, attackRange, enemyLayers);
            for (int i = 0; i < enemies.Length; i++)
            {
                HealthController healthController = enemies[i].GetComponent<HealthController>();
                healthController.TakeDamage(damage);
            }
        }
    }

    IEnumerator OffensiveAbility(Vector3 attackPos)
    {
        offensiveAbilitySelected = false;
        PlayerMovementController movementController = GetComponent<PlayerMovementController>();
        movementController.input = false;

        float directionX = attackPos.x - transform.position.x;
        float directionY = attackPos.y - transform.position.y;
        float length = (float)Math.Sqrt(Math.Pow(directionX, 2) + Math.Pow(directionY, 2));
       
        directionX /= length;
        directionY /= length;

        GetComponent<Rigidbody2D>().velocity = new Vector2(directionX * offensiveSpeed, directionY * offensiveSpeed);

        yield return new WaitForSeconds(length/offensiveSpeed);
        
        movementController.input = true;

        Impact();
    }

    void Impact()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, offensiveRange, enemyLayers);
        for (int i = 0; i < enemies.Length; i++)
        {
            HealthController healthController = enemies[i].GetComponent<HealthController>();
            healthController.TakeDamage(offensiveDamage);
        }
    }

}
