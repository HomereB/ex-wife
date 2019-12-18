using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nymph : Monster
{
    public State state;
    public float healingDistance;
    public GameObject projectile;


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
        capacityCooldown -= Time.deltaTime;
        direction = target.transform.position - transform.position;
        Flip();
        switch (state)
        {
            case State.chasing:
                transform.position += direction.normalized * Speed * Time.deltaTime;
                if (direction.sqrMagnitude < healingDistance * healingDistance)
                {
                    anim.SetTrigger("Heal");
                    state = State.healing;
                    break;
                }
                break;
            case State.healing:
                break;
        }
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
