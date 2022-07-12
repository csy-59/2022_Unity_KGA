using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���� ���� z ��. ��, ���� ��ǥ�� �ƴ� �ڽ� ��ǥ ���� ������ ���ư���.
        transform.Translate(0f, 0f, Speed); //�� ������ �� 1���־� ������
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if(playerHealth != null)
        {
            playerHealth.Die();
        }

        // ?. ������
        // (expression)?.~ << expression�� null�� �ƴϸ� �ɹ��� ������.
        // playerHealth?.Die(); //���� ����
        // other.GetComponent<PlayerHealth>()?.Die(); //���� ����

        //if(other.tag == "Player")
        //{
        //    other.GetComponent<PlayerHealth>().Die();
        //}
    }
    */
}
