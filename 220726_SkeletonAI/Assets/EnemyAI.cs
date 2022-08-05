using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,       // 대기
    Walk,       // 순찰 patrol
    Run,        // 추적 trace
    Attack,     // 공격
    KnockBack   // 피격 damaged
}

public class EnemyAI : MonoBehaviour
{
    public EnemyState state;
    public float MoveSpeed;
    public float RunSpeed;
    public float RotateSpeed;
    public float AttackCooltime;

    private Animator anim;
    private Vector3 targetPos;

    //적 찾기 관련
    public GameObject target; // 타겟 오브젝트
    private bool isFindEnemy;
    private Camera eye;
    Plane[] eyePlanes;

    //공격
    private GameObject attackCollider;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        // 시아(카메라)를 찾아, 해당 카메라를 이루는 영역(내모난 영역)을 받아 충돌 처리를 통해 타겟을 찾는다.
        eye = transform.GetComponentInChildren<Camera>();

        ChangeState(EnemyState.Idle);

        attackCollider = transform.Find("WeaponCollider").gameObject;
        attackCollider.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case EnemyState.Idle: UpdateIdle(); break;
            case EnemyState.Walk: UpdateWalk(); break;
            case EnemyState.Run: UpdateRun(); break;
            case EnemyState.Attack: UpdateAttack(); break;
            case EnemyState.KnockBack: UpdateKnockBack(); break;
        }
    }
    // 매 프레임마다 수행해야 하는 동작 (상태가 바뀔 때 마다)
    void UpdateIdle()
    {
    }
    void UpdateWalk()
    {
        if(isFoundTarget())
        {
            ChangeState(EnemyState.Run);
            return;
        }    

        Vector3 dir = targetPos - transform.position;
        if (dir.sqrMagnitude <= 0.2f)
        {
            ChangeState(EnemyState.Idle);
            return;
        }

        //목표 지점을 바라봄(거기까지의 각도)
        var targetRotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
        // Lerp = 선형 보간. a가 c가 선상에 있다고 가정하여 이동값을 구함 (이동 관련)
        // Slerp = 원형에 a와 c가 있다고 가정

        transform.position += MoveSpeed * Time.deltaTime * transform.forward;
    }
    void UpdateRun()
    {
        Vector3 dir = targetPos - transform.position;
        if (dir.sqrMagnitude <= 1.5f)
        {
            ChangeState(EnemyState.Attack);
            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
            return;
        }

        //목표 지점을 바라봄(거기까지의 각도)
        var targetRotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
        // Lerp = 선형 보간. a가 c가 선상에 있다고 가정하여 이동값을 구함 (이동 관련)
        // Slerp = 원형에 a와 c가 있다고 가정

        transform.position += RunSpeed * Time.deltaTime * transform.forward;
    }
    void UpdateAttack()
    {

    }
    void UpdateKnockBack()
    {

    }


    IEnumerator CoroutineIdle()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        Debug.Log("대기 상태 시작");
        anim.SetBool("isIdle", true);

        while (true)
        {
            yield return new WaitForSeconds(2f);
            // 시간마다 수행해야 하는 동작 (상태가 바뀔 때 마다)
            ChangeState(EnemyState.Walk);
            yield break;            
        }
    }
    IEnumerator CoroutineWalk()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        Debug.Log("순찰 상태 시작");
        anim.SetBool("isWalking", true);

        // 목적지 설정
        targetPos = transform.position + new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f));

        while (true)
        {
            yield return new WaitForSeconds(AttackCooltime);
            // 시간마다 수행해야 하는 동작 (상태가 바뀔 때 마다)
        }
    }
    IEnumerator CoroutineRun()
    {
        Debug.Log("추적 상태 시작");
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        targetPos = target.transform.position;
        anim.SetBool("isRuning", true);

        while (true)
        {
            yield return new WaitForSeconds(5f);
            // 시간마다 수행해야 하는 동작 (상태가 바뀔 때 마다)

        }
    }
    IEnumerator CoroutineAttack()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)
        anim.SetTrigger("Attack");
        //ChangeState(EnemyState.Idle);
        yield return new WaitForSeconds(1.5f);
        ChangeState(EnemyState.Idle);
        yield break;


        //while (true)
        //{
        //    // 시간마다 수행해야 하는 동작 (상태가 바뀔 때 마다)
        //}
    }
    IEnumerator CoroutineKnockBack()
    {
        // 한번만 수행해야 하는 동작 (상태가 바뀔 때 마다)

        while (true)
        {
            yield return new WaitForSeconds(2f);
            // 시간마다 수행해야 하는 동작 (상태가 바뀔 때 마다)

        }
    }

    void ChangeState(EnemyState nextState)
    {
        StopAllCoroutines();

        state = nextState;
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isRuning", false);

        switch (state)
        {
            case EnemyState.Idle: StartCoroutine(CoroutineIdle()); break;
            case EnemyState.Walk: StartCoroutine(CoroutineWalk()); break;
            case EnemyState.Run: StartCoroutine(CoroutineRun()); break;
            case EnemyState.Attack: StartCoroutine(CoroutineAttack()); break;
            case EnemyState.KnockBack: StartCoroutine(CoroutineKnockBack()); break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "target")
        {
            anim.SetBool("isRuning", true);
            target = other.gameObject;
            ChangeState(EnemyState.Run);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "target")
        {
            anim.SetBool("isRuning", false);
            Debug.Log("dkdk");
            ChangeState(EnemyState.Attack);
        }
    }

    private bool isFoundTarget()
    {
        eyePlanes = GeometryUtility.CalculateFrustumPlanes(eye);
        Bounds targetBounds = target.GetComponentInChildren<SkinnedMeshRenderer>().bounds;
        // 해당 시아(카메라 시아) 내부에 target이 있으면 true를 반환(충돌처리 같은 것) 
        isFindEnemy = GeometryUtility.TestPlanesAABB(eyePlanes, targetBounds);

        return isFindEnemy;
    }

    public void OnAttack(AnimationEvent animationEvent)
    {
        Debug.Log(animationEvent.intParameter);
        if(animationEvent.intParameter == 1)
        {
            attackCollider.SetActive(true);
        }
        else
        {
            attackCollider.SetActive(false);
        }
    }
}
