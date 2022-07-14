using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbiting : MonoBehaviour
{
    public Transform Target;

    public float MinDistance = 3f;
    public float MaxDistance = 3.2f;

    public float Speed = 3f;


    private Vector3 distanceVector;
    private float currentDistance;
    private float currentDegree = 0f; 
    // Start is called before the first frame update
    void Start()
    {
        calculateDistance();
    }

    // Update is called once per frame
    void Update()
    {
        currentDegree += Time.deltaTime;

        if (currentDistance < MinDistance || currentDistance > MaxDistance)
        {
            //rigid.AddForce(distanceVector.normalized * Time.deltaTime * Speed);
            transform.Translate(distanceVector.normalized * Speed * Time.deltaTime * (currentDistance - MinDistance < 0 ? -1 : 1));
            Debug.Log(currentDistance);
            calculateDistance();
        }

        orbit();

        //transform.Rotate(0, 10f, 0);
    }

    private void calculateDistance()
    {
        distanceVector = Target.position - transform.position;
        currentDistance = distanceVector.magnitude;
    }

    private void orbit()
    {
        float nextx = Target.position.x + Mathf.Cos(currentDegree) * currentDistance;
        float nextz = Target.position.z + Mathf.Sin(currentDegree) * currentDistance;
        transform.Translate((nextx - transform.position.x) * Time.deltaTime * Speed, (Target.position.y - transform.position.y) * Time.deltaTime * Speed, (nextz - transform.position.z) * Time.deltaTime * Speed);
    }
}
