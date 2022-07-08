using System;
using UnityEngine;

public class LogTest : MonoBehaviour
{
    Rigidbody rigid;
    public int Number;
    int preNum;
    float maxHeight;
    bool hasBounced;

    private void Start()
    {
        /*
        // 정수
        byte b = 15;
        int i = 0x20;
        long l = 0b_1000_0101;
        Debug.Log($"byte: {b}");
        Debug.Log($"int: {i}");
        Debug.Log($"long: {l}");

        // 부동 소수점
        float f = 13.34f;
        Debug.Log($"float: {f}");

        // 불리언
        bool bo = true;
        Debug.Log($"bool: {bo}");

        // 문자
        char c = '☹';
        Debug.Log($"char: {c}");

        //문자열
        string s = "집에가고싶다";
        Debug.Log($"'{s}'의 길이: {s.Length}");

        string s1 = "     Hello     ";
        Debug.Log($"{s1.ToLower()}");
        Debug.Log($"{s1.ToUpper()}");
        Debug.Log($"{s1.Trim()}");

        if(s1 == "H")
        {
            Debug.Log("s1 is H");
        }

        if(s1.StartsWith("H"))
        {
            Debug.Log("s1 is starts with H");
        }

        if(s1.EndsWith("o"))
        {
            Debug.Log("s1 is ends with o");
        }
        */

        /*
        // 배열 실습
        int[] arr = new int[5] { 3, 1, 4, 5, 2 };

        for(int i = 0; i<arr.Length; ++i)
        {
            Debug.Log($"{arr[i]}");
        }

        Array.Sort(arr);

        for (int i = 0; i < arr.Length; ++i)
        {
            Debug.Log($"{arr[i]}");
        }

        // 다차원 배열
        int[,] arr2 = new int[2, 5];

        // 가변 배열
        int[][] arr3 = new int[arr.Length][];

        for(int i = 0; i<arr3.Length; ++i)
        {
            arr3[i] = new int[arr[i]];
        }

        //함수 실습
        int a = 10;
        int b = 20;
        int result;
        Debug.Log($"a = {a}, b = {b}");
        Swap(ref a, ref b);
        Debug.Log($"a = {a}, b = {b}");
        Foo(a, b, out result);
        Debug.Log($"Foo out result = {result}");
        Boo(a, b);
        */

        //이렇게 컴포넌트를 가져올 수 있다.
        rigid = GetComponent<Rigidbody>();
        maxHeight = 0;
        hasBounced = false;
    }

    private void FixedUpdate()
    {
        if(Number != 0)
        {
            rigid.AddForce(0, Number, 0, ForceMode.Impulse);
            preNum = Number;
            Number = 0;
            hasBounced = true;
        }
    }

    private void Update()
    {
        if(rigid.velocity.y > 0)
        {
            maxHeight = rigid.position.y;
        }
        else if(hasBounced)
        {
            hasBounced = false;
            Debug.Log($"{preNum}: {maxHeight}");
        }
    }

    void Swap(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }

    void Foo(int a, int b, out int result)
    {
        result = a + b;
    }

    void Boo(in int a, in int b)
    {
        Debug.Log($"{a}, {b}");
    }
}
