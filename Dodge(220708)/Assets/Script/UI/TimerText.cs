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
        ui.text = $"시간 : {(int)SurvivalTime} 초";
        IsOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsOn)
        {
            // 1초에 한번만 업데이트 되도록 하기
            currentTime += Time.deltaTime;
            if (currentTime >= 1f)
            {
                ++SurvivalTime;
                currentTime = 0f;

                ui.text = $"시간 : {(int)SurvivalTime} 초";
            }
        }

    }
}
