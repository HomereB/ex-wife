using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float activeTime;
    public float destructionTime;
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
        velocity *= 0.99f;
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(activeTime);
        velocity = Vector3.zero;
        gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(destructionTime);
        Destroy(gameObject);


    }
}
