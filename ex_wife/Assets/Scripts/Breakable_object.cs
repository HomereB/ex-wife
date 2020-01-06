using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_object : Unit
{
    public GameObject[] ammoDrop;
    public int dropProbability; //plus le nombre est grand, moins on a de chance de drop une munition


    protected void SpawnAmmo()
    {
        int ammoType = UnityEngine.Random.Range(0, ammoDrop.Length + dropProbability);
        if (ammoType < ammoDrop.Length)
        {
            Instantiate(ammoDrop[ammoType], transform.position, Quaternion.identity);
        }
    }

    protected override IEnumerator Die()
    {
        SpawnAmmo();
        yield return StartCoroutine(base.Die());
    }
}
