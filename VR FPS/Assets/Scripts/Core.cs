using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Core : MonoBehaviour
{
    public int maxHP = 10; //최대 체력 
    int curHP; //현재 체력

    public UnityEvent<string> OnHpChanged; // 체력 변경 이벤트
    public UnityEvent OnHit; // 피격 이벤트
    public UnityEvent OnDestroy; //파괴 이벤트

    static Core instance; // 싱글톤 인스턴스 (실제)
    public static Core Instance // 싱글톤 인스턴스 (명목)
    {
        get
        {
            if(instance == null)
            {
                instance = FindAnyObjectByType<Core>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        curHP = maxHP; //현재 체력을 최대로 지정
        UpdateUI();
    }

    public void OnTriggerEnter(Collider other)
    {
        var mob = other.GetComponent<Mob>();
        if (mob != null)
        {
            OnHit?.Invoke(); //피격 이벤트 실행
            DecreasedHP(1); // 체력 1 감소
            mob.Destroy(); // 몹 제거
        }
    }

    private void DecreasedHP(int amount)
    {
        if (curHP <= 0) //현재 체력이 0 이하라면
            return; // 실행 중단

        curHP -= amount; //체력 감소
        if(curHP <= 0) //현재 체력이 0 이하라면
        {
            curHP = 0;
            OnDestroy?.Invoke(); //파괴 이벤트 실행
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        //체력 변경 이벤트 실행
        OnHpChanged?.Invoke($"HP: {curHP}");
    }
}
