using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour {
    public GameObject PlatformPrefab; // 생성할 발판의 원본 프리팹
    public int MaxPlatformCount = 3; // 생성할 발판의 개수

    public float MinSpawnCooltime = 1.25f; // 다음 배치까지의 시간 간격 최솟값
    public float MaxSpawnCooltime = 2.25f; // 다음 배치까지의 시간 간격 최댓값
    private float nextSpawnCooltime; // 다음 배치까지의 시간 간격
    private float currentCooltime; // 마지막 배치 시점

    public float MinY = -3.5f; // 배치할 위치의 최소 y값
    public float MaxY = 1.5f; // 배치할 위치의 최대 y값
    private float xPos = 20f; // 배치할 위치의 x 값

    private GameObject[] platforms; // 미리 생성한 발판들
    private int nextSpawnPlatformIndex = 0; // 사용할 현재 순번의 발판

    private readonly Vector2 poolPosition = new Vector2(0, -20); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치


    void Start() {
        platforms = new GameObject[MaxPlatformCount];

        // 변수들을 초기화하고 사용할 발판들을 미리 생성
        for(int i = 0; i < MaxPlatformCount; ++i)
        {
            platforms[i] = Instantiate(PlatformPrefab, poolPosition, Quaternion.identity);
            platforms[i].SetActive(false);
        }
    }

    void Update() {
        // 순서를 돌아가며 주기적으로 발판을 배치
        // 1. 배치 쿨타임이 다 찼는지
        currentCooltime += Time.deltaTime;
        if(currentCooltime >= nextSpawnCooltime)
        {
            currentCooltime = 0f;
            nextSpawnCooltime = Random.Range(MinSpawnCooltime, MaxSpawnCooltime);

            // 2. 배치 쿨타임이 다 찼다면 랜덤하게 발판 배치
            Vector2 spawnPosition = new Vector2(xPos, Random.Range(MinY, MaxY));
            GameObject currentPlatform = platforms[nextSpawnPlatformIndex];
            //currentPlatform.SetActive(false);
            currentPlatform.transform.position = spawnPosition;
            currentPlatform.SetActive(true);

            nextSpawnPlatformIndex = (nextSpawnPlatformIndex + 1) % MaxPlatformCount;
        }
    }
}