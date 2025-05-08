using System;
using System.Collections.Generic;
using Tools;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Repeater))]
public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public List<Transform> spawnLocations = new();
    public GameObjectPool enemyPool;
    private List<GameObject> activeEnemies = new();
    public int waveSize = 5;
    private Repeater repeater;
    public float pushForce = 5;
    public float spawnSpread = 2;
    public int randomSpawn = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        repeater = GetComponent<Repeater>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            if (!activeEnemies[i].activeInHierarchy)
            {
                activeEnemies.RemoveAt(i);
            }
        }
        if (activeEnemies.Count == 0 && !repeater.isActiveAndEnabled)
        {
            Debug.Log("Starting new wave");
            StartNewWave();
        }
    }

    private void StartNewWave()
    {
        repeater.repeatCount = waveSize;
        repeater.enabled = true;
    }

    public void SpawnEnemy()
    {
        GameObject enemy = enemyPool.GetFirstAvailableObject();
        if (enemy != null)
        {        Rigidbody2D rb;
        enemy.SetActive(true);
        activeEnemies.Add(enemy);
      
        int furthestSpawnPoint= 0;
        float maxDistance = 0;
        int i = 0;

        do
        {
            float distance = Vector3.Distance(spawnLocations[i].transform.position, player.transform.position);
            if (maxDistance < distance)
            {
                maxDistance = distance;
                furthestSpawnPoint = i;
            } 
            i++;
        } while (i < spawnLocations.Count);

        if(randomSpawn > 0)
            {
                furthestSpawnPoint = (furthestSpawnPoint + UnityEngine.Random.Range(-randomSpawn,randomSpawn))%spawnLocations.Count;
            }

        enemy.transform.position = spawnLocations[furthestSpawnPoint].position + new Vector3(UnityEngine.Random.Range(-spawnSpread, spawnSpread), UnityEngine.Random.Range(-spawnSpread, spawnSpread), 0);

         if( enemy.TryGetComponent<Rigidbody2D>(out rb))
        {
            Vector2 dir = new Vector2() - new Vector2 (enemy.transform.position.x, enemy.transform.position.y );
            rb.AddForce(pushForce* dir.normalized);
        }
        } else
        {
            Debug.Log("Over Pool limit!");
        }

    }
}
