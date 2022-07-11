using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public void Die()
    {
        // 게임 오브젝트를 삭제 하면 된다
        gameObject.SetActive(false);

        GameManager gm = FindObjectOfType<GameManager>();
        gm.EndGame();
    }
}
