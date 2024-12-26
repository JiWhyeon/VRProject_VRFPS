using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReturnToTarget : MonoBehaviour
{
    public Transform target; // 목표지점
    public float duration = 1f; //거치대까지 이동하는 시간
    //이동 속도 커브 (시작/종료 : 빠름, 중간, 느림)
    public AnimationCurve curve = 
        AnimationCurve.EaseInOut(0f,0f,1f,1f);
    public UnityEvent OnCompleted; // 종료시 이벤트

    public void Call()
    {
        //Hierarchy에서 비활성 상태이면
        if (!gameObject.activeInHierarchy)
            return; //실행중단

        StopAllCoroutines(); //모든 코루틴 중단
        StartCoroutine(Process()); //Process 코루틴 실행
    }

    private IEnumerator Process()
    {
        if (target == null) //target이 null이면
            yield break; //실행중단

        var beginTime = Time.time; //현재시간으로 지정
        while (true)
        {
            //경과 시간을 비율로 계산 (0-1)
            var t = (Time.time - beginTime) / duration;
            if( t >= 1f) //거치애데 도착했으면
            {
                break; //이동중단
            }

            t = curve.Evaluate(t); //이동 커브를 이용한 위치 계산
            //현재 위치와 목표 위치를 t의 비율로 
            transform.position = Vector3.Lerp(transform.position, target.position, t);

            yield return null;
        }

        transform.position = target.position; // 목표 위치로 지정
        OnCompleted?.Invoke(); //OnCompleted 이벤트 있으면 실행
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
