using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public float damages;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            Destroy(collision.gameObject);
        }
    }
}
