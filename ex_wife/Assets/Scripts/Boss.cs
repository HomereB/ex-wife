using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Unit
{
    public State state;

    public float capacityCooldown1;
    public float capacityCooldown2;

    public float maxCapacityCooldown1;
    public float maxCapacityCooldown2;

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
    // Start is called before the first frame update
    void Start()
    {
        capacityCooldown1 = maxCapacityCooldown1;
        capacityCooldown2 = maxCapacityCooldown2;
        target = target = GameObject.FindGameObjectWithTag("Player");

        lightStrkP1 = LightningStrikeAttack(4);
        lightStrkP2 = LightningStrikeAttack(8);
        lightStrkP3 = LightningStrikeAttack(12);

        ElekOrbP2 = ElectricOrbAttack(8,1);
        ElekOrbP3 = ElectricOrbAttack(16,2);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.phase1:

                capacityCooldown1 -= Time.deltaTime;

                if (capacityCooldown1 <= 0)
                {
                    StartCoroutine(lightStrkP1);
                    capacityCooldown1 = maxCapacityCooldown1;
                }

                if (Health<=MaxHealth*0.66f)
                {
                    state = State.phase2;
                }

                break;

            case State.phase2:

                capacityCooldown1 -= Time.deltaTime;

                if (capacityCooldown1 <= 0)
                {
                    StartCoroutine(lightStrkP2);
                    capacityCooldown1 = maxCapacityCooldown1 / 2.0f;

                }

                capacityCooldown2 -= Time.deltaTime;

                if (capacityCooldown2 <= 0)
                {
                    StartCoroutine(ElekOrbP2);
                }

                if (Health <= MaxHealth * 0.33f)
                {
                    state = State.phase2;
                }

                break;

            case State.phase3:

                capacityCooldown1 -= Time.deltaTime;

                if (capacityCooldown1 <= 0)
                {
                    StartCoroutine(lightStrkP3);
                    capacityCooldown1 = maxCapacityCooldown1 / 3.0f;
                }

                capacityCooldown2 -= Time.deltaTime;

                if (capacityCooldown2 <= 0)
                {
                    StartCoroutine(ElekOrbP3);
                    capacityCooldown2 = maxCapacityCooldown2 / 2.0f;
                }

                break;
        }
    }

    IEnumerator LightningStrikeAttack(int nbStrikes)
    {
        for(int i =0 ; i < nbStrikes;i++)
        {
            float x = Random.Range(-lightningStrikeDistanceToTarget, lightningStrikeDistanceToTarget);
            float y = Random.Range(-lightningStrikeDistanceToTarget, lightningStrikeDistanceToTarget);
            Vector3 strikePos = target.transform.position + new Vector3(x, y, 0);
            Instantiate(attacks[0],strikePos,Quaternion.identity);
            yield return new WaitForSeconds(delayBetweenStrikes);
        }
    }

    IEnumerator ElectricOrbAttack(int nbOrbs, int  nbWaves)
    {
        for (int j = 0; j < nbWaves; j++)
        {
            for (int i = 0; i < nbOrbs; i++)
            {
                float angle = i * 2 * Mathf.PI / nbOrbs;
                Vector3 orbPos = transform.position + new Vector3(Mathf.Cos(angle) * orbDistanceFromSource, Mathf.Sin(angle) * orbDistanceFromSource, 0);
                Instantiate(attacks[1], orbPos, Quaternion.identity);
                yield return new WaitForSeconds(delayBetweenStrikes);
            }
            yield return new WaitForSeconds(delayBetweenWaves);
        }
    }

    public enum State
    {
        phase1,
        phase2,
        phase3
    }
}
