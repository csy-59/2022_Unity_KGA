using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotProductPrac : MonoBehaviour
{
    public Transform Target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceVector = Target.position - transform.position;
        Debug.Log(Vector3.Dot(transform.forward, distanceVector.normalized));

        Debug.DrawRay(transform.position, transform.forward * 5f, new Color(0, 0, 1));
        Debug.DrawLine(transform.position, distanceVector, new Color(1, 0, 0));

        Vector3 normalVector = Vector3.Cross(transform.forward, distanceVector.normalized);
        Debug.DrawRay(transform.position, normalVector, Color.green);
    }
}
