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

    public float damage;

    public GameObject[] ammoDrop;
    public int dropProbability; //plus le nombre est grand, moins on a de chance de drop une munition



    protected void SpawnAmmo()
    {
        int ammoType = UnityEngine.Random.Range(0,ammoDrop.Length+dropProbability);
        if(ammoType<ammoDrop.Length)
        {
            Instantiate(ammoDrop[ammoType], transform.position,Quaternion.identity);
        }
    }

    protected override IEnumerator Die()
    {
        SpawnAmmo();
        yield return StartCoroutine(base.Die());        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            collision.gameObject.GetComponent<Unit>().TakeDamage(damage);
        }
    }
}