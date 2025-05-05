using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidGenerator : MonoBehaviour
{
    public static AsteroidGenerator Instance { get; private set; }

    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<Sprite> _bigAsteroidSprites;
    [SerializeField] private List<Sprite> _mediumAsteroidSprites;
    [SerializeField] private List<Sprite> _smallAsteroidSprites;
    [SerializeField] private List<GameObject> _asteroidPrefabs;
    
    [SerializeField] private UiTextDisplay _uiDisplayText;

    private float _spawnDelay = 0.8f;
    private float _spawnTimer = 0f;
    private int _spawnedCount = 0;
    private bool _isSpawningWave = false;

    private int _currentWave = 1;
    private int _asteroidToSpawn = 4;

    public int _asteroidRemaining = 0;

    public int CurrentWave
    {
        get => _currentWave;
        set => _currentWave = value;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        StartSpawningWave(_asteroidToSpawn);
        _uiDisplayText.UpdateWaveText(_currentWave);
    }

    private void Update()
    {
        if (!_isSpawningWave) return;

        if (_spawnedCount >= _asteroidToSpawn)
        {
            _isSpawningWave = false;
            return;
        }

        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _spawnDelay)
        {
            _spawnTimer = 0f;
            _spawnedCount++;
            _asteroidRemaining++;
            SpawnAsteroid();
        }
    }

    private void StartSpawningWave(int count)
    {
        _spawnedCount = 0;
        _spawnTimer = 0f;
        _isSpawningWave = true;
    }

    private void GoToNextWave()
    {
        CurrentWave++;
        _uiDisplayText.UpdateWaveText(_currentWave);
        _asteroidToSpawn = 3 * CurrentWave;
        StartSpawningWave(_asteroidToSpawn);
    }

    public void CheckIfWaveIsFinished()
    {
        if (_asteroidRemaining == 0)
        {
            GoToNextWave();
            Debug.Log("Next wave");
        }
    }

    private void SpawnAsteroid()
    {
        GameObject asteroidPrefab = GetRandomAsteroidToSpawn();
        Vector3 position = GetRandomSpawnPosition();
        Instantiate(asteroidPrefab, position, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
    }

    private GameObject GetRandomAsteroidToSpawn()
    {
        int index = Random.Range(0, _asteroidPrefabs.Count);
        GameObject asteroidToSpawn = _asteroidPrefabs[index];
        asteroidToSpawn = GetRandomSprite(index, asteroidToSpawn);
        return asteroidToSpawn;
    }

    private GameObject GetRandomSprite(int index, GameObject asteroidToSpawn)
    {
        Sprite asteroidSprite;

        if (index == 0)
        {
            asteroidSprite = _bigAsteroidSprites[Random.Range(0, _bigAsteroidSprites.Count)];
        }
        else if (index == 1)
        {
            asteroidSprite = _mediumAsteroidSprites[Random.Range(0, _mediumAsteroidSprites.Count)];
        }
        else
        {
            asteroidSprite = _smallAsteroidSprites[Random.Range(0, _smallAsteroidSprites.Count)];
        }

        asteroidToSpawn.GetComponent<SpriteRenderer>().sprite = asteroidSprite;
        return asteroidToSpawn;
    }
}
