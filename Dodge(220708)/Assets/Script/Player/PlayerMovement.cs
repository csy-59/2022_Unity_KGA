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
        // �� ���⿡ ���� ���ֱ�
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
            // velocity�� ���ο� ���� �ִ´�.
            // ��� �������� �ӵ��� �� ���ΰ��� ���� ����
            rigid.velocity=new Vector3(xspeed, 0f, zspeed);
        }
        else
        {
            rigid.AddForce(xspeed, 0f, zspeed);
        }


        //Debug.Log("PlayerMovement Update");
    }
}
