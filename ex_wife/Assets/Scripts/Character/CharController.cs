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
    public List<Image> selectWeapon;
    public List<TextMeshProUGUI> MunitionsUI;
    public Slider HP;
    public Slider progressionBar;
    public TextMeshProUGUI HP_nb;
    public int[] tabMun;

    private float metamorphe = 0.0f;
    private bool isMeta = false;
    private Animator animator;
    private float speedMeta = 0.08f;
    private float xAxisJoystick = 0.0f;
    private float yAxisJoystick = 0.0f;
    private bool shotgun = false;
    private bool bazooka = false;
    private int actualIndex = 0;
    private bool gameover = false;

    void Start()
    {
        tabMun = new int[3]; tabMun[0] = 10000; tabMun[1] = 10; tabMun[2] = 0; 
        animator = this.GetComponent<Animator>();
        animator.SetFloat("Speed", 0f);   
    }

    void Update()
    {
        JoystickCall();
        Metamorphose();
        checkIndex();
        UpdateUI();
        GameOver();
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
        if (Input.GetButtonUp("XboxA"))
        {
            animator.SetTrigger("Attack");
            Attack();
        }
        if (Input.GetButtonUp("scroll"))
        {
            actualIndex++;
        }
        if (Input.GetButtonUp("descroll"))
        {
            actualIndex--;
        }
        if (gameover)
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
        if (tabMun[1] > 0)
        {
            metamorphe++;
            tabMun[1]--;
            GameObject InstantiateBullet = Instantiate(smallbullet, this.transform.position, Quaternion.identity);
            GameObject InstantiateBullet1 = Instantiate(smallbullet, this.transform.position, Quaternion.identity);
            GameObject InstantiateBullet2 = Instantiate(smallbullet, this.transform.position, Quaternion.identity);
            InstantiateBullet.transform.right = new Vector3(xAxisJoystick, yAxisJoystick, 0);
            InstantiateBullet1.transform.right = new Vector3(xAxisJoystick + 0.15f, yAxisJoystick + 0.15f, 0);
            InstantiateBullet2.transform.right = new Vector3(xAxisJoystick - 0.15f, yAxisJoystick - 0.15f, 0);
        }
    }

    void BazookaAttack()
    {
        if (tabMun[2] > 0)
        {
            metamorphe += 10.0f;
            GameObject InstantiateBullet = Instantiate(Bazookabullet, this.transform.position, Quaternion.identity);
            InstantiateBullet.transform.right = new Vector3(xAxisJoystick, yAxisJoystick, 0);
            tabMun[2]--;
        }
    }

    void ClassicAttack()
    {
        metamorphe += 0.5f;
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
            gameover = true;
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            
        }
    }
}

