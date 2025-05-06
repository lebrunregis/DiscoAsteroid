using Tools;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public GameObjectPool laserPool;
    public float laserSpeed = 10;
    public float laserCooldown = 0.5f;
    private float cooldownDelta = 0;
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
        if(cooldownDelta > 0)
        {
            cooldownDelta -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        if (cooldownDelta <= 0)
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

    public void ShootAt(Transform target)
    {
        if (cooldownDelta < 0)
        {
            GameObject laser = laserPool.GetFistAvailableObject();
            LaserController laserController;
            laser.SetActive(true);
            laser.transform.position = transform.position;
            laser.transform.LookAt(target, transform.up);
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

    public void HomingShootAt(Transform target)
    {
        if (cooldownDelta < 0)
        {
            GameObject laser = laserPool.GetFistAvailableObject();
            LaserController laserController;
            laser.SetActive(true);
            laser.transform.position = transform.position;
            laser.transform.LookAt(target, transform.up);
            if (laser.TryGetComponent<LaserController>(out laserController))
            {

                laserController.laserSpeed = laserSpeed;
                laserController.laserDamage = laserDamage;
                laserController.timeToLive = timeToLive;
                laserController.laserRange = laserRange;
                laserController.shooter = this;
                laserController.homingTarget = target;
            }
            cooldownDelta = laserCooldown;
        }
    }
}
