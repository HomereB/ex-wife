using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nymph : Monster
{
    public State state;
    public float healingDistance;
    public int healQuantity;
    public Monster healTarget;

    private void Start()
    {
        state = State.idle;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(3);
        }
        capacityCooldown -= Time.deltaTime;
        direction = target.transform.position - transform.position;
        Flip();
        switch (state)
        {
            case State.idle:
                SearchTarget();
                break;
            case State.chasing:
                transform.position += direction.normalized * Speed * Time.deltaTime;
                if (direction.sqrMagnitude < healingDistance * healingDistance)
                {
                    anim.SetTrigger("Heal");
                    healTarget = target.GetComponent<Monster>();
                    gameObject.GetComponent<ParticleSystem>().Play();
                    healTarget.GetComponent<ParticleSystem>().Play();
                    state = State.healing;
                    break;
                }
                break;
            case State.healing:
                gameObject.GetComponent<Monster>().Heal(healQuantity);
                if (healTarget.Health >= MaxHealth)
                {
                    anim.SetTrigger("Heal");
                    gameObject.GetComponent<ParticleSystem>().Stop();
                    healTarget.GetComponent<ParticleSystem>().Stop();
                    //search for target
                    state = State.chasing;
                    break;
                }
                break;
        }
    }

    private void SearchTarget()
    {
        throw new NotImplementedException();
    }

    public enum State
    {
        idle,
        chasing,
        healing,
        hit,
        death,
    }
}
