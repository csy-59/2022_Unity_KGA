using UnityEngine;

public class LogTest : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("����� Awake");
    }
    private void OnEnable()
    {
        Debug.Log("OnEable()");
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start()");
        // ���� �α�
        //Debug.LogError("�½�Ʈ�� ���� ���� ����");

        Destroy(gameObject, 3f); 
        // gameObject == �ش� ������Ʈ�� ���� �ִ� ���� ������Ʈ�� �ǹ�(Cube)
        // 3f => 3�� �ڿ� �ڱ��ڽ��� �ı�
    }
    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate()");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update()");
    }

    private void LateUpdate()
    {
        Debug.Log("LateUpdate()");
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable()");
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy()");
    }
}
