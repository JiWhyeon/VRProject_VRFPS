using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mob : MonoBehaviour
{
    public float destroyDelay = 1;
    bool isDestroyed = false;

    public UnityEvent OnCreated;
    public UnityEvent OnDestroyed;

    // Start is called before the first frame update
    private void Start()
    {
        OnCreated?.Invoke();

        // MobManager의 OnSpawned 호출
        MobManager.Instance.OnSpawned(this);

        //Invoke(nameof(Destroy), 3); //destroy 테스트
    }

    public void Destroy()
    {
        if(isDestroyed)
            return;
        isDestroyed = true;

        Destroy(gameObject, destroyDelay);
        OnDestroyed?.Invoke();

        // MobManager의 OnDestroyed 호출
        MobManager.Instance.OnDestroyed(this);
    }
}
