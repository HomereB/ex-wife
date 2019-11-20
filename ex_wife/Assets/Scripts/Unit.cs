using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float speed;

    public float Health { get => health; set => health = value; }
    public float Speed { get => speed; set => speed = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
