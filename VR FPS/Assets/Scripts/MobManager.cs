using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MobManager : MonoBehaviour
{
    static MobManager instance; //싱글톤 인스턴스 (실제 사용)

    public static MobManager Instance // 싱글톤 인스턴스 (명목 사용)
    {
        get // 싱글톤 인스턴스 사용시 호출
        {
            if(instance == null) // instance가 널이면
                //인스턴스 얻기
                instance = FindObjectOfType<MobManager>();
            return instance;
        }
    }

    public UnityEvent<Mob> OnSpawn; // 몹 소환 이벤트
    public UnityEvent<Mob> OnDestroy; //몹 제거 이벤트
    List<Mob> mobs = new List<Mob>(); //몹 배열

    private void Awake()
    {
        instance = this; //인스턴스 지정, 싱글톤 사용할 때 항상 같이 추가
    }

    public void OnSpawned(Mob mob)
    {
        mobs.Add(mob); //몹 추가
        OnSpawn?.Invoke(mob); // 소환 이벤트 실행
    }

    public void OnDestroyed(Mob mob)
    {
        if(mobs.Remove(mob)) //제거할 몹이 있다면
        {
            OnDestroy?.Invoke(mob); // 제거 이벤트 실행
        }
    }

    public void DestroyAll()
    {
        while (mobs.Count>0) // 몹 개수가 0보다 크다면
            mobs[0]?.Destroy(); // 첫번째 몹이 아니면 제거
    }
}
