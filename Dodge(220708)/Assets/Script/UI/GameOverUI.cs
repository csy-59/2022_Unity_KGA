using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI BestTimeUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(int bestTime)
    {
        gameObject.SetActive(true);
        BestTimeUI.text = $"최고 기록: {bestTime} 초";
    }
}
