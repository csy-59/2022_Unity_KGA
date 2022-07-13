using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public void Die()
    {
        // 게임 오브젝트를 삭제 하면 된다
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
