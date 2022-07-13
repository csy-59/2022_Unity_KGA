using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{
    // 1. ������ ����Ǹ� GameOverUI�� ���������.
    // 2. ����� �ȳ��� ���ش�
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
        // ���� ������ ���� �Ǿ���, RŰ�� �����ٸ� �����
        if(isOver && Input.GetKeyDown(KeyCode.R))
        {
            // BuildSetting���� �ε����� Ȯ���ؼ� ��ȯ�� ���� ����
            // Ȥ�� �� �̸��� ���� ���� ����
            SceneManager.LoadScene(0);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        // Ÿ�̸� ����
        Timer.IsOn = false;

        // ������ ����
        int bestTime = System.Math.Max(PlayerPrefs.GetInt("BestTime", 0), Timer.SurvivalTime);
        PlayerPrefs.SetInt("BestTime", bestTime);

        // ���� UI ����
        GameOverUI.Activate(bestTime);

        // ���� ���� true
        isOver = true;
    }
}
