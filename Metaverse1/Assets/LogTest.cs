using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // 일반 로그
        Debug.Log("이것은 일반 로그입니다.");

        // 경고 로그
        Debug.LogWarning("이것은 경고 로그입니다.");

        // 에러 로그
        Debug.LogError("이것은 에러 로그입니다.");
    }
}
