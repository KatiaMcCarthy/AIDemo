using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;
    public int numberOfWaves = 6; //sets it to 6 to start


    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;

    private bool b_finnishedSpawning;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves); //wait to start the next coroutine till the time has passed
        StartCoroutine(SpawnWave(index)); //moves the spawn the next wave
    }

    IEnumerator SpawnWave(int index)
    {
        currentWave = waves[index];

        for (int i = 0; i < currentWave.count; i++)
        {
            if(player == null)
            {
                yield break; //exits the for loop
            }

            GameObject randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];//chooses an enemy from the array of enemies
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)]; //chooses an spot from the array of spots
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation); //spawns the enemy

            if(i == currentWave.count -1)
            {
                b_finnishedSpawning = true;
            }
            else
            {
                b_finnishedSpawning = false;
            }

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns); //waits for however long we want to wait (can vary baised of off individual wave properties
        }
    }

    private void Update()
    {
        
        if(b_finnishedSpawning == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) //if we are done spawning and no more enemies exist on the map
        {
            b_finnishedSpawning = false; //reset the finnished spawning
            if(currentWaveIndex + 1 < waves.Length) //if the new wave is still less than the total number of waves
            {
                currentWaveIndex++; //increase our index, (were on a new wave)
                StartCoroutine(StartNextWave(currentWaveIndex)); //pass in the index to the next new wave
            }
            else
            { 

                Debug.Log("Game Finnished");  //temp win screen
            }

        }
      
    }

    //Getters

    public int GetCurrentWaveIndex()
    {
        return currentWaveIndex;
    }
}
