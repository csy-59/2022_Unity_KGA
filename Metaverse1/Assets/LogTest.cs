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
        // �Ϲ� �α�
        Debug.Log("�̰��� �Ϲ� �α��Դϴ�.");

        // ��� �α�
        Debug.LogWarning("�̰��� ��� �α��Դϴ�.");

        // ���� �α�
        Debug.LogError("�̰��� ���� �α��Դϴ�.");
    }
}
