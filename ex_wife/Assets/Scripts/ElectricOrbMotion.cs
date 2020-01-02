using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricOrbMotion : MonoBehaviour
{
    public float angle;
    public float initialSpeed;
    public float speedMultiplicator;
    public float maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, angle*Time.deltaTime);
        initialSpeed *= speedMultiplicator;
        if(initialSpeed>=maxSpeed)
        {
            initialSpeed = maxSpeed;
        }
        transform.position += transform.up * maxSpeed * Time.deltaTime;
    }
}
