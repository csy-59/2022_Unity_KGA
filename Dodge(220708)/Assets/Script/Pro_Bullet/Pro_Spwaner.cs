using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pro_Spwaner : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform Player;

    public float MinTime;
    public float MaxTime;

    private float SpawnRate;
    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        SpawnRate = Random.Range(MinTime, MaxTime);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= SpawnRate)
        {
            currentTime = 0f;

            GameObject bullet = Instantiate(BulletPrefab, gameObject.transform);
            bullet.transform.LookAt(Player);
            SpawnRate = Random.Range(MinTime, MaxTime);
        }
    }
}
