using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int _health;
    [SerializeField] private int _maxHealth = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        _health = _maxHealth;
    }


    public int Health
    {
        get => _health;
        set => _health = value;
    }

    public virtual void TakeDamage(int damageValue)
    {
        _health -= damageValue;
        CheckIfEnemyIsAlive();
    }

    private void CheckIfEnemyIsAlive()
    {
        if (_health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

}
