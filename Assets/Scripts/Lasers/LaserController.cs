using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float laserSpeed;
    public float laserFireRate;
    public float laserRange;
    public int laserDamage;
    public float laserDamageMultiplier;
    public float timeToLive;

    void Update()
    {
        transform.position += (transform.up * (laserSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        AsteroidBase asteroidScript = other.gameObject.GetComponent<AsteroidBase>();

        if (asteroidScript == null) return;

        asteroidScript.TakeDamage(laserDamage);
        gameObject.SetActive(false);
    }
}
