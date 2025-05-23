using Tools;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public GameObjectPool laserPool;
    public Transform target;
    public float laserSpeed = 10;
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
            GameObject laser = laserPool.GetFirstAvailableObject();
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
                laserController.homingTarget = null;
            }
        }
    

    public void ShootAt()
    {
            GameObject laser = laserPool.GetFirstAvailableObject();
            LaserController laserController;
            laser.SetActive(true);
            laser.transform.position = transform.position;
            RotateTowards(laser, new Vector2(target.position.x, target.position.y));
            if (laser.TryGetComponent<LaserController>(out laserController))
            {

                laserController.laserSpeed = laserSpeed;
                laserController.laserDamage = laserDamage;
                laserController.timeToLive = timeToLive;
                laserController.laserRange = laserRange;
                laserController.shooter = this;
                laserController.homingTarget = null;
            }
    }

    public void ShootAt(Transform target)
    {

            GameObject laser = laserPool.GetFirstAvailableObject();
            LaserController laserController;
            laser.SetActive(true);
            laser.transform.position = transform.position;
            RotateTowards(laser, new Vector2(target.position.x, target.position.y));
            if (laser.TryGetComponent<LaserController>(out laserController))
            {

                laserController.laserSpeed = laserSpeed;
                laserController.laserDamage = laserDamage;
                laserController.timeToLive = timeToLive;
                laserController.laserRange = laserRange;
                laserController.shooter = this;
                laserController.homingTarget = null;
            }
    }

    public void HomingShootAt()
    {
            GameObject laser = laserPool.GetFirstAvailableObject();
            LaserController laserController;
            laser.SetActive(true);
            laser.transform.position = transform.position;
            RotateTowards(laser, new Vector2(target.position.x, target.position.y));
            if (laser.TryGetComponent<LaserController>(out laserController))
            {

                laserController.laserSpeed = laserSpeed;
                laserController.laserDamage = laserDamage;
                laserController.timeToLive = timeToLive;
                laserController.laserRange = laserRange;
                laserController.shooter = this;
                laserController.homingTarget = target;
            }
    }

    public void HomingShootAt(Transform target)
    {
            GameObject laser = laserPool.GetFirstAvailableObject();
            LaserController laserController;
            laser.SetActive(true);
            laser.transform.position = transform.position;
            RotateTowards(laser, new Vector2(target.position.x, target.position.y));
            if (laser.TryGetComponent<LaserController>(out laserController))
            {

                laserController.laserSpeed = laserSpeed;
                laserController.laserDamage = laserDamage;
                laserController.timeToLive = timeToLive;
                laserController.laserRange = laserRange;
                laserController.shooter = this;
                laserController.homingTarget = target;
            }
    }

    private static void RotateTowards(GameObject go, Vector2 target)
    {
        Vector2 direction = (target - (Vector2)go.transform.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var offset = -90f;
        go.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
