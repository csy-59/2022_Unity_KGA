using UnityEngine;

// 발판으로서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour {
    private GameObject[] obstacles; // 장애물 오브젝트들
    private int obstaclesCount;
    private bool isStepped = false; // 플레이어 캐릭터가 밟았었는가

    private void Awake()
    {
        obstaclesCount = transform.childCount;
        obstacles = new GameObject[obstaclesCount];
        for(int i = 0; i < obstaclesCount; ++i)
        {
            obstacles[i] = transform.GetChild(i).gameObject;
        }
    }

    // 컴포넌트가 활성화될때 마다 매번 실행되는 메서드
    private void OnEnable() {
        // 발판을 리셋하는 처리
        isStepped = false;

        // 장애물을 활성화 비활성화 해야함. 확률을 알아서
        for (int i = 0; i < obstaclesCount; ++i)
        {
            int rand = Random.Range(0, 100);
            if(rand <= 24)
            {
                obstacles[i].SetActive(true);
            }
            else
            {
                obstacles[i].SetActive(false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // 플레이어 캐릭터가 자신을 밟았을때 점수를 추가하는 처리
        if(collision.gameObject.tag == "Player")
        {
            if (isStepped == false)
            {
                isStepped = true;
                GameManager.Instance.AddScore();
            }
        }    
    }
}