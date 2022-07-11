using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    public float speed = 10f;
    public bool UseSpeed;

    private PlayerInput input;
    private Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
        Debug.Log("PlayerMovement Start");
    }

    // Update is called once per frame
    void Update()
    {
        // 각 방향에 따라 힘주기
        //if(input.Up)
        //{
        //    rigid.AddForce(0f, 0f, speed);
        //}

        //if(input.Down)
        //{
        //    rigid.AddForce(0f, 0f, -speed);
        //}

        //if (input.Right)
        //{
        //    rigid.AddForce(speed, 0f, 0f);
        //}

        //if (input.Left)
        //{
        //    rigid.AddForce(-speed, 0f, 0f);
        //}

        float xspeed = input.X * speed;
        float zspeed = input.Y * speed;

        if (UseSpeed)
        {
            // velocity에 새로운 값을 넣는다.
            // 어느 방향으로 속도를 줄 것인가에 대한 내용
            rigid.velocity=new Vector3(xspeed, 0f, zspeed);
        }
        else
        {
            rigid.AddForce(xspeed, 0f, zspeed);
        }


        //Debug.Log("PlayerMovement Update");
    }
}
