using UnityEngine;

public class LogTest : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("여기는 Awake");
    }
    private void OnEnable()
    {
        Debug.Log("OnEable()");
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start()");
        // 에러 로그
        //Debug.LogError("태스트를 위한 에러 보고");

        Destroy(gameObject, 3f); 
        // gameObject == 해당 컴포넌트를 갖고 있는 게임 오브젝트를 의미(Cube)
        // 3f => 3초 뒤에 자기자신을 파괴
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
