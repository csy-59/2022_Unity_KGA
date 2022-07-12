using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // 본인 기준 앞은 z 축. 즉, 월드 좌표가 아닌 자신 좌표 기준 앞으로 나아간다.
        transform.Translate(0f, 0f, Speed); //한 프레임 당 1유닛씩 움직임
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if(playerHealth != null)
        {
            playerHealth.Die();
        }

        // ?. 연산자
        // (expression)?.~ << expression이 null이 아니면 맴버에 접근함.
        // playerHealth?.Die(); //위와 같음
        // other.GetComponent<PlayerHealth>()?.Die(); //위와 같음

        //if(other.tag == "Player")
        //{
        //    other.GetComponent<PlayerHealth>().Die();
        //}
    }
    */
}
