using UnityEngine;
using UnityEngine.UI; // UI 관련 코드

// 플레이어 캐릭터의 생명체로서의 동작을 담당
public class PlayerHealth : LivingEntity {
    public Slider HealthSlider; // 체력을 표시할 UI 슬라이더

    public AudioClip DeathClip; // 사망 소리
    public AudioClip HitClip; // 피격 소리
    public AudioClip ItemPickupClip; // 아이템 습득 소리

    private AudioSource audioSource; // 플레이어 소리 재생기
    private Animator animator; // 플레이어의 애니메이터
    private PlayerMovement movement; // 플레이어 움직임 컴포넌트
    private PlayerShooter shooter; // 플레이어 슈터 컴포넌트

    private void Awake() {
        // 사용할 컴포넌트를 가져오기
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        movement = GetComponent<PlayerMovement>();
        shooter = GetComponent<PlayerShooter>();
    }

    protected override void OnEnable() {
        // LivingEntity의 OnEnable() 실행 (상태 초기화)
        base.OnEnable();

        // 체력 슬라이드 다시 리셋
        HealthSlider.gameObject.SetActive(true);
        HealthSlider.maxValue = InitialHealth;
        HealthSlider.value = CurrentHealth;

        // 컴포넌트도 다시 활성화
        movement.enabled = true;
        shooter.enabled = true;
    }

    // 체력 회복
    public override void RestoreHealth(float newHealth) {
        // LivingEntity의 RestoreHealth() 실행 (체력 증가)
        base.RestoreHealth(newHealth);

        // HelathSlider의 값 맞춰주기
        HealthSlider.value = CurrentHealth;
    }

    // 데미지 처리
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection) {
        // LivingEntity의 OnDamage() 실행(데미지 적용)
        base.OnDamage(damage, hitPoint, hitDirection);

        // 피격 오디오 재생
        if(isDead == false)
        {
            audioSource.PlayOneShot(HitClip);
        }
        HealthSlider.value = CurrentHealth;
    }

    // 사망 처리
    public override void Die() {
        // LivingEntity의 Die() 실행(사망 적용)
        base.Die();

        // 사망 소리 재생
        audioSource.PlayOneShot(DeathClip);

        // 체력 슬라이더 다시 초기화
        HealthSlider.gameObject.SetActive(false);

        movement.enabled = false;
        shooter.enabled = false;

        // 애니메이션 사용
        animator.SetTrigger(PlayerAnimID.Die);
    }

    private void OnTriggerEnter(Collider other) {
        // 아이템과 충돌한 경우 해당 아이템을 사용하는 처리
        if(isDead)
        {
            return;
        }

        IItem item = other.GetComponent<IItem>();
        item?.Use(gameObject);
        audioSource.PlayOneShot(ItemPickupClip);
    }
}