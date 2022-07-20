using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// T���� ������ Component�� �;� �Ѵ�.
// *where: T(���׸�)�� ���� ���� ����. ���׸��� ������ ����� �� ����
//      >> �ش� ������ MonoBehaviour�� ��ӹ޾� �Ѵٴ� ��
public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                // ���� �����ϰ� �ش� ������Ʈ�� �������� �ʾҴٸ� ����� �ش�.
                if (instance == null)
                {
                    GameObject go = new GameObject("@GameManager");
                    instance = go.AddComponent<T>();
                }

                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    protected void Awake()
    {
        if (instance != null)
        {
            // ���� �ƴ� ��� ����
            if (instance.gameObject != gameObject)
                Destroy(gameObject);

            return;
        }

        instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject);
    }
}