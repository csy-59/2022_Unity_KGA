using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EulerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float x;
    // Update is called once per frame
    void Update()
    {
        // 잘못된 예시 1
        //Quaternion rot = transform.rotation;
        //rot.x += Time.deltaTime * 10;
        //transform.rotation = rot;

        // 잘못된 예시 2
        //Vector3 angles = transform.rotation.eulerAngles;
        //angles.x += Time.deltaTime * 2;
        //transform.rotation = Quaternion.Euler(angles);

        // 올바른 예시
        x += Time.deltaTime * 10;
        transform.rotation = Quaternion.Euler(x, 0, 0);
    }
}
