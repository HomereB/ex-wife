using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float damage;
    public float explosionRadius;
    public Animator anim;
    bool targetHit = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!targetHit)
        {
            this.transform.position += this.transform.right * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Monster" || collision.gameObject.tag == "Boss")
        {
            targetHit = true;
            anim.enabled = true;
            GetComponent<Collider2D>().enabled = false;
            Collider2D[] hitMonsters = Physics2D.OverlapCircleAll(transform.position,explosionRadius);
            foreach(Collider2D col in hitMonsters)
            {
                col.GetComponent<Unit>().TakeDamage(damage);
            }
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
