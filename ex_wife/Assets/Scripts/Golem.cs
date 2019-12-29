using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Monster
{
    public State state;
    public float attackingDistance;


    private void Start()
    {
        state = State.chasing;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(3);
        }
        direction = target.transform.position - transform.position;
        Flip();
        switch (state)
        {
            case State.chasing:
                transform.position += direction.normalized * Speed * Time.deltaTime;
                if (direction.sqrMagnitude < attackingDistance * attackingDistance)
                {
                    anim.SetTrigger("Attack");
                    state = State.attacking;
                }
                break;
            case State.attacking:

                break;
        }
    }

    public enum State
    {
        idle,
        chasing,
        attacking,
        hit,
        death,
    }
}
