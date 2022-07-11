using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRotation : MonoBehaviour
{
    public float rotationSpeed = 60f;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
