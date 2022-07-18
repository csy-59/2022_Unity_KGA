using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : SingletonBehaviour<GameManager> {

    public int ScoreIncreaseAmount = 1;
    public ScoreText scoreText; // 점수를 출력할 UI 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화 할 UI 게임 오브젝트

    private int score = 0; // 게임 점수
    private bool isGameover = false; // 게임 오버 상태

    void Update() {
        // 게임 오버 상태에서 게임을 재시작할 수 있게 하는 처리
        if(isGameover)
        {
            if(Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
                isGameover = false;
                Time.timeScale = 1f;
            }
            return;
        }

    }

    // 점수를 증가시키는 메서드
    public void AddScore() {
        score += ScoreIncreaseAmount;
        if (scoreText == null)
            scoreText = FindObjectOfType<ScoreText>();
        scoreText.UpdateText(score);
    }

    // 플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    public void OnPlayerDead() {
        isGameover = true;
        gameoverUI.SetActive(true);
        Invoke("StopTime", 0.4f);
    }

    private void StopTime()
    {
        Time.timeScale = 0f;
    }
}