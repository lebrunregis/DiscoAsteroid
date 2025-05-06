using Tools;
using UnityEngine;

[RequireComponent(typeof(Repeater))]
[RequireComponent(typeof(Rigidbody2D))]
public class DancerController : MonoBehaviour
{
    public Transform laserSpawnPoint;
    public GameObject upFrame;
    public GameObject downFrame;
    private bool state = false;
    public GameObjectPool laserPool;
    public LaserShooter laserShooter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBeat()
    {

        upFrame.SetActive(state);
        downFrame.SetActive(!state);
        state = !state;
        if (state)
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (laserShooter != null)
        {
            laserShooter.Shoot();
        }
    }
}
