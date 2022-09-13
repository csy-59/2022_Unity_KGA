using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput input;

    private Collider[] colliders = new Collider[3];
    private int colliderCount;
    private int enemyLayerMask;

    [SerializeField] private int damage = 10;
    private void Awake()
    {
        input = GetComponent<PlayerInput>();

        LayerMask layerMask = LayerMask.NameToLayer("Enemy");
        enemyLayerMask = (1 << layerMask);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(input.Attack)
        {
            attack();
        }
    }

    private void attack()
    {
        Vector3 attackPosition = transform.position + transform.forward;
        colliderCount = Physics.OverlapSphereNonAlloc(attackPosition, 1f, colliders, enemyLayerMask);

        for(int i = 0; i<colliderCount; ++i)
        {
            // 데미지 부여
            EnemyHealth enemy = colliders[i].GetComponent<EnemyHealth>();
            Debug.Assert(enemy != null);
            enemy.TakeDamage(damage);
        }

        Debug.Log("Attack");
    }
}
