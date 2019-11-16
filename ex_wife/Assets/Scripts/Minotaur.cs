using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : Monster
{
    public State state;
    public GameObject target;
    public float chargingDistance;
    public float attackingDistance;
    public Animator anim;


    private void Start()
    {
        state = State.chasing;
    }

    private void Update()
    {
        capacityCooldown -= Time.deltaTime;
        Vector3 direction = target.transform.position - transform.position;
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
                direction = target.transform.position - transform.position;
                if (direction.sqrMagnitude > attackingDistance * attackingDistance)
                {
                    state = State.chasing;
                }
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
