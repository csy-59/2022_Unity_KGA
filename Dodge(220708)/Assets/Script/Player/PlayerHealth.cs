using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public void Die()
    {
        // ���� ������Ʈ�� ���� �ϸ� �ȴ�
        gameObject.SetActive(false);

        FindObjectOfType<GameManager1>().GameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            Die();
        }
    }
}
