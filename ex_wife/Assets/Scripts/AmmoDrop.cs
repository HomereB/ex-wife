using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDrop : MonoBehaviour
{
    public int ammoToRefill;
    public int amountToRefill;
    public AudioSource audiosource;
    public AudioClip sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CharController>().tabMun[ammoToRefill] += amountToRefill;
            Destroy(gameObject);
        }
    }
}
