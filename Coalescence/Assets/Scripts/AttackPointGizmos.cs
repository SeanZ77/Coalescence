using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointGizmos : MonoBehaviour
{

    public float attackRange;

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
