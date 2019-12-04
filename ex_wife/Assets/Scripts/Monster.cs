using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Monster : Unit
{
    public float capacityCooldown;
    public float maxCapacityCooldown;

    public float CapacityCooldown { get => capacityCooldown; set => capacityCooldown = value; }

    public GameObject target;
    protected Vector3 direction;

    public Animator anim;

    public GameObject[] ammoDrop;
    public int dropProbability; //plus le nombre est grand, moins on a de chance de drop une munition
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


    protected void Flip()
    {
        if (direction.x < 0.0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    protected IEnumerator Die()
    {
        anim.SetTrigger("Dead");
        yield return new WaitForSeconds(1.4f);
        SpawnAmmo();
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    protected void SpawnAmmo()
    {
        int ammoType = UnityEngine.Random.Range(0,ammoDrop.Length+dropProbability);
        if(ammoType<ammoDrop.Length)
        {
            Instantiate(ammoDrop[ammoType]);
        }
    }
}