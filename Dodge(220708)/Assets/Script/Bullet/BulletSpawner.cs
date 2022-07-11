using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject Bullet;
    public float spawnRateMax = 3f;
    public float spawnRateMin = 0.5f;

    private Transform target;
    private float spawnRate;
    private float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        if(lifeTime >= spawnRate)
        {
            lifeTime = 0f;

            GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation);

            bullet.transform.LookAt(target);

            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }
}
