using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour {
    public float MoveSpeed = 5f; // 앞뒤 움직임의 속도
    public float RotateSpeed = 180f; // 좌우 회전 속도

    private PlayerInput input; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody rigid; // 플레이어 캐릭터의 리지드바디
    private Animator animator; // 플레이어 캐릭터의 애니메이터

    private static class AnimationID
    {
        public static readonly int MOVE = Animator.StringToHash("Move");
        public static readonly int RELOAD = Animator.StringToHash("Reload");
    }

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // FixedUpdate는 물리 갱신 주기(50fps. 설정에서 변경가능)에 맞춰 실행됨
    // 항상 일관된 물리 계산을 준다.
    private void FixedUpdate() {
        // 물리 갱신 주기마다 움직임, 회전, 애니메이션 처리 실행
        // Time.fixedDeltaTime; << FixedUpdate에서만 사용. 이걸 써야함
        move();
        rotate();

        animator.SetFloat(AnimationID.MOVE, input.MoveDirection);
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void move() {
        Vector3 moveDirection = transform.forward * MoveSpeed * input.MoveDirection * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + moveDirection);
    }

    // 입력값에 따라 캐릭터를 좌우로 회전
    private void rotate() {
    }
}
