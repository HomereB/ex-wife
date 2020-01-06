using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_position : MonoBehaviour
{
    private GameObject player;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.transform.position = player.transform.position - Vector3.forward*10;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = player.transform.position - Vector3.forward * 10;
        if(minX > transform.position.x)
        {
            gameObject.transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }
        if (maxX < transform.position.x)
        {
            gameObject.transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }
        if (minY > transform.position.y)
        {
            gameObject.transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        }
        if (maxY < transform.position.y)
        {
            gameObject.transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }
    }
}
