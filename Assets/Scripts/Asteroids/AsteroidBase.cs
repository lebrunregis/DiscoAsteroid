using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class AsteroidBase : MonoBehaviour
{
    //Renommer la methode OnMouseDown par DeathHandler quand tout est finis

    [SerializeField] private GameObject _asteroidPrefab;
    [SerializeField] private List<Sprite> _asteroidSprites;

    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    protected bool _usedForCount = true;

    public int Damage
    {
        get => _damage;
        set => _damage = value;
    }

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    public int Health
    {
        get => _health;
        set => _health = value;
    }

    public virtual void TakeDamage(int damageValue)
    {
        _health -= damageValue;
        CheckIfAsteroidIsAlive();
    }

    private void CheckIfAsteroidIsAlive()
    {
        if (_health <= 0)
        {
            OnMouseDown();
        }
    }

    public void SpawnAsteroid()
    {
        GameObject asteroidPrefab = GetRandomAsteroidToSpawn();
        GameObject instantiatedAsteroid = Instantiate(asteroidPrefab, transform.position, Quaternion.identity);

        instantiatedAsteroid.GetComponent<AsteroidBase>()._usedForCount = false;
    }

    public virtual void OnMouseDown()
    {
        Destroy(gameObject);
        if (!_usedForCount) return;

        AsteroidGenerator.Instance._asteroidRemaining--;
        AsteroidGenerator.Instance.CheckIfWaveIsFinished();
    }

    private GameObject GetRandomAsteroidToSpawn()
    {
        GameObject asteroidToSpawn = _asteroidPrefab;
        asteroidToSpawn = GetRandomSprite(asteroidToSpawn);
        return asteroidToSpawn;
    }

    private GameObject GetRandomSprite(GameObject asteroidToSpawn)
    {
        Sprite asteroidSprite;

        asteroidSprite = _asteroidSprites[Random.Range(0, _asteroidSprites.Count)];

        asteroidToSpawn.GetComponent<SpriteRenderer>().sprite = asteroidSprite;
        return asteroidToSpawn;
    }
}