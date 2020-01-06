using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_object : Unit
{
    public GameObject[] ammoDrop;
    public int dropProbability;
    public float repairTime;
    private Sprite initialSprite;

    private void Start()
    {
        initialSprite = GetComponent<SpriteRenderer>().sprite;
    }

    protected void SpawnAmmo()
    {
        int ammoType = UnityEngine.Random.Range(0, ammoDrop.Length + dropProbability);
        if (ammoType < ammoDrop.Length)
        {
            Instantiate(ammoDrop[ammoType], transform.position, Quaternion.identity);
        }
    }

    public override void TakeDamage(float dam)
    {
        Health -= dam;

        if (Health <= 0)
        {
            audioSource.clip = DeadSound; audioSource.Play();
            anim.enabled = true;
            StartCoroutine("Break");
        }
    }

    protected IEnumerator Break()
    {
        SpawnAmmo();
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(repairTime);
        GameObject go = Instantiate(gameObject);
        go.GetComponent<Collider2D>().enabled = true;

        go.GetComponent<Animator>().enabled = false;
        go.GetComponent<SpriteRenderer>().sprite = initialSprite;

        Destroy(gameObject);
    }
}
