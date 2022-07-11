using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverText;
    public Text timeText;
    public Text recordText;

    private float surviveTime;
    private bool isGameover;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        surviveTime = 0f;
        isGameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameover == false)
        {
            surviveTime += Time.deltaTime;
            timeText.text = $"Time: {(int)surviveTime}";
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        isGameover = true;
        gameOverText.SetActive(true);

        float bestScore = PlayerPrefs.GetFloat("BestTime");

        if(surviveTime > bestScore)
        {
            bestScore = surviveTime;
            PlayerPrefs.SetFloat("BestTime", bestScore);
        }

        recordText.text = $"BestTime: {(int)bestScore}";
    }
}
