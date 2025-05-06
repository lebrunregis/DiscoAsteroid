using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float laserSpeed = 10;
    public float laserRange = 5;
    public int laserDamage = 1;
    public float timeToLive = 10;
    public LaserShooter shooter;
    public Transform homingTarget;

    void Update()
    {
        if (homingTarget != null)
        {
            transform.LookAt(homingTarget.transform, transform.up);
        }
        transform.position += (transform.up * (laserSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        AsteroidController asteroidScript = other.gameObject.GetComponent<AsteroidController>();

        if (asteroidScript == null) return;

        asteroidScript.TakeDamage(laserDamage);
        gameObject.SetActive(false);
    }
}
