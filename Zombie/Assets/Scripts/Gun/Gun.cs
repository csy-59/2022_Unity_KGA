using System.Collections;
using UnityEngine;

// 총을 구현한다
public class Gun : MonoBehaviour {
    // 총의 상태를 표현하는데 사용할 타입을 선언한다
    public enum State {
        Ready, // 발사 준비됨
        Empty, // 탄창이 빔
        Reloading // 재장전 중
    }

    public State CurrentState { get; private set; } // 현재 총의 상태

    public Transform FireTransform; // 총알이 발사될 위치

    public ParticleSystem MuzzleFlashEffect; // 총구 화염 효과
    public ParticleSystem ShellEjectEffect; // 탄피 배출 효과

    private LineRenderer bulletLineRenderer; // 총알 궤적을 그리기 위한 렌더러

    public GunData Data;


    private AudioSource audioSource; // 총 소리 재생기

    private float fireDistance = 50f; // 사정거리
    private float lastFireTime; // 총을 마지막으로 발사한 시점

    private int remainAmmo = 100; // 남은 전체 탄약
    private int currentAmmo; // 현재 탄창에 남아있는 탄약



    private void Awake() 
    {
        // 사용할 컴포넌트들의 참조를 가져오기
        bulletLineRenderer = GetComponent<LineRenderer>();
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;

        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable() 
    {
        // 총 상태 초기화
        remainAmmo = Data.InitialAmmoCount;
        currentAmmo = Data.MagazineCapacity;
        CurrentState = State.Ready;
        lastFireTime = 0f;
    }

    // 발사 시도
    public void Fire() 
    {
        // 발사가 가능 한때
        // 1. 상태가 레디일 때 >> 일단 
        // 2. 쿨타임이 다 찼을 때
        if(CurrentState != State.Ready || Time.time < lastFireTime + Data.FireCooltime)
        {
            return;
        }

        lastFireTime = Time.time;
        Shot();
    }

    // 실제 발사 처리
    private void Shot() 
    {
        RaycastHit hit;
        Vector3 hitPosition;

        if(Physics.Raycast(FireTransform.position, transform.forward, out hit, fireDistance))
        {
            // ray에 맞은 대상의 IDamageable을 가져옴
            IDamageable target = hit.collider.GetComponent<IDamageable>();
            if(target != null)
            {
                target.OnDamage(Data.Damage, hit.point, hit.normal);
            }

            // target?.OnDamage(Data.Damage, hit.point, hit.normal);

            hitPosition = hit.point; // 맞은 지점 저장
        }
        else
        {
            hitPosition = FireTransform.position + transform.forward * fireDistance;
        }

        // 내부적으로 코루틴 등록
        StartCoroutine(ShotEffect(hitPosition));

        --currentAmmo;
        if(currentAmmo <= 0)
        {
            CurrentState = State.Empty;
        }
    }

    // 발사 이펙트와 소리를 재생하고 총알 궤적을 그린다
    private IEnumerator ShotEffect(Vector3 hitPosition) {
        // 발사 이펙트 실행
        MuzzleFlashEffect.Play();
        ShellEjectEffect.Play();

        // 라인 렌더러를 활성화하여 총알 궤적을 그린다
        // 점 세팅
        bulletLineRenderer.SetPosition(0, FireTransform.position);
        bulletLineRenderer.SetPosition(1, hitPosition);
        
        bulletLineRenderer.enabled = true;

        // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);

        // 라인 렌더러를 비활성화하여 총알 궤적을 지운다
        bulletLineRenderer.enabled = false;
    }

    // 재장전 시도
    public bool TryReload() {

        // 재장전 금지
        // 1. 이미 재장전 중
        // 2. 탄알집에 이미 총알이 가득하거나
        // 3. 장전할 총알이 없거나
        if(remainAmmo <= 0 || CurrentState == State.Reloading || currentAmmo == Data.MagazineCapacity)
        {
            return false;
        }

        StartCoroutine(ReloadRoutine());
        return true;
    }

    // 실제 재장전 처리를 진행
    private IEnumerator ReloadRoutine() {
        // 현재 상태를 재장전 중 상태로 전환
        CurrentState = State.Reloading;

        // 재장전 소리 재생
        audioSource.PlayOneShot(Data.ReloadClip);
        
        // 재장전 소요 시간 만큼 처리를 쉬기
        yield return new WaitForSeconds(Data.ReloadTime);

        // 총알을 잘 채워야 함
        int ammoToFill = remainAmmo % Data.MagazineCapacity;
        currentAmmo += ammoToFill;
        remainAmmo -= ammoToFill;

        // 총의 현재 상태를 발사 준비된 상태로 변경
        CurrentState = State.Ready;
    }
}