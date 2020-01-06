using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{

    public float damage;
    bool targetHit = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster" || collision.gameObject.tag == "Boss")
        {
            targetHit = true;
            collision.GetComponent<Unit>().TakeDamage(damage);
        }
    }
}
