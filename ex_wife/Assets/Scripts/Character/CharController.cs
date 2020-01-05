using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharController : MonoBehaviour
{
    public float speed = 2.0f;
    public float metamorphe = 0.0f;
    public Slider progressionBar;
    public GameObject bullet;
    public GameObject smallbullet;

    private bool isMeta = false;
    private Animator animator;
    private float speedMeta = 0.08f;
    private float xAxisJoystick = 0.0f;
    private float yAxisJoystick = 0.0f;
    private bool shotgun = false;
    private bool bazooka = false;

    public int actualIndex = 0;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.SetFloat("Speed", 0f);   
    }

    void Update()
    {
        JoystickCall();
        Metamorphose();
        checkIndex();
    }

    void JoystickCall()
    {
        animator.SetFloat("Speed", 0f);
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.15f)
        {
            this.transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 0) * speed * Time.deltaTime;
            xAxisJoystick = Input.GetAxis("Horizontal");
            Flip();
            animator.SetFloat("Speed", 1.0f);
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.15f)
        {
            yAxisJoystick = Input.GetAxis("Vertical");
            this.transform.position += new Vector3(0, Input.GetAxis("Vertical"), 0) * speed * Time.deltaTime;
            animator.SetFloat("Speed", 1.0f);
        }
        if (Input.GetButtonUp("XboxA"))
        {
            animator.SetTrigger("Attack");
            Attack();
        }
        if (Input.GetButtonUp("scroll"))
        {
            Debug.Log(" plus ");
            actualIndex++;
        }
        if (Input.GetButtonUp("descroll"))
        {
            Debug.Log(" moin ");
            actualIndex--;
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
            switch (actualIndex)
            {
                case 0:
                    ClassicAttack();
                    break;
                case 1:
                    ShotGunAttack();
                    break;
                default:
                    break;
            }
        }
    }
    void Metamorphose()
    {
        progressionBar.value = metamorphe / 100.0f;
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
        GameObject InstantiateBullet = Instantiate(smallbullet, this.transform.position, Quaternion.identity);
        GameObject InstantiateBullet1 = Instantiate(smallbullet, this.transform.position, Quaternion.identity);
        GameObject InstantiateBullet2 = Instantiate(smallbullet, this.transform.position, Quaternion.identity);
        InstantiateBullet.transform.right = new Vector3(xAxisJoystick, yAxisJoystick, 0);
        InstantiateBullet1.transform.right = new Vector3(xAxisJoystick + 0.15f, yAxisJoystick + 0.15f, 0);
        InstantiateBullet2.transform.right = new Vector3(xAxisJoystick - 0.15f, yAxisJoystick - 0.15f, 0);
    }

    void BazookaAttack()
    {

    }

    void ClassicAttack()
    {
        GameObject InstantiateBullet = Instantiate(bullet, this.transform.position, Quaternion.identity);
        InstantiateBullet.transform.right = new Vector3(xAxisJoystick, yAxisJoystick, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ShotGun")
        {
            shotgun = true;
            Destroy(collision.gameObject);
        }
    }
    void checkIndex()
    {
        if (actualIndex < 0)
        {
            actualIndex = 1;
        }
        actualIndex = actualIndex % 2;
    }
}

