using UnityEngine;

public class LaserPool : MonoBehaviour
{
    public GameObject laserPrefab;
    public int poolSize = 5;
    public GameObject[] lasers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lasers = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            GameObject laser = Instantiate(laserPrefab);
                laser.SetActive(false);
                lasers[i] = laser;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
