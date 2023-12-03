using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField]
    private int baseEnemies = 8;
    [SerializeField]
    private float enemiesPerSecond = 0.5f;
    [SerializeField]
    private float timeBetweenWaves = 5f;
    [SerializeField]
    private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    [SerializeField]
    private int currentWave = 1;
    [SerializeField]
    private float timeSinceLastSpawn;
    [SerializeField]
    private int enemiesAlive;
    [SerializeField]
    private int enemiesLeftToSpawn;
    [SerializeField]
    private bool isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }
    private void Start()
    {
        StartCoroutine(StartWave());
    }

    void Update()
    {
        if (!isSpawning) return;



        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0;
        }

        if(enemiesAlive <= 0 && enemiesLeftToSpawn == 0)
        {
            enemiesAlive = 0;
            EndWave();
        }
    }


    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        enemiesAlive = 0;
        yield return new WaitForSeconds(timeBetweenWaves);
        if (enemiesPerSecond < 4f)
        {
            enemiesPerSecond = enemiesPerSecond * 1.15f;
        }

        isSpawning = true; 
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());

    }

    private void SpawnEnemy()
    {
        if (currentWave <= 2)
        {
            GameObject prefabToSpawn = enemyPrefabs[0];
            Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
        }
        else if (currentWave > 2 && currentWave <= 9)
        {
            int randNum = Random.Range(0, 2);
            GameObject prefabToSpawn = enemyPrefabs[randNum];
            Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
        }
        else if (currentWave == 10)
        {
            enemiesLeftToSpawn = 1;
            GameObject prefabToSpawn = enemyPrefabs[2];
            Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
        }
        else if (currentWave >= 10)
        {
            int randNum = Random.Range(0, 6);
            Debug.Log("random Num is : " + randNum);
            int numChoice;
            if (randNum <= 2)
            {
                numChoice = 0;
            }
            else if (randNum > 2 && randNum <= 4)
            {
                numChoice = 1;
            }
            else
            {
                numChoice = 2;
            }
            Debug.Log("numChoice is : " + numChoice);
            GameObject prefabToSpawn = enemyPrefabs[numChoice];
            Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
        }
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

}
