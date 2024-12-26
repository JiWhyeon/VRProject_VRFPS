using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MobCounterUI : MonoBehaviour
{
    int killCount; // 죽인 몹 개수
    int spawnCount; // 스폰된 몹 개수
    TextMeshProUGUI textUI; //텍스트 UI

    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateUI()
    {
        if (!enabled) // 비활성화 상태라면
            return; // 실행 중단

        // 텍스트 UI 내용 지정
        textUI.text = $"Kill/Alive/Spawn\n{killCount}/" + $"{spawnCount - killCount}/{spawnCount}";
    }

    private void OnEnable()
    {
        killCount = spawnCount = 0; //개수 초기화
        UpdateUI();
    }

    public void OnSpawn()
    {
        spawnCount++;
        UpdateUI();
    }

    public void OnKill()
    {
        killCount++; // 몹 제거 개수 증가
        UpdateUI();
    }
}
