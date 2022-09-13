using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private float rotationSpeed = 120f;

    private PlayerInput input;
    private Rigidbody rigid;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveForward(input.Y);
        rotateClockwise(input.X);
    }

    /// <summary>
    /// �¸��� �� �ڷ� ������
    /// </summary>
    /// <param name="x">����� forward �ƴϸ� backward</param>
    private void MoveForward(float direction)
    {
        Vector3 deltaPosition = moveSpeed * direction * Time.fixedDeltaTime * transform.forward;
        Vector3 newPosition = rigid.position + deltaPosition;
        rigid.MovePosition(newPosition);
    }

    /// <summary>
    /// ĳ���͸� �ð�������� ȸ����Ų��
    /// </summary>
    /// <param name="y">����� �ð����, ������ �ݽð������ �ǹ�</param>
    private void rotateClockwise(float direction)
    {
        Quaternion deltaRotation = Quaternion.Euler(0f, rotationSpeed * direction * Time.fixedDeltaTime, 0f);
        Quaternion newRotation = rigid.rotation * deltaRotation;
        rigid.MoveRotation(newRotation);
    }
}
