using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private float health;

    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float speed;

    public float Health { get => health; set => health = value; }
    public float Speed { get => speed; set => speed = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    public AudioClip hitSound, DeadSound;
    public AudioSource audioSource;

    protected Vector3 direction;

    public Animator anim;

    public virtual void Heal(float hp)
    {
        health += hp;

        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }


    public virtual void TakeDamage(float dam)
    {
        Health -= dam;

        if (Health <= 0)
        {
            audioSource.clip = DeadSound; audioSource.Play();
            StartCoroutine("Die");
        }
        else
        {
            audioSource.clip = hitSound; audioSource.Play();
            anim.SetTrigger("Hit");
        }
    }

    protected void Flip()
    {
        if (direction.x < 0.0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    protected virtual IEnumerator Die()
    {
        anim.SetTrigger("Dead");
        yield return new WaitForSeconds(1.4f);
        
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
