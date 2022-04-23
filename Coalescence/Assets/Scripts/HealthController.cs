using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public float health, maxHealth;
    public Slider slider;

    void Update() 
    {
        if (health < 0)
        {
            Destroy(gameObject);
        }

        slider.value = health;
        slider.maxValue = maxHealth;
    }

    public void TakeDamage(float damage) 
    {
        health -= damage;
    }

}
