using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{
    // 1. 게임이 종료되면 GameOverUI를 보여줘야함.
    // 2. 재시작 안내를 해준다
    public TimerText Timer;
    public GameOverUI GameOverUI;

    private bool isOver;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // 만약 게임이 종료 되었고, R키를 눌렀다면 재시작
        if(isOver && Input.GetKeyDown(KeyCode.R))
        {
            // BuildSetting에서 인덱스를 확인해서 전환할 수도 있음
            // 혹은 씬 이름을 적을 수도 있음
            SceneManager.LoadScene(0);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        // 타이머 종료
        Timer.IsOn = false;

        // 데이터 갱신
        int bestTime = System.Math.Max(PlayerPrefs.GetInt("BestTime", 0), Timer.SurvivalTime);
        PlayerPrefs.SetInt("BestTime", bestTime);

        // 게임 UI 갱신
        GameOverUI.Activate(bestTime);

        // 게임 종료 true
        isOver = true;
    }
}
