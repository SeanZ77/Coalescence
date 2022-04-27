using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public double shield, maxShield, shieldRegenAmount, health, maxHealth, healthRegenAmount;
    public Slider shieldBar;
    public Slider healthBar;
    private bool inCombat = false;

    void Update() 
    {

        if (!inCombat)
        {
            if (shield < maxShield)
            {
                shield += maxShield * shieldRegenAmount;
            }

            if (health < maxHealth)
            {
                health += maxHealth * healthRegenAmount;
            }
        }

        if (health < 0)
        {
            Destroy(gameObject);
        }

        shieldBar.value = (float)shield;
        shieldBar.maxValue = (float)maxShield;
        healthBar.value = (float)health;
        healthBar.maxValue = (float)maxHealth;

    }

    public void TakeDamage(float damage) 
    {   
        inCombat = true;

        if (shield > 0)
        {
            if (shield - damage > 0)
            {
                shield -= damage;
            }
            else
            {
                shield = 0;
                health -= damage - shield;
            }
        }
        else
        {
            health -= damage;
        }

        StartCoroutine(CombatCooldown());
    }

    IEnumerator CombatCooldown()
    {
        yield return new WaitForSeconds(10f);
        inCombat = false;
    }
}


