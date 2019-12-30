using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : Monster
{
    public State state;
    public float chargingDistance;
    public float attackingDistance;


    private void Start()
    {
        GetComponent<ParticleSystem>().Stop();
        target = GameObject.FindGameObjectWithTag("Player");
        state = State.chasing;
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
        switch(state)
        {       
            case State.chasing:
                transform.position += direction.normalized * Speed * Time.deltaTime;
                if(CapacityCooldown < 0 && direction.sqrMagnitude < chargingDistance * chargingDistance)
                {
                    anim.SetTrigger("Charge");
                    capacityCooldown = maxCapacityCooldown;
                    state = State.charging;
                }
                if (direction.sqrMagnitude < attackingDistance * attackingDistance)
                {
                    anim.SetTrigger("Attack");
                    state = State.attacking;
                }
                break;
            case State.attacking:

                break;
            case State.charging:
                direction = target.transform.position - transform.position;
                transform.position += direction.normalized * Speed * 3 * Time.deltaTime;
                break;
        }
    }


    

    public enum State
    {
        idle,
        chasing,
        attacking,
        charging,
        hit,
        death,
    }
}
