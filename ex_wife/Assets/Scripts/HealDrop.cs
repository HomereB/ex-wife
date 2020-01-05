﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealDrop : MonoBehaviour
{
    public int healAmount;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Unit>().Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
