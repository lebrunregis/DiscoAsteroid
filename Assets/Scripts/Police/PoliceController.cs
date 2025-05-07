using System.Collections.Generic;
using UnityEngine;

public class PoliceController : MonoBehaviour
{
    public List<LaserShooter> laserShooters;
    public bool shootsLasers = false;

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
        Attack();
    }

    public void Attack()
    {
        if (shootsLasers)
        {
            foreach (LaserShooter s in laserShooters)
            {
                s.ShootAt();
            }
        }
    }
}
