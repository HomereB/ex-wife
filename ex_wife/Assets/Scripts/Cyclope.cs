﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclope : Monster
{
    public State state;
    public float throwingDistance;
    public float attackingDistance;
    public GameObject projectile;
    public float projectileSpeed;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        state = State.idle;
    }

    private void Update()
    {

        capacityCooldown -= Time.deltaTime;
        if (target != null)
        {
            direction = target.transform.position - transform.position;
        }
        Flip();
        switch (state)
        {
            case State.idle:
                target = GameObject.FindGameObjectWithTag("Player");

                if (target!=null)
                {
                    anim.SetBool("hasTarget",true);

                    state = State.chasing;
                }
                break;
            case State.chasing:
                transform.position += direction.normalized * Speed * Time.deltaTime;
                if (direction.sqrMagnitude < attackingDistance * attackingDistance)
                {
                    anim.SetTrigger("Attack");
                    state = State.attacking;
                    break;
                }
                else if (CapacityCooldown < 0 && direction.sqrMagnitude < throwingDistance * throwingDistance)
                {
                    anim.SetTrigger("Throw");
                    capacityCooldown = maxCapacityCooldown;
                    state = State.throwing;
                    break;
                }
                break;
            case State.attacking:
                break;

            case State.throwing:
                break;
        }
    }

    public void ThrowRock()
    {
        GameObject go = Instantiate(projectile);
        go.transform.position = transform.position + Vector3.up * 0.2f;
        go.GetComponent<Rock>().velocity = direction.normalized*projectileSpeed;
    }

    public enum State
    {
        idle,
        chasing,
        attacking,
        throwing,
        hit,
        death,
    }
}
