using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour {
    public AudioClip DeathClip;
    public float JumpForce = 700f;
    public int MaxJumpCount = 2;
    
    private int jumpCount = 0; 
    private bool isGrounded = false; 
    private bool isDead = false; 
    
    private Rigidbody2D playerRigidbody; 
    private Animator animator; 
    private AudioSource playerAudio;
    private Vector2 zero;

    // 정적 상수 맴버
    static private class AnimationId
    {
        static public readonly int IS_ON_GROUND = Animator.StringToHash("IsOnGround");
        static public readonly int DIE = Animator.StringToHash("Die");
    };
    
    private void Awake()
    {
        // 초기화
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        zero = Vector2.zero;
    }

    private static readonly float MIN_NORMAL_Y = Mathf.Sin(45f * Mathf.Deg2Rad);

    private void Update() {
        if(isDead)
        {
            return;
        }

        // 사용자 입력을 감지하고 점프하는 처리
        // 더블 점프 지원
        if(Input.GetMouseButtonDown(0))
        {
            // 최대 점프에 도달했으면 아무것도 안함
            if(jumpCount >= MaxJumpCount)
            {
                return;
            }

            ++jumpCount;
            // 이중 점프 시 일시적으로 속도를 0으로 하고, 다시 힘을 줌 >> 계속 사용하는 정적 변수는 담아서 사용하는 것이 좋음
            playerRigidbody.velocity = zero;
            playerRigidbody.AddForce(new Vector2(0f, JumpForce));
            playerAudio.Play();
        }

        // 마우스 버튼을 땔 때 속도를 절반으로 줄임(마우스 클릭한 시간에 비례하여 점프 크기가 달라짐)
        if(Input.GetMouseButtonUp(0))
        {
            if(playerRigidbody.velocity.y > 0)
            {
                playerRigidbody.velocity *= 0.5f;
            }
        }



        // 애니메이션 업데이트
        animator.SetBool(AnimationId.IS_ON_GROUND, isGrounded);
    }
    
    private void Die() {
        // 사망 처리
        // 1. isDead = true
        isDead = true;
        // 2. 에니메이션 업데이트
        animator.SetTrigger(AnimationId.DIE);
        // 3. 플레이어 캐릭터 멈추기
        playerRigidbody.velocity = zero;
        // 4. 죽을 때 소리도 재생
        playerAudio.PlayOneShot(DeathClip);
        
        GameManager.Instance.OnPlayerDead();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        // 트리거 콜라이더를 가진 장애물과의 충돌을 감지
        if(other.tag == "Dead")
        {
            if (isDead == false)
            {
                Die();
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        // 바닥에 닿았음을 감지하는 처리
        // 처음 닿은 곳
        ContactPoint2D point = collision.GetContact(0);

        // 플랫폼 위로 안착함
        if(point.normal.y >= MIN_NORMAL_Y)
        {
            isGrounded = true;
            jumpCount = 0;
            GameManager.Instance.AddScore();
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        // 바닥에서 벗어났음을 감지하는 처리
        isGrounded = false;
    }
}