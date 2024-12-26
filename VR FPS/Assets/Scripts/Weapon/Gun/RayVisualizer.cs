using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayVisualizer : MonoBehaviour
{
    [Header("[Ray]")]
    public LineRenderer ray;
    public LayerMask hitRayMask;
    public float distance = 100f;

    [Header("Reticle Point")]
    public GameObject reticlePoint;
    public bool showReticle = true;

    private void Awake()
    {
        Off();
    }

    public void On()
    {
        StopAllCoroutines();
        StartCoroutine(Process());
    }

    public void Off()
    {
        StopAllCoroutines();

        ray.enabled = false;
        reticlePoint.SetActive(false);
    }

    private IEnumerator Process()
    {
        while(true)
        {
            //총의 레이와 충돌하는 지점이 있다면
            if(Physics.Raycast(
                transform.position, transform.forward,
                out RaycastHit hitInfo, distance, hitRayMask))
            {
                //충돌 지점의 로컬 좌표를 선의 끝부분 위치로 지정
                ray.SetPosition(1, transform.InverseTransformPoint(hitInfo.point));
                ray.enabled = true ; //레이 보여주기

                //레이 끝 부분에 reticle 표시
                reticlePoint.transform.position = hitInfo.point;
                reticlePoint.SetActive(showReticle);
            }
            else
            {
                ray.enabled=false; //레이 감추기
                reticlePoint.SetActive(false); // reticle 감추기
            }

            yield return null;
        }
    }
}
