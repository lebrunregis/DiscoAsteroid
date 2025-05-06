using Tools;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public GameObjectPool laserPool;
    public float laserSpeed = 10;
    private float laserCooldown = 0.5f;
    public float cooldownDelta = 0;
    public float laserRange = 5;
    public int laserDamage = 1;
    public float laserDamageMultiplier;
    public float timeToLive = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
        GameObject laser = laserPool.GetFistAvailableObject();
        LaserController laserController;
        laser.SetActive(true);
        laser.transform.position = transform.position;
        laser.transform.rotation = transform.rotation;
        if (laser.TryGetComponent<LaserController>(out laserController))
        {

            laserController.laserSpeed = laserSpeed;
            laserController.laserDamage = laserDamage;
            laserController.timeToLive = timeToLive;
            laserController.laserRange = laserRange;
            laserController.shooter = this;
        }
        cooldownDelta = laserCooldown;
    }
}
