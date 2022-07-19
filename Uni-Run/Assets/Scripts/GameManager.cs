using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class GameManager : SingletonBehaviour<GameManager> {

    public int ScoreIncreaseAmount = 1;
    //public GameObject GameoverUI; // 게임 오버시 활성화 할 UI 게임 오브젝트

    public UnityEvent<int> OnScoreChanged = new UnityEvent<int>(); // 옵저버 패턴 사용, 스코어 변경
    public event UnityAction<int> OnScoreChanged2;

    public UnityEvent OnGameOver = new UnityEvent();
    public event UnityAction OnGameOver2;
    
    public int Score 
    { 
        get 
        { 
            return currentScore; 
        } 
        
        set 
        { 
            currentScore = value; // value: 프로퍼티를 생성할 때 사용할 수 있음. 외부에서 gm.score = 10; 이면 score은 10이다. (gm.setScore(10)과 같음)
            OnScoreChanged.Invoke(currentScore); // 갱신 사실을 통지함
            OnScoreChanged2?.Invoke(currentScore);
        } 
    }

    private int currentScore = 0; // 게임 점수
    private bool isGameover = false; // 게임 오버 상태

    void Update() {
        // 게임 오버 상태에서 게임을 재시작할 수 있게 하는 처리
        if(isGameover)
        {
            if(Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(0);
                isGameover = false;
                Time.timeScale = 1f;
                Score = 0;
            }
        }
    }

    // 점수를 증가시키는 메서드
    public void AddScore() {
        Score += ScoreIncreaseAmount;
    }

    // 플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    public void OnPlayerDead() {
        Invoke("GameStop", 0.4f);
    }

    private void GameStop()
    {
        isGameover = true;
        OnGameOver.Invoke();
        OnGameOver2?.Invoke();
        Time.timeScale = 0f;
    }
}