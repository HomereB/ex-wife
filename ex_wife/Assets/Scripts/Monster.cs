using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    public float capacityCooldown;
    public float maxCapacityCooldown;

    public float CapacityCooldown { get => capacityCooldown; set => capacityCooldown = value; }

    public GameObject target;


    public Animator anim;


    protected void TakeDamage(float dam)
    {
        Health -= dam;
        
        if ( Health <= 0 )
        {
            StartCoroutine("Die");
        }
        else
        {
            anim.SetTrigger("Hit");
        }
    }

    protected IEnumerator Die()
    {
        anim.SetTrigger("Dead");
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}