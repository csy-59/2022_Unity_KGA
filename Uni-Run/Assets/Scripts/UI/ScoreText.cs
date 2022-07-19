using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI ui;

    private void Awake()
    {
        ui = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnScoreChanged.AddListener(UpdateText); //OnScoreChanged 이벤트에 구독함. 이벤트에서 통지가 오면 UpdateText를 실행하도록 함.
        //UpdateText에서 필요한 정보는 OnScoreChanged에서 <int> 형으로 전달해줌
    }

    public void UpdateText(int score) => ui.text = $"SCORE: {score}";

    private void OnDisable()
    {
        // 통지 받을 필요 없음 이벤트 구독 해재
        GameManager.Instance.OnScoreChanged.RemoveListener(UpdateText);
    }
}
