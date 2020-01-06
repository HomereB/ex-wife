using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealDrop : MonoBehaviour
{
    public int healAmount;
    public AudioSource audiosource;
    public AudioClip sound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Unit>().Heal(healAmount);
            audiosource.clip = sound; audiosource.Play();
            Destroy(gameObject);
        }
    }
}
