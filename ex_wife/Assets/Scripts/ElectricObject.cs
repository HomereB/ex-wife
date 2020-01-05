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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Unit>().TakeDamage(damage);
            //GetComponent<Collider>().enabled = false;
            //Destroy(gameObject);
        }
    }
}
