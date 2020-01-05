using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Boss : Unit
{
    public State state;

    public float capacityCooldown1;
    public float capacityCooldown2;
    public float capacityCooldown3;

    public float maxCapacityCooldown1;
    public float maxCapacityCooldown2;
    public float maxCapacityCooldown3;

    public GameObject[] attacks;

    public GameObject target;

    public float delayBetweenStrikes;
    public float lightningStrikeDistanceToTarget;

    public float delayBetweenWaves;
    public float delayBetweenOrbs;
    public float orbDistanceFromSource;

    IEnumerator lightStrkP1;
    IEnumerator lightStrkP2;
    IEnumerator lightStrkP3;
                           
    IEnumerator ElekOrbP2;
    IEnumerator ElekOrbP3;
    IEnumerator ElekOrbP4;

    //IEnumerator CleanDmgText;

    public SpawnMonsters spwn;
    public SpriteRenderer SR;
    public Image healthBar;
    public TextMeshProUGUI dmgTaken;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ParticleSystem>().Stop();

        capacityCooldown1 = maxCapacityCooldown1;
        capacityCooldown2 = maxCapacityCooldown2;
        target = target = GameObject.FindGameObjectWithTag("Player");

        lightStrkP1 = LightningStrikeAttack(4);
        lightStrkP2 = LightningStrikeAttack(8);
        lightStrkP3 = LightningStrikeAttack(12);

        ElekOrbP2 = ElectricOrbAttack(8,1,0,0);
        ElekOrbP3 = ElectricOrbAttack(16, 1,0,0);
        ElekOrbP4 = ElectricOrbAttack(16,2,0.5f,5);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(40);
        }

        switch (state)
        {
            case State.phase1:
                capacityCooldown1 -= Time.deltaTime;

                if (capacityCooldown1 <= 0)
                {
                    lightStrkP1 = LightningStrikeAttack(4);

                    StartCoroutine(lightStrkP1);
                    capacityCooldown1 = maxCapacityCooldown1;
                }

                if (Health<= MaxHealth * 0.66f)
                {
                    capacityCooldown1 = 5;
                    SR.color = Color.magenta;
                    state = State.phase2;
                }

                break;

            case State.phase2:

                capacityCooldown1 -= Time.deltaTime;

                if (capacityCooldown1 <= 0)
                {
                    lightStrkP2 = LightningStrikeAttack(8);

                    StartCoroutine(lightStrkP2);
                    capacityCooldown1 = maxCapacityCooldown1 / 2.0f;

                }

                capacityCooldown2 -= Time.deltaTime;

                if (capacityCooldown2 <= 0)
                {
                    ElekOrbP2 = ElectricOrbAttack(8, 1, 0, 0);

                    StartCoroutine(ElekOrbP2);
                    capacityCooldown2 = maxCapacityCooldown2;

                }

                if (Health <= MaxHealth * 0.33f)
                {
                    capacityCooldown1 = 5;
                    capacityCooldown2 = 15;
                    SR.color = Color.red;

                    state = State.phase3;
                }

                break;

            case State.phase3:

                capacityCooldown1 -= Time.deltaTime;

                if (capacityCooldown1 <= 0)
                {
                    lightStrkP3 = LightningStrikeAttack(12);


                    StartCoroutine(lightStrkP3);
                    capacityCooldown1 = maxCapacityCooldown1 / 3.0f;
                }

                capacityCooldown2 -= Time.deltaTime;

                if (capacityCooldown2 <= 0)
                {
                    ElekOrbP3 = ElectricOrbAttack(16, 1, 0, 0);

                    StartCoroutine(ElekOrbP3);
                    capacityCooldown2 = maxCapacityCooldown2 / 2.0f;
                }

                capacityCooldown3 -= Time.deltaTime;

                if (capacityCooldown3 <= 0)
                {
                    ElekOrbP4 = ElectricOrbAttack(16, 2, 0.5f, 5);
                    StartCoroutine(ElekOrbP4);
                    capacityCooldown3 = maxCapacityCooldown3;
                }

                break;
        }
    }


    private IEnumerator CleanDamageText()
    {
        yield return new WaitForSeconds(3.0f);
        dmgTaken.text = "";
    }

    IEnumerator LightningStrikeAttack(int nbStrikes)
    {
        for(int i =0 ; i < nbStrikes;i++)
        {
            float x = UnityEngine.Random.Range(-lightningStrikeDistanceToTarget, lightningStrikeDistanceToTarget);
            float y = UnityEngine.Random.Range(-lightningStrikeDistanceToTarget, lightningStrikeDistanceToTarget);
            Vector3 strikePos = target.transform.position + new Vector3(x, y, 0);
            Instantiate(attacks[0],strikePos,Quaternion.identity);
            yield return new WaitForSeconds(delayBetweenStrikes);
        }
    }

    IEnumerator ElectricOrbAttack(int nbOrbs, int nbWaves, float timeBetweenOrbs, float timeBetweenWaves )
    {
        for (int j = 0; j < nbWaves; j++)
        {
            for (int i = 0; i < nbOrbs; i++)
            {
                float angle = 1+(i * 2 * Mathf.PI / nbOrbs);
                Vector3 orbPos = transform.position + new Vector3(Mathf.Cos(angle) * orbDistanceFromSource, Mathf.Sin(angle) * orbDistanceFromSource, 0);
                GameObject go = Instantiate(attacks[1], orbPos, Quaternion.identity);
                go.transform.up = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle) , 0);
                if (timeBetweenOrbs > 0) 
                yield return new WaitForSeconds(timeBetweenOrbs);
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    public override void  TakeDamage(float dam)
    {
        StopCoroutine("CleanDamageText");
        Health -= dam * spwn.waveNumber;
        if (Health <= 0)
        {
            Health = 0;
            //StartCoroutine("Die");
        }
        else
        {
            //anim.SetTrigger("Hit");
        }
        Debug.Log(dmgTaken.text);
        if(dmgTaken.text!="")
        {
            dmgTaken.text = (int.Parse(dmgTaken.text) + dam * spwn.waveNumber).ToString();
        }
        else
        {
            dmgTaken.text = (dam * spwn.waveNumber).ToString();
        }
        healthBar.fillAmount = (float)Health / (float)MaxHealth;
        StartCoroutine("CleanDamageText");
    }

    public enum State
    {
        phase1,
        phase2,
        phase3
    }
}
