using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// T에는 무조건 Component가 와야 한다.
// *where: T(제네릭)에 대한 제한 조건. 제네릭이 없으면 사용할 수 없음
//      >> 해당 조건은 MonoBehaviour를 상속받야 한다는 뜻
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

                // 만약 깜빡하고 해당 오브젝트를 생성하지 않았다면 만들어 준다.
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
            // 내가 아닌 경우 삭제
            if (instance.gameObject != gameObject)
                Destroy(gameObject);

            return;
        }

        instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject);
    }
}