using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    private int layer;

    private void Awake()
    {
        enemyMovement = GetComponentInParent<EnemyMovement>();
        layer = LayerMask.NameToLayer("Enemy");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == layer)
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            Debug.Assert(enemyHealth != null);

            enemyHealth.OnTakenDamage -= enemyMovement.MoveTo;
            enemyHealth.OnTakenDamage += enemyMovement.MoveTo;

            Debug.Log($"{other.name} �� {transform.parent.name}�� MoveTo�� �߰���");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == layer)
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            Debug.Assert(enemyHealth != null);

            enemyHealth.OnTakenDamage -= enemyMovement.MoveTo;
            Debug.Log($"{other.name} �� {transform.parent.name}�� MoveTo�� ���ŵ�");
        }
    }
}
