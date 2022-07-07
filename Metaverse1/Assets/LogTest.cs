using UnityEngine;

public class LogTest : MonoBehaviour
{
    public int Number;
    Transform pos;

    private void Start()
    {
        pos = GetComponent<Transform>();
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


    }

    private void FixedUpdate()
    {
        pos.position = new Vector3(pos.position.x, pos.position.y + 1, pos.position.z);
    }

    private void Update()
    {
        Debug.Log($"{pos.transform.position.y}");
        //switch(Number)
        //{
        //    case > 10:
        //        Debug.Log($"10보다 큼({Number})");
        //        break;
        //    case > 5:
        //        Debug.Log($"5보다 큼({Number})");
        //        break;
        //    default:
        //        Debug.Log($"5와 같거나 작음({Number})");
        //        break;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
