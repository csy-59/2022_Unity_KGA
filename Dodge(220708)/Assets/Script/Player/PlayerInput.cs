using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //public bool Up { get; private set; }
    //public bool Down { get; private set; }
    //public bool Right { get; private set; }
    //public bool Left { get; private set; }

    public float X { get; private set; }
    public float Y { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PlayerInput Start");
    }

    // Update is called once per frame
    void Update()
    {
        //// 기본 값으로 초기화
        //Up = Down = Right = Left = false;
        X = Y = 0f;

        //// 현재 프래임의 입력 값을 저장
        //// (특정 상황일 경우 입력을 무시해야 함으로 분리함)
        //Up = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        //Down = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        //Right = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        //Left = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        X = Input.GetAxis("Horizontal");
        Y = Input.GetAxis("Vertical");

        //Debug.Log("PlayerInput Update");
    }
}
