using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour {
    [SerializeField] //에디터 상에서의 편집기능은 사용
    private float MoveSpeed = 5f; // 앞뒤 움직임의 속도
    public float RotateSpeed = 180f; // 좌우 회전 속도

    private PlayerInput input; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody rigid; // 플레이어 캐릭터의 리지드바디
    private Animator animator; // 플레이어 캐릭터의 애니메이터


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

        animator.SetFloat(PlayerAnimID.Move, input.MoveDirection);
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void move() 
    {
        // Vector3가 앞으로 오면 백터 연산 호출이 자꾸 되어서 Vector3는 뒤로 가는 것이 좋다
        Vector3 offset = MoveSpeed * input.MoveDirection * Time.fixedDeltaTime * transform.forward;

        rigid.MovePosition(rigid.position + offset);
    }

    // 입력값에 따라 캐릭터를 좌우로 회전
    private void rotate() 
    {
        Quaternion offset = Quaternion.Euler(0f, RotateSpeed * input.RotateDirection * Time.fixedDeltaTime, 0f);

        rigid.MoveRotation(rigid.rotation * offset);
    }
}
