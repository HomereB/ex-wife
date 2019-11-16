using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public float speed = 2.0f;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.SetFloat("Speed", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        JoystickCall();
    }

    void JoystickCall()
    {
        animator.SetFloat("Speed", 0f);
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.15f)
        {
            this.transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 0) * speed * Time.deltaTime;

            Flip();

            animator.SetFloat("Speed", 1.0f);
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.15f)
        {
            this.transform.position += new Vector3(0,Input.GetAxis("Vertical"), 0) * speed * Time.deltaTime;
            animator.SetFloat("Speed", 1.0f);
        }
        
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") < 0.0f)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
