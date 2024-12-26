using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SurvivalTimeUI : MonoBehaviour
{
    float startTime; // 시작 시간
    TextMeshProUGUI textUI; // 텍스트 UI

    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        startTime = Time.time; // 현재 시간 지정
    }

    private void Update()
    {
        // 경과 시간 표시 (0.0s 표현 방식 : 소수점 한자리까지만 보인다)
        textUI.text = $"Survival Time\n{Time.time - startTime:0.0s}s"; 
    }
}
