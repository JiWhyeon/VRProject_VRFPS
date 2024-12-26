using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    public LayerMask hittableMask;
    public GameObject hitEffectPrefab;
    public Transform shootPoint;

    public float shootDelay = 0.1f;
    public float maxDistance = 100f;

    public UnityEvent<Vector3> OnShootSuccess;
    public UnityEvent OnShootFail;

    private Magazine magazine; //Magazine 스트립트
    private void Awake()
    {
        magazine = GetComponent<Magazine>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Stop();
    }

   public void Play()
   {
        StopAllCoroutines();
        StartCoroutine(Process());
   }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private IEnumerator Process()
    {
        var wfs = new WaitForSeconds(shootDelay);

        while (true)
        {
            if (magazine.Use())//총알이 있다면
                Shoot(); //총알 발사
            else //총알이 없다면
                OnShootFail?.Invoke(); //실패 이벤트 실행
            yield return wfs; // 0.1초 대기
        }
    }

    private void Shoot()
    {
        //총의 레이와 충돌하는 지점이 있다면
        if (Physics.Raycast(
            shootPoint.position, shootPoint.forward,
            out RaycastHit hitInfo, maxDistance, hittableMask))
        {
            //충돌 이펙트 출력
            Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.identity);

            var hitObject = hitInfo.transform.GetComponent<Hittable>();
            hitObject?.Hit();

            OnShootSuccess?.Invoke(hitInfo.point); //OnShootSucess 이벤트 호출
        }
        else
        {
            //레이 끝 부분 위치 지정
            var hitPoint = shootPoint.position + shootPoint.forward * maxDistance;
            OnShootSuccess?.Invoke(hitPoint); //OnShoot Success 이벤트 호출
        }
    }
}
