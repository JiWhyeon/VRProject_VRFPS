using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Magazine : MonoBehaviour, IReloadable
{
    public int maxBullets = 20; //총알 최대 개수
    public float chargingTime = 2f; //충전 시간 (2초)

    private int currentBullets; //현재 총알 개수

    private int CurrentBullets  //현재 총알 개수
    {
        get => currentBullets; //값을 얻어올 때 currentBullets 적용
        set //값을 지정할 때
        {
            if(value < 0) //지정된 값이 0 미만의 값이면
                currentBullets = 0;
            else if(value > maxBullets) //지정된 값이 maxBullets 보다 크면
                currentBullets = maxBullets;
            else //지정된 값을 그대로 적용
                currentBullets = value;

            //OnBulletsChanged 이벤트가 있으면 실행
            OnBulletsChanged?.Invoke(currentBullets);
            //OnChargeChanged 이벤트가 있으면 실행
            OnChargeChanged?.Invoke((float)currentBullets/maxBullets);
        }
    }

    public UnityEvent OnReloadStart; // 재장전 시작 이벤트
    public UnityEvent OnReloadEnd; //재장전 종료 이벤트

    public UnityEvent<int> OnBulletsChanged; //총알 개수 변경 이벤트
    public UnityEvent<float> OnChargeChanged; //총 충전 이벤트

    private void Start()
    {
        CurrentBullets = maxBullets; //총알 개수를 maxBullets으로 지정
    }

    public bool Use(int amount = 1)
    {
        if(CurrentBullets >= amount) //총알 개수가 amount 이상이면 
        {
            CurrentBullets -= amount; //통알 개수를 amount만큼 감소
            return true;
        }
        else
        {
            return false;
        }
    }

    //임시 재장전 기능 추가
    //[ContextMenu("Reload")]
    public void StartReload()
    {
        //현재 총알이 최대치이면
        if(currentBullets == maxBullets)
            return; //실행중단
        
        //모든 코르틴 종료
        StopAllCoroutines(); //모든 코르틴을 종료하기보다, 특정 코루틴을 종료하는 것이 안정하다.
        //ReloadProcess 코루틴 실행
        StartCoroutine(ReloadProcess());
    }

    public void StopReload()
    {
        //모든 코루틴 종료
        StopAllCoroutines();
    }

    private IEnumerator ReloadProcess()
    {
        OnReloadStart?.Invoke(); //재장전 시작 이벤트 실행
        var beginTime = Time.time; //현재 시간 지정
        var beginBullets = currentBullets; //총알 개수 지정
        //총알 비율 계산(0~1)
        var enoughPercent = 1f - ((float)currentBullets / maxBullets);
        //충전 시간 계산
        var enoughChargingTime = chargingTime * enoughPercent;

        while(true)
        {
            //충전 상태 계산(0~1)
            var t = (Time.time - beginTime) / enoughChargingTime;
            if (t >= 1f) //완충되었으면
                break; //충전 중단행
            //충전된 총알 개수 지정
            CurrentBullets = (int)Mathf.Lerp(beginBullets, maxBullets, t);
            yield return null;

        }

        CurrentBullets = maxBullets; //총알 개수를 최대치로 지정
        OnReloadEnd?.Invoke(); //재장전 종료 이벤트 실행

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
