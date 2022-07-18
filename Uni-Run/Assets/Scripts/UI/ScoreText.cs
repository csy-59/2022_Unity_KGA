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

    public void UpdateText(int score) => ui.text = $"SCORE: {score}";
}
