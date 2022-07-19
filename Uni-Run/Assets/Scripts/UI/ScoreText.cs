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
        GameManager.Instance.OnScoreChanged.AddListener(UpdateText); //OnScoreChanged �̺�Ʈ�� ������. �̺�Ʈ���� ������ ���� UpdateText�� �����ϵ��� ��.
        //UpdateText���� �ʿ��� ������ OnScoreChanged���� <int> ������ ��������
    }

    public void UpdateText(int score) => ui.text = $"SCORE: {score}";

    private void OnDisable()
    {
        // ���� ���� �ʿ� ���� �̺�Ʈ ���� ����
        GameManager.Instance.OnScoreChanged.RemoveListener(UpdateText);
    }
}
