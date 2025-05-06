using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidMovement : MonoBehaviour
{
    private AsteroidBase _asteroidBase;
    private Rigidbody2D _rigidbody2D;

    private bool _movementEnabled = true;

    private Vector2 _direction;

    private void Awake()
    {
        _asteroidBase = GetComponent<AsteroidBase>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartMovement();
        SetDirection();
        SetMovement();
    }

    private Vector3 _targetPoint;

    private void SetDirection()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Vector3 worldCenter = Camera.main.ScreenToWorldPoint(screenCenter);

        Vector2 randomOffset = Random.insideUnitCircle * 6f;
        worldCenter += new Vector3(randomOffset.x, randomOffset.y, 0f);

        _direction = (worldCenter - transform.position).normalized;
    }

    private void SetMovement()
    {
        if (_movementEnabled)
        {
            _rigidbody2D.AddForce(10 * (_asteroidBase.Speed * _rigidbody2D.mass) * _direction, ForceMode2D.Force);
        }
    }

    private void StartMovement()
    {
        _movementEnabled = true;
    }

    private void StopMovement()
    {
        _movementEnabled = false;
    }
}