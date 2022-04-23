using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAbilityController : MonoBehaviour
{
    public float offensiveCooldown;
    private bool canUseOffensive = true;
    private bool offensiveAbilitySelected = false;
    private bool offensiveAbilityActive = false;



    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (offensiveAbilitySelected)
            {
                StartCoroutine(OffensiveAbility(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0)));
            }
        }
        if (Input.GetKeyDown("q"))
        {
            if (canUseOffensive)
            {
                offensiveAbilitySelected = !offensiveAbilitySelected;
            }
            
        }
        
    }

    IEnumerator OffensiveAbility(Vector3 attackPos)
    {
        offensiveAbilitySelected = false;
        canUseOffensive = false;
        PlayerMovementController movementController = GetComponent<PlayerMovementController>();
        movementController.input = false;

        //Vector2 direction = attackPos - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        //transform.rotation = rotation;
        GetComponent<Rigidbody2D>().velocity = new Vector2(attackPos.x - transform.position.x, attackPos.y - transform.position.y);

        yield return new WaitForSeconds(1f);

        offensiveAbilityActive = false;
        movementController.input = true;
        StartCoroutine(OffensiveCooldown());
    }

    IEnumerator OffensiveCooldown()
    {
        yield return new WaitForSeconds(offensiveCooldown);
        canUseOffensive = true;
    }

}
