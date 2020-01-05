using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nymph : Monster
{
    public State state;
    public float healingDistance;
    public float healQuantity;
    public Unit healTarget;

    private void Start()
    {
        state = State.idle;
        gameObject.GetComponent<ParticleSystem>().Stop();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(3);
        }
        //capacityCooldown -= Time.deltaTime;

        Flip();
        switch (state)
        {
            case State.idle:
                SearchTarget();
                if (healTarget.Health < healTarget.MaxHealth)
                {
                    state = State.chasing;
                }
                break;
            case State.chasing:
                direction = target.transform.position - transform.position;
                transform.position += direction.normalized * Speed * Time.deltaTime;
                if (direction.sqrMagnitude < healingDistance * healingDistance)
                {
                    anim.SetTrigger("Heal");
                    healTarget = target.GetComponent<Unit>();
                    gameObject.GetComponent<ParticleSystem>().Play();
                    healTarget.GetComponent<ParticleSystem>().Play();
                    state = State.healing;
                    break;
                }
                break;
            case State.healing:
                gameObject.GetComponent<Unit>().Heal(healQuantity);
                if (healTarget.Health >= healTarget.MaxHealth)
                {
                    anim.SetTrigger("Heal");
                    gameObject.GetComponent<ParticleSystem>().Stop();
                    healTarget.GetComponent<ParticleSystem>().Stop();
                    state = State.idle;
                    break;
                }
                break;
        }
    }

    private void SearchTarget()
    {
        target = GameObject.FindGameObjectWithTag("Boss");
        healTarget = target.GetComponent<Unit>();
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
