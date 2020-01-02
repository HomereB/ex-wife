using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricObject : MonoBehaviour
{
    public float damage;
    public GameObject spawn;
    public float timeBeforeDestruction;

    private void Update()
    {
        timeBeforeDestruction -= Time.deltaTime;
        if(timeBeforeDestruction<=0)
        {
            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void SpawnObject()
    {
        Instantiate(spawn,transform.position,Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Unit>().TakeDamage(damage);
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject);
        }
    }
}
