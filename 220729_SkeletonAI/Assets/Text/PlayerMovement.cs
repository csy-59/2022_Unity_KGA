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
    /// 태릭터 앞 뒤로 움직임
    /// </summary>
    /// <param name="x">양수면 forward 아니면 backward</param>
    private void MoveForward(float direction)
    {
        Vector3 deltaPosition = moveSpeed * direction * Time.fixedDeltaTime * transform.forward;
        Vector3 newPosition = rigid.position + deltaPosition;
        rigid.MovePosition(newPosition);
    }

    /// <summary>
    /// 캐릭터를 시계방향으로 회전시킨다
    /// </summary>
    /// <param name="y">양수면 시계방향, 음수면 반시계방향을 의미</param>
    private void rotateClockwise(float direction)
    {
        Quaternion deltaRotation = Quaternion.Euler(0f, rotationSpeed * direction * Time.fixedDeltaTime, 0f);
        Quaternion newRotation = rigid.rotation * deltaRotation;
        rigid.MoveRotation(newRotation);
    }
}
