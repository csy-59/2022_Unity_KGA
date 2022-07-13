using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerText : MonoBehaviour
{
    public int SurvivalTime { get; private set; }

    public bool IsOn { get; set; }

    private float currentTime = 0f;
    private TextMeshProUGUI ui;

    // Start is called before the first frame update
    void Start()
    {
        ui = GetComponent<TextMeshProUGUI>();
        ui.text = $"�ð� : {(int)SurvivalTime} ��";
        IsOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsOn)
        {
            // 1�ʿ� �ѹ��� ������Ʈ �ǵ��� �ϱ�
            currentTime += Time.deltaTime;
            if (currentTime >= 1f)
            {
                ++SurvivalTime;
                currentTime = 0f;

                ui.text = $"�ð� : {(int)SurvivalTime} ��";
            }
        }

    }
}
