using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int Hp = 100;

    public event Action<Transform> OnTakenDamage;

    public bool IsDead { get; private set; }

    public void TakeDamage(int damage)
    {
        Hp -=  damage;

        OnTakenDamage?.Invoke(transform); // 주변 객체한테 알림
        Debug.Log("hell");

        if(Hp <=0)
        {
            Hp = 0;
            IsDead = true;
            gameObject.SetActive(false);
        }

    }
}
