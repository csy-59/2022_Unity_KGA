using System.Collections;
using UnityEngine;
using UnityEngine.AI; // AI, 내비게이션 시스템 관련 코드를 가져오기

// 적 AI를 구현한다
public class Enemy : LivingEntity 
{
    public LayerMask TargetLayer; // 추적 대상 레이어
    public ParticleSystem HitEffect; // 피격시 재생할 파티클 효과
    public AudioClip DeathSound; // 사망시 재생할 소리
    public AudioClip HitSound; // 피격시 재생할 소리
    public float Damage = 20f; // 공격력
    public float AttackCooltime = 0.5f; // 공격 간격

    private LivingEntity target; // 추적할 대상

    private NavMeshAgent nevMeshAgent; // 경로계산 AI 에이전트
    private Animator animator; // 애니메이터 컴포넌트
    private AudioSource audioSource; // 오디오 소스 컴포넌트
    private Renderer render; // 렌더러 컴포넌트

    private float lastAttackTime; // 마지막 공격 시점
    private Collider[] targetCandidates = new Collider[5]; // 타겟 후보군
    private int targetCandidateCount; // 타겟 후보군 개수

    private void Awake()
    {
        // 초기화
        nevMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        render = GetComponentInChildren<Renderer>();
    }

    // 추적할 대상이 존재하는지 알려주는 프로퍼티
    private bool hasTarget
    {
        get
        {
            // 추적할 대상이 존재하고, 대상이 사망하지 않았다면 true
            if (target != null && !target.isDead)
            {
                return true;
            }

            // 그렇지 않다면 false
            return false;
        }
    }


    // 적 AI의 초기 스펙을 결정하는 셋업 메서드
    public void Setup(float newHealth, float newDamage, float newSpeed, Color skinColor) {
        InitialHealth = newHealth;
        Damage = newDamage;
        nevMeshAgent.speed = newSpeed;
        render.material.color = skinColor;
    }

    private void Start() {
        // 게임 오브젝트 활성화와 동시에 AI의 추적 루틴 시작
        StartCoroutine(UpdatePath());
    }

    private void Update() {
        // 추적 대상의 존재 여부에 따라 다른 애니메이션을 재생
        animator.SetBool(ZombieAnimID.HasTarget, hasTarget);
    }

    // 주기적으로 추적할 대상의 위치를 찾아 경로를 갱신
    private IEnumerator UpdatePath() {
        // 살아있는 동안 무한 루프
        while (!isDead)
        {
            // 대상을 찾았을 때
            if(hasTarget)
            {
                nevMeshAgent.isStopped = false;
                nevMeshAgent.SetDestination(target.transform.position);
            }
            // 대상을 찾지 못했을 때
            else
            {
                nevMeshAgent.isStopped = true;

                targetCandidateCount = Physics.OverlapSphereNonAlloc(transform.position, 8f, targetCandidates, TargetLayer);

                for(int i = 0; i<targetCandidateCount; ++i)
                {
                    Collider other = targetCandidates[i];
                    LivingEntity livingEntity = other.GetComponent<LivingEntity>();

                    Debug.Assert(livingEntity != null);
                    if (livingEntity.isDead == false)
                    {
                        target = livingEntity;
                        break;
                    }
                }
            }

            // 0.25초 주기로 처리 반복
            yield return new WaitForSeconds(0.25f);
        }
    }

    // 데미지를 입었을때 실행할 처리
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal) {
        // LivingEntity의 OnDamage()를 실행하여 데미지 적용
        base.OnDamage(damage, hitPoint, hitNormal);

        if(isDead == false)
        {
            audioSource.PlayOneShot(HitSound);

            HitEffect.transform.position = hitPoint;
            HitEffect.transform.rotation = Quaternion.LookRotation(hitNormal); // 맞은 부위 쪽으로 피가 나오도록 설정
            HitEffect.Play();
        }
    }

    // 사망 처리
    public override void Die() {
        // LivingEntity의 Die()를 실행하여 기본 사망 처리 실행
        base.Die();

        // 1. 죽을 때 사운드 재생
        audioSource.PlayOneShot(DeathSound);

        // 2. 애니메이션 트리거 설정
        animator.SetTrigger(ZombieAnimID.Die);

        // 3. 내비에서 비활성화
        nevMeshAgent.isStopped = true;
        nevMeshAgent.enabled = false;
    }

    private void OnTriggerStay(Collider other) {

        // 트리거 충돌한 상대방 게임 오브젝트가 추적 대상이라면 공격 실행   
        
        // 공격이 가능한지 확인
        // 1. 내가 살아있는지
        // 2. 공격 쿨타임이 지났는지
        if(isDead == false && Time.time >= lastAttackTime + AttackCooltime)
        {
            LivingEntity livingEntity = other.GetComponent<LivingEntity>();
            if (livingEntity == target)
            {
                livingEntity.OnDamage(Damage, other.ClosestPoint(transform.position), transform.position - other.transform.position);
                lastAttackTime = Time.time;
            }
        }
    }
}