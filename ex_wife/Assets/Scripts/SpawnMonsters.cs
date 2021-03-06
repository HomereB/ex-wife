﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnMonsters : MonoBehaviour
{
    public GameObject[] monsterToSpawn;
    public int[] nbMonstersToSpawn;
    public int[] incrementMonsterPerWaves;

    public int waveNumber = 0;
    public TextMeshProUGUI txtWave;
    public TextMeshProUGUI txtTimeToNewtWave;
    public float timeToNextWave;
    public float initialTimeBetweenWaves;
    public int incrementTimeBetweenWaves;

    public float spawnCooldown;

    public GameObject[] spawners;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        txtWave.text = "Wave " + waveNumber;

        timeToNextWave += incrementTimeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        timeToNextWave -= Time.deltaTime;
        txtTimeToNewtWave.text = "Time until next wave : " + ((int)timeToNextWave).ToString();

        if(timeToNextWave <= 0)
        {
            IncrementWaveNumber();
            StartCoroutine("SpawnWave");
            timeToNextWave = initialTimeBetweenWaves + waveNumber * incrementTimeBetweenWaves;
        }
    }

    private void IncrementWaveNumber()
    {
        waveNumber++;
        txtWave.text = "Wave " + waveNumber;
    }


    IEnumerator SpawnWave()
    {
        for (int i = 0; i < monsterToSpawn.Length; i++)
        {
            for (int j = 0; j < nbMonstersToSpawn[i]; j++)
            {
                Spawn(monsterToSpawn[i], spawners[j % spawners.Length]);
                yield return new WaitForSeconds(spawnCooldown);
            }
        }

        for (int i = 0; i < monsterToSpawn.Length; i++)
        {
            nbMonstersToSpawn[i] += incrementMonsterPerWaves[i];   
        }
    }



    void Spawn(GameObject m, GameObject spwn)
    {
        GameObject go = Instantiate(m, spwn.transform.position, Quaternion.identity);
    }
}
