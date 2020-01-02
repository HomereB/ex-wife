using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharController : MonoBehaviour
{
    public float speed = 2.0f;
    public float metamorphe = 0.0f;
    public Slider slider;
    public GameObject bullet;

    private bool isMeta = false;
    private Animator animator;
    private GameObject weapon;
    private float speedMeta = 0.08f;
    private float xAxisStick;
    private float yAxisStick;
    private 

    void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.SetFloat("Speed", 0f);
        weapon = GameObject.Find("Weapon");
        
    }

    void Update()
    {
        JoystickCall();
        Metamorphose();
    }

    void JoystickCall()
    {
        animator.SetFloat("Speed", 0f);
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.15f)
        {
            xAxisStick = Input.GetAxis("Horizontal");
            this.transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 0) * speed * Time.deltaTime;
            Flip();
            animator.SetFloat("Speed", 1.0f);
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.15f)
        {
            yAxisStick = Input.GetAxis("Vertical");
            this.transform.position += new Vector3(0,Input.GetAxis("Vertical"), 0) * speed * Time.deltaTime;
            animator.SetFloat("Speed", 1.0f);
        }
        if (Input.GetButtonUp("XboxA"))
        {
            animator.SetTrigger("Attack");
            Attack();
        }
        if (Input.GetButtonUp("XboxB"))
        {
            //take weapons
        }
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") < 0.0f)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else //if (Input.GetAxis("Horizontal") > 0.0f)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void Attack()
    {
        if (!isMeta)
        {
            ShotGunAttack();
            /*GameObject bulletInstantiate = Instantiate(bullet, this.transform.position, Quaternion.identity);
            bulletInstantiate.transform.up = new Vector2(xAxisStick, yAxisStick);*/
        }
    }
    void Metamorphose()
    {
        slider.value = metamorphe / 100.0f;
        metamorphe += speedMeta;

        if (metamorphe >= 100.0f)
        {
            speedMeta = 0.0f;
            if (!isMeta)
            {
                //change du poids du layer pour changer le layer
                animator.SetLayerWeight(1, 1);
                isMeta = true;
            }
        }
    }

    void ShotGunAttack()
    {
        GameObject bulletInstantiate = Instantiate(bullet, this.transform.position, Quaternion.identity);
        GameObject bulletInstantiate1 = Instantiate(bullet, this.transform.position, Quaternion.identity);
        GameObject bulletInstantiate2 = Instantiate(bullet, this.transform.position, Quaternion.identity);
        bulletInstantiate.transform.up = new Vector2(xAxisStick, yAxisStick);
        bulletInstantiate1.transform.up = new Vector2(xAxisStick + 0.5f, yAxisStick);
        bulletInstantiate2.transform.up = new Vector2(xAxisStick - 0.5f, yAxisStick);
    }

    void ClassicAttack()
    {
        GameObject bulletInstantiate = Instantiate(bullet, this.transform.position, Quaternion.identity);
        bulletInstantiate.transform.up = new Vector2(xAxisStick, yAxisStick);
    }
}
