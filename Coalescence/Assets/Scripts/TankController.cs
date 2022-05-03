using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TankController : MonoBehaviour
{
    public float damage, attackRange, attackRate, attackAnimationLength, offensiveDamage, offensiveSpeed, offensiveRange, offensiveCooldown, defensiveShieldPercentage, defensiveCooldown;
    public GameObject offensiveIndicator;
    public Text offensiveCooldownText;
    public Text defensiveCooldownText;
    public GameObject sprite;
    public Transform leftAttackPoint;
    public Transform rightAttackPoint;
    public Transform upAttackPoint;
    public Transform downAttackPoint;

    public LayerMask enemyLayers;
    public PlayerMovementController playerMovementController;
    public HealthController playerHealthController;

    private Animator animator;
    private bool offensiveAbilitySelected = false;
    private float nextOffensive = 0f;
    private float nextDefensive = 0f;
    private float nextAttack = 0f;
    private Vector3 offensiveTargetPos;

    void Start()
    {
        animator = sprite.GetComponent<Animator>();
    }

    void Update()
    {

        //Inputs
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Time.time >= nextOffensive)
            {
                offensiveAbilitySelected = !offensiveAbilitySelected;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Time.time >= nextDefensive)
            {
                DefensiveAbility();
                nextDefensive = Time.time + defensiveCooldown;
            }

        }

        //Ability Cooldown Display

        if (nextOffensive - Time.time < 0)
        {
            offensiveCooldownText.text = "0";
        }
        else
        {
            offensiveCooldownText.text = Math.Round(nextOffensive - Time.time).ToString();
        }

        if (nextDefensive - Time.time < 0)
        {
            defensiveCooldownText.text = "0";
        }
        else
        {
            defensiveCooldownText.text = Math.Round(nextDefensive - Time.time).ToString();
        }


        offensiveIndicator.SetActive(offensiveAbilitySelected);
        offensiveIndicator.transform.position = Input.mousePosition;

    }

    IEnumerator Attack()
    {
        if (playerMovementController.facingLeft)
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

        if (playerMovementController.facingRight)
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

        if (playerMovementController.facingUp)
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

        if (playerMovementController.facingDown)
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

    }



    void DefensiveAbility()
    {
        double shieldAmount = playerHealthController.maxShield * defensiveShieldPercentage;


        if (playerHealthController.shield + shieldAmount < playerHealthController.maxShield)
        {
            playerHealthController.shield += shieldAmount;
        }
        else
        {
            playerHealthController.shield = playerHealthController.maxShield;
        }

    }

}
