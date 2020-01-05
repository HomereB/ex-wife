using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Monster
{
    public State state;
    public float attackingDistance;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        state = State.chasing;
        anim.SetBool("hasTarget", true);

    }

    private void Update()
    {

        if (target != null)
        {
            direction = target.transform.position - transform.position;
        }
        Flip();
        Debug.Log(state.ToString());

        switch (state)
        {
            //case State.idle:
            //    target = GameObject.FindGameObjectWithTag("Player");
            //    state = State.chasing;
            //    if (target != null)
            //    {
                    
            //    }
                //break;

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
