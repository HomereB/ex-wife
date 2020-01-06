using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharController : Unit
{
    [Header("Bullets prefabs")]
    public GameObject bullet;
    public GameObject smallbullet;
    public GameObject Bazookabullet;


    [Header("UI Settings")]
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    public List<Image> selectWeapon;
    public List<TextMeshProUGUI> MunitionsUI;
    public Slider HP;
    public Slider progressionBar;
    public TextMeshProUGUI HP_nb;
    public int[] tabMun;

    [Header("Sounds Settings")]
    public List<AudioClip> bulletsSound;

    public GameObject boss;

    private float metamorphe = 0.1f;
    private bool isMeta = false;
    private Animator animator;
    private float speedMeta = 0.7f;
    private float xAxisJoystick = 0.0f;
    private float yAxisJoystick = 0.0f;
    private float xAxisJoystickRight = 0.0f;
    private float yAxisJoystickRight = 0.0f;
    private int actualIndex = 0;
    private bool gameover = false;
    private bool hasAttacked = false;
    private bool victory = false;

    public GameObject crosshair;

    void Start()
    {
        Time.timeScale = 1;
        tabMun = new int[3]; tabMun[0] = 10000; tabMun[1] = 10; tabMun[2] = 0; 
        animator = this.GetComponent<Animator>();
        animator.SetFloat("Speed", 0f);   
    }

    void Update()
    {
        JoystickCall();
        //-----Meta-----
        Metamorphose();
        progressionBar.value = metamorphe / 100.0f;
        MetaInverse();

        checkIndex();
        UpdateUI();

        //-----UI End-----
        GameOver();
        Victory();
    }

    void JoystickCall()
    {
        animator.SetFloat("Speed", 0f);
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.15f)
        {
            this.transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 0) * 3.5f * Time.deltaTime;
            xAxisJoystick = Input.GetAxis("Horizontal");
            direction.x = xAxisJoystick;
            Flip();
            animator.SetFloat("Speed", 1.0f);
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.15f)
        {
            yAxisJoystick = Input.GetAxis("Vertical");
            this.transform.position += new Vector3(0, Input.GetAxis("Vertical"), 0) * 3.5f * Time.deltaTime;
            animator.SetFloat("Speed", 1.0f);
        }

        Vector3 crosshairPos = Vector3.zero;
        if (Mathf.Abs(Input.GetAxis("Horizontal Right")) > 0.05f)
        {
            xAxisJoystickRight = Input.GetAxis("Horizontal Right");
            crosshairPos += new Vector3(Input.GetAxis("Horizontal Right"), 0, 0) * 3.5f * Time.deltaTime;
        }

        if (Mathf.Abs(Input.GetAxis("Vertical Right")) > 0.05f)
        {
            yAxisJoystickRight = Input.GetAxis("Vertical Right");
            crosshairPos += new Vector3(0, -Input.GetAxis("Vertical Right"), 0) * 3.5f * Time.deltaTime;
        }

        crosshairPos.Normalize();
        crosshair.transform.position = transform.position + crosshairPos;

        if (Mathf.Abs(Input.GetAxis("Fire Trigger")) > 0.15f)
        {
            if(!hasAttacked)
            {
                animator.SetTrigger("Attack");
                Attack();
                hasAttacked = true;
            }
        }

        if (Mathf.Abs(Input.GetAxis("Fire Trigger")) < 0.15f)
        {
            hasAttacked = false;        
        }
        if (Input.GetButtonUp("scroll"))
        {
            actualIndex++;
        }
        if (Input.GetButtonUp("descroll"))
        {
            actualIndex--;
        }
        if (gameover || victory)
        {
            if (Input.GetButtonUp("Start"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
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
                case 2:
                    BazookaAttack();
                    break;
                default:
                    break;
            }
        }
    }
    void Metamorphose()
    {
        if (metamorphe >= 100.0f)
        {
            
            if (!isMeta)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                //change du poids du layer pour changer le layer
                animator.SetLayerWeight(1, 1);
                speedMeta = 0.7f;
                isMeta = true;
            }
        }
    }

    void MetaInverse()
    {
        if (isMeta)
        {
            metamorphe -= speedMeta;

            if (metamorphe <= 0.0f)
            {
                speedMeta = 0.0f;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                animator.SetLayerWeight(1, 0);
                isMeta = false;
            }
        }
    }

    void ShotGunAttack()
    {
        if (tabMun[1] > 0)
        {
            audioSource.clip = bulletsSound[1];
            audioSource.Play();
            metamorphe++;
            tabMun[1]--;
            GameObject InstantiateBullet = Instantiate(smallbullet, this.transform.position, Quaternion.identity);
            GameObject InstantiateBullet1 = Instantiate(smallbullet, this.transform.position, Quaternion.identity);
            GameObject InstantiateBullet2 = Instantiate(smallbullet, this.transform.position, Quaternion.identity);
            GameObject InstantiateBullet3 = Instantiate(smallbullet, this.transform.position, Quaternion.identity);
            GameObject InstantiateBullet4 = Instantiate(smallbullet, this.transform.position, Quaternion.identity);
            InstantiateBullet.transform.right = new Vector3(xAxisJoystickRight, -yAxisJoystickRight, 0);
            InstantiateBullet1.transform.right = new Vector3(xAxisJoystickRight + 0.15f, -yAxisJoystickRight + 0.15f, 0);
            InstantiateBullet2.transform.right = new Vector3(xAxisJoystickRight - 0.15f, -yAxisJoystickRight - 0.15f, 0);
            InstantiateBullet3.transform.right = new Vector3(xAxisJoystickRight + 0.075f, -yAxisJoystickRight + 0.075f, 0);
            InstantiateBullet4.transform.right = new Vector3(xAxisJoystickRight - 0.075f, -yAxisJoystickRight - 0.075f, 0);
        }
    }

    void BazookaAttack()
    {
        if (tabMun[2] > 0)
        {
            audioSource.clip = bulletsSound[2];
            audioSource.Play();
            metamorphe += 10.0f;
            GameObject InstantiateBullet = Instantiate(Bazookabullet, this.transform.position, Quaternion.identity);
            InstantiateBullet.transform.right = new Vector3(xAxisJoystickRight, -yAxisJoystickRight, 0);
            tabMun[2]--;
        }
    }

    void ClassicAttack()
    {
        audioSource.clip = bulletsSound[0];
        audioSource.Play();
        metamorphe += 0.5f;
        GameObject InstantiateBullet = Instantiate(bullet, this.transform.position, Quaternion.identity);
        InstantiateBullet.transform.right = new Vector3(xAxisJoystickRight, -yAxisJoystickRight, 0);
    }
    void checkIndex()
    {
        if (actualIndex < 0)
        {
            actualIndex = 1;
        }
        actualIndex = actualIndex % 3;
    }

    void UpdateUI()
    {
        foreach(Image i in selectWeapon)
        {
            i.color = Color.white;
        }
        selectWeapon[actualIndex].color = Color.green;

        MunitionsUI[0].text = tabMun[1].ToString();
        MunitionsUI[1].text = tabMun[2].ToString();

        HP.value = Health / MaxHealth;
        HP_nb.text = Health.ToString();
    }

    void GameOver()
    {
        if (Health <= 0)
        {
            Health = 0;
            gameover = true;
            //Time.timeScale = 0;
            //this.gameObject.SetActive(false);
            gameOverPanel.SetActive(true);
            
        }
    }

    void Victory()
    {
        if (boss.GetComponent<Boss>().Health <= 0)
        {
            victory = true;
            victoryPanel.SetActive(true);
        }
    }

    protected virtual IEnumerator Die()
    {
        //anim.SetTrigger("Dead");
        yield return new WaitForSeconds(1.4f);

        yield return new WaitForSeconds(0.1f);
        //Destroy(gameObject);
    }
}

