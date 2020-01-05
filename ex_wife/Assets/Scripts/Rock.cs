using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float activeTime;
    public float destructionTime;
    public float speedMultiplier;
    public float damage;
    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DestroyAfterTime");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
        velocity *= speedMultiplier;
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(activeTime);
        velocity = Vector3.zero;
        gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(destructionTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            StopAllCoroutines();
            collision.gameObject.GetComponent<Unit>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
